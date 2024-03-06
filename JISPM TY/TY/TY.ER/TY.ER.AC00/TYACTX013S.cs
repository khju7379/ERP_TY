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
    public partial class TYACTX013S : TYBase
    {
        string fsS3YEAR    = string.Empty;
        string fsS3BRANCH  = string.Empty;
        string fsS3CONFGB  = string.Empty;
        string fsS3RPTCDAC = string.Empty;

        string fsPOPUP     = string.Empty;

        #region Description : 페이지 로드
        public TYACTX013S()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_3CG45771, "S3RPTCDAC", "S3RPTCDACNM", "S3RPTCDAC");
        }

        public TYACTX013S(string sS3YEAR, string sS3BRANCH, string sS3CONFGB, string sPOPUP)
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_3CG45771, "S3RPTCDAC", "S3RPTCDACNM", "S3RPTCDAC");

            fsS3YEAR    = sS3YEAR.ToString();
            fsS3BRANCH  = sS3BRANCH.ToString();
            fsS3CONFGB  = sS3CONFGB.ToString();

            fsPOPUP     = sPOPUP.ToString();

            // 폼사이즈 조정
            this.ClientSize = new System.Drawing.Size(1184, 750);
        }

        private void TYACTX013S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.FPS91_TY_S_AC_3CG45771.Initialize();

            if (fsPOPUP.ToString() == "")
            {
                UP_Cookie_Load();
            }
            else
            {
                this.TXT01_S3YEAR.SetValue(fsS3YEAR.ToString());
                this.CBO01_S3BRANCH.SetValue(fsS3BRANCH.ToString());
                this.CBO01_S3CONFGB.SetValue(fsS3CONFGB.ToString());

                this.BTN61_INQ_Click(null, null);
            }

            this.TXT01_S3TXAMT.SetReadOnly(true);
            this.TXT01_S3TXVAT.SetReadOnly(true);

            SetStartingFocus(this.TXT01_S3YEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            fsS3YEAR    = this.TXT01_S3YEAR.GetValue().ToString();
            fsS3BRANCH  = this.CBO01_S3BRANCH.GetValue().ToString();
            fsS3CONFGB  = this.CBO01_S3CONFGB.GetValue().ToString();

            fsS3RPTCDAC = "";

            this.FPS91_TY_S_AC_3CG45771.Initialize();

            // 세금계산서 총합계
            this.FPS91_TY_S_AC_3CG2D765.SetValue(UP_TOTAL_SUM());

            for (int i = 0; i < this.FPS91_TY_S_AC_3CG2D765.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_3CG2D765.GetValue(i, "GUBUN").ToString() == "")
                {
                    // 특정 ROW 글자 크기 변경
                    //this.FPS91_TY_S_AC_3CG2D765.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                }

                if (this.FPS91_TY_S_AC_3CG2D765.GetValue(i, "GUBUN").ToString() == "HAP")
                {
                    this.FPS91_TY_S_AC_3CG2D765.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_3CG2D765.ActiveSheet.Rows[i] .BackColor = Color.FromArgb(218, 239, 244);
                }

                if (this.FPS91_TY_S_AC_3CG2D765.GetValue(i, "BIGO").ToString() != "")
                {
                    this.FPS91_TY_S_AC_3CG2D765.ActiveSheet.Columns["BIGO"].ForeColor = Color.Red;
                }
            }

            UP_Cookie_Save();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_3CG4S774", ds.Tables[0].Rows[i]["S3RPTCDAC"].ToString(),
                                                            this.TXT01_S3YEAR.GetValue().ToString(),
                                                            this.CBO01_S3BRANCH.GetValue().ToString(),
                                                            getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 1),
                                                            getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 2),
                                                            ds.Tables[0].Rows[i]["S3TAXGUBN"].ToString(),
                                                            ds.Tables[0].Rows[i]["S3JPNO"].ToString()
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");

            string sS3RPTCDAC = string.Empty;

            sS3RPTCDAC = fsS3RPTCDAC.ToString();

            if ((new TYACTX019S(this.TXT01_S3YEAR.GetValue().ToString(),   this.CBO01_S3BRANCH.GetValue().ToString(),
                                this.CBO01_S3CONFGB.GetValue().ToString(), "POPUP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BTN61_INQ_Click(null, null);

                UP_SAUPNO_SUM(sS3RPTCDAC.ToString());
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            int i = 0;
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_42562211", this.TXT01_S3YEAR.GetValue().ToString(),
                                                            this.CBO01_S3BRANCH.GetValue().ToString(),
                                                            getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 1),
                                                            getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 2),
                                                            ds.Tables[0].Rows[i]["S3TAXGUBN"].ToString(),
                                                            ds.Tables[0].Rows[i]["S3JPNO"].ToString()
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            if ((new TYACTX019S(this.TXT01_S3YEAR.GetValue().ToString(),   this.CBO01_S3BRANCH.GetValue().ToString(),
                                this.CBO01_S3CONFGB.GetValue().ToString(), "POPUP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 건물등 감가상각자산 취득명세 총합계
        private DataTable UP_TOTAL_SUM()
        {
            string sS3RPTCDAC  = string.Empty;
            string sTITLE      = string.Empty;
            string sS3TAXCDGN2 = string.Empty;

            DataTable Retdt = new DataTable();

            DataRow row;

            //Retdt.Columns.Add("TITLE1", typeof(System.String));
            Retdt.Columns.Add("TITLE2", typeof(System.String));
            Retdt.Columns.Add("CNT",    typeof(System.String));
            Retdt.Columns.Add("AMT",    typeof(System.String));
            Retdt.Columns.Add("VAT",    typeof(System.String));
            Retdt.Columns.Add("BIGO",   typeof(System.String));
            Retdt.Columns.Add("GUBUN",  typeof(System.String));

            DataTable dt = new DataTable();

            for (int i = 0; i < 5; i++)
            {
                switch (i)
                {
                    case 1:
                        sS3RPTCDAC = "12200200,12200300";
                        sTITLE     = "(1)건물·구축물";

                        break;
                    case 2:
                        sS3RPTCDAC = "12200400";
                        sTITLE     = "(2)기 계 장 치";

                        break;
                    case 3:
                        sS3RPTCDAC = "12200500,12200600";
                        sTITLE     = "(3)차량 운반구";

                        break;
                    case 4:
                        sS3RPTCDAC = "12200700,12200800,12200900,12210000,11101001";
                        sTITLE     = "(4)기타 감가상각자산";

                        break;
                }
                
                // 총합계
                if (i == 0)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CG2L766",
                        this.TXT01_S3YEAR.GetValue().ToString(),
                        this.CBO01_S3BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 2)
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        //row["TITLE1"] = "취득내역";
                        row["TITLE2"] = "합    계";
                        row["CNT"]    = dt.Rows[0]["CNT"].ToString();
                        row["AMT"]    = dt.Rows[0]["AMT"].ToString();
                        row["VAT"]    = dt.Rows[0]["VAT"].ToString();
                        row["BIGO"]   = dt.Rows[0]["BIGO"].ToString();
                        row["GUBUN"]  = "HAP";

                        Retdt.Rows.Add(row);
                    }
                }
                else if (i != 0)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CG2O767",
                        this.TXT01_S3YEAR.GetValue().ToString(),
                        this.CBO01_S3BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 2),
                        this.TXT01_S3YEAR.GetValue().ToString(),
                        this.CBO01_S3BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 2),
                        sS3RPTCDAC.ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        row = Retdt.NewRow();

                        //row["TITLE1"] = "취득내역";
                        row["TITLE2"] = sTITLE.ToString();
                        row["CNT"]    = dt.Rows[0]["CNT"].ToString();
                        row["AMT"]    = dt.Rows[0]["AMT"].ToString();
                        row["VAT"]    = dt.Rows[0]["VAT"].ToString();
                        if (i != 4)
                        {
                            row["BIGO"] = "";
                        }
                        else
                        {
                            if (dt.Rows[0]["BIGO"].ToString() == "0건")
                            {
                                row["BIGO"] = "";
                            }
                            else
                            {
                                row["BIGO"] = dt.Rows[0]["BIGO"].ToString();
                            }
                        }
                        row["GUBUN"]  = sS3RPTCDAC.ToString();

                        Retdt.Rows.Add(row);
                    }
                }
            }

            return Retdt;
        }
        #endregion

        #region Description : 거래처별 감가상각자산 취득명세 리스트
        private void UP_SAUPNO_SUM(string sS3RPTCDAC)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3CG43770",
                fsS3YEAR.ToString(),
                fsS3BRANCH.ToString(),
                getCONFGB(this.fsS3CONFGB.ToString(), 1),
                getCONFGB(this.fsS3CONFGB.ToString(), 2),
                sS3RPTCDAC.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_3CG45771.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_AC_3CG45771.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_3CG45771.GetValue(i, "S3RPTCDAC").ToString() == "11101001" || this.FPS91_TY_S_AC_3CG45771.GetValue(i, "S3RPTCDAC").ToString() == "12210000")
                {
                    this.FPS91_TY_S_AC_3CG45771.ActiveSheet.Cells[i,9,i,10].ForeColor = Color.Red;

                    //this.FPS91_TY_S_AC_3CG45771.ActiveSheet.Rows[i].ForeColor = Color.Red;
                }
            }

            // 건물등 감가상각자산 취득명세 거래처별 합계 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3CH8Y775",
                fsS3YEAR.ToString(),
                fsS3BRANCH.ToString(),
                getCONFGB(this.fsS3CONFGB.ToString(), 1),
                getCONFGB(this.fsS3CONFGB.ToString(), 2),
                sS3RPTCDAC.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_S3TXAMT.SetValue(dt.Rows[0]["S3TXAMT"].ToString());
                this.TXT01_S3TXVAT.SetValue(dt.Rows[0]["S3TXVAT"].ToString());
            }

            // 마감체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42B8L317",
                this.TXT01_S3YEAR.GetValue().ToString(),
                this.CBO01_S3BRANCH.GetValue().ToString(),
                getCONFGB(this.fsS3CONFGB.ToString(), 1),
                getCONFGB(this.fsS3CONFGB.ToString(), 2)
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_3CG45771.ActiveSheet.RowCount; i++)
                {
                    this.FPS91_TY_S_AC_3CG45771_Sheet1.Cells[i, 9].Locked = true;
                    this.FPS91_TY_S_AC_3CG45771_Sheet1.Cells[i, 10].Locked = true;
                }
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3C910655",
                this.TXT01_S3YEAR.GetValue().ToString(),
                getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 2),
                this.CBO01_S3BRANCH.GetValue().ToString()
                );
            
            DataTable dt = this.DbConnector.ExecuteDataTable();

            DataTable dt2 = UP_TOTAL_SUM_13R();
            SectionReport rpt = new TYACTX013R(dt, this.TXT01_S3YEAR.GetValue().ToString(), getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 1), getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 2));
            (new TYERGB001P(rpt, dt2)).ShowDialog();

            UP_Cookie_Save();
        }
        #endregion

        #region Description : 건물등 감가상각 자산취득명세서 총합계 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_AC_3CG2D765_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.FPS91_TY_S_AC_3CG2D765.GetValue("GUBUN").ToString() == "HAP")
            {
                this.ShowMessage("TY_M_MR_2BF8A365");

                this.FPS91_TY_S_AC_3CG45771.Initialize();
            }
            else if (this.FPS91_TY_S_AC_3CG2D765.GetValue("AMT").ToString() == "0")
            {
                this.ShowMessage("TY_M_AC_3CA7D692");

                this.FPS91_TY_S_AC_3CG45771.Initialize();
            }
            else
            {
                fsS3RPTCDAC = this.FPS91_TY_S_AC_3CG2D765.GetValue("GUBUN").ToString();

                UP_SAUPNO_SUM(fsS3RPTCDAC.ToString());
            }
        }
        #endregion

        #region Description : 거래처별 감가상각자산 취득명세 스프레드 이벤트
        private void FPS91_TY_S_AC_3CG45771_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sB2DPMK = this.FPS91_TY_S_AC_3CG45771.GetValue("S3JPNO").ToString().Substring(0, 6);
            string sB2DTMK = this.FPS91_TY_S_AC_3CG45771.GetValue("S3JPNO").ToString().Substring(6, 8);
            string sB2NOSQ = this.FPS91_TY_S_AC_3CG45771.GetValue("S3JPNO").ToString().Substring(14, 3);

            if (this.OpenModalPopup(new TYACBJ001I(sB2DPMK, sB2DTMK, sB2NOSQ)) == System.Windows.Forms.DialogResult.OK)
            {
            }
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_3CG45771.GetDataSourceInclude(TSpread.TActionType.Update, "S3RPTCDAC", "S3TAXGUBN", "S3JPNO"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42B8L317",
                this.TXT01_S3YEAR.GetValue().ToString(),
                this.CBO01_S3BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 2)
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_42B8N318");
                e.Successed = false;
                return;
            }

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["S3RPTCDAC"].ToString() == "11101001" || ds.Tables[0].Rows[i]["S3RPTCDAC"].ToString() == "12210000")
                {
                    this.ShowMessage("TY_M_AC_3CG4O773");
                    e.Successed = false;
                    return;
                }
            }

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["S3RPTCDAC"].ToString() != "12200200" &&
                    ds.Tables[0].Rows[i]["S3RPTCDAC"].ToString() != "12200300" &&
                    ds.Tables[0].Rows[i]["S3RPTCDAC"].ToString() != "12200400" &&
                    ds.Tables[0].Rows[i]["S3RPTCDAC"].ToString() != "12200600" &&
                    ds.Tables[0].Rows[i]["S3RPTCDAC"].ToString() != "12200700" &&
                    ds.Tables[0].Rows[i]["S3RPTCDAC"].ToString() != "12200800" &&
                    ds.Tables[0].Rows[i]["S3RPTCDAC"].ToString() != "12200900" 
                    )
                {
                    this.ShowMessage("TY_M_AC_43D2Q719");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_3CG45771.GetDataSourceInclude(TSpread.TActionType.Remove, "S3TAXGUBN", "S3JPNO"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42B8L317",
                this.TXT01_S3YEAR.GetValue().ToString(),
                this.CBO01_S3BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 2)
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_42B8N318");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 세금계산서 합계표 데이터셋 변환
        protected DataTable UP_ConvertHap(DataTable dt, string sGubn)
        {
            string sNUMBER = string.Empty;
            string sS3SAUPNO = string.Empty;
            string sVNSANGHO = string.Empty;
            string sS3TAXCDGN = string.Empty;
            string sCNT = string.Empty;
            string sAMT = string.Empty;
            string sVAT = string.Empty;
            int iBLANK = 0;

            if (sGubn == "1")
            {
                iBLANK = 14 - ((dt.Rows.Count - 5) % 14);
            }
            else
            {
                iBLANK = 15 - ((dt.Rows.Count - 5) % 15);
            }

            int i = 0;

            sNUMBER = "";
            sS3SAUPNO = "";
            sVNSANGHO = "";
            sS3TAXCDGN = "";
            sCNT = "";
            sAMT = "";
            sVAT = "";

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
                    row["CNT"] = DBNull.Value;
                    row["AMT"] = DBNull.Value;
                    row["VAT"] = DBNull.Value;

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
                    row["CNT"] = DBNull.Value;
                    row["AMT"] = DBNull.Value;
                    row["VAT"] = DBNull.Value;

                    Retdt.Rows.Add(row);
                }
            }

            return Retdt;
        }
        #endregion

        #region Description : 건물등감가상각자산 총합계 출력
        private DataTable UP_TOTAL_SUM_13R()
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

            for (int i = 0; i < 5; i++)
            {
                // 총합계
                if (i == 0)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CG30769",
                        this.TXT01_S3YEAR.GetValue().ToString(),
                        this.CBO01_S3BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 2),
                        "12200200,12200300,12200400,12200500,12200600,12200700,12200800,12200900,12210000,11101001"
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
                else if (i == 1) // 건물.구축물
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CG30769",
                        this.TXT01_S3YEAR.GetValue().ToString(),
                        this.CBO01_S3BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 2),
                        "12200200,12200300"
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
                else if (i == 2) // 기계장치
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CG30769",
                        this.TXT01_S3YEAR.GetValue().ToString(),
                        this.CBO01_S3BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 2),
                        "12200400"
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
                else if (i == 3) // 차량 운반구
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CG30769",
                        this.TXT01_S3YEAR.GetValue().ToString(),
                        this.CBO01_S3BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 2),
                        "12200500,12200600"
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

                else if (i == 4) // 기타 감가상각자산
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_3CG30769",
                        this.TXT01_S3YEAR.GetValue().ToString(),
                        this.CBO01_S3BRANCH.GetValue().ToString(),
                        getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 1),
                        getCONFGB(this.CBO01_S3CONFGB.GetValue().ToString(), 2),
                        "12200700,12200800,12200900,12210000,11101001"
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
                this.TXT01_S3YEAR.SetValue(TYCookie.Year);
                this.CBO01_S3BRANCH.SetValue(TYCookie.Branch);
                this.CBO01_S3CONFGB.SetValue(TYCookie.Confgb);
            }
            else
            {
                this.TXT01_S3YEAR.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 4));
            }
        }
        #endregion

        #region Description : 쿠키 저장
        private void UP_Cookie_Save()
        {
            TYCookie.Save(this.TXT01_S3YEAR.GetValue().ToString(), this.CBO01_S3BRANCH.GetValue().ToString(), this.CBO01_S3CONFGB.GetValue().ToString());
        }
        #endregion
    }
}