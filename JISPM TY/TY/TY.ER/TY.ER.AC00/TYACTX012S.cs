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
    public partial class TYACTX012S : TYBase
    {
        string fsS2YEAR    = string.Empty;
        string fsS2BRANCH  = string.Empty;
        string fsS2CONFGB  = string.Empty;
        string fsS2TAXGUBN = string.Empty;
        string fsATELECTGB = string.Empty;
        string fsS2SJGUBN  = string.Empty;
        string fsD2SAUPNO  = string.Empty;

        #region Description : 페이지 로드
        public TYACTX012S()
        {
            InitializeComponent();
        }

        private void TYACTX012S_Load(object sender, System.EventArgs e)
        {
            this.TXT01_O1STYYMM.SetReadOnly(true);
            this.TXT01_O1EDYYMM.SetReadOnly(true);

            UP_Spread_Title();

            this.FPS91_TY_S_AC_3CA9S672.Initialize();
            this.FPS91_TY_S_AC_3CA9T673.Initialize();

            UP_Cookie_Load();

            SetStartingFocus(this.TXT01_S2YEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.TXT01_O1STYYMM.SetValue("");
            this.TXT01_O1EDYYMM.SetValue("");

            fsS2YEAR    = this.TXT01_S2YEAR.GetValue().ToString();
            fsS2BRANCH  = this.CBO01_S2BRANCH.GetValue().ToString();
            fsS2CONFGB  = this.CBO01_S2CONFGB.GetValue().ToString();
            fsS2TAXGUBN = this.CBO01_S2TAXGUBN.GetValue().ToString();

            fsATELECTGB = "";
            fsS2SJGUBN  = "";
            fsD2SAUPNO  = "";

            UP_Spread_Title();

            this.FPS91_TY_S_AC_3CA9S672.Initialize();
            this.FPS91_TY_S_AC_3CA9T673.Initialize();

            // 세금계산서 총합계
            this.FPS91_TY_S_AC_3CA9U674.SetValue(UP_TOTAL_SUM());

            for (int i = 0; i < this.FPS91_TY_S_AC_3CA9U674.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_3CA9U674.GetValue(i, "ATELECTGB").ToString() == "")
                {
                    // 특정 ROW 글자 크기 변경
                    //this.FPS91_TY_S_AC_3CA9U674.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                }

                if (this.FPS91_TY_S_AC_3CA9U674.GetValue(i, "ATELECTGB").ToString() == "" &&
                    this.FPS91_TY_S_AC_3CA9U674.GetValue(i, "S2SJGUBN").ToString() == "")
                {
                    this.FPS91_TY_S_AC_3CA9U674.ActiveSheet.Rows[i].ForeColor = Color.Red;

                    this.FPS91_TY_S_AC_3CA9U674.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }

                if (this.FPS91_TY_S_AC_3CA9U674.GetValue(i, "ATELECTGB").ToString() != "" &&
                    this.FPS91_TY_S_AC_3CA9U674.GetValue(i, "S2SJGUBN").ToString() == "")
                {
                    this.FPS91_TY_S_AC_3CA9U674.ActiveSheet.Rows[i].ForeColor = Color.Blue;

                    this.FPS91_TY_S_AC_3CA9U674.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                }
            }

            DataTable dt = new DataTable();

            // 작업년월 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3CAB6679",
                this.TXT01_S2YEAR.GetValue().ToString(),
                this.CBO01_S2BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 2),
                this.CBO01_S2TAXGUBN.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_O1STYYMM.SetValue(dt.Rows[0]["O1STYYMM"].ToString());
                this.TXT01_O1EDYYMM.SetValue(dt.Rows[0]["O1EDYYMM"].ToString());
            }

            UP_Cookie_Save();
        }
        #endregion

        #region Description : 계산서 총합계
        private DataTable UP_TOTAL_SUM()
        {
            string sS2TAXCDGN_HAP = string.Empty;
            string sS2TAXCDGN1 = string.Empty;
            string sS2TAXCDGN2 = string.Empty;

            if (this.CBO01_S2TAXGUBN.GetValue().ToString() == "1")
            {
                sS2TAXCDGN_HAP = "79,59";
                sS2TAXCDGN1    = "79";
                sS2TAXCDGN2    = "59";
            }
            else
            {
                sS2TAXCDGN_HAP = "66,22";
                sS2TAXCDGN1    = "66";
                sS2TAXCDGN2    = "22";
            }

            DataTable Retdt = new DataTable();

            DataRow row;

            Retdt.Columns.Add("TITLE1",     typeof(System.String));
            Retdt.Columns.Add("TITLE2",     typeof(System.String));
            Retdt.Columns.Add("ATELECTGB",  typeof(System.String));
            Retdt.Columns.Add("S2SJGUBN",   typeof(System.String));
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
                        "TY_P_AC_3CA9V675",
                        this.TXT01_S2YEAR.GetValue().ToString(),
                        this.CBO01_S2BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 2),
                        this.CBO01_S2TAXGUBN.GetValue().ToString(),
                        sS2TAXCDGN_HAP.ToString(),
                        ""
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"]     = "합      계";
                        row["TITLE2"]     = "";
                        row["ATELECTGB"]  = "";
                        row["S2SJGUBN"]   = "";
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
                        "TY_P_AC_3CA9V675",
                        this.TXT01_S2YEAR.GetValue().ToString(),
                        this.CBO01_S2BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 2),
                        this.CBO01_S2TAXGUBN.GetValue().ToString(),
                        sS2TAXCDGN1.ToString(),
                        "1"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"]     = "과세기간 종료일 다음달 11일까지 전송된 전자세금계산서 발급분";
                        row["TITLE2"]     = "사업자등록번호 발급분";
                        row["ATELECTGB"]  = "1";
                        row["S2SJGUBN"]   = "1";
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
                        "TY_P_AC_3CA9V675",
                        this.TXT01_S2YEAR.GetValue().ToString(),
                        this.CBO01_S2BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 2),
                        this.CBO01_S2TAXGUBN.GetValue().ToString(),
                        sS2TAXCDGN1.ToString(),
                        "2"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"]     = "과세기간 종료일 다음달 11일까지 전송된 전자세금계산서 발급분";
                        row["TITLE2"]     = "주민등록번호 발급분";
                        row["ATELECTGB"]  = "1";
                        row["S2SJGUBN"]   = "2";
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
                        "TY_P_AC_3CA9V675",
                        this.TXT01_S2YEAR.GetValue().ToString(),
                        this.CBO01_S2BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 2),
                        this.CBO01_S2TAXGUBN.GetValue().ToString(),
                        sS2TAXCDGN1.ToString(),
                        ""
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"]     = "과세기간 종료일 다음달 11일까지 전송된 전자세금계산서 발급분";
                        row["TITLE2"]     = "소      계";
                        row["ATELECTGB"]  = "1";
                        row["S2SJGUBN"]   = "";
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
                        "TY_P_AC_3CA9V675",
                        this.TXT01_S2YEAR.GetValue().ToString(),
                        this.CBO01_S2BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 2),
                        this.CBO01_S2TAXGUBN.GetValue().ToString(),
                        sS2TAXCDGN2.ToString(),
                        "1"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"]     = "위 전자세금계산서 외의 발급분";
                        row["TITLE2"]     = "사업자등록번호 발급분";
                        row["ATELECTGB"]  = "2";
                        row["S2SJGUBN"]   = "1";
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
                        "TY_P_AC_3CA9V675",
                        this.TXT01_S2YEAR.GetValue().ToString(),
                        this.CBO01_S2BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 2),
                        this.CBO01_S2TAXGUBN.GetValue().ToString(),
                        sS2TAXCDGN2.ToString(),
                        "2"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"]     = "위 전자세금계산서 외의 발급분";
                        row["TITLE2"]     = "주민등록번호 발급분";
                        row["ATELECTGB"]  = "2";
                        row["S2SJGUBN"]   = "2";
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
                        "TY_P_AC_3CA9V675",
                        this.TXT01_S2YEAR.GetValue().ToString(),
                        this.CBO01_S2BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 2),
                        this.CBO01_S2TAXGUBN.GetValue().ToString(),
                        sS2TAXCDGN2.ToString(),
                        ""
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["TITLE1"]     = "위 전자세금계산서 외의 발급분";
                        row["TITLE2"]     = "소      계";
                        row["ATELECTGB"]  = "2";
                        row["S2SJGUBN"]   = "";
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

        #region Description : 계산서 사업자별 합계
        private void UP_SAUPNO_SUM()
        {
            string sS2TAXCDGN = string.Empty;

            if (this.CBO01_S2TAXGUBN.GetValue().ToString() == "1")
            {
                if (fsATELECTGB.ToString() == "1")
                {
                    sS2TAXCDGN = "79";
                }
                else
                {
                    sS2TAXCDGN = "59";
                }
            }
            else
            {
                if (fsATELECTGB.ToString() == "1")
                {
                    sS2TAXCDGN = "66";
                }
                else
                {
                    sS2TAXCDGN = "22";
                }
            }

            this.FPS91_TY_S_AC_3CA9T673.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3CA9W676",
                fsS2YEAR.ToString(),
                fsS2BRANCH.ToString(),
                getCONFGB(this.fsS2CONFGB.ToString(), 1),
                getCONFGB(this.fsS2CONFGB.ToString(), 2),
                fsS2TAXGUBN.ToString(),
                sS2TAXCDGN.ToString(),
                fsS2SJGUBN.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_3CA9S672.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_AC_3CA9S672.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_3CA9S672.GetValue(i, "NUMBER").ToString() == "합      계")
                {
                    this.FPS91_TY_S_AC_3CA9S672.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_3CA9S672.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }
        }
        #endregion

        #region Description : 세금계산서 사업자별 조회
        private void UP_SAUPNO_List()
        {
            string sS2TAXCDGN = string.Empty;

            if (this.CBO01_S2TAXGUBN.GetValue().ToString() == "1")
            {
                if (fsATELECTGB.ToString() == "1")
                {
                    sS2TAXCDGN = "79";
                }
                else
                {
                    sS2TAXCDGN = "59";
                }
            }
            else
            {
                if (fsATELECTGB.ToString() == "1")
                {
                    sS2TAXCDGN = "66";
                }
                else
                {
                    sS2TAXCDGN = "22";
                }
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3CA9Y677",
                fsS2YEAR.ToString(),
                fsS2BRANCH.ToString(),
                getCONFGB(this.fsS2CONFGB.ToString(), 1),
                getCONFGB(this.fsS2CONFGB.ToString(), 2),
                fsS2TAXGUBN.ToString(),
                sS2TAXCDGN.ToString(),
                fsS2SJGUBN.ToString(),
                fsD2SAUPNO.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_3CA9T673.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_AC_3CA9T673.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_3CA9T673.GetValue(i, "NUMBER").ToString() == "합      계")
                {
                    this.FPS91_TY_S_AC_3CA9T673.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_3CA9T673.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }
        }
        #endregion

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_AC_3CA9U674_Sheet1.ColumnHeaderRowCount = 1;
            this.FPS91_TY_S_AC_3CA9U674_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_3CA9U674_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 2);

            this.FPS91_TY_S_AC_3CA9U674_Sheet1.ColumnHeader.Cells[0, 0].Value = "구    분";
            this.FPS91_TY_S_AC_3CA9U674_Sheet1.ColumnHeader.Cells[0, 4].Value = "거래처수";
            this.FPS91_TY_S_AC_3CA9U674_Sheet1.ColumnHeader.Cells[0, 5].Value = "매수";
            this.FPS91_TY_S_AC_3CA9U674_Sheet1.ColumnHeader.Cells[0, 6].Value = "공급가액";
            this.FPS91_TY_S_AC_3CA9U674_Sheet1.ColumnHeader.Cells[0, 7].Value = "세    액";

            this.FPS91_TY_S_AC_3CA9U674_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sS1TAXGUBN = string.Empty;

            if (this.CBO01_S2TAXGUBN.GetValue().ToString() == "1")
            {
                sS1TAXGUBN = "59";
            }
            else
            {
                sS1TAXGUBN = "22";
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_3CB6O731",
                this.TXT01_S2YEAR.GetValue().ToString(),
                this.CBO01_S2BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 2),
                this.CBO01_S2TAXGUBN.GetValue().ToString(),
                sS1TAXGUBN,
                "1"
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            DataTable dt2 = UP_TOTAL_SUM();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3C910655",
                this.TXT01_S2YEAR.GetValue().ToString(),
                getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 2),
                this.CBO01_S2BRANCH.GetValue().ToString()
                );

            DataTable dt3 = this.DbConnector.ExecuteDataTable();

            if (this.CBO01_S2TAXGUBN.GetValue().ToString() == "1")
            {
                string sStdate = "00000000";
                string sEddate = "00000000";
                int iDD = 0;

                DataTable dt_date = new DataTable();

                // 작업년월 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3CAB6679",
                    this.TXT01_S2YEAR.GetValue().ToString(),
                    this.CBO01_S2BRANCH.GetValue().ToString(),
                    getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 2),
                    this.CBO01_S2TAXGUBN.GetValue().ToString()
                    );

                dt_date = this.DbConnector.ExecuteDataTable();

                if (dt_date.Rows.Count > 0)
                {
                    iDD = DateTime.DaysInMonth(int.Parse(dt_date.Rows[0]["O1EDYYMM"].ToString().Substring(0, 4)), int.Parse(dt_date.Rows[0]["O1EDYYMM"].ToString().Substring(4, 2)));

                    sStdate = dt_date.Rows[0]["O1STYYMM"].ToString() + "01";
                    sEddate = dt_date.Rows[0]["O1EDYYMM"].ToString() + Set_Fill2(Convert.ToString(iDD));
                }

                SectionReport rpt = new TYACTX012R1(dt2, dt3, this.TXT01_S2YEAR.GetValue().ToString(), getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 1), getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 2), sStdate, sEddate ,"1");
                (new TYERGB001P(rpt, UP_ConvertHap(dt))).ShowDialog();
            }
            else
            {
                SectionReport rpt = new TYACTX012R3(dt2, dt3, this.TXT01_S2YEAR.GetValue().ToString(), getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 1), getCONFGB(this.CBO01_S2CONFGB.GetValue().ToString(), 2));
                (new TYERGB001P(rpt, UP_ConvertHap(dt))).ShowDialog();
            }

            UP_Cookie_Save();
        }
        #endregion

        #region Description : 세금계산서 총합계 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_AC_3CA9U674_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.FPS91_TY_S_AC_3CA9U674.GetValue("S2SJGUBN").ToString() == "")
            {
                this.ShowMessage("TY_M_MR_2BF8A365");

                this.FPS91_TY_S_AC_3CA9S672.Initialize();
                this.FPS91_TY_S_AC_3CA9T673.Initialize();
            }
            else if (this.FPS91_TY_S_AC_3CA9U674.GetValue("HAP_AMT").ToString() == "0")
            {
                this.ShowMessage("TY_M_AC_3CA7D692");

                this.FPS91_TY_S_AC_3CA9S672.Initialize();
                this.FPS91_TY_S_AC_3CA9T673.Initialize();
            }
            else
            {
                fsATELECTGB = this.FPS91_TY_S_AC_3CA9U674.GetValue("ATELECTGB").ToString();
                fsS2SJGUBN  = this.FPS91_TY_S_AC_3CA9U674.GetValue("S2SJGUBN").ToString();
                fsD2SAUPNO  = "";

                UP_SAUPNO_SUM();
            }
        }
        #endregion

        #region Description : 세금계산서 사업자별 집계 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_AC_3CA9S672_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.FPS91_TY_S_AC_3CA9S672.GetValue("NUMBER").ToString() == "합      계")
            {
                this.ShowMessage("TY_M_MR_2BF8A365");

                this.FPS91_TY_S_AC_3CA9T673.Initialize();
            }
            else
            {
                fsD2SAUPNO = this.FPS91_TY_S_AC_3CA9S672.GetValue("S2SAUPNO").ToString();

                UP_SAUPNO_List();
            }
        }
        #endregion

        #region Description : 세금계산서 사업자별 내역 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_AC_3CA9T673_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column.ToString() == "1")
            {
                if (this.FPS91_TY_S_AC_3CA9T673.GetValue("NUMBER").ToString() == "합      계")
                {
                    this.ShowMessage("TY_M_MR_2BF8A365");
                }
                else
                {
                    string sB2DPMK = this.FPS91_TY_S_AC_3CA9T673.GetValue("D2JPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_AC_3CA9T673.GetValue("D2JPNO").ToString().Substring(6, 8);
                    string sB2NOSQ = this.FPS91_TY_S_AC_3CA9T673.GetValue("D2JPNO").ToString().Substring(14, 3);

                    if (this.OpenModalPopup(new TYACBJ001I(sB2DPMK, sB2DTMK, sB2NOSQ)) == System.Windows.Forms.DialogResult.OK)
                    {
                    }
                }
            }
        }
        #endregion

        #region Description : 세금계산서 합계표 데이터셋 변환
        protected DataTable UP_ConvertHap(DataTable dt)
        {
            string sNUMBER = string.Empty;
            string sS1SAUPNO = string.Empty;
            string sVNSANGHO = string.Empty;
            string sS1TAXCDGN = string.Empty;
            string sMAESU_CNT = string.Empty;
            string sHAP_AMT = string.Empty;
            string sHAP_VAT = string.Empty;
            int iBLANK = 0;

            iBLANK = 13 - ((dt.Rows.Count - 5) % 13);
            if (iBLANK == 13)
            {
                iBLANK = 0;
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
                    row["S2SAUPNO"] = "";
                    row["VNSANGHO"] = "";
                    row["S2TAXCDGN"] = "";
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
                    row["S2SAUPNO"] = "";
                    row["VNSANGHO"] = "";
                    row["S2TAXCDGN"] = "";
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
                this.TXT01_S2YEAR.SetValue(TYCookie.Year);
                this.CBO01_S2BRANCH.SetValue(TYCookie.Branch);
                this.CBO01_S2CONFGB.SetValue(TYCookie.Confgb);
            }
            else
            {
                this.TXT01_S2YEAR.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 4));
            }
        }
        #endregion

        #region Description : 쿠키 저장
        private void UP_Cookie_Save()
        {
            TYCookie.Save(this.TXT01_S2YEAR.GetValue().ToString(), this.CBO01_S2BRANCH.GetValue().ToString(), this.CBO01_S2CONFGB.GetValue().ToString());
        }
        #endregion
    }
}