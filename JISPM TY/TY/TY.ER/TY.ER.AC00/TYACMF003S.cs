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
    /// 일일 거래내역현황 및 집계표 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.11.05 11:35
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2B23G036 : 일일 거래내역 상세(원화) 조회
    ///  TY_P_AC_2B53E066 : 일일 거래내역 집계(원화) 조회
    ///  TY_P_AC_2B59W051 : 일일 거래내역 집계(외화) 조회
    ///  TY_P_AC_2B5BG052 : 일일 거래내역 상세(외화) 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2B53A064 : 일일 거래내역 집계(원화) 조회
    ///  TY_S_AC_2B53L074 : 일일 거래내역 상세(외화) 조회
    ///  TY_S_AC_2B5BN053 : 일일 거래내역 상세(원화) 조회
    ///  TY_S_AC_2B62H128 : 일일 거래내역 집계(외화) 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  H1CDBK : 은 행
    ///  H1NOAC : 계좌번호
    ///  GGUBUN : 구분
    ///  H1DATE : 거래일자
    /// </summary>
    public partial class TYACMF003S : TYBase
    {
        private bool _Isloaded = false;

        #region  Description : 폼 로드 이벤트
        public TYACMF003S()
        {
            InitializeComponent();
        }

        private void TYACMF003S_Load(object sender, System.EventArgs e)
        {
            this.CBH01_H1CDBK.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(CBH01_H1CDBK_CodeBoxDataBinded);

            this.DTP01_H1DATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_H1CDBK.OnCodeBoxDataBinded(null, null);

            this.SetStartingFocus(this.DTP01_H1DATE);

        }
        #endregion

        #region  Description : 계좌번호 CBH01_H1CDBK_CodeBoxDataBinded 이벤트
        private void CBH01_H1CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_H1CDBK.GetValue().ToString();
            this.CBH01_H1NOAC.DummyValue = groupCode;
            this.CBH01_H1NOAC.SetValue("");
            this.CBH01_H1NOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_H1NOAC.Initialize();
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
              this.FPS91_TY_S_AC_2B53A064.Initialize();
              this.FPS91_TY_S_AC_2B5BN053.Initialize();

              this.FPS91_TY_S_AC_2B53L074.Initialize();              
              this.FPS91_TY_S_AC_2B62H128.Initialize(); 
            
            if (CBO01_GGUBUN.GetValue().ToString() == "1")
            {
                this.FPS91_TY_S_AC_2B53A064.Visible = true;
                this.FPS91_TY_S_AC_2B5BN053.Visible = true;

                this.FPS91_TY_S_AC_2B53L074.Visible = false;
                this.FPS91_TY_S_AC_2B62H128.Visible = false;

                this.DbConnector.CommandClear();
                //상세
                this.DbConnector.Attach("TY_P_AC_2B23G036", this.DTP01_H1DATE.GetString(), this.DTP01_H1DATE.GetString());
                this.FPS91_TY_S_AC_2B5BN053.SetValue(UP_SumRowAdd_Won_Detail(this.DbConnector.ExecuteDataTable()));

                if (this.FPS91_TY_S_AC_2B5BN053.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2B5BN053, "B1NAME", "당좌예금 소계", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2B5BN053, "B1NAME", "보통예금 소계", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2B5BN053, "B1NAME", "기업신탁 소계", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2B5BN053, "B1NAME", "합 계", SumRowType.Total);
                }

                this.DbConnector.CommandClear();
                //집계
                this.DbConnector.Attach("TY_P_AC_2B53E066", this.DTP01_H1DATE.GetString());
                this.FPS91_TY_S_AC_2B53A064.SetValue(UP_SumRowAdd_Won_Master(this.DbConnector.ExecuteDataTable()));

                if (this.FPS91_TY_S_AC_2B53A064.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2B53A064, "B1NMBK", "당좌예금 소계", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2B53A064, "B1NMBK", "보통예금 소계", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2B53A064, "B1NMBK", "기업신탁 소계", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2B53A064, "B1NMBK", "합 계", SumRowType.Total);

                }

            }
            else
            {
                this.FPS91_TY_S_AC_2B53A064.Visible = false;
                this.FPS91_TY_S_AC_2B5BN053.Visible = false;

                this.FPS91_TY_S_AC_2B53L074.Visible = true;
                this.FPS91_TY_S_AC_2B62H128.Visible = true;

                this.DbConnector.CommandClear();
                //상세
                this.DbConnector.Attach("TY_P_AC_2B5BG052", this.DTP01_H1DATE.GetString().Substring(0,4), this.DTP01_H1DATE.GetString(), this.DTP01_H1DATE.GetString());
                this.FPS91_TY_S_AC_2B53L074.SetValue(UP_SumRowAdd_USD_Detail(this.DbConnector.ExecuteDataTable()));

                if (this.FPS91_TY_S_AC_2B53L074.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2B53L074, "H1NOAC", "외화당좌예금 소계", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2B53L074, "H1NOAC", "외화보통예금 소계", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2B53L074, "H1NOAC", "합 계", SumRowType.Total);
                }

                this.DbConnector.CommandClear();
                //집계
                this.DbConnector.Attach("TY_P_AC_2B59W051", this.DTP01_H1DATE.GetString().Substring(0,4), this.DTP01_H1DATE.GetString());
                this.FPS91_TY_S_AC_2B62H128.SetValue(UP_SumRowAdd_USD_Master(this.DbConnector.ExecuteDataTable()));

                if (this.FPS91_TY_S_AC_2B62H128.CurrentRowCount > 0)
                {
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2B62H128, "H1NOAC", "외화당좌예금 소계", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2B62H128, "H1NOAC", "외화보통예금 소계", SumRowType.SubTotal);
                    this.SetSpreadSumRow(this.FPS91_TY_S_AC_2B62H128, "H1NOAC", "합 계", SumRowType.Total);
                }

            }
        }
        #endregion

        
        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {            
            DataTable dtdetail     = new DataTable();
            DataTable dtHap = new DataTable();

            if (CBO01_GGUBUN.GetValue().ToString() == "1")
            {               
                this.DbConnector.CommandClear();
                //상세
                this.DbConnector.Attach("TY_P_AC_2B23G036", this.DTP01_H1DATE.GetString(), this.DTP01_H1DATE.GetString());
                dtdetail = this.DbConnector.ExecuteDataTable();

                this.DbConnector.CommandClear();
                //집계
                this.DbConnector.Attach("TY_P_AC_2B53E066", this.DTP01_H1DATE.GetString());
                dtHap = this.DbConnector.ExecuteDataTable();

            }
            else
            {

                this.DbConnector.CommandClear();
                //상세
                this.DbConnector.Attach("TY_P_AC_2B5BG052", this.DTP01_H1DATE.GetString().Substring(0,4), this.DTP01_H1DATE.GetString(), this.DTP01_H1DATE.GetString());
                dtdetail = this.DbConnector.ExecuteDataTable();

                this.DbConnector.Attach("TY_P_AC_2B59W051", this.DTP01_H1DATE.GetString().Substring(0, 4), this.DTP01_H1DATE.GetString());
                dtHap = this.DbConnector.ExecuteDataTable();
            }


            if (dtdetail.Rows.Count > 0 && dtHap.Rows.Count > 0)
            {
                SectionReport rptMaster = null;
                SectionReport rptdetail = null;

                if (CBO01_GGUBUN.GetValue().ToString() == "1")
                {
                    rptMaster = new TYACMF003R1();
                    rptdetail = new TYACMF003R();

                }
                else
                {
                    rptMaster = new TYACMF003R3();
                    rptdetail = new TYACMF003R2();
                }

                rptMaster.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rptMaster, dtHap)).ShowDialog();

                rptdetail.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rptdetail, dtdetail)).ShowDialog();
            }
        }
        #endregion

        #region  Description : UP_SumRowAdd_Won_Detail 원화 소계, 총계 넣기
        private DataTable UP_SumRowAdd_Won_Detail(DataTable dt)
        {
            int i = 0;

            string sE3CDAC = "";

            double dJunilAmt = 0;
            double dInAmt = 0;
            double dOutAmt = 0;
            double dTotalAmt = 0;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            if (nNum > 0)
            {
                for (i = 1; i < nNum; i++)
                {
                    if (table.Rows[i - 1]["E3CDAC"].ToString() != table.Rows[i]["E3CDAC"].ToString())
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        // 합 계 이름 넣기
                        //if (table.Rows[i - 1]["E3CDAC"].ToString() == "11100301")
                        //{
                        //    table.Rows[i]["B1NAME"] = "당좌예금 소계";
                        //}
                        //else if (table.Rows[i - 1]["E3CDAC"].ToString() == "11100301")
                        //{
                        //    table.Rows[i]["B1NAME"] = "보통예금 소계";
                        //}

                        table.Rows[i]["B1NAME"] = table.Rows[i-1]["A1ABAC"].ToString() + " 소계";

                        sE3CDAC = "E3CDAC = '" + table.Rows[i - 1]["E3CDAC"].ToString() + "' ";


                        table.Rows[i]["JUNILJANAMT"] = table.Compute("SUM(JUNILJANAMT)", sE3CDAC).ToString();
                        table.Rows[i]["INAMT"] = table.Compute("SUM(INAMT)", sE3CDAC).ToString();
                        table.Rows[i]["OUTAMT"] = table.Compute("SUM(OUTAMT)", sE3CDAC).ToString();
                        //table.Rows[i]["TOTALJAN"] = table.Compute("SUM(TOTALJAN)", sE3CDAC).ToString();

                        dJunilAmt = dJunilAmt + Convert.ToDouble(table.Rows[i]["JUNILJANAMT"].ToString());
                        dInAmt = dInAmt + Convert.ToDouble(table.Rows[i]["INAMT"].ToString());
                        dOutAmt = dOutAmt + Convert.ToDouble(table.Rows[i]["OUTAMT"].ToString());
                        dTotalAmt = Convert.ToDouble(table.Rows[i]["JUNILJANAMT"].ToString()) + Convert.ToDouble(table.Rows[i]["INAMT"].ToString()) - Convert.ToDouble(table.Rows[i]["OUTAMT"].ToString());

                        table.Rows[i]["TOTALJAN"] = dTotalAmt.ToString();

                        nNum = nNum + 1;

                        i = i + 1;
                    }
                }

                /******* 마지막 거래처의 대한 로우 생성*****/
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                //if (table.Rows[i]["E3CDAC"].ToString() == "11100301")
                //{
                //    table.Rows[i]["B1NAME"] = "당좌예금 소계";
                //}
                //else
                //{
                //    table.Rows[i]["B1NAME"] = "보통예금 소계";
                //}

                table.Rows[i]["B1NAME"] = table.Rows[i-1]["A1ABAC"].ToString() + " 소계";

                sE3CDAC = "E3CDAC = '" + table.Rows[i - 1]["E3CDAC"].ToString() + "' ";

                // 잔액
                table.Rows[i]["JUNILJANAMT"] = table.Compute("SUM(JUNILJANAMT)", sE3CDAC).ToString();
                table.Rows[i]["INAMT"] = table.Compute("SUM(INAMT)", sE3CDAC).ToString();
                table.Rows[i]["OUTAMT"] = table.Compute("SUM(OUTAMT)", sE3CDAC).ToString();
                //table.Rows[i]["TOTALJAN"] = table.Compute("SUM(TOTALJAN)", sE3CDAC).ToString();            

                dJunilAmt = dJunilAmt + Convert.ToDouble(table.Rows[i]["JUNILJANAMT"].ToString());
                dInAmt = dInAmt + Convert.ToDouble(table.Rows[i]["INAMT"].ToString());
                dOutAmt = dOutAmt + Convert.ToDouble(table.Rows[i]["OUTAMT"].ToString());
                dTotalAmt = Convert.ToDouble(table.Rows[i]["JUNILJANAMT"].ToString()) + Convert.ToDouble(table.Rows[i]["INAMT"].ToString()) - Convert.ToDouble(table.Rows[i]["OUTAMT"].ToString()); ;

                table.Rows[i]["TOTALJAN"] = dTotalAmt.ToString();


                /******** 총계를 위한 Row 생성 **************/
                row = table.NewRow();
                table.Rows.InsertAt(row, i + 1);

                // 합 계 이름 넣기
                table.Rows[i + 1]["B1NAME"] = "합 계";

                dTotalAmt = dJunilAmt + dInAmt - dOutAmt;

                table.Rows[i + 1]["JUNILJANAMT"] = string.Format("{0:#,##0}", dJunilAmt);
                table.Rows[i + 1]["INAMT"] = string.Format("{0:#,##0}", dInAmt);
                table.Rows[i + 1]["OUTAMT"] = string.Format("{0:#,##0}", dOutAmt);
                table.Rows[i + 1]["TOTALJAN"] = string.Format("{0:#,##0}", dTotalAmt);
            }

            return table;

        }
        #endregion

        #region  Description : UP_SumRowAdd_Won_Master 원화 소계, 총계 넣기
        private DataTable UP_SumRowAdd_Won_Master(DataTable dt)
        {
            int i = 0;

            string sB1CDAC = "";

            double dJunilAmt = 0;
            double dInAmt = 0;
            double dOutAmt = 0;
            double dTotalAmt = 0;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            if (nNum > 0)
            {
                for (i = 1; i < nNum; i++)
                {
                    if (table.Rows[i - 1]["B1CDAC"].ToString() != table.Rows[i]["B1CDAC"].ToString())
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        // 합 계 이름 넣기
                        //if (table.Rows[i - 1]["B1CDAC"].ToString() == "11100301")
                        //{
                        //    table.Rows[i]["B1NMBK"] = "당좌예금 소계";
                        //}
                        //else
                        //{
                        //    table.Rows[i]["B1NMBK"] = "보통예금 소계";
                        //}

                        table.Rows[i]["B1NMBK"] = table.Rows[i - 1]["A1ABAC"].ToString() + " 소계";

                        sB1CDAC = "B1CDAC = '" + table.Rows[i - 1]["B1CDAC"].ToString() + "' ";


                        table.Rows[i]["JUNILJANAMT"] = table.Compute("SUM(JUNILJANAMT)", sB1CDAC).ToString();
                        table.Rows[i]["INAMT"] = table.Compute("SUM(INAMT)", sB1CDAC).ToString();
                        table.Rows[i]["OUTAMT"] = table.Compute("SUM(OUTAMT)", sB1CDAC).ToString();
                        //table.Rows[i]["TOTALJAN"] = table.Compute("SUM(TOTALJAN)", sB1CDAC).ToString();

                        dJunilAmt = dJunilAmt + Convert.ToDouble(table.Rows[i]["JUNILJANAMT"].ToString());
                        dInAmt = dInAmt + Convert.ToDouble(table.Rows[i]["INAMT"].ToString());
                        dOutAmt = dOutAmt + Convert.ToDouble(table.Rows[i]["OUTAMT"].ToString());
                        dTotalAmt = Convert.ToDouble(table.Rows[i]["JUNILJANAMT"].ToString()) + Convert.ToDouble(table.Rows[i]["INAMT"].ToString()) - Convert.ToDouble(table.Rows[i]["OUTAMT"].ToString());

                        table.Rows[i]["TOTALJAN"] = dTotalAmt.ToString();

                        nNum = nNum + 1;

                        i = i + 1;
                    }
                }

                /******* 마지막 거래처의 대한 로우 생성*****/
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                //if (table.Rows[i]["B1CDAC"].ToString() == "11100301")
                //{
                //    table.Rows[i]["B1NMBK"] = "당좌예금 소계";
                //}
                //else
                //{
                //    table.Rows[i]["B1NMBK"] = "보통예금 소계";
                //}

                table.Rows[i]["B1NMBK"] = table.Rows[i - 1]["A1ABAC"].ToString() + " 소계";

                sB1CDAC = "B1CDAC = '" + table.Rows[i - 1]["B1CDAC"].ToString() + "' ";

                // 잔액
                table.Rows[i]["JUNILJANAMT"] = table.Compute("SUM(JUNILJANAMT)", sB1CDAC).ToString();
                table.Rows[i]["INAMT"] = table.Compute("SUM(INAMT)", sB1CDAC).ToString();
                table.Rows[i]["OUTAMT"] = table.Compute("SUM(OUTAMT)", sB1CDAC).ToString();
                //table.Rows[i]["TOTALJAN"] = table.Compute("SUM(TOTALJAN)", sB1CDAC).ToString();            

                dJunilAmt = dJunilAmt + Convert.ToDouble(table.Rows[i]["JUNILJANAMT"].ToString());
                dInAmt = dInAmt + Convert.ToDouble(table.Rows[i]["INAMT"].ToString());
                dOutAmt = dOutAmt + Convert.ToDouble(table.Rows[i]["OUTAMT"].ToString());
                dTotalAmt = Convert.ToDouble(table.Rows[i]["JUNILJANAMT"].ToString()) + Convert.ToDouble(table.Rows[i]["INAMT"].ToString()) - Convert.ToDouble(table.Rows[i]["OUTAMT"].ToString()); ;

                table.Rows[i]["TOTALJAN"] = dTotalAmt.ToString();


                /******** 총계를 위한 Row 생성 **************/
                row = table.NewRow();
                table.Rows.InsertAt(row, i + 1);

                // 합 계 이름 넣기
                table.Rows[i + 1]["B1NMBK"] = "합 계";

                dTotalAmt = dJunilAmt + dInAmt - dOutAmt;

                table.Rows[i + 1]["JUNILJANAMT"] = string.Format("{0:#,##0}", dJunilAmt);
                table.Rows[i + 1]["INAMT"] = string.Format("{0:#,##0}", dInAmt);
                table.Rows[i + 1]["OUTAMT"] = string.Format("{0:#,##0}", dOutAmt);
                table.Rows[i + 1]["TOTALJAN"] = string.Format("{0:#,##0}", dTotalAmt);
            }

            return table;

        }
        #endregion

        #region  Description : UP_SumRowAdd_USD_Detail 원화 소계, 총계 넣기
        private DataTable UP_SumRowAdd_USD_Detail(DataTable dt)
        {
            int i = 0;

            string sH1CDAC = "";

            double dJunilAmt = 0;
            double dInAmt = 0;
            double dOutAmt = 0;
            double dTotalAmt = 0;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            if (nNum > 0)
            {
                for (i = 1; i < nNum; i++)
                {
                    if (table.Rows[i - 1]["H1CDAC"].ToString() != table.Rows[i]["H1CDAC"].ToString())
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        // 합 계 이름 넣기
                        if (table.Rows[i - 1]["H1CDAC"].ToString() == "11100309")
                        {
                            table.Rows[i]["H1NOAC"] = "외화당좌예금 소계";
                        }
                        else
                        {
                            table.Rows[i]["H1NOAC"] = "외화보통예금 소계";
                        }

                        sH1CDAC = "H1CDAC = '" + table.Rows[i - 1]["H1CDAC"].ToString() + "' ";


                        table.Rows[i]["JUNILJANAMT"] = table.Compute("SUM(JUNILJANAMT)", sH1CDAC).ToString();
                        table.Rows[i]["INAMT"] = table.Compute("SUM(INAMT)", sH1CDAC).ToString();
                        table.Rows[i]["OUTAMT"] = table.Compute("SUM(OUTAMT)", sH1CDAC).ToString();
                        //table.Rows[i]["TOTALJAN"] = table.Compute("SUM(TOTALJAN)", sH1CDAC).ToString();

                        dJunilAmt = dJunilAmt + Convert.ToDouble(table.Rows[i]["JUNILJANAMT"].ToString());
                        dInAmt = dInAmt + Convert.ToDouble(table.Rows[i]["INAMT"].ToString());
                        dOutAmt = dOutAmt + Convert.ToDouble(table.Rows[i]["OUTAMT"].ToString());
                        dTotalAmt = Convert.ToDouble(table.Rows[i]["JUNILJANAMT"].ToString()) + Convert.ToDouble(table.Rows[i]["INAMT"].ToString()) - Convert.ToDouble(table.Rows[i]["OUTAMT"].ToString());

                        table.Rows[i]["TOTALJAN"] = dTotalAmt.ToString();

                        nNum = nNum + 1;

                        i = i + 1;
                    }
                }

                /******* 마지막 거래처의 대한 로우 생성*****/
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                if (table.Rows[i]["H1CDAC"].ToString() == "11100309")
                {
                    table.Rows[i]["H1NOAC"] = "외화당좌예금 소계";
                }
                else
                {
                    table.Rows[i]["H1NOAC"] = "외화보통예금 소계";
                }

                sH1CDAC = "H1CDAC = '" + table.Rows[i - 1]["H1CDAC"].ToString() + "' ";

                // 잔액
                table.Rows[i]["JUNILJANAMT"] = table.Compute("SUM(JUNILJANAMT)", sH1CDAC).ToString();
                table.Rows[i]["INAMT"] = table.Compute("SUM(INAMT)", sH1CDAC).ToString();
                table.Rows[i]["OUTAMT"] = table.Compute("SUM(OUTAMT)", sH1CDAC).ToString();
                //table.Rows[i]["TOTALJAN"] = table.Compute("SUM(TOTALJAN)", sH1CDAC).ToString();            

                dJunilAmt = dJunilAmt + Convert.ToDouble(table.Rows[i]["JUNILJANAMT"].ToString());
                dInAmt = dInAmt + Convert.ToDouble(table.Rows[i]["INAMT"].ToString());
                dOutAmt = dOutAmt + Convert.ToDouble(table.Rows[i]["OUTAMT"].ToString());
                dTotalAmt = Convert.ToDouble(table.Rows[i]["JUNILJANAMT"].ToString()) + Convert.ToDouble(table.Rows[i]["INAMT"].ToString()) - Convert.ToDouble(table.Rows[i]["OUTAMT"].ToString()); ;

                table.Rows[i]["TOTALJAN"] = dTotalAmt.ToString();


                /******** 총계를 위한 Row 생성 **************/
                row = table.NewRow();
                table.Rows.InsertAt(row, i + 1);

                // 합 계 이름 넣기
                table.Rows[i + 1]["H1NOAC"] = "합 계";

                dTotalAmt = dJunilAmt + dInAmt - dOutAmt;

                table.Rows[i + 1]["JUNILJANAMT"] = string.Format("{0:#,##0.00}", dJunilAmt);
                table.Rows[i + 1]["INAMT"] = string.Format("{0:#,##0.00}", dInAmt);
                table.Rows[i + 1]["OUTAMT"] = string.Format("{0:#,##0.00}", dOutAmt);
                table.Rows[i + 1]["TOTALJAN"] = string.Format("{0:#,##0.00}", dTotalAmt);
            }

            return table;

        }
        #endregion

        #region  Description : UP_SumRowAdd_USD_Master 원화 소계, 총계 넣기
        private DataTable UP_SumRowAdd_USD_Master(DataTable dt)
        {
            int i = 0;

            string sH1CDAC = "";

            double dJunilAmt = 0;
            double dInAmt = 0;
            double dOutAmt = 0;
            double dTotalAmt = 0;

            DataTable table = new DataTable();

            table = dt;

            DataRow row;

            int nNum = table.Rows.Count;

            if (nNum > 0)
            {

                for (i = 1; i < nNum; i++)
                {
                    if (table.Rows[i - 1]["H1CDAC"].ToString() != table.Rows[i]["H1CDAC"].ToString())
                    {
                        row = table.NewRow();
                        table.Rows.InsertAt(row, i);

                        // 합 계 이름 넣기
                        if (table.Rows[i - 1]["H1CDAC"].ToString() == "11100309")
                        {
                            table.Rows[i]["H1NOAC"] = "외화당좌예금 소계";
                        }
                        else
                        {
                            table.Rows[i]["H1NOAC"] = "외화보통예금 소계";
                        }

                        sH1CDAC = "H1CDAC = '" + table.Rows[i - 1]["H1CDAC"].ToString() + "' ";


                        table.Rows[i]["JUNILJANAMT"] = table.Compute("SUM(JUNILJANAMT)", sH1CDAC).ToString();
                        table.Rows[i]["INAMT"] = table.Compute("SUM(INAMT)", sH1CDAC).ToString();
                        table.Rows[i]["OUTAMT"] = table.Compute("SUM(OUTAMT)", sH1CDAC).ToString();
                        //table.Rows[i]["TOTALJAN"] = table.Compute("SUM(TOTALJAN)", sH1CDAC).ToString();

                        dJunilAmt = dJunilAmt + Convert.ToDouble(table.Rows[i]["JUNILJANAMT"].ToString());
                        dInAmt = dInAmt + Convert.ToDouble(table.Rows[i]["INAMT"].ToString());
                        dOutAmt = dOutAmt + Convert.ToDouble(table.Rows[i]["OUTAMT"].ToString());
                        dTotalAmt = Convert.ToDouble(table.Rows[i]["JUNILJANAMT"].ToString()) + Convert.ToDouble(table.Rows[i]["INAMT"].ToString()) - Convert.ToDouble(table.Rows[i]["OUTAMT"].ToString());

                        table.Rows[i]["TOTALJAN"] = dTotalAmt.ToString();

                        nNum = nNum + 1;

                        i = i + 1;
                    }
                }

                /******* 마지막 거래처의 대한 로우 생성*****/
                row = table.NewRow();
                table.Rows.InsertAt(row, i);

                if (table.Rows[i]["H1CDAC"].ToString() == "11100309")
                {
                    table.Rows[i]["H1NOAC"] = "외화당좌예금 소계";
                }
                else
                {
                    table.Rows[i]["H1NOAC"] = "외화보통예금 소계";
                }

                sH1CDAC = "H1CDAC = '" + table.Rows[i - 1]["H1CDAC"].ToString() + "' ";

                // 잔액
                table.Rows[i]["JUNILJANAMT"] = table.Compute("SUM(JUNILJANAMT)", sH1CDAC).ToString();
                table.Rows[i]["INAMT"] = table.Compute("SUM(INAMT)", sH1CDAC).ToString();
                table.Rows[i]["OUTAMT"] = table.Compute("SUM(OUTAMT)", sH1CDAC).ToString();
                //table.Rows[i]["TOTALJAN"] = table.Compute("SUM(TOTALJAN)", sH1CDAC).ToString();            

                dJunilAmt = dJunilAmt + Convert.ToDouble(table.Rows[i]["JUNILJANAMT"].ToString());
                dInAmt = dInAmt + Convert.ToDouble(table.Rows[i]["INAMT"].ToString());
                dOutAmt = dOutAmt + Convert.ToDouble(table.Rows[i]["OUTAMT"].ToString());
                dTotalAmt = Convert.ToDouble(table.Rows[i]["JUNILJANAMT"].ToString()) + Convert.ToDouble(table.Rows[i]["INAMT"].ToString()) - Convert.ToDouble(table.Rows[i]["OUTAMT"].ToString()); ;

                table.Rows[i]["TOTALJAN"] = dTotalAmt.ToString();


                /******** 총계를 위한 Row 생성 **************/
                row = table.NewRow();
                table.Rows.InsertAt(row, i + 1);

                // 합 계 이름 넣기
                table.Rows[i + 1]["H1NOAC"] = "합 계";

                dTotalAmt = dJunilAmt + dInAmt - dOutAmt;

                table.Rows[i + 1]["JUNILJANAMT"] = string.Format("{0:#,##0.00}", dJunilAmt);
                table.Rows[i + 1]["INAMT"] = string.Format("{0:#,##0.00}", dInAmt);
                table.Rows[i + 1]["OUTAMT"] = string.Format("{0:#,##0.00}", dOutAmt);
                table.Rows[i + 1]["TOTALJAN"] = string.Format("{0:#,##0.00}", dTotalAmt);
            }

            return table;

        }
        #endregion


    }
}
