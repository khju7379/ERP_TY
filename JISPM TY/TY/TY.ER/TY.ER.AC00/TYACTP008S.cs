using System;
using System.Data;
using System.Drawing;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using GrapeCity.ActiveReports;
using TY.ER.GB00;

namespace TY.ER.AC00
{
    /// <summary>
    /// 원천세 집계표 조회 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.10.16 08:42
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_3BK25384 : 제출자정보 조회
    ///  TY_P_AC_49GGW889 : 원천세 환급금자료 조회
    ///  TY_P_AC_4AGHZ192 : 원천세 집계표 관리 조회(집계)
    ///  TY_P_AC_4AKIB209 : 원천세 집계표 관리 세부조회(근로소득)
    ///  TY_P_AC_4AKIB210 : 원천세 집계표 관리 세부조회(근로소득:간이세액)
    ///  TY_P_AC_4AKIC211 : 원천세 집계표 관리 세부조회(근로소득:중도퇴사)
    ///  TY_P_AC_4AKID212 : 원천세 집계표 관리 세부조회(근로소득:일용근로)
    ///  TY_P_AC_4AL8E213 : 원천세 집계표 관리 세부조회(근로소득:연말정산)
    ///  TY_P_AC_4AL8E214 : 원천세 집계표 관리 세부조회(퇴직소득:A22)
    ///  TY_P_AC_4AL8F215 : 원천세 집계표 관리 세부조회(사업소득:A25)
    ///  TY_P_AC_4AL8G216 : 원천세 집계표 관리 세부조회(기타소득:A42)
    ///  TY_P_AC_4AL8G217 : 원천세 집계표 관리 세부조회(이자소득:A50)
    ///  TY_P_AC_4AL8G218 : 원천세 집계표 관리 세부조회(배당소득:A60)
    ///  TY_P_AC_4AL8H219 : 원천세 집계표 관리 집계 세부조회(지방소득세종업원분)
    ///  TY_P_AC_4ANEQ246 : 원천세 집계표 관리 세부조회(지방소득세종업원분)
    ///  TY_P_AC_4BLDZ471 : 원천세 집계표 출력 (지방소득세 납부시)
    ///  TY_P_AC_4BOBG484 : 원천세 집계표 출력 (지방소득세 환급시[2월])
    ///  TY_P_AC_4BOGH487 : 원천세 집계표 출력 (지방소득세 환급시)
    ///  TY_P_HR_29EAS015 : 근태월력 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_4AH8Z197 : 원전체 집계표 조회
    ///  TY_S_AC_4AH90198 : 원집세 집계표 상세 조회(근로소득)
    ///  TY_S_AC_4AL9G220 : 원천세 집계표 세부조회(근로소득:중도퇴사)
    ///  TY_S_AC_4ALAY221 : 원천세 집계표 세부조회(근로소득:연말정산)
    ///  TY_S_AC_4ALFN226 : 원천세 집계표 세부조회(근로소득:간이세액)
    ///  TY_S_AC_4ALH1227 : 원천세 집계표 세부조회(근로소득:일용근로)
    ///  TY_S_AC_4ALHV228 : 원집세 집계표 상세 조회(퇴직,기타 소득)
    ///  TY_S_AC_4ALID229 : 원집세 집계표 상세 조회(사업소득)
    ///  TY_S_AC_4AMBT232 : 원집세 집계표 상세 조회(배당,이자 소득)
    ///  TY_S_AC_4ANEZ247 : 원천세 집계표 관리 세부조회(지방소득세종업원분)
    ///  TY_S_AC_4ANF5248 : 원천세 집계표 관리 세부조회(지방소득세종업원분)
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  WABRANCH : 지점구분
    ///  WREYYMM : 귀속년월
    /// </summary>
    public partial class TYACTP008S : TYBase
    {
        private string fsBRANCH = string.Empty;

        #region Description : 페이지 로드
        public TYACTP008S()
        {
            InitializeComponent();
        }

