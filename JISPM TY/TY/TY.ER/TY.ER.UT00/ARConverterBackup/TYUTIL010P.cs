using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 월 가열료 집계표 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.07.21 17:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_67LHI894 : 월 가열료 집계표 출력(2008-08 이전)
    ///  TY_P_UT_67LHJ895 : 월 가열료 집계표 출력(2008-08 이후)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  YYYYMM : 기준 년월
    /// </summary>
    public partial class TYUTIL010P : TYBase
    {
        #region Description : 페이지 로드
        public TYUTIL010P()
        {
            InitializeComponent();
        }

        private void TYUTIL010P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_YYYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(this.DTP01_YYYYMM);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {

            int stmm = 0;
            int styy = 0;

            int stdd = 0;
            int eddd = 0;

            int styymm = 0;
            int edyymm = 0;

            if (int.Parse(this.DTP01_YYYYMM.GetString()) > 202001)  // 2020-02월 부터 시작일 종료일 변경 (서태호 과장)
            {
                stdd = 21;
                eddd = 20;
            }
            else
            {
                stdd = 26;
                eddd = 25;
            }

            styymm = int.Parse(this.DTP01_YYYYMM.GetString());
            edyymm = int.Parse(Get_Numeric(this.DTP01_YYYYMM.GetString()));

            styy = int.Parse(Get_Numeric(this.DTP01_YYYYMM.GetString().Substring(0, 4)));
            stmm = int.Parse(Get_Numeric(this.DTP01_YYYYMM.GetString().Substring(4, 2)));

            stmm = stmm - 1;
            if (stmm == 0)
            {
                styy = styy - 1;
                stmm = 12;
            }

            int istyy = int.Parse(Get_Numeric(this.DTP01_YYYYMM.GetString().Substring(0, 4)));
            int istmm = int.Parse(Get_Numeric(this.DTP01_YYYYMM.GetString().Substring(4, 2)));

            string edstyy = this.DTP01_YYYYMM.GetString().Substring(0, 4);
            string edstmm = Set_Fill2(this.DTP01_YYYYMM.GetString().Substring(4, 2));
            string edstdd = "01";

            string tstyymmdd = edstyy + edstmm + edstdd; // 해당월 처리 화주 01일~ 
            string ededdd = System.DateTime.DaysInMonth(istyy, istmm).ToString();//해당월의 마지막 일자 구하기
            string tedyymmdd = edstyy + edstmm + ededdd; // ~ 마지막일 까지

            string wstyymmdd = Convert.ToString(styy) + Set_Fill2(Convert.ToString(stmm)) + Convert.ToString(stdd);// 시작 (26일) 부터 시작 , 2020-02 부터(21일)
            string ssdyy = this.DTP01_YYYYMM.GetString().Substring(0, 4);
            string ssdmm = this.DTP01_YYYYMM.GetString().Substring(4, 2);
            string wedyymmdd = ssdyy + ssdmm + Convert.ToString(eddd); // 종료(25일), 2020-02 부터(20일)

            string sqryy = Convert.ToString(styy);
            string sqrmm = Set_Fill2(Convert.ToString(stmm));
            string sqryymm = Convert.ToString(styy) + Set_Fill2(Convert.ToString(stmm));

            string sSDATE = wstyymmdd.Substring(0, 4).ToString() + " . " + wstyymmdd.Substring(4, 2).ToString() + " . " + wstyymmdd.Substring(6, 2).ToString();
            string sEDATE = wedyymmdd.Substring(0, 4).ToString() + " . " + wedyymmdd.Substring(4, 2).ToString() + " . " + wedyymmdd.Substring(6, 2).ToString();
            string sDATE = "( " + sSDATE + " ~ " + sEDATE + " )";

            string tSDATE = tstyymmdd.Substring(0, 4).ToString() + " . " + tstyymmdd.Substring(4, 2).ToString() + " . " + tstyymmdd.Substring(6, 2).ToString();
            string tEDATE = tedyymmdd.Substring(0, 4).ToString() + " . " + tedyymmdd.Substring(4, 2).ToString() + " . " + tedyymmdd.Substring(6, 2).ToString();
            string tDATE = "( " + tSDATE + " ~ " + tEDATE + " )";

            if (Convert.ToInt32(this.DTP01_YYYYMM.GetString().Substring(0, 6)) >= 202104)
            {
                string sSTDATE = string.Empty;
                string sEDDATE = string.Empty;

                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B4KA8209", this.DTP01_YYYYMM.GetString().Substring(0, 6));

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sSTDATE = dt.Rows[0]["STDATE"].ToString();
                    sEDDATE = dt.Rows[0]["EDDATE"].ToString();

                    sSTDATE = sSTDATE.Substring(0, 4).ToString() + " . " + sSTDATE.Substring(4, 2).ToString() + " . " + sSTDATE.Substring(6, 2).ToString();
                    sEDDATE = sEDDATE.Substring(0, 4).ToString() + " . " + sEDDATE.Substring(4, 2).ToString() + " . " + sEDDATE.Substring(6, 2).ToString();

                    sDATE = "( " + sSTDATE + " ~ " + sEDDATE + " )";
                }

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_67LHJ895", sDATE,
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            wstyymmdd,
                                                            wedyymmdd,
                                                            sDATE,
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6)
                                                            );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    ActiveReport rpt = new TYUTIL010R2();
                    // 가로 출력
                    rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, Convert_DataTable(dt))).ShowDialog();
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                }
            }
            else if (Convert.ToInt32(this.DTP01_YYYYMM.GetString().Substring(0, 6)) >= 200808)
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_67LHJ895", sDATE,
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            wstyymmdd,
                                                            wedyymmdd,
                                                            tDATE,
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6)
                                                            );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    ActiveReport rpt = new TYUTIL010R2();
                    // 가로 출력
                    rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, Convert_DataTable(dt))).ShowDialog();
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                }
            }
            else
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_67LHI894", sDATE,
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            wstyymmdd,
                                                            wedyymmdd,
                                                            tDATE,
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6)
                                                            );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    ActiveReport rpt = new TYUTIL010R1();
                    // 가로 출력
                    rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, Convert_DataTable(dt))).ShowDialog();
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                }
            }
        }
        #endregion

        #region Description : 데이터테이블 변경
        private DataTable Convert_DataTable(DataTable dt)
        {
            DataTable retDt = new DataTable();

            string sNEWCHK = string.Empty;
            string sNEWYYMM = string.Empty;
            string sNEWGUBUN = string.Empty;
            string sNEWHWAJU = string.Empty;
            string sNEWHWAMUL = string.Empty;

            retDt.Columns.Add("DACHKGB", typeof(System.String));
            retDt.Columns.Add("DAYYMM", typeof(System.String));
            retDt.Columns.Add("DAGUBUNNM1", typeof(System.String));
            retDt.Columns.Add("HWAJNM", typeof(System.String));
            retDt.Columns.Add("HWAMULNM", typeof(System.String));
            retDt.Columns.Add("GAELTIME", typeof(System.String));
            retDt.Columns.Add("GAKYQTY", typeof(System.String));
            retDt.Columns.Add("GASTTIME", typeof(System.String));
            retDt.Columns.Add("GASTQTY", typeof(System.String));
            retDt.Columns.Add("GADKQTY", typeof(System.String));
            retDt.Columns.Add("DAHAPAMT", typeof(System.String));
            retDt.Columns.Add("HMDESC1", typeof(System.String));
            retDt.Columns.Add("UTGAELTIME", typeof(System.String));
            retDt.Columns.Add("UTGAKYQTY", typeof(System.String));
            retDt.Columns.Add("UTGASTTIME", typeof(System.String));
            retDt.Columns.Add("UTGASTQTY", typeof(System.String));
            retDt.Columns.Add("UTGADKQTY", typeof(System.String));
            retDt.Columns.Add("UTDAHAPAMT", typeof(System.String));
            retDt.Columns.Add("WKDNEL", typeof(System.String));
            retDt.Columns.Add("DNKYUNG", typeof(System.String));
            retDt.Columns.Add("DNSKSTEAM", typeof(System.String));
            retDt.Columns.Add("DNSTDANGA", typeof(System.String));


            if (Convert.ToInt32(this.DTP01_YYYYMM.GetString().Substring(0, 6)) >= 200808)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 전월 26 ~ 당월 25 --> '1' ,당월 1 ~ 당월 31 --> '2'
                    sNEWCHK = dt.Rows[i]["DACHKGB"].ToString();
                    // 처리년월
                    sNEWYYMM = this.DTP01_YYYYMM.GetString().Substring(0, 6);
                    // 상하단지--> '1' , 송유단지 --> '2'
                    sNEWGUBUN = dt.Rows[i]["GAGUBUN"].ToString();
                    // 화주
                    sNEWHWAJU = dt.Rows[i]["GAHWAJU"].ToString();
                    // 화물
                    sNEWHWAMUL = dt.Rows[i]["GAHWAMUL"].ToString();

                    DataRow row = retDt.NewRow();

                    row["DACHKGB"] = dt.Rows[i]["DACHKGB"].ToString();

                    // 날짜
                    row["DAYYMM"] = dt.Rows[i]["DAYYMM"].ToString();
                    // 상하,송유단지
                    row["DAGUBUNNM1"] = dt.Rows[i]["DAGUBUNNM1"].ToString();

                    #region Description : 태영인더스트리(울산) 제외

                    // 화주
                    row["HWAJNM"] = dt.Rows[i]["HWAJNM"].ToString();
                    // 화물
                    row["HWAMULNM"] = dt.Rows[i]["HWAMULNM"].ToString();
                    // 보일러가동시간
                    row["GAELTIME"] = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["GAELTIME"].ToString())));
                    // 경유
                    row["GAKYQTY"] = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["GAKYQTY"].ToString())));
                    // 스팀사용시간
                    row["GASTTIME"] = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["GASTTIME"].ToString())));
                    // 스팀사용량
                    row["GASTQTY"] = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["GASTQTY"].ToString())));
                    // 등유
                    row["GADKQTY"] = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["GADKQTY"].ToString())));

                    double Hapelamt = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["HAPELAMT"].ToString()))); // 보일러사용금액
                    double Hapstamt = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["GMSTAMT"].ToString())));  // 스팀사용금액
                    double Hapkyamt = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["GMKYAMT"].ToString())));  // 경유사용금액
                    double Hapdkamt = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["GMDKAMT"].ToString())));  // 등유사용금액

                    double Haphapamt = Hapelamt + Hapstamt + Hapkyamt + Hapdkamt;

                    row["DAHAPAMT"] = Convert.ToDouble(UP_DotDelete(Convert.ToString(Haphapamt)));  // 합계

                    #endregion

                    #region Description : TYC(태영인더스트리(울산)

                    // 화물
                    row["HMDESC1"] = dt.Rows[i]["HMDESC1"].ToString();
                    // 보일러가동시간
                    row["UTGAELTIME"] = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["UTGAELTIME"].ToString())));
                    // 경유
                    row["UTGAKYQTY"] = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["UTGAKYQTY"].ToString())));
                    // 스팀사용시간
                    row["UTGASTTIME"] = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["UTGASTTIME"].ToString())));
                    // 스팀사용량
                    row["UTGASTQTY"] = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["UTGASTQTY"].ToString())));
                    // 등유사용량
                    row["UTGADKQTY"] = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["UTGADKQTY"].ToString())));

                    // 보일러사용금액
                    double UTGAHAPELAMT = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["UTGAHAPELAMT"].ToString())));
                    // 스팀사용금액
                    double UTGAMSTAMT = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["UTGAHAPSTAMT"].ToString())));
                    // 경유사용금액
                    double UTGAMKYAMT = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["UTGAHAPKYAMT"].ToString())));
                    // 경유사용금액
                    double UTGAMDKAMT = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["UTGAHAPDKAMT"].ToString())));

                    double UTGAHaphapamt = UTGAHAPELAMT + UTGAMSTAMT + UTGAMKYAMT + UTGAMDKAMT;
                    // 합계
                    row["UTDAHAPAMT"] = Convert.ToDouble(UP_DotDelete(Convert.ToString(UTGAHaphapamt)));

                    #endregion

                    // [ 단   가 ]
                    // 보일러가동시간(단가)
                    row["WKDNEL"] = double.Parse(Set_Numeric2(dt.Rows[i]["WKDNEL"].ToString(), 2));
                    // 경유(단가)
                    row["DNKYUNG"] = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["DNKYUNG"].ToString())));
                    // 스팀(단가)
                    row["DNSKSTEAM"] = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["DNSKSTEAM"].ToString())));
                    // 등유(단가)
                    row["DNSTDANGA"] = double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["DNSTDANGA"].ToString())));

                    retDt.Rows.Add(row);
                }
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sNEWCHK = dt.Rows[i]["DACHKGB"].ToString();    // 전월 26 ~ 당월 25 --> '1' ,당월 1 ~ 당월 31 --> '2'
                    sNEWYYMM = this.DTP01_YYYYMM.GetString().Substring(0, 6);     // 처리년월
                    sNEWGUBUN = dt.Rows[i]["GAGUBUN"].ToString();    // 상하단지--> '1' , 송유단지 --> '2'
                    sNEWHWAJU = dt.Rows[i]["GAHWAJU"].ToString();    // 화주
                    sNEWHWAMUL = dt.Rows[i]["GAHWAMUL"].ToString();   // 화물

                    // 스팀사용금액 =  단가 * 사용량
                    // 월집계가 있을때는 월집계 DATA로 처리 하고
                    // 월집계가 없을때는 계산하여 처리 한다.

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_67MAB897", sNEWGUBUN,
                                                                sNEWYYMM,
                                                                sNEWCHK,
                                                                sNEWHWAJU,
                                                                sNEWHWAMUL
                                                                );

                    DataTable dt2 = this.DbConnector.ExecuteDataTable();

                    DataRow row = retDt.NewRow();

                    if (dt2.Rows.Count > 0)
                    {
                        row["DACHKGB"] = dt.Rows[i]["DACHKGB"].ToString();

                        row["DAYYMM"] = dt.Rows[i]["DAYYMM"].ToString();                  // 날짜
                        row["DAGUBUNNM1"] = dt.Rows[i]["DAGUBUNNM1"].ToString();              // 상하,송유단지
                        row["HWAJNM"] = dt.Rows[i]["HWAJNM"].ToString();                  // 화주
                        row["HWAMULNM"] = dt.Rows[i]["HWAMULNM"].ToString();                // 화물
                        row["GAELTIME"] = double.Parse(dt.Rows[i]["GAELTIME"].ToString());  // 보일러가동시간
                        row["GAKYQTY"] = double.Parse(dt.Rows[i]["GAKYQTY"].ToString());   // 경유
                        row["GASTTIME"] = double.Parse(dt.Rows[i]["GASTTIME"].ToString());  // 스팀사용시간
                        row["GASTQTY"] = double.Parse(dt.Rows[i]["GASTQTY"].ToString());   // 스팀사용량

                        double Hapelamt = double.Parse(dt.Rows[i]["HAPELAMT"].ToString());  // 보일러사용금액
                        double Hapstamt = double.Parse(Get_Numeric(dt2.Rows[0]["GMSTAMT"].ToString()));  // 스팀사용금액
                        double Hapkyamt = double.Parse(Get_Numeric(dt2.Rows[0]["GMKYAMT"].ToString()));  // 경유사용금액

                        double Haphapamt = Hapelamt + Hapstamt + Hapkyamt;
                        row["DAHAPAMT"] = Convert.ToDouble(UP_DotDelete(Convert.ToString(Haphapamt)));  // 합계

                        // [ 단   가 ]
                        row["WKDNEL"] = double.Parse(Set_Numeric2(dt.Rows[i]["WKDNEL"].ToString(), 2)); // 보일러가동시간(단가)
                        row["DNKYUNG"] = double.Parse(dt.Rows[i]["DNKYUNG"].ToString());                // 경유(단가)
                        row["DNSKSTEAM"] = double.Parse(dt.Rows[i]["DNSKSTEAM"].ToString());              // 스팀(단가)

                        retDt.Rows.Add(row);
                    }
                    else
                    {
                        row["DACHKGB"] = dt.Rows[i]["DACHKGB"].ToString();
                        row["DAYYMM"] = dt.Rows[i]["DAYYMM"].ToString();                  // 날짜
                        row["DAGUBUNNM1"] = dt.Rows[i]["DAGUBUNNM1"].ToString();              // 상하,송유단지
                        row["HWAJNM"] = dt.Rows[i]["HWAJNM"].ToString();                  // 화주
                        row["HWAMULNM"] = dt.Rows[i]["HWAMULNM"].ToString();                // 화물
                        row["GAELTIME"] = double.Parse(dt.Rows[i]["GAELTIME"].ToString());  // 보일러가동시간
                        row["GAKYQTY"] = double.Parse(dt.Rows[i]["GAKYQTY"].ToString());   // 경유
                        row["GASTTIME"] = double.Parse(dt.Rows[i]["GASTTIME"].ToString());  // 스팀사용시간
                        row["GASTQTY"] = double.Parse(dt.Rows[i]["GASTQTY"].ToString());   // 스팀사용량

                        double Hapelamt = double.Parse(dt.Rows[i]["HAPELAMT"].ToString());  // 보일러사용금액
                        double Hapstamt = double.Parse(dt.Rows[i]["HAPSTAMT"].ToString());  // 스팀사용금액
                        double Hapkyamt = double.Parse(dt.Rows[i]["HAPKYAMT"].ToString());  // 경유사용금액

                        double Haphapamt = Hapelamt + Hapstamt + Hapkyamt;
                        row["DAHAPAMT"] = Haphapamt;  // 합계

                        // [ 단   가 ]
                        row["WKDNEL"] = double.Parse(Set_Numeric2(dt.Rows[i]["WKDNEL"].ToString(), 2));   // 보일러가동시간(단가)
                        row["DNKYUNG"] = double.Parse(dt.Rows[i]["DNKYUNG"].ToString());                  // 경유(단가)
                        row["DNSKSTEAM"] = double.Parse(dt.Rows[i]["DNSKSTEAM"].ToString());                // 스팀(단가)

                        retDt.Rows.Add(row);
                    }
                }
            }

            return retDt;
        }
        #endregion
    }
}
