using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.US00
{
    /// <summary>
    /// 하역료 단가 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2016.06.08 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_919HR470 : 하역료 단가 관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    /// </summary>
    public partial class TYUSME026P : TYBase
    {
        #region Descriptino : 페이지 로드
        public TYUSME026P()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYUSME026P_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.CBH01_STHANGCHA.CodeText);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_92EG4780", this.CBH01_STHANGCHA.GetValue().ToString(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString(),
                                                        this.CBH01_GGOKJONG.GetValue().ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                DataTable retDt = UP_ReportConvert(dt);
                
                ActiveReport rpt = new TYUSME026R();
                // 세로 출력
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

                (new TYERGB001P(rpt, retDt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 출력 변환
        private DataTable UP_ReportConvert(DataTable dt)
        {
            string sTYC_BIYUL = string.Empty;
            string sHJ_BIYUL  = string.Empty;

            string sTYC_CNDISBIYUL = string.Empty;
            string sTYC_CNDEMBIYUL = string.Empty;

            string sHJ_CNDISBIYUL  = string.Empty;
            string sHJ_CNDEMBIYUL  = string.Empty;

            int i = 0;
            int j = 0;
            

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();



            string sMonth = string.Empty;

            DataTable Table = new DataTable();

            DataRow Row;

            Table.Columns.Add("TMREPORTNM1",typeof(System.String));
			Table.Columns.Add("TMREPORT1",typeof(System.String));
			Table.Columns.Add("TMREPORTNM2",typeof(System.String));
			Table.Columns.Add("TMREPORT2",typeof(System.String));
			Table.Columns.Add("TMREPORTNM3",typeof(System.String));
			Table.Columns.Add("TMREPORT3",typeof(System.String));
			Table.Columns.Add("TMREPORTNM4",typeof(System.String));
			Table.Columns.Add("TMREPORT4",typeof(System.String));
			Table.Columns.Add("TMREPORTNM5",typeof(System.String));
			Table.Columns.Add("TMREPORT5",typeof(System.String));
			Table.Columns.Add("TMREPORTNM6",typeof(System.String));
			Table.Columns.Add("TMREPORT6",typeof(System.String));
			Table.Columns.Add("TMREPORTNM7",typeof(System.String));
			Table.Columns.Add("TMREPORT7",typeof(System.String));
			Table.Columns.Add("TMREPORTNM8",typeof(System.String));
			Table.Columns.Add("TMREPORT8",typeof(System.String));
			Table.Columns.Add("TMREPORTNM9",typeof(System.String));
			Table.Columns.Add("TMREPORT9",typeof(System.String));
			Table.Columns.Add("TMREPORTNM10",typeof(System.String));
			Table.Columns.Add("TMREPORT10",typeof(System.String));
			Table.Columns.Add("TMREPORTNM11",typeof(System.String));
			Table.Columns.Add("TMREPORT11",typeof(System.String));
			Table.Columns.Add("TMREPORTNM12",typeof(System.String));
			Table.Columns.Add("TMREPORT12",typeof(System.String));
			Table.Columns.Add("TMREPORTNM13",typeof(System.String));
			Table.Columns.Add("TMREPORT13",typeof(System.String));
			Table.Columns.Add("TMREPORTNM14",typeof(System.String));
			Table.Columns.Add("TMREPORT14",typeof(System.String));
			Table.Columns.Add("TMREPORTNM15",typeof(System.String));
			Table.Columns.Add("TMREPORT15",typeof(System.String));
			Table.Columns.Add("TMREPORTNM16",typeof(System.String));
			Table.Columns.Add("TMREPORT16",typeof(System.String));
			Table.Columns.Add("TMREPORTNM17",typeof(System.String));
			Table.Columns.Add("TMREPORT17",typeof(System.String));
			Table.Columns.Add("TMREPORTNM18",typeof(System.String));
			Table.Columns.Add("TMREPORT18",typeof(System.String));
			Table.Columns.Add("TMREPORTNM19",typeof(System.String));
			Table.Columns.Add("TMREPORT19",typeof(System.String));
			Table.Columns.Add("TMREPORTNM20",typeof(System.String));
			Table.Columns.Add("TMREPORT20",typeof(System.String));
			Table.Columns.Add("TMREPORTNM21",typeof(System.String));
			Table.Columns.Add("TMREPORT21",typeof(System.String));
			Table.Columns.Add("TMREPORTNM22",typeof(System.String));
			Table.Columns.Add("TMREPORT22",typeof(System.String));
			Table.Columns.Add("TMREPORTNM23",typeof(System.String));
			Table.Columns.Add("TMREPORT23",typeof(System.String));
			Table.Columns.Add("TMREPORTNM24",typeof(System.String));
			Table.Columns.Add("TMREPORT24",typeof(System.String));
			Table.Columns.Add("TMREPORTNM25",typeof(System.String));
			Table.Columns.Add("TMREPORT25",typeof(System.String));
			Table.Columns.Add("TMREPORTNM26",typeof(System.String));
			Table.Columns.Add("TMREPORT26",typeof(System.String));
			Table.Columns.Add("TMREPORTNM27",typeof(System.String));
			Table.Columns.Add("TMREPORT27",typeof(System.String));

            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["LTGUBUN"].ToString() == "1") // 조출료
                {
                    // 하역사 - 조출료
                    sTYC_BIYUL = dt.Rows[i]["CNDISBIYUL"].ToString();
                    // 화주   - 조출료
                    sHJ_BIYUL = Convert.ToString(100 - int.Parse(dt.Rows[i]["CNDISBIYUL"].ToString()));
                }
                else // 체선료
                {
                    // 하역사 - 체선료
                    sTYC_BIYUL = dt.Rows[i]["CNDEMBIYUL"].ToString();
                    // 화주   - 체선료
                    sHJ_BIYUL = Convert.ToString(100 - double.Parse(dt.Rows[i]["CNDEMBIYUL"].ToString()));
                }



                Row = Table.NewRow();

                Row["TMREPORTNM1"] = "1. 모    선     명 (VESSEL NAME)";
                Row["TMREPORT1"] = dt.Rows[i]["LTHANGCHANM"].ToString();

                Row["TMREPORTNM2"] = "2. B      /   L 량 (QUANTITY AS PER B/L)";
                Row["TMREPORT2"] = String.Format("{0,9:N3}", double.Parse(dt.Rows[i]["IHBLQTY"].ToString())) +
                                     "M/T of  " +
                                     dt.Rows[i]["LTGOKJONGNM"].ToString();

                sMonth = UP_MonthCheck(dt.Rows[i]["IHIPHANG"].ToString().Substring(4, 2));
                Row["TMREPORTNM3"] = "3. 울  산   도  착 (ARRIVED AT ULSAN)";
                Row["TMREPORT3"] = dt.Rows[i]["IHIPHTIM"].ToString().Substring(0, 2) + ":" +
                                     dt.Rows[i]["IHIPHTIM"].ToString().Substring(2, 2) + ",     " +
                                     dt.Rows[i]["IHIPHANG"].ToString().Substring(6, 2) + "Th  of  " +
                                     sMonth + ".,    " +
                                     dt.Rows[i]["IHIPHANG"].ToString().Substring(0, 4);

                sMonth = UP_MonthCheck(dt.Rows[i]["IHJUBDAT"].ToString().Substring(4, 2));
                Row["TMREPORTNM4"] = "4. 울산부두 접  안 (BERTHED AT ULSAN)";
                Row["TMREPORT4"] = dt.Rows[i]["IHJUBTIM"].ToString().Substring(0, 2) + ":" +
                                     dt.Rows[i]["IHJUBTIM"].ToString().Substring(2, 2) + ",     " +
                                     dt.Rows[i]["IHJUBDAT"].ToString().Substring(6, 2) + "Th  of  " +
                                     sMonth + ".,    " +
                                     dt.Rows[i]["IHJUBDAT"].ToString().Substring(0, 4);

                sMonth = UP_MonthCheck(dt.Rows[i]["LTTENDATE"].ToString().Substring(4, 2));
                Row["TMREPORTNM5"] = "5. N / R    제  출 (N/R TENDERED)";
                Row["TMREPORT5"] = dt.Rows[i]["LTTENTIME"].ToString().Substring(0, 2) + ":" +
                                     dt.Rows[i]["LTTENTIME"].ToString().Substring(2, 2) + ",     " +
                                     dt.Rows[i]["LTTENDATE"].ToString().Substring(6, 2) + "Th  of  " +
                                     sMonth + ".,    " +
                                     dt.Rows[i]["LTTENDATE"].ToString().Substring(0, 4);

                sMonth = UP_MonthCheck(dt.Rows[i]["LTACCDATE"].ToString().Substring(4, 2));
                Row["TMREPORTNM6"] = "6. N / R    접  수 (N/R ACCEPTED)";
                Row["TMREPORT6"] = dt.Rows[i]["LTACCTIME"].ToString().Substring(0, 2) + ":" +
                                     dt.Rows[i]["LTACCTIME"].ToString().Substring(2, 2) + ",     " +
                                     dt.Rows[i]["LTACCDATE"].ToString().Substring(6, 2) + "Th  of  " +
                                     sMonth + ".,    " +
                                     dt.Rows[i]["LTACCDATE"].ToString().Substring(0, 4);

                sMonth = UP_MonthCheck(dt.Rows[i]["LTSTDATE"].ToString().Substring(4, 2));
                Row["TMREPORTNM7"] = "7. LAYTIME   시 작 (COMMENCED LAYTIME)";
                Row["TMREPORT7"] = dt.Rows[i]["LTSTTIME"].ToString().Substring(0, 2) + ":" +
                                     dt.Rows[i]["LTSTTIME"].ToString().Substring(2, 2) + ",     " +
                                     dt.Rows[i]["LTSTDATE"].ToString().Substring(6, 2) + "Th  of  " +
                                     sMonth + ".,    " +
                                     dt.Rows[i]["LTSTDATE"].ToString().Substring(0, 4);

                sMonth = UP_MonthCheck(dt.Rows[i]["IHJAKSTDAT"].ToString().Substring(4, 2));
                Row["TMREPORTNM8"] = "8. 하  역    개 시 (COMMENCED DISCHARGE)";
                Row["TMREPORT8"] = dt.Rows[i]["IHJAKSTTIM"].ToString().Substring(0, 2) + ":" +
                                     dt.Rows[i]["IHJAKSTTIM"].ToString().Substring(2, 2) + ",     " +
                                     dt.Rows[i]["IHJAKSTDAT"].ToString().Substring(6, 2) + "Th  of  " +
                                     sMonth + ".,    " +
                                     dt.Rows[i]["IHJAKSTDAT"].ToString().Substring(0, 4);

                sMonth = UP_MonthCheck(dt.Rows[i]["IHJAKENDAT"].ToString().Substring(4, 2));
                Row["TMREPORTNM9"] = "9. 하  역    완 료 (COMPLETED DISCHARGE)";
                Row["TMREPORT9"] = dt.Rows[i]["IHJAKENTIM"].ToString().Substring(0, 2) + ":" +
                                     dt.Rows[i]["IHJAKENTIM"].ToString().Substring(2, 2) + ",     " +
                                     dt.Rows[i]["IHJAKENDAT"].ToString().Substring(6, 2) + "Th  of  " +
                                     sMonth + ".,    " +
                                     dt.Rows[i]["IHJAKENDAT"].ToString().Substring(0, 4);
                Row["TMREPORTNM10"] = "10. 하  역   회 사 (STEVEDORE)";
                Row["TMREPORT10"] = "TAE YOUNG INDUSTRY CORP";

                Row["TMREPORTNM11"] = "11. 일 일 작 업 량 (RUN AS PER DAY)";
                Row["TMREPORT11"] = String.Format("{0,9:N3}", double.Parse(dt.Rows[i]["LTQTY"].ToString())) + " M/T";
                Row["TMREPORTNM12"] = "12. 작업  허용기간 (ALLOWED TIME)";
                Row["TMREPORT12"] = dt.Rows[i]["LTALLOW"].ToString().Substring(0, 2) + "D " + "-" +
                                      dt.Rows[i]["LTALLOW"].ToString().Substring(2, 2) + "H " + "-" +
                                      dt.Rows[i]["LTALLOW"].ToString().Substring(4, 2) + "M ";

                Row["TMREPORTNM13"] = "13. 작업  사용기간 (USED TIME)";
                Row["TMREPORT13"] = dt.Rows[i]["LTUSED"].ToString().Substring(0, 2) + "D " + "-" +
                                      dt.Rows[i]["LTUSED"].ToString().Substring(2, 2) + "H " + "-" +
                                      dt.Rows[i]["LTUSED"].ToString().Substring(4, 2) + "M ";

                if (Convert.ToDouble(dt.Rows[i]["LTALLOW"].ToString()) > Convert.ToDouble(dt.Rows[i]["LTUSED"].ToString()))
                {
                    Row["TMREPORTNM14"] = "14. 조  출  기  간 (SAVED TIME)";
                }
                else
                {
                    Row["TMREPORTNM14"] = "14. 체  선  기  간 (LOST TIME)";
                }

                Row["TMREPORT14"] = dt.Rows[i]["LTSAVE"].ToString().Substring(0, 2) + "D " + "-" +
                                    dt.Rows[i]["LTSAVE"].ToString().Substring(2, 2) + "H " + "-" +
                                    dt.Rows[i]["LTSAVE"].ToString().Substring(4, 2) + "M ";

                if (Convert.ToDouble(dt.Rows[i]["LTALLOW"].ToString()) > Convert.ToDouble(dt.Rows[i]["LTUSED"].ToString()))
                {
                    Row["TMREPORTNM15"] = "15. 일  조  출  액 (DES.PER DAY)";
                }
                else
                {
                    Row["TMREPORTNM15"] = "15. 일  체  선  액 (DEM.PER DAY)";
                }
                Row["TMREPORT15"] = "$" + String.Format("{0,9:N3}", double.Parse(dt.Rows[i]["LTDAYSAVE"].ToString()));

                if (Convert.ToDouble(dt.Rows[i]["LTALLOW"].ToString()) > Convert.ToDouble(dt.Rows[i]["LTUSED"].ToString()))
                {
                    Row["TMREPORTNM16"] = "16. 조  출  총  액 (DES.AMOUNT)";
                }
                else
                {
                    Row["TMREPORTNM16"] = "16. 체  선  총  액 (DEM.AMOUNT)";
                }

                Row["TMREPORT16"] = "$" + String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()));

                if (int.Parse(dt.Rows[i]["LTHANGCHA"].ToString()) >= 201901)
                {
                    // 농협, 오경 금액이 있을 경우
                    if (double.Parse(dt.Rows[i]["LTHJNHSAVE"].ToString()) != 0 && double.Parse(dt.Rows[i]["LTHJBSSAVE"].ToString()) != 0 &&
                        double.Parse(dt.Rows[i]["LTTYCNHSAVE"].ToString()) != 0 && double.Parse(dt.Rows[i]["LTTYCBSSAVE"].ToString()) != 0)
                    {
                        double dNH = 0;
                        double dOKYUNG = 0;

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_92EG2777", dt.Rows[i]["LTHANGCHA"].ToString(),
                                                                    dt.Rows[i]["LTGOKJONG"].ToString()
                                                                    );

                        dt1 = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            for (j = 0; j < dt1.Rows.Count; j++)
                            {
                                switch (dt1.Rows[j]["VNSOSOK"].ToString())
                                {
                                    case "0":

                                        dOKYUNG = double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString());

                                        break;

                                    case "2":

                                        dNH = double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString());

                                        break;
                                }
                            }
                        }

                        Row["TMREPORTNM17"] = "       * 농협 배분액(" + String.Format("{0,9:N2}", dNH) + " M/T) ";
                        Row["TMREPORT17"] = "$" + String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["NH_TOT"].ToString()));

                        Row["TMREPORTNM18"] = "       * 오경 배분액(" + String.Format("{0,9:N2}", dOKYUNG) + " M/T) ";
                        Row["TMREPORT18"] = "$" + String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["BS_TOT"].ToString()));

                        Row["TMREPORTNM19"] = "17. 화  주  배분액 (CONSIGNEE PORTION " + sHJ_BIYUL + "%)";
                        Row["TMREPORT19"] = "$" + String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTHJSAVE"].ToString()));

                        Row["TMREPORTNM20"] = "      * 농협 배분액(" + String.Format("{0,9:N2}", dNH) + " M/T) ";
                        Row["TMREPORT20"] = "$" + String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTHJNHSAVE"].ToString()));

                        Row["TMREPORTNM21"] = "      * 오경 배분액(" + String.Format("{0,9:N2}", dOKYUNG) + " M/T) ";
                        Row["TMREPORT21"] = "$" + String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTHJBSSAVE"].ToString()));

                        Row["TMREPORTNM22"] = "18. 하역회사배분액 (TAE YOUNG IND.CORP " + sTYC_BIYUL + "%)";
                        Row["TMREPORT22"] = "$" + String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTYCSAVE"].ToString()));

                        Row["TMREPORTNM23"] = "      * 농협 배분액(" + String.Format("{0,9:N2}", dNH) + " M/T) ";
                        Row["TMREPORT23"] = "$" + String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTYCNHSAVE"].ToString()));

                        Row["TMREPORTNM24"] = "      * 오경 배분액(" + String.Format("{0,9:N2}", dOKYUNG) + " M/T) ";
                        Row["TMREPORT24"] = "$" + String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTYCBSSAVE"].ToString()));

                        Row["TMREPORTNM25"] = "19. B / L 발급일자 (B/L DATE) ";
                        Row["TMREPORT25"] = "";

                        Row["TMREPORTNM26"] = "20. 환          율 (EXCHANGE RATE) ";
                        Row["TMREPORT26"] = "";

                        Row["TMREPORTNM27"] = "21. 선  박   회 사 (SHIPPING COMPANY) ";
                        Row["TMREPORT27"] = dt.Rows[i]["IHBRANCHNM"].ToString();
                    }
                    else
                    {
                        Row["TMREPORTNM17"] = "17. 화  주  배분액 (CONSIGNEE PORTION " + sHJ_BIYUL + "%)";
                        Row["TMREPORT17"] = "$" + String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTHJSAVE"].ToString()));

                        Row["TMREPORTNM18"] = "18. 하역회사배분액 (TAE YOUNG IND.CORP " + sTYC_BIYUL + "%)";
                        Row["TMREPORT18"] = "$" + String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTYCSAVE"].ToString()));

                        Row["TMREPORTNM19"] = "19. B / L 발급일자 (B/L DATE) ";
                        Row["TMREPORT19"] = "";

                        Row["TMREPORTNM20"] = "20. 환          율 (EXCHANGE RATE) ";
                        Row["TMREPORT20"] = "";

                        Row["TMREPORTNM21"] = "21. 선  박   회 사 (SHIPPING COMPANY) ";
                        Row["TMREPORT21"] = dt.Rows[i]["IHBRANCHNM"].ToString();
                    }
                }
                else // 구 ERP 로직임
                {
                    string sSOSOKSAVE = "";
                    string sTYCSAVE = "";
                    string sPRPER1 = "";
                    string sPRPER = "";

                    if (dt.Rows[i]["LTGUBUN"].ToString() == "1")
                    {
                        if (dt.Rows[i]["LTHANGCHA"].ToString() == "9634")
                        {
                            sSOSOKSAVE = String.Format("{0,9:N2}", Convert.ToDouble(double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()) * 0.2));
                        }
                        else
                        {
                            sSOSOKSAVE = String.Format("{0,9:N2}", Convert.ToDouble(double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()) * 0.6));
                        }

                        sTYCSAVE = String.Format("{0,9:N3}", double.Parse(dt.Rows[i]["LTTYCSAVE"].ToString()));
                    }

                    /* 입항소속이	삼양제넥스 모선일 경우
                     * 조출료: 하역사 50% 화주 50%=>'07-02-01UPDATE(P.J.S조출료:화주: 60%,하역사:40%)
                     * 체선료: 하역사 20% 화주 80% 
                     * */
                    if (dt.Rows[i]["IHSOSOK"].ToString() == "C")
                    {
                        if (dt.Rows[i]["LTGUBUN"].ToString() == "1")
                        {   //조출료
                            sTYCSAVE = String.Format("{0,9:N3}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()));
                            sPRPER1 = " 50%";  //=> 60=>50 2007.03.15pjs수정
                            sSOSOKSAVE = String.Format("{0,9:N3}", Convert.ToDouble(String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()) * 0.5))); //=> 0.6=>0.5 2007.03.15pjs수정

                            sTYCSAVE = String.Format("{0,9:N3}", Convert.ToDouble(Convert.ToDouble(sTYCSAVE) -
                                                                Convert.ToDouble(sSOSOKSAVE)));
                        }
                        else
                        {
                            if (dt.Rows[i]["LTHANGCHA"].ToString() == "201449")
                            {
                                //체선료
                                sTYCSAVE = String.Format("{0,9:N3}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()));
                                sPRPER1 = " 0%";
                                sTYCSAVE = String.Format("{0,9:N3}", Convert.ToDouble(Convert.ToDouble(sTYCSAVE)));
                            }
                            else
                            {
                                //체선료
                                sTYCSAVE = String.Format("{0,9:N3}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()));
                                sPRPER1 = " 80%"; //UPDATE 07-02-01
                                sSOSOKSAVE = String.Format("{0,9:N3}", Convert.ToDouble(String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()) * 0.8)));
                                sTYCSAVE = String.Format("{0,9:N3}", Convert.ToDouble(Convert.ToDouble(sTYCSAVE) -
                                    Convert.ToDouble(sSOSOKSAVE)));
                            }
                        }
                    }
                    else if (dt.Rows[i]["IHSOSOK"].ToString() == "6")
                    {
                        sTYCSAVE = String.Format("{0,9:N3}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()));
                        sPRPER1 = "100%";
                        sSOSOKSAVE = String.Format("{0,9:N3}", Convert.ToDouble(double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString())));
                        sTYCSAVE = String.Format("{0,9:N3}", Convert.ToDouble(Convert.ToDouble(sTYCSAVE) -
                                                             Convert.ToDouble(sSOSOKSAVE)));
                    }
                    else
                    {
                        if (dt.Rows[i]["LTGUBUN"].ToString() == "1")
                        {
                            //사협일경우: 조출료  하역사 50% 화주 50%
                            if (dt.Rows[i]["IHSOSOK"].ToString() == "1")
                            {
                                sTYCSAVE = String.Format("{0,9:N3}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()));
                                sPRPER1 = " 50%";
                                sSOSOKSAVE = String.Format("{0,9:N3}", Convert.ToDouble(String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()) * 0.5)));
                                sTYCSAVE = String.Format("{0,9:N3}", Convert.ToDouble(Convert.ToDouble(sTYCSAVE) -
                                                                                     Convert.ToDouble(sSOSOKSAVE)));
                            }

                            /* 200901항차부터 협회가 2(농협중앙회)일 경우
                             * 조출료 = 화주분(50%) 하역회사분(50%)
                             */

                            //농협일경우: 조출료  하역사 40% 화주 60%
                            if (dt.Rows[i]["IHSOSOK"].ToString() == "2")
                            {
                                if (double.Parse(dt.Rows[i]["LTHANGCHA"].ToString()) >= 200901)
                                {
                                    sTYCSAVE = String.Format("{0,9:N3}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()));
                                    sPRPER1 = " 50%";
                                    sSOSOKSAVE = String.Format("{0,9:N3}", Convert.ToDouble(String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()) * 0.5)));
                                    sTYCSAVE = String.Format("{0,9:N3}", Convert.ToDouble(Convert.ToDouble(sTYCSAVE) -
                                        Convert.ToDouble(sSOSOKSAVE)));
                                }
                                else
                                {
                                    sTYCSAVE = String.Format("{0,9:N3}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()));
                                    sPRPER1 = " 60%";
                                    sSOSOKSAVE = String.Format("{0,9:N3}", Convert.ToDouble(String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()) * 0.6)));
                                    sTYCSAVE = String.Format("{0,9:N3}", Convert.ToDouble(Convert.ToDouble(sTYCSAVE) -
                                        Convert.ToDouble(sSOSOKSAVE)));
                                }
                            }
                        }

                        if (dt.Rows[i]["LTGUBUN"].ToString() == "2")
                        {
                            // 사협일경우: 체선료  하역사 20% 화주 80%
                            // 2009.01.12 사협일경우: 체선료  하역사 30% 화주 70%
                            if (dt.Rows[i]["IHSOSOK"].ToString() == "1")
                            {
                                sTYCSAVE = String.Format("{0,9:N3}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()));
                                sPRPER1 = " 70%";
                                sSOSOKSAVE = String.Format("{0,9:N3}", Convert.ToDouble(String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()) * 0.7)));
                                sTYCSAVE = String.Format("{0,9:N3}", Convert.ToDouble(Convert.ToDouble(sTYCSAVE) -
                                                            Convert.ToDouble(sSOSOKSAVE)));
                            }

                            /* 200901항차부터 협회가 2(농협중앙회)일 경우
                             * 체선료 = 화주분(70%) 하역회사분(30%)
                             */
                            if (dt.Rows[i]["IHSOSOK"].ToString() == "2")
                            {
                                sTYCSAVE = String.Format("{0,9:N3}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()));
                                sPRPER1 = " 70%";
                                sSOSOKSAVE = String.Format("{0,9:N3}", Convert.ToDouble(String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()) * 0.7)));
                                sTYCSAVE = String.Format("{0,9:N3}", Convert.ToDouble(Convert.ToDouble(sTYCSAVE) -
                                    Convert.ToDouble(sSOSOKSAVE)));
                            }
                        }
                    }

                    //조출료=1, 1=사협, 2=농협중앙회, 3=삼양제넥스
                    if (dt.Rows[i]["LTGUBUN"].ToString() == "1")
                    {
                        if (dt.Rows[i]["IHSOSOK"].ToString() == "1")
                        {
                            sPRPER = "50%";
                        }
                        else if (dt.Rows[i]["IHSOSOK"].ToString() == "C")
                        {
                            sPRPER = "50%";  //40=>50 2007.03.15pjs수정
                        }
                        else if (dt.Rows[i]["IHSOSOK"].ToString() == "6")
                        {
                            sPRPER = "0%";
                        }
                        else if (dt.Rows[i]["IHSOSOK"].ToString() == "2")
                        {
                            sPRPER = "50%";
                        }
                        else
                        {
                            sPRPER = "50%";
                        }
                    }
                    //체선료=2, 1=사협, 2=농협중앙회, 3=삼양제넥스
                    if (dt.Rows[i]["LTGUBUN"].ToString() == "2")
                    {
                        if (dt.Rows[i]["IHSOSOK"].ToString() == "1")
                        {
                            // 2009.01.12 사협일경우: 체선료  하역사 30% 화주 70%
                            sPRPER = "30%";
                        }
                        else if (dt.Rows[i]["IHSOSOK"].ToString() == "C")
                        {
                            if (dt.Rows[i]["LTHANGCHA"].ToString() == "201449")
                            {
                                sPRPER = "100%";
                            }
                            else
                            {
                                sPRPER = "20%";
                            }
                        }
                        else if (dt.Rows[i]["IHSOSOK"].ToString() == "6")
                        {
                            sPRPER = "0%";
                        }
                        else if (dt.Rows[i]["IHSOSOK"].ToString() == "2")
                        {
                            sPRPER = "30%";
                        }
                        else
                        {
                            sPRPER = "40%";
                        }
                    }




                    if (double.Parse(dt.Rows[i]["LTHANGCHA"].ToString()) >= 201727)
                    {
                        //농협일경우
                        if (dt.Rows[i]["IHSOSOK"].ToString() == "2")
                        {
                            if (dt.Rows[i]["LTGUBUN"].ToString() == "1")  //조출료 일때만,,
                            {
                                /***************************************************************
                                 * 200942항차부터 
                                 * 하역사 배분액(50%) : 조출총액 * 50%
                                 * 화주사 배분율      : (조출총액 * 50%) * (B/L량 / 총B/L량)
                                 ***************************************************************/

                                string sLTTOTSAVE = string.Empty;
                                double dLTTOTSAVE = 0;

                                string sHalf_LTTOTSAVE = string.Empty;
                                double dHalf_LTTOTSAVE = 0;

                                sHalf_LTTOTSAVE = String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()) * 0.5);

                                sLTTOTSAVE = String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()));

                                dLTTOTSAVE = double.Parse(sLTTOTSAVE.ToString());

                                DataSet dsBEBUN = new DataSet();
                                DataSet ds_SOSOK = new DataSet();

                                double dBEBUNAMT = 0;
                                double dBEBUNTOT = 0;

                                double dOKYUNG = 0;
                                double dNH = 0;

                                double dOKYUNG_TOT_AMT = 0;
                                double dNH_TOT_AMT = 0;

                                double dOKYUNG_AMT1 = 0;
                                double dNH_AMT1 = 0;

                                double dOKYUNG_AMT2 = 0;
                                double dNH_AMT2 = 0;
                                
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach("TY_P_US_92EGQ781", dt.Rows[i]["LTHANGCHA"].ToString(),
                                                                            dt.Rows[i]["LTGOKJONG"].ToString()
                                                                            );

                                dt2 = this.DbConnector.ExecuteDataTable();
                                
                                if (dt2.Rows.Count > 0) // 농협이면서 오경이 존재
                                {
                                    this.DbConnector.CommandClear();
                                    this.DbConnector.Attach("TY_P_US_92EG2777", dt.Rows[i]["LTHANGCHA"].ToString(),
                                                                                dt.Rows[i]["LTGOKJONG"].ToString()
                                                                                );

                                    dt1 = this.DbConnector.ExecuteDataTable();

                                    if (dt.Rows.Count > 0)
                                    {
                                        for (j = 0; j < dt1.Rows.Count; j++)
                                        {
                                            switch (dt1.Rows[j]["VNSOSOK"].ToString())
                                            {
                                                case "0":

                                                    dOKYUNG = double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString());

                                                    break;

                                                case "2":

                                                    dNH = double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString());

                                                    dBEBUNAMT = dLTTOTSAVE * double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString()) / double.Parse(dt1.Rows[j]["SUMQTY"].ToString());

                                                    dBEBUNAMT = dBEBUNAMT * 1000;
                                                    dBEBUNAMT = dBEBUNAMT - (dBEBUNAMT % 1);
                                                    dBEBUNAMT = dBEBUNAMT / 1000;
                                                    dBEBUNTOT = dBEBUNTOT + dBEBUNAMT;

                                                    dNH_TOT_AMT = double.Parse(String.Format("{0,9:N2}", dBEBUNAMT));

                                                    break;
                                            }
                                        }
                                    }




                                    // 오경총액 = 조출총액 - 농협총액
                                    //									dOKYUNG_TOT_AMT = dLTTOTSAVE - dNH_TOT_AMT;

                                    dOKYUNG_TOT_AMT = double.Parse(String.Format("{0,9:N2}", dLTTOTSAVE - dNH_TOT_AMT));

                                    // 하역사 배분액 - 농협
                                    dNH_AMT2 = dNH_TOT_AMT / 2;
                                    dNH_AMT2 = double.Parse(UP_DotDelete(Convert.ToString(dNH_AMT2 * 100)));
                                    dNH_AMT2 = dNH_AMT2 / 100;

                                    // 하역사 배분액 - 오경
                                    dOKYUNG_AMT2 = dOKYUNG_TOT_AMT / 2;
                                    dOKYUNG_AMT2 = double.Parse(UP_DotDelete(Convert.ToString(dOKYUNG_AMT2 * 100)));
                                    dOKYUNG_AMT2 = dOKYUNG_AMT2 / 100;


                                    // 화주 배분액 - 농협
                                    //									dNH_AMT1 = dNH_TOT_AMT - dNH_AMT2;
                                    dNH_AMT1 = double.Parse(String.Format("{0,9:N2}", dNH_TOT_AMT - dNH_AMT2));


                                    // 화주 배분액 - 오경
                                    //									dOKYUNG_AMT1 = dOKYUNG_TOT_AMT - dOKYUNG_AMT2;
                                    dOKYUNG_AMT1 = double.Parse(String.Format("{0,9:N2}", dOKYUNG_TOT_AMT - dOKYUNG_AMT2));


                                    Row["TMREPORTNM17"] = "       * 농협 배분액(" + String.Format("{0,9:N2}", dNH) + " M/T) ";
                                    Row["TMREPORT17"] = "$" + String.Format("{0,9:N2}", dNH_TOT_AMT);

                                    Row["TMREPORTNM18"] = "       * 오경 배분액(" + String.Format("{0,9:N2}", dOKYUNG) + " M/T) ";
                                    Row["TMREPORT18"] = "$" + String.Format("{0,9:N2}", dOKYUNG_TOT_AMT);

                                    dHalf_LTTOTSAVE = dNH_AMT1 + dOKYUNG_AMT1;

                                    Row["TMREPORTNM19"] = "17. 화  주  배분액 (CONSIGNEE PORTION 50%)";
                                    Row["TMREPORT19"] = "$" + String.Format("{0,9:N2}", dHalf_LTTOTSAVE);

                                    Row["TMREPORTNM20"] = "      * 농협 배분액(" + String.Format("{0,9:N2}", dNH) + " M/T) ";
                                    Row["TMREPORT20"] = "$" + String.Format("{0,9:N2}", dNH_AMT1);

                                    Row["TMREPORTNM21"] = "      * 오경 배분액(" + String.Format("{0,9:N2}", dOKYUNG) + " M/T) ";
                                    Row["TMREPORT21"] = "$" + String.Format("{0,9:N2}", dOKYUNG_AMT1);

                                    dHalf_LTTOTSAVE = dNH_AMT2 + dOKYUNG_AMT2;

                                    Row["TMREPORTNM22"] = "18. 하역회사배분액 (TAE YOUNG IND.CORP 50%)";
                                    Row["TMREPORT22"] = "$" + String.Format("{0,9:N2}", dHalf_LTTOTSAVE);

                                    Row["TMREPORTNM23"] = "      * 농협 배분액(" + String.Format("{0,9:N2}", dNH) + " M/T) ";
                                    Row["TMREPORT23"] = "$" + String.Format("{0,9:N2}", dNH_AMT2);

                                    Row["TMREPORTNM24"] = "      * 오경 배분액(" + String.Format("{0,9:N2}", dOKYUNG) + " M/T) ";
                                    Row["TMREPORT24"] = "$" + String.Format("{0,9:N2}", dOKYUNG_AMT2);




                                    Row["TMREPORTNM25"] = "19. B / L 발급일자 (B/L DATE) ";
                                    Row["TMREPORT25"] = "";

                                    Row["TMREPORTNM26"] = "20. 환          율 (EXCHANGE RATE) ";
                                    Row["TMREPORT26"] = "";

                                    Row["TMREPORTNM27"] = "21. 선  박   회 사 (SHIPPING COMPANY)                   :";
                                    Row["TMREPORT27"] = dt.Rows[i]["IHBRANCHNM"].ToString();


                                }
                                else // 농협만 있음(오경 없음)
                                {
                                    Row["TMREPORTNM17"] = "17. 화  주  배분액 (CONSIGNEE PORTION " + sPRPER1 + " )";
                                    Row["TMREPORT17"] = "$" + sSOSOKSAVE;

                                    Row["TMREPORTNM18"] = "18. 하역회사배분액 (TAE YOUNG IND.CORP " + sPRPER + " )";
                                    Row["TMREPORT18"] = "$" + sTYCSAVE;

                                    Row["TMREPORTNM19"] = "19. B / L 발급일자 (B/L DATE) ";
                                    Row["TMREPORT19"] = "";

                                    Row["TMREPORTNM20"] = "20. 환          율 (EXCHANGE RATE) ";
                                    Row["TMREPORT20"] = "";

                                    Row["TMREPORTNM21"] = "21. 선  박   회 사 (SHIPPING COMPANY)                   :";
                                    Row["TMREPORT21"] = dt.Rows[i]["IHBRANCHNM"].ToString();
                                }

                            }
                            else // 농협이면서 체선료일 경우
                            {
                                Row["TMREPORTNM17"] = "17. 화  주  배분액 (CONSIGNEE PORTION " + sPRPER1 + " )";
                                Row["TMREPORT17"] = "$" + sSOSOKSAVE;

                                Row["TMREPORTNM18"] = "18. 하역회사배분액 (TAE YOUNG IND.CORP " + sPRPER + " )";
                                Row["TMREPORT18"] = "$" + sTYCSAVE;

                                Row["TMREPORTNM19"] = "19. B / L 발급일자 (B/L DATE) ";
                                Row["TMREPORT19"] = "";

                                Row["TMREPORTNM20"] = "20. 환          율 (EXCHANGE RATE) ";
                                Row["TMREPORT20"] = "";

                                Row["TMREPORTNM21"] = "21. 선  박   회 사 (SHIPPING COMPANY)                   :";
                                Row["TMREPORT21"] = dt.Rows[i]["IHBRANCHNM"].ToString();
                            }
                        }
                        else // 농협이 아닐 경우(사료협회, 전분당)
                        {
                            //사협일경우
                            Row["TMREPORTNM17"] = "17. 화  주  배분액 (CONSIGNEE PORTION " + sPRPER1 + " )";
                            Row["TMREPORT17"] = "$" + sSOSOKSAVE;

                            Row["TMREPORTNM18"] = "18. 하역회사배분액 (TAE YOUNG IND.CORP " + sPRPER + " )";
                            Row["TMREPORT18"] = "$" + sTYCSAVE;

                            Row["TMREPORTNM19"] = "19. B / L 발급일자 (B/L DATE) ";
                            Row["TMREPORT19"] = "";

                            Row["TMREPORTNM20"] = "20. 환          율 (EXCHANGE RATE) ";
                            Row["TMREPORT20"] = "";

                            Row["TMREPORTNM21"] = "21. 선  박   회 사 (SHIPPING COMPANY)                   :";
                            Row["TMREPORT21"] = dt.Rows[i]["IHBRANCHNM"].ToString();
                        }
                    }
                    else
                    {
                        if (dt.Rows[i]["IHSOSOK"].ToString() == "2")
                        {
                            //농협일경우
                            if (dt.Rows[i]["LTGUBUN"].ToString() == "1")  //조출료 일때만,,
                            {
                                /***************************************************************
                                 * 200942항차부터 
                                 * 하역사 배분액(50%) : 조출총액 * 50%
                                 * 화주사 배분율      : (조출총액 * 50%) * (B/L량 / 총B/L량)
                                 ***************************************************************/
                                if (double.Parse(dt.Rows[i]["LTHANGCHA"].ToString()) >= 200942)
                                {
                                    if (double.Parse(dt.Rows[i]["LTHANGCHA"].ToString()) >= 201201)
                                    {
                                        string sHalf_LTTOTSAVE = string.Empty;
                                        double dHalf_LTTOTSAVE = 0;

                                        sHalf_LTTOTSAVE = String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()) * 0.5);

                                        dHalf_LTTOTSAVE = double.Parse(sHalf_LTTOTSAVE.ToString());

                                        // 2010.04.13일 추가 로직 시작
                                        sHalf_LTTOTSAVE = String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTYCSAVE"].ToString()));
                                        // 2010.04.13일 추가 로직 종료

                                        double dBEBUNAMT = 0;
                                        double dBEBUNTOT = 0;

                                        double dOKYUNG = 0;
                                        double dNH = 0;

                                        double dOKYUNG_AMT = 0;
                                        


                                        this.DbConnector.CommandClear();
                                        this.DbConnector.Attach("TY_P_US_92EG2777", dt.Rows[i]["LTHANGCHA"].ToString(),
                                                                                    dt.Rows[i]["LTGOKJONG"].ToString()
                                                                                    );

                                        dt1 = this.DbConnector.ExecuteDataTable();

                                        if (dt.Rows.Count > 0)
                                        {
                                            for (j = 0; j < dt1.Rows.Count; j++)
                                            {
                                                switch (dt1.Rows[j]["VNSOSOK"].ToString())
                                                {
                                                    case "0":

                                                        dOKYUNG = double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString());

                                                        Row["TMREPORTNM18"] = "       * 오경 배분액(" + String.Format("{0,9:N2}", double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString())) + " M/T) ";

                                                        dBEBUNAMT = dHalf_LTTOTSAVE * double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString()) / double.Parse(dt1.Rows[j]["SUMQTY"].ToString());
                                                        dBEBUNAMT = dBEBUNAMT * 1000;
                                                        dBEBUNAMT = dBEBUNAMT - (dBEBUNAMT % 1);
                                                        dBEBUNAMT = dBEBUNAMT / 1000;
                                                        dBEBUNTOT = dBEBUNTOT + dBEBUNAMT;
                                                        //String.Format("{0,9:N3}" ,double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()));

                                                        Row["TMREPORT18"] = "$" + String.Format("{0,9:N2}", dBEBUNAMT);

                                                        dOKYUNG_AMT = double.Parse(String.Format("{0,9:N2}", dBEBUNAMT));

                                                        break;

                                                    case "2":

                                                        dNH = double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString());

                                                        Row["TMREPORTNM17"] = "       * 농협 배분액(" + String.Format("{0,9:N2}", double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString())) + " M/T) ";
                                                        dBEBUNAMT = dHalf_LTTOTSAVE * double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString()) / double.Parse(dt1.Rows[j]["SUMQTY"].ToString());
                                                        dBEBUNAMT = dBEBUNAMT * 1000;
                                                        dBEBUNAMT = dBEBUNAMT - (dBEBUNAMT % 1);
                                                        dBEBUNAMT = dBEBUNAMT / 1000;
                                                        dBEBUNTOT = dBEBUNTOT + dBEBUNAMT;
                                                        if (dBEBUNTOT != dHalf_LTTOTSAVE)
                                                        {
                                                            dBEBUNAMT = dBEBUNAMT + dHalf_LTTOTSAVE - dBEBUNTOT;
                                                        }
                                                        Row["TMREPORT17"] = "$" + String.Format("{0,9:N2}", dBEBUNAMT);
                                                        break;
                                                }
                                            }
                                        }

                                        Row["TMREPORTNM19"] = "17. 하역사  배분율 (50%)";
                                        Row["TMREPORT19"] = "$" + sHalf_LTTOTSAVE.ToString();

                                        Row["TMREPORTNM20"] = "       * 농협 배분액(" + String.Format("{0,9:N2}", dNH) + " M/T) ";
                                        Row["TMREPORT20"] = "$" + String.Format("{0,9:N2}", double.Parse(Get_Numeric(sHalf_LTTOTSAVE.ToString())) - dOKYUNG_AMT);

                                        Row["TMREPORTNM21"] = "       * 오경 배분액(" + String.Format("{0,9:N2}", dOKYUNG) + " M/T) ";
                                        Row["TMREPORT21"] = "$" + String.Format("{0,9:N2}", dOKYUNG_AMT);

                                        Row["TMREPORTNM22"] = "18. 환          율 (EXCHANGE RATE)";
                                        Row["TMREPORT22"] = "";

                                        Row["TMREPORTNM23"] = "19. 선  박   회 사 (SHIPPING COMPANY)";
                                        Row["TMREPORT23"] = dt.Rows[i]["IHBRANCHNM"].ToString();
                                    }
                                    else
                                    {
                                        string sHalf_LTTOTSAVE = string.Empty;
                                        double dHalf_LTTOTSAVE = 0;

                                        sHalf_LTTOTSAVE = String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()) * 0.5);

                                        dHalf_LTTOTSAVE = double.Parse(sHalf_LTTOTSAVE.ToString());

                                        // 2010.04.13일 추가 로직 시작
                                        sHalf_LTTOTSAVE = String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTYCSAVE"].ToString()));
                                        // 2010.04.13일 추가 로직 종료

                                        
                                        double dBEBUNAMT = 0;
                                        double dBEBUNTOT = 0;

                                        this.DbConnector.CommandClear();
                                        this.DbConnector.Attach("TY_P_US_92EG2777", dt.Rows[i]["LTHANGCHA"].ToString(),
                                                                                    dt.Rows[i]["LTGOKJONG"].ToString()
                                                                                    );

                                        dt1 = this.DbConnector.ExecuteDataTable();

                                        if (dt.Rows.Count > 0)
                                        {
                                            for (j = 0; j < dt1.Rows.Count; j++)
                                            {
                                                switch (dt1.Rows[j]["VNSOSOK"].ToString())
                                                {
                                                    case "0":
                                                        Row["TMREPORTNM19"] = "       * 오경 배분액(" + String.Format("{0,9:N2}", double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString())) + " M/T) ";

                                                        dBEBUNAMT = dHalf_LTTOTSAVE * double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString()) / double.Parse(dt1.Rows[j]["SUMQTY"].ToString());
                                                        dBEBUNAMT = dBEBUNAMT * 1000;
                                                        dBEBUNAMT = dBEBUNAMT - (dBEBUNAMT % 1);
                                                        dBEBUNAMT = dBEBUNAMT / 1000;
                                                        dBEBUNTOT = dBEBUNTOT + dBEBUNAMT;
                                                        //String.Format("{0,9:N3}" ,double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()));

                                                        Row["TMREPORT19"] = "$" + String.Format("{0,9:N2}", dBEBUNAMT);
                                                        break;
                                                    case "1":
                                                        Row["TMREPORTNM18"] = "       * 합천 배분액(" + String.Format("{0,9:N2}", double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString())) + " M/T) ";
                                                        dBEBUNAMT = dHalf_LTTOTSAVE * double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString()) / double.Parse(dt1.Rows[j]["SUMQTY"].ToString());
                                                        dBEBUNAMT = dBEBUNAMT * 1000;
                                                        dBEBUNAMT = dBEBUNAMT - (dBEBUNAMT % 1);
                                                        dBEBUNAMT = dBEBUNAMT / 1000;
                                                        dBEBUNTOT = dBEBUNTOT + dBEBUNAMT;
                                                        Row["TMREPORT18"] = "$" + String.Format("{0,9:N2}", dBEBUNAMT);
                                                        break;
                                                    case "2":
                                                        Row["TMREPORTNM17"] = "       * 농협 배분액(" + String.Format("{0,9:N2}", double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString())) + " M/T) ";
                                                        dBEBUNAMT = dHalf_LTTOTSAVE * double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString()) / double.Parse(dt1.Rows[j]["SUMQTY"].ToString());
                                                        dBEBUNAMT = dBEBUNAMT * 1000;
                                                        dBEBUNAMT = dBEBUNAMT - (dBEBUNAMT % 1);
                                                        dBEBUNAMT = dBEBUNAMT / 1000;
                                                        dBEBUNTOT = dBEBUNTOT + dBEBUNAMT;
                                                        if (dBEBUNTOT != dHalf_LTTOTSAVE)
                                                        {
                                                            dBEBUNAMT = dBEBUNAMT + dHalf_LTTOTSAVE - dBEBUNTOT;
                                                        }
                                                        Row["TMREPORT17"] = "$" + String.Format("{0,9:N2}", dBEBUNAMT);
                                                        break;
                                                }
                                            }
                                        }

                                        Row["TMREPORTNM20"] = "17. 하역사  배분율 (50%)";
                                        Row["TMREPORT20"] = "$" + sHalf_LTTOTSAVE.ToString();

                                        Row["TMREPORTNM21"] = "18. 환          율 (EXCHANGE RATE)";
                                        Row["TMREPORT21"] = "";

                                        Row["TMREPORTNM22"] = "19. 선  박   회 사 (SHIPPING COMPANY)";
                                        Row["TMREPORT22"] = dt.Rows[i]["IHBRANCHNM"].ToString();
                                    }
                                }
                                else
                                {
                                    double dBEBUNAMT = 0;
                                    double dBEBUNTOT = 0;

                                    this.DbConnector.CommandClear();
                                    this.DbConnector.Attach("TY_P_US_92EG2777", dt.Rows[i]["LTHANGCHA"].ToString(),
                                                                                dt.Rows[i]["LTGOKJONG"].ToString()
                                                                                );

                                    dt1 = this.DbConnector.ExecuteDataTable();

                                    if (dt.Rows.Count > 0)
                                    {
                                        for (j = 0; j < dt1.Rows.Count; j++)
                                        {
                                            switch (dt1.Rows[j]["VNSOSOK"].ToString())
                                            {
                                                case "0":
                                                    Row["TMREPORTNM19"] = "       * 오경 배분액(" + String.Format("{0,9:N2}", double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString())) + " M/T) ";

                                                    dBEBUNAMT = double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()) * double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString()) / double.Parse(dt1.Rows[j]["SUMQTY"].ToString());
                                                    dBEBUNAMT = dBEBUNAMT * 1000;
                                                    dBEBUNAMT = dBEBUNAMT - (dBEBUNAMT % 1);
                                                    dBEBUNAMT = dBEBUNAMT / 1000;
                                                    dBEBUNTOT = dBEBUNTOT + dBEBUNAMT;

                                                    Row["TMREPORT19"] = "$" + String.Format("{0,9:N2}", dBEBUNAMT);
                                                    break;
                                                case "1":
                                                    Row["TMREPORTNM18"] = "       * 합천 배분액(" + String.Format("{0,9:N2}", double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString())) + " M/T) ";
                                                    dBEBUNAMT = double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()) * double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString()) / double.Parse(dt1.Rows[j]["SUMQTY"].ToString());
                                                    dBEBUNAMT = dBEBUNAMT * 1000;
                                                    dBEBUNAMT = dBEBUNAMT - (dBEBUNAMT % 1);
                                                    dBEBUNAMT = dBEBUNAMT / 1000;
                                                    dBEBUNTOT = dBEBUNTOT + dBEBUNAMT;
                                                    Row["TMREPORT18"] = "$" + String.Format("{0,9:N2}", dBEBUNAMT);
                                                    break;
                                                case "2":
                                                    Row["TMREPORTNM17"] = "       * 농협 배분액(" + String.Format("{0,9:N2}", double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString())) + " M/T) ";
                                                    dBEBUNAMT = double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()) * double.Parse(dt1.Rows[j]["JGBEJNQTY"].ToString()) / double.Parse(dt1.Rows[j]["SUMQTY"].ToString());
                                                    dBEBUNAMT = dBEBUNAMT * 1000;
                                                    dBEBUNAMT = dBEBUNAMT - (dBEBUNAMT % 1);
                                                    dBEBUNAMT = dBEBUNAMT / 1000;
                                                    if ((dBEBUNTOT + dBEBUNAMT) != double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()))
                                                    {
                                                        dBEBUNAMT = double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()) - dBEBUNTOT;
                                                    }
                                                    Row["TMREPORT17"] = "$" + String.Format("{0,9:N2}", dBEBUNAMT);
                                                    break;
                                            }
                                        }
                                    }

                                    Row["TMREPORTNM20"] = "17. 환          율 (EXCHANGE RATE)";
                                    Row["TMREPORT20"] = "";

                                    Row["TMREPORTNM21"] = "18. 선  박   회 사 (SHIPPING COMPANY)";
                                    Row["TMREPORT21"] = dt.Rows[i]["IHBRANCHNM"].ToString();
                                }
                            }
                            else
                            {
                                Row["TMREPORTNM17"] = "17. 화  주  배분액 (CONSIGNEE PORTION " + sPRPER1 + " )";
                                Row["TMREPORT17"] = "$" + sSOSOKSAVE;

                                Row["TMREPORTNM18"] = "18. 하역회사배분액 (TAE YOUNG IND.CORP " + sPRPER + " )";
                                Row["TMREPORT18"] = "$" + sTYCSAVE;

                                Row["TMREPORTNM19"] = "19. B / L 발급일자 (B/L DATE) ";
                                Row["TMREPORT19"] = "";

                                Row["TMREPORTNM20"] = "20. 환          율 (EXCHANGE RATE) ";
                                Row["TMREPORT20"] = "";

                                Row["TMREPORTNM21"] = "21. 선  박   회 사 (SHIPPING COMPANY)                   :";
                                Row["TMREPORT21"] = dt.Rows[i]["IHBRANCHNM"].ToString();
                            }
                        }
                        else // 농협이 아닐 경우(사료협회, 전분당)
                        {
                            //사협일경우
                            Row["TMREPORTNM17"] = "17. 화  주  배분액 (CONSIGNEE PORTION " + sPRPER1 + " )";
                            Row["TMREPORT17"] = "$" + sSOSOKSAVE;

                            Row["TMREPORTNM18"] = "18. 하역회사배분액 (TAE YOUNG IND.CORP " + sPRPER + " )";
                            Row["TMREPORT18"] = "$" + sTYCSAVE;

                            Row["TMREPORTNM19"] = "19. B / L 발급일자 (B/L DATE) ";
                            Row["TMREPORT19"] = "";

                            Row["TMREPORTNM20"] = "20. 환          율 (EXCHANGE RATE) ";
                            Row["TMREPORT20"] = "";

                            Row["TMREPORTNM21"] = "21. 선  박   회 사 (SHIPPING COMPANY)                   :";
                            Row["TMREPORT21"] = dt.Rows[i]["IHBRANCHNM"].ToString();
                        }
                    }
                }

                Table.Rows.Add(Row);

            }

            return Table;
        }
        #endregion


        #region Description : 달 체크
        private string UP_MonthCheck(string sMonth1)
        {
            string sMonth = "";

            switch (sMonth1)
            {
                case "01":
                    sMonth = "JAN";
                    break;
                case "02":
                    sMonth = "FEB";
                    break;
                case "03":
                    sMonth = "MAR";
                    break;
                case "04":
                    sMonth = "APR";
                    break;
                case "05":
                    sMonth = "MAY";
                    break;
                case "06":
                    sMonth = "JUN";
                    break;
                case "07":
                    sMonth = "JUL";
                    break;
                case "08":
                    sMonth = "AUG";
                    break;
                case "09":
                    sMonth = "SEP";
                    break;
                case "10":
                    sMonth = "OCT";
                    break;
                case "11":
                    sMonth = "NOV";
                    break;
                case "12":
                    sMonth = "DEC";
                    break;
            }

            return sMonth;
        }
        #endregion
    }
}