        private void TYACTP008S_Load(object sender, System.EventArgs e)
        {
            UP_Spread_Title();

            this.DTP01_WREYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            fsBRANCH = "1";

            SetStartingFocus(this.DTP01_WREYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            // 21.11.30 수정전 소스            
            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_4AGHZ192",
            //    fsBRANCH.ToString(),
            //    this.DTP01_WREYYMM.GetValue().ToString()
            //    );

            // 21.11.30 황성환 요청
            // (A50,A60)은 생성된 DATA를 가져오지 말고
            // 소득자료에 입력된 (A50,A60)데이터를 가져온다
            this.DbConnector.Attach
                (
                "TY_P_AC_BBTFA818",
                fsBRANCH.ToString(),
                this.DTP01_WREYYMM.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_4AH8Z197.SetValue(dt);

            if (this.FPS91_TY_S_AC_4AH8Z197.ActiveSheet.RowCount > 0)
            {
                this.FPS91_TY_S_AC_4AH8Z197_Sheet1.Cells[0, 3].BackColor = Color.Yellow;
                this.FPS91_TY_S_AC_4AH8Z197_Sheet1.Cells[0, 13].BackColor = Color.Yellow;
                this.FPS91_TY_S_AC_4AH8Z197_Sheet1.Cells[1, 3].BackColor = Color.Yellow;
                this.FPS91_TY_S_AC_4AH8Z197_Sheet1.Cells[2, 3].BackColor = Color.Yellow;
                this.FPS91_TY_S_AC_4AH8Z197_Sheet1.Cells[3, 3].BackColor = Color.Yellow;
                this.FPS91_TY_S_AC_4AH8Z197_Sheet1.Cells[4, 3].BackColor = Color.Yellow;
                this.FPS91_TY_S_AC_4AH8Z197_Sheet1.Cells[5, 3].BackColor = Color.Yellow;

                for (int i = 0; i < this.FPS91_TY_S_AC_4AH8Z197.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_AC_4AH8Z197.GetValue(i, "WPAYGUBNNM").ToString() == "계")
                    {
                        this.FPS91_TY_S_AC_4AH8Z197.ActiveSheet.Rows[i].ForeColor = Color.Red;
                        this.FPS91_TY_S_AC_4AH8Z197.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }

            this.FPS91_TY_S_AC_4AH90198.Initialize();  // 근로소득
            this.FPS91_TY_S_AC_4ALFN226.Initialize();  // 근로소득:간이세액
            this.FPS91_TY_S_AC_4AL9G220.Initialize();  // 근로소득:중도퇴사
            this.FPS91_TY_S_AC_4ALH1227.Initialize();  // 근로소득:일용근로
            this.FPS91_TY_S_AC_4ALAY221.Initialize();  // 근로소득:연말정산
            this.FPS91_TY_S_AC_4ANEZ247.Initialize();  // 주민세종업원분
            this.FPS91_TY_S_AC_4ANF5248.Initialize();  // 주민세종업원분(세부)

            this.FPS91_TY_S_AC_4ALHV228.Initialize();  // 퇴직소득
            this.FPS91_TY_S_AC_BC1BZ848.Initialize();  // 퇴직소득(세부)

            this.FPS91_TY_S_AC_4ALID229.Initialize();  // 사업소득
            this.FPS91_TY_S_AC_4AMBT232.Initialize();  // 이자소득,배당소득
            
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();

            string sGubn = string.Empty;
            string JIDATE = UP_getJIDATE(this.DTP01_WREYYMM.GetValue().ToString());

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_49GGW889",
                fsBRANCH.ToString(),
                this.DTP01_WREYYMM.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (DTP01_WREYYMM.GetValue().ToString().Substring(4, 2) == "02")    //2월
                {
                    if (dt.Rows[i]["WNNEXTTAX"].ToString() != "0")
                    {
                        sGubn = "1";
                    }
                    else
                    {
                        sGubn = "3";
                    }
                }
                else
                {
                    if (dt.Rows[i]["WNNEXTTAX"].ToString() != "0")
                    {
                        sGubn = "2";
                    }
                    else
                    {
                        sGubn = "3";
                    }
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_3BK25384",
                                        fsBRANCH.ToString(),
                                        "");

                dt2 = this.DbConnector.ExecuteDataTable();

                if (sGubn == "1")
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_AC_4BOBG484",
                        fsBRANCH.ToString(),
                        this.DTP01_WREYYMM.GetValue().ToString()
                        );
                    dt1 = this.DbConnector.ExecuteDataTable();

                    dt1 = UP_ChangeDatatable(dt1, 11);

                    SectionReport rpt = new TYACTP008R1(dt2, JIDATE);
                    (new TYERGB001P(rpt, dt1)).ShowDialog();
                }
                else if (sGubn == "2")
                {

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_AC_4BOGH487",
                        fsBRANCH.ToString(),
                        this.DTP01_WREYYMM.GetValue().ToString()
                        );
                    dt1 = this.DbConnector.ExecuteDataTable();

                    dt1 = UP_ChangeDatatable(dt1, 11);

                    SectionReport rpt = new TYACTP008R1(dt2, JIDATE);
                    (new TYERGB001P(rpt, dt1)).ShowDialog();
                }
                else if (sGubn == "3")
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_AC_4BLDZ471",
                        fsBRANCH.ToString(),
                        this.DTP01_WREYYMM.GetValue().ToString()
                        );
                    dt1 = this.DbConnector.ExecuteDataTable();

                    dt1 = UP_ChangeDatatable(dt1, 7);

