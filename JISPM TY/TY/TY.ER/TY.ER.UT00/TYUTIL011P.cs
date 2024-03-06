using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 화주별 가열료 사용현황 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.09.02 09:27
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_692DP070 : 화주별 가열료 사용현황 출력(200808 이전)
    ///  TY_P_UT_692EI072 : 화주별 가열료 사용현황 출력(200808 이후)
    ///  TY_P_UT_692EJ073 : 화주별 가열료 사용현황 출력(월 집계 파일)
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
    public partial class TYUTIL011P : TYBase
    {
        #region Description : 페이지 로드
        public TYUTIL011P()
        {
            InitializeComponent();
        }

        private void TYUTIL011P_Load(object sender, System.EventArgs e)
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

            string wstyymmdd = Convert.ToString(styy) + Set_Fill2(Convert.ToString(stmm)) + Convert.ToString(stdd);// 시작 (26일) 부터 시작, 2020-02 부터(21일)
            string ssdyy = this.DTP01_YYYYMM.GetString().Substring(0, 4);
            string ssdmm = this.DTP01_YYYYMM.GetString().Substring(4, 2);
            string wedyymmdd = ssdyy + ssdmm + Convert.ToString(eddd); // 종료(25일), 2020-02 부터(20일)

            string sqryy = Convert.ToString(styy);
            string sqrmm = Set_Fill2(Convert.ToString(stmm));
            string sqryymm = Convert.ToString(styy) + Set_Fill2(Convert.ToString(stmm));

            string sDATE = "( " + this.DTP01_YYYYMM.GetString().Substring(0, 4) + " 년 " + this.DTP01_YYYYMM.GetString().Substring(4, 2) + " 월 )";
            //string sDATE = "( " + this.DTP01_YYYYMM.GetString().Substring(0, 4) + "  " + this.DTP01_YYYYMM.GetString().Substring(4, 2) + "  )";

            if (Convert.ToInt32(this.DTP01_YYYYMM.GetString().Substring(0, 6)) >= 201012)
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_692DP070", "",
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            wstyymmdd,
                                                            wedyymmdd,
                                                            "",
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6)
                                                            );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYUTIL011R2(sDATE);
                    // 가로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, Convert_DataTable2(Convert_DataTable1(dt)))).ShowDialog();
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                }
            }
            else
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_692DP070", "",
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            wstyymmdd,
                                                            wedyymmdd,
                                                            "",
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6)
                                                            );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    SectionReport rpt = new TYUTIL011R1(sDATE);
                    // 가로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                    (new TYERGB001P(rpt, Convert_DataTable2(Convert_DataTable1(dt)))).ShowDialog();
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                }
            }
        }
        #endregion

        #region Description : 데이터테이블 변경
        private DataTable Convert_DataTable1(DataTable dt)
        {
            DataTable retDt = new DataTable();

            string sNEWCHK = string.Empty;
            string sNEWYYMM = string.Empty;
            string sNEWGUBUN = string.Empty;
            string sNEWHWAJU = string.Empty;
            string sNEWHWAMUL = string.Empty;

            string sOLDCHK = string.Empty;
            string sOLDYYMM = string.Empty;
            string sOLDGUBUN = string.Empty;
            string sOLDHWAJU = string.Empty;
            string sOLDHWAMUL = string.Empty;
            double Hapelamt = 0;  // 보일러사용금액
            double Hapstamt = 0;  // 스팀사용금액
            double Hapkyamt = 0;  // 경유사용금액
            double Hapdkamt = 0;  // 등유사용금액

            retDt.Columns.Add("DAYYMM", typeof(System.String));
            retDt.Columns.Add("DAGUBUNNM", typeof(System.String));
            retDt.Columns.Add("HWAJNM", typeof(System.String));
            retDt.Columns.Add("HWAMULNM", typeof(System.String));
            retDt.Columns.Add("GAYYMMDD", typeof(System.String));
            retDt.Columns.Add("GAELTIME", typeof(System.String));
            retDt.Columns.Add("GAKYQTY", typeof(System.String));
            retDt.Columns.Add("GADKQTY", typeof(System.String));
            retDt.Columns.Add("GASTTIME", typeof(System.String));
            retDt.Columns.Add("GASTQTY", typeof(System.String));
            retDt.Columns.Add("HAPELAMT", typeof(System.String));
            retDt.Columns.Add("HAPSTAMT", typeof(System.String));
            retDt.Columns.Add("HAPKYAMT", typeof(System.String));
            retDt.Columns.Add("HAPDKAMT", typeof(System.String));
            retDt.Columns.Add("WKDNEL", typeof(System.String));
            retDt.Columns.Add("DNKYUNG", typeof(System.String));
            retDt.Columns.Add("DNSKSTEAM", typeof(System.String));
            retDt.Columns.Add("DNSTDANGA", typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sNEWCHK = dt.Rows[i]["DACHKGB"].ToString();    // 전월 26 ~ 당월 25 --> '1' ,당월 1 ~ 당월 31 --> '2'
                sNEWYYMM = this.DTP01_YYYYMM.GetString().Substring(0, 6);                           // 처리년월
                sNEWGUBUN = dt.Rows[i]["GAGUBUN"].ToString();    // 상하단지--> '1' , 송유단지 --> '2'
                sNEWHWAJU = dt.Rows[i]["GAHWAJU"].ToString();    // 화주
                sNEWHWAMUL = dt.Rows[i]["GAHWAMUL"].ToString();   // 화물


                // 스팀사용금액 =  단가 * 사용량
                // 월집계가 있을때는 월집계 DATA로 처리 하고
                // 월집계가 없을때는 계산하여 처리 한다.

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_692EJ073", sNEWGUBUN.ToString(),
                                                            sNEWYYMM.ToString(),
                                                            sNEWCHK.ToString(),
                                                            sNEWHWAJU.ToString(),
                                                            sNEWHWAMUL.ToString()
                                                            );

                DataTable dtgm = this.DbConnector.ExecuteDataTable();

                DataRow row = retDt.NewRow();

                if (dtgm.Rows.Count > 0)
                {
                    row["DAYYMM"] = dt.Rows[i]["DAYYMM"].ToString();                 // 날짜
                    row["DAGUBUNNM"] = dt.Rows[i]["DAGUBUNNM"].ToString();              // 상하,송유단지
                    row["HWAJNM"] = dt.Rows[i]["HWAJNM"].ToString();                 // 화주
                    row["HWAMULNM"] = dt.Rows[i]["HWAMULNM"].ToString();               // 화물
                    row["GAYYMMDD"] = dt.Rows[i]["GAYYMMDD"].ToString();               // 거래일자
                    row["GAELTIME"] = double.Parse(dt.Rows[i]["GAELTIME"].ToString()); // 보일러가동시간
                    row["GAKYQTY"] = double.Parse(dt.Rows[i]["GAKYQTY"].ToString());  // 경유
                    row["GADKQTY"] = double.Parse(dt.Rows[i]["GADKQTY"].ToString());  // 등유
                    row["GASTTIME"] = double.Parse(dt.Rows[i]["GASTTIME"].ToString()); // 스팀사용시간
                    row["GASTQTY"] = double.Parse(dt.Rows[i]["GASTQTY"].ToString());  // 스팀사용량

                    row["HAPELAMT"] = double.Parse(dt.Rows[i]["HAPELAMT"].ToString()); // 보일러사용금
                    row["HAPSTAMT"] = double.Parse(dt.Rows[i]["HAPSTAMT"].ToString()); // 스팀사용금액
                    row["HAPKYAMT"] = double.Parse(dt.Rows[i]["HAPKYAMT"].ToString()); // 경유사용금액
                    row["HAPDKAMT"] = Hapdkamt;                                                  // 등유사용금액

                    // [ 단   가 ]
                    row["WKDNEL"] = double.Parse(Set_Numeric2(dt.Rows[i]["WKDNEL"].ToString(), 2)); // 보일러가동시간(단가)
                    row["DNKYUNG"] = double.Parse(dt.Rows[i]["DNKYUNG"].ToString());                // 경유(단가)
                    row["DNSKSTEAM"] = double.Parse(dt.Rows[i]["DNSKSTEAM"].ToString());              // 스팀(단가)
                    row["DNSTDANGA"] = double.Parse(dt.Rows[i]["DNSTDANGA"].ToString());              // 상하단지스팀(단가)
                }
                else
                {
                    row["DAYYMM"] = dt.Rows[i]["DAYYMM"].ToString();                 // 날짜
                    row["DAGUBUNNM"] = dt.Rows[i]["DAGUBUNNM"].ToString();              // 상하,송유단지
                    row["HWAJNM"] = dt.Rows[i]["HWAJNM"].ToString();                 // 화주
                    row["HWAMULNM"] = dt.Rows[i]["HWAMULNM"].ToString();               // 화물
                    row["GAYYMMDD"] = dt.Rows[i]["GAYYMMDD"].ToString();               // 거래일자
                    row["GAELTIME"] = double.Parse(dt.Rows[i]["GAELTIME"].ToString()); // 보일러가동시간
                    row["GAKYQTY"] = double.Parse(dt.Rows[i]["GAKYQTY"].ToString());  // 경유
                    row["GADKQTY"] = double.Parse(dt.Rows[i]["GADKQTY"].ToString());  // 등유
                    row["GASTTIME"] = double.Parse(dt.Rows[i]["GASTTIME"].ToString()); // 스팀사용시간
                    row["GASTQTY"] = double.Parse(dt.Rows[i]["GASTQTY"].ToString());  // 스팀사용량

                    if (sOLDCHK != sNEWCHK || sOLDGUBUN != sNEWGUBUN || sOLDHWAJU != sNEWHWAJU || sOLDHWAMUL != sNEWHWAMUL)
                    {
                        sOLDCHK = dt.Rows[i]["DACHKGB"].ToString();    // 전월 26 ~ 당월 25 --> '1' ,당월 1 ~ 당월 31 --> '2'
                        sOLDGUBUN = dt.Rows[i]["GAGUBUN"].ToString();    // 상하단지--> '1' , 송유단지 --> '2'
                        sOLDHWAJU = dt.Rows[i]["GAHWAJU"].ToString();    // 화주
                        sOLDHWAMUL = dt.Rows[i]["GAHWAMUL"].ToString();   // 화물
                        Hapelamt = GridHap1(dt, "HAPELAMT", i, sOLDCHK, sOLDGUBUN, sOLDHWAJU, sOLDHWAMUL);// 보일러사용금액
                        Hapstamt = GridHap1(dt, "HAPSTAMT", i, sOLDCHK, sOLDGUBUN, sOLDHWAJU, sOLDHWAMUL);// 스팀사용금액
                        Hapkyamt = GridHap1(dt, "HAPKYAMT", i, sOLDCHK, sOLDGUBUN, sOLDHWAJU, sOLDHWAMUL);// 경유사용금액
                        //						Hapdkamt = GridHap1(ds, "HAPDKAMT",i,sOLDCHK,sOLDGUBUN,sOLDHWAJU,sOLDHWAMUL);// 등유사용금액
                    }

                    row["HAPELAMT"] = Hapelamt;  // 보일러사용금액
                    row["HAPSTAMT"] = Hapstamt;  // 스팀사용금액
                    row["HAPKYAMT"] = Hapkyamt;  // 경유사용금액
                    row["HAPDKAMT"] = 0;         // 등유사용금액

                    // [ 단   가 ]
                    row["WKDNEL"] = double.Parse(Set_Numeric2(dt.Rows[i]["WKDNEL"].ToString(), 2)); // 보일러가동시간(단가)
                    row["DNKYUNG"] = double.Parse(Set_Numeric2(dt.Rows[i]["DNKYUNG"].ToString(), 2));                // 경유(단가)
                    row["DNSKSTEAM"] = double.Parse(Set_Numeric2(dt.Rows[i]["DNSKSTEAM"].ToString(), 2));              // 스팀(단가)
                    row["DNSTDANGA"] = double.Parse(Set_Numeric2(dt.Rows[i]["DNSTDANGA"].ToString(), 2));              // 상하단지스팀(단가)
                }
                retDt.Rows.Add(row);
            }

            return retDt;
        }
        #endregion

        #region Description : 데이터테이블 변경2
        private DataTable Convert_DataTable2(DataTable dt)
        {
            DataTable retDt = new DataTable();

            string sCHECK = "*";
            string sNEWDAGUBUNNM = string.Empty;
            string sOLDDAGUBUNNM = string.Empty;
            string sNEWHWAJNM = string.Empty;
            string sOLDHWAJNM = string.Empty;
            string sNEWHWAMULNM = string.Empty;
            string sOLDHWAMULNM = string.Empty;
            string sCOUNT = "0";

            retDt.Columns.Add("DAYYMM", typeof(System.String));
            retDt.Columns.Add("DAGUBUNNM", typeof(System.String));
            retDt.Columns.Add("HWAJNM", typeof(System.String));
            retDt.Columns.Add("HWAMULNM", typeof(System.String));
            retDt.Columns.Add("GAELTIME", typeof(System.String));
            retDt.Columns.Add("GAKYQTY", typeof(System.String));
            retDt.Columns.Add("GADKQTY", typeof(System.String));
            retDt.Columns.Add("GASTTIME", typeof(System.String));
            retDt.Columns.Add("GASTQTY", typeof(System.String));
            retDt.Columns.Add("DNSKSTEAM", typeof(System.String));
            retDt.Columns.Add("DNKYUNG", typeof(System.String));
            retDt.Columns.Add("DNSTDANGA", typeof(System.String));
            retDt.Columns.Add("WKDNEL", typeof(System.String));
            retDt.Columns.Add("HAPELAMT", typeof(System.String));
            retDt.Columns.Add("HAPSTAMT", typeof(System.String));
            retDt.Columns.Add("HAPKYAMT", typeof(System.String));
            retDt.Columns.Add("HAPDKAMT", typeof(System.String));
            retDt.Columns.Add("GAYYMMDD", typeof(System.String));
            retDt.Columns.Add("GUBUN", typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sNEWDAGUBUNNM = dt.Rows[i]["DAGUBUNNM"].ToString();
                sNEWHWAJNM = dt.Rows[i]["HWAJNM"].ToString();
                sNEWHWAMULNM = dt.Rows[i]["HWAMULNM"].ToString();

                if (sCHECK == "*")
                {
                    sOLDDAGUBUNNM = sNEWDAGUBUNNM.ToString();
                    sOLDHWAJNM = sNEWHWAJNM.ToString();
                    sOLDHWAMULNM = sNEWHWAMULNM.ToString();
                    sCOUNT = Convert.ToString(double.Parse(sCOUNT) + 1);
                    sCHECK = "";
                }
                else
                {
                    if (sOLDDAGUBUNNM.ToString() != sNEWDAGUBUNNM.ToString() ||
                       sOLDHWAJNM.ToString() != sNEWHWAJNM.ToString() ||
                       sOLDHWAMULNM.ToString() != sNEWHWAMULNM.ToString())
                    {
                        sOLDDAGUBUNNM = sNEWDAGUBUNNM.ToString();
                        sOLDHWAJNM = sNEWHWAJNM.ToString();
                        sOLDHWAMULNM = sNEWHWAMULNM.ToString();
                        sCOUNT = Convert.ToString(double.Parse(sCOUNT) + 1);
                    }
                }
                DataRow row1 = retDt.NewRow();
                row1["DAYYMM"] = dt.Rows[i]["DAYYMM"].ToString();                  // 날짜
                row1["DAGUBUNNM"] = dt.Rows[i]["DAGUBUNNM"].ToString();               // 상하,송유단지
                row1["HWAJNM"] = dt.Rows[i]["HWAJNM"].ToString();                  // 화주명				
                row1["HWAMULNM"] = dt.Rows[i]["HWAMULNM"].ToString();                // 화물명
                row1["GAELTIME"] = double.Parse(dt.Rows[i]["GAELTIME"].ToString());  // 보일러가동시간
                row1["GAKYQTY"] = double.Parse(dt.Rows[i]["GAKYQTY"].ToString());   // 경유
                row1["GADKQTY"] = double.Parse(dt.Rows[i]["GADKQTY"].ToString());   // 등유
                row1["GASTTIME"] = double.Parse(dt.Rows[i]["GASTTIME"].ToString());  // 스팀사용시간
                row1["GASTQTY"] = double.Parse(dt.Rows[i]["GASTQTY"].ToString());   // 스팀사용량
                row1["DNSKSTEAM"] = double.Parse(dt.Rows[i]["DNSKSTEAM"].ToString()); // 스팀(단가)
                row1["DNKYUNG"] = double.Parse(dt.Rows[i]["DNKYUNG"].ToString());   // 경유(단가)
                row1["DNSTDANGA"] = double.Parse(dt.Rows[i]["DNSTDANGA"].ToString()); // 상하단지스팀(단가)
                row1["WKDNEL"] = double.Parse(dt.Rows[i]["WKDNEL"].ToString());    // 보일러가동시간(단가)

                row1["HAPELAMT"] = double.Parse(dt.Rows[i]["HAPELAMT"].ToString());  // 보일러사용금액
                row1["HAPSTAMT"] = double.Parse(dt.Rows[i]["HAPSTAMT"].ToString());  // 스팀사용금액
                row1["HAPKYAMT"] = double.Parse(dt.Rows[i]["HAPKYAMT"].ToString());  // 경유사용금액
                row1["HAPDKAMT"] = double.Parse(dt.Rows[i]["HAPDKAMT"].ToString());  // 등유사용금액
                row1["GAYYMMDD"] = dt.Rows[i]["GAYYMMDD"].ToString();                // 거래일자
                row1["GUBUN"] = sCOUNT;                                           // 그룹에 따른 순번

                retDt.Rows.Add(row1);
            }
            return retDt;
        }
        #endregion

        #region Description : 합계 구하기
        protected double GridHap1(DataTable dg, string sField, int sI, string swchk, string swgubun, string swhwaju, string swhwamul)
        {
            double iTot = 0;
            int k = sI;

            if (dg.Rows.Count > 0)
            {
                int iCount = dg.Rows.Count;
                for (int i = k; i < iCount; i++)
                {
                    if (dg.Rows[i][sField].ToString() == "" ||
                        swchk != dg.Rows[i]["DACHKGB"].ToString() ||    // 전월 26 ~ 당월 25 --> '1' ,당월 1 ~ 당월 31 --> '2'
                        swgubun != dg.Rows[i]["GAGUBUN"].ToString() ||    // 상하단지--> '1' , 송유단지 --> '2'
                        swhwaju != dg.Rows[i]["GAHWAJU"].ToString() ||    // 화주
                        swhwamul != dg.Rows[i]["GAHWAMUL"].ToString()     // 화물
                        )

                        continue;
                    iTot += Convert.ToDouble(dg.Rows[i][sField]);
                }
            }
            return iTot;
        }
        #endregion
    }
}
