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
    /// 월별영업비용명세서 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.08.16 11:00
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28GBE412 : 월별영업비용명세서
    ///  TY_P_AC_28G7P418 : 월별영업비용명세서-사업부
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28G5J414 : 월별영업비용명세서
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GCDAC : 계정코드
    ///  GCDDP : 사업장코드
    ///  GLIST : 영업비용사업부
    ///  GPRTGN : 출력구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACBJ030S : TYBase
    {
        #region Description : 페이지 로드
        public TYACBJ030S()
        {
            InitializeComponent();
        }

        private void TYACBJ030S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sCDDP = string.Empty;

            if (this.CBO01_GCDAC.GetValue().ToString() == "0")
            {
                //this.ShowMessage("TY_M_AC_2C134777");

                //SetFocus(CBO01_GCDAC);
                //return;
            }
            else if (this.CBO01_GCDAC.GetValue().ToString() != "1")
            {
                if (this.CBO01_GLIST.GetValue().ToString() == "0")
                {
                    this.ShowMessage("TY_M_AC_2C134776");
                    SetFocus(CBO01_GLIST);
                    return;
                }

                if (this.CBO01_GCDDP.GetText() != "")
                {
                    sCDDP = this.CBO01_GCDDP.GetText().ToString() + "-";
                }
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_28GBE412",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue(),
                this.CBO01_GCDAC.GetValue(),
                this.CBO01_GLIST.GetValue(),
                this.CBO01_GCDDP.GetValue(),
                this.CBO01_GPRTGN.GetValue(),
                sCDDP.ToString()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_28G5J414.SetValue(UP_ConvertDt(dt));

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_28G5J414.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_28G5J414.GetValue(i, "ABAC").ToString() == "합   계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_AC_28G5J414.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                    }
                }
            }
            else
            {
                this.FPS91_TY_S_AC_28G5J414.SetValue(dt);

                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sCDDP = string.Empty;

            if (this.CBO01_GCDAC.GetValue().ToString() == "0")
            {
                //this.ShowMessage("TY_M_AC_2C134777");

                //SetFocus(CBO01_GCDAC);
                //return;
            }
            else if (this.CBO01_GCDAC.GetValue().ToString() != "1")
            {
                if (this.CBO01_GLIST.GetValue().ToString() == "0")
                {
                    this.ShowMessage("TY_M_AC_2C134776");
                    SetFocus(CBO01_GLIST);
                    return;
                }

                if (this.CBO01_GCDDP.GetText() != "")
                {
                    sCDDP = this.CBO01_GCDDP.GetText().ToString() + "-";
                }
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_28GBE412",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue(),
                this.CBO01_GCDAC.GetValue(),
                this.CBO01_GLIST.GetValue(),
                this.CBO01_GCDDP.GetValue(),
                this.CBO01_GPRTGN.GetValue(),
                sCDDP.ToString()
                );

            SectionReport rpt = new TYACBJ030R();

            // 가로 출력
            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {

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
            string sCB2DTAC = string.Empty;
            string sCB2CDAC = string.Empty;

            double dSUM01_TOT = 0;
            double dSUM02_TOT = 0;
            double dSUM03_TOT = 0;
            double dSUM04_TOT = 0;
            double dSUM05_TOT = 0;
            double dSUM06_TOT = 0;
            double dSUM07_TOT = 0;
            double dSUM08_TOT = 0;
            double dSUM09_TOT = 0;
            double dSUM10_TOT = 0;
            double dSUM11_TOT = 0;
            double dSUM12_TOT = 0;
            double dHAP_TOT   = 0;

            DataTable Condt = new DataTable();

            DataRow row;

            Condt.Columns.Add("CDAC",    typeof(System.String));
            Condt.Columns.Add("ABAC",    typeof(System.String));
            Condt.Columns.Add("A1TAG01", typeof(System.String));
            Condt.Columns.Add("SUM01",   typeof(System.String));
            Condt.Columns.Add("SUM02",   typeof(System.String));
            Condt.Columns.Add("SUM03",   typeof(System.String));
            Condt.Columns.Add("SUM04",   typeof(System.String));
            Condt.Columns.Add("SUM05",   typeof(System.String));
            Condt.Columns.Add("SUM06",   typeof(System.String));
            Condt.Columns.Add("SUM07",   typeof(System.String));
            Condt.Columns.Add("SUM08",   typeof(System.String));
            Condt.Columns.Add("SUM09",   typeof(System.String));
            Condt.Columns.Add("SUM10",   typeof(System.String));
            Condt.Columns.Add("SUM11",   typeof(System.String));
            Condt.Columns.Add("SUM12",   typeof(System.String));
            Condt.Columns.Add("HAP",     typeof(System.String));
            Condt.Columns.Add("CB2DTAC", typeof(System.String));
            Condt.Columns.Add("CB2CDAC", typeof(System.String));

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                if (dt.Rows[i]["CDAC"].ToString().Substring(6, 2) == "00")
                {
                    dSUM01_TOT = dSUM01_TOT + double.Parse(dt.Rows[i]["SUM01"].ToString());
                    dSUM02_TOT = dSUM02_TOT + double.Parse(dt.Rows[i]["SUM02"].ToString());
                    dSUM03_TOT = dSUM03_TOT + double.Parse(dt.Rows[i]["SUM03"].ToString());
                    dSUM04_TOT = dSUM04_TOT + double.Parse(dt.Rows[i]["SUM04"].ToString());
                    dSUM05_TOT = dSUM05_TOT + double.Parse(dt.Rows[i]["SUM05"].ToString());
                    dSUM06_TOT = dSUM06_TOT + double.Parse(dt.Rows[i]["SUM06"].ToString());
                    dSUM07_TOT = dSUM07_TOT + double.Parse(dt.Rows[i]["SUM07"].ToString());
                    dSUM08_TOT = dSUM08_TOT + double.Parse(dt.Rows[i]["SUM08"].ToString());
                    dSUM09_TOT = dSUM09_TOT + double.Parse(dt.Rows[i]["SUM09"].ToString());
                    dSUM10_TOT = dSUM10_TOT + double.Parse(dt.Rows[i]["SUM10"].ToString());
                    dSUM11_TOT = dSUM11_TOT + double.Parse(dt.Rows[i]["SUM11"].ToString());
                    dSUM12_TOT = dSUM12_TOT + double.Parse(dt.Rows[i]["SUM12"].ToString());
                    dHAP_TOT   = dHAP_TOT + double.Parse(dt.Rows[i]["HAP"].ToString());
                }

                sCB2DTAC = dt.Rows[i]["CB2DTAC"].ToString();
                sCB2CDAC = dt.Rows[i]["CB2CDAC"].ToString();

                row = Condt.NewRow();

                row["CDAC"] = dt.Rows[i]["CDAC"].ToString();

                if (dt.Rows[i]["CDAC"].ToString().Substring(6, 2) == "00")
                {
                    row["ABAC"] = dt.Rows[i]["ABAC"].ToString();
                }
                else
                {
                    row["ABAC"] = "   " + dt.Rows[i]["ABAC"].ToString();
                }
                row["A1TAG01"] = dt.Rows[i]["A1TAG01"].ToString();
                row["SUM01"]   = string.Format("{0:#,##0}", dt.Rows[i]["SUM01"].ToString());
                row["SUM02"]   = string.Format("{0:#,##0}", dt.Rows[i]["SUM02"].ToString());
                row["SUM03"]   = string.Format("{0:#,##0}", dt.Rows[i]["SUM03"].ToString());
                row["SUM04"]   = string.Format("{0:#,##0}", dt.Rows[i]["SUM04"].ToString());
                row["SUM05"]   = string.Format("{0:#,##0}", dt.Rows[i]["SUM05"].ToString());
                row["SUM06"]   = string.Format("{0:#,##0}", dt.Rows[i]["SUM06"].ToString());
                row["SUM07"]   = string.Format("{0:#,##0}", dt.Rows[i]["SUM07"].ToString());
                row["SUM08"]   = string.Format("{0:#,##0}", dt.Rows[i]["SUM08"].ToString());
                row["SUM09"]   = string.Format("{0:#,##0}", dt.Rows[i]["SUM09"].ToString());
                row["SUM10"]   = string.Format("{0:#,##0}", dt.Rows[i]["SUM10"].ToString());
                row["SUM11"]   = string.Format("{0:#,##0}", dt.Rows[i]["SUM11"].ToString());
                row["SUM12"]   = string.Format("{0:#,##0}", dt.Rows[i]["SUM12"].ToString());
                row["HAP"]     = string.Format("{0:#,##0}", dt.Rows[i]["HAP"].ToString());
                row["CB2DTAC"] = sCB2DTAC.ToString();
                row["CB2CDAC"] = sCB2CDAC.ToString();

                Condt.Rows.Add(row);
            }

            // 합계
            row = Condt.NewRow();

            row["CDAC"] = "99999";

            row["ABAC"] = "합   계";
            
            row["A1TAG01"] = "";
            row["SUM01"]   = string.Format("{0:#,##0}", dSUM01_TOT);
            row["SUM02"]   = string.Format("{0:#,##0}", dSUM02_TOT);
            row["SUM03"]   = string.Format("{0:#,##0}", dSUM03_TOT);
            row["SUM04"]   = string.Format("{0:#,##0}", dSUM04_TOT);
            row["SUM05"]   = string.Format("{0:#,##0}", dSUM05_TOT);
            row["SUM06"]   = string.Format("{0:#,##0}", dSUM06_TOT);
            row["SUM07"]   = string.Format("{0:#,##0}", dSUM07_TOT);
            row["SUM08"]   = string.Format("{0:#,##0}", dSUM08_TOT);
            row["SUM09"]   = string.Format("{0:#,##0}", dSUM09_TOT);
            row["SUM10"]   = string.Format("{0:#,##0}", dSUM10_TOT);
            row["SUM11"]   = string.Format("{0:#,##0}", dSUM11_TOT);
            row["SUM12"]   = string.Format("{0:#,##0}", dSUM12_TOT);
            row["HAP"]     = string.Format("{0:#,##0}",   dHAP_TOT);
            row["CB2DTAC"] = sCB2DTAC.ToString();
            row["CB2CDAC"] = sCB2CDAC.ToString();

            Condt.Rows.Add(row);

            return Condt;
        }
        #endregion

        #region Description : 사업부 콤보박스
        private void CBO01_GLIST_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_28G7P418",
                this.CBO01_GLIST.GetValue().ToString()
                );

            // 콤보박스에 바인드
            this.CBO01_GCDDP.DataBind(this.DbConnector.ExecuteDataTable());
        }
        #endregion
    }
}