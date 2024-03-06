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
    public partial class TYACTX016S : TYBase
    {
        string fsS7YEAR    = string.Empty;
        string fsS7BRANCH  = string.Empty;
        string fsS7CONFGB  = string.Empty;
        string fsS7EXPGUBN = string.Empty;
        string fsPOPUP     = string.Empty;

        #region Description : 페이지 로드
        public TYACTX016S()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_3CK1H837, "S7CURRCD", "FRDESC", "S7CURRCD");
        }

        public TYACTX016S(string sS7YEAR, string sS7BRANCH, string sS7CONFGB, string sPOPUP)
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_3CK1H837, "S7CURRCD", "FRDESC", "S7CURRCD");

            fsS7YEAR    = sS7YEAR.ToString();
            fsS7BRANCH  = sS7BRANCH.ToString();
            fsS7CONFGB  = sS7CONFGB.ToString();
            fsPOPUP     = sPOPUP.ToString();

            // 폼사이즈 조정
            this.ClientSize = new System.Drawing.Size(1184, 750);
        }

        private void TYACTX016S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.FPS91_TY_S_AC_3CK1H837.Initialize();

            if (fsPOPUP.ToString() == "")
            {
                UP_Cookie_Load();
            }
            else
            {
                this.TXT01_S7YEAR.SetValue(fsS7YEAR.ToString());
                this.CBO01_S7BRANCH.SetValue(fsS7BRANCH.ToString());
                this.CBO01_S7CONFGB.SetValue(fsS7CONFGB.ToString());

                this.BTN61_INQ_Click(null, null);
            }

            this.TXT01_S7FORGIAMT.SetReadOnly(true);
            this.TXT01_S7WONHAAMT.SetReadOnly(true);

            SetStartingFocus(this.TXT01_S7YEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            fsS7YEAR    = this.TXT01_S7YEAR.GetValue().ToString();
            fsS7BRANCH  = this.CBO01_S7BRANCH.GetValue().ToString();
            fsS7CONFGB  = this.CBO01_S7CONFGB.GetValue().ToString();

            fsS7EXPGUBN = "";

            this.FPS91_TY_S_AC_3CK1H837.Initialize();

            // 세금계산서 총합계
            this.FPS91_TY_S_AC_3CK1C836.SetValue(UP_TOTAL_SUM());

            for (int i = 0; i < this.FPS91_TY_S_AC_3CK1C836.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_3CK1C836.GetValue(i, "GUBUN").ToString() == "")
                {
                    // 특정 ROW 글자 크기 변경
                    //this.FPS91_TY_S_AC_3CK1C836.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                }

                if (this.FPS91_TY_S_AC_3CK1C836.GetValue(i, "GUBUN").ToString() == "HAP")
                {
                    this.FPS91_TY_S_AC_3CK1C836.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_3CK1C836.ActiveSheet.Rows[i] .BackColor = Color.FromArgb(218, 239, 244);
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
                this.DbConnector.Attach("TY_P_AC_3CK23841", ds.Tables[0].Rows[i]["S7SINNO"].ToString(),
                                                            ds.Tables[0].Rows[i]["S7CURRCD"].ToString(),
                                                            ds.Tables[0].Rows[i]["S7CXCHAN"].ToString(),
                                                            ds.Tables[0].Rows[i]["S7FORGIAMT"].ToString(),
                                                            this.TXT01_S7YEAR.GetValue().ToString(),
                                                            this.CBO01_S7BRANCH.GetValue().ToString(),
                                                            getCONFGB(this.CBO01_S7CONFGB.GetValue().ToString(), 1),
                                                            getCONFGB(this.CBO01_S7CONFGB.GetValue().ToString(), 2),
                                                            fsS7EXPGUBN.ToString(),
                                                            ds.Tables[0].Rows[i]["S7SHIPDT"].ToString(),
                                                            ds.Tables[0].Rows[i]["S7JPNO"].ToString()
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");

            string sS7EXPGUBN = string.Empty;

            sS7EXPGUBN = fsS7EXPGUBN.ToString();

            if ((new TYACTX019S(this.TXT01_S7YEAR.GetValue().ToString(),   this.CBO01_S7BRANCH.GetValue().ToString(),
                                this.CBO01_S7CONFGB.GetValue().ToString(), "POPUP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BTN61_INQ_Click(null, null);

                UP_SET_LIST(sS7EXPGUBN.ToString());
            }
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            int i = 0;
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_57OC1662", this.TXT01_S7YEAR.GetValue().ToString(),
                                                            this.CBO01_S7BRANCH.GetValue().ToString(),
                                                            getCONFGB(this.CBO01_S7CONFGB.GetValue().ToString(), 1),
                                                            getCONFGB(this.CBO01_S7CONFGB.GetValue().ToString(), 2),
                                                            fsS7EXPGUBN.ToString(),
                                                            ds.Tables[0].Rows[i]["S7SHIPDT"].ToString(),
                                                            ds.Tables[0].Rows[i]["S7JPNO"].ToString()
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            string sS7EXPGUBN = string.Empty;

            sS7EXPGUBN = fsS7EXPGUBN.ToString();

            if ((new TYACTX019S(this.TXT01_S7YEAR.GetValue().ToString(), this.CBO01_S7BRANCH.GetValue().ToString(),
                                this.CBO01_S7CONFGB.GetValue().ToString(), "POPUP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BTN61_INQ_Click(null, null);

                UP_SET_LIST(sS7EXPGUBN.ToString());
            }
        }
        #endregion

        #region Description : 수출실적명세서 총합계
        private DataTable UP_TOTAL_SUM()
        {
            string sS7EXPGUBN  = string.Empty;
            string sTITLE      = string.Empty;
            string sGUBUN      = string.Empty;

            DataTable Retdt = new DataTable();

            DataRow row;

            Retdt.Columns.Add("TITLE",      typeof(System.String));
            Retdt.Columns.Add("CNT",        typeof(System.String));
            Retdt.Columns.Add("S7FORGIAMT", typeof(System.String));
            Retdt.Columns.Add("S7WONHAAMT", typeof(System.String));
            Retdt.Columns.Add("BIGO",       typeof(System.String));
            Retdt.Columns.Add("GUBUN",      typeof(System.String));

            DataTable dt = new DataTable();

            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        sS7EXPGUBN = "";
                        sTITLE     = "⑨합계";
                        sGUBUN     = "HAP";

                        break;

                    case 1:
                        sS7EXPGUBN = "1";
                        sTITLE     = "⑩수출재화(=⑫합계)";
                        sGUBUN     = "1";

                        break;
                    case 2:
                        sS7EXPGUBN = "2";
                        sTITLE     = "⑪기타영세율적용";
                        sGUBUN     = "2";

                        break;
                }
                
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3CK13833",
                    this.TXT01_S7YEAR.GetValue().ToString(),
                    this.CBO01_S7BRANCH.GetValue().ToString(),
                    getCONFGB(this.CBO01_S7CONFGB.GetValue().ToString(), 1),
                    getCONFGB(this.CBO01_S7CONFGB.GetValue().ToString(), 2),
                    sS7EXPGUBN.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    row = Retdt.NewRow();

                    row["TITLE"]      = sTITLE.ToString();
                    row["CNT"]        = dt.Rows[0]["CNT"].ToString();
                    row["S7FORGIAMT"] = dt.Rows[0]["S7FORGIAMT"].ToString();
                    row["S7WONHAAMT"] = dt.Rows[0]["S7WONHAAMT"].ToString();
                    row["BIGO"]       = "";
                    row["GUBUN"]      = sGUBUN.ToString();

                    Retdt.Rows.Add(row);
                }
            }

            return Retdt;
        }
        #endregion

        #region Description : 수출실적명세서 리스트
        private void UP_SET_LIST(string sS7EXPGUBN)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3CK1Q838",
                fsS7YEAR.ToString(),
                fsS7BRANCH.ToString(),
                getCONFGB(this.fsS7CONFGB.ToString(), 1),
                getCONFGB(this.fsS7CONFGB.ToString(), 2),
                sS7EXPGUBN.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_3CK1H837.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_AC_3CK1H837.ActiveSheet.RowCount; i++)
            {
                //if (this.FPS91_TY_S_AC_3CK1H837.GetValue(i, "S7SHIPDT").ToString() == "합       계")
                //{
                //    this.FPS91_TY_S_AC_3CK1H837.ActiveSheet.Rows[i].Locked = true;

                //    this.FPS91_TY_S_AC_3CK1H837.ActiveSheet.Cells[i, 5, i, 6].Locked = true;

                //    this.FPS91_TY_S_AC_3CK1H837.ActiveSheet.Rows[i].ForeColor = Color.Red;
                //    this.FPS91_TY_S_AC_3CK1H837.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                //}
            }

            // 수출실적명세서 총합계 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3CK13833",
                fsS7YEAR.ToString(),
                fsS7BRANCH.ToString(),
                getCONFGB(this.fsS7CONFGB.ToString(), 1),
                getCONFGB(this.fsS7CONFGB.ToString(), 2),
                sS7EXPGUBN.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_S7FORGIAMT.SetValue(dt.Rows[0]["S7FORGIAMT"].ToString());
                this.TXT01_S7WONHAAMT.SetValue(dt.Rows[0]["S7WONHAAMT"].ToString());
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42B8L317",
                this.TXT01_S7YEAR.GetValue().ToString(),
                this.CBO01_S7BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S7CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S7CONFGB.GetValue().ToString(), 2)
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_3CK1H837.ActiveSheet.RowCount; i++)
                {
                    this.FPS91_TY_S_AC_3CK1H837_Sheet1.Cells[i, 4].Locked = true;
                    this.FPS91_TY_S_AC_3CK1H837_Sheet1.Cells[i, 5].Locked = true;
                    this.FPS91_TY_S_AC_3CK1H837_Sheet1.Cells[i, 6].Locked = true;
                    this.FPS91_TY_S_AC_3CK1H837_Sheet1.Cells[i, 7].Locked = true;
                    this.FPS91_TY_S_AC_3CK1H837_Sheet1.Cells[i, 8].Locked = true;
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
                "TY_P_AC_3CN14860",
                this.TXT01_S7YEAR.GetValue().ToString(),
                this.CBO01_S7BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S7CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S7CONFGB.GetValue().ToString(), 2),
                "1"
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            DataTable dt2 = UP_TOTAL_SUM();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3C910655",
                this.TXT01_S7YEAR.GetValue().ToString(),
                getCONFGB(this.CBO01_S7CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S7CONFGB.GetValue().ToString(), 2),
                this.CBO01_S7BRANCH.GetValue().ToString()
                );

            DataTable dt3 = this.DbConnector.ExecuteDataTable();

            SectionReport rpt = new TYACTX016R1(dt2, dt3, this.TXT01_S7YEAR.GetValue().ToString(), getCONFGB(this.CBO01_S7CONFGB.GetValue().ToString(), 1), getCONFGB(this.CBO01_S7CONFGB.GetValue().ToString(), 2));
            (new TYERGB001P(rpt, UP_ConvertHap(dt))).ShowDialog();

            UP_Cookie_Save();
        }
        #endregion

        #region Description : 건물등 감가상각 자산취득명세서 총합계 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_AC_3CK1C836_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.FPS91_TY_S_AC_3CK1C836.GetValue("GUBUN").ToString() == "HAP")
            {
                this.ShowMessage("TY_M_MR_2BF8A365");

                this.FPS91_TY_S_AC_3CK1H837.Initialize();
            }
            else if (this.FPS91_TY_S_AC_3CK1C836.GetValue("S7FORGIAMT").ToString() == "0" &&
                     this.FPS91_TY_S_AC_3CK1C836.GetValue("S7WONHAAMT").ToString() == "0")
            {
                this.ShowMessage("TY_M_AC_3CA7D692");

                this.FPS91_TY_S_AC_3CK1H837.Initialize();
            }
            else
            {
                fsS7EXPGUBN = this.FPS91_TY_S_AC_3CK1C836.GetValue("GUBUN").ToString();

                UP_SET_LIST(fsS7EXPGUBN.ToString());
            }
        }
        #endregion

        #region Description : 거래처별 감가상각자산 취득명세 스프레드 이벤트
        private void FPS91_TY_S_AC_3CK1H837_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == 1)
            {
                string sB2DPMK = this.FPS91_TY_S_AC_3CK1H837.GetValue("S7JPNO").ToString().Substring(0, 6);
                string sB2DTMK = this.FPS91_TY_S_AC_3CK1H837.GetValue("S7JPNO").ToString().Substring(6, 8);
                string sB2NOSQ = this.FPS91_TY_S_AC_3CK1H837.GetValue("S7JPNO").ToString().Substring(14, 3);

                if (this.OpenModalPopup(new TYACBJ001I(sB2DPMK, sB2DTMK, sB2NOSQ)) == System.Windows.Forms.DialogResult.OK)
                {
                }
            }
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_3CK1H837.GetDataSourceInclude(TSpread.TActionType.Update, "S7SINNO", "S7CURRCD", "S7CXCHAN", "S7FORGIAMT", "S7SHIPDT", "S7JPNO"));

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
                this.TXT01_S7YEAR.GetValue().ToString(),
                this.CBO01_S7BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S7CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S7CONFGB.GetValue().ToString(), 2)
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_42B8N318");
                e.Successed = false;
                return;
            }

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["S7CURRCD"].ToString() != "")
                {
                    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["S7FORGIAMT"].ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_AC_3CK2B843");
                        e.Successed = false;
                        return;
                    }

                    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["S7CXCHAN"].ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_AC_3CK2B845");
                        e.Successed = false;
                        return;
                    }
                }

                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["S7FORGIAMT"].ToString())) != 0)
                {
                    if (ds.Tables[0].Rows[i]["S7CURRCD"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_AC_3CK2B846");
                        e.Successed = false;
                        return;
                    }

                    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["S7CXCHAN"].ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_AC_3CK2B845");
                        e.Successed = false;
                        return;
                    }
                }

                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["S7CXCHAN"].ToString())) != 0)
                {
                    if (ds.Tables[0].Rows[i]["S7CURRCD"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_AC_3CK2B846");
                        e.Successed = false;
                        return;
                    }

                    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["S7FORGIAMT"].ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_AC_3CK2B846");
                        e.Successed = false;
                        return;
                    }
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

            ds.Tables.Add(this.FPS91_TY_S_AC_3CK1H837.GetDataSourceInclude(TSpread.TActionType.Remove, "S7SINNO", "S7CURRCD", "S7CXCHAN", "S7FORGIAMT", "S7SHIPDT", "S7JPNO"));

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
                this.TXT01_S7YEAR.GetValue().ToString(),
                this.CBO01_S7BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S7CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S7CONFGB.GetValue().ToString(), 2)
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


        #region Description : 수출실적명세서 데이터셋 변환
        protected DataTable UP_ConvertHap(DataTable dt)
        {
            int iBLANK = 0;

            iBLANK = 22 - ((dt.Rows.Count - 12) % 22);
            if (iBLANK == 22)
            {
                iBLANK = 0;
            }
            
            int i = 0;

            DataTable Retdt = dt;
            
            if (dt != null && dt.Rows.Count > 12)
            {
                DataRow row;

                for (i = 1; i <= iBLANK; i++)
                {
                    row = Retdt.NewRow();

                    row["NUMBER"] = DBNull.Value;
                    row["S7SHIPDT"] = "";
                    row["S7CUSTCD"] = "";
                    row["S7CURRCD"] = DBNull.Value;
                    row["FRDESC"] = DBNull.Value;
                    row["S7CXCHAN"] = DBNull.Value;
                    row["S7FORGIAMT"] = DBNull.Value;
                    row["S7WONHAAMT"] = DBNull.Value;

                    Retdt.Rows.Add(row);
                }
            }
            else if (dt.Rows.Count < 12)
            {
                DataRow row;

                iBLANK = 12 - (dt.Rows.Count);

                for (i = 1; i <= iBLANK; i++)
                {
                    row = Retdt.NewRow();

                    row["NUMBER"] = DBNull.Value;
                    row["S7SHIPDT"] = "";
                    row["S7CUSTCD"] = "";
                    row["S7CURRCD"] = DBNull.Value;
                    row["FRDESC"] = DBNull.Value;
                    row["S7CXCHAN"] = DBNull.Value;
                    row["S7FORGIAMT"] = DBNull.Value;
                    row["S7WONHAAMT"] = DBNull.Value;

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
                this.TXT01_S7YEAR.SetValue(TYCookie.Year);
                this.CBO01_S7BRANCH.SetValue(TYCookie.Branch);
                this.CBO01_S7CONFGB.SetValue(TYCookie.Confgb);
            }
            else
            {
                this.TXT01_S7YEAR.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 4));
            }
        }
        #endregion

        #region Description : 쿠키 저장
        private void UP_Cookie_Save()
        {
            TYCookie.Save(this.TXT01_S7YEAR.GetValue().ToString(), this.CBO01_S7BRANCH.GetValue().ToString(), this.CBO01_S7CONFGB.GetValue().ToString());
        }
        #endregion

       

       
    }
}