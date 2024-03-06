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
    public partial class TYACTX014S : TYBase
    {
        string fsS4YEAR     = string.Empty;
        string fsS4BRANCH   = string.Empty;
        string fsS4CONFGB   = string.Empty;
        //string fsS4PURCHGB  = string.Empty;
        string fsS4TAXGUBN  = string.Empty;
        string fsD4CREDITNO = string.Empty;
        string fsD4CUSTCD   = string.Empty;

        string fsPOPUP = string.Empty;

        #region Description : 페이지 로드
        public TYACTX014S()
        {
            InitializeComponent();
        }

        public TYACTX014S(string sS4YEAR, string sS4BRANCH, string sS4CONFGB, string sPOPUP)
        {
            InitializeComponent();

            fsS4YEAR    = sS4YEAR.ToString();
            fsS4BRANCH  = sS4BRANCH.ToString();
            fsS4CONFGB  = sS4CONFGB.ToString();
            fsPOPUP    = sPOPUP.ToString();

            // 폼사이즈 조정
            this.ClientSize = new System.Drawing.Size(1184, 750);
        }

        private void TYACTX014S_Load(object sender, System.EventArgs e)
        {
            //UP_Spread_Title();

            this.FPS91_TY_S_AC_3CHBD783.Initialize();
            this.FPS91_TY_S_AC_3CHBE784.Initialize();

            if (fsPOPUP.ToString() == "")
            {
                UP_Cookie_Load();
            }
            else
            {
                this.TXT01_S4YEAR.SetValue(fsS4YEAR.ToString());
                this.CBO01_S4BRANCH.SetValue(fsS4BRANCH.ToString());
                this.CBO01_S4CONFGB.SetValue(fsS4CONFGB.ToString());

                this.BTN61_INQ_Click(null, null);
            }

            SetStartingFocus(this.TXT01_S4YEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            fsS4YEAR     = this.TXT01_S4YEAR.GetValue().ToString();
            fsS4BRANCH   = this.CBO01_S4BRANCH.GetValue().ToString();
            fsS4CONFGB   = this.CBO01_S4CONFGB.GetValue().ToString();
            //fsS4PURCHGB  = this.CBO01_S4PURCHGB.GetValue().ToString();

            fsS4TAXGUBN  = "";
            fsD4CREDITNO = "";
            fsD4CUSTCD   = "";

            //UP_Spread_Title();

            this.FPS91_TY_S_AC_3CHBD783.Initialize();
            this.FPS91_TY_S_AC_3CHBE784.Initialize();

            // 세금계산서 총합계
            this.FPS91_TY_S_AC_3CHBC782.SetValue(UP_TOTAL_SUM());

            for (int i = 0; i < this.FPS91_TY_S_AC_3CHBC782.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_3CHBC782.GetValue(i, "TITLE").ToString() == "")
                {
                    // 특정 ROW 글자 크기 변경
                    //this.FPS91_TY_S_AC_3CHBC782.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                }

                if (this.FPS91_TY_S_AC_3CHBC782.GetValue(i, "TITLE").ToString().Substring(0,1) == "⑤")
                {
                    this.FPS91_TY_S_AC_3CHBC782.ActiveSheet.Rows[i].ForeColor = Color.Red;

                    this.FPS91_TY_S_AC_3CHBC782.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }

                if (this.FPS91_TY_S_AC_3CHBC782.GetValue(i, "TITLE").ToString().Substring(0, 1) == "⑥" ||
                    this.FPS91_TY_S_AC_3CHBC782.GetValue(i, "TITLE").ToString().Substring(0, 1) == "⑨")
                {
                    this.FPS91_TY_S_AC_3CHBC782.ActiveSheet.Rows[i].ForeColor = Color.Blue;

                    this.FPS91_TY_S_AC_3CHBC782.ActiveSheet.Rows[i].BackColor = Color.FromArgb(254, 209, 164);
                }
            }

            UP_Cookie_Save();
        }
        #endregion

        #region Description : 신용카드매출전표 총합계
        private DataTable UP_TOTAL_SUM()
        {
            string sS4TAXGUBN = string.Empty;
            string sTITLE     = string.Empty;

            int i = 0;

            DataTable Retdt = new DataTable();

            DataRow row;

            Retdt.Columns.Add("TITLE",   typeof(System.String));
            Retdt.Columns.Add("CNT",     typeof(System.String));
            Retdt.Columns.Add("AMT",     typeof(System.String));
            Retdt.Columns.Add("VAT",     typeof(System.String));
            Retdt.Columns.Add("TAXGUBN", typeof(System.String));

            DataTable dt = new DataTable();

            if ((int.Parse(this.TXT01_S4YEAR.GetValue().ToString()) >= 2021) || (this.TXT01_S4YEAR.GetValue().ToString() == "2020" && this.CBO01_S4CONFGB.GetValue().ToString() == "22"))
            {
                for (i = 0; i < 7; i++)
                {
                    sS4TAXGUBN = "";

                    switch (i)
                    {
                        case 0:
                            sS4TAXGUBN = "";
                            sTITLE = "⑤합   계";

                            break;

                        case 1:
                            sS4TAXGUBN = "57";
                            sTITLE = "⑥현금영수증";

                            break;
                        case 2:
                            sTITLE = "⑦화물운전자복지카드";

                            break;
                        case 3:
                            sS4TAXGUBN = "58";
                            sTITLE = "⑧사업용신용카드";

                            break;
                        case 4:
                            sTITLE = "⑨기타 신용카드";

                            break;
                    }

                    if (i == 0 || i == 1 || i == 3)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_3CHAG779",
                            this.TXT01_S4YEAR.GetValue().ToString(),
                            this.CBO01_S4BRANCH.GetValue().ToString(),
                            getCONFGB(this.CBO01_S4CONFGB.GetValue().ToString(), 1),
                            getCONFGB(this.CBO01_S4CONFGB.GetValue().ToString(), 2),
                            //this.CBO01_S4PURCHGB.GetValue().ToString(),
                            sS4TAXGUBN.ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            row = Retdt.NewRow();

                            row["TITLE"] = sTITLE.ToString();
                            row["CNT"] = dt.Rows[0]["CNT"].ToString();
                            row["AMT"] = dt.Rows[0]["AMT"].ToString();
                            row["VAT"] = dt.Rows[0]["VAT"].ToString();
                            row["TAXGUBN"] = sS4TAXGUBN.ToString();

                            Retdt.Rows.Add(row);
                        }
                    }
                    else if (i == 2 || i == 4)
                    {
                        row = Retdt.NewRow();

                        row["TITLE"] = sTITLE.ToString();
                        row["CNT"] = "0";
                        row["AMT"] = "0";
                        row["VAT"] = "0";
                        row["TAXGUBN"] = sS4TAXGUBN.ToString();

                        Retdt.Rows.Add(row);
                    }
                }
            }
            else
            {
                for (i = 0; i < 7; i++)
                {
                    sS4TAXGUBN = "";

                    switch (i)
                    {
                        case 0:
                            sS4TAXGUBN = "";
                            sTITLE = "⑤합   계";

                            break;

                        case 1:
                            sS4TAXGUBN = "57";
                            sTITLE = "⑥현금영수증";

                            break;
                        case 2:
                            sTITLE = "⑦화물운전자복지카드";

                            break;
                        case 3:
                            sTITLE = "⑧사업용신용카드";

                            break;
                        case 4:
                            sS4TAXGUBN = "58";
                            sTITLE = "⑨기타 신용카드";

                            break;
                    }

                    if (i == 0 || i == 1 || i == 4)
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_3CHAG779",
                            this.TXT01_S4YEAR.GetValue().ToString(),
                            this.CBO01_S4BRANCH.GetValue().ToString(),
                            getCONFGB(this.CBO01_S4CONFGB.GetValue().ToString(), 1),
                            getCONFGB(this.CBO01_S4CONFGB.GetValue().ToString(), 2),
                            //this.CBO01_S4PURCHGB.GetValue().ToString(),
                            sS4TAXGUBN.ToString()
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            row = Retdt.NewRow();

                            row["TITLE"] = sTITLE.ToString();
                            row["CNT"] = dt.Rows[0]["CNT"].ToString();
                            row["AMT"] = dt.Rows[0]["AMT"].ToString();
                            row["VAT"] = dt.Rows[0]["VAT"].ToString();
                            row["TAXGUBN"] = sS4TAXGUBN.ToString();

                            Retdt.Rows.Add(row);
                        }
                    }
                    else if (i == 2 || i == 3)
                    {
                        row = Retdt.NewRow();

                        row["TITLE"] = sTITLE.ToString();
                        row["CNT"] = "0";
                        row["AMT"] = "0";
                        row["VAT"] = "0";
                        row["TAXGUBN"] = sS4TAXGUBN.ToString();

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
            this.FPS91_TY_S_AC_3CHBE784.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3CHAJ780",
                fsS4YEAR.ToString(),
                fsS4BRANCH.ToString(),
                getCONFGB(this.fsS4CONFGB.ToString(), 1),
                getCONFGB(this.fsS4CONFGB.ToString(), 2),
                //fsS4PURCHGB.ToString(),
                fsS4TAXGUBN.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_3CHBD783.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_AC_3CHBD783.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_3CHBD783.GetValue(i, "NUMBER").ToString() == "합      계")
                {
                    this.FPS91_TY_S_AC_3CHBD783.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_3CHBD783.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }

            this.FPS91_TY_S_AC_3CHBD783.ActiveSheet.Rows[this.FPS91_TY_S_AC_3CHBD783.ActiveSheet.Rows.Count - 1].Locked = true;
        }
        #endregion

        #region Description : 신용카드매출전표 카드번호별 조회
        private void UP_SAUPNO_List()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3CHAN781",
                fsS4YEAR.ToString(),
                fsS4BRANCH.ToString(),
                getCONFGB(this.fsS4CONFGB.ToString(), 1),
                getCONFGB(this.fsS4CONFGB.ToString(), 2),
                //fsS4PURCHGB.ToString(),
                fsS4TAXGUBN.ToString(),
                fsD4CREDITNO.ToString(),
                fsD4CUSTCD.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_3CHBE784.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_AC_3CHBE784.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_3CHBE784.GetValue(i, "NUMBER").ToString() == "합      계")
                {
                    this.FPS91_TY_S_AC_3CHBE784.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_3CHBE784.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }
        }
        #endregion

        #region Description : 스프레드 타이틀
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_AC_3CHBC782_Sheet1.ColumnHeaderRowCount = 1;
            this.FPS91_TY_S_AC_3CHBC782_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_AC_3CHBC782_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 2);

            this.FPS91_TY_S_AC_3CHBC782_Sheet1.ColumnHeader.Cells[0, 0].Value = "구    분";
            this.FPS91_TY_S_AC_3CHBC782_Sheet1.ColumnHeader.Cells[0, 4].Value = "거래처수";
            this.FPS91_TY_S_AC_3CHBC782_Sheet1.ColumnHeader.Cells[0, 5].Value = "매수";
            this.FPS91_TY_S_AC_3CHBC782_Sheet1.ColumnHeader.Cells[0, 6].Value = "공급가액";
            this.FPS91_TY_S_AC_3CHBC782_Sheet1.ColumnHeader.Cells[0, 7].Value = "세    액";

            this.FPS91_TY_S_AC_3CHBC782_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {   
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3CH30787",
                this.TXT01_S4YEAR.GetValue().ToString(),
                this.CBO01_S4BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S4CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S4CONFGB.GetValue().ToString(), 2),
                "58"
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            DataTable dt2 = UP_TOTAL_SUM_14R();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3C910655",
                this.TXT01_S4YEAR.GetValue().ToString(),
                getCONFGB(this.CBO01_S4CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S4CONFGB.GetValue().ToString(), 2),
                this.CBO01_S4BRANCH.GetValue().ToString()
                );

            DataTable dt3 = this.DbConnector.ExecuteDataTable();

            if ((int.Parse(this.TXT01_S4YEAR.GetValue().ToString()) >= 2021) || (this.TXT01_S4YEAR.GetValue().ToString() == "2020" && this.CBO01_S4CONFGB.GetValue().ToString() == "22"))
            {
                SectionReport rpt = new TYACTX014R3(dt2, dt3, this.TXT01_S4YEAR.GetValue().ToString(), getCONFGB(this.CBO01_S4CONFGB.GetValue().ToString(), 1), getCONFGB(this.CBO01_S4CONFGB.GetValue().ToString(), 2));
                (new TYERGB001P(rpt, UP_ConvertHap(dt, "4"))).ShowDialog();
            }
            else
            {
                SectionReport rpt = new TYACTX014R1(dt2, dt3, this.TXT01_S4YEAR.GetValue().ToString(), getCONFGB(this.CBO01_S4CONFGB.GetValue().ToString(), 1), getCONFGB(this.CBO01_S4CONFGB.GetValue().ToString(), 2));
                (new TYERGB001P(rpt, UP_ConvertHap(dt, "4"))).ShowDialog();
            }            

            UP_Cookie_Save();
        }
        #endregion

        #region Description : 신용카드매출전표 총합계 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_AC_3CHBC782_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 기타신용카드일 경우에만 내역 뿌려줌.

            if (this.FPS91_TY_S_AC_3CHBC782.GetValue("TITLE").ToString().Substring(0, 1) == "⑤")
            {
                this.ShowMessage("TY_M_MR_2BF8A365");

                this.FPS91_TY_S_AC_3CHBD783.Initialize();
                this.FPS91_TY_S_AC_3CHBE784.Initialize();
            }
            else if (this.FPS91_TY_S_AC_3CHBC782.GetValue("AMT").ToString() == "0")
            {
                this.ShowMessage("TY_M_AC_3CA7D692");

                this.FPS91_TY_S_AC_3CHBD783.Initialize();
                this.FPS91_TY_S_AC_3CHBE784.Initialize();
            }
            else if (this.FPS91_TY_S_AC_3CHBC782.GetValue("TAXGUBN").ToString() == "58" || this.FPS91_TY_S_AC_3CHBC782.GetValue("TAXGUBN").ToString() == "57")
            {
                fsS4TAXGUBN  = this.FPS91_TY_S_AC_3CHBC782.GetValue("TAXGUBN").ToString();
                fsD4CREDITNO = "";
                fsD4CUSTCD   = "";

                UP_SAUPNO_SUM();
            }
        }
        #endregion

        #region Description : 신용카드매출전표 카드번호별 집계 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_AC_3CHBD783_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.FPS91_TY_S_AC_3CHBD783.GetValue("NUMBER").ToString() == "합      계")
            {
                this.ShowMessage("TY_M_MR_2BF8A365");

                this.FPS91_TY_S_AC_3CHBE784.Initialize();
            }
            else
            {
                fsD4CREDITNO = this.FPS91_TY_S_AC_3CHBD783.GetValue("S4CREDITNO").ToString();
                fsD4CUSTCD   = this.FPS91_TY_S_AC_3CHBD783.GetValue("S4CUSTCD").ToString();

                UP_SAUPNO_List();
            }
        }
        #endregion

        #region Description : 세금계산서 사업자별 내역 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_AC_3CHBE784_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column.ToString() == "1")
            {
                if (this.FPS91_TY_S_AC_3CHBE784.GetValue("NUMBER").ToString() == "합      계")
                {
                    this.ShowMessage("TY_M_MR_2BF8A365");
                }
                else
                {
                    string sB2DPMK = this.FPS91_TY_S_AC_3CHBE784.GetValue("D4JPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_AC_3CHBE784.GetValue("D4JPNO").ToString().Substring(6, 8);
                    string sB2NOSQ = this.FPS91_TY_S_AC_3CHBE784.GetValue("D4JPNO").ToString().Substring(14, 3);

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
            }
            else if (sGubn == "2")
            {
                iBLANK = 15 - ((dt.Rows.Count - 5) % 15);
            }
            else if (sGubn == "3")
            {
                iBLANK = 13 - ((dt.Rows.Count - 5) % 13);
            }
            else if (sGubn == "4")
            {
                iBLANK = 26 - ((dt.Rows.Count - 15) % 26);
                if (iBLANK == 26)
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


            if (sGubn == "1" || sGubn == "2")
            {
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
            }
            else if (sGubn == "3")
            {
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
            }
            else
            {
                if (dt != null && dt.Rows.Count > 15)
                {
                    DataRow row;

                    for (i = 1; i <= iBLANK; i++)
                    {
                        row = Retdt.NewRow();

                        row["NUMBER"] = DBNull.Value;
                        row["S4CREDITNO"] = "";
                        row["S4CORPNO"] = "";
                        row["S4TXCNT"] = DBNull.Value;
                        row["S4TXAMT"] = DBNull.Value;
                        row["S4TXVAT"] = DBNull.Value;

                        Retdt.Rows.Add(row);
                    }
                }
                else if (dt.Rows.Count < 15)
                {
                    DataRow row;

                    iBLANK = 15 - (dt.Rows.Count);

                    for (i = 1; i <= iBLANK; i++)
                    {
                        row = Retdt.NewRow();

                        row["NUMBER"] = DBNull.Value;
                        row["S4CREDITNO"] = "";
                        row["S4CORPNO"] = "";
                        row["S4TXCNT"] = DBNull.Value;
                        row["S4TXAMT"] = DBNull.Value;
                        row["S4TXVAT"] = DBNull.Value;

                        Retdt.Rows.Add(row);
                    }
                }
            }

            return Retdt;
        }
        #endregion

        #region Description : 신용카드매출전표 수취명세서 총합계
        private DataTable UP_TOTAL_SUM_14R()
        {
            string sS2TAXCDGN_HAP = string.Empty;
            string sS2TAXCDGN1 = string.Empty;
            string sS2TAXCDGN2 = string.Empty;

            DataTable Retdt = new DataTable();

            DataRow row;

            Retdt.Columns.Add("GUNSU_CNT", typeof(System.String));
            Retdt.Columns.Add("HAP_AMT", typeof(System.String));
            Retdt.Columns.Add("HAP_VAT", typeof(System.String));

            DataTable dt = new DataTable();

            for (int i = 0; i < 3; i++)
            {
                // 총합계
                if (i == 0)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CHBW786",
                        this.TXT01_S4YEAR.GetValue().ToString(),
                        this.CBO01_S4BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S4CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S4CONFGB.GetValue().ToString(), 2),
                        "57,58"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["GUNSU_CNT"] = dt.Rows[0]["GUNSU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 1) // 현금영수증
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CHBW786",
                        this.TXT01_S4YEAR.GetValue().ToString(),
                        this.CBO01_S4BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S4CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S4CONFGB.GetValue().ToString(), 2),
                        "57"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["GUNSU_CNT"] = dt.Rows[0]["GUNSU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i == 2) // 기타 신용카드
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CHBW786",
                        this.TXT01_S4YEAR.GetValue().ToString(),
                        this.CBO01_S4BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S4CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S4CONFGB.GetValue().ToString(), 2),
                        "58"
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        row["GUNSU_CNT"] = dt.Rows[0]["GUNSU_CNT"].ToString();
                        row["HAP_AMT"] = dt.Rows[0]["HAP_AMT"].ToString();
                        row["HAP_VAT"] = dt.Rows[0]["HAP_VAT"].ToString();

                        Retdt.Rows.Add(row);
                    }
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
                this.TXT01_S4YEAR.SetValue(TYCookie.Year);
                this.CBO01_S4BRANCH.SetValue(TYCookie.Branch);
                this.CBO01_S4CONFGB.SetValue(TYCookie.Confgb);
            }
            else
            {
                this.TXT01_S4YEAR.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 4));
            }
        }
        #endregion

        #region Description : 쿠키 저장
        private void UP_Cookie_Save()
        {
            TYCookie.Save(this.TXT01_S4YEAR.GetValue().ToString(), this.CBO01_S4BRANCH.GetValue().ToString(), this.CBO01_S4CONFGB.GetValue().ToString());
        }
        #endregion
    }
}