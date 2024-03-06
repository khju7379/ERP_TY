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
    /// 일일어음입력 명세조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.08.24 15:11
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28OB9539 : 일일어음입력 명세조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28OBP540 : 일일어음입력 명세조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  E6CDCL : 거래처코드
    ///  E6CDSO : 발생부서
    ///  E6GUBUN : 어음구분
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACEI011S : TYBase
    {
        #region Description : 페이지 로드
        public TYACEI011S()
        {
            InitializeComponent();
        }

        private void TYACEI011S_Load(object sender, System.EventArgs e)
        {
            this.CBH01_E6CDSO.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            SetStartingFocus(this.DTP01_GSTDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_28OB9539",
                this.DTP01_GSTDATE.GetValue(),
                this.DTP01_GEDDATE.GetValue(),
                this.CBH01_E6CDSO.GetValue(),
                this.CBH01_E6CDCL.GetValue(),
                this.CBO01_E6GUBUN.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_28OBP540.SetValue(UP_ConvertDt(dt,"SEL"));

                // 특정 ROW 색깔 입히기
                for (int i = 0; i < this.FPS91_TY_S_AC_28OBP540.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_28OBP540.GetValue(i, "E6DTCO").ToString() == "일자계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_28OBP540.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                    else if (this.FPS91_TY_S_AC_28OBP540.GetValue(i, "E6DTCO").ToString() == "부서계")
                    {
                        // 특정 ROW 색깔 입히기
                        this.FPS91_TY_S_AC_28OBP540.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
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
                "TY_P_AC_28OB9539",
                this.DTP01_GSTDATE.GetValue(),
                this.DTP01_GEDDATE.GetValue(),
                this.CBH01_E6CDSO.GetValue(),
                this.CBH01_E6CDCL.GetValue(),
                this.CBO01_E6GUBUN.GetValue()
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYACEI011R();

                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, UP_ConvertDt(dt,"PRT"))).ShowDialog();
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
            string sNEWE6CDSO = string.Empty;
            string sOLDE6CDSO = string.Empty;
            string sNEWDT     = string.Empty;
            string sOLDDT     = string.Empty;

            string sNEWE6DTCO = string.Empty;
            string sOLDE6DTCO = string.Empty;

            int iCount = 0;
            int iHap = 0;

            double dE6AMNR = 0;

            DataTable Condt = new DataTable();

            DataRow row;

            Condt.Columns.Add("E6CDSO",   typeof(System.String));
            Condt.Columns.Add("DT",       typeof(System.String));
            Condt.Columns.Add("E6DTCO",   typeof(System.String));
            Condt.Columns.Add("VNSANGHO", typeof(System.String));
            Condt.Columns.Add("E6DTED",   typeof(System.String));
            Condt.Columns.Add("E6NONR",   typeof(System.String));
            Condt.Columns.Add("E6AMNR",   typeof(System.String));
            Condt.Columns.Add("BK",       typeof(System.String));
            Condt.Columns.Add("BG",       typeof(System.String));
            Condt.Columns.Add("BS",       typeof(System.String));
            Condt.Columns.Add("E6NOIS",   typeof(System.String));

            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                sNEWE6CDSO = dt.Rows[i]["E6CDSO"].ToString();
                sNEWDT     = dt.Rows[i]["DT"].ToString();

                sNEWE6DTCO = dt.Rows[i]["E6DTCO"].ToString();

                if (sOLDE6CDSO == "")
                {
                    sOLDE6CDSO = sNEWE6CDSO.ToString();
                    sOLDDT     = sNEWDT;

                    row = Condt.NewRow();

                    row["E6CDSO"]   = dt.Rows[i]["E6CDSO"].ToString();
                    row["DT"]       = dt.Rows[i]["DT"].ToString();

                    if (sNEWE6DTCO != sOLDE6DTCO)
                    {
                        row["E6DTCO"] = dt.Rows[i]["E6DTCO"].ToString();

                        sOLDE6DTCO = sNEWE6DTCO.ToString();
                    }
                    else
                    {
                        row["E6DTCO"] = "";
                    }
                    
                    row["VNSANGHO"] = dt.Rows[i]["VNSANGHO"].ToString();
                    row["E6DTED"]   = dt.Rows[i]["E6DTED"].ToString();
                    if (dt.Rows[i]["E6DTCO"].ToString() == "일자계")
                    {
                        row["E6NONR"] = Convert.ToString(iCount) + "매";
                    }
                    else
                    {
                        row["E6NONR"] = dt.Rows[i]["E6NONR"].ToString();
                    }
                    row["E6AMNR"]   = dt.Rows[i]["E6AMNR"].ToString();
                    row["BK"]       = dt.Rows[i]["BK"].ToString();
                    row["BG"]       = dt.Rows[i]["BG"].ToString();
                    row["BS"]       = dt.Rows[i]["BS"].ToString();
                    row["E6NOIS"]   = dt.Rows[i]["E6NOIS"].ToString();

                    Condt.Rows.Add(row);

                }
                else
                {
                    if (sNEWE6CDSO == sOLDE6CDSO)
                    {
                        row = Condt.NewRow();

                        if (sGUBUN == "SEL")
                        {
                            row["E6CDSO"] = "";
                            row["DT"]     = "";
                        }
                        else
                        {
                            row["E6CDSO"] = sOLDE6CDSO.ToString();
                            row["DT"]     = sOLDDT.ToString();
                        }

                        if (sNEWE6DTCO != sOLDE6DTCO)
                        {
                            row["E6DTCO"] = dt.Rows[i]["E6DTCO"].ToString();

                            sOLDE6DTCO = sNEWE6DTCO.ToString();
                        }
                        else
                        {
                            row["E6DTCO"] = "";
                        }

                        row["VNSANGHO"] = dt.Rows[i]["VNSANGHO"].ToString();
                        row["E6DTED"]   = dt.Rows[i]["E6DTED"].ToString();
                        if (dt.Rows[i]["E6DTCO"].ToString() == "일자계")
                        {
                            row["E6NONR"] = Convert.ToString(iCount) + "매";
                        }
                        else
                        {
                            row["E6NONR"] = dt.Rows[i]["E6NONR"].ToString();
                        }
                        row["E6AMNR"]   = dt.Rows[i]["E6AMNR"].ToString();
                        row["BK"]       = dt.Rows[i]["BK"].ToString();
                        row["BG"]       = dt.Rows[i]["BG"].ToString();
                        row["BS"]       = dt.Rows[i]["BS"].ToString();
                        row["E6NOIS"]   = dt.Rows[i]["E6NOIS"].ToString();

                        Condt.Rows.Add(row);
                    }
                    else
                    {
                        // 부서 총계
                        row = Condt.NewRow();

                        if (sGUBUN == "SEL")
                        {
                            row["E6CDSO"] = "";
                            row["DT"]     = "";
                        }
                        else
                        {
                            row["E6CDSO"] = sOLDE6CDSO.ToString();
                            row["DT"]     = sOLDDT.ToString();
                        }
                        row["E6DTCO"]   = "부서계";
                        row["VNSANGHO"] = "";
                        row["E6DTED"]   = "";
                        row["E6NONR"]   = Convert.ToString(iHap) + "매";
                        row["E6AMNR"]   = string.Format("{0:#,##0}", dE6AMNR);
                        row["BK"]       = "";
                        row["BG"]       = "";
                        row["BS"]       = "";
                        row["E6NOIS"]   = "";

                        Condt.Rows.Add(row);

                        iHap = 0;

                        dE6AMNR = 0;

                        row = Condt.NewRow();

                        row["E6CDSO"]   = dt.Rows[i]["E6CDSO"].ToString();
                        row["DT"]       = dt.Rows[i]["DT"].ToString();
                        row["E6DTCO"]   = dt.Rows[i]["E6DTCO"].ToString();
                        row["VNSANGHO"] = dt.Rows[i]["VNSANGHO"].ToString();
                        row["E6DTED"]   = dt.Rows[i]["E6DTED"].ToString();
                        if (dt.Rows[i]["E6DTCO"].ToString() == "일자계")
                        {
                            row["E6NONR"] = Convert.ToString(iCount) + "매";
                        }
                        else
                        {
                            row["E6NONR"] = dt.Rows[i]["E6NONR"].ToString();
                        }
                        row["E6AMNR"]   = dt.Rows[i]["E6AMNR"].ToString();
                        row["BK"]       = dt.Rows[i]["BK"].ToString();
                        row["BG"]       = dt.Rows[i]["BG"].ToString();
                        row["BS"]       = dt.Rows[i]["BS"].ToString();
                        row["E6NOIS"]   = dt.Rows[i]["E6NOIS"].ToString();

                        Condt.Rows.Add(row);

                        sOLDE6DTCO = sNEWE6DTCO.ToString();
                        
                        sOLDE6CDSO = sNEWE6CDSO.ToString();
                        sOLDDT     = sNEWDT;
                    }
                }

                if (dt.Rows[i]["E6DTCO"].ToString() == "일자계")
                {
                    iHap = iHap + iCount;

                    iCount = 0;

                    dE6AMNR = dE6AMNR + double.Parse(dt.Rows[i]["E6AMNR"].ToString());
                }
                else
                {
                    iCount++;
                }
            }

            // 부서 총계
            row = Condt.NewRow();

            if (sGUBUN == "SEL")
            {
                row["E6CDSO"] = "";
                row["DT"]     = "";
            }
            else
            {
                row["E6CDSO"] = sOLDE6CDSO.ToString();
                row["DT"]     = sOLDDT.ToString();
            }
            row["E6DTCO"]   = "부서계";
            row["VNSANGHO"] = "";
            row["E6DTED"]   = "";
            row["E6NONR"]   = Convert.ToString(iHap) + "매";
            row["E6AMNR"]   = string.Format("{0:#,##0}", dE6AMNR);
            row["BK"]       = "";
            row["BG"]       = "";
            row["BS"]       = "";
            row["E6NOIS"]   = "";

            Condt.Rows.Add(row);

            return Condt;
        }
        #endregion

        #region Description : 시작일자 이벤트
        private void DTP01_GSTDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_E6CDSO.DummyValue = this.DTP01_GSTDATE.GetValue();
        }
        #endregion
    }
}