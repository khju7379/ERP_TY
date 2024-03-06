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
    /// 일별자금집계표(계좌별) 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.11.12 09:06
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2B95W237 : 일별자금집계표(계좌별) 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2BC98248 : 일별자금집계표(계좌별)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  GSTDATE : 시작일자
    ///  GSOVERDRAMT : 당좌차월한도액
    /// </summary>
    public partial class TYACMF006S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACMF006S()
        {
            InitializeComponent();
        }

        private void TYACMF006S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(this.DTP01_GSTDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_2BC98248.Initialize(); 

            this.DbConnector.CommandClear();

            if (CBO01_INQOPTION.GetValue().ToString() == "1")
            {

                this.DbConnector.Attach("TY_P_AC_2BDBT275", this.DTP01_GSTDATE.GetString(), this.TXT01_GSOVERDRAMT.GetValue());
                DataSet ds = this.DbConnector.ExecuteDataSet();

                this.FPS91_TY_S_AC_2BC98248.SetValue(UP_CdacSuTotal_ds(UP_QueryDataSetReport(ds, CBO01_INQOPTION.GetValue().ToString())));

                if (this.FPS91_TY_S_AC_2BC98248.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2BC98248, "A1NMAC", "[소     계]", SumRowType.SubTotal);
                }
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_2B95W237", this.DTP01_GSTDATE.GetString(), this.TXT01_GSOVERDRAMT.GetValue());
                DataSet ds = this.DbConnector.ExecuteDataSet();

                this.FPS91_TY_S_AC_2BC98248.SetValue(UP_SuTotal_ds(UP_QueryDataSetReport(ds, CBO01_INQOPTION.GetValue().ToString())));

                if (this.FPS91_TY_S_AC_2BC98248.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2BC98248, "A1NMAC", "[소     계]", SumRowType.SubTotal);
                }
            }
        }
        #endregion

        #region Description : 데이터 셋 변경
        private DataSet UP_QueryDataSetReport(DataSet ds, string sGubn)
        {
            double dWONJUNAMT = 0;
            double dJUNAMT = 0;
            double dJANAMT = 0;
            string sNEWA1NMAC = string.Empty;
            string sOLDA1NMAC = string.Empty;
            string sNEWCDDESC2 = string.Empty;
            string sOLDCDDESC2 = string.Empty;

            DataSet retDs = new DataSet();

            DataTable table = new DataTable();

            table.Columns.Add("A1CDAC", typeof(System.String));
            table.Columns.Add("A1NMAC", typeof(System.String));
            table.Columns.Add("CDDESC2", typeof(System.String));
            table.Columns.Add("GAEJUA", typeof(System.String));
            table.Columns.Add("JUNAMT", typeof(System.Double));
            table.Columns.Add("WONIPAMT", typeof(System.Double));
            table.Columns.Add("IPAMT", typeof(System.Double));
            table.Columns.Add("WONCHAMT", typeof(System.Double));
            table.Columns.Add("CHAMT", typeof(System.Double));
            table.Columns.Add("WONJANAMT", typeof(System.Double));
            table.Columns.Add("JANAMT", typeof(System.Double));
            table.Columns.Add("GUBN", typeof(System.String));
            table.Columns.Add("WONJUNAMT", typeof(System.Double));
            

            retDs.Tables.Add(table);   
            
            try
            {
                // 우선 테이타 셋의 테이블의 처음부터 끝까지 도는 루프 코딩
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sNEWA1NMAC = ds.Tables[0].Rows[i]["A1NMAC"].ToString();
                    sNEWCDDESC2 = ds.Tables[0].Rows[i]["CDDESC2"].ToString();

                    if (i == 0)
                    {
                        sOLDA1NMAC = sNEWA1NMAC;
                        sOLDCDDESC2 = sNEWCDDESC2;
                    }

                    if (sOLDA1NMAC == sNEWA1NMAC)
                    {
                        if (i != 0)
                        {
                            sNEWA1NMAC = "";
                        }
                    }
                    else
                    {
                        sOLDA1NMAC = sNEWA1NMAC;
                    }

                    if (sOLDCDDESC2 == sNEWCDDESC2)
                    {
                        if (i != 0)
                        {
                            sNEWCDDESC2 = "";
                        }
                    }
                    else
                    {
                        sOLDCDDESC2 = sNEWCDDESC2;
                    }

                    DataRow row = retDs.Tables[0].NewRow();

                    //계정코드
                    row["A1CDAC"] = ds.Tables[0].Rows[i]["A1CDAC"].ToString();

                    // 계정명
                    row["A1NMAC"] = sNEWA1NMAC.ToString();

                    // 은행명
                    row["CDDESC2"] = sNEWCDDESC2.ToString();

                    if (sGubn.ToString() != "1")
                    {
                        // 계좌번호
                        row["GAEJUA"] = ds.Tables[0].Rows[i]["GAEJUA"].ToString();
                    }
                    else
                    {
                        row["GAEJUA"] = ds.Tables[0].Rows[i]["CODE"].ToString() + " " + ds.Tables[0].Rows[i]["IHGUBN"].ToString().Trim();
                    }

                    // 전일 원화잔액
                    if (ds.Tables[0].Rows[i]["A1TAG01"].ToString() == "D" &&
                       ds.Tables[0].Rows[i]["GBN"].ToString() != "5" &&
                       ds.Tables[0].Rows[i]["GBN"].ToString() != "6")
                    {
                        dWONJUNAMT = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AMOUNT1"].ToString())) +
                                     double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["JDRAMT"].ToString())) -
                                     double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["JCRAMT"].ToString()));
                    }
                    else if (ds.Tables[0].Rows[i]["A1TAG01"].ToString() == "C" &&
                            ds.Tables[0].Rows[i]["GBN"].ToString() != "5" &&
                            ds.Tables[0].Rows[i]["GBN"].ToString() != "6")
                    {
                        dWONJUNAMT = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AMOUNT1"].ToString())) +
                                     double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["JCRAMT"].ToString())) -
                                     double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["JDRAMT"].ToString()));
                    }
                    else
                    {
                        dWONJUNAMT = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AMOUNT1"].ToString()));
                    }

                        // 전일 외화잔액
                        if (ds.Tables[0].Rows[i]["GBN"].ToString() == "5" ||
                           ds.Tables[0].Rows[i]["GBN"].ToString() == "6")
                        {
                            dJUNAMT = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AMOUNT1"].ToString()));
                        }
                        else
                        {
                            dJUNAMT = 0;
                        }

                    // 전일 원화잔액
                    row["WONJUNAMT"] = dWONJUNAMT - dJUNAMT;

                    // 전일 외화잔액
                    row["JUNAMT"] = dJUNAMT;

                    // 원화 입금액
                    row["WONIPAMT"] = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["IN_AMT"].ToString()));

                    // 외화 입금액
                    if (ds.Tables[0].Rows[i]["GBN"].ToString() == "5" ||
                       ds.Tables[0].Rows[i]["GBN"].ToString() == "6")
                    {
                        row["IPAMT"] = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["IN_AMT"].ToString()));
                    }
                    else
                    {
                        row["IPAMT"] = 0;
                    }

                    // 원화 출금액
                    row["WONCHAMT"] = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["OUT_AMT"].ToString()));

                 
                        // 외화 출금액
                        if (ds.Tables[0].Rows[i]["GBN"].ToString() == "5" ||
                           ds.Tables[0].Rows[i]["GBN"].ToString() == "6")
                        {
                            row["CHAMT"] = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["OUT_AMT"].ToString()));
                        }
                        else
                        {
                            row["CHAMT"] = 0;
                        }


                   
                        // 외화 잔액
                        if (ds.Tables[0].Rows[i]["GBN"].ToString() == "5" ||
                           ds.Tables[0].Rows[i]["GBN"].ToString() == "6")
                        {
                            dJANAMT = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AMOUNT2"].ToString()));
                        }
                        else
                        {
                            dJANAMT = 0;
                        }

                    // 원화 잔액
                    if (ds.Tables[0].Rows[i]["A1TAG01"].ToString() == "D" &&
                       ds.Tables[0].Rows[i]["GBN"].ToString() != "5" &&
                       ds.Tables[0].Rows[i]["GBN"].ToString() != "6")
                    {
                        row["WONJANAMT"] = dWONJUNAMT + double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["IN_AMT"].ToString())) -
                                           double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["OUT_AMT"].ToString()));
                    }
                    else if (ds.Tables[0].Rows[i]["A1TAG01"].ToString() == "C" &&
                            ds.Tables[0].Rows[i]["GBN"].ToString() != "5" &&
                            ds.Tables[0].Rows[i]["GBN"].ToString() != "6")
                    {
                        row["WONJANAMT"] = dWONJUNAMT + double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["OUT_AMT"].ToString())) -
                                           double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["IN_AMT"].ToString())) - dJANAMT;
                    }
                    else
                    {
                        row["WONJANAMT"] = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AMOUNT2"].ToString())) - dJANAMT;
                    }

                    // 외화 잔액
                    row["JANAMT"] = dJANAMT;

                    if (sGubn.ToString() != "1")
                    {
                        row["GUBN"] = ds.Tables[0].Rows[i]["GUBN"].ToString();
                    }
                    else
                    {
                        row["GUBN"] = "";
                    }

                    retDs.Tables[0].Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                string sMessage = string.Empty;
                sMessage = ex.ToString();
            }

            return retDs;
        }
        #endregion

        #region Description : 소계 내기
        private DataTable UP_SuTotal_ds(DataSet ds)
        {
            string sNEWA1NMAC = string.Empty;
            int i = 0;

            // 합계를 보여주기 위한 빈 로우 하나 생성
            DataTable table = new DataTable();
            table = ds.Tables[0];

            DataRow row;
            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["GUBN"].ToString() != table.Rows[i]["GUBN"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    table.Rows[i]["A1NMAC"] = "[소     계]";

                    sNEWA1NMAC = "GUBN = '" + table.Rows[i - 1]["GUBN"].ToString() + "' ";

                    // 전일 원화잔액
                    table.Rows[i]["WONJUNAMT"] = table.Compute("Sum(WONJUNAMT)", sNEWA1NMAC).ToString();

                    // 전일 외화잔액
                    table.Rows[i]["JUNAMT"] = table.Compute("Sum(JUNAMT)", sNEWA1NMAC).ToString();

                    // 원화입금
                    table.Rows[i]["WONIPAMT"] = table.Compute("Sum(WONIPAMT)", sNEWA1NMAC).ToString();

                    // 외화입금
                    table.Rows[i]["IPAMT"] = table.Compute("Sum(IPAMT)", sNEWA1NMAC).ToString();

                    // 원화출금
                    table.Rows[i]["WONCHAMT"] = table.Compute("Sum(WONCHAMT)", sNEWA1NMAC).ToString();

                    // 외화출금
                    table.Rows[i]["CHAMT"] = table.Compute("Sum(CHAMT)", sNEWA1NMAC).ToString();

                    // 원화잔액
                    table.Rows[i]["WONJANAMT"] = table.Compute("Sum(WONJANAMT)", sNEWA1NMAC).ToString();

                    // 외화잔액
                    table.Rows[i]["JANAMT"] = table.Compute("Sum(JANAMT)", sNEWA1NMAC).ToString();

                    nNum = nNum + 1;

                    i = i + 1;
                }
            }

            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            table.Rows[i]["A1NMAC"] = "[소     계]";

            sNEWA1NMAC = "GUBN = '" + table.Rows[i - 1]["GUBN"].ToString() + "' ";

            // 전일 원화잔액
            table.Rows[i]["WONJUNAMT"] = table.Compute("Sum(WONJUNAMT)", sNEWA1NMAC).ToString();

            // 전일 외화잔액
            table.Rows[i]["JUNAMT"] = table.Compute("Sum(JUNAMT)", sNEWA1NMAC).ToString();

            // 원화입금
            table.Rows[i]["WONIPAMT"] = table.Compute("Sum(WONIPAMT)", sNEWA1NMAC).ToString();

            // 외화입금
            table.Rows[i]["IPAMT"] = table.Compute("Sum(IPAMT)", sNEWA1NMAC).ToString();

            // 원화출금
            table.Rows[i]["WONCHAMT"] = table.Compute("Sum(WONCHAMT)", sNEWA1NMAC).ToString();

            // 외화출금
            table.Rows[i]["CHAMT"] = table.Compute("Sum(CHAMT)", sNEWA1NMAC).ToString();

            // 원화잔액
            table.Rows[i]["WONJANAMT"] = table.Compute("Sum(WONJANAMT)", sNEWA1NMAC).ToString();

            // 외화잔액
            table.Rows[i]["JANAMT"] = table.Compute("Sum(JANAMT)", sNEWA1NMAC).ToString();

            return table;
        }
        #endregion

        #region Description : 계정과목 기준 소계 내기
        private DataTable UP_CdacSuTotal_ds(DataSet ds)
        {
            string sNEWA1NMAC = string.Empty;
            int i = 0;

            // 합계를 보여주기 위한 빈 로우 하나 생성
            DataTable table = new DataTable();
            table = ds.Tables[0];

            DataRow row;
            int nNum = table.Rows.Count;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["A1CDAC"].ToString() != table.Rows[i]["A1CDAC"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    table.Rows[i]["A1NMAC"] = "[소     계]";

                    sNEWA1NMAC = "A1CDAC = '" + table.Rows[i - 1]["A1CDAC"].ToString() + "' ";

                    // 전일 원화잔액
                    table.Rows[i]["WONJUNAMT"] = table.Compute("Sum(WONJUNAMT)", sNEWA1NMAC).ToString();

                    // 전일 외화잔액
                    table.Rows[i]["JUNAMT"] = table.Compute("Sum(JUNAMT)", sNEWA1NMAC).ToString();

                    // 원화입금
                    table.Rows[i]["WONIPAMT"] = table.Compute("Sum(WONIPAMT)", sNEWA1NMAC).ToString();

                    // 외화입금
                    table.Rows[i]["IPAMT"] = table.Compute("Sum(IPAMT)", sNEWA1NMAC).ToString();

                    // 원화출금
                    table.Rows[i]["WONCHAMT"] = table.Compute("Sum(WONCHAMT)", sNEWA1NMAC).ToString();

                    // 외화출금
                    table.Rows[i]["CHAMT"] = table.Compute("Sum(CHAMT)", sNEWA1NMAC).ToString();

                    // 원화잔액
                    table.Rows[i]["WONJANAMT"] = table.Compute("Sum(WONJANAMT)", sNEWA1NMAC).ToString();

                    // 외화잔액
                    table.Rows[i]["JANAMT"] = table.Compute("Sum(JANAMT)", sNEWA1NMAC).ToString();

                    nNum = nNum + 1;

                    i = i + 1;
                }
            }

            row = table.NewRow();
            table.Rows.InsertAt(row, i);

            table.Rows[i]["A1NMAC"] = "[소     계]";

            sNEWA1NMAC = "A1CDAC = '" + table.Rows[i - 1]["A1CDAC"].ToString() + "' ";

            // 전일 원화잔액
            table.Rows[i]["WONJUNAMT"] = table.Compute("Sum(WONJUNAMT)", sNEWA1NMAC).ToString();

            // 전일 외화잔액
            table.Rows[i]["JUNAMT"] = table.Compute("Sum(JUNAMT)", sNEWA1NMAC).ToString();

            // 원화입금
            table.Rows[i]["WONIPAMT"] = table.Compute("Sum(WONIPAMT)", sNEWA1NMAC).ToString();

            // 외화입금
            table.Rows[i]["IPAMT"] = table.Compute("Sum(IPAMT)", sNEWA1NMAC).ToString();

            // 원화출금
            table.Rows[i]["WONCHAMT"] = table.Compute("Sum(WONCHAMT)", sNEWA1NMAC).ToString();

            // 외화출금
            table.Rows[i]["CHAMT"] = table.Compute("Sum(CHAMT)", sNEWA1NMAC).ToString();

            // 원화잔액
            table.Rows[i]["WONJANAMT"] = table.Compute("Sum(WONJANAMT)", sNEWA1NMAC).ToString();

            // 외화잔액
            table.Rows[i]["JANAMT"] = table.Compute("Sum(JANAMT)", sNEWA1NMAC).ToString();

            return table;
        }
        #endregion

        #region Description :  데이터 셋 변경(출력용)
        private DataSet UP_PRDataSetReport(DataSet ds, string sGubn)
        {
            double dWONJUNAMT = 0;
            double dJUNAMT = 0;
            double dJANAMT = 0;

            DataSet retDs = new DataSet();

            DataTable table = new DataTable();

            table.Columns.Add("A1CDAC", typeof(System.String));
            table.Columns.Add("A1NMAC", typeof(System.String));
            table.Columns.Add("CDDESC2", typeof(System.String));
            table.Columns.Add("CODE", typeof(System.String));
            table.Columns.Add("IHGUBN", typeof(System.String));
            table.Columns.Add("JUNAMT", typeof(System.Double));
            table.Columns.Add("WONIPAMT", typeof(System.Double));
            table.Columns.Add("IPAMT", typeof(System.Double));
            table.Columns.Add("WONCHAMT", typeof(System.Double));
            table.Columns.Add("CHAMT", typeof(System.Double));
            table.Columns.Add("WONJANAMT", typeof(System.Double));
            table.Columns.Add("JANAMT", typeof(System.Double));            
            table.Columns.Add("WONJUNAMT", typeof(System.Double));
            table.Columns.Add("DATE", typeof(System.String));
            table.Columns.Add("GBN", typeof(System.String));
            table.Columns.Add("FUSFUNDAMT", typeof(System.Double));
            
            retDs.Tables.Add(table);

            try
            {
                // 우선 테이타 셋의 테이블의 처음부터 끝까지 도는 루프 코딩
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow row = retDs.Tables[0].NewRow();

                    //계정코드
                    row["A1CDAC"] = ds.Tables[0].Rows[i]["A1CDAC"].ToString();

                    // 계정명
                    row["A1NMAC"] = ds.Tables[0].Rows[i]["A1NMAC"].ToString();

                    // 은행명
                    row["CDDESC2"] = ds.Tables[0].Rows[i]["CDDESC2"].ToString();

                    row["CODE"] = ds.Tables[0].Rows[i]["CODE"].ToString();

                    if (sGubn.ToString() != "1")
                    {
                        // 계좌번호
                        row["IHGUBN"] = ds.Tables[0].Rows[i]["GAEJUA"].ToString().Trim();
                    }
                    else
                    {
                        row["IHGUBN"] = ds.Tables[0].Rows[i]["IHGUBN"].ToString().Trim();
                    }

                    // 전일 원화잔액
                    if (ds.Tables[0].Rows[i]["A1TAG01"].ToString() == "D" &&
                       ds.Tables[0].Rows[i]["GBN"].ToString() != "5" &&
                       ds.Tables[0].Rows[i]["GBN"].ToString() != "6")
                    {
                        dWONJUNAMT = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AMOUNT1"].ToString())) +
                                     double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["JDRAMT"].ToString())) -
                                     double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["JCRAMT"].ToString()));
                    }
                    else if (ds.Tables[0].Rows[i]["A1TAG01"].ToString() == "C" &&
                            ds.Tables[0].Rows[i]["GBN"].ToString() != "5" &&
                            ds.Tables[0].Rows[i]["GBN"].ToString() != "6")
                    {
                        dWONJUNAMT = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AMOUNT1"].ToString())) +
                                     double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["JCRAMT"].ToString())) -
                                     double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["JDRAMT"].ToString()));
                    }
                    else
                    {
                        dWONJUNAMT = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AMOUNT1"].ToString()));
                    }

                    // 전일 외화잔액
                    if (ds.Tables[0].Rows[i]["GBN"].ToString() == "5" ||
                       ds.Tables[0].Rows[i]["GBN"].ToString() == "6")
                    {
                        dJUNAMT = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AMOUNT1"].ToString()));
                    }
                    else
                    {
                        dJUNAMT = 0;
                    }

                    // 전일 원화잔액
                    row["WONJUNAMT"] = dWONJUNAMT - dJUNAMT;

                    // 전일 외화잔액
                    row["JUNAMT"] = dJUNAMT;

                    // 원화 입금액
                    row["WONIPAMT"] = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["IN_AMT"].ToString()));

                    // 외화 입금액
                    if (ds.Tables[0].Rows[i]["GBN"].ToString() == "5" ||
                       ds.Tables[0].Rows[i]["GBN"].ToString() == "6")
                    {
                        row["IPAMT"] = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["IN_AMT"].ToString()));
                    }
                    else
                    {
                        row["IPAMT"] = 0;
                    }

                    // 원화 출금액
                    row["WONCHAMT"] = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["OUT_AMT"].ToString()));


                    // 외화 출금액
                    if (ds.Tables[0].Rows[i]["GBN"].ToString() == "5" ||
                       ds.Tables[0].Rows[i]["GBN"].ToString() == "6")
                    {
                        row["CHAMT"] = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["OUT_AMT"].ToString()));
                    }
                    else
                    {
                        row["CHAMT"] = 0;
                    }

                    // 외화 잔액
                    if (ds.Tables[0].Rows[i]["GBN"].ToString() == "5" ||
                       ds.Tables[0].Rows[i]["GBN"].ToString() == "6")
                    {
                        dJANAMT = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AMOUNT2"].ToString()));
                    }
                    else
                    {
                        dJANAMT = 0;
                    }

                    // 원화 잔액
                    if (ds.Tables[0].Rows[i]["A1TAG01"].ToString() == "D" &&
                       ds.Tables[0].Rows[i]["GBN"].ToString() != "5" &&
                       ds.Tables[0].Rows[i]["GBN"].ToString() != "6")
                    {
                        row["WONJANAMT"] = dWONJUNAMT + double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["IN_AMT"].ToString())) -
                                           double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["OUT_AMT"].ToString()));
                    }
                    else if (ds.Tables[0].Rows[i]["A1TAG01"].ToString() == "C" &&
                            ds.Tables[0].Rows[i]["GBN"].ToString() != "5" &&
                            ds.Tables[0].Rows[i]["GBN"].ToString() != "6")
                    {
                        row["WONJANAMT"] = dWONJUNAMT + double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["OUT_AMT"].ToString())) -
                                           double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["IN_AMT"].ToString())) - dJANAMT;
                    }
                    else
                    {
                        row["WONJANAMT"] = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["AMOUNT2"].ToString())) - dJANAMT;
                    }

                    // 외화 잔액
                    row["JANAMT"] = dJANAMT;
                    
                    row["DATE"] = DTP01_GSTDATE.GetString().ToString();
                    row["GBN"] = ds.Tables[0].Rows[i]["GBN"].ToString();
                    row["FUSFUNDAMT"] = Convert.ToDouble(Get_Numeric(TXT01_GSOVERDRAMT.GetValue().ToString())); 

                    retDs.Tables[0].Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                string sMessage = string.Empty;
                sMessage = ex.ToString();
            }

            return retDs;
        }
        #endregion

        #region  Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            if (CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2BDBT275", this.DTP01_GSTDATE.GetString(), this.TXT01_GSOVERDRAMT.GetValue());
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B95W237", this.DTP01_GSTDATE.GetString(), this.TXT01_GSOVERDRAMT.GetValue());
            }

            ds = this.DbConnector.ExecuteDataSet();
            ds = UP_PRDataSetReport(ds, CBO01_INQOPTION.GetValue().ToString().Trim());


            if (ds.Tables[0].Rows.Count > 0)
            {
                SectionReport rptMaster = null;
                rptMaster = new TYACMF006R();
                rptMaster.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rptMaster, ds.Tables[0])).ShowDialog();
            }
        }
        #endregion
    }
}
