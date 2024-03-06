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
    /// 원천징수영수증 출력 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.10.22 17:42
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_4ADEW171 : 소득자료 출력(이자배당소득 추가입력자료)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  PRT : 출력
    ///  WABRANCH : 지점구분
    ///  TP010CHK1 : 기타소득
    ///  TP010CHK2 : 사업소득
    ///  TP010CHK3 : 배당소득
    ///  TP010CHK4 : 일용소득
    ///  TP010CHK5 : 사업소득
    ///  TP010CHK6 : 기타소득
    ///  TP010CHK7 : 일용소득
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACTP010P : TYBase
    {
        public TYACTP010P()
        {
            InitializeComponent();
        }

        private void TYACTP010P_Load(object sender, System.EventArgs e)
        {
        }

        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable rtnDt = new DataTable();
            DataTable rtnDt2 = new DataTable();

            string WBRANCH03  = string.Empty;
            string WREYYMM03  = string.Empty;
            string WGIDATE03  = string.Empty;
            string WINCOME03  = string.Empty;
            string WRDEPART03 = string.Empty;
            string WRJUMIN03  = string.Empty;
            string WRGUNMU03  = string.Empty;

            #region Description : 월 단위 출력
            if (DTP01_GSTYYMM.GetValue().ToString() == DTP01_GEDYYMM.GetValue().ToString())
            {
                string WINCOME = string.Empty;

                if (CKB01_TP010CHK1.Checked == true)    //기타
                {
                    if (WINCOME.Length > 0)
                    {
                        WINCOME = WINCOME + ",";
                    }
                    WINCOME = WINCOME + "A42";
                }
                if (CKB01_TP010CHK2.Checked == true)    //사업
                {
                    if (WINCOME.Length > 0)
                    {
                        WINCOME = WINCOME + ",";
                    }
                    WINCOME = WINCOME + "A25";
                }
                if (CKB01_TP010CHK3.Checked == true)    //배당
                {
                    if (WINCOME.Length > 0)
                    {
                        WINCOME = WINCOME + ",";
                    }
                    WINCOME = WINCOME + "A60";
                }
                if (CKB01_TP010CHK4.Checked == true)    //일용
                {
                    if (WINCOME.Length > 0)
                    {
                        WINCOME = WINCOME + ",";
                    }
                    WINCOME = WINCOME + "A03";
                }
                if (CKB01_TP010CHK8.Checked == true)    //이자
                {
                    if (WINCOME.Length > 0)
                    {
                        WINCOME = WINCOME + ",";
                    }
                    WINCOME = WINCOME + "A50";
                }

                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_4A1HV117",
                    TYUserInfo.SecureKey, "Y",
                    "1",
                    this.CBO01_WRGUNMU.GetValue().ToString(),
                    this.DTP01_GSTYYMM.GetValue().ToString(),
                    "",
                    WINCOME,
                    "",
                    TYUserInfo.SecureKey, "Y",
                    ""
                    );
                dt = this.DbConnector.ExecuteDataTable();

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
                                    (new TYERGB001P(rpt, UP_ChangeDatatable(rtnDt, 22))).ShowDialog();

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
                        (new TYERGB001P(rpt, UP_ChangeDatatable(rtnDt, 22))).ShowDialog();
                    }
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2422N250"); 
                }
            }
            #endregion

            #region Description : 분기,년 단위 출력
            else
            {
                string WINCOME = string.Empty;

                if (CKB01_TP010CHK5.Checked == true)
                {
                    if (WINCOME.Length > 0)
                    {
                        WINCOME = WINCOME + ",";
                    }
                    WINCOME = WINCOME + "A25";  //사업
                }
                if (CKB01_TP010CHK6.Checked == true)
                {
                    if (WINCOME.Length > 0)
                    {
                        WINCOME = WINCOME + ",";
                    }
                    WINCOME = WINCOME + "A42";  //기타
                }
                if (CKB01_TP010CHK7.Checked == true)
                {
                    if (WINCOME.Length > 0)
                    {
                        WINCOME = WINCOME + ",";
                    }
                    WINCOME = WINCOME + "A03";  //일용
                }

                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_AC_4ANAM239",
                    "1",
                    this.CBO01_WRGUNMU.GetValue().ToString(),
                    this.DTP01_GSTYYMM.GetValue().ToString(),
                    this.DTP01_GEDYYMM.GetValue().ToString(),
                    WINCOME
                    );
                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["WINCOME"].ToString() == "A25")
                        {
                            this.DbConnector.Attach
                                (
                                "TY_P_AC_4ANBK240",
                                TYUserInfo.SecureKey, "Y",
                                TYUserInfo.SecureKey, "Y",
                                TYUserInfo.SecureKey, "Y",
                                dt.Rows[i]["WBRANCH"].ToString(),
                                dt.Rows[i]["WRGUNMU"].ToString(),
                                this.DTP01_GSTYYMM.GetValue().ToString(),
                                this.DTP01_GEDYYMM.GetValue().ToString(),
                                dt.Rows[i]["WINCOME"].ToString()
                                );
                            rtnDt = this.DbConnector.ExecuteDataTable();

                            this.DbConnector.Attach
                                (
                                "TY_P_AC_4ATHJ273",
                                dt.Rows[i]["WBRANCH"].ToString(),
                                dt.Rows[i]["WRGUNMU"].ToString(),
                                this.DTP01_GSTYYMM.GetValue().ToString(),
                                this.DTP01_GEDYYMM.GetValue().ToString(),
                                dt.Rows[i]["WINCOME"].ToString()
                                );
                            rtnDt2 = this.DbConnector.ExecuteDataTable();

                            SectionReport rpt = new TYACTP004R5(rtnDt2);
                            (new TYERGB001P(rpt, UP_ChangeDatatableYY(rtnDt,1))).ShowDialog();
                        }
                        if (dt.Rows[i]["WINCOME"].ToString() == "A42")
                        {
                            this.DbConnector.Attach
                                (
                                "TY_P_AC_4ANBK240",
                                TYUserInfo.SecureKey, "Y",
                                TYUserInfo.SecureKey, "Y",
                                TYUserInfo.SecureKey, "Y",
                                dt.Rows[i]["WBRANCH"].ToString(),
                                dt.Rows[i]["WRGUNMU"].ToString(),
                                this.DTP01_GSTYYMM.GetValue().ToString(),
                                this.DTP01_GEDYYMM.GetValue().ToString(),
                                dt.Rows[i]["WINCOME"].ToString()
                                );
                            rtnDt = this.DbConnector.ExecuteDataTable();

                            this.DbConnector.Attach
                                (
                                "TY_P_AC_4ATHJ273",
                                dt.Rows[i]["WBRANCH"].ToString(),
                                dt.Rows[i]["WRGUNMU"].ToString(),
                                this.DTP01_GSTYYMM.GetValue().ToString(),
                                this.DTP01_GEDYYMM.GetValue().ToString(),
                                dt.Rows[i]["WINCOME"].ToString()
                                );
                            rtnDt2 = this.DbConnector.ExecuteDataTable();

                            SectionReport rpt = new TYACTP004R7(rtnDt2);
                            (new TYERGB001P(rpt, UP_ChangeDatatableYY(rtnDt, 2))).ShowDialog();
                        }
                        if (dt.Rows[i]["WINCOME"].ToString() == "A03")
                        {
                            this.DbConnector.Attach
                                (
                                "TY_P_AC_4B3FJ296",
                                 TYUserInfo.SecureKey, "Y",
                                  TYUserInfo.SecureKey, "Y",
                                  TYUserInfo.SecureKey, "Y",
                                dt.Rows[i]["WBRANCH"].ToString(),
                                dt.Rows[i]["WRGUNMU"].ToString(),
                                this.DTP01_GSTYYMM.GetValue().ToString(),
                                this.DTP01_GEDYYMM.GetValue().ToString(),
                                dt.Rows[i]["WINCOME"].ToString()
                                );
                            rtnDt = this.DbConnector.ExecuteDataTable();

                            this.DbConnector.Attach
                                (
                                "TY_P_AC_4ATHJ273",
                                dt.Rows[i]["WBRANCH"].ToString(),
                                dt.Rows[i]["WRGUNMU"].ToString(),
                                this.DTP01_GSTYYMM.GetValue().ToString(),
                                this.DTP01_GEDYYMM.GetValue().ToString(),
                                dt.Rows[i]["WINCOME"].ToString()
                                );
                            rtnDt2 = this.DbConnector.ExecuteDataTable();

                            SectionReport rpt = new TYACTP004R9(rtnDt2);
                            (new TYERGB001P(rpt, UP_ChangeDatatable(rtnDt))).ShowDialog();
                        }
                    }
                }
            }
            #endregion


        }

        #region Description : 탭 선택 이벤트
        private void tabControl1_Selected(object sender, System.Windows.Forms.TabControlEventArgs e)
        {
            CKB01_TP010CHK1.Checked = false;
            CKB01_TP010CHK2.Checked = false;
            CKB01_TP010CHK3.Checked = false;
            CKB01_TP010CHK4.Checked = false;
            CKB01_TP010CHK5.Checked = false;
            CKB01_TP010CHK6.Checked = false;
            CKB01_TP010CHK7.Checked = false;
        }
        #endregion

        #region Description : DataTable 변환(월)
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
                    row["WCOUNTRY"] = DBNull.Value;
                    row["WCOUNTRYNM"] = DBNull.Value;
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
        #endregion

        #region Description : DataTable 변환(년)
        private DataTable UP_ChangeDatatableYY(DataTable dt, int iGubn)
        {
            DataTable Retdt = dt;

            if (dt != null)
            {
                int iBLANK = 0;

                if (iGubn == 1)
                {
                    if (dt.Rows.Count > 9)
                    {
                        iBLANK = 37 - ((dt.Rows.Count - 9) % 37);
                        if (iBLANK == 37)
                        {
                            iBLANK = 0;
                        }
                    }
                    else
                    {
                        iBLANK = 9 - dt.Rows.Count;
                    }
                }
                if (iGubn == 2)
                {
                    if (dt.Rows.Count > 15)
                    {
                        iBLANK = 45 - ((dt.Rows.Count - 15) % 45);
                        if (iBLANK == 45)
                        {
                            iBLANK = 0;
                        }
                    }
                    else
                    {
                        iBLANK = 15 - dt.Rows.Count;
                    }
                }
                DataRow row;

                for (int i = 1; i <= iBLANK; i++)
                {
                    row = Retdt.NewRow();

                    row["NUMBER"] = DBNull.Value;
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
                    row["WREYY"] = DBNull.Value;
                    row["WYEAR"] = DBNull.Value;
                    row["WRJUMIN"] = DBNull.Value;
                    row["WTRADNAME"] = DBNull.Value;
                    row["WNATIVEGB"] = DBNull.Value;
                    row["WNATIVEGBNM"] = DBNull.Value;
                    row["WHOLDYUL"] = DBNull.Value;
                    row["WTOTALAMT"] = DBNull.Value;
                    row["WCOSTAMT"] = DBNull.Value;
                    row["WINCOMAMT"] = DBNull.Value;
                    row["WTAXINCOM"] = DBNull.Value;
                    row["WLOCALTAX"] = DBNull.Value;
                    row["WLSUMTAX"] = DBNull.Value;
                    row["AMT"] = DBNull.Value;
                    row["COUNT"] = DBNull.Value;
                    row["WBUSIGBN"] = DBNull.Value;

                    Retdt.Rows.Add(row);
                }
            }
            return Retdt;
        }
        #endregion

        #region Description : DataTable 변환(분기)
        private DataTable UP_ChangeDatatable(DataTable dt)
        {
            DataTable Retdt = dt;

            if (dt != null)
            {
                int iBLANK = 0;

                if (dt.Rows.Count > 16)
                {
                    iBLANK = 40 - ((dt.Rows.Count - 16) % 40);
                    if (iBLANK == 40)
                    {
                        iBLANK = 0;
                    }
                }
                else
                {
                    iBLANK = 16 - dt.Rows.Count;
                }

                DataRow row;

                for (int i = 1; i <= iBLANK; i++)
                {
                    row = Retdt.NewRow();

                    row["NUMBER"] = DBNull.Value;
                    row["ASMSANGHO"] = DBNull.Value;
                    row["ASMNAMENM"] = DBNull.Value;
                    row["ASMSAUPNO"] = DBNull.Value;
                    row["ASMCORPNO"] = DBNull.Value;
                    row["ASMVNADDRS"] = DBNull.Value;
                    row["ASMTELNUM"] = DBNull.Value;
                    row["WBRANCHNM"] = DBNull.Value;
                    row["WREYY"] = DBNull.Value;
                    row["WYEAR"] = DBNull.Value;
                    row["WMONTH"] = DBNull.Value;
                    row["WRJUMIN"] = DBNull.Value;
                    row["WTRADNAME"] = DBNull.Value;
                    row["WTRADTEL"] = DBNull.Value;
                    row["WNATIVEGB"] = DBNull.Value;
                    row["WNATIVEGBNM"] = DBNull.Value;
                    row["WHOLDYUL"] = DBNull.Value;
                    row["WDAYWORK"] = DBNull.Value;
                    row["WTOTALAMT"] = DBNull.Value;
                    row["WCOSTAMT"] = DBNull.Value;
                    row["WINCOMAMT"] = DBNull.Value;
                    row["WTAXINCOM"] = DBNull.Value;
                    row["WLOCALTAX"] = DBNull.Value;
                    row["WLSUMTAX"] = DBNull.Value;
                    row["AMT"] = DBNull.Value;
                    row["COUNT"] = DBNull.Value;
                    row["WBUSIGBN"] = DBNull.Value;

                    Retdt.Rows.Add(row);
                }
            }
            return Retdt;
        }
        #endregion
    }
}
