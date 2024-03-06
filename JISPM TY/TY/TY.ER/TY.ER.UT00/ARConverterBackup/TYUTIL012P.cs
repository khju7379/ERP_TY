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
    /// 월 탱크세척 집계표 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.09.07 17:55
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_697HZ109 : 월 탱크세척 집계표 출력
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
    public partial class TYUTIL012P : TYBase
    {
        #region Description : 페이지 로드
        public TYUTIL012P()
        {
            InitializeComponent();
        }

        private void TYUTIL012P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_YYYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(this.DTP01_YYYYMM);
        }
        #endregion

        #region Description : 닫기버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 출력버튼 이벤트
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
            string wedyymmdd = ssdyy + ssdmm + Convert.ToString(eddd); // 종료(25일) , 2020-02 부터(20일)

            string sqryy = Convert.ToString(styy);
            string sqrmm = Set_Fill2(Convert.ToString(stmm));
            string sqryymm = Convert.ToString(styy) + Set_Fill2(Convert.ToString(stmm));

            string sDATE = "( " + this.DTP01_YYYYMM.GetString().Substring(0, 4) + " 년 " + this.DTP01_YYYYMM.GetString().Substring(4, 2) + " 월 )";

            this.DbConnector.CommandClear();
            
            if (int.Parse(this.DTP01_YYYYMM.GetString()) >= 20220301)
            {
                this.DbConnector.Attach("TY_P_UT_C2LG1096", this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6)
                                                            );
            }
            else if (int.Parse(this.DTP01_YYYYMM.GetString()) >= 20210101)
            {
                this.DbConnector.Attach("TY_P_UT_B799S438", this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6)
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_697HZ109", wstyymmdd,
                                                            wedyymmdd,
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6)
                                                            );
            }

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                ActiveReport rpt = new TYUTIL012R(sDATE);
                // 가로 출력
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                (new TYERGB001P(rpt, Convert_DataTable(dt))).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 데이터테이블 변경
        private DataTable Convert_DataTable(DataTable dt)
        {
            DataTable retDt = new DataTable();

            string[] wkclanme = new string[10];
            double[] wkclamt = new double[10];

            retDt.Columns.Add("CLHWAJU", typeof(System.String));
            retDt.Columns.Add("CLTANK", typeof(System.String));
            retDt.Columns.Add("CAPA", typeof(System.String));
            retDt.Columns.Add("HWAJNM", typeof(System.String));
            retDt.Columns.Add("CLJNHWAMUL", typeof(System.String));
            retDt.Columns.Add("HWAMULNM", typeof(System.String));
            retDt.Columns.Add("CLIPHWAMUL", typeof(System.String));
            retDt.Columns.Add("CLAMT", typeof(System.String));
            retDt.Columns.Add("PSAMT", typeof(System.String));
            retDt.Columns.Add("DMAMT", typeof(System.String));
            retDt.Columns.Add("CLELAMT", typeof(System.String));
            retDt.Columns.Add("TOTAMT", typeof(System.String));
            retDt.Columns.Add("NAME", typeof(System.String));
            retDt.Columns.Add("DAYYMM", typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = retDt.NewRow();

                row["CLHWAJU"] = dt.Rows[i]["CLHWAJU"].ToString();               // 지급처(화주코드)
                row["CLTANK"] = dt.Rows[i]["CLTANK"].ToString();                // TNAK
                row["CAPA"] = double.Parse(dt.Rows[i]["CAPA"].ToString());    // CAPA
                row["HWAJNM"] = dt.Rows[i]["HWAJNM"].ToString();                // 지급처(화주명)
                row["CLJNHWAMUL"] = dt.Rows[i]["CLJNHWAMUL"].ToString();            // 전화물(화물코드)
                row["HWAMULNM"] = dt.Rows[i]["HWAMULNM"].ToString();              // 전화물(화물명)
                row["CLIPHWAMUL"] = dt.Rows[i]["CLIPHWAMUL"].ToString();            // 입고화물
                row["CLAMT"] = double.Parse(dt.Rows[i]["CLAMT"].ToString());   // 세척비
                row["PSAMT"] = double.Parse(dt.Rows[i]["PSAMT"].ToString());   // 폐수비용
                row["DMAMT"] = double.Parse(dt.Rows[i]["DMAMT"].ToString());   // 드럼비용
                row["CLELAMT"] = double.Parse(dt.Rows[i]["CLELAMT"].ToString()); // 전기료
                row["TOTAMT"] = double.Parse(dt.Rows[i]["CLAMT"].ToString()) + double.Parse(dt.Rows[i]["PSAMT"].ToString()) + double.Parse(dt.Rows[i]["DMAMT"].ToString()) + double.Parse(dt.Rows[i]["CLELAMT"].ToString()); // 합계
                row["NAME"] = dt.Rows[i]["NAME"].ToString();                  // 세척자
                row["DAYYMM"] = dt.Rows[i]["DAYYMM"].ToString();                // 날자(타이틀)

                for (int j = 0; j < 10; j++)
                {
                    if (wkclanme[j] == null)
                    {
                        wkclanme[j] = dt.Rows[i]["NAME"].ToString();
                        wkclamt[j] = double.Parse(dt.Rows[i]["CLAMT"].ToString());
                        j = 99;
                    }
                    else
                    {
                        if (wkclanme[j] == dt.Rows[i]["NAME"].ToString())
                        {
                            wkclamt[j] += double.Parse(dt.Rows[i]["CLAMT"].ToString());
                            j = 99;
                        }
                    }
                }
                retDt.Rows.Add(row);
            }

            return retDt;
        }
        #endregion
    }
}
