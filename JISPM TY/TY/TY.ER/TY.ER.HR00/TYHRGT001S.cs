using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Globalization;

namespace TY.ER.HR00
{
    /// <summary>
    /// 월력관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.11.28 17:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BSC3589 : 근태월력 일자별 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY4BSDY590 : 월력관리 조회
    ///  TY4BSDY591 : 월력관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CREATE : 생성
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  YYYYMM : 기준 년월
    /// </summary>
    public partial class TYHRGT001S : TYBase
    {
        private static KoreanLunisolarCalendar _calendar = new KoreanLunisolarCalendar();

        private int _year;				// 음력 년
        private int _month;				// 음력 월
        private int _day;				// 음력 일
        private bool _isLeapMonth;		// 윤달 여부

        private DateTime _solarDate;	// 양력 년월일

        #region Description : 페이지 로드
        public TYHRGT001S()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4C1A3605, "SYYOILCD", "SYYOILCDNM", "SYYOILCD");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4C1A3605, "SYHUMUCD", "SYHUMUCDNM", "SYHUMUCD");
        }

        private void TYHRGT001S_Load(object sender, System.EventArgs e)
        {
            // Key필드 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4C1A3605, "SYDATE");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4C1A3605, "SYCALGUBN");
            // 등록 체크
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.FPS91_TY_S_HR_4C1A3605.Initialize();

            this.DTP01_YYYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_YYYYMM);

        }
        #endregion

        #region Description : 생성 버튼 이벤트
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            if ((new TYHRGT001B()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_HR_4BSC3589",
                DTP01_YYYYMM.GetString().Substring(0,4),
                DTP01_YYYYMM.GetString().Substring(4,2)
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_HR_4C1A4606.SetValue(dt);     

                this.DbConnector.CommandClear();

                this.DbConnector.Attach
                    (
                    "TY_P_HR_4C1B1609",
                    DTP01_YYYYMM.GetString().Substring(0,4)
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_HR_4C1A3605.SetValue(dt);
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_HR_4C2FO628", dt.Rows[i]["SYYEAR"],
                                                            dt.Rows[i]["SYMONTH"],
                                                            dt.Rows[i]["SYDAY"],
                                                            dt.Rows[i]["SYCALGUBN"]);

                // 음력 삭제
                if (dt.Rows[i]["SYCALGUBN"].ToString() == "2")
                {
                    // 휴무코드가 설날인 경우
                    if (dt.Rows[i]["SYHUMUCD"].ToString() == "02")
                    {
                        ConvertFromSolarDate(Convert.ToDateTime(dt.Rows[i]["SYYEAR"].ToString() + "-01-01"));    // 윤달체크
                        LunisolarDate(Convert.ToInt16(dt.Rows[i]["SYYEAR"].ToString()) - 1, 12, 30, _isLeapMonth);   // 연휴

                        this.DbConnector.Attach("TY_P_HR_4C2FQ629", "",
                                                                    _solarDate.ToString("yyyy"),
                                                                    _solarDate.ToString("MM"),
                                                                    _solarDate.ToString("dd"));

                        LunisolarDate(Convert.ToInt16(dt.Rows[i]["SYYEAR"].ToString()), 01, 02, _isLeapMonth);   // 연휴

                        this.DbConnector.Attach("TY_P_HR_4C2FQ629", "",
                                                                    _solarDate.ToString("yyyy"),
                                                                    _solarDate.ToString("MM"),
                                                                    _solarDate.ToString("dd"));
                    }
                    // 휴무코드가 추석인 경우
                    else if (dt.Rows[i]["SYHUMUCD"].ToString() == "12")
                    {
                        ConvertFromSolarDate(Convert.ToDateTime(dt.Rows[i]["SYYEAR"].ToString() + "-08-15"));    // 윤달체크
                        LunisolarDate(Convert.ToInt16(dt.Rows[i]["SYYEAR"].ToString()), 8, 14, _isLeapMonth);   // 연휴

                        this.DbConnector.Attach("TY_P_HR_4C2FQ629", "",
                                                                    _solarDate.ToString("yyyy"),
                                                                    _solarDate.ToString("MM"),
                                                                    _solarDate.ToString("dd"));

                        LunisolarDate(Convert.ToInt16(dt.Rows[i]["SYYEAR"].ToString()), 8, 16, _isLeapMonth);   // 연휴

                        this.DbConnector.Attach("TY_P_HR_4C2FQ629", "",
                                                                    _solarDate.ToString("yyyy"),
                                                                    _solarDate.ToString("MM"),
                                                                    _solarDate.ToString("dd"));
                    }

                    ConvertFromSolarDate(Convert.ToDateTime(dt.Rows[i]["SYYEAR"].ToString() + "-" + dt.Rows[i]["SYMONTH"].ToString() + "-" + dt.Rows[i]["SYDAY"].ToString()));    //윤달체크
                    LunisolarDate(Convert.ToInt16(dt.Rows[i]["SYYEAR"].ToString()), Convert.ToInt16(dt.Rows[i]["SYMONTH"].ToString()), Convert.ToInt16(dt.Rows[i]["SYDAY"].ToString()), _isLeapMonth);   //양력 가져오기

                    this.DbConnector.Attach("TY_P_HR_4C2FQ629", "",
                                                                _solarDate.ToString("yyyy"),
                                                                _solarDate.ToString("MM"),
                                                                _solarDate.ToString("dd")
                                                                ); 
                }
                else
                {
                    this.DbConnector.Attach("TY_P_HR_4C2FQ629", "",
                                                                dt.Rows[i]["SYYEAR"],
                                                                dt.Rows[i]["SYMONTH"],
                                                                dt.Rows[i]["SYDAY"]
                                                                );
                                                                
                }
            }
            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            #region Description : 등록
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_HR_4C2DU626", ds.Tables[0].Rows[i]["SYYEAR"],
                                                            ds.Tables[0].Rows[i]["SYMONTH"],
                                                            ds.Tables[0].Rows[i]["SYDAY"],
                                                            ds.Tables[0].Rows[i]["SYCALGUBN"],
                                                            ds.Tables[0].Rows[i]["SYYOILCD"],
                                                            ds.Tables[0].Rows[i]["SYHUMUCD"],
                                                            TYUserInfo.EmpNo
                                                            ); //신규
                
                // 음력 등록
                if (ds.Tables[0].Rows[i]["SYCALGUBN"].ToString() == "2")
                {
                    ConvertFromSolarDate(Convert.ToDateTime(ds.Tables[0].Rows[i]["SYYEAR"] + "-" + ds.Tables[0].Rows[i]["SYMONTH"] + "-" + ds.Tables[0].Rows[i]["SYDAY"]));    //윤달체크
                    LunisolarDate(Convert.ToInt16(ds.Tables[0].Rows[i]["SYYEAR"]), Convert.ToInt16(ds.Tables[0].Rows[i]["SYMONTH"]), Convert.ToInt16(ds.Tables[0].Rows[i]["SYDAY"]), _isLeapMonth);   //양력 가져오기

                    this.DbConnector.Attach("TY_P_HR_4C2FQ629", ds.Tables[0].Rows[i]["SYHUMUCD"],
                                                                _solarDate.ToString("yyyy"),
                                                                _solarDate.ToString("MM"),
                                                                _solarDate.ToString("dd"));

                    DateTime Date = _solarDate;

                    // 휴무코드가 설날인 경우
                    if (ds.Tables[0].Rows[i]["SYHUMUCD"].ToString() == "02")
                    {
                        Date = _solarDate.AddDays(-1);

                        this.DbConnector.Attach("TY_P_HR_4C2FQ629", "16",
                                                                    Date.ToString("yyyy"),
                                                                    Date.ToString("MM"),
                                                                    Date.ToString("dd"));

                        Date = _solarDate.AddDays(+1);

                        this.DbConnector.Attach("TY_P_HR_4C2FQ629", "16",
                                                                    Date.ToString("yyyy"),
                                                                    Date.ToString("MM"),
                                                                    Date.ToString("dd"));
                    }
                    // 휴무코드가 추석인 경우
                    else if (ds.Tables[0].Rows[i]["SYHUMUCD"].ToString() == "12")
                    {
                        Date = _solarDate.AddDays(-1);

                        this.DbConnector.Attach("TY_P_HR_4C2FQ629", "16",
                                                                    Date.ToString("yyyy"),
                                                                    Date.ToString("MM"),
                                                                    Date.ToString("dd"));

                        Date = _solarDate.AddDays(+1);

                        this.DbConnector.Attach("TY_P_HR_4C2FQ629", "16",
                                                                    Date.ToString("yyyy"),
                                                                    Date.ToString("MM"),
                                                                    Date.ToString("dd"));
                    }
                    
                    
                }
                // 양력 등록
                else
                {
                    this.DbConnector.Attach("TY_P_HR_4C2FQ629", ds.Tables[0].Rows[i]["SYHUMUCD"],
                                                                ds.Tables[0].Rows[i]["SYYEAR"],
                                                                ds.Tables[0].Rows[i]["SYMONTH"],
                                                                ds.Tables[0].Rows[i]["SYDAY"]);     
                }
            }
            #endregion
            #region Description : 수정
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_HR_4C1FZ620", ds.Tables[1].Rows[i]["SYYOILCD"],
                                                            ds.Tables[1].Rows[i]["SYHUMUCD"],
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["SYYEAR"],
                                                            ds.Tables[1].Rows[i]["SYMONTH"],
                                                            ds.Tables[1].Rows[i]["SYDAY"],
                                                            ds.Tables[1].Rows[i]["SYCALGUBN"]); //수정

                // 음력인 경우
                if (ds.Tables[1].Rows[i]["SYCALGUBN"].ToString() == "2")
                {
                    ConvertFromSolarDate(Convert.ToDateTime(ds.Tables[1].Rows[i]["SYYEAR"] + "-" + ds.Tables[1].Rows[i]["SYMONTH"] + "-" + ds.Tables[1].Rows[i]["SYDAY"]));    //윤달체크
                    LunisolarDate(Convert.ToInt16(ds.Tables[1].Rows[i]["SYYEAR"]), Convert.ToInt16(ds.Tables[1].Rows[i]["SYMONTH"]), Convert.ToInt16(ds.Tables[1].Rows[i]["SYDAY"]), _isLeapMonth);   //양력 가져오기

                    this.DbConnector.Attach("TY_P_HR_4C2FQ629", ds.Tables[1].Rows[i]["SYHUMUCD"],
                                                                _solarDate.ToString("yyyy"),
                                                                _solarDate.ToString("MM"),
                                                                _solarDate.ToString("dd")); 
                }
                else
                {
                    this.DbConnector.Attach("TY_P_HR_4C2FQ629", ds.Tables[1].Rows[i]["SYHUMUCD"],
                                                                ds.Tables[1].Rows[i]["SYYEAR"],
                                                                ds.Tables[1].Rows[i]["SYMONTH"],
                                                                ds.Tables[1].Rows[i]["SYDAY"]
                                                                ); 
                }
            }
            #endregion

            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            
            DataSet ds = new DataSet();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_4C1A3605.GetDataSourceInclude(TSpread.TActionType.New, "SYYEAR", "SYMONTH", "SYDAY", "SYYOILCD", "SYHUMUCD", "SYCALGUBN"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_4C1A3605.GetDataSourceInclude(TSpread.TActionType.Update, "SYYEAR", "SYMONTH", "SYDAY", "SYYOILCD", "SYHUMUCD", "SYCALGUBN"));

            //신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_HR_4C1BX616",
                                       ds.Tables[0].Rows[i]["SYYEAR"].ToString(),
                                       ds.Tables[0].Rows[i]["SYMONTH"].ToString(),
                                       ds.Tables[0].Rows[i]["SYDAY"].ToString(),
                                       ds.Tables[0].Rows[i]["SYCALGUBN"].ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_3219C986");
                    e.Successed = false;
                    return;
                }
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    if (i != j)
                    {
                        if (ds.Tables[0].Rows[i]["SYYEAR"].ToString() == ds.Tables[0].Rows[j]["SYYEAR"].ToString() && ds.Tables[0].Rows[i]["SYMONTH"].ToString() == ds.Tables[0].Rows[j]["SYMONTH"].ToString() &&
                            ds.Tables[0].Rows[i]["SYDAY"].ToString() == ds.Tables[0].Rows[j]["SYDAY"].ToString() && ds.Tables[0].Rows[i]["SYCALGUBN"].ToString() == ds.Tables[0].Rows[j]["SYCALGUBN"].ToString())
                        {
                            this.ShowMessage("TY_M_AC_3219C986");
                            e.Successed = false;
                            return;
                        }
                    }
                }
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_4C1A3605.GetDataSourceInclude(TSpread.TActionType.Remove, "SYYEAR", "SYMONTH", "SYDAY", "SYHUMUCD", "SYCALGUBN");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 그리드 날짜 입력 이벤트
        private void FPS91_TY_S_HR_4C1A3605_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            string sDate = this.FPS91_TY_S_HR_4C1A3605.GetValue("SYDATE").ToString();
            string sGubn = this.FPS91_TY_S_HR_4C1A3605.GetValue("SYCALGUBN").ToString();

            if (sDate.Length >= 8)
            {
                DateTime dateValue = new DateTime(Convert.ToInt16(sDate.Substring(0, 4)), Convert.ToInt16(sDate.Substring(4, 2)), Convert.ToInt16(sDate.Substring(6, 2)));

                this.FPS91_TY_S_HR_4C1A3605.SetValue("SYYEAR", sDate.Substring(0, 4));
                this.FPS91_TY_S_HR_4C1A3605.SetValue("SYMONTH", sDate.Substring(4, 2));
                this.FPS91_TY_S_HR_4C1A3605.SetValue("SYDAY", sDate.Substring(6, 2));

                if (sGubn == "2")
                {
                    ConvertFromSolarDate(dateValue);    //윤달체크
                    LunisolarDate(Convert.ToInt16(dateValue.ToString("yyyy")), Convert.ToInt16(dateValue.ToString("MM")), Convert.ToInt16(dateValue.ToString("dd")), _isLeapMonth);   //양력 가져오기

                    dateValue = _solarDate;
                }

                if (dateValue.ToString("ddd") == "일")
                {
                    this.FPS91_TY_S_HR_4C1A3605.SetValue("SYYOILCD", "1");
                    this.FPS91_TY_S_HR_4C1A3605.SetValue("SYYOILCDNM", "일요일");
                }
                else if (dateValue.ToString("ddd") == "월")
                {
                    this.FPS91_TY_S_HR_4C1A3605.SetValue("SYYOILCD", "2");
                    this.FPS91_TY_S_HR_4C1A3605.SetValue("SYYOILCDNM", "월요일");
                }
                else if (dateValue.ToString("ddd") == "화")
                {
                    this.FPS91_TY_S_HR_4C1A3605.SetValue("SYYOILCD", "3");
                    this.FPS91_TY_S_HR_4C1A3605.SetValue("SYYOILCDNM", "화요일");
                }
                else if (dateValue.ToString("ddd") == "수")
                {
                    this.FPS91_TY_S_HR_4C1A3605.SetValue("SYYOILCD", "4");
                    this.FPS91_TY_S_HR_4C1A3605.SetValue("SYYOILCDNM", "수요일");
                }
                else if (dateValue.ToString("ddd") == "목")
                {
                    this.FPS91_TY_S_HR_4C1A3605.SetValue("SYYOILCD", "5");
                    this.FPS91_TY_S_HR_4C1A3605.SetValue("SYYOILCDNM", "목요일");
                }
                else if (dateValue.ToString("ddd") == "금")
                {
                    this.FPS91_TY_S_HR_4C1A3605.SetValue("SYYOILCD", "6");
                    this.FPS91_TY_S_HR_4C1A3605.SetValue("SYYOILCDNM", "금요일");
                }
                else if (dateValue.ToString("ddd") == "토")
                {
                    this.FPS91_TY_S_HR_4C1A3605.SetValue("SYYOILCD", "7");
                    this.FPS91_TY_S_HR_4C1A3605.SetValue("SYYOILCDNM", "토요일");
                }
            }
            else
            {
                this.FPS91_TY_S_HR_4C1A3605.SetValue("SYDATE", "");
            }
        }
        #endregion

        #region Description : 음력 -> 양력 변환
        public void LunisolarDate(int year, int month, int day, bool isLeapMonth)
        {
            _year = year;
            _month = month;
            _day = day;

            if (_calendar.GetMonthsInYear(year) > 12)
            {
                int leapMonth = _calendar.GetLeapMonth(year);

                if (month >= leapMonth - 1)
                {
                    _isLeapMonth = ((month + 1) == leapMonth && isLeapMonth);
                    _solarDate = _calendar.ToDateTime(year, month + 1, day, 0, 0, 0, 0);
                }
                else
                {
                    _solarDate = _calendar.ToDateTime(year, month, day, 0, 0, 0, 0);
                }
            }
            else
            {   
                _solarDate = _calendar.ToDateTime(year, month, day, 0, 0, 0, 0);
            }
        }
        #endregion

        #region Description : 윤달여부 체크
        public void ConvertFromSolarDate(DateTime solarDate)
        {
            DateTime date = solarDate;					// 양력 년월일
            int year  = _calendar.GetYear(solarDate);		// 음력 년
            int month = _calendar.GetMonth(solarDate);	// 음력 월
            int day = _calendar.GetDayOfMonth(solarDate);	// 음력 일

            // 윤달 체크
            if (_calendar.GetMonthsInYear(year) > 12)
            {
                int leapMonth = _calendar.GetLeapMonth(date.Year);

                // 윤달보다 크거나 같으면 달수가 1씩 더해지므로 이를 재조정 함.
                if (date.Month >= leapMonth)
                {
                    _isLeapMonth = (date.Month == leapMonth);
                    _month--;
                }
            }
        }
        #endregion
    }
}
