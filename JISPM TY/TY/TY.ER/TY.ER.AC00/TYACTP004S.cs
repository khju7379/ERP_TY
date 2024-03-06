using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.GB00;

namespace TY.ER.AC00
{
    /// <summary>
    /// 소득자료 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.10.02 15:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_4A2AM119 : 소득자료 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  PRT : 출력
    ///  WABRANCH : 지점구분
    ///  WREYYMM : 귀속년월
    /// </summary>
    public partial class TYACTP004S : TYBase
    {
        #region Description : 페이지 로드
        public TYACTP004S()
        {
            InitializeComponent();
        }
        
        private void TYACTP004S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(BTN61_PRT_ProcessCheck);

            this.DTP01_WREYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_4A1HV117",
                TYUserInfo.SecureKey,
                "Y",
                "1",
                this.CBO01_WRGUNMU.GetValue().ToString(),
                this.DTP01_WREYYMM.GetValue().ToString(),
                "",
                "A03,A25,A42,A50,A60",
                "",
                TYUserInfo.SecureKey, "Y",
                ""
                );

            this.FPS91_TY_S_AC_4A2AM119.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACTP004I("","", "","","","","")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;
            DataTable rtnDt = new DataTable();
            DataTable rtnDt2 = new DataTable();

            string WBRANCH03  = string.Empty;
            string WREYYMM03  = string.Empty;
            string WGIDATE03  = string.Empty;
            string WINCOME03  = string.Empty;
            string WRDEPART03 = string.Empty;
            string WRJUMIN03  = string.Empty;
            string WRGUNMU03  = string.Empty;

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["WINCOME"].ToString() == "A03")
                    {
                        if (WBRANCH03 == "")
                        {
                            WBRANCH03  = dt.Rows[i]["WBRANCH"].ToString();
                            WREYYMM03  = dt.Rows[i]["WREYYMM"].ToString();
                            WGIDATE03  = dt.Rows[i]["WGIDATE"].ToString();
                            WINCOME03  = dt.Rows[i]["WINCOME"].ToString();
                            WRDEPART03 = dt.Rows[i]["WRDEPART"].ToString();
                            WRJUMIN03  = dt.Rows[i]["WRJUMIN"].ToString();
                            WRGUNMU03  = dt.Rows[i]["WRGUNMU"].ToString();
                        }
                        else
                        {
                            if (WBRANCH03 == dt.Rows[i]["WBRANCH"].ToString() && WREYYMM03 == dt.Rows[i]["WREYYMM"].ToString() && WGIDATE03 == dt.Rows[i]["WGIDATE"].ToString() &&
                                WINCOME03 == dt.Rows[i]["WINCOME"].ToString() && WRJUMIN03 == dt.Rows[i]["WRJUMIN"].ToString() && WRGUNMU03 == dt.Rows[i]["WRGUNMU"].ToString())
                            {
                                WRDEPART03 += "," + dt.Rows[i]["WRDEPART"].ToString();
                            }
                            else
                            {
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach
                                    (
                                    "TY_P_AC_4A7AT156",
                                    TYUserInfo.SecureKey, "Y",
                                    TYUserInfo.SecureKey, "Y",
                                    TYUserInfo.SecureKey, "Y",
                                    WBRANCH03,
                                    WREYYMM03,
                                    WGIDATE03,
                                    WINCOME03,
                                    WRDEPART03,
                                    WRJUMIN03,
                                    WRGUNMU03
                                    );

                                rtnDt = this.DbConnector.ExecuteDataTable();

                                SectionReport rpt = new TYACTP004R1();
                                (new TYERGB001P(rpt, UP_ChangeDatatable(rtnDt, 21))).ShowDialog();

                                WBRANCH03  = dt.Rows[i]["WBRANCH"].ToString();
                                WREYYMM03  = dt.Rows[i]["WREYYMM"].ToString();
                                WGIDATE03  = dt.Rows[i]["WGIDATE"].ToString();
                                WINCOME03  = dt.Rows[i]["WINCOME"].ToString();
                                WRDEPART03 = dt.Rows[i]["WRDEPART"].ToString();
                                WRJUMIN03  = dt.Rows[i]["WRJUMIN"].ToString();
                                WRGUNMU03  = dt.Rows[i]["WRGUNMU"].ToString();
                            }
                        }
                    }
                    if (dt.Rows[i]["WINCOME"].ToString() == "A25")
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_4A7AT156",
                            TYUserInfo.SecureKey, "Y",
                            TYUserInfo.SecureKey, "Y",
                            TYUserInfo.SecureKey, "Y",
                            dt.Rows[i]["WBRANCH"].ToString(),
                            dt.Rows[i]["WREYYMM"].ToString(),
                            dt.Rows[i]["WGIDATE"].ToString(),
                            dt.Rows[i]["WINCOME"].ToString(),
                            dt.Rows[i]["WRDEPART"].ToString(),
                            dt.Rows[i]["WRJUMIN"].ToString(),
                            dt.Rows[i]["WRGUNMU"].ToString()
                            );

                        rtnDt = this.DbConnector.ExecuteDataTable();

                        SectionReport rpt = new TYACTP004R2();
                        (new TYERGB001P(rpt, UP_ChangeDatatable(rtnDt, 14))).ShowDialog();
                    }
                    if (dt.Rows[i]["WINCOME"].ToString() == "A42")
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_4A7AT156",
                            TYUserInfo.SecureKey, "Y",
                            TYUserInfo.SecureKey, "Y",
                            TYUserInfo.SecureKey, "Y",
                            dt.Rows[i]["WBRANCH"].ToString(),
                            dt.Rows[i]["WREYYMM"].ToString(),
                            dt.Rows[i]["WGIDATE"].ToString(),
                            dt.Rows[i]["WINCOME"].ToString(),
                            dt.Rows[i]["WRDEPART"].ToString(),
                            dt.Rows[i]["WRJUMIN"].ToString(),
                            dt.Rows[i]["WRGUNMU"].ToString()
                            );

                        rtnDt = this.DbConnector.ExecuteDataTable();

                        SectionReport rpt = new TYACTP004R3();
                        (new TYERGB001P(rpt, UP_ChangeDatatable(rtnDt, 10))).ShowDialog();
                    }
                    if (dt.Rows[i]["WINCOME"].ToString() == "A50" || dt.Rows[i]["WINCOME"].ToString() == "A60")
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_AC_4A7AT156",
                            TYUserInfo.SecureKey, "Y",
                            TYUserInfo.SecureKey, "Y",
                            TYUserInfo.SecureKey, "Y",
                            dt.Rows[i]["WBRANCH"].ToString(),
                            dt.Rows[i]["WREYYMM"].ToString(),
                            dt.Rows[i]["WGIDATE"].ToString(),
                            dt.Rows[i]["WINCOME"].ToString(),
                            dt.Rows[i]["WRDEPART"].ToString(),
                            dt.Rows[i]["WRJUMIN"].ToString(),
                            dt.Rows[i]["WRGUNMU"].ToString()
                            );

                        rtnDt = this.DbConnector.ExecuteDataTable();

                        SectionReport rpt = new TYACTP004R4();
                        (new TYERGB001P(rpt, UP_ChangeDatatable(rtnDt, 7))).ShowDialog();
                    }
                }
                if (WRGUNMU03 != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_AC_4A7AT156",
                        TYUserInfo.SecureKey, "Y",
                        TYUserInfo.SecureKey, "Y",
                        TYUserInfo.SecureKey, "Y",
                        WBRANCH03,
                        WREYYMM03,
                        WGIDATE03,
                        WINCOME03,
                        WRDEPART03,
                        WRJUMIN03,
                        WRGUNMU03
                        );

                    rtnDt = this.DbConnector.ExecuteDataTable();

                    SectionReport rpt = new TYACTP004R1();
                    (new TYERGB001P(rpt, UP_ChangeDatatable(rtnDt,21))).ShowDialog();
                }
            }
            else
            {
            }
        }
        #endregion


        private void FPS91_TY_S_AC_4A2AM119_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYACTP004I(this.FPS91_TY_S_AC_4A2AM119.GetValue("WBRANCH").ToString(),
                                this.FPS91_TY_S_AC_4A2AM119.GetValue("WREYYMM").ToString(),
                                this.FPS91_TY_S_AC_4A2AM119.GetValue("WGIDATE").ToString(),
                                this.FPS91_TY_S_AC_4A2AM119.GetValue("WINCOME").ToString(),
                                this.FPS91_TY_S_AC_4A2AM119.GetValue("WRDEPART").ToString(),
                                this.FPS91_TY_S_AC_4A2AM119.GetValue("WRJUMIN").ToString(),
                                this.FPS91_TY_S_AC_4A2AM119.GetValue("WRGUNMU").ToString()
                                )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }

        #region Description : 출력 ProcessCheck 이벤트
        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_4A2AM119.GetDataSourceInclude(TSpread.TActionType.Select, "WBRANCH", "WRGUNMU", "WREYYMM", "WGIDATE", "WINCOME", "WRDEPART", "WRJUMIN");

            e.ArgData = dt;
        }
        #endregion

        private DataTable UP_ChangeDatatable(DataTable dt, int iRows)
        {
            DataTable Retdt = dt;

            if (dt != null)
            {
                int iBLANK = iRows - dt.Rows.Count;

                DataRow row;

                for (int i = 1; i <= iBLANK; i++)
                {
                    row = Retdt.NewRow();

                    row["ASMSANGHO"] = DBNull.Value;
                    row["ASMNAMENM"] = DBNull.Value;
                    row["ASMSAUPNO"] = DBNull.Value;
                    row["ASMCORPNO"] = DBNull.Value;
                    row["ASMVNADDRS"] = DBNull.Value;
                    row["ASMTELNUM"] = DBNull.Value;
                    row["WBRANCH"] = DBNull.Value;
                    row["WBRANCHNM"] = DBNull.Value;
                    row["WRGUNMU"] = DBNull.Value;
                    row["WRGUNMUNM"] = DBNull.Value;
                    row["WREYYMM"] = DBNull.Value;
                    row["WREYY"] = DBNull.Value;
                    row["WREMM"] = DBNull.Value;
                    row["WGIDATE"] = DBNull.Value;
                    row["WYEAR"] = DBNull.Value;
                    row["WMONTH"] = DBNull.Value;
                    row["WDAY"] = DBNull.Value;
                    row["WRJUMIN"] = DBNull.Value;
                    row["WTRADNAME"] = DBNull.Value;
                    row["WTRADTEL"] = DBNull.Value;
                    row["WNATIVEGB"] = DBNull.Value;
                    row["WADDRESS"] = DBNull.Value;
                    row["WHOLDYUL"] = DBNull.Value;
                    row["WDAYWORK"] = DBNull.Value;
                    row["WTOTALAMT"] = DBNull.Value;
                    row["WCOSTAMT"] = DBNull.Value;
                    row["WINCOMAMT"] = DBNull.Value;
                    row["WTAXINCOM"] = DBNull.Value;
                    row["WLOCALTAX"] = DBNull.Value;
                    row["WLSUMTAX"] = DBNull.Value;
                    row["AMT"] = DBNull.Value;
                    row["WBUSIGBN"] = DBNull.Value;

                    Retdt.Rows.Add(row);
                }
            }
            return Retdt;
        }
    }
}
