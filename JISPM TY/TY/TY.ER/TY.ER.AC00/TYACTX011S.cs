using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 세무구분별 매입명세서 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.14 16:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25EAH372 : 세무구분별 매입명세서 조회
    ///  TY_P_AC_25G19489 : 세무구분별 매입명세서 출력
    ///  TY_P_AC_25H3V532 : 세무구분별 매입명세서 집계표
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25E4Y431 : 세무구분별 매입명세서
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_25GAZ484 : 세무 구분을 선택하세요.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  VNCODE : 거래처코드
    ///  B4VLMI1 : 관리항목값１
    ///  B4VLMI2 : 관리항목값２
    ///  B4VLMI4 : 관리항목값４
    ///  GDATEGUBUN : 일자구분
    ///  CBO01_GPRTGN : 출력구분
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACTX011S : TYBase
    {
        string fsS1YEAR    = string.Empty;
        string fsS1BRANCH  = string.Empty;
        string fsS1CONFGB  = string.Empty;
        string fsS1TAXGUBN = string.Empty;
        string fsATELECTGB = string.Empty;
        string fsS1SJGUBN  = string.Empty;
        string fsD1SAUPNO  = string.Empty;

        string fsPOPUP     = string.Empty;

        #region Description : 페이지 로드
        public TYACTX011S()
        {
            InitializeComponent();
        }

        public TYACTX011S(string sS1YEAR, string sS1BRANCH, string sS1CONFGB, string sS1TAXGUBN, string sPOPUP)
        {
            InitializeComponent();

            fsS1YEAR    = sS1YEAR.ToString();
            fsS1BRANCH  = sS1BRANCH.ToString();
            fsS1CONFGB  = sS1CONFGB.ToString();
            fsS1TAXGUBN = sS1TAXGUBN.ToString();
            fsPOPUP     = sPOPUP.ToString();

            // 폼사이즈 조정
            this.ClientSize = new System.Drawing.Size(1184, 750);
        }

        private void TYACTX011S_Load(object sender, System.EventArgs e)
        {
            UP_Spread_Title();

            this.FPS91_TY_S_AC_3C62J600.Initialize();
            this.FPS91_TY_S_AC_3C63F610.Initialize();

            if (fsPOPUP.ToString() == "")
            {
                UP_Cookie_Load();
            }
            else
            {
                this.TXT01_S1YEAR.SetValue(fsS1YEAR.ToString());
                this.CBO01_S1BRANCH.SetValue(fsS1BRANCH.ToString());
                this.CBO01_S1CONFGB.SetValue(fsS1CONFGB.ToString());
                this.CBO01_S1TAXGUBN.SetValue(fsS1TAXGUBN.ToString());

                this.BTN61_INQ_Click(null, null);
            }

            SetStartingFocus(this.TXT01_S1YEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            fsS1YEAR    = this.TXT01_S1YEAR.GetValue().ToString();
            fsS1BRANCH  = this.CBO01_S1BRANCH.GetValue().ToString();
            fsS1CONFGB  = this.CBO01_S1CONFGB.GetValue().ToString();
            fsS1TAXGUBN = this.CBO01_S1TAXGUBN.GetValue().ToString();

            fsATELECTGB = "";
            fsS1SJGUBN  = "";
            fsD1SAUPNO  = "";

            UP_Spread_Title();

            this.FPS91_TY_S_AC_3C62J600.Initialize();
            this.FPS91_TY_S_AC_3C63F610.Initialize();

            // 세금계산서 총합계
            this.FPS91_TY_S_AC_3C341525.SetValue(UP_TOTAL_SUM());

            for (int i = 0; i < this.FPS91_TY_S_AC_3C341525.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_3C341525.GetValue(i, "ATELECTGB").ToString() == "")
                {
                    // 특정 ROW 글자 크기 변경
                    //this.FPS91_TY_S_AC_3C341525.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                }

                if (this.FPS91_TY_S_AC_3C341525.GetValue(i, "ATELECTGB").ToString() == "" &&
                    this.FPS91_TY_S_AC_3C341525.GetValue(i, "S1SJGUBN").ToString() == "")
                {
                    this.FPS91_TY_S_AC_3C341525.ActiveSheet.Rows[i].ForeColor = Color.Red;

                    this.FPS91_TY_S_AC_3C341525.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }

                if (this.FPS91_TY_S_AC_3C341525.GetValue(i, "ATELECTGB").ToString() != "" &&
                    this.FPS91_TY_S_AC_3C341525.GetValue(i, "S1SJGUBN").ToString() == "")
                {
                    this.FPS91_TY_S_AC_3C341525.ActiveSheet.Rows[i].ForeColor = Color.Blue;

                    this.FPS91_TY_S_AC_3C341525.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                }
            }

            UP_Cookie_Save();
        }
        #endregion

        #region Description : 세금계산서 총합계
        private DataTable UP_TOTAL_SUM()
        {
            string sS1TAXCDGN_HAP = string.Empty;
            string sS1TAXCDGN1    = string.Empty;
            string sS1TAXCDGN2    = string.Empty;

            if (this.CBO01_S1TAXGUBN.GetValue().ToString() == "1")
            {
                sS1TAXCDGN_HAP = "71,72,74,75,51,52,54,55";
                sS1TAXCDGN1    = "71,72,74,75";
                sS1TAXCDGN2    = "51,52,54,55";
            }
            else
            {
                sS1TAXCDGN_HAP = "61,62,68,69,11,12,19";
                sS1TAXCDGN1    = "61,62,68,69";
                sS1TAXCDGN2    = "11,12,19";
            }

            DataTable Retdt = new DataTable();

            DataRow row;

            Retdt.Columns.Add("TITLE1",     typeof(System.String));
            Retdt.Columns.Add("TITLE2",     typeof(System.String));
            Retdt.Columns.Add("ATELECTGB",  typeof(System.String));
            Retdt.Columns.Add("S1SJGUBN",   typeof(System.String));
            Retdt.Columns.Add("VNCODE_CNT", typeof(System.String));
            Retdt.Columns.Add("MAESU_CNT",  typeof(System.String));
            Retdt.Columns.Add("HAP_AMT",    typeof(System.String));
            Retdt.Columns.Add("HAP_VAT",    typeof(System.String));

            DataTable dt = new DataTable();

            for (int i = 0; i < 7; i++)
            {
                // 총합계
                if (i == 0)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3C61T592",
                        this.TXT01_S1YEAR.GetValue().ToString(),
                        this.CBO01_S1BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 2),
                        this.CBO01_S1TAXGUBN.GetValue().ToString(),
                        sS1TAXCDGN_HAP.ToString(),
                        ""
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"]     = "합      계";
                        row["TITLE2"]     = "";
                        row["ATELECTGB"]  = "";
                        row["S1SJGUBN"]   = "";
                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"]  = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"]    = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"]    = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 1) // 전자 - 사업자
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3C61T592",
                        this.TXT01_S1YEAR.GetValue().ToString(),
                        this.CBO01_S1BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 2),
                        this.CBO01_S1TAXGUBN.GetValue().ToString(),
                        sS1TAXCDGN1.ToString(),
                        "1"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"]     = "과세기간 종료일 다음달 11일까지 전송된 전자세금계산서 발급분";
                        row["TITLE2"]     = "사업자등록번호 발급분";
                        row["ATELECTGB"]  = "1";
                        row["S1SJGUBN"]   = "1";
                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"]  = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"]    = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"]    = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 2) // 전자 - 주민
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3C61T592",
                        this.TXT01_S1YEAR.GetValue().ToString(),
                        this.CBO01_S1BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 2),
                        this.CBO01_S1TAXGUBN.GetValue().ToString(),
                        sS1TAXCDGN1.ToString(),
                        "2"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"]     = "과세기간 종료일 다음달 11일까지 전송된 전자세금계산서 발급분";
                        row["TITLE2"]     = "주민등록번호 발급분";
                        row["ATELECTGB"]  = "1";
                        row["S1SJGUBN"]   = "2";
                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"]  = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"]    = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"]    = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 3) // 전자 - 소계
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3C61T592",
                        this.TXT01_S1YEAR.GetValue().ToString(),
                        this.CBO01_S1BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 2),
                        this.CBO01_S1TAXGUBN.GetValue().ToString(),
                        sS1TAXCDGN1.ToString(),
                        ""
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"]     = "과세기간 종료일 다음달 11일까지 전송된 전자세금계산서 발급분";
                        row["TITLE2"]     = "소      계";
                        row["ATELECTGB"]  = "1";
                        row["S1SJGUBN"]   = "";
                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"]  = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"]    = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"]    = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }

                else if (i == 4) // 전자외 - 사업자
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3C61T592",
                        this.TXT01_S1YEAR.GetValue().ToString(),
                        this.CBO01_S1BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 2),
                        this.CBO01_S1TAXGUBN.GetValue().ToString(),
                        sS1TAXCDGN2.ToString(),
                        "1"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"]     = "위 전자세금계산서 외의 발급분";
                        row["TITLE2"]     = "사업자등록번호 발급분";
                        row["ATELECTGB"]  = "2";
                        row["S1SJGUBN"]   = "1";
                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"]  = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"]    = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"]    = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 5) // 전자외 - 주민
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3C61T592",
                        this.TXT01_S1YEAR.GetValue().ToString(),
                        this.CBO01_S1BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 2),
                        this.CBO01_S1TAXGUBN.GetValue().ToString(),
                        sS1TAXCDGN2.ToString(),
                        "2"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"]     = "위 전자세금계산서 외의 발급분";
                        row["TITLE2"]     = "주민등록번호 발급분";
                        row["ATELECTGB"]  = "2";
                        row["S1SJGUBN"]   = "2";
                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"]  = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"]    = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"]    = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 6) // 전자외 - 소계
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3C61T592",
                        this.TXT01_S1YEAR.GetValue().ToString(),
                        this.CBO01_S1BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 2),
                        this.CBO01_S1TAXGUBN.GetValue().ToString(),
                        sS1TAXCDGN2.ToString(),
                        ""
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"]     = "위 전자세금계산서 외의 발급분";
                        row["TITLE2"]     = "소      계";
                        row["ATELECTGB"]  = "2";
                        row["S1SJGUBN"]   = "";
                        row["VNCODE_CNT"] = dt.Rows[0]["VNCODE_CNT"].ToString();
                        row["MAESU_CNT"]  = dt.Rows[0]["MAESU_CNT"].ToString();
                        row["HAP_AMT"]    = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"]    = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
            }

            return Retdt;
        }
        #endregion

        #region Description : 세금계산서 사업자별 합계
        private void UP_SAUPNO_SUM()
        {
            string sS1TAXCDGN = string.Empty;

            if (this.CBO01_S1TAXGUBN.GetValue().ToString() == "1")
            {
                if (fsATELECTGB.ToString() == "1")
                {
                    sS1TAXCDGN = "71,72,74,75";
                }
                else
                {
                    sS1TAXCDGN = "51,52,54,55";
                }
            }
            else
            {
                if (fsATELECTGB.ToString() == "1")
                {
                    sS1TAXCDGN = "61,62,68,69";
                }
                else
                {
                    sS1TAXCDGN = "11,12,19";
                }
            }

            this.FPS91_TY_S_AC_3C63F610.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3C62G599",
                fsS1YEAR.ToString(),
                fsS1BRANCH.ToString(),
                getCONFGB(fsS1CONFGB.ToString(), 1),
                getCONFGB(fsS1CONFGB.ToString(), 2),
                fsS1TAXGUBN.ToString(),
                sS1TAXCDGN.ToString(),
                fsS1SJGUBN.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_3C62J600.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_AC_3C62J600.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_3C62J600.GetValue(i, "NUMBER").ToString() == "합      계")
                {
                    this.FPS91_TY_S_AC_3C62J600.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_3C62J600.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }
        }
        #endregion

        #region Description : 세금계산서 사업자별 조회
        private void UP_SAUPNO_List()
        {
            string sS1TAXCDGN = string.Empty;

            if (this.CBO01_S1TAXGUBN.GetValue().ToString() == "1")
            {
                if (fsATELECTGB.ToString() == "1")
                {
                    sS1TAXCDGN = "71,72,74,75";
                }
                else
                {
                    sS1TAXCDGN = "51,52,54,55";
                }
            }
            else
            {
                if (fsATELECTGB.ToString() == "1")
                {
                    sS1TAXCDGN = "61,62,68,69";
                }
                else
                {
                    sS1TAXCDGN = "11,12,19";
                }
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3C63E609",
                fsS1YEAR.ToString(),
                fsS1BRANCH.ToString(),
                getCONFGB(fsS1CONFGB.ToString(), 1),
                getCONFGB(fsS1CONFGB.ToString(), 2),
                fsS1TAXGUBN.ToString(),
                sS1TAXCDGN.ToString(),
                fsS1SJGUBN.ToString(),
                fsD1SAUPNO.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_3C63F610.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_AC_3C63F610.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_3C63F610.GetValue(i, "NUMBER").ToString() == "합      계")
                {
                    this.FPS91_TY_S_AC_3C63F610.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_3C63F610.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }
        }
        #endregion

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_AC_3C341525_Sheet1.ColumnHeaderRowCount = 1;
            this.FPS91_TY_S_AC_3C341525_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_3C341525_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 2);

            this.FPS91_TY_S_AC_3C341525_Sheet1.ColumnHeader.Cells[0, 0].Value = "구    분";
            this.FPS91_TY_S_AC_3C341525_Sheet1.ColumnHeader.Cells[0, 4].Value = "거래처수";
            this.FPS91_TY_S_AC_3C341525_Sheet1.ColumnHeader.Cells[0, 5].Value = "매수";
            this.FPS91_TY_S_AC_3C341525_Sheet1.ColumnHeader.Cells[0, 6].Value = "공급가액";
            this.FPS91_TY_S_AC_3C341525_Sheet1.ColumnHeader.Cells[0, 7].Value = "세    액";

            this.FPS91_TY_S_AC_3C341525_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sS1TAXGUBN = string.Empty;

            if (this.CBO01_S1TAXGUBN.GetValue().ToString() == "1")
            {
                sS1TAXGUBN = "51,52,54,55";
            }
            else
            {
                sS1TAXGUBN = "11,12,19";
            }

            this.DbConnector.CommandClear();
                
            this.DbConnector.Attach
                (
                "TY_P_AC_3C95X667",
                this.TXT01_S1YEAR.GetValue().ToString(),
                this.CBO01_S1BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 2),
                this.CBO01_S1TAXGUBN.GetValue().ToString(),
                sS1TAXGUBN,
                "1"
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            DataTable dt2 = UP_TOTAL_SUM();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3C910655",
                this.TXT01_S1YEAR.GetValue().ToString(),
                getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 2),
                this.CBO01_S1BRANCH.GetValue().ToString()
                );

            DataTable dt3 = this.DbConnector.ExecuteDataTable();

            if (this.CBO01_S1TAXGUBN.GetValue().ToString() == "1")
            {
                SectionReport rpt = new TYACTX011R1(dt2, dt3, this.TXT01_S1YEAR.GetValue().ToString(), getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 1), getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 2));
                (new TYERGB001P(rpt, UP_ConvertHap(dt, "1"))).ShowDialog();
            }
            else
            {
                SectionReport rpt = new TYACTX011R3(dt2, dt3, this.TXT01_S1YEAR.GetValue().ToString(), getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 1), getCONFGB(this.CBO01_S1CONFGB.GetValue().ToString(), 2));
                (new TYERGB001P(rpt, UP_ConvertHap(dt, "2"))).ShowDialog();
            }

            UP_Cookie_Save();
        }
        #endregion

        #region Description : 세금계산서 총합계 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_AC_3C341525_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.FPS91_TY_S_AC_3C341525.GetValue("S1SJGUBN").ToString() == "")
            {
                this.ShowMessage("TY_M_MR_2BF8A365");

                this.FPS91_TY_S_AC_3C62J600.Initialize();
                this.FPS91_TY_S_AC_3C63F610.Initialize();
            }
            else if (this.FPS91_TY_S_AC_3C341525.GetValue("HAP_AMT").ToString() == "0")
            {
                this.ShowMessage("TY_M_AC_3CA7D692");

                this.FPS91_TY_S_AC_3C62J600.Initialize();
                this.FPS91_TY_S_AC_3C63F610.Initialize();
            }
            else
            {
                fsATELECTGB = this.FPS91_TY_S_AC_3C341525.GetValue("ATELECTGB").ToString();
                fsS1SJGUBN  = this.FPS91_TY_S_AC_3C341525.GetValue("S1SJGUBN").ToString();
                fsD1SAUPNO  = "";

                UP_SAUPNO_SUM();
            }
        }
        #endregion

        #region Description : 세금계산서 사업자별 집계 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_AC_3C62J600_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.FPS91_TY_S_AC_3C62J600.GetValue("NUMBER").ToString() == "합      계")
            {
                this.ShowMessage("TY_M_MR_2BF8A365");

                this.FPS91_TY_S_AC_3C63F610.Initialize();
            }
            else
            {
                fsD1SAUPNO = this.FPS91_TY_S_AC_3C62J600.GetValue("S1SAUPNO").ToString();

                UP_SAUPNO_List();
            }
        }
        #endregion

        #region Description : 세금계산서 사업자별 내역 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_AC_3C63F610_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column.ToString() == "1")
            {
                if (this.FPS91_TY_S_AC_3C63F610.GetValue("NUMBER").ToString() == "합      계")
                {
                    this.ShowMessage("TY_M_MR_2BF8A365");
                }
                else
                {
                    string sB2DPMK = this.FPS91_TY_S_AC_3C63F610.GetValue("D1JPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_AC_3C63F610.GetValue("D1JPNO").ToString().Substring(6, 8);
                    string sB2NOSQ = this.FPS91_TY_S_AC_3C63F610.GetValue("D1JPNO").ToString().Substring(14, 3);

                    if (this.OpenModalPopup(new TYACBJ001I(sB2DPMK, sB2DTMK, sB2NOSQ)) == System.Windows.Forms.DialogResult.OK)
                    {
                    }
                }
            }
        }
        #endregion

        #region Description : 세금계산서 합계표 데이터셋 변환
        protected DataTable UP_ConvertHap(DataTable dt, string sGubn)
        {
            string sNUMBER = string.Empty;
            string sS1SAUPNO = string.Empty;
            string sVNSANGHO = string.Empty;
            string sS1TAXCDGN = string.Empty;
            string sMAESU_CNT = string.Empty;
            string sHAP_AMT = string.Empty;
            string sHAP_VAT = string.Empty;
            int iBLANK = 0;

            if (sGubn == "1")
            {
                iBLANK = 14 - ((dt.Rows.Count - 5) % 14);
                if (iBLANK == 14)
                {
                    iBLANK = 0;
                }
            }
            else
            {
                iBLANK = 15 - ((dt.Rows.Count - 5) % 15);
                if (iBLANK == 15)
                {
                    iBLANK = 0;
                }
            }

            int i = 0;

            sNUMBER = "";
            sS1SAUPNO = "";
            sVNSANGHO = "";
            sS1TAXCDGN = "";
            sMAESU_CNT = "";
            sHAP_AMT = "";
            sHAP_VAT = "";

            DataTable Retdt = dt;



            if (dt != null && dt.Rows.Count > 5)
            {
                DataRow row;

                for (i = 1; i <= iBLANK; i++)
                {
                    row = Retdt.NewRow();

                    row["NUMBER"] = DBNull.Value;
                    row["S1SAUPNO"] = "";
                    row["VNSANGHO"] = "";
                    row["S1TAXCDGN"] = "";
                    row["MAESU_CNT"] = DBNull.Value;
                    row["HAP_AMT"] = DBNull.Value;
                    row["HAP_VAT"] = DBNull.Value;

                    Retdt.Rows.Add(row);
                }
            }
            else if (dt.Rows.Count < 5)
            {
                DataRow row;

                iBLANK = 5 - (dt.Rows.Count);

                for (i = 1; i <= iBLANK; i++)
                {
                    row = Retdt.NewRow();

                    row["NUMBER"] = DBNull.Value;
                    row["S1SAUPNO"] = "";
                    row["VNSANGHO"] = "";
                    row["S1TAXCDGN"] = "";
                    row["MAESU_CNT"] = DBNull.Value;
                    row["HAP_AMT"] = DBNull.Value;
                    row["HAP_VAT"] = DBNull.Value;

                    Retdt.Rows.Add(row);
                }
            }

            return Retdt;
        }
        #endregion

        #region Description : 쿠키 불러오기
        private void UP_Cookie_Load()
        {
            if (TYCookie.Chk == "Cookie")
            {
                this.TXT01_S1YEAR.SetValue(TYCookie.Year);
                this.CBO01_S1BRANCH.SetValue(TYCookie.Branch);
                this.CBO01_S1CONFGB.SetValue(TYCookie.Confgb);
            }
            else
            {
                this.TXT01_S1YEAR.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 4));
            }
        }
        #endregion

        #region Description : 쿠키 저장
        private void UP_Cookie_Save()
        {
            TYCookie.Save(this.TXT01_S1YEAR.GetValue().ToString(), this.CBO01_S1BRANCH.GetValue().ToString(), this.CBO01_S1CONFGB.GetValue().ToString());
        }
        #endregion
    }
}