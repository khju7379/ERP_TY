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
    /// 근태월력 생성 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.12.01 17:20
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4C2BB622 : 근태월력 생성 (년단위)
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  CREATE : 생성
    ///  VSYEAR : 기준년도
    /// </summary>
    public partial class TYHRGT001B : TYBase
    {
        private static KoreanLunisolarCalendar _calendar = new KoreanLunisolarCalendar();

        private int _year;				// 음력 년
        private int _month;				// 음력 월
        private int _day;				// 음력 일
        private bool _isLeapMonth;		// 윤달 여부

        private DateTime _solarDate;	// 양력 년월일

        #region Description : 페이지 로드
        public TYHRGT001B()
        {
            InitializeComponent();
        }

        private void TYHRGT001B_Load(object sender, System.EventArgs e)
        {
            // 생성 체크
            this.BTN61_CREATE.ProcessCheck += new TButton.CheckHandler(BTN61_CREATE_ProcessCheck);

            TXT01_VSYEAR.SetValue(DateTime.Now.ToString("yyyy"));
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 생성 버튼 이벤트
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            try
            {
                
                this.DbConnector.CommandClear();

                // TYFILELIB.GTMRREF 년단위 생성
                this.DbConnector.Attach("TY_P_HR_4C2BB622", TXT01_VSYEAR.GetValue().ToString(),
                                                            TXT01_VSYEAR.GetValue().ToString(),
                                                            (Convert.ToInt16(TXT01_VSYEAR.GetValue().ToString()) - 1).ToString()
                                                            );

                
                this.DbConnector.ExecuteNonQueryList();


                UP_leapmonth(Convert.ToInt32(TXT01_VSYEAR.GetValue().ToString().Substring(0, 4)));
                ConvertFromSolarDate(Convert.ToDateTime(TXT01_VSYEAR.GetValue().ToString() + "-01-01"));    //윤달체크

                LunisolarDate(Convert.ToInt16(TXT01_VSYEAR.GetValue().ToString()), 01, 01, _isLeapMonth);   //설 양력 가져오기

                DateTime Date = _solarDate;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4C2FQ629", "02",
                                                            Date.ToString("yyyy"),
                                                            Date.ToString("MM"),
                                                            Date.ToString("dd"));

                this.DbConnector.ExecuteNonQueryList();
                this.DbConnector.CommandClear();
                

                this.DbConnector.Attach("TY_P_HR_4C1FZ620", UP_getYoil(Date),
                                                            "02",
                                                            TYUserInfo.EmpNo,
                                                            TXT01_VSYEAR.GetValue().ToString(),
                                                            "01",
                                                            "01",
                                                            "2");
                this.DbConnector.ExecuteNonQueryList();
                this.DbConnector.CommandClear();
                Date = _solarDate.AddDays(-1);

                this.DbConnector.Attach("TY_P_HR_4C2FQ629", "16",
                                                            Date.ToString("yyyy"),
                                                            Date.ToString("MM"),
                                                            Date.ToString("dd"));
                this.DbConnector.ExecuteNonQueryList();
                this.DbConnector.CommandClear();
                Date = _solarDate.AddDays(+1);

                this.DbConnector.Attach("TY_P_HR_4C2FQ629", "16",
                                                            Date.ToString("yyyy"),
                                                            Date.ToString("MM"),
                                                            Date.ToString("dd"));

                this.DbConnector.ExecuteNonQueryList();
                this.DbConnector.CommandClear();

                LunisolarDate(Convert.ToInt16(TXT01_VSYEAR.GetValue().ToString()), 08, 15, _isLeapMonth);   //추석 양력 가져오기

                Date = _solarDate;

                this.DbConnector.Attach("TY_P_HR_4C2FQ629", "12",
                                                            Date.ToString("yyyy"),
                                                            Date.ToString("MM"),
                                                            Date.ToString("dd"));
                this.DbConnector.ExecuteNonQueryList();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4C1FZ620", UP_getYoil(Date),
                                                            "12",
                                                            TYUserInfo.EmpNo,
                                                            TXT01_VSYEAR.GetValue().ToString(),
                                                            "08",
                                                            "15",
                                                            "2");
                this.DbConnector.ExecuteNonQueryList();
                this.DbConnector.CommandClear();
                Date = _solarDate.AddDays(-1);

                this.DbConnector.Attach("TY_P_HR_4C2FQ629", "16",
                                                            Date.ToString("yyyy"),
                                                            Date.ToString("MM"),
                                                            Date.ToString("dd"));
                this.DbConnector.ExecuteNonQueryList();
                this.DbConnector.CommandClear();
                Date = _solarDate.AddDays(+1);

                this.DbConnector.Attach("TY_P_HR_4C2FQ629", "16",
                                                            Date.ToString("yyyy"),
                                                            Date.ToString("MM"),
                                                            Date.ToString("dd"));
                this.DbConnector.ExecuteNonQueryList();
                this.DbConnector.CommandClear();
                //TYSCMLIB.GTSRREF 공휴일 생성
                this.DbConnector.Attach("TY_P_HR_4C49P637", TYUserInfo.EmpNo,
                                                            TXT01_VSYEAR.GetValue().ToString()
                                                            );

                this.DbConnector.ExecuteNonQueryList();

                this.ShowMessage("TY_M_GB_26E30875");
            }
            catch
            {
                this.ShowMessage("TY_M_GB_26E31876");
            }
            finally
            {
            }
        }
        #endregion

        #region Description : 생성 ProcessCheck 이벤트
        private void BTN61_CREATE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                                    "TY_P_HR_4C2C7623",
                                    TXT01_VSYEAR.GetValue().ToString()
                                    );

            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_26D6A858");
                e.Successed = false;
                return;
            }
            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
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
            int year = _calendar.GetYear(solarDate);		// 음력 년
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

        #region Description : 음력요일 구하기
        private string UP_getYoil(DateTime dateValue)
        {
            string SYYOILCD = string.Empty;
            if (dateValue.ToString("ddd") == "일")
            {
                SYYOILCD = "1";
            }
            else if (dateValue.ToString("ddd") == "월")
            {
                SYYOILCD = "2";
            }
            else if (dateValue.ToString("ddd") == "화")
            {
                SYYOILCD = "3";
            }
            else if (dateValue.ToString("ddd") == "수")
            {
                SYYOILCD = "4";
            }
            else if (dateValue.ToString("ddd") == "목")
            {
                SYYOILCD = "5";
            }
            else if (dateValue.ToString("ddd") == "금")
            {
                SYYOILCD = "6";
            }
            else if (dateValue.ToString("ddd") == "토")
            {
                SYYOILCD = "7";
            }
            return SYYOILCD;
        }
        #endregion

        #region Description : 윤달인 경우 2월 29일 등록
        public void UP_leapmonth(int year)
        {
            if( year%4==0 && year%100!=0 || year%400==0 ) {

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_632DD580", TXT01_VSYEAR.GetValue().ToString().Substring(0,4),
                                                            TXT01_VSYEAR.GetValue().ToString().Substring(0,4) + "-02-29"
                                                            );

                this.DbConnector.ExecuteNonQueryList();
            }
        }
        #endregion
    }
}
