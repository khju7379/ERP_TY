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
    /// 화주별 탱크세척 현황 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.09.08 13:04
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_698D3111 : 화주별 탱크세척 현황 출력
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
    public partial class TYUTIL013P : TYBase
    {
        #region Description : 페이지 로드
        public TYUTIL013P()
        {
            InitializeComponent();
        }

        private void TYUTIL013P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_YYYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(this.DTP01_YYYYMM);
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            int stmm = 0;
            int styy = 0;

            int stdd = 0;
            int eddd = 0;

            int styymm = 0;
            int edyymm = 0;

            if (int.Parse(this.DTP01_YYYYMM.GetString()) >= 20200201)  // 2020-02월 부터 시작일 종료일 변경 (서태호 과장)
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

            // 2022년 03월 부터 세척비용 조회 테이블 변경 UTIUTYOF ->UTIUTCLF
            // 최효은 대리 요청 2022년 자료부터 적용년월 기준으로 조회
            if (int.Parse(this.DTP01_YYYYMM.GetString()) >= 20220301)
            {
                this.DbConnector.Attach("TY_P_UT_C2I9E086", ssdyy,
                                                            ssdmm,
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            ssdyy,
                                                            ssdmm,
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6)
                                                            );
            }
            else if (int.Parse(this.DTP01_YYYYMM.GetString()) >= 20220101)
            {
                this.DbConnector.Attach("TY_P_UT_C2GEE061", ssdyy,
                                                            ssdmm,
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6),
                                                            ssdyy,
                                                            ssdmm,
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6)
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_698D3111", ssdyy,
                                                            ssdmm,
                                                            wstyymmdd,
                                                            wedyymmdd,
                                                            ssdyy,
                                                            ssdmm,
                                                            this.DTP01_YYYYMM.GetString().Substring(0, 6)
                                                            );
            }

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                ActiveReport rpt = new TYUTIL013R();
                // 가로 출력
                rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion
    }
}
