using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

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
    public partial class TYUSME027P : TYBase
    {
        #region Descriptino : 페이지 로드
        public TYUSME027P()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYUSME027P_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.CBH01_STHANGCHA.CodeText);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_92IBE793", this.CBH01_STHANGCHA.GetValue().ToString(),
                                                        this.CBH01_EDHANGCHA.GetValue().ToString(),
                                                        this.CBH01_GGOKJONG.GetValue().ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                DataTable retDt = UP_ReportConvert(dt);

                SectionReport rpt = new TYUSME027R();
                // 세로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Portrait;

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
            int i = 0;
            int j = 0;
            int m = 0;

            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();



            string sMonth = string.Empty;

            DataTable Table = new DataTable();

            DataRow Row;

            Table.Columns.Add("TMREPORT1", typeof(System.String));
            Table.Columns.Add("TMREPORTNM2", typeof(System.String));
            Table.Columns.Add("TMREPORT2", typeof(System.String));
            Table.Columns.Add("TMREPORTNM3", typeof(System.String));
            Table.Columns.Add("TMREPORT3", typeof(System.String));
            Table.Columns.Add("TMREPORTNM5", typeof(System.String));
            Table.Columns.Add("TMREPORT5", typeof(System.String));
            Table.Columns.Add("TMREPORTNM6", typeof(System.String));
            Table.Columns.Add("TMREPORT6", typeof(System.String));
            Table.Columns.Add("TMREPORTNM7", typeof(System.String));
            Table.Columns.Add("TMREPORT7", typeof(System.String));
            Table.Columns.Add("TMREPORTNM8", typeof(System.String));
            Table.Columns.Add("TMREPORT8", typeof(System.String));

            Table.Columns.Add("TMREPORT9", typeof(System.String));
            Table.Columns.Add("TMREPORT10", typeof(System.String));
            Table.Columns.Add("TMREPORT11", typeof(System.String));
            Table.Columns.Add("TMREPORT12", typeof(System.String));
            Table.Columns.Add("TMREPORT13", typeof(System.String));
            Table.Columns.Add("TMREPORT14", typeof(System.String));
            Table.Columns.Add("TMREPORT15", typeof(System.String));
            Table.Columns.Add("TMREPORT16", typeof(System.String));
            Table.Columns.Add("TMREPORT17", typeof(System.String));
            Table.Columns.Add("TMREPORT18", typeof(System.String));
            Table.Columns.Add("TMREPORT19", typeof(System.String));
            Table.Columns.Add("TMREPORT20", typeof(System.String));
            Table.Columns.Add("TMREPORT21", typeof(System.String));
            Table.Columns.Add("TMREPORT22", typeof(System.String));
            Table.Columns.Add("TMREPORT23", typeof(System.String));
            Table.Columns.Add("TMREPORT24", typeof(System.String));
            Table.Columns.Add("TMREPORT25", typeof(System.String));
            Table.Columns.Add("TMREPORT26", typeof(System.String));
            Table.Columns.Add("TMREPORT27", typeof(System.String));

            string sLDDATE = string.Empty;
            string sAgo_LDDATE = string.Empty;


            for (i = 0; i < dt.Rows.Count; i++)
            {
                string sDaytime = "";

                int iTotDayTimeDD = 0;
                int iTotDayTimeHH = 0;
                int iTotDayTimeMM = 0;

                int iTotExDayTimeDD = 0;
                int iTotExDayTimeHH = 0;
                int iTotExDayTimeMM = 0;

                int iTotUSDayTimeDD = 0;
                int iTotUSDayTimeHH = 0;
                int iTotUSDayTimeMM = 0;

                //조출, 체선료 일일 제외 내역파일
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92IBY796", dt.Rows[i]["LTHANGCHA"].ToString(),
                                                            dt.Rows[i]["LTGOKJONG"].ToString()
                                                            );

                dt1 = this.DbConnector.ExecuteDataTable();

                for (j = 0; j < dt1.Rows.Count; j++)
                {
                    Row = Table.NewRow();

                    Row["TMREPORT1"] = dt.Rows[i]["LTHANGCHANM"].ToString();

                    Row["TMREPORTNM2"] = "CARGO";
                    Row["TMREPORT2"] = String.Format("{0,9:N3}", double.Parse(dt.Rows[i]["IHBLQTY"].ToString())) +
                                         "M/T of  " +
                                         dt.Rows[i]["LTGOKJONGNM"].ToString();

                    sMonth = UP_MonthCheck(dt.Rows[i]["IHIPHANG"].ToString().Substring(4, 2));
                    Row["TMREPORTNM3"] = "ARRIVED AT ULSAN";
                    Row["TMREPORT3"] = dt.Rows[i]["IHIPHTIM"].ToString().Substring(0, 2) + ":" +
                                         dt.Rows[i]["IHIPHTIM"].ToString().Substring(2, 2) + ",     " +
                                         dt.Rows[i]["IHIPHANG"].ToString().Substring(6, 2) + "Th  of  " +
                                         sMonth + ".,    " +
                                         dt.Rows[i]["IHIPHANG"].ToString().Substring(0, 4);

                    sMonth = UP_MonthCheck(dt.Rows[i]["LTTENDATE"].ToString().Substring(4, 2));
                    Row["TMREPORTNM5"] = "NOTICE OF READINESS TENDERED";
                    Row["TMREPORT5"] = dt.Rows[i]["LTTENTIME"].ToString().Substring(0, 2) + ":" +
                                         dt.Rows[i]["LTTENTIME"].ToString().Substring(2, 2) + ",     " +
                                         dt.Rows[i]["LTTENDATE"].ToString().Substring(6, 2) + "Th  of  " +
                                         sMonth + ".,    " +
                                         dt.Rows[i]["LTTENDATE"].ToString().Substring(0, 4);

                    sMonth = UP_MonthCheck(dt.Rows[i]["LTACCDATE"].ToString().Substring(4, 2));
                    Row["TMREPORTNM6"] = "NOTICE OF READINESS ACCEPTED";
                    Row["TMREPORT6"] = dt.Rows[i]["LTACCTIME"].ToString().Substring(0, 2) + ":" +
                                         dt.Rows[i]["LTACCTIME"].ToString().Substring(2, 2) + ",     " +
                                         dt.Rows[i]["LTACCDATE"].ToString().Substring(6, 2) + "Th  of  " +
                                         sMonth + ".,    " +
                                         dt.Rows[i]["LTACCDATE"].ToString().Substring(0, 4);

                    sMonth = UP_MonthCheck(dt.Rows[i]["LTSTDATE"].ToString().Substring(4, 2));
                    Row["TMREPORTNM7"] = "LAYTIME COMMENCED TO COUNT";
                    Row["TMREPORT7"] = dt.Rows[i]["LTSTTIME"].ToString().Substring(0, 2) + ":" +
                                         dt.Rows[i]["LTSTTIME"].ToString().Substring(2, 2) + ",     " +
                                         dt.Rows[i]["LTSTDATE"].ToString().Substring(6, 2) + "Th  of  " +
                                         sMonth + ".,    " +
                                         dt.Rows[i]["LTSTDATE"].ToString().Substring(0, 4);

                    sMonth = UP_MonthCheck(dt.Rows[i]["IHJAKENDAT"].ToString().Substring(4, 2));
                    Row["TMREPORTNM8"] = "COMPLETED DISCHARGING";
                    Row["TMREPORT8"] = dt.Rows[i]["IHJAKENTIM"].ToString().Substring(0, 2) + ":" +
                                         dt.Rows[i]["IHJAKENTIM"].ToString().Substring(2, 2) + ",     " +
                                         dt.Rows[i]["IHJAKENDAT"].ToString().Substring(6, 2) + "Th  of  " +
                                         sMonth + ".,    " +
                                         dt.Rows[i]["IHJAKENDAT"].ToString().Substring(0, 4);


                    if (sAgo_LDDATE != dt1.Rows[j]["LDDATE"].ToString())
                    {
                        sAgo_LDDATE = dt1.Rows[j]["LDDATE"].ToString();

                        sMonth = UP_MonthCheck(dt1.Rows[j]["LDDATE"].ToString().Substring(4, 2));
                        Row["TMREPORT9"] = dt1.Rows[j]["LDDATE"].ToString().Substring(6, 2) + "Th/" +
                                           sMonth + ". (" +
                                           dt1.Rows[j]["LDWEEKNM"].ToString() + ")";

                        Row["TMREPORT10"] = dt1.Rows[j]["LDSTTIME"].ToString().Substring(0, 2) + ":" +
                                            dt1.Rows[j]["LDSTTIME"].ToString().Substring(2, 2) + "-" +
                                            dt1.Rows[j]["LDEDTIME"].ToString().Substring(0, 2) + ":" +
                                            dt1.Rows[j]["LDEDTIME"].ToString().Substring(2, 2);


                        Row["TMREPORT11"] = dt1.Rows[j]["LDNEXSTTM"].ToString().Substring(0, 2) + ":" +
                                            dt1.Rows[j]["LDNEXSTTM"].ToString().Substring(2, 2) + "-" +
                                            dt1.Rows[j]["LDNEXEDTM"].ToString().Substring(0, 2) + ":" +
                                            dt1.Rows[j]["LDNEXEDTM"].ToString().Substring(2, 2);

                        Row["TMREPORT12"] = dt1.Rows[j]["LDTOTTIME"].ToString().Substring(0, 2) + ":" +
                                            dt1.Rows[j]["LDTOTTIME"].ToString().Substring(2, 2);

                        Row["TMREPORT13"] = dt1.Rows[j]["LDESAYU"].ToString();

                        Row["TMREPORT14"] = dt1.Rows[j]["LDDAY"].ToString();

                        Row["TMREPORT15"] = "(" + dt1.Rows[j]["LDNEXCOTM"].ToString().Substring(0, 2) + ":" +
                                                  dt1.Rows[j]["LDNEXCOTM"].ToString().Substring(2, 2) + ")";


                        string sUSED_TIME = string.Empty;

                        sUSED_TIME = "";

                        //조출, 체선료 일일 제외 내역파일
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_92IEE798", dt.Rows[i]["LTHANGCHA"].ToString(),
                                                                    dt.Rows[i]["LTGOKJONG"].ToString(),
                                                                    dt1.Rows[j]["LDDATE"].ToString()
                                                                    );

                        dt2 = this.DbConnector.ExecuteDataTable();

                        if (dt2.Rows.Count > 0)
                        {
                            for (m = 0; m < dt2.Rows.Count; m++)
                            {
                                if (m == 0)
                                {
                                    if (dt1.Rows[j]["LDSTTIME"].ToString() == dt2.Rows[m]["LDNEXSTTM"].ToString())
                                    {
                                        if (int.Parse(Get_Numeric(dt1.Rows[j]["LDTOTTIME"].ToString())) == 0)
                                        {
                                            sUSED_TIME = "";
                                        }
                                        else
                                        {
                                            if (m + 1 == dt2.Rows.Count)
                                            {
                                                sUSED_TIME = dt2.Rows[m]["LDNEXEDTM"].ToString().Substring(0, 2) + ":" +
                                                             dt2.Rows[m]["LDNEXEDTM"].ToString().Substring(2, 2) + "-" +
                                                             dt1.Rows[j]["LDEDTIME"].ToString().Substring(0, 2) + ":" +
                                                             dt1.Rows[j]["LDEDTIME"].ToString().Substring(2, 2);
                                            }
                                            else
                                            {
                                                sUSED_TIME = dt2.Rows[m]["LDNEXEDTM"].ToString().Substring(0, 2) + ":" +
                                                             dt2.Rows[m]["LDNEXEDTM"].ToString().Substring(2, 2) + "-" +
                                                             dt2.Rows[m + 1]["LDNEXSTTM"].ToString().Substring(0, 2) + ":" +
                                                             dt2.Rows[m + 1]["LDNEXSTTM"].ToString().Substring(2, 2);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (m + 1 == dt2.Rows.Count)
                                        {
                                            sUSED_TIME = dt1.Rows[j]["LDSTTIME"].ToString().Substring(0, 2) + ":" +
                                                         dt1.Rows[j]["LDSTTIME"].ToString().Substring(2, 2) + "-" +
                                                         dt2.Rows[m]["LDNEXSTTM"].ToString().Substring(0, 2) + ":" +
                                                         dt2.Rows[m]["LDNEXSTTM"].ToString().Substring(2, 2);

                                            sUSED_TIME = sUSED_TIME.ToString() + ", " + dt2.Rows[m]["LDNEXEDTM"].ToString().Substring(0, 2) + ":" +
                                                                                        dt2.Rows[m]["LDNEXEDTM"].ToString().Substring(2, 2) + "-" +
                                                                                        dt1.Rows[j]["LDEDTIME"].ToString().Substring(0, 2) + ":" +
                                                                                        dt1.Rows[j]["LDEDTIME"].ToString().Substring(2, 2);
                                        }
                                        else
                                        {
                                            sUSED_TIME = dt1.Rows[j]["LDSTTIME"].ToString().Substring(0, 2) + ":" +
                                                         dt1.Rows[j]["LDSTTIME"].ToString().Substring(2, 2) + "-" +
                                                         dt2.Rows[m]["LDNEXSTTM"].ToString().Substring(0, 2) + ":" +
                                                         dt2.Rows[m]["LDNEXSTTM"].ToString().Substring(2, 2);
                                        }
                                    }
                                }
                                else
                                {
                                    if (m + 1 == dt2.Rows.Count)
                                    {
                                        sUSED_TIME = sUSED_TIME.ToString() + ", " + dt2.Rows[m]["LDNEXEDTM"].ToString().Substring(0, 2) + ":" +
                                                                                    dt2.Rows[m]["LDNEXEDTM"].ToString().Substring(2, 2) + "-" +
                                                                                    dt1.Rows[j]["LDEDTIME"].ToString().Substring(0, 2) + ":" +
                                                                                    dt1.Rows[j]["LDEDTIME"].ToString().Substring(2, 2);
                                    }
                                    else
                                    {
                                        sUSED_TIME = sUSED_TIME.ToString() + ", " + dt2.Rows[m]["LDNEXEDTM"].ToString().Substring(0, 2) + ":" +
                                                                                    dt2.Rows[m]["LDNEXEDTM"].ToString().Substring(2, 2) + "-" +
                                                                                    dt2.Rows[m + 1]["LDNEXSTTM"].ToString().Substring(0, 2) + ":" +
                                                                                    dt2.Rows[m + 1]["LDNEXSTTM"].ToString().Substring(2, 2);
                                    }
                                }
                            }
                            Row["TMREPORT26"] = sUSED_TIME.ToString();
                        }
                        else
                        {
                            Row["TMREPORT26"] = dt1.Rows[j]["LDSTTIME"].ToString().Substring(0, 2) + ":" +
                                                dt1.Rows[j]["LDSTTIME"].ToString().Substring(2, 2) + "-" +
                                                dt1.Rows[j]["LDEDTIME"].ToString().Substring(0, 2) + ":" +
                                                dt1.Rows[j]["LDEDTIME"].ToString().Substring(2, 2);
                        }

                        //TOTAL: L/T USED 시간계산
                        sDaytime = UP_TimeCheck(dt1.Rows[j]["LDSTTIME"].ToString(),
                                                dt1.Rows[j]["LDEDTIME"].ToString());

                        iTotDayTimeHH = iTotDayTimeHH + Convert.ToInt16(sDaytime.Substring(0, 2));
                        iTotDayTimeMM = iTotDayTimeMM + Convert.ToInt16(sDaytime.Substring(2, 2));

                        if (iTotDayTimeMM > 59)
                        {
                            iTotDayTimeHH = iTotDayTimeHH + 1;
                            iTotDayTimeMM = iTotDayTimeMM - 60;
                        }

                        if (iTotDayTimeHH > 23)
                        {
                            iTotDayTimeDD = iTotDayTimeDD + 1;
                            iTotDayTimeHH = iTotDayTimeHH - 24;
                        }

                        Row["TMREPORT16"] = Set_Fill2(Convert.ToString(iTotDayTimeDD)) + "D-" +
                                            Set_Fill2(Convert.ToString(iTotDayTimeHH)) + "H-" +
                                            Set_Fill2(Convert.ToString(iTotDayTimeMM)) + "M";

                        //TOTAL : USED TIME 시간계산
                        sDaytime = dt1.Rows[j]["LDTOTTIME"].ToString();

                        iTotUSDayTimeHH = iTotUSDayTimeHH + Convert.ToInt16(sDaytime.Substring(0, 2));
                        iTotUSDayTimeMM = iTotUSDayTimeMM + Convert.ToInt16(sDaytime.Substring(2, 2));

                        if (iTotUSDayTimeMM > 59)
                        {
                            iTotUSDayTimeHH = iTotUSDayTimeHH + 1;
                            iTotUSDayTimeMM = iTotUSDayTimeMM - 60;
                        }

                        if (iTotUSDayTimeHH > 23)
                        {
                            iTotUSDayTimeDD = iTotUSDayTimeDD + 1;
                            iTotUSDayTimeHH = iTotUSDayTimeHH - 24;
                        }
                        Row["TMREPORT18"] = Set_Fill2(Convert.ToString(iTotUSDayTimeDD)) + "D-" +
                                            Set_Fill2(Convert.ToString(iTotUSDayTimeHH)) + "H-" +
                                            Set_Fill2(Convert.ToString(iTotUSDayTimeMM)) + "M";

                        Row["TMREPORT19"] = dt.Rows[i]["LTALLOW"].ToString().Substring(0, 2) + "D-" +
                                            dt.Rows[i]["LTALLOW"].ToString().Substring(2, 2) + "H-" +
                                            dt.Rows[i]["LTALLOW"].ToString().Substring(4, 2) + "M";

                        Row["TMREPORT20"] = dt.Rows[i]["LTUSED"].ToString().Substring(0, 2) + "D-" +
                                            dt.Rows[i]["LTUSED"].ToString().Substring(2, 2) + "H-" +
                                            dt.Rows[i]["LTUSED"].ToString().Substring(4, 2) + "M";

                        Row["TMREPORT21"] = dt.Rows[i]["LTSAVE"].ToString().Substring(0, 2) + "D-" +
                                            dt.Rows[i]["LTSAVE"].ToString().Substring(2, 2) + "H-" +
                                            dt.Rows[i]["LTSAVE"].ToString().Substring(4, 2) + "M";
                    }
                    else
                    {
                        sMonth = UP_MonthCheck(dt1.Rows[i]["LDDATE"].ToString().Substring(4, 2));
                        Row["TMREPORT9"] = "";

                        Row["TMREPORT10"] = "";

                        Row["TMREPORT11"] = dt1.Rows[j]["LDNEXSTTM"].ToString().Substring(0, 2) + ":" +
                                            dt1.Rows[j]["LDNEXSTTM"].ToString().Substring(2, 2) + "-" +
                                            dt1.Rows[j]["LDNEXEDTM"].ToString().Substring(0, 2) + ":" +
                                            dt1.Rows[j]["LDNEXEDTM"].ToString().Substring(2, 2);

                        Row["TMREPORT12"] = "";

                        Row["TMREPORT13"] = dt1.Rows[j]["LDESAYU"].ToString();

                        Row["TMREPORT14"] = "";

                        Row["TMREPORT15"] = "(" + dt1.Rows[j]["LDNEXCOTM"].ToString().Substring(0, 2) + ":" +
                                                  dt1.Rows[j]["LDNEXCOTM"].ToString().Substring(2, 2) + ")";

                        Row["TMREPORT16"] = Table.Rows[j - 1]["TMREPORT16"].ToString();

                        Row["TMREPORT18"] = Table.Rows[j - 1]["TMREPORT18"].ToString();

                        Row["TMREPORT19"] = Table.Rows[j - 1]["TMREPORT19"].ToString();

                        Row["TMREPORT20"] = Table.Rows[j - 1]["TMREPORT20"].ToString();

                        Row["TMREPORT21"] = Table.Rows[j - 1]["TMREPORT21"].ToString();
                    }

                    //TOTAL L/T EXCEPTED 시간계산
                    sDaytime = Set_Fill4(dt1.Rows[j]["LDNEXCOTM"].ToString());

                    iTotExDayTimeHH = iTotExDayTimeHH + Convert.ToInt16(sDaytime.Substring(0, 2));
                    iTotExDayTimeMM = iTotExDayTimeMM + Convert.ToInt16(sDaytime.Substring(2, 2));

                    if (iTotExDayTimeMM > 59)
                    {
                        iTotExDayTimeHH = iTotExDayTimeHH + 1;
                        iTotExDayTimeMM = iTotExDayTimeMM - 60;
                    }

                    if (iTotExDayTimeHH > 23)
                    {
                        iTotExDayTimeDD = iTotExDayTimeDD + 1;
                        iTotExDayTimeHH = iTotExDayTimeHH - 24;
                    }

                    Row["TMREPORT17"] = Set_Fill2(Convert.ToString(iTotExDayTimeDD)) + "D-" +
                                        Set_Fill2(Convert.ToString(iTotExDayTimeHH)) + "H-" +
                                        Set_Fill2(Convert.ToString(iTotExDayTimeMM)) + "M";





                    if (dt.Rows[i]["LTGUBUN"].ToString() == "2")
                    {
                        Row["TMREPORT22"] = "DEMURRAGE";
                    }
                    else
                    {
                        Row["TMREPORT22"] = "DESPATCH";
                    }

                    Row["TMREPORT23"] = "$" + String.Format("{0,9:N3}", double.Parse(dt.Rows[i]["LTDAYSAVE"].ToString()));


                    Row["TMREPORT24"] = String.Format("{0,9:N5}", (((Convert.ToDouble(dt.Rows[i]["LTSAVE"].ToString().Substring(4, 2)) / 60) / 24) +
                                        (Convert.ToDouble(dt.Rows[i]["LTSAVE"].ToString().Substring(2, 2)) / 24) +
                                        Convert.ToDouble(dt.Rows[i]["LTSAVE"].ToString().Substring(0, 2))));
                    Row["TMREPORT25"] = "$" + String.Format("{0,9:N2}", double.Parse(dt.Rows[i]["LTTOTSAVE"].ToString()));

                    Row["TMREPORT27"] = "LAYTIME SAVE";

                    Table.Rows.Add(Row);
                }
            }

            return Table;
        }
        #endregion

        #region Description : 시간 체크
        private string UP_TimeCheck(string sSTTime, string sEDTime)
        {
            string sHHMM = "";
            double dParamHH = 0;
            double dParamMM = 0;

            if (sSTTime == "0000" && sEDTime == "0000")
            {
                sHHMM = "0000";
                return sHHMM;
            }
            if (sSTTime == sEDTime)
            {
                sHHMM = "2400";
                return sHHMM;
            }

            dParamMM = Convert.ToDouble(sEDTime.Substring(2, 2)) -
                       Convert.ToDouble(sSTTime.Substring(2, 2));

            if (dParamMM < 0)
            {
                dParamMM = dParamMM + 60;
                dParamHH = (Convert.ToDouble(sEDTime.Substring(0, 2)) - 1) - Convert.ToDouble(sSTTime.Substring(0, 2));
            }
            else
            {
                dParamHH = Convert.ToDouble(sEDTime.Substring(0, 2)) - Convert.ToDouble(sSTTime.Substring(0, 2));
            }

            if (dParamHH < 0)
            {
                dParamHH = dParamHH + 24;
            }

            sHHMM = Set_Fill2(Convert.ToString(dParamHH)) + Set_Fill2(Convert.ToString(dParamMM));

            return sHHMM;
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
