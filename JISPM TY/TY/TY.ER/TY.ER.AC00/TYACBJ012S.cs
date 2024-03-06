using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 총계정원장출력 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.08.02 09:46
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2822N311 : 총계정원장출력
    ///  TY_P_AC_2832Z317 : 총계정원장 월계
    ///  TY_P_AC_28330318 : 총계정원장 누계
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2822P313 : 총계정원장 출력
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GEDCDAC : 계정코드
    ///  GSTCDAC : 계정코드
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACBJ012S : TYBase
    {
        #region Description : 페이지 로드
        public TYACBJ012S()
        {
            InitializeComponent();
        }

        private void TYACBJ012S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.CBH01_GSTCDAC.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_2822N311",
                TYUserInfo.EmpNo.ToString(),
                this.CBH01_GSTCDAC.GetValue(),
                this.CBH01_GEDCDAC.GetValue(),
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_2822P313.SetValue(UP_ConvertDt(dt));

                // 특정 ROW 잠금
                for (int i = 0; i < this.FPS91_TY_S_AC_2822P313.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_2822P313.GetValue(i, "DATE").ToString() == "월     계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_AC_2822P313.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                    else if (this.FPS91_TY_S_AC_2822P313.GetValue(i, "DATE").ToString() == "누     계")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_AC_2822P313.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
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
                "TY_P_AC_2822N311",
                TYUserInfo.EmpNo.ToString(),
                this.CBH01_GSTCDAC.GetValue(),
                this.CBH01_GEDCDAC.GetValue(),
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue()
                );

            SectionReport rpt = new TYACBJ012R();

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
            int i = 0;
            
            DataTable table  = new DataTable();
            DataTable dt_WOL = new DataTable();
            DataTable dt_NU  = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["DATE"].ToString() != table.Rows[i]["DATE"].ToString())
                {
                    if (table.Rows[i - 1]["TMCDAC1"].ToString() == table.Rows[i]["TMCDAC1"].ToString())
                    {
                        table.Rows[i]["TMAMJAN"] = (Convert.ToDouble(table.Rows[i - 1]["TMAMJAN"].ToString())
                                                 + Convert.ToDouble(table.Rows[i]["TMAMDR"].ToString())
                                                 - Convert.ToDouble(table.Rows[i]["TMAMCR"].ToString()));
                    }
                }

                if (table.Rows[i - 1]["WOL"].ToString() != table.Rows[i]["WOL"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    this.DbConnector.CommandClear();

                    // 월계
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_2832Z317",
                        TYUserInfo.EmpNo.ToString(),
                        table.Rows[i - 1]["WOL"].ToString(),
                        table.Rows[i - 1]["TMCDAC1"].ToString()
                        );

                    dt_WOL = this.DbConnector.ExecuteDataTable();

                    table.Rows[i]["DATE"]    = "월     계";
                    table.Rows[i]["TMCDAC"]  = table.Rows[i - 1]["TMCDAC"].ToString();
                    table.Rows[i]["A1NMAC"]  = table.Rows[i - 1]["A1NMAC"].ToString();
                    table.Rows[i]["TMCDAC1"] = table.Rows[i - 1]["TMCDAC1"].ToString();

                    // 차   변
                    table.Rows[i]["TMAMDR"]  = Get_Numeric(dt_WOL.Rows[0]["TMAMDR"].ToString());
                    // 대   변
                    table.Rows[i]["TMAMCR"]  = Get_Numeric(dt_WOL.Rows[0]["TMAMCR"].ToString());

                    table.Rows[i]["FROMMM"]  = table.Rows[i - 1]["FROMMM"].ToString();
                    table.Rows[i]["TOMM"]    = table.Rows[i - 1]["TOMM"].ToString();

                    nNum = nNum + 1;

                    i = i + 1;
                }

                if (table.Rows[i - 1]["TMCDAC1"].ToString() != table.Rows[i]["TMCDAC1"].ToString())
                {
                    if (this.DTP01_GSTYYMM.GetValue().ToString() == this.DTP01_GEDYYMM.GetValue().ToString())
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        this.DbConnector.CommandClear();

                        // 월계
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_2832Z317",
                            TYUserInfo.EmpNo.ToString(),
                            table.Rows[i - 1]["WOL"].ToString(),
                            table.Rows[i - 1]["TMCDAC1"].ToString()
                            );

                        dt_WOL = this.DbConnector.ExecuteDataTable();

                        table.Rows[i]["DATE"]    = "월     계";
                        table.Rows[i]["TMCDAC"]  = table.Rows[i - 1]["TMCDAC"].ToString();
                        table.Rows[i]["A1NMAC"]  = table.Rows[i - 1]["A1NMAC"].ToString();
                        table.Rows[i]["TMCDAC1"] = table.Rows[i - 1]["TMCDAC1"].ToString();

                        // 차   변
                        table.Rows[i]["TMAMDR"] = Get_Numeric(dt_WOL.Rows[0]["TMAMDR"].ToString());
                        // 대   변
                        table.Rows[i]["TMAMCR"] = Get_Numeric(dt_WOL.Rows[0]["TMAMCR"].ToString());

                        table.Rows[i]["FROMMM"] = table.Rows[i - 1]["FROMMM"].ToString();
                        table.Rows[i]["TOMM"]   = table.Rows[i - 1]["TOMM"].ToString();

                        nNum = nNum + 1;

                        i = i + 1;
                    }
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    this.DbConnector.CommandClear();

                    // 누계
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_28330318",
                        TYUserInfo.EmpNo.ToString(),
                        table.Rows[i - 1]["TMCDAC1"].ToString()
                        );

                    dt_WOL = this.DbConnector.ExecuteDataTable();

                    table.Rows[i]["DATE"]    = "누     계";
                    table.Rows[i]["TMCDAC"]  = table.Rows[i - 1]["TMCDAC"].ToString();
                    table.Rows[i]["A1NMAC"]  = table.Rows[i - 1]["A1NMAC"].ToString();
                    table.Rows[i]["TMCDAC1"] = table.Rows[i - 1]["TMCDAC1"].ToString();

                    // 차   변
                    table.Rows[i]["TMAMDR"]  = Get_Numeric(dt_WOL.Rows[0]["TMAMDR"].ToString());
                    // 대   변
                    table.Rows[i]["TMAMCR"]  = Get_Numeric(dt_WOL.Rows[0]["TMAMCR"].ToString());
                    // 잔   액
                    table.Rows[i]["TMAMJAN"] = Get_Numeric(dt_WOL.Rows[0]["TMAMJAN"].ToString());

                    table.Rows[i]["FROMMM"]  = table.Rows[i - 1]["FROMMM"].ToString();
                    table.Rows[i]["TOMM"]    = table.Rows[i - 1]["TOMM"].ToString();

                    nNum = nNum + 1;

                    i = i + 1;
                }
            }

            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            this.DbConnector.CommandClear();

            // 월계
            this.DbConnector.Attach
                (
                "TY_P_AC_2832Z317",
                TYUserInfo.EmpNo.ToString(),
                table.Rows[i - 1]["WOL"].ToString(),
                table.Rows[i - 1]["TMCDAC1"].ToString()
                );

            dt_WOL = this.DbConnector.ExecuteDataTable();

            table.Rows[i]["DATE"]    = "월     계";
            table.Rows[i]["TMCDAC"]  = table.Rows[i - 1]["TMCDAC"].ToString();
            table.Rows[i]["A1NMAC"]  = table.Rows[i - 1]["A1NMAC"].ToString();
            table.Rows[i]["TMCDAC1"] = table.Rows[i - 1]["TMCDAC1"].ToString();

            // 차   변
            table.Rows[i]["TMAMDR"]  = Get_Numeric(dt_WOL.Rows[0]["TMAMDR"].ToString());
            // 대   변
            table.Rows[i]["TMAMCR"]  = Get_Numeric(dt_WOL.Rows[0]["TMAMCR"].ToString());

            table.Rows[i]["FROMMM"]  = table.Rows[i - 1]["FROMMM"].ToString();
            table.Rows[i]["TOMM"]    = table.Rows[i - 1]["TOMM"].ToString();

            i = i + 1;

            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            this.DbConnector.CommandClear();

            // 누계
            this.DbConnector.Attach
                (
                "TY_P_AC_28330318",
                TYUserInfo.EmpNo.ToString(),
                table.Rows[i - 1]["TMCDAC1"].ToString()
                );

            dt_WOL = this.DbConnector.ExecuteDataTable();

            table.Rows[i]["DATE"]    = "누     계";
            table.Rows[i]["TMCDAC"]  = table.Rows[i - 1]["TMCDAC"].ToString();
            table.Rows[i]["A1NMAC"]  = table.Rows[i - 1]["A1NMAC"].ToString();
            table.Rows[i]["TMCDAC1"] = table.Rows[i - 2]["TMCDAC1"].ToString();

            // 차   변
            table.Rows[i]["TMAMDR"]  = Get_Numeric(dt_WOL.Rows[0]["TMAMDR"].ToString());
            // 대   변
            table.Rows[i]["TMAMCR"]  = Get_Numeric(dt_WOL.Rows[0]["TMAMCR"].ToString());
            // 잔   액
            table.Rows[i]["TMAMJAN"] = Get_Numeric(dt_WOL.Rows[0]["TMAMJAN"].ToString());

            table.Rows[i]["FROMMM"]  = table.Rows[i - 1]["FROMMM"].ToString();
            table.Rows[i]["TOMM"]    = table.Rows[i - 1]["TOMM"].ToString();

            return table;
        }
        #endregion
    }
}