                    SectionReport rpt = new TYACTP008R2(dt2, JIDATE, UP_NumToString(dt1.Rows[dt1.Rows.Count - 1]["ARETAX"].ToString()));
                    (new TYERGB001P(rpt, dt1)).ShowDialog();
                }
            }
        }
        #endregion

        #region Description : 출력 테이블 변환
        private DataTable UP_ChangeDatatable(DataTable dt, int RowNum)
        {
            int iRowCount = dt.Rows.Count - 1;
            DataTable Retdt = new DataTable();

            Retdt.Columns.Add("BRANCH", typeof(System.String));
            Retdt.Columns.Add("REVYYMM", typeof(System.String));
            Retdt.Columns.Add("WRGYYMM", typeof(System.String));
            Retdt.Columns.Add("INCOMGB", typeof(System.String));
            Retdt.Columns.Add("INCOMGBNM", typeof(System.String));
            Retdt.Columns.Add("PEOPLE", typeof(System.String));
            Retdt.Columns.Add("INCTAX", typeof(System.String));
            Retdt.Columns.Add("ARETAX", typeof(System.String));

            DataRow row = Retdt.NewRow();

            for (int i = 0; i < RowNum; i++)
            {
                row = Retdt.NewRow();

                if (i < iRowCount)
                {
                    row["BRANCH"] = dt.Rows[i]["BRANCH"].ToString();
                    row["REVYYMM"] = dt.Rows[i]["REVYYMM"].ToString();
                    row["WRGYYMM"] = dt.Rows[i]["WRGYYMM"].ToString();
                    row["INCOMGB"] = dt.Rows[i]["INCOMGB"].ToString();
                    row["INCOMGBNM"] = dt.Rows[i]["INCOMGBNM"].ToString();
                    row["PEOPLE"] = dt.Rows[i]["PEOPLE"].ToString();
                    row["INCTAX"] = dt.Rows[i]["INCTAX"].ToString();
                    row["ARETAX"] = dt.Rows[i]["ARETAX"].ToString();
                }
                else if (i < RowNum-1)
                {
                    row["BRANCH"] = DBNull.Value;
                    row["REVYYMM"] = DBNull.Value;
                    row["WRGYYMM"] = DBNull.Value;
                    row["INCOMGB"] = DBNull.Value;
                    row["INCOMGBNM"] = DBNull.Value;
                    row["PEOPLE"] = DBNull.Value;
                    row["INCTAX"] = DBNull.Value;
                    row["ARETAX"] = DBNull.Value;
                }
                else
                {
                    row["BRANCH"] = dt.Rows[iRowCount]["BRANCH"].ToString();
                    row["REVYYMM"] = dt.Rows[iRowCount]["REVYYMM"].ToString();
                    row["WRGYYMM"] = dt.Rows[iRowCount]["WRGYYMM"].ToString();
                    row["INCOMGB"] = dt.Rows[iRowCount]["INCOMGB"].ToString();
                    row["INCOMGBNM"] = dt.Rows[iRowCount]["INCOMGBNM"].ToString();
                    row["PEOPLE"] = dt.Rows[iRowCount]["PEOPLE"].ToString();
                    row["INCTAX"] = dt.Rows[iRowCount]["INCTAX"].ToString();
                    row["ARETAX"] = dt.Rows[iRowCount]["ARETAX"].ToString();
                }

                Retdt.Rows.Add(row);
            }

            return Retdt;
        }
        #endregion

        #region Description : 숫자 -> 한글로 변환
        private string UP_NumToString(string NUM)
        {
            //NUM = "44268440";

            int iJrCount1 = 0;
            int iJrCount2 = 0;

            string rtnValue = string.Empty;

            string[] sData = new string[NUM.Length];
            string[] sData_r = new string[NUM.Length];

            string[] sNum = {"","일","이","삼","사","오","육","칠","팔","구"};
            string[] sJari1 = { "", "십", "백", "천", "", "", "", "", "", "" };
            string[] sJari2 = { "", "만", "억", "조", "경" };

            for(int i = 0 ; i < NUM.Length ; i++)
            {   
                sData[i] = sNum[Convert.ToInt32(NUM.Substring(i, 1))];
            }
            for (int i = NUM.Length; i > 0; i--)
            {
                sData_r[i - 1] = sData[NUM.Length - i];
            }
            for (int i = 0; i < NUM.Length; i++)
            {
                if (sData_r[i] != "")
                {
                    sData_r[i] = sData_r[i] + sJari1[iJrCount1];
                }
                
                if (iJrCount1 == 4)
                {
                    iJrCount1 = 0;
                    iJrCount2++;
                    sData_r[i] = sData_r[i] + sJari2[iJrCount2];
                }
                iJrCount1++;
            }
            for (int i = NUM.Length; i > 0; i--)
            {
                rtnValue += sData_r[i - 1];
            }

            rtnValue = "일금 " + rtnValue + "원정";

            return rtnValue;
        }
        #endregion

        #region Description : 지급일 계산
        private string UP_getJIDATE(string sDATE)
        {
            int iWeek = 0;
            bool nReturn = true;
            string sWK_Date = "";
            string sYOIL = "";
            string sHUMU = "";
            string sSW = "";
            string sRetrun_Date = "";

            DateTime t1 = new DateTime();
            DateTime t2 = new DateTime();

            if (sDATE.Substring(4, 2) == "12")
            {
                sDATE = (Convert.ToInt32(sDATE.Substring(0, 4)) + 1).ToString() + "-01-10";
            }
            else
            {
                sDATE = sDATE.Substring(0, 4) + "-" + (Convert.ToInt32(sDATE.Substring(4, 2)) + 1).ToString() + "-10";
            }

            t1 = Convert.ToDateTime(sDATE);
            t2 = t1;
            sWK_Date = t2.Year + "-" + Set_Fill2(t2.Month.ToString()) + "-" + Set_Fill2(t2.Day.ToString());

            while (nReturn)
            {
                //요일
                iWeek = Convert.ToInt16(t2.DayOfWeek);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_29EAS015", sWK_Date.Substring(0, 4), sWK_Date.Substring(5, 2), sWK_Date.Substring(8, 2));
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    sYOIL = dt.Rows[0]["UYYOILCD"].ToString();
                    sHUMU = dt.Rows[0]["UYHUMUCD"].ToString();
                }
                else
                {
                    sYOIL = "";
                    sHUMU = "";
                }

                // 10 = 창립기념일 , 20=회사휴뮤 , 21=체육대회
                if (sHUMU.Trim() == "10" || sHUMU.Trim() == "20" || sHUMU.Trim() == "21" || sHUMU.Trim() == "")
                {
                    sSW = "";
                }
                else
                {
                    sSW = "*";
                }

                if ((iWeek == 6 || iWeek == 0) || (sSW == "*"))
                {
                    t1 = Convert.ToDateTime(sWK_Date);
                    t2 = t1.AddDays(+1);
                    sWK_Date = t2.Year + "-" + Set_Fill2(t2.Month.ToString()) + "-" + Set_Fill2(t2.Day.ToString());
                }
                else
                {
                    sRetrun_Date = sWK_Date;
                    nReturn = false;
                }
            } // End...While

            return sRetrun_Date;
        }
        #endregion

        #region Description : 원천세 집계표 스프레드 클릭 이벤트
        private void FPS91_TY_S_AC_4AH8Z197_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Content_Display(e.Row, e.Column, this.FPS91_TY_S_AC_4AH8Z197_Sheet1.Cells[e.Row, 2].Text.ToString(), this.FPS91_TY_S_AC_4AH8Z197_Sheet1.Cells[e.Row, 3].Text.ToString(), this.FPS91_TY_S_AC_4AH8Z197_Sheet1.Cells[e.Row, 13].Text.ToString());
        }
        #endregion


        #region Description : 세부내역 보여주기
        private void UP_Content_Display(int iRow, int iCol, string sPAYGUBN, string sContent1, string sContent2)
        {
            this.FPS91_TY_S_AC_4AH90198.Visible = false; // 근로소득
            this.FPS91_TY_S_AC_4ALFN226.Visible = false; // 근로소득:간이세액
            this.FPS91_TY_S_AC_4AL9G220.Visible = false; // 근로소득:중도퇴사
            this.FPS91_TY_S_AC_4ALH1227.Visible = false; // 근로소득:일용근로
            this.FPS91_TY_S_AC_4ALAY221.Visible = false; // 근로소득:연말정산
            this.FPS91_TY_S_AC_4ANEZ247.Visible = false; // 주민세종업원분
            this.FPS91_TY_S_AC_4ANF5248.Visible = false; // 주민세종업원분(세부)

            this.FPS91_TY_S_AC_4ALHV228.Visible = false; // 퇴직소득
            this.FPS91_TY_S_AC_BC1BZ848.Visible = false; // 퇴직소득(세부)
            this.FPS91_TY_S_AC_4ALID229.Visible = false; // 사업소득, 기타소득, 이자소득, 배당소득 - 1단계
            this.FPS91_TY_S_AC_4AMBT232.Visible = false; // 사업소득, 기타소득, 이자소득, 배당소득 - 2단계

            switch (iRow)
            {
                case 0:

                    if (sPAYGUBN.ToString() == "A10")
                    {
                        if (iCol == 3)
                        {
                            this.FPS91_TY_S_AC_4AH90198.Visible = true;  // 근로소득
                            this.FPS91_TY_S_AC_4ALFN226.Visible = true;  // 근로소득:간이세액

                            UP_Earned_display(); // 근로소득
                        }

                        if (iCol == 13)
                        {
                            this.FPS91_TY_S_AC_4ANEZ247.Visible = true;  // 주민세종업원분
                            this.FPS91_TY_S_AC_4ANF5248.Visible = true;  // 주민세종업원분(세부)

                            UP_Employee_display(); // 주민세종업원분 
                        }
                    }

                    break;

                case 1:

                    if (sPAYGUBN.ToString() == "A20")
                    {
                        this.FPS91_TY_S_AC_4ALHV228.Visible = true;  // 퇴직소득
                        this.FPS91_TY_S_AC_BC1BZ848.Visible = true;  // 퇴직소득(세부)

                        if (iCol == 3)
                        {
                            UP_Retirement_display();  // 퇴직소득
                        }
                    }

                    break;

                case 2:

                    if (sPAYGUBN.ToString() == "A30")
                    {
                        this.FPS91_TY_S_AC_4ALID229.Visible = true;   // 사업소득, 기타소득, 이자소득, 배당소득 - 1단계
                        this.FPS91_TY_S_AC_4AMBT232.Visible = true;   // 사업소득, 기타소득, 이자소득, 배당소득 - 2단계

                        if (iCol == 3)
                        {
                            UP_WIINCOM_Display(2); // 사업소득
                            //UP_Business_display(); // 사업소득
                        }
                    }

                    break;

                case 3:

                    if (sPAYGUBN.ToString() == "A40")
                    {
                        this.FPS91_TY_S_AC_4ALID229.Visible = true;  // 사업소득, 기타소득, 이자소득, 배당소득 - 1단계
                        this.FPS91_TY_S_AC_4AMBT232.Visible = true;  // 사업소득, 기타소득, 이자소득, 배당소득 - 2단계

                        if (iCol == 3)
                        {
                            UP_WIINCOM_Display(3); // 기타소득
                            //UP_Other_display(); // 기타소득
                        }
                    }

                    break;

                case 4:

                    if (sPAYGUBN.ToString() == "A50")
                    {
                        this.FPS91_TY_S_AC_4ALID229.Visible = true;  // 사업소득, 기타소득, 이자소득, 배당소득 - 1단계
                        this.FPS91_TY_S_AC_4AMBT232.Visible = true;  // 사업소득, 기타소득, 이자소득, 배당소득 - 2단계

                        if (iCol == 3)
                        {
                            UP_WIINCOM_Display(4); // 이자소득
                            //UP_Interests_display(); // 이자소득
                        }
                    }

                    break;

                case 5:

                    if (sPAYGUBN.ToString() == "A60")
                    {
                        this.FPS91_TY_S_AC_4ALID229.Visible = true;  // 사업소득, 기타소득, 이자소득, 배당소득 - 1단계
                        this.FPS91_TY_S_AC_4AMBT232.Visible = true;  // 사업소득, 기타소득, 이자소득, 배당소득 - 2단계

                        if (iCol == 3)
                        {
                            UP_WIINCOM_Display(5); // 배당소득
                            //UP_dividend_display(); // 배당소득
                        }
                    }

                    break;
            }
        }
        #endregion

        #region Description : 근로소득 집계표 내역
        private void UP_Earned_display()
        {
            UP_Spread_Earned_Title();

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_4AKIB209",
            //    fsBRANCH.ToString(),
            //    this.DTP01_WREYYMM.GetValue().ToString()
            //    );

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_BBTAS809",
                fsBRANCH.ToString(),
                this.DTP01_WREYYMM.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_4AH90198.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_AC_4AH90198.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_4AH90198.GetValue(i, "WPAYGUBNNM").ToString() == "계")
                {
                    this.FPS91_TY_S_AC_4AH90198.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_4AH90198.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }

            this.FPS91_TY_S_AC_4ALFN226.Initialize();  // 근로소득:간이세액
            this.FPS91_TY_S_AC_4AL9G220.Initialize();  // 근로소득:중도퇴사
            this.FPS91_TY_S_AC_4ALH1227.Initialize();  // 근로소득:일용근로
            this.FPS91_TY_S_AC_4ALAY221.Initialize();  // 근로소득:연말정산

        }
        #endregion
        #region Description : 근로소득 집계표 스프레드 클릭 이벤트
        private void FPS91_TY_S_AC_4AH90198_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Earned_Content_Display(e.Row, e.Column, this.FPS91_TY_S_AC_4AH90198_Sheet1.Cells[e.Row, 0].Text.ToString(), this.FPS91_TY_S_AC_4AH90198_Sheet1.Cells[e.Row, 1].Text.ToString());
        }
        #endregion
        #region Description : 근로소득 집계표 관리 세부조회 보여주기
        private void UP_Earned_Content_Display(int irow, int icol, string scontent1, string scontent2)
        {
            this.FPS91_TY_S_AC_4ALFN226.Visible = false;  // 근로소득:간이세액
            this.FPS91_TY_S_AC_4AL9G220.Visible = false; // 근로소득:중도퇴사
            this.FPS91_TY_S_AC_4ALH1227.Visible = false; // 근로소득:일용근로
            this.FPS91_TY_S_AC_4ALAY221.Visible = false; // 근로소득:연말정산

            switch (irow)
            {
                // 간이세액 
                case 0:
                    if (icol == 0)
                    {
                        this.FPS91_TY_S_AC_4ALFN226.Visible = true;  // 근로소득:간이세액

                        UP_Withholding_Display();
                    }
                    break;

                // 중도퇴사
                case 1:
                    if (icol == 0)
                    {
                        this.FPS91_TY_S_AC_4AL9G220.Visible = true;  // 근로소득:중도퇴사

                        UP_Middle_Display();
                    }
                    break;

                // 일용근로
                case 2:
                    if (icol == 0)
                    {
                        this.FPS91_TY_S_AC_4ALH1227.Visible = true;  // 근로소득:일용근로

                        UP_Daily_Display();
                    }
                    break;

                // 연말정산
                case 3:
                    if (icol == 0)
                    {
                        this.FPS91_TY_S_AC_4ALAY221.Visible = true;  // 근로소득:연말정산

                        UP_Yearend_Display();
                    }
                    break;
            }

        }
        #endregion


        #region Description : 근로소득 집계표 관리 세부조회(근로소득:간이세액)
        private void UP_Withholding_Display()
        {
            UP_Spread_Withholding_Title();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                //"TY_P_AC_4AKIB210",
                "TY_P_AC_C37CY128",
                fsBRANCH.ToString(),
                this.DTP01_WREYYMM.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_4ALFN226.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_AC_4ALFN226.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_4ALFN226.GetValue(i, "GIVENM").ToString() == "계")
                {
                    this.FPS91_TY_S_AC_4ALFN226.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_4ALFN226.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }
        }
        #endregion
        #region Description : 근로소득 집계표 관리 세부조회(근로소득:중도퇴사)
        private void UP_Middle_Display()
        {
            UP_Spread_Middle_Title();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4AKIC211",
                fsBRANCH.ToString(),
                this.DTP01_WREYYMM.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_4AL9G220.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_AC_4AL9G220.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_4AL9G220.GetValue(i, "W2GUNMUNM").ToString() == "계")
                {
                    this.FPS91_TY_S_AC_4AL9G220.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_4AL9G220.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }
        }
        #endregion
        #region Description : 근로소득 집계표 관리 세부조회(근로소득:일용근로)
        private void UP_Daily_Display()
        {
            UP_Spread_Daily_Title();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4AKID212",
                fsBRANCH.ToString(),
                this.DTP01_WREYYMM.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_4ALH1227.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_AC_4ALH1227.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_4ALH1227.GetValue(i, "GUBUN").ToString() == "계")
                {
                    this.FPS91_TY_S_AC_4ALH1227.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_4ALH1227.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }
        }
        #endregion
        #region Description : 근로소득 집계표 관리 세부조회(근로소득:연말정산)
        private void UP_Yearend_Display()
        {
            UP_Yearend_Title();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4AL8E213",
                fsBRANCH.ToString(),
                this.DTP01_WREYYMM.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_4ALAY221.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_AC_4ALAY221.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_4ALAY221.GetValue(i, "GUNMUNM").ToString() == "계")
                {
                    this.FPS91_TY_S_AC_4ALAY221.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_4ALAY221.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }
        }
        #endregion

        #region Description : 주민세 종업원분 내역
        private void UP_Employee_display()
        {
            this.FPS91_TY_S_AC_4ANEZ247.Initialize();  // 주민세종업원분
            this.FPS91_TY_S_AC_4ANF5248.Initialize();  // 주민세종업원분(세부)

            //// 주민세종업원분
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach
            //    (
            //    "TY_P_AC_4AL8H219",
            //    fsBRANCH.ToString(),
            //    this.DTP01_WREYYMM.GetValue().ToString()
            //    );

            // 주민세종업원분 - 21.11.30
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_BBTB5810",
                fsBRANCH.ToString(),
                this.DTP01_WREYYMM.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_4ANEZ247.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_AC_4ANEZ247.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_4ANEZ247.GetValue(i, "GUNMUNM").ToString() == "계")
                {
                    this.FPS91_TY_S_AC_4ANEZ247.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_4ANEZ247.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }

            // 주민세종업원분(세부)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4ANEQ246",
                fsBRANCH.ToString(),
                this.DTP01_WREYYMM.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_4ANF5248.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_AC_4ANF5248.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_4ANF5248.GetValue(i, "GUNMU").ToString() == "계")
                {
                    this.FPS91_TY_S_AC_4ANF5248.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_4ANF5248.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }

        }
        #endregion

        #region Description : 퇴직소득 집계표 관리 세부조회(퇴직소득:A22)
        private void UP_Retirement_display()
        {
            UP_Spread_Retirement_Title();

            // 퇴직소득 세부내역 하위 조회
            UP_Spread_Retirement_Down_Title();

            DataTable dt = new DataTable();

            // 퇴직소득
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4AL8E214",
                fsBRANCH.ToString(),
                this.DTP01_WREYYMM.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_4ALHV228.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_AC_4ALHV228.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_4ALHV228.GetValue(i, "GUBUN").ToString() == "계")
                {
                    this.FPS91_TY_S_AC_4ALHV228.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_4ALHV228.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }

            // 퇴직소득(세부)
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_BC1CS849",
                fsBRANCH.ToString(),
                this.DTP01_WREYYMM.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_BC1BZ848.SetValue(dt);

            this.FPS91_TY_S_AC_BC1BZ848.ActiveSheet.SetRowHeight(0, 30);

            for (int i = 0; i < this.FPS91_TY_S_AC_BC1BZ848.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_BC1BZ848.GetValue(i, "KBHANGL").ToString() == "계")
                {
                    this.FPS91_TY_S_AC_BC1BZ848.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_BC1BZ848.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }
        }
        #endregion

        #region Description : 사업소득 집계표 관리 세부조회(사업소득:A25)
        private void UP_WIINCOM_Display(int icode)
        {
            string sWINCOME = string.Empty;

            if (icode == 2)
            {
                sWINCOME = "A25";
            }
            else if (icode == 3)
            {
                sWINCOME = "A42";
            }
            else if (icode == 4)
            {
                sWINCOME = "A50";
            }
            else if (icode == 5)
            {
                sWINCOME = "A60";
            }

            UP_WIINCOM_First_Title();
            UP_WIINCOM_Second_Title();

            // 1단계
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_BBIFE755",
                fsBRANCH.ToString(),
                this.DTP01_WREYYMM.GetValue().ToString(),
                sWINCOME.ToString()
                );

            this.FPS91_TY_S_AC_4ALID229.SetValue(this.DbConnector.ExecuteDataTable());

            // 2단계
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_BBIFT756",
                fsBRANCH.ToString(),
                this.DTP01_WREYYMM.GetValue().ToString(),
                sWINCOME.ToString()
                );

            this.FPS91_TY_S_AC_4AMBT232.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_AC_4AMBT232.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_4AMBT232.GetValue(i, "GUNMU").ToString() == "계")
                {
                    this.FPS91_TY_S_AC_4AMBT232.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_4AMBT232.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }
        }
        #endregion

        #region Description : 사업소득 집계표 관리 세부조회(사업소득:A25)
        private void UP_Business_display()
        {
            this.FPS91_TY_S_AC_4ALID229.Visible = true;   // 사업소득, 기타소득, 이자소득, 배당소득 - 1단계
            this.FPS91_TY_S_AC_4AMBT232.Visible = true;   // 사업소득, 기타소득, 이자소득, 배당소득 - 2단계

            UP_Spread_Business_Title();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4AL8F215",
                fsBRANCH.ToString(),
                this.DTP01_WREYYMM.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_4ALID229.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_AC_4ALID229.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_4ALID229.GetValue(i, "KBHANGL").ToString() == "계")
                {
                    this.FPS91_TY_S_AC_4ALID229.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_4ALID229.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }
        }
        #endregion

        #region Description : 기타소득 집계표 관리 세부조회(기타소득:A42)
        private void UP_Other_display()
        {
            UP_Spread_Other_Title();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4AL8G216",
                fsBRANCH.ToString(),
                this.DTP01_WREYYMM.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_4ALHV228.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_AC_4ALHV228.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_4ALHV228.GetValue(i, "GBGIVE").ToString() == "계")
                {
                    this.FPS91_TY_S_AC_4ALHV228.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_4ALHV228.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }
        }
        #endregion

        #region Description : 이자소득 집계표 관리 세부조회(이자소득:A50)
        private void UP_Interests_display()
        {
            UP_Spread_Interests_dividend_Title();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4AL8G217",
                fsBRANCH.ToString(),
                this.DTP01_WREYYMM.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_4AMBT232.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_AC_4AMBT232.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_4AMBT232.GetValue(i, "GBINCOMNM").ToString() == "계")
                {
                    this.FPS91_TY_S_AC_4AMBT232.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_4AMBT232.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }
        }
        #endregion

        #region Description : 배당소득 집계표 관리 세부조회(배당소득:A60)
        private void UP_dividend_display()
        {
            UP_Spread_Interests_dividend_Title();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_4AL8G218",
                fsBRANCH.ToString(),
                this.DTP01_WREYYMM.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_4AMBT232.SetValue(this.DbConnector.ExecuteDataTable());

            for (int i = 0; i < this.FPS91_TY_S_AC_4AMBT232.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_AC_4AMBT232.GetValue(i, "GBINCOMNM").ToString() == "계")
                {
                    this.FPS91_TY_S_AC_4AMBT232.ActiveSheet.Rows[i].ForeColor = Color.Red;
                    this.FPS91_TY_S_AC_4AMBT232.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                }
            }
        }
        #endregion


        // 스프레드
        #region Description : 스프레드 타이틀 변경(원전체 집계표 조회)
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.RowHeaderColumnCount = 1;

            //(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1); // 소득구분
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1); // 인원
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1); // 지급액
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.AddColumnHeaderSpanCell(0, 13, 2, 1); // 주민세종업원분

            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 2); // 원천징수액
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 2); // 당월조정환급세액
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.AddColumnHeaderSpanCell(0, 10, 1, 3); // 납부세액

            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[0, 3].Value = "소득구분";
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[0, 4].Value = "인원";
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[0, 5].Value = "지급액";
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[0, 13].Value = "주민세종업원분";

            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[0, 6].Value = "원천징수액";
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[0, 8].Value = "당월조정환급세액";
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[0, 10].Value = "납부세액";


            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[1, 3].Value = "";
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[1, 4].Value = "";
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[1, 5].Value = "";
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[1, 13].Value = "";

            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[1, 6].Value = "국세";
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[1, 7].Value = "지방세";

            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[1, 8].Value = "국세";
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[1, 9].Value = "지방세";

            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[1, 10].Value = "국세";
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[1, 11].Value = "지방세";
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[1, 12].Value = "세액계";


            if (this.FPS91_TY_S_AC_4AH8Z197_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_4AH8Z197_Sheet1.AlternatingRows[0].BackColor = Color.White;
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AH8Z197_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;



        }
        #endregion

        #region Description : 스프레드 타이틀 변경(근로소득 집계 조회)
        private void UP_Spread_Earned_Title()
        {
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_4AH90198_Sheet1.RowHeaderColumnCount = 1;

            //(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            this.FPS91_TY_S_AC_4AH90198_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1); // 소득구분

            this.FPS91_TY_S_AC_4AH90198_Sheet1.AddColumnHeaderSpanCell(0, 1, 1, 5); // 울산
            this.FPS91_TY_S_AC_4AH90198_Sheet1.AddColumnHeaderSpanCell(0, 6, 1, 5); // 서울

            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[0, 0].Value  = "구분";
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[0, 1].Value  = "울산";
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[0, 6].Value  = "서울";

            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 1].Value  = "인원";
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 2].Value  = "지급액";
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 3].Value  = "소득세";
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 4].Value  = "농특세";
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 5].Value  = "주민세";
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 6].Value  = "인원";
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 7].Value  = "지급액";
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 8].Value  = "소득세";
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 9].Value  = "농특세";
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 10].Value = "주민세";

            if (this.FPS91_TY_S_AC_4AH90198_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_4AH90198_Sheet1.AlternatingRows[0].BackColor = Color.White;
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 1].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 2].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 3].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AH90198_Sheet1.ColumnHeader.Cells[1, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }
        #endregion

        #region Description : 스프레드 타이틀 변경(근로소득 집계 조회 --> 근로소득:간이세액)
        private void UP_Spread_Withholding_Title()
        {
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.RowHeaderColumnCount = 1;

            //(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1); // 소득구분

            this.FPS91_TY_S_AC_4ALFN226_Sheet1.AddColumnHeaderSpanCell(0, 1, 1, 4); // 울산
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 4); // 서울

            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[0, 0].Value = "구분";
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[0, 1].Value = "울산";
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[0, 5].Value = "서울";

            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[1, 1].Value = "인원";
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[1, 2].Value = "지급액";
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[1, 3].Value = "소득세";
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[1, 4].Value = "주민세";
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[1, 5].Value = "인원";
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[1, 6].Value = "지급액";
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[1, 7].Value = "소득세";
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[1, 8].Value = "주민세";

            if (this.FPS91_TY_S_AC_4ALFN226_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_4ALFN226_Sheet1.AlternatingRows[0].BackColor = Color.White;

            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALFN226_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }
        #endregion
        #region Description : 스프레드 타이틀 변경(근로소득 집계 조회 --> 근로소득:중도퇴사)
        private void UP_Spread_Middle_Title()
        {
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.RowHeaderColumnCount = 1;

            //(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1); // 구분
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1); // 근무지역
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1); // 성명
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1); // 담당부서
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1); // 급여정산여부
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.AddColumnHeaderSpanCell(0, 5, 2, 1); // 1.급상여지급액
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.AddColumnHeaderSpanCell(0, 6, 2, 1); // 2.임원한도초과액
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.AddColumnHeaderSpanCell(0, 7, 2, 1); // 3.총지급액(1+2)
            

            this.FPS91_TY_S_AC_4AL9G220_Sheet1.AddColumnHeaderSpanCell(0, 8, 1, 2);  // 결정세액
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.AddColumnHeaderSpanCell(0, 10, 1, 2); // 기납부세액
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.AddColumnHeaderSpanCell(0, 12, 1, 2); // 차가감징수세액

            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 0].Value  = "구분";
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 1].Value  = "근무지역";
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 2].Value  = "성명";
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 3].Value  = "담당부서";
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 4].Value  = "급여정산여부";

            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 5].Value  = "1.급상여지급액";
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 6].Value  = "2.임원한도초과액";
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 7].Value  = "3.총지급액(1+2)";
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 8].Value  = "결정세액";
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 10].Value = "기납부세액";
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 12].Value = "차가감징수세액";

            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[1, 8].Value  = "소득세";
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[1, 9].Value  = "주민세";
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[1, 10].Value = "소득세";
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[1, 11].Value = "주민세";
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[1, 12].Value = "소득세";
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[1, 13].Value = "주민세";

            if (this.FPS91_TY_S_AC_4AL9G220_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_4AL9G220_Sheet1.AlternatingRows[0].BackColor = Color.White;

            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 6].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 8].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[0, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[1, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[1, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[1, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[1, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AL9G220_Sheet1.ColumnHeader.Cells[1, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description : 스프레드 타이틀 변경(근로소득 집계 조회 --> 근로소득:일용근로)
        private void UP_Spread_Daily_Title()
        {
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.RowHeaderColumnCount = 1;

            //(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1); // 구분
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1); // 성명
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1); // 적요
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1); // 귀속부서

            this.FPS91_TY_S_AC_4ALH1227_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 3); // 소득금액
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 2); // 세액

            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[0, 0].Value = "구분";
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[0, 1].Value = "성명";
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[0, 2].Value = "적요";
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[0, 3].Value = "귀속부서";

            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[0, 4].Value = "소득금액";

            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[0, 7].Value = "세 액";

            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[1, 4].Value = "지급총액";
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[1, 5].Value = "필요경비";
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[1, 6].Value = "소득금액";
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[1, 7].Value = "소득세";
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[1, 8].Value = "주민세";

            if (this.FPS91_TY_S_AC_4ALH1227_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_4ALH1227_Sheet1.AlternatingRows[0].BackColor = Color.White;

            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALH1227_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }
        #endregion
        #region Description : 스프레드 타이틀 변경(근로소득 집계 조회 --> 근로소득:연말정산)
        private void UP_Yearend_Title()
        {
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.RowHeaderColumnCount = 1;

            //(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1); // 구분
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1); // 인원
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1); // 지급액

            this.FPS91_TY_S_AC_4ALAY221_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 4); // 결정세액
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 4); // 기부납부세액
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.AddColumnHeaderSpanCell(0, 13, 1, 4); // 차감원천징수세액

            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[0, 2].Value = "구분";
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[0, 3].Value = "인원";
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[0, 4].Value = "총 급여액";
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[0, 5].Value = "결정세액";
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[0, 9].Value = "기부납부세액";
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[0, 13].Value = "차가감원천징수세액";

            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 5].Value = "소득세";
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 6].Value = "농특세";
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 7].Value = "주민세";
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 8].Value = "계";

            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 9].Value  = "소득세";
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 10].Value = "농특세";
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 11].Value = "주민세";
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 12].Value = "계";
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 13].Value = "소득세";
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 14].Value = "농특세";
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 15].Value = "주민세";
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 16].Value = "계";

            if (this.FPS91_TY_S_AC_4ALAY221_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_4ALAY221_Sheet1.AlternatingRows[0].BackColor = Color.White;

            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[0, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 13].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 14].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALAY221_Sheet1.ColumnHeader.Cells[1, 16].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            

        }
        #endregion

        #region Description : 스프레드 타이틀 변경(퇴직소득 집계 조회 --> 퇴직소득:A22)
        private void UP_Spread_Retirement_Title()
        {
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.RowHeaderColumnCount = 1;

            //(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1); // 퇴직구분
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.AddColumnHeaderSpanCell(0, 1, 1, 4); // 울산
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 4); // 서울

            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[0, 0].Value = "구분";
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[0, 1].Value = "울산";
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[0, 5].Value = "서울";

            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 1].Value = "인원";
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 2].Value = "지급액";
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 3].Value = "소득세";
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 4].Value = "주민세";

            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 5].Value = "인원";
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 6].Value = "지급액";
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 7].Value = "소득세";
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 8].Value = "주민세";

            if (this.FPS91_TY_S_AC_4ALHV228_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_4ALHV228_Sheet1.AlternatingRows[0].BackColor = Color.White;

            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }
        #endregion

        #region Description : 스프레드 타이틀 변경(퇴직소득 세부내역 하위 조회 --> 퇴직소득:A22)
        private void UP_Spread_Retirement_Down_Title()
        {
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.RowHeaderColumnCount = 1;

            //(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);  // 성명
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);  // 지급계좌
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.AddColumnHeaderSpanCell(0, 2, 1, 5);  // 소득금액
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 2);  // 산출세액
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.AddColumnHeaderSpanCell(0, 9, 1, 2);  // 이연세액
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.AddColumnHeaderSpanCell(0, 11, 1, 2); // 납부세액

            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[0, 0].Value  = "성명";
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[0, 1].Value  = "지급계좌";
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[0, 2].Value  = "소득금액";
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[0, 7].Value  = "산축세액";
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[0, 9].Value  = "이연세액";
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[0, 11].Value = "납부세액";

            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 2].Value  = "퇴직금지급총액";
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 3].Value  = "임원퇴직소득한도내";
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 4].Value  = "임원퇴직소득한도초과";
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 5].Value  = "퇴직소득금액";
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 6].Value  = "이연계좌지급액";
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 7].Value  = "소득세";
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 8].Value  = "주민세";
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 9].Value  = "소득세";
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 10].Value = "주민세";
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 11].Value = "소득세";
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 12].Value = "주민세";

            if (this.FPS91_TY_S_AC_BC1BZ848_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_BC1BZ848_Sheet1.AlternatingRows[0].BackColor = Color.White;

            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[0, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[0, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 2].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 3].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 9].HorizontalAlignment  = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 10].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 11].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_BC1BZ848_Sheet1.ColumnHeader.Cells[1, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }
        #endregion

        #region Description : 스프레드 타이틀 변경(사업,기타,이자,배당 집계 조회)
        private void UP_WIINCOM_First_Title()
        {
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.RowHeaderColumnCount = 1;

            //(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            this.FPS91_TY_S_AC_4ALID229_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1); // 구분
            this.FPS91_TY_S_AC_4ALID229_Sheet1.AddColumnHeaderSpanCell(0, 1, 1, 4); // 울산
            this.FPS91_TY_S_AC_4ALID229_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 4); // 서울

            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[0, 0].Value = "구분";
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[0, 1].Value = "울산";
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[0, 5].Value = "서울";

            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 1].Value = "인원";
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 2].Value = "지급액";
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 3].Value = "소득세";
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 4].Value = "주민세";

            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 5].Value = "인원";
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 6].Value = "지급액";
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 7].Value = "소득세";
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 8].Value = "주민세";

            if (this.FPS91_TY_S_AC_4ALID229_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_4ALID229_Sheet1.AlternatingRows[0].BackColor = Color.White;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description : 스프레드 타이틀 변경(사업,기타,이자,배당 집계 상세 조회)
        private void UP_WIINCOM_Second_Title()
        {
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.RowHeaderColumnCount = 1;

            //(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1); // 지역
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1); // 성명
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1); // 적요
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1); // 귀속부서

            this.FPS91_TY_S_AC_4AMBT232_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 3); // 소득금액
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 2); // 세액

            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[0, 0].Value = "지역";
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[0, 1].Value = "성명";
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[0, 2].Value = "적요";
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[0, 3].Value = "귀속부서";
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[0, 4].Value = "소득금액";
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[0, 7].Value = "세액";

            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[1, 4].Value = "지급총액";
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[1, 5].Value = "필요경비";
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[1, 6].Value = "소득금액";
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[1, 7].Value = "소득세";
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[1, 8].Value = "주민세";

            if (this.FPS91_TY_S_AC_4AMBT232_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_4AMBT232_Sheet1.AlternatingRows[0].BackColor = Color.White;

            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[0, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[0, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description : 스프레드 타이틀 변경(사업소득 집계 조회 --> 사업소득:A25)
        private void UP_Spread_Business_Title()
        {
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.RowHeaderColumnCount = 1;

            //(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            this.FPS91_TY_S_AC_4ALID229_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1); // 구분
            this.FPS91_TY_S_AC_4ALID229_Sheet1.AddColumnHeaderSpanCell(0, 1, 1, 4); // 울산
            this.FPS91_TY_S_AC_4ALID229_Sheet1.AddColumnHeaderSpanCell(0, 1, 5, 4); // 서울

            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[0, 0].Value = "구분";
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[0, 1].Value = "울산";
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[0, 5].Value = "서울";

            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 1].Value = "인원";
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 2].Value = "지급액";
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 3].Value = "소득세";
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 4].Value = "주민세";

            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 5].Value = "인원";
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 6].Value = "지급액";
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 7].Value = "소득세";
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 8].Value = "주민세";

            if (this.FPS91_TY_S_AC_4ALID229_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_4ALID229_Sheet1.AlternatingRows[0].BackColor = Color.White;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[0, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 6].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_AC_4ALID229_Sheet1.ColumnHeader.Cells[1, 8].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region Description : 스프레드 타이틀 변경(기타소득 집계 조회 --> 기타소득:A42)
        private void UP_Spread_Other_Title()
        {
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.RowHeaderColumnCount = 1;

            //(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1); // 소득구분
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1); // 성명
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1); // 사업부
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1); // 인원
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1); // 지급액
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 3); // 원천징수세액

            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[0, 0].Value = "소득구분";
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[0, 1].Value = "성명";
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[0, 2].Value = "사업부";
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[0, 3].Value = "인원";
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[0, 4].Value = "지급액";

            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[0, 5].Value = "원천징수세액";

            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 0].Value = "";
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 1].Value = "";
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 2].Value = "";
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 3].Value = "";
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 4].Value = "";

            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 5].Value = "소득세";
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 6].Value = "지방소득세";
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[1, 7].Value = "세액계";

            if (this.FPS91_TY_S_AC_4ALHV228_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_4ALHV228_Sheet1.AlternatingRows[0].BackColor = Color.White;
            this.FPS91_TY_S_AC_4ALHV228_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }
        #endregion

        #region Description : 스프레드 타이틀 변경(이자,배당소득 집계 조회 --> 이자소득:A50 ,배당소득:A60)
        private void UP_Spread_Interests_dividend_Title()
        {
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.RowHeaderColumnCount = 1;

            //(현재ROW, 현재COLUMN, 묶을ROW수, 묶을 COLUMN수)
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1); // 소득자구분
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1); // 성명
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1); // 인원
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1); // 지급액
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 3); // 원천징수세액

            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[0, 0].Value = "소득자구분";
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[0, 1].Value = "성명/상호";
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[0, 2].Value = "인원";
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[0, 3].Value = "지급액";

            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[0, 4].Value = "원천징수세액";

            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[1, 0].Value = "";
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[1, 1].Value = "";
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[1, 2].Value = "";
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[1, 3].Value = "";

            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[1, 4].Value = "소득세";
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[1, 5].Value = "지방소득세";
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[1, 6].Value = "세액계";

            if (this.FPS91_TY_S_AC_4AMBT232_Sheet1.AlternatingRows.Count > 0)
                this.FPS91_TY_S_AC_4AMBT232_Sheet1.AlternatingRows[0].BackColor = Color.White;
            this.FPS91_TY_S_AC_4AMBT232_Sheet1.ColumnHeader.Cells[0, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

        }
        #endregion

    }
}