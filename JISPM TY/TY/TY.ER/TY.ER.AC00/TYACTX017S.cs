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
    public partial class TYACTX017S : TYBase
    {
        string fsS8YEAR    = string.Empty;
        string fsS8BRANCH  = string.Empty;
        string fsS8CONFGB  = string.Empty;

        string fsPOPUP = string.Empty;

        #region Description : 페이지 로드
        public TYACTX017S()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_3CNBW859, "S8CURRCD", "FRDESC", "S8CURRCD");
        }

        public TYACTX017S(string sS8YEAR, string sS8BRANCH, string sS8CONFGB, string sPOPUP)
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_3CNBW859, "S8CURRCD", "FRDESC", "S8CURRCD");

            fsS8YEAR    = sS8YEAR.ToString();
            fsS8BRANCH  = sS8BRANCH.ToString();
            fsS8CONFGB  = sS8CONFGB.ToString();
            fsPOPUP     = sPOPUP.ToString();

            // 폼사이즈 조정
            this.ClientSize = new System.Drawing.Size(1184, 750);
        }

        private void TYACTX017S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            if (fsPOPUP.ToString() == "")
            {
                UP_Cookie_Load();
            }
            else
            {
                this.TXT01_S8YEAR.SetValue(fsS8YEAR.ToString());
                this.CBO01_S8BRANCH.SetValue(fsS8BRANCH.ToString());
                this.CBO01_S8CONFGB.SetValue(fsS8CONFGB.ToString());

                this.BTN61_INQ_Click(null, null);
            }

            this.TXT01_S8SBFORAMT.SetReadOnly(true);
            this.TXT01_S8SBWONAMT.SetReadOnly(true);

            SetStartingFocus(this.TXT01_S8YEAR);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3CNBW858",
                this.TXT01_S8YEAR.GetValue().ToString(),
                this.CBO01_S8BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 2)
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_3CNBW859.SetValue(dt);

            // 총합계
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3CVCG908",
                this.TXT01_S8YEAR.GetValue().ToString(),
                this.CBO01_S8BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 2)
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_S8SBFORAMT.SetValue(dt.Rows[0]["S8SBFORAMT"].ToString());
                this.TXT01_S8SBWONAMT.SetValue(dt.Rows[0]["S8SBWONAMT"].ToString());
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_42B8L317",
                this.TXT01_S8YEAR.GetValue().ToString(),
                this.CBO01_S8BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 2)
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_AC_3CNBW859.ActiveSheet.RowCount; i++)
                {
                    this.FPS91_TY_S_AC_3CNBW859_Sheet1.Cells[i, 3].Locked = true;
                    this.FPS91_TY_S_AC_3CNBW859_Sheet1.Cells[i, 4].Locked = true;
                    this.FPS91_TY_S_AC_3CNBW859_Sheet1.Cells[i, 5].Locked = true;
                    this.FPS91_TY_S_AC_3CNBW859_Sheet1.Cells[i, 6].Locked = true;
                    this.FPS91_TY_S_AC_3CNBW859_Sheet1.Cells[i, 7].Locked = true;
                    this.FPS91_TY_S_AC_3CNBW859_Sheet1.Cells[i, 8].Locked = true;
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
                this.DbConnector.Attach("TY_P_AC_3CN1O861", ds.Tables[0].Rows[i]["S8DOCNUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["S8ISSUER"].ToString(),
                                                            ds.Tables[0].Rows[i]["S8CURRCD"].ToString(),
                                                            ds.Tables[0].Rows[i]["S8CXCHAN"].ToString(),
                                                            ds.Tables[0].Rows[i]["S8SBFORAMT"].ToString(),
                                                            ds.Tables[0].Rows[i]["S8SBFORAMT"].ToString(),
                                                            this.TXT01_S8YEAR.GetValue().ToString(),
                                                            this.CBO01_S8BRANCH.GetValue().ToString(),
                                                            getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 1),
                                                            getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 2),
                                                            ds.Tables[0].Rows[i]["S8SHIPDT"].ToString(),
                                                            ds.Tables[0].Rows[i]["S8JPNO"].ToString()
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");

            if ((new TYACTX019S(this.TXT01_S8YEAR.GetValue().ToString(),   this.CBO01_S8BRANCH.GetValue().ToString(),
                                this.CBO01_S8CONFGB.GetValue().ToString(), "POPUP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
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
                this.DbConnector.Attach("TY_P_AC_4255V210", this.TXT01_S8YEAR.GetValue().ToString(),
                                                            this.CBO01_S8BRANCH.GetValue().ToString(),
                                                            getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 1),
                                                            getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 2),
                                                            ds.Tables[0].Rows[i]["S8SHIPDT"].ToString(),
                                                            ds.Tables[0].Rows[i]["S8JPNO"].ToString()
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            if ((new TYACTX019S(this.TXT01_S8YEAR.GetValue().ToString(),   this.CBO01_S8BRANCH.GetValue().ToString(),
                                this.CBO01_S8CONFGB.GetValue().ToString(), "POPUP")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3CO2H866",
                this.TXT01_S8YEAR.GetValue().ToString(),
                this.CBO01_S8BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 2)
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3C910655",
                this.TXT01_S8YEAR.GetValue().ToString(),
                getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 2),
                this.CBO01_S8BRANCH.GetValue().ToString()
                );

            DataTable dt2 = this.DbConnector.ExecuteDataTable();

            SectionReport rpt = new TYACTX017R(dt2, this.TXT01_S8YEAR.GetValue().ToString(), getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 1), getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 2));
            (new TYERGB001P(rpt, UP_ConvertHap(dt))).ShowDialog();

            UP_Cookie_Save();
        }
        #endregion

        #region Description : 건물등 감가상각 자산취득명세서 총합계 스프레드 더블클릭 이벤트
        private void FPS91_TY_S_AC_3CNBW859_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == 0)
            {
                string sB2DPMK = this.FPS91_TY_S_AC_3CNBW859.GetValue("S8JPNO").ToString().Substring(0, 6);
                string sB2DTMK = this.FPS91_TY_S_AC_3CNBW859.GetValue("S8JPNO").ToString().Substring(6, 8);
                string sB2NOSQ = this.FPS91_TY_S_AC_3CNBW859.GetValue("S8JPNO").ToString().Substring(14, 3);

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

            ds.Tables.Add(this.FPS91_TY_S_AC_3CNBW859.GetDataSourceInclude(TSpread.TActionType.Update, "S8JPNO", "S8SHIPDT", "S8DOCNUM", "S8ISSUER", "S8CURRCD", "S8CXCHAN", "S8SBFORAMT", "S8SBWONAMT"));

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
                this.TXT01_S8YEAR.GetValue().ToString(),
                this.CBO01_S8BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 2)
                );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_42B8N318");
                e.Successed = false;
                return;
            }

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["S8CURRCD"].ToString() != "")
                {
                    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["S8SBFORAMT"].ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_AC_3CK2B843");
                        e.Successed = false;
                        return;
                    }

                    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["S8CXCHAN"].ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_AC_3CK2B845");
                        e.Successed = false;
                        return;
                    }
                }

                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["S8SBFORAMT"].ToString())) != 0)
                {
                    if (ds.Tables[0].Rows[i]["S8CURRCD"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_AC_3CK2B846");
                        e.Successed = false;
                        return;
                    }

                    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["S8CXCHAN"].ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_AC_3CK2B845");
                        e.Successed = false;
                        return;
                    }
                }

                if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["S8CXCHAN"].ToString())) != 0)
                {
                    if (ds.Tables[0].Rows[i]["S8CURRCD"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_AC_3CK2B846");
                        e.Successed = false;
                        return;
                    }

                    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["S8SBFORAMT"].ToString())) == 0)
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

            ds.Tables.Add(this.FPS91_TY_S_AC_3CNBW859.GetDataSourceInclude(TSpread.TActionType.Remove, "S8JPNO", "S8SHIPDT"));

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
                this.TXT01_S8YEAR.GetValue().ToString(),
                this.CBO01_S8BRANCH.GetValue().ToString(),
                getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 1),
                getCONFGB(this.CBO01_S8CONFGB.GetValue().ToString(), 2)
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

        #region Description : 출력 데이터셋 변환
        protected DataTable UP_ConvertHap(DataTable dt)
        {
            DataTable Retdt = dt;

            int iBLANK = 0;
            int i = 0;
                if (dt != null && dt.Rows.Count > 12)
                {
                    DataRow row;
                    iBLANK = 17 - ((dt.Rows.Count - 12) % 18);
                    if (iBLANK == 17)
                    {
                        iBLANK = 0;
                    }
                    for (i = 1; i <= iBLANK; i++)
                    {
                        row = Retdt.NewRow();

                        row["NUMBER"] = DBNull.Value;
                        row["S8JPNO"] = "";
                        row["S8DATE"] = "";
                        row["S8SHIPDT"] = DBNull.Value;
                        row["S8DOCNUM"] = DBNull.Value;
                        row["S8ISSUER"] = DBNull.Value;
                        row["S8CURRCD"] = DBNull.Value;
                        row["FRDESC"] = DBNull.Value;
                        row["S8CXCHAN"] = DBNull.Value;
                        row["S8SBFORAMT"] = DBNull.Value;
                        row["S8SBWONAMT"] = DBNull.Value;

                        Retdt.Rows.Add(row);
                    }
                }
                else if (dt.Rows.Count < 12)
                {
                    DataRow row;

                    iBLANK = 11 - (dt.Rows.Count);

                    for (i = 1; i <= iBLANK; i++)
                    {
                        row = Retdt.NewRow();

                        row["NUMBER"] = DBNull.Value;
                        row["S8JPNO"] = "";
                        row["S8DATE"] = "";
                        row["S8SHIPDT"] = DBNull.Value;
                        row["S8DOCNUM"] = DBNull.Value;
                        row["S8ISSUER"] = DBNull.Value;
                        row["S8CURRCD"] = DBNull.Value;
                        row["FRDESC"] = DBNull.Value;
                        row["S8CXCHAN"] = DBNull.Value;
                        row["S8SBFORAMT"] = DBNull.Value;
                        row["S8SBWONAMT"] = DBNull.Value;

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
                this.TXT01_S8YEAR.SetValue(TYCookie.Year);
                this.CBO01_S8BRANCH.SetValue(TYCookie.Branch);
                this.CBO01_S8CONFGB.SetValue(TYCookie.Confgb);
            }
            else
            {
                this.TXT01_S8YEAR.SetValue(DateTime.Now.ToString("yyyyMMdd").Substring(0, 4));
            }
        }
        #endregion

        #region Description : 쿠키 저장
        private void UP_Cookie_Save()
        {
            TYCookie.Save(this.TXT01_S8YEAR.GetValue().ToString(), this.CBO01_S8BRANCH.GetValue().ToString(), this.CBO01_S8CONFGB.GetValue().ToString());
        }
        #endregion
    }
}