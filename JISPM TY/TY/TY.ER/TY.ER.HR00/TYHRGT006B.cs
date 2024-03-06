using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 일일근태생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.01.14 11:47
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GISABUN : 사원번호
    ///  KBGUNMU : 근무처
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYHRGT006B : TYBase
    {
        private TYData DAT02_GIDATE;
        private TYData DAT02_GISABUN;
        private TYData DAT02_GIYOIL;
        private TYData DAT02_GIHUMUCD;
        private TYData DAT02_GIHUGACD;
        private TYData DAT02_GIYGCHUL;
        private TYData DAT02_GICHLTIME;
        private TYData DAT02_GIENDTIME;
        private TYData DAT02_GIINCHLTM;
        private TYData DAT02_GIINENDTM;
        private TYData DAT02_GIYACHLTM;
        private TYData DAT02_GIYAENDTM;
        private TYData DAT02_GIYAINCHL;
        private TYData DAT02_GIYAINEND;
        private TYData DAT02_GIJOCHLST;
        private TYData DAT02_GIJOCHLED;
        private TYData DAT02_GIYUNJGST;
        private TYData DAT02_GIYUNJGED;
        private TYData DAT02_GIJOTIME;
        private TYData DAT02_GIHTTIME;
        private TYData DAT02_GIOTTIME;
        private TYData DAT02_GINTTIME;
        private TYData DAT02_GIHUTIME;
        private TYData DAT02_GIGJTIME;
        private TYData DAT02_GIGOTIME;
        private TYData DAT02_GISATIME;
        private TYData DAT02_GIJITIME;
        private TYData DAT02_GIJTTIME;
        private TYData DAT02_GIINTIME;
        private TYData DAT02_GICARDGB;
        private TYData DAT02_GIHUILGN;
        private TYData DAT02_GIHISAB;

        private TYData DAT03_KBBUSEO;
        private TYData DAT03_KBJKCD;
        private TYData DAT03_KBGUNMU;
        private TYData DAT03_GHCODE;
        private TYData DAT03_GDROTGN;
        private TYData DAT03_STGOTIME;
        private TYData DAT03_EDGOTIME;
        private TYData DAT03_STSATIME;
        private TYData DAT03_EDSATIME;
        private TYData DAT03_GDDATE;
        private TYData DAT03_DAEKEUN;

        private DataTable dt_WorkGroup;   //교대조
        private DataTable dt_OtDoc; //연장근무
        private DataTable dt_GTILREF; //근태관리

        //일일근태관리(등록용)
        System.Collections.Generic.List<object[]> datas_GTILREF = new System.Collections.Generic.List<object[]>();
        //일일근태관리(수정용)
        System.Collections.Generic.List<object[]> datas_GTILREF_Edit = new System.Collections.Generic.List<object[]>();
        //외출관리(계산용)
        System.Collections.Generic.List<object[]> datas_GTOJREF = new System.Collections.Generic.List<object[]>();
        //외출관리
        System.Collections.Generic.List<object[]> datas_GTOIREF = new System.Collections.Generic.List<object[]>();
        //철야관리
        System.Collections.Generic.List<object[]> datas_GTCHREF = new System.Collections.Generic.List<object[]>();        
        
        #region  Description : 폼 로드 이벤트
        public TYHRGT006B()
        {            

            InitializeComponent();

            this.DAT02_GIDATE = new TYData("DAT02_GIDATE", null);
            this.DAT02_GISABUN = new TYData("DAT02_GISABUN", null);
            this.DAT02_GIYOIL = new TYData("DAT02_GIYOIL", null);
            this.DAT02_GIHUMUCD = new TYData("DAT02_GIHUMUCD", null);
            this.DAT02_GIHUGACD = new TYData("DAT02_GIHUGACD", null);
            this.DAT02_GIYGCHUL = new TYData("DAT02_GIYGCHUL", null);
            this.DAT02_GICHLTIME = new TYData("DAT02_GICHLTIME", null);
            this.DAT02_GIENDTIME = new TYData("DAT02_GIENDTIME", null);
            this.DAT02_GIINCHLTM = new TYData("DAT02_GIINCHLTM", null);
            this.DAT02_GIINENDTM = new TYData("DAT02_GIINENDTM", null);
            this.DAT02_GIYACHLTM = new TYData("DAT02_GIYACHLTM", null);
            this.DAT02_GIYAENDTM = new TYData("DAT02_GIYAENDTM", null);
            this.DAT02_GIYAINCHL = new TYData("DAT02_GIYAINCHL", null);
            this.DAT02_GIYAINEND = new TYData("DAT02_GIYAINEND", null);
            this.DAT02_GIJOCHLST = new TYData("DAT02_GIJOCHLST", null);
            this.DAT02_GIJOCHLED = new TYData("DAT02_GIJOCHLED", null);
            this.DAT02_GIYUNJGST = new TYData("DAT02_GIYUNJGST", null);
            this.DAT02_GIYUNJGED = new TYData("DAT02_GIYUNJGED", null);
            this.DAT02_GIJOTIME = new TYData("DAT02_GIJOTIME", null);
            this.DAT02_GIHTTIME = new TYData("DAT02_GIHTTIME", null);
            this.DAT02_GIOTTIME = new TYData("DAT02_GIOTTIME", null);
            this.DAT02_GINTTIME = new TYData("DAT02_GINTTIME", null);
            this.DAT02_GIHUTIME = new TYData("DAT02_GIHUTIME", null);
            this.DAT02_GIGJTIME = new TYData("DAT02_GIGJTIME", null);
            this.DAT02_GIGOTIME = new TYData("DAT02_GIGOTIME", null);
            this.DAT02_GISATIME = new TYData("DAT02_GISATIME", null);
            this.DAT02_GIJITIME = new TYData("DAT02_GIJITIME", null);
            this.DAT02_GIJTTIME = new TYData("DAT02_GIJTTIME", null);
            this.DAT02_GIINTIME = new TYData("DAT02_GIINTIME", null);
            this.DAT02_GICARDGB = new TYData("DAT02_GICARDGB", null);
            this.DAT02_GIHUILGN = new TYData("DAT02_GIHUILGN", null);
            this.DAT02_GIHISAB = new TYData("DAT02_GIHISAB", null);

            this.DAT03_KBBUSEO = new TYData("DAT03_KBBUSEO", null);
            this.DAT03_KBJKCD = new TYData("DAT03_KBJKCD", null);
            this.DAT03_KBGUNMU = new TYData("DAT03_KBGUNMU", null);
            this.DAT03_GHCODE = new TYData("DAT03_GHCODE", null);
            this.DAT03_GDROTGN = new TYData("DAT03_GDROTGN", null);

            this.DAT03_STGOTIME = new TYData("DAT03_STGOTIME", null);
            this.DAT03_EDGOTIME = new TYData("DAT03_EDGOTIME", null);
            this.DAT03_STSATIME = new TYData("DAT03_STSATIME", null);
            this.DAT03_EDSATIME = new TYData("DAT03_EDSATIME", null);
            this.DAT03_GDDATE = new TYData("DAT03_GDDATE", null);
            this.DAT03_DAEKEUN = new TYData("DAT03_DAEKEUN", null);            
            
        }

        private void TYHRGT006B_Load(object sender, System.EventArgs e)
        {
            this.ControlFactory.Add(this.DAT02_GIDATE);
            this.ControlFactory.Add(this.DAT02_GISABUN);
            this.ControlFactory.Add(this.DAT02_GIYOIL);
            this.ControlFactory.Add(this.DAT02_GIHUMUCD);
            this.ControlFactory.Add(this.DAT02_GIHUGACD);
            this.ControlFactory.Add(this.DAT02_GIYGCHUL);
            this.ControlFactory.Add(this.DAT02_GICHLTIME);
            this.ControlFactory.Add(this.DAT02_GIENDTIME);
            this.ControlFactory.Add(this.DAT02_GIINCHLTM);
            this.ControlFactory.Add(this.DAT02_GIINENDTM);
            this.ControlFactory.Add(this.DAT02_GIYACHLTM);
            this.ControlFactory.Add(this.DAT02_GIYAENDTM);
            this.ControlFactory.Add(this.DAT02_GIYAINCHL);
            this.ControlFactory.Add(this.DAT02_GIYAINEND);
            this.ControlFactory.Add(this.DAT02_GIJOCHLST);
            this.ControlFactory.Add(this.DAT02_GIJOCHLED);
            this.ControlFactory.Add(this.DAT02_GIYUNJGST);
            this.ControlFactory.Add(this.DAT02_GIYUNJGED);
            this.ControlFactory.Add(this.DAT02_GIJOTIME);
            this.ControlFactory.Add(this.DAT02_GIHTTIME);
            this.ControlFactory.Add(this.DAT02_GIOTTIME);
            this.ControlFactory.Add(this.DAT02_GINTTIME);
            this.ControlFactory.Add(this.DAT02_GIHUTIME);
            this.ControlFactory.Add(this.DAT02_GIGJTIME);
            this.ControlFactory.Add(this.DAT02_GIGOTIME);
            this.ControlFactory.Add(this.DAT02_GISATIME);
            this.ControlFactory.Add(this.DAT02_GIJITIME);
            this.ControlFactory.Add(this.DAT02_GIJTTIME);
            this.ControlFactory.Add(this.DAT02_GIINTIME);
            this.ControlFactory.Add(this.DAT02_GICARDGB);
            this.ControlFactory.Add(this.DAT02_GIHUILGN);
            this.ControlFactory.Add(this.DAT02_GIHISAB);

            this.ControlFactory.Add(this.DAT03_KBBUSEO);
            this.ControlFactory.Add(this.DAT03_KBJKCD);
            this.ControlFactory.Add(this.DAT03_KBGUNMU);
            this.ControlFactory.Add(this.DAT03_GHCODE);
            this.ControlFactory.Add(this.DAT03_GDROTGN);
            this.ControlFactory.Add(this.DAT03_STGOTIME);
            this.ControlFactory.Add(this.DAT03_EDGOTIME);
            this.ControlFactory.Add(this.DAT03_STSATIME);
            this.ControlFactory.Add(this.DAT03_EDSATIME);
            this.ControlFactory.Add(this.DAT03_GDDATE);
            this.ControlFactory.Add(this.DAT03_DAEKEUN);            
            
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.dt_GTILREF = new DataTable();

            if (TYUserInfo.EmpNo == "0259-F" || TYUserInfo.EmpNo == "0287-M")
            {
                this.CBO01_KBGUNMU.SetValue("1");
            }
            else
            {
                this.CBO01_KBGUNMU.SetValue("2");
            }

            this.SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region  Description : 처리 버튼 이벤트
        
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            /*
            int iCnt = 0;

            DateTime TmSdate = new DateTime();
            DateTime TmEdate = new DateTime();
            DateTime TmWkdate = new DateTime();

            string sKIJUNDATE = string.Empty;
            string sJUNILDATE = string.Empty;

            
            //날짜 기간동안 근태 생성
            TmSdate = Convert.ToDateTime(Set_Date(this.DTP01_STDATE.GetString().ToString()));
            TmEdate = Convert.ToDateTime(Set_Date(this.DTP01_EDDATE.GetString().ToString()));

                       

                //해당일자 근태관리 삭제

                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_HR_51F8X148", TmSdate.Year.ToString() + Set_Fill2(TmSdate.Month.ToString()) + Set_Fill2(TmSdate.Day.ToString()),
                //                                            TmEdate.Year.ToString() + Set_Fill2(TmEdate.Month.ToString()) + Set_Fill2(TmEdate.Day.ToString()),
                //                                            this.CBO01_KBGUNMU.GetValue().ToString(), this.CBH01_GISABUN.GetValue().ToString());
                //this.DbConnector.ExecuteTranQuery();


                //일일근태집계파일 삭제
                this.UP_WorkHourData_Del(TmEdate.AddDays(1).Year.ToString() + Set_Fill2(TmEdate.AddDays(1).Month.ToString()) + Set_Fill2(TmEdate.AddDays(1).Day.ToString()), this.CBH01_GISABUN.GetValue().ToString());

                //일일근태집계파일 등록
                this.UP_WorkHourData_ADD(TmSdate.Year.ToString() + Set_Fill2(TmSdate.Month.ToString()) + Set_Fill2(TmSdate.Day.ToString()),
                                         TmEdate.AddDays(1).Year.ToString() + Set_Fill2(TmEdate.AddDays(1).Month.ToString()) + Set_Fill2(TmEdate.AddDays(1).Day.ToString()),
                                         this.CBO01_KBGUNMU.GetValue().ToString(), this.CBH01_GISABUN.GetValue().ToString());

                //철야관리 삭제
                this.UP_WorkAllNight_DEL(TmSdate.Year.ToString() + Set_Fill2(TmSdate.Month.ToString()) + Set_Fill2(TmSdate.Day.ToString()),
                                         TmEdate.AddDays(1).Year.ToString() + Set_Fill2(TmEdate.AddDays(1).Month.ToString()) + Set_Fill2(TmEdate.AddDays(1).Day.ToString()),
                                         this.CBH01_GISABUN.GetValue().ToString());

                //외출관리 삭제
                this.UP_GoOutTime_DEL(TmSdate.Year.ToString() + Set_Fill2(TmSdate.Month.ToString()) + Set_Fill2(TmSdate.Day.ToString()),
                                      TmEdate.Year.ToString() + Set_Fill2(TmEdate.Month.ToString()) + Set_Fill2(TmEdate.Day.ToString()),
                                      this.CBH01_GISABUN.GetValue().ToString());

                //교대조,연장근무
                this.UP_Set_WkGroupOtDoc_Sel();


                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_51F95149", this.CBO01_KBGUNMU.GetValue().ToString(), this.CBH01_GISABUN.GetValue().ToString());
                DataTable dt = this.DbConnector.ExecuteDataTable();

                pgb_Status.Value = 0;
                pgb_Status.Minimum = 0;
                pgb_Status.Maximum = dt.Rows.Count;

                if (dt.Rows.Count > 0)
                {
                    this.UP_TYData_Clear();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        iCnt = 0;

                        this.DAT03_GDDATE.SetValue("");

                        for (TmWkdate = TmSdate; TmWkdate <= TmEdate; TmWkdate.AddDays(1))
                        {
                            this.UP_TYData_Clear();

                            this.DAT02_GISABUN.SetValue(dt.Rows[i]["KBSABUN"].ToString());
                            this.DAT03_KBBUSEO.SetValue(dt.Rows[i]["KBBUSEO"].ToString());
                            this.DAT03_KBJKCD.SetValue(dt.Rows[i]["KBJKCD"].ToString());
                            this.DAT03_KBGUNMU.SetValue(dt.Rows[i]["KBGUNMU"].ToString());


                            //기준일자
                            //sKIJUNDATE = TmWkdate.Year.ToString() + Set_Fill2(TmWkdate.Month.ToString()) + Set_Fill2(TmWkdate.Day.ToString());
                            //기준일자
                            //if (iCnt == 0)
                            //{
                            //    sKIJUNDATE = TmWkdate.AddDays(-1).Year.ToString() + Set_Fill2(TmWkdate.AddDays(-1).Month.ToString()) + Set_Fill2(TmWkdate.AddDays(-1).Day.ToString());
                            //}
                            //else
                            //{
                            //    sKIJUNDATE = TmWkdate.Year.ToString() + Set_Fill2(TmWkdate.Month.ToString()) + Set_Fill2(TmWkdate.Day.ToString());
                            //}

                            sKIJUNDATE = TmWkdate.Year.ToString() + Set_Fill2(TmWkdate.Month.ToString()) + Set_Fill2(TmWkdate.Day.ToString());

                            this.DAT02_GIDATE.SetValue(sKIJUNDATE);

                            //근태월력
                            this.UP_Calendar_Read(sKIJUNDATE.Substring(0, 4), sKIJUNDATE.Substring(4, 2), sKIJUNDATE.Substring(6, 2));

                            //개인휴무
                            this.UP_BreakDay_Read(this.DAT02_GISABUN.GetValue().ToString(), sKIJUNDATE);

                            //출퇴근 계산
                            this.UP_WorkTimeCheck_Compute(sKIJUNDATE, this.CBO01_KBGUNMU.GetValue().ToString(), this.DAT02_GISABUN.GetValue().ToString());


                            //근태관리 등록
                            this.UP_Set_GTILREF_List();

                            this.UP_Set_GTILREF_Add();

                            TmWkdate = TmWkdate.AddDays(1);

                        }//for (TmWkdate = TmSdate; TmWkdate <= TmEdate; TmWkdate.AddDays(1))..end

                        pgb_Status.Value = pgb_Status.Value + 1;

                    }//for (int i = 0; i < dt.Rows.Count; i++)..end
                    

                } //if (dt.Rows.Count > 0)..end

                //this.UP_Set_GTILREF_Add();

                pgb_Status.Value = 0;
                pgb_Status.Maximum = 0;             
            */

            string sOUT_MSG = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_97JGV055",
                                    this.DTP01_STDATE.GetString().ToString(),
                                    this.DTP01_EDDATE.GetString().ToString(),
                                    this.CBH01_GISABUN.GetValue().ToString(),                                    
                                    this.CBO01_KBGUNMU.GetValue().ToString(),
                                    TYUserInfo.EmpNo,
                                    sOUT_MSG.ToString()
                                    );

            sOUT_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUT_MSG.Substring(0, 2).ToString() == "OK")
            {
                this.ShowMessage("TY_M_GB_26E30875");
            }
            else
            {
                this.ShowMessage("TY_M_GB_26E31876");
            }

            //this.ShowMessage("TY_M_GB_26E30875");
        }

        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string STDATE = this.DTP01_STDATE.GetString();
            string EDDATE = this.DTP01_EDDATE.GetString();

            if (Convert.ToInt32(STDATE) > Convert.ToInt32(EDDATE))
            {
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다 .", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            //최가연 지점만 생성할수 있게 제한
            if (TYUserInfo.EmpNo == "0346-F" && CBO01_KBGUNMU.GetValue().ToString() != "2")
            {
                this.ShowCustomMessage("서울 지점만 생성가능합니다 .", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }

        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region  Description : 사용자 정의 함수
        private void UP_WorkTimeCheck_Compute(string sGDDATE, string sCHULIB, string sSABUN)
        {
            string sGDTIME = string.Empty;
            string sSeq = string.Empty;
            string sNextDay = UP_Calendar_NextDay(Set_Date(sGDDATE));
            string sPreDay = UP_Calendar_PreDay(Set_Date(sGDDATE));

            


                //근태파일의 전일자 자료 가져오기
                dt_GTILREF.Clear();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BQJP565", sPreDay, sPreDay, "", "1");
                dt_GTILREF = this.DbConnector.ExecuteDataTable();

                if (sCHULIB == "2") sCHULIB = "9";

                //생성된 근태자료 있으면 가져온다.
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4BQJP565", sGDDATE, sNextDay, sSABUN, sCHULIB);
                DataTable dk = this.DbConnector.ExecuteDataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_51FB4154", sGDDATE, sNextDay, sSABUN, sCHULIB);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    //근무조
                    foreach (DataRow rw in dt_WorkGroup.Select("GDDATE = '" + sGDDATE + "' AND GDSABUN = '" + sSABUN + "'", "GDDATE ASC "))
                    {
                        if (rw.ItemArray.Length > 0)
                        {
                            this.DAT03_GDROTGN.SetValue(rw.ItemArray[3].ToString());
                        }
                    }


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (sGDDATE == dt.Rows[i]["GDDATE"].ToString().Trim())
                        {

                            this.DAT03_GHCODE.SetValue(dt.Rows[i]["GHCODE"].ToString());

                            sGDTIME = dt.Rows[i]["GDTIME"].ToString();

                            switch (dt.Rows[i]["GDGUBUN"].ToString())
                            {
                                case "01": //출근
                                    sGDTIME = this.UP_Get_GTILREFToReturn(dk, "GICHLTIME", sGDTIME);
                                    this.DAT02_GICHLTIME.SetValue(sGDTIME);
                                    break;
                                case "02": //퇴근

                                    sGDTIME = this.UP_Get_GTILREFToReturn(dk, "GIENDTIME", sGDTIME);
                                    if (this.DAT03_KBJKCD.GetValue().ToString() == "3C" && this.DAT03_KBJKCD.GetValue().ToString() == "3D" && this.DAT03_KBJKCD.GetValue().ToString() == "6C")
                                    {
                                        this.DAT02_GIENDTIME.SetValue(sGDTIME);
                                    }
                                    else
                                    {
                                        //연봉직, 사무직은 당일 퇴근자료만 넣는다.
                                        if (sGDDATE == dt.Rows[i]["GDDATE"].ToString().Trim())
                                        {
                                            this.DAT02_GIENDTIME.SetValue(sGDTIME);
                                        }
                                    }

                                    if (int.Parse(this.DAT02_GIENDTIME.GetValue().ToString()) <= 0)
                                    {
                                        this.DAT02_GIENDTIME.SetValue(UP_Set_EndTime(sGDDATE, dt.Rows[i]["GDDATE"].ToString(), sGDTIME, dt.Rows[i]["GDROTGN"].ToString(), "1"));
                                    }

                                    if (this.DAT03_KBJKCD.GetValue().ToString() == "3C" || this.DAT03_KBJKCD.GetValue().ToString() == "3D")
                                    {
                                        if (int.Parse(this.DAT02_GIYACHLTM.GetValue().ToString()) > 0)
                                        {
                                            this.DAT02_GIYAENDTM.SetValue(UP_Set_EndTime(sGDDATE, dt.Rows[i]["GDDATE"].ToString(), sGDTIME, dt.Rows[i]["GDROTGN"].ToString(), "2"));
                                        }
                                    }

                                    if (int.Parse(this.DAT02_GIYAENDTM.GetValue().ToString()) > 0)
                                    {
                                        //야간퇴근이 있으면 일반퇴근 시간은 0으로 한다.
                                        this.DAT02_GIENDTIME.SetValue("0");

                                        //야간퇴근이면 철야TABLE에 등록
                                        this.UP_WorkAllNight_ADD(dt.Rows[i]["GDDATE"].ToString(), this.DAT02_GISABUN.GetValue().ToString(), sGDTIME);
                                    }
                                    break;
                                case "03": //야간출근
                                    sGDTIME = this.UP_Get_GTILREFToReturn(dk, "GIYACHLTM", sGDTIME);
                                    this.DAT02_GIYACHLTM.SetValue(sGDTIME);
                                    this.DAT02_GIYGCHUL.SetValue("*");
                                    break;
                                case "04": //야간퇴근

                                    sGDTIME = this.UP_Get_GTILREFToReturn(dk, "GIYAENDTM", sGDTIME);
                                    if (this.DAT03_KBBUSEO.GetValue().ToString().Substring(0, 1) == "S" || this.DAT03_KBBUSEO.GetValue().ToString() == "E10200")
                                    {
                                        //if (Convert.ToDouble(sGDTIME) > 0900 && Convert.ToDouble(sGDTIME) < 0950)
                                        if (Convert.ToDouble(sGDTIME) > 0830 && Convert.ToDouble(sGDTIME) < 0920)
                                        {
                                            //sGDTIME = "0900";

                                            sGDTIME = "0830";
                                        }
                                    }
                                    this.DAT02_GIYAENDTM.SetValue(sGDTIME);

                                    //야간퇴근이면 철야TABLE에 등록
                                    this.UP_WorkAllNight_ADD(dt.Rows[i]["GDDATE"].ToString(), this.DAT02_GISABUN.GetValue().ToString(), sGDTIME);

                                    break;
                                case "05": //공외시작
                                    this.DAT03_STGOTIME.SetValue(sGDTIME);
                                    this.UP_GoOutTime_ADD(dt.Rows[i]["GDDATE"].ToString(), this.DAT02_GISABUN.GetValue().ToString(), dt.Rows[i]["GDGUBUN"].ToString(),
                                                          sGDTIME, TYUserInfo.EmpNo);
                                    break;
                                case "06": //공외종료
                                    this.DAT03_EDGOTIME.SetValue(sGDTIME);
                                    this.UP_GoOutTime_ADD(dt.Rows[i]["GDDATE"].ToString(), this.DAT02_GISABUN.GetValue().ToString(), dt.Rows[i]["GDGUBUN"].ToString(),
                                                          sGDTIME, TYUserInfo.EmpNo);
                                    break;
                                case "07": //사외시작
                                    this.DAT03_STSATIME.SetValue(sGDTIME);
                                    this.UP_GoOutTime_ADD(dt.Rows[i]["GDDATE"].ToString(), this.DAT02_GISABUN.GetValue().ToString(), dt.Rows[i]["GDGUBUN"].ToString(),
                                                          sGDTIME, TYUserInfo.EmpNo);
                                    break;
                                case "08": //사외종료
                                    this.DAT03_EDSATIME.SetValue(sGDTIME);
                                    this.UP_GoOutTime_ADD(dt.Rows[i]["GDDATE"].ToString(), this.DAT02_GISABUN.GetValue().ToString(), dt.Rows[i]["GDGUBUN"].ToString(),
                                                          sGDTIME, TYUserInfo.EmpNo);
                                    break;
                                case "09": //조퇴                            
                                    this.DAT02_GIENDTIME.SetValue(sGDTIME);
                                    this.DAT02_GIHUGACD.SetValue("530");
                                    break;
                                default:
                                    break;

                            }//switch...end
                        }
                        else
                        {
                            if (Convert.ToInt32(sGDDATE) > Convert.ToInt32(dt.Rows[i]["GDDATE"].ToString()))
                            {
                                //전일 처리
                                sGDTIME = dt.Rows[i]["GDTIME"].ToString();

                                switch (dt.Rows[i]["GDGUBUN"].ToString())
                                {
                                    case "04": //야간퇴근
                                        if (this.DAT03_GDROTGN.GetValue().ToString() != "5")
                                        {
                                            if (this.DAT03_KBBUSEO.GetValue().ToString().Substring(0, 1) == "S" || this.DAT03_KBBUSEO.GetValue().ToString() == "E10200")
                                            {
                                                //if (Convert.ToDouble(sGDTIME) > 0900 && Convert.ToDouble(sGDTIME) < 0950)
                                                if (Convert.ToDouble(sGDTIME) > 0830 && Convert.ToDouble(sGDTIME) < 0920)
                                                {
                                                    //sGDTIME = "0900";
                                                    sGDTIME = "0830";
                                                }
                                            }
                                            this.DAT02_GIYAENDTM.SetValue(sGDTIME);
                                        }
                                        break;
                                    default:
                                        break;

                                }//switch...end
                            }

                        }

                    }//for (int i = 0; i < dt.Rows.Count; i++)..end

                    //공적외출
                    if (int.Parse(this.DAT03_STGOTIME.GetValue().ToString()) > 0)
                    {
                        if (int.Parse(this.DAT03_EDGOTIME.GetValue().ToString()) == 0)
                        {
                            if (this.DAT03_KBJKCD.GetValue().ToString() == "3C" || this.DAT03_KBJKCD.GetValue().ToString() == "3D")
                            {
                                //this.DAT03_EDGOTIME.SetValue("1800");
                                this.DAT03_EDGOTIME.SetValue("1730");
                            }
                            else
                            {
                                //this.DAT03_EDGOTIME.SetValue("1830");
                                this.DAT03_EDGOTIME.SetValue("1800");
                            }
                        }
                        sSeq = UP_GetGTOIREF_Read(sGDDATE, this.DAT02_GISABUN.GetValue().ToString(), "01");

                        this.UP_MainGoOutTime_ADD(sGDDATE, this.DAT02_GISABUN.GetValue().ToString(), "01", sSeq,
                                                  this.DAT03_STGOTIME.GetValue().ToString(), this.DAT03_EDGOTIME.GetValue().ToString(), "0", "0",
                                                  "", "", TYUserInfo.EmpNo);
                        //공적외출 인정시간
                        this.DAT02_GIGOTIME.SetValue(UP_Set_AppGoOutTime(sGDDATE, this.DAT03_STGOTIME.GetValue().ToString(), this.DAT03_EDGOTIME.GetValue().ToString()));
                    }

                    //사적외출
                    if (int.Parse(this.DAT03_STSATIME.GetValue().ToString()) > 0)
                    {
                        if (int.Parse(this.DAT03_EDSATIME.GetValue().ToString()) == 0)
                        {
                            if (this.DAT03_KBJKCD.GetValue().ToString() == "3C" || this.DAT03_KBJKCD.GetValue().ToString() == "3D")
                            {
                                //this.DAT03_EDSATIME.SetValue("1800");
                                this.DAT03_EDSATIME.SetValue("1730");
                            }
                            else
                            {
                                //this.DAT03_EDSATIME.SetValue("1830");
                                this.DAT03_EDSATIME.SetValue("1800");
                            }
                        }
                        sSeq = UP_GetGTOIREF_Read(sGDDATE, this.DAT02_GISABUN.GetValue().ToString(), "02");
                        this.UP_MainGoOutTime_ADD(sGDDATE, this.DAT02_GISABUN.GetValue().ToString(), "02", sSeq,
                                                  this.DAT03_STSATIME.GetValue().ToString(), this.DAT03_EDSATIME.GetValue().ToString(), "0", "0",
                                                  "", "", TYUserInfo.EmpNo);
                        //사적외출 인정시간
                        this.DAT02_GISATIME.SetValue(UP_Set_AppGoOutTime(sGDDATE, this.DAT03_STSATIME.GetValue().ToString(), this.DAT03_EDSATIME.GetValue().ToString()));
                    }

                    //무단결근
                    if (this.DAT02_GIHUGACD.GetValue().ToString() == "900")
                    {
                        if (Convert.ToInt16(this.DAT02_GICHLTIME.GetValue().ToString()) != 0 &&
                            Convert.ToInt16(this.DAT02_GIENDTIME.GetValue().ToString()) != 0 ||
                            Convert.ToInt16(this.DAT02_GIYACHLTM.GetValue().ToString()) != 0 &&
                            Convert.ToInt16(this.DAT02_GIYAENDTM.GetValue().ToString()) != 0)
                        {
                            this.DAT02_GIHUGACD.SetValue("");
                        }
                    }

                    //연장근무 체크
                    this.UP_Get_OverTime(sGDDATE);

                    //출근인정시간 계산
                    if (Convert.ToInt16(this.DAT02_GICHLTIME.GetValue().ToString()) > 0)
                    {
                        this.DAT02_GIINCHLTM.SetValue(UP_Get_InApprovalTime(sGDDATE, this.DAT03_KBJKCD.GetValue().ToString(), this.DAT03_KBGUNMU.GetValue().ToString(),
                                                                          this.DAT02_GICHLTIME.GetValue().ToString(),
                                                                          this.DAT02_GIHUGACD.GetValue().ToString()));
                    }
                    //퇴근인정시간 계산
                    if (Convert.ToInt16(this.DAT02_GIENDTIME.GetValue().ToString()) > 0)
                    {
                        this.DAT02_GIINENDTM.SetValue(UP_Get_OutApprovalTime(sGDDATE, this.DAT03_KBJKCD.GetValue().ToString(), this.DAT03_KBGUNMU.GetValue().ToString(),
                                                                          this.DAT02_GIENDTIME.GetValue().ToString(),
                                                                          this.DAT03_GHCODE.GetValue().ToString()));
                    }
                    //야간출근인정시간 계산
                    if (Convert.ToInt16(this.DAT02_GIYACHLTM.GetValue().ToString()) > 0)
                    {
                        this.DAT02_GIYAINCHL.SetValue(UP_Get_InOutNightTime(sGDDATE, this.DAT03_KBJKCD.GetValue().ToString(), this.DAT03_KBGUNMU.GetValue().ToString(),
                                                                          this.DAT02_GIYACHLTM.GetValue().ToString(), "1"));
                    }
                    //야간퇴근인정시간 계산
                    if (Convert.ToInt16(this.DAT02_GIYAENDTM.GetValue().ToString()) > 0)
                    {
                        this.DAT02_GIYAINEND.SetValue(UP_Get_InOutNightTime(sGDDATE, this.DAT03_KBJKCD.GetValue().ToString(), this.DAT03_KBGUNMU.GetValue().ToString(),
                                                                          this.DAT02_GIYAENDTM.GetValue().ToString(), "2"));
                    }

                    //지각시간 계산
                    this.DAT02_GIJITIME.SetValue(UP_Get_LateTime(sGDDATE,
                                                                 this.DAT02_GICHLTIME.GetValue().ToString(),
                                                                 this.DAT02_GIINCHLTM.GetValue().ToString(),
                                                                 this.DAT02_GIYACHLTM.GetValue().ToString(),
                                                                 this.DAT02_GIYAINCHL.GetValue().ToString(),
                                                                 this.DAT03_KBGUNMU.GetValue().ToString(),
                                                                 this.DAT03_KBJKCD.GetValue().ToString(),
                                                                 sSABUN));

                    
                    //연장근무 OT 인정시간 계산
                    this.UP_Get_ApprovalOTTime_Create(sGDDATE);

                }
                else
                {
                    //근태자료가 없으면 현장직 근무조인지 체크
                    foreach (DataRow rw in dt_WorkGroup.Select("GDDATE = '" + sGDDATE + "' AND GDSABUN = '" + sSABUN + "'", "GDDATE ASC "))
                    {
                        if (rw.ItemArray.Length > 0)
                        {
                            //교대조
                            this.DAT03_GDROTGN.SetValue(rw.ItemArray[3].ToString());
                        }
                    }

                    //연장근무 체크
                    this.UP_Get_OverTime(sGDDATE);

                    //연장근무 OT 인정시간 계산
                    this.UP_Get_ApprovalOTTime_Create(sGDDATE);
                }
                //if (dt.Rows.Count > 0)...end            


                //예외처리
                this.UP_Set_GoOutException(sGDDATE);

                //개인휴무 최종판단
                this.UP_Set_IndHUGACD();

        }

        #region  Description : 개인휴무 코드 처리
        private void UP_Set_IndHUGACD()
        {
           //UTT 근무조 비번
            //if (int.Parse(this.DAT03_GDROTGN.GetValue().ToString()) == 5 && this.DAT02_GIHUGACD.GetValue().ToString() == "" )
            if (int.Parse(this.DAT03_GDROTGN.GetValue().ToString()) == 5 )
            {
                //this.DAT02_GIHUGACD.SetValue("520"); //비번

                ////총인정시간이 있는경우 출,퇴근 시간을 그대로 가져온다.
                //if (Convert.ToDouble(this.DAT02_GIINTIME.GetValue().ToString()) <= 0)
                //{
                //    this.DAT02_GICHLTIME.SetValue(0);
                //    this.DAT02_GIENDTIME.SetValue(0);
                //    this.DAT02_GIINCHLTM.SetValue(0);
                //    this.DAT02_GIINENDTM.SetValue(0);

                //    this.DAT02_GIYACHLTM.SetValue(0);
                //    this.DAT02_GIYAENDTM.SetValue(0);
                //    this.DAT02_GIYAINCHL.SetValue(0);
                //    this.DAT02_GIYAINEND.SetValue(0);
                //}

                // 개인휴무기간에 연차, 하기휴가 경우 비번조이면 비번으로 처리 한다. 
                if (this.DAT02_GIHUGACD.GetValue().ToString() == "120" || this.DAT02_GIHUGACD.GetValue().ToString() == "130" || this.DAT02_GIHUGACD.GetValue().ToString() == "")
                {
                    this.DAT02_GIHUGACD.SetValue("520"); //비번
                }              
            }

            //if (int.Parse(this.DAT02_GIYACHLTM.GetValue().ToString()) >= 2100 )
            if (int.Parse(this.DAT02_GIYACHLTM.GetValue().ToString()) >= 2030)
            {
                //this.DAT02_GIHUGACD.SetValue("525");  //야간출근
                this.DAT02_GIYGCHUL.SetValue("*");
            }


            if (this.DAT02_GIHUGACD.GetValue().ToString() == "" &&
                int.Parse(this.DAT02_GICHLTIME.GetValue().ToString()) == 0 &&
                int.Parse(this.DAT02_GIENDTIME.GetValue().ToString()) == 0 &&
                int.Parse(this.DAT02_GIYACHLTM.GetValue().ToString()) == 0 &&
                int.Parse(this.DAT02_GIYAENDTM.GetValue().ToString()) > 0)
            {
                //철야퇴근이면 전일에 근무자료가 있을경우만 철야퇴근으로 한다.
                DataRow[] rw;
                rw = dt_GTILREF.Select("GISABUN = '" + this.DAT02_GISABUN.GetValue().ToString() + "'", "GIDATE ASC ");
                for (int i = 0; i < rw.Length; i++)
                {
                    if (Convert.ToInt32(Get_Numeric(rw[i]["GIYAINCHL"].ToString())) > 0)
                    {
                        this.DAT02_GIHUGACD.SetValue("510"); //철야퇴근

                        //this.DAT02_GIYAENDTM.SetValue("0");
                        //this.DAT02_GIYAINEND.SetValue("0");
                    }
                }                
            }

            //근태자료 없으면 결근
            if (this.DAT02_GIHUGACD.GetValue().ToString() == "" &&
                int.Parse(this.DAT03_GDROTGN.GetValue().ToString()) == 0 &&
                int.Parse(this.DAT02_GICHLTIME.GetValue().ToString()) == 0 &&
                int.Parse(this.DAT02_GIENDTIME.GetValue().ToString()) == 0 &&
                int.Parse(this.DAT02_GIYACHLTM.GetValue().ToString()) == 0 &&
                int.Parse(this.DAT02_GIYAENDTM.GetValue().ToString()) == 0 &&
                int.Parse(this.DAT02_GIJOCHLST.GetValue().ToString()) == 0 &&
                int.Parse(this.DAT02_GIYUNJGST.GetValue().ToString()) == 0 
                )
            {
                //if (this.DAT02_GIYOIL.GetValue().ToString() != "1" && this.DAT02_GIYOIL.GetValue().ToString() != "7" && this.DAT02_GIHUGACD.GetValue().ToString() != "")
                if ( (this.DAT02_GIYOIL.GetValue().ToString() != "1" && this.DAT02_GIYOIL.GetValue().ToString() != "7") && this.DAT02_GIHUMUCD.GetValue().ToString() == "")
                {
                    this.DAT02_GIHUGACD.SetValue("900");
                }
            }

        }
        #endregion

        #region  Description : 근태 예외처리
        private void UP_Set_GoOutException(string sGDDATE)
        {            
            if (this.DAT02_GIYOIL.GetValue().ToString() == "1" || this.DAT02_GIYOIL.GetValue().ToString() == "7")
            {
                return;
            }

            //노조위원장, 서울기사
            if (this.CBO01_KBGUNMU.GetValue().ToString() == "1")
            {
                if (Convert.ToInt32(sGDDATE) >= 20160101)
                {
                    //정희석
                    if (this.DAT02_GISABUN.GetValue().ToString() == "0152-M")
                    {
                        this.DAT02_GICHLTIME.SetValue("0800"); // 주간출근시간
                        this.DAT02_GIINCHLTM.SetValue("0800"); // 주간출인시간

                        this.DAT02_GIENDTIME.SetValue("1730"); // 주간퇴근시간 
                        this.DAT02_GIINENDTM.SetValue("1730"); // 주간퇴인시간 
                    }
                }
                //else
                //{
                //    //박정일
                //    if (this.DAT02_GISABUN.GetValue().ToString() == "0223-M")
                //    {
                //        this.DAT02_GICHLTIME.SetValue("0800"); // 주간출근시간
                //        this.DAT02_GIINCHLTM.SetValue("0800"); // 주간출인시간
                //        this.DAT02_GIENDTIME.SetValue("1800"); // 주간퇴근시간 
                //        this.DAT02_GIINENDTM.SetValue("1800"); // 주간퇴인시간 
                //    }
                //}
            }
            else
            {
                if (this.DAT02_GISABUN.GetValue().ToString() == "C036-M" || this.DAT02_GISABUN.GetValue().ToString() == "C032-M")
                {
                    this.DAT02_GICHLTIME.SetValue("0800"); // 주간출근시간
                    this.DAT02_GIINCHLTM.SetValue("0800"); // 주간출인시간
                    this.DAT02_GIENDTIME.SetValue("1730"); // 주간퇴근시간 
                    this.DAT02_GIINENDTM.SetValue("1730"); // 주간퇴인시간 
                }
            }           

        }
        #endregion

        #region  Description : 퇴근 시간 체크
        private string UP_Set_EndTime(string sKIJUNDATE, string sWkDate, string sTime, string sWorkGroup, string sGubn)
        {
            string sReturnStrTime = string.Empty;

            sReturnStrTime = "0";

            //UTT 근무조 이면
            if (Convert.ToInt16(sWorkGroup) > 0)
            {
                if (Convert.ToInt32(sKIJUNDATE.Trim()) <= Convert.ToInt32(sWkDate.Trim()))
                {
                    if (sGubn == "1")
                    {
                        sReturnStrTime = int.Parse(this.DAT02_GICHLTIME.GetValue().ToString()) > 0 ? sTime : "0"; 
                    }
                    else
                    {
                        sReturnStrTime = int.Parse(this.DAT02_GIYACHLTM.GetValue().ToString()) > 0 ? sTime : "0"; 
                    }
                }
            }
            else
            {
                if (Convert.ToInt32(sKIJUNDATE.Trim()) == Convert.ToInt32(sWkDate.Trim()))
                {
                    sReturnStrTime = sTime;
                }
                else
                {
                    sReturnStrTime = "0";
                }
            }

            return sReturnStrTime;
        }
        #endregion

        #region  Description : OT 인정시간 계산 함수
        private void UP_Get_ApprovalOTTime_Create(string sDATE)
        {
            //조출
            if (Convert.ToInt16(this.DAT02_GIJOCHLST.GetValue().ToString()) > 0)
            {
                if (this.DAT02_GIYOIL.GetValue().ToString() == "1" || this.DAT02_GIYOIL.GetValue().ToString() == "7" || this.DAT02_GIHUMUCD.GetValue().ToString() != "")
                {
                    if (this.DAT03_KBJKCD.GetValue().ToString() == "3C" || this.DAT03_KBJKCD.GetValue().ToString() == "3D" || this.DAT03_KBJKCD.GetValue().ToString() == "6C")
                    {
                        if (this.DAT03_KBBUSEO.GetValue().ToString().Substring(0, 1) == "T" && this.DAT03_GDROTGN.GetValue().ToString() != "0")
                        {
                            this.UP_DayoffUTT_OT(sDATE, int.Parse(this.DAT02_GIJOCHLST.GetValue().ToString()), int.Parse(this.DAT02_GIJOCHLED.GetValue().ToString()), int.Parse(this.DAT02_GIYAINCHL.GetValue().ToString()),
                                                 this.DAT03_KBBUSEO.GetValue().ToString(), this.DAT02_GIYOIL.GetValue().ToString(), this.DAT02_GIHUMUCD.GetValue().ToString());
                        }
                        else
                        {
                            //this.UP_Dayoff_OT(sDATE, int.Parse(this.DAT02_GIJOCHLST.GetValue().ToString()), int.Parse(this.DAT02_GIJOCHLED.GetValue().ToString()), int.Parse(this.DAT02_GIYAINCHL.GetValue().ToString()),
                            //                  this.DAT03_KBBUSEO.GetValue().ToString(), this.DAT02_GIYOIL.GetValue().ToString(), this.DAT02_GIHUMUCD.GetValue().ToString(), "1");
                            this.UP_NewDay_OT(sDATE, int.Parse(this.DAT02_GIJOCHLST.GetValue().ToString()), int.Parse(this.DAT02_GIJOCHLED.GetValue().ToString()), int.Parse(this.DAT02_GIYAINCHL.GetValue().ToString()),
                                                      this.DAT03_KBBUSEO.GetValue().ToString(), this.DAT02_GIYOIL.GetValue().ToString(), this.DAT02_GIHUMUCD.GetValue().ToString());
                        }
                    }
                    else
                    {
                        this.UP_OfficeDayoff_OT(sDATE, int.Parse(this.DAT02_GIJOCHLST.GetValue().ToString()), int.Parse(this.DAT02_GIJOCHLED.GetValue().ToString()), int.Parse(this.DAT02_GIYAINCHL.GetValue().ToString()),
                                                this.DAT03_KBBUSEO.GetValue().ToString(), this.DAT02_GIYOIL.GetValue().ToString(), this.DAT02_GIHUMUCD.GetValue().ToString());
                    }
                }
                else
                {
                    if (this.DAT03_KBBUSEO.GetValue().ToString().Substring(0, 1) == "T" && this.DAT03_GDROTGN.GetValue().ToString() != "0")
                    {
                        this.UP_HYUIL_380_UTT(sDATE, int.Parse(this.DAT02_GIJOCHLST.GetValue().ToString()), int.Parse(this.DAT02_GIJOCHLED.GetValue().ToString()), int.Parse(this.DAT02_GIYAINCHL.GetValue().ToString()),
                                                this.DAT03_KBBUSEO.GetValue().ToString(), this.DAT02_GIYOIL.GetValue().ToString(), this.DAT02_GIHUMUCD.GetValue().ToString(), int.Parse(this.DAT03_GDROTGN.GetValue().ToString()));
                    }
                    else
                    {
                        this.UP_Day_OT(sDATE, int.Parse(this.DAT02_GIJOCHLST.GetValue().ToString()), int.Parse(this.DAT02_GIJOCHLED.GetValue().ToString()), int.Parse(this.DAT02_GIYAINCHL.GetValue().ToString()),
                                          this.DAT03_KBBUSEO.GetValue().ToString(), this.DAT02_GIYOIL.GetValue().ToString(), this.DAT02_GIHUMUCD.GetValue().ToString(),
                                          "1");
                    }
                }
            }

            //연장, 철야, 특근
            /*
            if ( int.Parse(this.DAT02_GIYACHLTM.GetValue().ToString()) != 0 && int.Parse(this.DAT02_GIYAENDTM.GetValue().ToString()) != 0)
            {
                if (int.Parse(this.DAT03_GDROTGN.GetValue().ToString()) != 0)
                {
                    if (this.DAT02_GIYOIL.GetValue().ToString() == "1" || this.DAT02_GIYOIL.GetValue().ToString() == "7" || this.DAT02_GIHUMUCD.GetValue().ToString() != "")
                    {
                        this.UP_Dayoff_OT(sDATE, int.Parse(this.DAT02_GIYACHLTM.GetValue().ToString()), int.Parse(this.DAT02_GIYAENDTM.GetValue().ToString()), int.Parse(this.DAT02_GIYAINCHL.GetValue().ToString()),
                                          this.DAT03_KBBUSEO.GetValue().ToString(), this.DAT02_GIYOIL.GetValue().ToString(), this.DAT02_GIHUMUCD.GetValue().ToString());
                    }
                    else
                    {
                        this.UP_Day_OT(sDATE, int.Parse(this.DAT02_GIYACHLTM.GetValue().ToString()), int.Parse(this.DAT02_GIYAENDTM.GetValue().ToString()), int.Parse(this.DAT02_GIYAINCHL.GetValue().ToString()),
                                          this.DAT03_KBBUSEO.GetValue().ToString(), this.DAT02_GIYOIL.GetValue().ToString(), this.DAT02_GIHUMUCD.GetValue().ToString());
                    }
                }
            }*/

            if (int.Parse(this.DAT02_GIYUNJGST.GetValue().ToString()) != 0 && int.Parse(this.DAT02_GIYUNJGED.GetValue().ToString()) != 0)
            {
                    if (this.DAT02_GIYOIL.GetValue().ToString() == "1" || this.DAT02_GIYOIL.GetValue().ToString() == "7" || this.DAT02_GIHUMUCD.GetValue().ToString() != "")
                    {
                        if (this.DAT03_KBBUSEO.GetValue().ToString().Substring(0, 1) == "T" && this.DAT03_GDROTGN.GetValue().ToString() != "0")
                        {
                            this.UP_HYUIL_380_UTT(sDATE, int.Parse(this.DAT02_GIYUNJGST.GetValue().ToString()), int.Parse(this.DAT02_GIYUNJGED.GetValue().ToString()), int.Parse(this.DAT02_GIYAINCHL.GetValue().ToString()),
                                                    this.DAT03_KBBUSEO.GetValue().ToString(), this.DAT02_GIYOIL.GetValue().ToString(), this.DAT02_GIHUMUCD.GetValue().ToString(), int.Parse(this.DAT03_GDROTGN.GetValue().ToString()));
                        }
                        else
                        {
                            if (this.DAT03_KBJKCD.GetValue().ToString() == "3C" || this.DAT03_KBJKCD.GetValue().ToString() == "3D" || this.DAT03_KBJKCD.GetValue().ToString() == "6C")
                            {
                                this.UP_NewDay_OT(sDATE, int.Parse(this.DAT02_GIYUNJGST.GetValue().ToString()), int.Parse(this.DAT02_GIYUNJGED.GetValue().ToString()), int.Parse(this.DAT02_GIYAINCHL.GetValue().ToString()),
                                                            this.DAT03_KBBUSEO.GetValue().ToString(), this.DAT02_GIYOIL.GetValue().ToString(), this.DAT02_GIHUMUCD.GetValue().ToString());
                            }
                            else
                            {
                                this.UP_OfficeDayoff_OT(sDATE, int.Parse(this.DAT02_GIYUNJGST.GetValue().ToString()), int.Parse(this.DAT02_GIYUNJGED.GetValue().ToString()), int.Parse(this.DAT02_GIYAINCHL.GetValue().ToString()),
                                                        this.DAT03_KBBUSEO.GetValue().ToString(), this.DAT02_GIYOIL.GetValue().ToString(), this.DAT02_GIHUMUCD.GetValue().ToString());
                            }
                        }
                    }
                    else
                    {
                        if (this.DAT03_KBBUSEO.GetValue().ToString().Substring(0, 1) == "T" && this.DAT03_GDROTGN.GetValue().ToString() != "0")
                        {
                            this.UP_HYUIL_380_UTT(sDATE, int.Parse(this.DAT02_GIYUNJGST.GetValue().ToString()), int.Parse(this.DAT02_GIYUNJGED.GetValue().ToString()), int.Parse(this.DAT02_GIYAINCHL.GetValue().ToString()),
                                                    this.DAT03_KBBUSEO.GetValue().ToString(), this.DAT02_GIYOIL.GetValue().ToString(), this.DAT02_GIHUMUCD.GetValue().ToString(), int.Parse(this.DAT03_GDROTGN.GetValue().ToString()));
                        }
                        else
                        {
                            if (this.DAT03_KBJKCD.GetValue().ToString() == "3C" || this.DAT03_KBJKCD.GetValue().ToString() == "3D" || this.DAT03_KBJKCD.GetValue().ToString() == "6C")
                            {
                                //SILO 평일 주간출근 없이 야간출근 하는경우 18:00 ~ 09:00 근무형태
                                if (this.DAT03_KBBUSEO.GetValue().ToString().Substring(0, 1) == "S" &&
                                     (int.Parse(this.DAT02_GICHLTIME.GetValue().ToString()) == 0 && int.Parse(this.DAT02_GIENDTIME.GetValue().ToString()) == 0 &&
                                      int.Parse(this.DAT02_GIYACHLTM.GetValue().ToString()) > 0 && int.Parse(this.DAT02_GIYAENDTM.GetValue().ToString()) > 0 
                                     )
                                   )
                                {
                                    this.UP_SILODay_OT(sDATE, int.Parse(this.DAT02_GIYUNJGST.GetValue().ToString()), int.Parse(this.DAT02_GIYUNJGED.GetValue().ToString()), int.Parse(this.DAT02_GIYAINCHL.GetValue().ToString()),
                                                                this.DAT03_KBBUSEO.GetValue().ToString(), this.DAT02_GIYOIL.GetValue().ToString(), this.DAT02_GIHUMUCD.GetValue().ToString());




                                }
                                else
                                {
                                    this.UP_NewDay_OT(sDATE, int.Parse(this.DAT02_GIYUNJGST.GetValue().ToString()), int.Parse(this.DAT02_GIYUNJGED.GetValue().ToString()), int.Parse(this.DAT02_GIYAINCHL.GetValue().ToString()),
                                                                this.DAT03_KBBUSEO.GetValue().ToString(), this.DAT02_GIYOIL.GetValue().ToString(), this.DAT02_GIHUMUCD.GetValue().ToString());
                                }
                            }
                            else
                            {
                                this.UP_Day_OT(sDATE, int.Parse(this.DAT02_GIYUNJGST.GetValue().ToString()), int.Parse(this.DAT02_GIYUNJGED.GetValue().ToString()), int.Parse(this.DAT02_GIYAINCHL.GetValue().ToString()),
                                                  this.DAT03_KBBUSEO.GetValue().ToString(), this.DAT02_GIYOIL.GetValue().ToString(), this.DAT02_GIHUMUCD.GetValue().ToString(),"2");
                            }
                        }
                    }
            }

            //총인정시간 계산
            if (this.DAT03_KBBUSEO.GetValue().ToString().Substring(0, 1) == "T" && this.DAT03_GDROTGN.GetValue().ToString() != "0")
            {
                if(UP_Get_OverTimeCheck())
                {
                    this.UP_Set_WkGroupOverTime(this.DAT03_GDROTGN.GetValue().ToString(), this.DAT02_GIYOIL.GetValue().ToString(), this.DAT02_GIHUMUCD.GetValue().ToString());
                }
            }
            else
            {
               if (UP_Get_OverTimeCheck())
               {
                   this.UP_Set_TotalOverTime(this.DAT02_GIYOIL.GetValue().ToString(), this.DAT02_GIHUMUCD.GetValue().ToString());
               }
            }
        }
        #endregion

        #region  Description : OT 계산 유무 판단
        private bool UP_Get_OverTimeCheck()
        {
            bool bResult = false;

            if (int.Parse(this.DAT02_GICHLTIME.GetValue().ToString()) > 0 ||
                int.Parse(this.DAT02_GIENDTIME.GetValue().ToString()) > 0 ||
                int.Parse(this.DAT02_GIYACHLTM.GetValue().ToString()) > 0 ||
                int.Parse(this.DAT02_GIYAENDTM.GetValue().ToString()) > 0 ||
                (int.Parse(this.DAT02_GIYUNJGST.GetValue().ToString()) > 0 && int.Parse(this.DAT02_GIYUNJGED.GetValue().ToString()) > 0) ||
                (int.Parse(this.DAT02_GIJOCHLST.GetValue().ToString()) > 0 && int.Parse(this.DAT02_GIJOCHLED.GetValue().ToString()) > 0)
               )
            {
                //근무조
                if (this.DAT03_GDROTGN.GetValue().ToString() != "0")
                {
                    if (this.DAT02_GIHUGACD.GetValue().ToString() != "120" && this.DAT02_GIHUGACD.GetValue().ToString() != "130" && this.DAT02_GIHUGACD.GetValue().ToString() != "205")
                    {
                        bResult = true;
                    }
                }
                else
                {
                    //SILO운영, 사무직
                    if (this.DAT02_GIHUGACD.GetValue().ToString() != "120" && this.DAT02_GIHUGACD.GetValue().ToString() != "130" && this.DAT02_GIHUGACD.GetValue().ToString() != "205")
                    {
                        bResult = true;
                    }
                }
            }

            return bResult;
        }
        #endregion


        #region  Description : 총 인정시간 계산 함수
        private void UP_Set_TotalOverTime(string sGIYOIL, string sGIHUMUCD)
        {
            double dGIJOTIME = 0;   //조출인정시간'
            double dGIHTTIME = 0;   //하프인정시간'
            double dGIOTTIME = 0;   //연장인정시간'
            double dGINTTIME = 0;   //철야인정시간'
            double dGIHUTIME = 0;   //특근인정시간'
            double dGIINTIME = 0;   //총인정시간

            dGIJOTIME = Convert.ToDouble(this.DAT02_GIJOTIME.GetValue().ToString()) * 1.5;
            dGIHTTIME = Convert.ToDouble(this.DAT02_GIHTTIME.GetValue().ToString()) * 0.5;
            dGIOTTIME = Convert.ToDouble(this.DAT02_GIOTTIME.GetValue().ToString()) * 1.5;
            dGINTTIME = Convert.ToDouble(this.DAT02_GINTTIME.GetValue().ToString()) * 2;
            dGIHUTIME = Convert.ToDouble(this.DAT02_GIHUTIME.GetValue().ToString()) * 2.5;

            dGIINTIME = dGIJOTIME + dGIHTTIME + dGIOTTIME + dGINTTIME + dGIHUTIME;

            /* 
            // SILO 야근근무 ->>  평  일 : 인정시간 - 8 
            if (this.DAT03_KBBUSEO.GetValue().ToString().Substring(0, 1) == "S" || this.DAT03_KBBUSEO.GetValue().ToString() == "E10200")
            {
                if ((this.DAT03_KBJKCD.GetValue().ToString() == "3C" || this.DAT03_KBJKCD.GetValue().ToString() == "3D") &&
                     (this.DAT02_GIYOIL.GetValue().ToString() == "2" ||
                      this.DAT02_GIYOIL.GetValue().ToString() == "3" ||
                      this.DAT02_GIYOIL.GetValue().ToString() == "4" ||
                      this.DAT02_GIYOIL.GetValue().ToString() == "5" ||
                      this.DAT02_GIYOIL.GetValue().ToString() == "6") &&
                      sGIHUMUCD.Trim() == "" )
                {
                    if (int.Parse(this.DAT02_GIYAINCHL.GetValue().ToString()) > 0 && int.Parse(this.DAT02_GIYAINEND.GetValue().ToString()) > 0)
                    {
                        dGIINTIME = dGIINTIME - 8;
                    }
                }
            }

            //SILO 야간출근시 4시간 공제 
            if (this.DAT03_KBBUSEO.GetValue().ToString().Substring(0, 1) == "S" || this.DAT03_KBBUSEO.GetValue().ToString() == "E10200")
            {
                if ( (this.DAT03_KBJKCD.GetValue().ToString() == "3C" || this.DAT03_KBJKCD.GetValue().ToString() == "3D") &&
                     (this.DAT02_GIYOIL.GetValue().ToString() == "2" ||
                      this.DAT02_GIYOIL.GetValue().ToString() == "3" ||
                      this.DAT02_GIYOIL.GetValue().ToString() == "4" ||
                      this.DAT02_GIYOIL.GetValue().ToString() == "5" ||
                      this.DAT02_GIYOIL.GetValue().ToString() == "6"))
                {
                   dGIINTIME = dGINTTIME == 16 ? dGIINTIME -= 4: dGIINTIME;
                }

                if (dGIHUTIME == 20)
                {
                    dGIINTIME -= 4;
                }
            } */
            

            this.DAT02_GIINTIME.SetValue(dGIINTIME.ToString());
        }

        
        private void UP_Set_WkGroupOverTime(string sWorkGroup, string sGIYOIL, string sGIHUMUCD)
        {
            int iENDTIME = 0;

            double dWKGITIME = 0;

            //법정공휴일경우
            if (sGIHUMUCD != "" )
            {
                switch (int.Parse(sWorkGroup))
                {
                    case 1: //M ( 07:00 ~ 15:00 )
                        dWKGITIME = Convert.ToDouble(this.DAT02_GIOTTIME.GetValue().ToString()) + 8;
                        this.DAT02_GIOTTIME.SetValue(dWKGITIME);
                        dWKGITIME = Convert.ToDouble(this.DAT02_GIINTIME.GetValue().ToString()) + 12;
                        this.DAT02_GIINTIME.SetValue(dWKGITIME);
                        break;
                    case 2: //E ( 15:00 ~ 23:00 )
                        dWKGITIME = Convert.ToDouble(this.DAT02_GIOTTIME.GetValue().ToString()) + 7;
                        this.DAT02_GIOTTIME.SetValue(dWKGITIME);
                        dWKGITIME = Convert.ToDouble(this.DAT02_GINTTIME.GetValue().ToString()) + 1;
                        this.DAT02_GINTTIME.SetValue(dWKGITIME);
                        dWKGITIME = Convert.ToDouble(this.DAT02_GIINTIME.GetValue().ToString()) + 12.5;
                        this.DAT02_GIINTIME.SetValue(dWKGITIME);
                        break;
                    case 3: //N ( 23:00 ~ 익일07:00 )
                        dWKGITIME = Convert.ToDouble(this.DAT02_GIOTTIME.GetValue().ToString()) + 1;
                        this.DAT02_GIOTTIME.SetValue(dWKGITIME);
                        dWKGITIME = Convert.ToDouble(this.DAT02_GINTTIME.GetValue().ToString()) + 7;
                        this.DAT02_GINTTIME.SetValue(dWKGITIME);
                        dWKGITIME = Convert.ToDouble(this.DAT02_GIINTIME.GetValue().ToString()) + 15.5;
                        this.DAT02_GIINTIME.SetValue(dWKGITIME);
                        break;
                }             
            }
            else
            {
                //평일,토요일, 일요일
                switch (int.Parse(sWorkGroup))
                {
                    case 1: //M ( 07:00 ~ 15:00 )
                        this.DAT02_GIINTIME.SetValue(0);
                        iENDTIME = 1500;
                        break;
                    case 2: //E ( 15:00 ~ 23:00 )
                        dWKGITIME =  Convert.ToDouble(this.DAT02_GIHTTIME.GetValue().ToString()) + 1;
                        this.DAT02_GIHTTIME.SetValue(dWKGITIME);
                        dWKGITIME = Convert.ToDouble(this.DAT02_GIINTIME.GetValue().ToString()) + 0.5;
                        this.DAT02_GIINTIME.SetValue(dWKGITIME);
                        iENDTIME = 2300;
                        break;
                    case 3: //N ( 23:00 ~ 익일07:00 )
                        dWKGITIME = Convert.ToDouble(this.DAT02_GIHTTIME.GetValue().ToString()) + 7;
                        this.DAT02_GIHTTIME.SetValue(dWKGITIME);
                        dWKGITIME = Convert.ToDouble(this.DAT02_GIINTIME.GetValue().ToString()) + 3.5;
                        this.DAT02_GIINTIME.SetValue(dWKGITIME);
                        iENDTIME = 0700;
                        break;
                }

                //반년차 쓰고 근무조 퇴근시간보다 퇴근시간이 빠르면 인정ot를 주지 않는다.
                if (this.DAT02_GIHUGACD.GetValue().ToString() == "140")
                {
                    if (int.Parse(this.DAT02_GIINENDTM.GetValue().ToString()) > 0 &&
                        int.Parse(this.DAT02_GIINENDTM.GetValue().ToString()) < iENDTIME)
                    {
                        this.DAT02_GIINTIME.SetValue(0);
                    }
                }
            }

            double dGIJOTIME = 0;   //조출인정시간'
            double dGIHTTIME = 0;   //하프인정시간'
            double dGIOTTIME = 0;   //연장인정시간'
            double dGINTTIME = 0;   //철야인정시간'
            double dGIHUTIME = 0;   //특근인정시간'
            double dGIINTIME = 0;   //총인정시간

            dGIJOTIME = Convert.ToDouble(this.DAT02_GIJOTIME.GetValue().ToString()) * 1.5;
            dGIHTTIME = Convert.ToDouble(this.DAT02_GIHTTIME.GetValue().ToString()) * 0.5;
            dGIOTTIME = Convert.ToDouble(this.DAT02_GIOTTIME.GetValue().ToString()) * 1.5;
            dGINTTIME = Convert.ToDouble(this.DAT02_GINTTIME.GetValue().ToString()) * 2;
            dGIHUTIME = Convert.ToDouble(this.DAT02_GIHUTIME.GetValue().ToString()) * 2.5;

            //dGIINTIME = dGIJOTIME + dGIHTTIME + dGIOTTIME + dGINTTIME + dGIHUTIME + Convert.ToDouble(this.DAT02_GIINTIME.GetValue().ToString());

            dGIINTIME = dGIJOTIME + dGIHTTIME + dGIOTTIME + dGINTTIME + dGIHUTIME;

            this.DAT02_GIINTIME.SetValue(dGIINTIME.ToString());
        }
        #endregion

        #region  Description : 연장근무신청서 연장 조회 함수
        private void UP_Get_OverTime(string sDATE)
        {
            DataRow[] rw;

           
                rw = dt_OtDoc.Select("GYDATE = '" + sDATE + "' AND GYSABUN = '" + this.DAT02_GISABUN.GetValue().ToString() + "'", "GYDATE ASC ");
                //연장근무 체크
                //연장근무신청서가 있으면 인정시간으로 한다.          
                for (int i = 0; i < rw.Length; i++)
                {
                    switch (rw[i]["GYGUBN"].ToString())
                    {
                        case "1": //조출
                            this.DAT02_GIJOCHLST.SetValue(rw[i]["GYSTTIME"].ToString());
                            this.DAT02_GIJOCHLED.SetValue(rw[i]["GYEDTIME"].ToString());
                            break;
                        case "2": //연장
                            this.DAT02_GIYUNJGST.SetValue(rw[i]["GYSTTIME"].ToString());
                            this.DAT02_GIYUNJGED.SetValue(rw[i]["GYEDTIME"].ToString());
                            break;
                        case "3": //대근
                            this.DAT02_GIYUNJGST.SetValue(rw[i]["GYSTTIME"].ToString());
                            this.DAT02_GIYUNJGED.SetValue(rw[i]["GYEDTIME"].ToString());
                            this.DAT03_DAEKEUN.SetValue("Y");
                            break;
                        case "4": //중식
                            if (int.Parse(this.DAT02_GIJOCHLST.GetValue().ToString()) > 0 && int.Parse(this.DAT02_GIJOCHLED.GetValue().ToString()) > 0)
                            {
                                this.DAT02_GIYUNJGST.SetValue(rw[i]["GYSTTIME"].ToString());
                                this.DAT02_GIYUNJGED.SetValue(rw[i]["GYEDTIME"].ToString());
                            }
                            else
                            {
                                this.DAT02_GIJOCHLST.SetValue(rw[i]["GYSTTIME"].ToString());
                                this.DAT02_GIJOCHLED.SetValue(rw[i]["GYEDTIME"].ToString());
                            }
                            break;

                    }
                    //대근자 있으면 대근으로 본다.
                    if (rw[i]["GYDGSABUN"].ToString() != "")
                    {
                        this.DAT03_DAEKEUN.SetValue("Y");
                    }
                }
            

        }
        #endregion

        #region  Description : 야간출근퇴근 인정시간 계산 함수
        private string UP_Get_InOutNightTime(string sDATE, string sKBJKCD, string sKBGUNMU, string sTime, string sTimeGubn)
        {
            string sNightTime = string.Empty;

            //sNightTime = UP_Get_AppTime(sDATE, sTime);

            DataRow[] rw;
            rw = dt_OtDoc.Select("GYDATE = '" + sDATE + "' AND GYSABUN = '" + this.DAT02_GISABUN.GetValue().ToString() + "' AND GYGUBN = '2' ", "GYDATE ASC ");
            
            //연장근무 체크
            //연장근무신청서가 있으면 인정시간으로 한다.            
            if (rw.Length > 0)
            {
                if (sTimeGubn == "1")
                {
                    sNightTime = rw[0]["GYSTTIME"].ToString();
                }
                else
                {
                    sNightTime = rw[0]["GYEDTIME"].ToString();
                }
            }

            DataRow[] rwWorkGroup;
            rwWorkGroup = dt_WorkGroup.Select("GDDATE = '" + sDATE + "' AND GDSABUN = '" + this.DAT02_GISABUN.GetValue().ToString() + "'", "GDDATE ASC ");

            //교대조
            if (rwWorkGroup.Length > 0)
            {
                if (sTimeGubn == "1")
                {
                    sNightTime = rwWorkGroup[0]["GDINTIME"].ToString();
                }
                else
                {
                    sNightTime = rwWorkGroup[0]["GDOUTTIME"].ToString();
                }
            }

            if (sNightTime == "")
            {
                if (sKBJKCD == "3C" || sKBJKCD == "3D" || sKBJKCD == "6C")
                {
                    //sNightTime = "2100";
                    sNightTime = "2030";
                }
            }

            if (sNightTime == "")
                sNightTime = "0";

            return sNightTime;
        }
        #endregion

        #region  Description : 공적외출, 사적외출 시간 계산
        private string UP_Set_AppGoOutTime(string sDATE, string sStartTime, string sEndTime )
        {
            string sKijunTime = string.Empty;

            double dAppTime = 0;

            DateTime dtLatetime1 = new DateTime(int.Parse(sDATE.Substring(0, 4)), int.Parse(sDATE.Substring(4, 2)), int.Parse(sDATE.Substring(6, 2)), int.Parse(sStartTime.Substring(0, 2)), int.Parse(sStartTime.Substring(2, 2)), 0);
            DateTime dtLatetime2 = new DateTime(int.Parse(sDATE.Substring(0, 4)), int.Parse(sDATE.Substring(4, 2)), int.Parse(sDATE.Substring(6, 2)), int.Parse(sEndTime.Substring(0, 2)), int.Parse(sEndTime.Substring(2, 2)), 0);
            
            TimeSpan timeSpan = dtLatetime2 - dtLatetime1;

            dAppTime = Convert.ToDouble(timeSpan.Hours.ToString()) + Math.Round(Convert.ToDouble(Convert.ToDouble(timeSpan.Minutes) / 60), 1);
            
            return dAppTime.ToString();
        }
        #endregion

        #region  Description : 지각시간 계산
        private string UP_Get_LateTime(string sDATE, string sTime, string sApprovalTime, string sYaChTime, string sYaChApprovalTime, string sKBGUNMU, string sKBJKCD, string sKBSABUN)
        {
            string sKijunTime = string.Empty;

            double dLastMMTime = 0;

            //sApprovalTime = int.Parse(sApprovalTime) > 0 ? sApprovalTime : sYaChApprovalTime;

            sApprovalTime = int.Parse(sApprovalTime) > 0 ? sTime : sYaChTime;

            //지각자
            if ( int.Parse(sApprovalTime.Trim()) > 0 )
            {
                //지각시간 계산
                if (sKBGUNMU == "1") //울산
                {
                    if (string.Compare(sKBJKCD, "2B") <= 0)  //대리이상
                    {
                        sKijunTime = "0830";
                    }
                    else
                    {
                        if (sKBJKCD == "3C" || sKBJKCD == "3D" || sKBJKCD == "6C")
                        {
                            if (int.Parse(this.DAT03_GDROTGN.GetValue().ToString()) != 0)
                            {
                                switch (int.Parse(this.DAT03_GDROTGN.GetValue().ToString()))
                                {
                                    case 1: //M ( 07:00 ~ 15:00 )
                                        sKijunTime = "0700";
                                        break;
                                    case 2: //E ( 15:00 ~ 23:00 )
                                        sKijunTime = "1500";
                                        break;
                                    case 3: //N ( 23:00 ~ 익일07:00 )
                                        sKijunTime = "2300";
                                        break;
                                }
                            }
                            else
                            {
                                if (int.Parse(sTime) == 0 && int.Parse(sYaChApprovalTime) != 0)
                                {
                                    //야간출근만 있을경우
                                    sKijunTime = sYaChApprovalTime;
                                }
                                else
                                {
                                    //sKijunTime = "0900";
                                    sKijunTime = "0830";
                                }
                            }
                        }
                        else //사무직
                        {
                            //sKijunTime = "0900";
                            sKijunTime = "0830";
                        }
                    }
                }
                else  //서울
                {
                    //sKijunTime = "0800";
                    sKijunTime = "0830";
                }

                if (sKijunTime == "")
                {
                    //sKijunTime = "0900";
                    sKijunTime = "0830";
                }
                else if (sKijunTime == "2400")
                {
                    sKijunTime = "2359";
                }
                
                DateTime dtLatetime1 = new DateTime(int.Parse(sDATE.Substring(0, 4)), int.Parse(sDATE.Substring(4, 2)), int.Parse(sDATE.Substring(6, 2)), int.Parse(sKijunTime.Substring(0, 2)), int.Parse(sKijunTime.Substring(2, 2)), 0);
                DateTime dtLatetime2 = new DateTime(int.Parse(sDATE.Substring(0, 4)), int.Parse(sDATE.Substring(4, 2)), int.Parse(sDATE.Substring(6, 2)), int.Parse(sApprovalTime.Substring(0, 2)), int.Parse(sApprovalTime.Substring(2, 2)), 0);

                TimeSpan timeSpan = dtLatetime2 - dtLatetime1;

                //지각시간(시)
                dLastMMTime = Convert.ToDouble(timeSpan.Hours.ToString()) + Math.Round(Convert.ToDouble(Convert.ToDouble(timeSpan.Minutes) / 60), 1);

                //1분이라도 지각이면 지각처리
                if (dLastMMTime <= 0)
                {
                    if (Convert.ToDouble(timeSpan.Minutes) > 0)
                    {
                        dLastMMTime = 0.1;
                    }
                }

                //자원봉사는 지각시간 제외
                if (this.DAT02_GIHUGACD.GetValue().ToString() == "220")
                {
                    dLastMMTime = 0;
                }

                if (dLastMMTime < 0)
                {
                    dLastMMTime = 0;
                }

            }

            //지각시간이 있어도 연차,출장,하기휴가 이면 지각시간 삭제
            if (this.DAT02_GIHUGACD.GetValue().ToString() == "120" || this.DAT02_GIHUGACD.GetValue().ToString() == "130" || this.DAT02_GIHUGACD.GetValue().ToString() == "205" || this.DAT02_GIHUMUCD.GetValue().ToString() != "" )
            {
                dLastMMTime = 0;
            }

            return dLastMMTime.ToString();
        }
        #endregion

        private string UP_Get_OutApprovalTime(string sDATE, string sKBJKCD, string sKBGUNMU, string sTime, string sGHCODE)
        {
            string sApprovalTime = string.Empty;
            string sWorkGroupTime = string.Empty;

            sApprovalTime = UP_Get_AppTime(sDATE, sTime);

            //교대조 체크
            //교대조는 교대조시간이 인정시간
            foreach (DataRow rw in dt_WorkGroup.Select("GDDATE = '" + sDATE + "' AND GDSABUN = '" + this.DAT02_GISABUN.GetValue().ToString() + "'", "GDDATE ASC "))
            {
                if (rw.ItemArray.Length > 0)
                {
                    sWorkGroupTime = rw.ItemArray[6].ToString();
                }
            }

            if (sWorkGroupTime != "")
            {
                //sApprovalTime =  Convert.ToInt32(sApprovalTime) 
                sApprovalTime = int.Parse(sWorkGroupTime) > int.Parse(sApprovalTime) ? sApprovalTime : sWorkGroupTime; 
            }

            return sApprovalTime;
        }

        private string UP_Get_InApprovalTime(string sDATE, string sKBJKCD, string sKBGUNMU, string sTime, string sGHCODE )
        {
            string sApprovalTime = string.Empty;
            
            if (string.Compare(sKBJKCD, "2B") <= 0)  //대리이상
            {
                if (sKBGUNMU == "1") //울산
                {
                    if (Convert.ToInt16(sTime) < 0830 || sGHCODE == "220")
                    {
                        sApprovalTime = "0830";
                    }
                }
                else //서울
                {
                    if (Convert.ToInt16(sTime) < 0800)
                    {
                        sApprovalTime = "0800";
                    }
                }               
            }
            else  //주임이하
            {
                if (sKBGUNMU == "1") //울산
                {
                    if (sGHCODE == "220") //자원봉사면 9시로 셋팅
                    {
                        //sApprovalTime = "0900";
                        sApprovalTime = "0830";
                    }
                    else
                    {
                        if (this.DAT03_KBJKCD.GetValue().ToString() == "3A" || this.DAT03_KBJKCD.GetValue().ToString() == "3B" ||
                            this.DAT03_KBJKCD.GetValue().ToString() == "2C" || this.DAT03_KBJKCD.GetValue().ToString() == "4A")
                        {
                            //if (Convert.ToInt16(sTime) <= 0900)
                            if (Convert.ToInt16(sTime) <= 0830)
                            {
                                //sApprovalTime = "0900";
                                sApprovalTime = "0830";
                            }                            
                        }
                        else if (this.DAT03_KBJKCD.GetValue().ToString() == "3C" || this.DAT03_KBJKCD.GetValue().ToString() == "3D" ||
                                  this.DAT03_KBJKCD.GetValue().ToString() == "6C")
                        {
                            if (this.DAT03_KBBUSEO.GetValue().ToString().Substring(0, 1) == "S")
                            {
                                //if (Convert.ToInt16(sTime) <= 0900)
                                if (Convert.ToInt16(sTime) <= 0830)
                                {
                                    //sApprovalTime = "0900";
                                    sApprovalTime = "0830";
                                }                            
                            }
                        }
                        else
                        {
                            sApprovalTime = UP_Get_AppTime(sDATE, sTime);
                        }
                    }

                    //연장근무신청서 시간이 출근인정시간보다 빠르면 연장근무 시작시간으로 한다.
                    if (int.Parse(this.DAT02_GIJOCHLST.GetValue().ToString()) > 0 )
                    {
                        if (sApprovalTime != "" && int.Parse(sApprovalTime) > int.Parse(this.DAT02_GIJOCHLST.GetValue().ToString()))
                        {
                            sApprovalTime = this.DAT02_GIJOCHLST.GetValue().ToString();
                        }
                    }

                    if (int.Parse(this.DAT02_GIYUNJGST.GetValue().ToString()) > 0)
                    {
                        if (sApprovalTime != "" && int.Parse(sApprovalTime) > int.Parse(this.DAT02_GIYUNJGST.GetValue().ToString()))
                        {
                            sApprovalTime = this.DAT02_GIYUNJGST.GetValue().ToString();
                        }
                    }

                    //if ( (this.DAT03_KBJKCD.GetValue().ToString() == "3C" || this.DAT03_KBJKCD.GetValue().ToString() == "3D" || this.DAT03_KBJKCD.GetValue().ToString() == "6C") &&
                    //     (int.Parse(this.DAT03_GDROTGN.GetValue().ToString()) > 0 ) )
                    //{
                    //    sApprovalTime = UP_Get_AppTime(sDATE, sTime);
                    //}

                    //if(this.DAT03_KBJKCD.GetValue().ToString() == "3C" || this.DAT03_KBJKCD.GetValue().ToString() == "3D" || this.DAT03_KBJKCD.GetValue().ToString() == "6C")
                    //{
                    //    sApprovalTime = UP_Get_AppTime(sDATE, sTime);
                    //}
                    
                }
                else //서울
                {
                    if (Convert.ToInt16(sTime) < 0800)
                    {
                        sApprovalTime = "0800";
                    }
                }
            }

            if (sApprovalTime == "")
            {
                sApprovalTime = UP_Get_AppTime(sDATE, sTime);
            }
  
            //교대조 체크
            //교대조는 교대조시간이 인정시간
            foreach (DataRow rw in dt_WorkGroup.Select("GDDATE = '" + sDATE + "' AND GDSABUN = '" + this.DAT02_GISABUN.GetValue().ToString() + "'", "GDDATE ASC "))
            {
                if (rw.ItemArray.Length > 0)
                {
                    sApprovalTime = rw.ItemArray[5].ToString();
                }
            }

            return sApprovalTime;
        }
        
        private string UP_Get_AppTime(string sDate, string sTime)
        {
            string  sApprovalTime = string.Empty;

            int iTimeHH = 0;
            int iTimeMM = 0;

            //기준시간 설정
            DateTime dtAppime = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), int.Parse(sTime.Substring(0, 2)), int.Parse(sTime.Substring(2, 2)), 0);

            iTimeHH = int.Parse(sTime.Substring(0, 2));
            iTimeMM = int.Parse(sTime.Substring(2, 2));

            if (dtAppime.Minute >= 0 && dtAppime.Minute <= 15)
            {
                iTimeMM = 0;
            }
            else if (dtAppime.Minute >= 16 && dtAppime.Minute <= 45)
            {
                iTimeMM = 30;
            }
            else if (dtAppime.Minute >= 46 && dtAppime.Minute <= 59)
            {
                iTimeHH += 1;
                iTimeMM = 0;
            }
            sApprovalTime = Set_Fill2(iTimeHH.ToString()) + Set_Fill2(iTimeMM.ToString());
            
            return sApprovalTime;
        }

        private void UP_MainGoOutTime_ADD(string sGODATE,string sGOSABUN,string sGOGUBUN,string sGOSEQ,string sGOITIME,string sGOJTIME,string sGOGITIME,string sGOGJTIME,string sGOREASON,string sGOACCEPT,
                                          string sGOHISAB)
        {
            datas_GTOIREF.Add(new object[] { sGODATE, sGOSABUN, sGOGUBUN , sGOSEQ, sGOITIME, sGOJTIME , sGOGITIME, sGOGJTIME, sGOREASON, sGOACCEPT, sGOHISAB });
        }

        private void UP_GoOutTime_ADD(string sGJDATE, string sGJSABUN, string sGJGUBUN, string sGJTIME, string sGJHISAB)
        {
            datas_GTOJREF.Add(new object[] { sGJDATE  , sGJSABUN , sGJGUBUN , sGJTIME, sGJHISAB } );
        }

        #region  Description : 근태월력 조회
        private void UP_Calendar_Read(string sYear, string sMonth, string sDay)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51F9N151", sYear, sMonth, sDay);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.DAT02_GIYOIL.SetValue(dt.Rows[0]["UYYOILCD"].ToString());
                this.DAT02_GIHUMUCD.SetValue(dt.Rows[0]["UYHUMUCD"].ToString());
            }
        }
        #endregion

        #region  Description : 개인휴무 조회
        private void UP_BreakDay_Read(string sGISABUN, string sDATE)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51JEQ179", sGISABUN, sDATE);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                //회사휴무가 없는경우만 개인휴무를 넣는다
                if (this.DAT02_GIHUMUCD.GetValue().ToString() == "")
                {               
                   this.DAT02_GIHUGACD.SetValue(dt.Rows[0]["GHCODE"].ToString());
                }              

                //유급휴가는 무조건 넣는다.
                if (dt.Rows[0]["GHCODE"].ToString() == "435")
                {
                    this.DAT02_GIHUGACD.SetValue(dt.Rows[0]["GHCODE"].ToString());
                }
            }
        }
        #endregion

        private string UP_GetGTOIREF_Read(string sGODATE, string sGOSABUN, string sGOGUBUN)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51FGK161", sGODATE, sGOSABUN, sGOGUBUN);
            Int16 iSeq = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            return Set_Fill2(iSeq.ToString());
        }

        private void UP_TYData_Clear()
        {
            this.DAT02_GIDATE.SetValue("");
            this.DAT02_GISABUN.SetValue("");
            this.DAT02_GIYOIL.SetValue("");
            this.DAT02_GIHUMUCD.SetValue("");
            this.DAT02_GIHUGACD.SetValue("");
            this.DAT02_GIYGCHUL.SetValue("");
            
            this.DAT02_GICHLTIME.SetValue(0);
            this.DAT02_GIENDTIME.SetValue(0);
            
            this.DAT02_GIINCHLTM.SetValue(0);
            this.DAT02_GIINENDTM.SetValue(0);
            
            this.DAT02_GIYACHLTM.SetValue(0);            
            this.DAT02_GIYAENDTM.SetValue(0);
            this.DAT02_GIYAINCHL.SetValue(0);
            this.DAT02_GIYAINEND.SetValue(0);

            this.DAT02_GIJOCHLST.SetValue(0);
            this.DAT02_GIJOCHLED.SetValue(0);
            this.DAT02_GIYUNJGST.SetValue(0);
            this.DAT02_GIYUNJGED.SetValue(0);
            this.DAT02_GIJOTIME.SetValue(0);
            this.DAT02_GIHTTIME.SetValue(0);
            this.DAT02_GIOTTIME.SetValue(0);
            this.DAT02_GINTTIME.SetValue(0);
            this.DAT02_GIHUTIME.SetValue(0);
            this.DAT02_GIGJTIME.SetValue(0);
            this.DAT02_GIGOTIME.SetValue(0);
            this.DAT02_GISATIME.SetValue(0);
            this.DAT02_GIJITIME.SetValue(0);
            this.DAT02_GIJTTIME.SetValue(0);
            this.DAT02_GIINTIME.SetValue(0);
            this.DAT02_GICARDGB.SetValue("");
            this.DAT02_GIHUILGN.SetValue("N");
            this.DAT02_GIHISAB.SetValue(TYUserInfo.EmpNo);

            this.DAT03_KBBUSEO.SetValue("");
            this.DAT03_KBJKCD.SetValue("");
            this.DAT03_KBGUNMU.SetValue("");
            this.DAT03_GHCODE.SetValue("");
            this.DAT03_GDROTGN.SetValue("0");
            this.DAT03_STGOTIME.SetValue("0");
            this.DAT03_EDGOTIME.SetValue("0");
            this.DAT03_STSATIME.SetValue("0");
            this.DAT03_EDSATIME.SetValue("0");
            this.DAT03_DAEKEUN.SetValue("N");
            //this.DAT03_GDDATE.SetValue("");
        }
        #endregion
        
        #region  Description : OT 인정시간 계산 함수
        private void UP_HYUIL_380_UTT(string sDate, int iST_TIME, int iED_TIME, int iGI_YAINCHL, string sBUSEO, string sGIYOIL, string sGIHUMUCD, int iTTGUBUN)
        {
            string sST_TIME = Set_Fill4(iST_TIME.ToString()); //연장시작시간
            string sED_TIME = Set_Fill4(iED_TIME.ToString()); //연장종료시간

            int iTTJOTIME = 0; //utt근무조 시작시간

            double iOTTIME = 0;
            double iNTTIME = 0;

            bool bCheck = false;

            bool bOTChek = false; 

            //야간출근시간보다 ot시작시간이 빠르면 8시간 근무를 안하다고 시작한것으로 보고 ot전체 시간은 1.5배로 보고 처리한다.
            if (iGI_YAINCHL > 0 && (iGI_YAINCHL > iST_TIME))
            {
                bOTChek = true; 
            }

            switch (iTTGUBUN)
            {
                case 1: iTTJOTIME = 0700;
                        break;
                case 2: iTTJOTIME = 1500;
                        break;
                case 3: iTTJOTIME = 2300;
                        break;
                case 5: iTTJOTIME = 0;
                        break;
            }

            //연장근무시작시간이 근무조 시간보다 빠르면 먼저 출근한것으로 한다.
            if (iTTJOTIME != 0 && iST_TIME < iTTJOTIME)
            {
                bOTChek = true;
            }
            
            string sDateTime = string.Empty;
            string sStandTime = string.Empty;
            string sNextTime = string.Empty;

            double iTimeHours;
            Int16 iTimeMinutes;
            double dMinutes = 0;

            int iLINK_TOTAL;

            int iHours = 0;
            
            sStandTime = sDate;

            DateTime dt_OTTime1 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), 07, 00, 0);

            DateTime dt_OTTime2 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), 15, 00, 0);

            DateTime dt_OTTime3 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), 22, 00, 0);

            DateTime dt_OTTime4 = dt_OTTime3.AddHours(8);
            DateTime dt_OTTime5 = dt_OTTime3.AddHours(9);


            //연장 시작시간 설정
            DateTime st_dt = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), int.Parse(sST_TIME.Substring(0, 2)), int.Parse(sST_TIME.Substring(2, 2)), 0);
            DateTime ed_dt = new DateTime();            

            if (Convert.ToInt16(sST_TIME.Substring(0, 2)) > Convert.ToInt16(sED_TIME.Substring(0, 2)))
            {
                sDateTime = String.Format("{0:yyyyMMdd}", st_dt.AddDays(1));


                if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 12)
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오후" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }
                else
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }

            }
            else
            {

                if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 12)
                {
                    if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 24)
                    {
                        sDateTime = String.Format("{0:yyyyMMdd}", st_dt.AddDays(1));
                        sED_TIME = "00" + sED_TIME.Substring(2, 2);
                        ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                    }
                    else
                    {
                        ed_dt = Convert.ToDateTime(Set_Date(sDate) + "오후" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                    }
                }
                else
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDate) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }

            }

            TimeSpan timeSpan = new TimeSpan();

            timeSpan = ed_dt - st_dt;

            iLINK_TOTAL = Convert.ToInt16(timeSpan.Hours.ToString());
            iTimeHours = iLINK_TOTAL;
            iTimeMinutes = 0;

            if (Convert.ToInt16(timeSpan.Minutes.ToString()) > 0)
            {
                iLINK_TOTAL = iLINK_TOTAL + 1;
                iTimeMinutes = Convert.ToInt16(timeSpan.Minutes.ToString());
            }

            iLINK_TOTAL = iLINK_TOTAL * 2;
            iTimeHours = iLINK_TOTAL;

            for (int i = 0; i < iLINK_TOTAL; i++)
            {
                st_dt = st_dt.AddMinutes(30);
                if (st_dt <= ed_dt)
                {
                    iTimeHours = iTimeHours - 0.5;

                    iHours = st_dt.Hour;

                    if (iTimeHours < 0)
                    {
                        st_dt = st_dt.AddHours(-30);
                        st_dt = st_dt.AddMinutes(Convert.ToDouble(iTimeMinutes));

                        iHours = (st_dt.Hour * 100) + iTimeMinutes;

                        bCheck = true;
                    }
                    else
                    {
                        if (iHours == 0)
                        {
                            iHours = 2400;
                        }
                        else
                        {
                            iHours = (iHours * 100) + st_dt.Minute;
                        }
                    }

                    if (bCheck)
                    {
                        dMinutes = Math.Round(Convert.ToDouble(iTimeMinutes) / 60, 1);
                    }
                    else
                    {
                        dMinutes = 0;
                    }

                    if (bOTChek != true)
                    {
                        //오전 7시 ~ 오후 15시까지
                        if (iHours > 700 && iHours <= 1500)
                        {
                            if (iHours > 1200 && iHours <= 1300)
                            {
                                if (iTTGUBUN == 5)  //비번조가 ot하면 점심시간 1시간 제외
                                {
                                    //iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                                    iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.0;
                                }
                                else
                                {
                                    iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                                }
                            }
                            else
                            {
                                iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                            }
                        }

                        if (iHours > 1500 && iHours <= 2200)
                        {
                            iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                        }

                        if (iHours > 2200 && iHours <= 2400)
                        {
                            iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                        }

                        if (iHours >= 0 && iHours <= 600)
                        {
                            iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                        }

                        if (iHours > 600 && iHours <= 700)
                        {
                            iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                        }
                    }
                    else
                    {
                        //오전 7시 ~ 오후 15시까지
                        if (iHours > 700 && iHours <= 1500)
                        {
                            iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                        }

                        if (iHours > 1500 && iHours <= 2200)
                        {
                            iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                        }

                        //교대조이면 심야할증 처리
                        if (iTTGUBUN > 0 && iTTGUBUN < 5)
                        {
                            if (iHours > 2200 && iHours <= 2400)
                            {
                                iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                            }

                            if (iHours >= 0 && iHours <= 600)
                            {
                                iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                            }
                        }
                        else
                        {
                            if (iHours > 2200 && iHours <= 2400)
                            {
                                iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                            }

                            if (iHours >= 0 && iHours <= 600)
                            {
                                iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                            }
                        }                                               

                        if (iHours > 600 && iHours <= 700)
                        {
                            iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                        }
                    }

                }
            }

            double dGIOTTIME = Convert.ToDouble(this.DAT02_GIOTTIME.GetValue().ToString());
            double dGINTTIME = Convert.ToDouble(this.DAT02_GINTTIME.GetValue().ToString());

            this.DAT02_GIOTTIME.SetValue(Math.Round(dGIOTTIME + iOTTIME, 1).ToString());
            this.DAT02_GINTTIME.SetValue(Math.Round(dGINTTIME + iNTTIME, 1).ToString());
        }

        private void UP_Day_OT(string sDate, int iST_TIME, int iED_TIME, int iGI_YAINCHL, string sBUSEO, string sGIYOIL, string sGIHUMUCD, string OTGubn)
        {
            string sST_TIME = Set_Fill4(iST_TIME.ToString()); //연장시작시간
            string sED_TIME = Set_Fill4(iED_TIME.ToString()); //연장종료시간                       
            
            
            double iOTTIME = 0;
            double iNTTIME = 0;
            double iHUTIME = 0;

            bool bCheck = false;

            string sDateTime = string.Empty;
            string sStandTime = string.Empty;
            string sNextTime = string.Empty;

            double iTimeHours;
            Int16 iTimeMinutes;
            double dMinutes = 0;

            int iLINK_TOTAL;

            int iHours = 0;


            sStandTime = sDate;

            //dLINK_TOTAL = Convert.ToDecimal(UP_COUNT_380(int.Parse(sST_TIME.Substring(0, 2)), int.Parse(sED_TIME.Substring(0, 2)))) + 1;

            DateTime dt_OTTime1 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), 07, 00, 0);

            DateTime dt_OTTime2 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), 15, 00, 0);

            DateTime dt_OTTime3 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), 22, 00, 0);

            DateTime dt_OTTime4 = dt_OTTime3.AddHours(8);
            DateTime dt_OTTime5 = dt_OTTime3.AddHours(9);

            //연장 시작시간 설정
            DateTime st_dt = new DateTime();            
            DateTime ed_dt = new DateTime();

            //연장 시작일자가 24시 처리
            if (sST_TIME.Substring(0, 2) == "24")
            {
                sST_TIME = "00" + sST_TIME.Substring(2, 2);

                st_dt = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), int.Parse(sST_TIME.Substring(0, 2)), int.Parse(sST_TIME.Substring(2, 2)), 0);

                sDate = String.Format("{0:yyyyMMdd}", st_dt.AddDays(1));

                st_dt = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), int.Parse(sST_TIME.Substring(0, 2)), int.Parse(sST_TIME.Substring(2, 2)), 0);
            }
            else
            {
                st_dt = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), int.Parse(sST_TIME.Substring(0, 2)), int.Parse(sST_TIME.Substring(2, 2)), 0);
            }

            if (Convert.ToInt16(sST_TIME.Substring(0, 2)) > Convert.ToInt16(sED_TIME.Substring(0, 2)))
            {
                sDateTime = String.Format("{0:yyyyMMdd}", st_dt.AddDays(1));

                if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 12)
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오후" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }
                else
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }
            }
            else
            {
                if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 12)
                {
                    if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 24)
                    {
                        sDateTime = String.Format("{0:yyyyMMdd}", st_dt.AddDays(1));
                        sED_TIME = "00" + sED_TIME.Substring(2, 2);
                        ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                    }
                    else
                    {
                        ed_dt = Convert.ToDateTime(Set_Date(sDate) + "오후" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                    }
                }
                else
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDate) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }
            }

            TimeSpan timeSpan = new TimeSpan();


            timeSpan = ed_dt - st_dt;

            iLINK_TOTAL = Convert.ToInt16(timeSpan.Hours.ToString());

            iTimeMinutes = 0;

            if (Convert.ToInt16(timeSpan.Minutes.ToString()) > 0)
            {
                iLINK_TOTAL = iLINK_TOTAL + 1;
                iTimeMinutes = Convert.ToInt16(timeSpan.Minutes.ToString());
            }

            iTimeHours = iLINK_TOTAL;
            iLINK_TOTAL = iLINK_TOTAL * 2;
            

            for (int i = 0; i < iLINK_TOTAL; i++)
            {
                if (bCheck == false)
                {
                    st_dt = st_dt.AddMinutes(30);

                    if (st_dt <= ed_dt)
                    {

                        iTimeHours = iTimeHours - 0.5;

                        iHours = st_dt.Hour;


                        //if (st_dt <= ed_dt)
                        //{
                        //    st_dt = st_dt.AddMinutes(-30);
                        //    st_dt = st_dt.AddMinutes(Convert.ToDouble(iTimeMinutes));

                        //    iHours = (st_dt.Hour * 100) + iTimeMinutes;

                        //    bCheck = true;
                        //}
                        //else
                        //{
                        //    if (iHours == 0)
                        //    {
                        //        iHours = 2400;
                        //    }
                        //    else
                        //    {
                        //        iHours = (iHours * 100) + st_dt.Minute;
                        //    }
                        //}

                        if (iTimeHours < 0)
                        {
                            st_dt = st_dt.AddMinutes(-30);
                            st_dt = st_dt.AddMinutes(Convert.ToDouble(iTimeMinutes));

                            iHours = (st_dt.Hour * 100) + iTimeMinutes;

                            bCheck = true;
                        }
                        else
                        {
                            if (iHours == 0)
                            {
                                iHours = 2400;
                            }
                            else
                            {
                                iHours = (iHours * 100) + st_dt.Minute;
                            }
                        }

                        if (bCheck)
                        {
                            dMinutes = Math.Round(Convert.ToDouble(iTimeMinutes) / 60, 1);
                        }
                        else
                        {
                            dMinutes = 0;
                        }

                        // 오후 18시 ~ 오후 22시 * 1.5
                        if (iHours > 900 && iHours <= 2200)
                        {
                            iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                        }
                        // 오후 22시 ~ 오후 24시 * 2
                        if (iHours > 2200 && iHours <= 2400)
                        {
                            iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                        }
                        // 오후 24시 ~ 오전 6시 * 2
                        if (iHours >= 0 && iHours <= 600)
                        {
                            iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                        }
                        // 오전 6시 ~ 오전 9시 * 1.5
                        if (iHours > 600 && iHours <= 900)
                        {
                            iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                        }

                    }

                }

            }

            double dGIJOTIME = Convert.ToDouble(this.DAT02_GIJOTIME.GetValue().ToString());
            double dGIOTTIME = Convert.ToDouble(this.DAT02_GIOTTIME.GetValue().ToString());
            double dGINTTIME = Convert.ToDouble(this.DAT02_GINTTIME.GetValue().ToString());
            double dGIHUTIME = Convert.ToDouble(this.DAT02_GIHUTIME.GetValue().ToString());

            if (OTGubn == "1")
            {
                this.DAT02_GIJOTIME.SetValue(Math.Round(dGIJOTIME+ iOTTIME, 1).ToString());
            }
            else
            {
                this.DAT02_GIOTTIME.SetValue(Math.Round(dGIOTTIME + iOTTIME, 1).ToString());
            }
            this.DAT02_GINTTIME.SetValue(Math.Round(dGINTTIME + iNTTIME, 1).ToString());
            this.DAT02_GIHUTIME.SetValue(Math.Round(dGIHUTIME + iHUTIME, 1).ToString());
        }

        private void UP_DayoffUTT_OT(string sDate, int iST_TIME, int iED_TIME, int iGI_YAINCHL, string sBUSEO, string sGIYOIL, string sGIHUMUCD)
        {
            string sST_TIME = Set_Fill4(iST_TIME.ToString()); //연장시작시간
            string sED_TIME = Set_Fill4(iED_TIME.ToString()); //연장종료시간

            double iOTTIME = 0;
            double iNTTIME = 0;
            double iHUTIME = 0;

            bool bCheck = false;

            string sDateTime = string.Empty;
            string sStandTime = string.Empty;
            string sNextTime = string.Empty;

            double iTimeHours;
            Int16 iTimeMinutes;
            double dMinutes = 0;

            int iLINK_TOTAL;

            int iHours = 0;


            sStandTime = sDate;

            //dLINK_TOTAL = Convert.ToDecimal(UP_COUNT_380(int.Parse(sST_TIME.Substring(0, 2)), int.Parse(sED_TIME.Substring(0, 2)))) + 1;

            DateTime dt_OTTime1 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), 07, 00, 0);

            DateTime dt_OTTime2 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), 15, 00, 0);

            DateTime dt_OTTime3 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), 22, 00, 0);

            DateTime dt_OTTime4 = dt_OTTime3.AddHours(8);
            DateTime dt_OTTime5 = dt_OTTime3.AddHours(9);


            //연장 시작시간 설정
            DateTime st_dt = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), int.Parse(sST_TIME.Substring(0, 2)), int.Parse(sST_TIME.Substring(2, 2)), 0);
            DateTime ed_dt = new DateTime();

            if (Convert.ToInt16(sST_TIME.Substring(0, 2)) > Convert.ToInt16(sED_TIME.Substring(0, 2)))
            {
                sDateTime = String.Format("{0:yyyyMMdd}", st_dt.AddDays(1));


                if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 12)
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오후" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }
                else
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }

            }
            else
            {

                if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 12)
                {
                    if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 24)
                    {
                        sDateTime = String.Format("{0:yyyyMMdd}", st_dt.AddDays(1));
                        sED_TIME = "00" + sED_TIME.Substring(2, 2);
                        ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                    }
                    else
                    {
                        ed_dt = Convert.ToDateTime(Set_Date(sDate) + "오후" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                    }
                }
                else
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDate) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }

            }

            TimeSpan timeSpan = new TimeSpan();


            timeSpan = ed_dt - st_dt;

            iLINK_TOTAL = Convert.ToInt16(timeSpan.Hours.ToString());

            iTimeMinutes = 0;

            if (Convert.ToInt16(timeSpan.Minutes.ToString()) > 0)
            {
                iLINK_TOTAL = iLINK_TOTAL + 1;
                iTimeMinutes = Convert.ToInt16(timeSpan.Minutes.ToString());
            }

            iLINK_TOTAL = iLINK_TOTAL * 2;
            iTimeHours = iLINK_TOTAL;

            for (int i = 0; i < iLINK_TOTAL; i++)
            {

                st_dt = st_dt.AddMinutes(30);
                iTimeHours = iTimeHours - 0.5;

                iHours = st_dt.Hour;

                if (iTimeHours < 0)
                {
                    st_dt = st_dt.AddMinutes(-30);
                    st_dt = st_dt.AddMinutes(Convert.ToDouble(iTimeMinutes));

                    iHours = (st_dt.Hour * 100) + iTimeMinutes;

                    bCheck = true;
                }
                else
                {
                    if (iHours == 0)
                    {
                        iHours = 2400;
                    }
                    else
                    {
                        iHours = (iHours * 100) + st_dt.Minute;
                    }
                }

                if (bCheck)
                {
                    dMinutes = Math.Round(Convert.ToDouble(iTimeMinutes) / 60, 1);
                }
                else
                {
                    dMinutes = 0;
                }

                //오전 9시 ~ 오후 18시까지*1.5
                if (iHours > 900 && iHours <= 1800)
                {
                    if (iHours > 1200 && iHours <= 1300)
                    {
                        //iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0;
                        iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                    }
                    else
                    {
                        iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                    }
                }
                // 오후 18시 ~ 오후 22시 * 2
                if (iHours > 1800 && iHours <= 2200)
                {
                    iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                }
                // 오후 22시 ~ 오후 24시 * 2.5
                if (iHours > 2200 && iHours <= 2400)
                {
                    iHUTIME = dMinutes > 0 ? iHUTIME + dMinutes : iHUTIME + 0.5;
                }
                // 오후 24시 ~ 오전 6시 * 2.5

                if (iHours >= 0 && iHours <= 600)
                {
                    iHUTIME = dMinutes > 0 ? iHUTIME + dMinutes : iHUTIME + 0.5;
                }
                // 오전 6시 ~ 오전 9시 * 2
                if (iHours > 600 && iHours <= 900)
                {
                    //iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                    iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                }

            }

            
            double dGIOTTIME = Convert.ToDouble(this.DAT02_GIOTTIME.GetValue().ToString());
            double dGINTTIME = Convert.ToDouble(this.DAT02_GINTTIME.GetValue().ToString());
            double dGIHUTIME = Convert.ToDouble(this.DAT02_GIHUTIME.GetValue().ToString());

            this.DAT02_GIOTTIME.SetValue(Math.Round(dGIOTTIME + iOTTIME, 1).ToString());
            this.DAT02_GINTTIME.SetValue(Math.Round(dGINTTIME + iNTTIME, 1).ToString());
            this.DAT02_GIHUTIME.SetValue(Math.Round(dGIHUTIME + iHUTIME, 1).ToString());
        }


        private void UP_Dayoff_OT(string sDate, int iST_TIME, int iED_TIME, int iGI_YAINCHL, string sBUSEO, string sGIYOIL, string sGIHUMUCD, string OTGubn)
        {
            string sST_TIME = Set_Fill4(iST_TIME.ToString()); //연장시작시간
            string sED_TIME = Set_Fill4(iED_TIME.ToString()); //연장종료시간

            double iOTTIME = 0;
            double iNTTIME = 0;
            double iHUTIME = 0;

            bool bCheck = false;

            string sDateTime = string.Empty;
            string sStandTime = string.Empty;
            string sNextTime = string.Empty;

            double iTimeHours;
            Int16 iTimeMinutes;
            double dMinutes = 0;

            int iLINK_TOTAL;

            int iHours = 0;


            sStandTime = sDate;

            //dLINK_TOTAL = Convert.ToDecimal(UP_COUNT_380(int.Parse(sST_TIME.Substring(0, 2)), int.Parse(sED_TIME.Substring(0, 2)))) + 1;

            DateTime dt_OTTime1 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), 07, 00, 0);

            DateTime dt_OTTime2 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), 15, 00, 0);

            DateTime dt_OTTime3 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), 22, 00, 0);

            DateTime dt_OTTime4 = dt_OTTime3.AddHours(8);
            DateTime dt_OTTime5 = dt_OTTime3.AddHours(9);


            //연장 시작시간 설정
            DateTime st_dt = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), int.Parse(sST_TIME.Substring(0, 2)), int.Parse(sST_TIME.Substring(2, 2)), 0);
            DateTime ed_dt = new DateTime();

            if (Convert.ToInt16(sST_TIME.Substring(0, 2)) > Convert.ToInt16(sED_TIME.Substring(0, 2)))
            {
                sDateTime = String.Format("{0:yyyyMMdd}", st_dt.AddDays(1));


                if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 12)
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오후" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }
                else
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }

            }
            else
            {

                if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 12)
                {                    
                    if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 24)
                    {
                        sDateTime = String.Format("{0:yyyyMMdd}", st_dt.AddDays(1));
                        sED_TIME = "00" + sED_TIME.Substring(2, 2);
                        ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                    }
                    else
                    {
                        ed_dt = Convert.ToDateTime(Set_Date(sDate) + "오후" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                    }
                }
                else
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDate) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }

            }

            TimeSpan timeSpan = new TimeSpan();


            timeSpan = ed_dt - st_dt;

            iLINK_TOTAL = Convert.ToInt16(timeSpan.Hours.ToString());

            iTimeMinutes = 0;

            if (Convert.ToInt16(timeSpan.Minutes.ToString()) > 0)
            {
                iLINK_TOTAL = iLINK_TOTAL + 1;
                iTimeMinutes = Convert.ToInt16(timeSpan.Minutes.ToString());
            }

            iLINK_TOTAL = iLINK_TOTAL * 2;
            iTimeHours = iLINK_TOTAL;

            for (int i = 0; i < iLINK_TOTAL; i++)
            {

                st_dt = st_dt.AddMinutes(30);
                iTimeHours = iTimeHours - 0.5;

                iHours = st_dt.Hour;

                if (iTimeHours < 0)
                {
                    st_dt = st_dt.AddMinutes(-30);
                    st_dt = st_dt.AddMinutes(Convert.ToDouble(iTimeMinutes));

                    iHours = (st_dt.Hour * 100) + iTimeMinutes;

                    bCheck = true;
                }
                else
                {
                    if (iHours == 0)
                    {
                        iHours = 2400;
                    }
                    else
                    {
                        iHours = (iHours * 100) + st_dt.Minute;
                    }
                }

                if (bCheck)
                {
                    dMinutes = Math.Round(Convert.ToDouble(iTimeMinutes) / 60, 1);
                }
                else
                {
                    dMinutes = 0;
                }

                //오전 9시 ~ 오후 18시까지*1.5
                if (iHours > 900 && iHours <= 1800)
                {
                    if (iHours > 1200 && iHours <= 1300)
                    {
                        iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0;                        
                    }
                    else
                    {
                        iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                    }
                }
                // 오후 18시 ~ 오후 22시 * 2
                if (iHours > 1800 && iHours <= 2200)
                {
                    iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                }
                // 오후 22시 ~ 오후 24시 * 2.5
                if (iHours > 2200 && iHours <= 2400)
                {
                    iHUTIME = dMinutes > 0 ? iHUTIME + dMinutes : iHUTIME + 0.5;
                }
                // 오후 24시 ~ 오전 6시 * 2.5

                if (iHours >= 0 && iHours <= 600)
                {
                    iHUTIME = dMinutes > 0 ? iHUTIME + dMinutes : iHUTIME + 0.5;
                }
                // 오전 6시 ~ 오전 9시 * 2
                if (iHours > 600 && iHours <= 900)
                {
                    iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;                    
                }
            }


            double dGIJOTIME = Convert.ToDouble(this.DAT02_GIJOTIME.GetValue().ToString());
            double dGIOTTIME = Convert.ToDouble(this.DAT02_GIOTTIME.GetValue().ToString());
            double dGINTTIME = Convert.ToDouble(this.DAT02_GINTTIME.GetValue().ToString());
            double dGIHUTIME = Convert.ToDouble(this.DAT02_GIHUTIME.GetValue().ToString());

            if (OTGubn == "1")
            {
                this.DAT02_GIJOTIME.SetValue(Math.Round(dGIJOTIME + iOTTIME, 1).ToString());
            }
            else
            {
                this.DAT02_GIOTTIME.SetValue(Math.Round(dGIOTTIME + iOTTIME, 1).ToString());
            }
            this.DAT02_GINTTIME.SetValue(Math.Round(dGINTTIME + iNTTIME, 1).ToString());
            this.DAT02_GIHUTIME.SetValue(Math.Round(dGIHUTIME + iHUTIME, 1).ToString());
        }

        private void UP_OfficeDayoff_OT(string sDate, int iST_TIME, int iED_TIME, int iGI_YAINCHL, string sBUSEO, string sGIYOIL, string sGIHUMUCD)
        {
            string sST_TIME = Set_Fill4(iST_TIME.ToString()); //연장시작시간
            string sED_TIME = Set_Fill4(iED_TIME.ToString()); //연장종료시간

            double dStartTime = 0;

            double iOTTIME = 0;
            double iNTTIME = 0;
            double iHUTIME = 0;

            double dComTotalTime = 0;

            bool bCheck = false;

            string sDateTime = string.Empty;
            string sStandTime = string.Empty;
            string sNextTime = string.Empty;

            double iTimeHours;
            Int16 iTimeMinutes;
            double dMinutes = 0;

            int iLINK_TOTAL;

            int iHours = 0;

            string sStartTimeText = string.Empty;


            sStandTime = sDate;

            //dLINK_TOTAL = Convert.ToDecimal(UP_COUNT_380(int.Parse(sST_TIME.Substring(0, 2)), int.Parse(sED_TIME.Substring(0, 2)))) + 1;

            DateTime dt_OTTime1 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), 07, 00, 0);

            DateTime dt_OTTime2 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), 15, 00, 0);

            DateTime dt_OTTime3 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), 22, 00, 0);

            DateTime dt_OTTime4 = dt_OTTime3.AddHours(8);
            DateTime dt_OTTime5 = dt_OTTime3.AddHours(9);


            //연장 시작시간 설정
            DateTime st_dt = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), int.Parse(sST_TIME.Substring(0, 2)), int.Parse(sST_TIME.Substring(2, 2)), 0);
            DateTime ed_dt = new DateTime();

            if (Convert.ToInt16(sST_TIME.Substring(0, 2)) > Convert.ToInt16(sED_TIME.Substring(0, 2)))
            {
                sDateTime = String.Format("{0:yyyyMMdd}", st_dt.AddDays(1));


                if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 12)
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오후" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }
                else
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }

            }
            else
            {

                if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 12)
                {
                    if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 24)
                    {
                        sDateTime = String.Format("{0:yyyyMMdd}", st_dt.AddDays(1));
                        sED_TIME = "00" + sED_TIME.Substring(2, 2);
                        ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                    }
                    else
                    {
                        ed_dt = Convert.ToDateTime(Set_Date(sDate) + "오후" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                    }                    
                }
                else
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDate) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }

            }

            TimeSpan timeSpan = new TimeSpan();


            timeSpan = ed_dt - st_dt;

            iLINK_TOTAL = Convert.ToInt16(timeSpan.Hours.ToString());

            iTimeMinutes = 0;

            if (Convert.ToInt16(timeSpan.Minutes.ToString()) > 0)
            {
                iLINK_TOTAL = iLINK_TOTAL + 1;
                iTimeMinutes = Convert.ToInt16(timeSpan.Minutes.ToString());
            }

            iLINK_TOTAL = iLINK_TOTAL * 2;
            iTimeHours = iLINK_TOTAL;

            //최초 시작 시간
            dStartTime = Convert.ToDouble(sST_TIME.Trim().Substring(0, 2));

            if (dStartTime >= 06 && dStartTime <= 22)
            {
                sStartTimeText = "OT";
            }
            else if (dStartTime > 22 && dStartTime <= 24)
            {
                sStartTimeText = "NT";
            }
            else if (dStartTime >= 0 && dStartTime < 06)
            {
                sStartTimeText = "NT";
            }
            else
            {
                sStartTimeText = "OT";
            }

            for (int i = 0; i < iLINK_TOTAL; i++)
            {

                st_dt = st_dt.AddMinutes(30);
                iTimeHours = iTimeHours - 0.5;

                if (st_dt <= ed_dt)
                {

                    iHours = st_dt.Hour;

                    if (iTimeHours < 0)
                    {
                        st_dt = st_dt.AddMinutes(-30);
                        st_dt = st_dt.AddMinutes(Convert.ToDouble(iTimeMinutes));

                        iHours = (st_dt.Hour * 100) + iTimeMinutes;

                        bCheck = true;
                    }
                    else
                    {
                        if (iHours == 0)
                        {
                            iHours = 2400;
                        }
                        else
                        {
                            iHours = (iHours * 100) + st_dt.Minute;
                        }
                    }

                    if (bCheck)
                    {
                        dMinutes = Math.Round(Convert.ToDouble(iTimeMinutes) / 60, 1);
                    }
                    else
                    {
                        dMinutes = 0;
                    }


                    if (dComTotalTime < 8)
                    {
                        //오전 9시 ~ 오후 18시까지*1.5
                        if (iHours > 900 && iHours <= 1800)
                        {
                            if (iHours > 1200 && iHours <= 1300)
                            {
                                iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0;
                            }
                            else
                            {
                                iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                            }
                        }
                        // 오후 18시 ~ 오후 22시 * 2
                        if (iHours > 1800 && iHours <= 2200)
                        {
                            iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                        }
                        // 오후 22시 ~ 오후 24시 * 2.5
                        if (iHours > 2200 && iHours <= 2400)
                        {
                            iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                        }
                        // 오후 24시 ~ 오전 6시 * 2.5

                        if (iHours > 0 && iHours <= 600)
                        {
                            iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                        }
                        // 오전 6시 ~ 오전 9시 * 2
                        if (iHours > 600 && iHours <= 900)
                        {
                            iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                        }
                    }
                    else
                    {
                        //사무실 8시간 경과후는 2배 부터 시작한다.

                        //오전 9시 ~ 오후 18시까지*1.5
                        if (iHours > 900 && iHours <= 1800)
                        {
                            if (sStartTimeText == "OT")
                            {
                                if (iHours > 1200 && iHours <= 1300)
                                {
                                    iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0;
                                }
                                else
                                {
                                    iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                                }
                            }
                            else
                            {
                                if (iHours > 1200 && iHours <= 1300)
                                {
                                    iHUTIME = dMinutes > 0 ? iHUTIME + dMinutes : iHUTIME + 0;
                                }
                                else
                                {
                                    iHUTIME = dMinutes > 0 ? iHUTIME + dMinutes : iHUTIME + 0.5;
                                }
                            }
                        }
                        // 오후 18시 ~ 오후 22시 * 2
                        if (iHours > 1800 && iHours <= 2200)
                        {
                            if (sStartTimeText == "OT")
                            {
                                iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                            }
                            else
                            {
                                iHUTIME = dMinutes > 0 ? iHUTIME + dMinutes : iHUTIME + 0.5;
                            }
                        }
                        // 오후 22시 ~ 오후 24시 * 2.5
                        if (iHours > 2200 && iHours <= 2400)
                        {
                            if (sStartTimeText == "OT")
                            {
                                iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                            }
                            else
                            {
                                iHUTIME = dMinutes > 0 ? iHUTIME + dMinutes : iHUTIME + 0.5;
                            }
                        }
                        // 오후 24시 ~ 오전 6시 * 2.5

                        if (iHours >= 0 && iHours <= 600)
                        {
                            if (sStartTimeText == "OT")
                            {
                                iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                            }
                            else
                            {
                                iHUTIME = dMinutes > 0 ? iHUTIME + dMinutes : iHUTIME + 0.5;
                            }
                        }
                        // 오전 6시 ~ 오전 9시 * 2
                        if (iHours > 600 && iHours <= 900)
                        {
                            iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                        }
                    }

                    if (iHours > 1200 && iHours <= 1300)
                    {
                        dComTotalTime = dComTotalTime + 0;
                    }
                    else
                    {
                        dComTotalTime = dComTotalTime + 0.5;
                    }
                }

            }

            double dGIOTTIME = Convert.ToDouble(this.DAT02_GIOTTIME.GetValue().ToString());
            double dGINTTIME = Convert.ToDouble(this.DAT02_GINTTIME.GetValue().ToString());
            double dGIHUTIME = Convert.ToDouble(this.DAT02_GIHUTIME.GetValue().ToString());


            this.DAT02_GIOTTIME.SetValue(Math.Round(dGIOTTIME+iOTTIME, 1).ToString());
            this.DAT02_GINTTIME.SetValue(Math.Round(dGINTTIME+iNTTIME, 1).ToString());
            this.DAT02_GIHUTIME.SetValue(Math.Round(dGIHUTIME+iHUTIME, 1).ToString());
        }


        private void UP_NewDay_OT(string sDate, int iST_TIME, int iED_TIME, int iGI_YAINCHL, string sBUSEO, string sGIYOIL, string sGIHUMUCD)
        {
            string sST_TIME = Set_Fill4(iST_TIME.ToString()); //연장시작시간
            string sED_TIME = Set_Fill4(iED_TIME.ToString()); //연장종료시간

            double dStartTime = 0;

            double iHTTIME = 0;
            double iOTTIME = 0;
            double iNTTIME = 0;
            double iHUTIME = 0;

            double dComTotalTime = 0;

            bool bCheck = false;

            string sDateTime = string.Empty;
            string sStandTime = string.Empty;
            string sNextTime = string.Empty;

            double iTimeHours;
            Int16 iTimeMinutes;
            double dMinutes = 0;

            int iLINK_TOTAL;

            int iHours = 0;

            string sStartTimeText = string.Empty;


            sStandTime = sDate;

            //dLINK_TOTAL = Convert.ToDecimal(UP_COUNT_380(int.Parse(sST_TIME.Substring(0, 2)), int.Parse(sED_TIME.Substring(0, 2)))) + 1;

            DateTime dt_OTTime1 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), 07, 00, 0);

            DateTime dt_OTTime2 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), 15, 00, 0);

            DateTime dt_OTTime3 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), 22, 00, 0);

            DateTime dt_OTTime4 = dt_OTTime3.AddHours(8);
            DateTime dt_OTTime5 = dt_OTTime3.AddHours(9);


            //연장 시작시간 설정
            DateTime st_dt = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), int.Parse(sST_TIME.Substring(0, 2)), int.Parse(sST_TIME.Substring(2, 2)), 0);
            DateTime ed_dt = new DateTime();

            if (Convert.ToInt16(sST_TIME.Substring(0, 2)) > Convert.ToInt16(sED_TIME.Substring(0, 2)))
            {
                sDateTime = String.Format("{0:yyyyMMdd}", st_dt.AddDays(1));


                if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 12)
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오후" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }
                else
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }

            }
            else
            {

                if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 12)
                {
                    if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 24)
                    {
                        sDateTime = String.Format("{0:yyyyMMdd}", st_dt.AddDays(1));
                        sED_TIME = "00" + sED_TIME.Substring(2, 2);
                        ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                    }
                    else
                    {
                        ed_dt = Convert.ToDateTime(Set_Date(sDate) + "오후" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                    }                    
                }
                else
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDate) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }

            }

            TimeSpan timeSpan = new TimeSpan();


            timeSpan = ed_dt - st_dt;

            iLINK_TOTAL = Convert.ToInt16(timeSpan.Hours.ToString());

            iTimeMinutes = 0;

            if (Convert.ToInt16(timeSpan.Minutes.ToString()) > 0)
            {
                iLINK_TOTAL = iLINK_TOTAL + 1;
                iTimeMinutes = Convert.ToInt16(timeSpan.Minutes.ToString());
            }

            iLINK_TOTAL = iLINK_TOTAL * 2;
            iTimeHours = iLINK_TOTAL;

            //최초 시작 시간
            dStartTime = Convert.ToDouble(sST_TIME.Trim().Substring(0, 2));

            if (dStartTime >= 06 && dStartTime <= 22)
            {
                sStartTimeText = "OT";
            }
            else if (dStartTime > 22 && dStartTime <= 24)
            {
                sStartTimeText = "NT";
            }
            else if (dStartTime >= 0 && dStartTime < 06)
            {
                sStartTimeText = "NT";
            }
            else
            {
                sStartTimeText = "OT";
            }

            if (sGIYOIL == "1" || sGIYOIL == "7" || sGIHUMUCD != "")
            {
                //휴일
                for (int i = 0; i < iLINK_TOTAL; i++)
                {
                    st_dt = st_dt.AddMinutes(30);

                    if (st_dt <= ed_dt)
                    {
                        iTimeHours = iTimeHours - 0.5;

                        iHours = st_dt.Hour;

                        if (iTimeHours < 0)
                        {
                            st_dt = st_dt.AddMinutes(-30);
                            st_dt = st_dt.AddMinutes(Convert.ToDouble(iTimeMinutes));

                            iHours = (st_dt.Hour * 100) + iTimeMinutes;

                            bCheck = true;
                        }
                        else
                        {
                            if (iHours == 0)
                            {
                                iHours = 2400;
                            }
                            else
                            {
                                iHours = (iHours * 100) + st_dt.Minute;
                            }
                        }

                        if (bCheck)
                        {
                            dMinutes = Math.Round(Convert.ToDouble(iTimeMinutes) / 60, 1);
                        }
                        else
                        {
                            dMinutes = 0;
                        }


                        if (dComTotalTime < 8)
                        {
                            //오전 9시 ~ 오후 18시까지*1.5
                            if (iHours > 900 && iHours <= 1800)
                            {
                                if (iHours > 1200 && iHours <= 1300)
                                {
                                    iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0;
                                }
                                else
                                {
                                    iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                                }
                            }
                            // 오후 18시 ~ 오후 22시 * 1.5
                            if (iHours > 1800 && iHours <= 2200)
                            {
                                iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                            }
                            // 오후 22시 ~ 오후 24시 * 2
                            if (iHours > 2200 && iHours <= 2400)
                            {
                                iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                            }
                            // 오후 24시 ~ 오전 6시 * 2
                            if (iHours > 0 && iHours <= 600)
                            {
                                iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                            }
                            // 오전 6시 ~ 오전 9시 * 1.5
                            if (iHours > 600 && iHours <= 900)
                            {
                                iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                            }
                        }
                        else
                        {
                            //8시간 경과후는 2배 부터 시작한다.

                            //오전 9시 ~ 오후 18시까지* 2
                            if (iHours > 900 && iHours <= 1800)
                            {
                                if (iHours > 1200 && iHours <= 1300)
                                {
                                    iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0;
                                }
                                else
                                {
                                    iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                                }
                            }
                            // 오후 18시 ~ 오후 22시 * 2
                            if (iHours > 1800 && iHours <= 2200)
                            {
                                iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                            }
                            // 오후 22시 ~ 오후 24시 * 2.5
                            if (iHours > 2200 && iHours <= 2400)
                            {
                                iHUTIME = dMinutes > 0 ? iHUTIME + dMinutes : iHUTIME + 0.5;
                            }

                            // 오후 24시 ~ 오전 6시 * 2.5
                            if (iHours >= 0 && iHours <= 600)
                            {
                                iHUTIME = dMinutes > 0 ? iHUTIME + dMinutes : iHUTIME + 0.5;
                            }

                            // 오전 6시 ~ 오전 9시 * 2
                            if (iHours > 600 && iHours <= 900)
                            {
                                iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                            }
                        }

                        if (iHours > 1200 && iHours <= 1300)
                        {
                            dComTotalTime = dComTotalTime + 0;
                        }
                        else
                        {
                            dComTotalTime = dComTotalTime + 0.5;
                        }
                    }

                } //for ..end
            }
            else
            {
                //평일
                for (int i = 0; i < iLINK_TOTAL; i++)
                {
                    st_dt = st_dt.AddMinutes(30);

                    if (st_dt <= ed_dt)
                    {
                        iTimeHours = iTimeHours - 0.5;

                        iHours = st_dt.Hour;

                        if (iTimeHours < 0)
                        {
                            st_dt = st_dt.AddMinutes(-30);
                            st_dt = st_dt.AddMinutes(Convert.ToDouble(iTimeMinutes));

                            iHours = (st_dt.Hour * 100) + iTimeMinutes;

                            bCheck = true;
                        }
                        else
                        {
                            if (iHours == 0)
                            {
                                iHours = 2400;
                            }
                            else
                            {
                                iHours = (iHours * 100) + st_dt.Minute;
                            }
                        }

                        if (bCheck)
                        {
                            dMinutes = Math.Round(Convert.ToDouble(iTimeMinutes) / 60, 1);
                        }
                        else
                        {
                            dMinutes = 0;
                        }


                        if (dComTotalTime < 8)
                        {
                            if (int.Parse(this.DAT02_GIYACHLTM.GetValue().ToString()) > 0)
                            {
                                // 오후 22시 ~ 오후 24시 * 2.5
                                if (iHours > 2200 && iHours <= 2400)
                                {
                                    iHTTIME = dMinutes > 0 ? iHTTIME + dMinutes : iHTTIME + 0.5;
                                }
                                // 오후 24시 ~ 오전 6시 * 2.5
                                if (iHours > 0 && iHours <= 600)
                                {
                                    iHTTIME = dMinutes > 0 ? iHTTIME + dMinutes : iHTTIME + 0.5;
                                }
                            }
                            else
                            {

                                //오전 9시 ~ 오후 18시까지*1.5
                                if (iHours > 900 && iHours <= 1800)
                                {
                                    iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                                }
                                // 오후 18시 ~ 오후 22시 * 1.5
                                if (iHours > 1800 && iHours <= 2200)
                                {
                                    iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                                }
                                // 오후 22시 ~ 오후 24시 * 2
                                if (iHours > 2200 && iHours <= 2400)
                                {
                                    iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                                }
                                // 오후 24시 ~ 오전 6시 * 2
                                if (iHours >= 0 && iHours <= 600)
                                {
                                    iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                                }
                                // 오전 6시 ~ 오전 9시 * 1.5
                                if (iHours > 600 && iHours <= 900)
                                {
                                    iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                                }
                            }
                        }
                        else
                        {
                            //8시간 경과후는 2배 부터 시작한다.

                            //오전 9시 ~ 오후 18시까지*1.5
                            if (iHours > 900 && iHours <= 1800)
                            {
                                if (sStartTimeText == "OT")
                                {
                                    if (iHours > 1200 && iHours <= 1300)
                                    {
                                        iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0;
                                    }
                                    else
                                    {
                                        iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                                    }
                                }
                                else
                                {
                                    if (iHours > 1200 && iHours <= 1300)
                                    {
                                        iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0;
                                    }
                                    else
                                    {
                                        iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                                    }
                                }
                            }
                            // 오후 18시 ~ 오후 22시 * 1.5
                            if (iHours > 1800 && iHours <= 2200)
                            {
                                if (sStartTimeText == "OT")
                                {
                                    iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                                }
                                else
                                {
                                    iHUTIME = dMinutes > 0 ? iHUTIME + dMinutes : iHUTIME + 0.5;
                                }
                            }

                            // 오후 22시 ~ 오후 24시 * 2
                            if (iHours > 2200 && iHours <= 2400)
                            {
                                if (sStartTimeText == "OT")
                                {
                                    iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                                }
                                else
                                {
                                    iHUTIME = dMinutes > 0 ? iHUTIME + dMinutes : iHUTIME + 0.5;
                                }
                            }

                            // 오후 24시 ~ 오전 6시 * 2
                            if (iHours >= 0 && iHours <= 600)
                            {
                                if (sStartTimeText == "OT")
                                {
                                    iNTTIME = dMinutes > 0 ? iNTTIME + dMinutes : iNTTIME + 0.5;
                                }
                                else
                                {
                                    iHUTIME = dMinutes > 0 ? iHUTIME + dMinutes : iHUTIME + 0.5;
                                }
                            }
                            // 오전 6시 ~ 오전 9시 * 1.5
                            if (iHours > 600 && iHours <= 900)
                            {
                                iOTTIME = dMinutes > 0 ? iOTTIME + dMinutes : iOTTIME + 0.5;
                            }
                        }

                        if (iHours > 1200 && iHours <= 1300)
                        {
                            dComTotalTime = dComTotalTime + 0;
                        }
                        else
                        {
                            dComTotalTime = dComTotalTime + 0.5;
                        }
                    }
                } //for..end
            }

            double dGIHTTIME = Convert.ToDouble(this.DAT02_GIHTTIME.GetValue().ToString());
            double dGIOTTIME = Convert.ToDouble(this.DAT02_GIOTTIME.GetValue().ToString());
            double dGINTTIME = Convert.ToDouble(this.DAT02_GINTTIME.GetValue().ToString());
            double dGIHUTIME = Convert.ToDouble(this.DAT02_GIHUTIME.GetValue().ToString());

            this.DAT02_GIHTTIME.SetValue(Math.Round(dGIHTTIME+iHTTIME, 1).ToString());
            this.DAT02_GIOTTIME.SetValue(Math.Round(dGIOTTIME+iOTTIME, 1).ToString());
            this.DAT02_GINTTIME.SetValue(Math.Round(dGINTTIME+iNTTIME, 1).ToString());
            this.DAT02_GIHUTIME.SetValue(Math.Round(dGIHUTIME+iHUTIME, 1).ToString());
        }        

        private void UP_SILODay_OT(string sDate, int iST_TIME, int iED_TIME, int iGI_YAINCHL, string sBUSEO, string sGIYOIL, string sGIHUMUCD)
        {
            string sST_TIME = Set_Fill4(iST_TIME.ToString()); //연장시작시간
            string sED_TIME = Set_Fill4(iED_TIME.ToString()); //연장종료시간

            
            double iHTTIME = 0;
            double iOTTIME = 0;
            double iNTTIME = 0;
            double iHUTIME = 0;           
            

            string sDateTime = string.Empty;
            string sStandTime = string.Empty;
            string sNextTime = string.Empty;


            int iHours = 0;
            int iMinutes = 0;
            int iNowTime = 0;

            double dComTotalTime = 0;

            //10시 기준
            DateTime dt_wk10 = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), int.Parse("22"), int.Parse("00"), 0);
            //근무 8시간이 되는 시점 
            DateTime dt_wk8 = new DateTime();
            //익일 오전 6시 되는 시점
            DateTime dt_wk6 = new DateTime();

            //연장 시작시간 설정
            DateTime st_dt = new DateTime(int.Parse(sDate.Substring(0, 4)), int.Parse(sDate.Substring(4, 2)), int.Parse(sDate.Substring(6, 2)), int.Parse(sST_TIME.Substring(0, 2)), int.Parse(sST_TIME.Substring(2, 2)), 0);
            DateTime ed_dt = new DateTime();
            
            if (Convert.ToInt16(sST_TIME.Substring(0, 2)) > Convert.ToInt16(sED_TIME.Substring(0, 2)))
            {
                sDateTime = String.Format("{0:yyyyMMdd}", st_dt.AddDays(1));


                if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 12)
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오후" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }
                else
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }

            }
            else
            {

                if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 12)
                {                    
                    if (Convert.ToInt16(sED_TIME.Substring(0, 2)) >= 24)
                    {
                        sDateTime = String.Format("{0:yyyyMMdd}", st_dt.AddDays(1));
                        sED_TIME = "00" + sED_TIME.Substring(2, 2);
                        ed_dt = Convert.ToDateTime(Set_Date(sDateTime) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                    }
                    else
                    {
                        ed_dt = Convert.ToDateTime(Set_Date(sDate) + "오후" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                    }
                }
                else
                {
                    ed_dt = Convert.ToDateTime(Set_Date(sDate) + "오전" + sED_TIME.Substring(0, 2) + ":" + sED_TIME.Substring(2, 2) + ":00");
                }

            }

            //8시간 시점
            dt_wk8 = st_dt.AddHours(8);
            //익일 오전 6시
            sDateTime = String.Format("{0:yyyyMMdd}", st_dt.AddDays(1));
            dt_wk6 = Convert.ToDateTime(Set_Date(sDateTime) + "오전" + "06" + ":" + "00" + ":00");
            
            while (st_dt < ed_dt)
            {
                //30분씩 증가
                st_dt = st_dt.AddMinutes(30);
                dComTotalTime = dComTotalTime + 0.5;                          

                //현재시
                iHours = st_dt.Hour;
                //현재분
                iMinutes = st_dt.Minute;

                if( iHours == 0 )
                {
                    iHours = 24;
                }
                //현재시간
                iNowTime = (iHours * 100) + iMinutes;

                if (st_dt >= dt_wk10)
                {
                    //오후 10시 이후 ~ 8시간 되는 시점까지 * 0.5
                    if (st_dt > dt_wk10 && st_dt <= dt_wk8)
                    {
                        iHTTIME = iHTTIME + 0.5;
                    }
                    //8시간이후 ~ 오전 6시 * 2
                    if (st_dt > dt_wk8 && st_dt <= dt_wk6)
                    {
                        iNTTIME = iNTTIME + 0.5;
                    }
                    if (st_dt > dt_wk6)
                    {
                        iOTTIME = iOTTIME + 0.5;
                    }
                }                
            }



            double dGIHTTIME = Convert.ToDouble(this.DAT02_GIHTTIME.GetValue().ToString());
            double dGIOTTIME = Convert.ToDouble(this.DAT02_GIOTTIME.GetValue().ToString());
            double dGINTTIME = Convert.ToDouble(this.DAT02_GINTTIME.GetValue().ToString());
            double dGIHUTIME = Convert.ToDouble(this.DAT02_GIHUTIME.GetValue().ToString());


            this.DAT02_GIHTTIME.SetValue(Math.Round(dGIHTTIME+iHTTIME, 1).ToString());
            this.DAT02_GIOTTIME.SetValue(Math.Round(dGIOTTIME+iOTTIME, 1).ToString());
            this.DAT02_GINTTIME.SetValue(Math.Round(dGINTTIME+iNTTIME, 1).ToString());
            this.DAT02_GIHUTIME.SetValue(Math.Round(dGIHUTIME+iHUTIME, 1).ToString());
        }
        #endregion

        #region  Description : 일일근태관리 List 등록
        private void UP_Set_GTILREF_List()
        {
            bool bresult = false;

            if (this.DAT02_GIYOIL.GetValue().ToString() == "1" || this.DAT02_GIYOIL.GetValue().ToString() == "7" || this.DAT02_GIHUMUCD.GetValue().ToString() != "")
            {
                //휴일
                if (int.Parse(DAT02_GICHLTIME.GetValue().ToString()) > 0 ||
                    int.Parse(DAT02_GIENDTIME.GetValue().ToString()) > 0 ||
                    int.Parse(DAT02_GIYACHLTM.GetValue().ToString()) > 0 ||
                    int.Parse(DAT02_GIYAENDTM.GetValue().ToString()) > 0 ||
                    this.DAT02_GIHUGACD.GetValue().ToString().Trim() != "" )
                {
                    bresult = true;
                }
                else
                {
                    bresult = false;
                }

                //휴일 비번이면 제외
                if (this.DAT02_GIHUGACD.GetValue().ToString() == "520" &&
                    Convert.ToDouble(this.DAT02_GIINTIME.GetValue().ToString()) <= 0
                    )
                {
                    bresult = false;
                }

                //연봉직 토,일요일은 근태 등록하지 않는다.( 단, 정지석, 류상희 제외 )
                if (this.DAT02_GIYOIL.GetValue().ToString() == "1" || this.DAT02_GIYOIL.GetValue().ToString() == "7")
                {
                    if (  this.DAT03_KBJKCD.GetValue().ToString() == "01" ||
                           this.DAT03_KBJKCD.GetValue().ToString() == "1A" ||
                           this.DAT03_KBJKCD.GetValue().ToString() == "1B" ||
                           this.DAT03_KBJKCD.GetValue().ToString() == "2A" ||
                           this.DAT03_KBJKCD.GetValue().ToString() == "2B") 
                    {
                        if (DAT02_GISABUN.GetValue().ToString() == "0079-M" || DAT02_GISABUN.GetValue().ToString() == "0081-M")
                        {
                            bresult = true;
                        }
                        else
                        {
                            bresult = false;
                        }
                    }

                    if (this.DAT03_KBJKCD.GetValue().ToString() == "3A" ||
                        this.DAT03_KBJKCD.GetValue().ToString() == "3B" ||
                        this.DAT03_KBJKCD.GetValue().ToString() == "2C"
                        )
                    {
                        if (int.Parse(DAT02_GICHLTIME.GetValue().ToString()) > 0 ||
                            int.Parse(DAT02_GIENDTIME.GetValue().ToString()) > 0 ||
                            int.Parse(DAT02_GIYACHLTM.GetValue().ToString()) > 0 ||
                            int.Parse(DAT02_GIYAENDTM.GetValue().ToString()) > 0 )
                        {
                            bresult = true;
                        }
                        else
                        {
                            bresult = false;
                        }
                    }

                    if (this.DAT03_KBJKCD.GetValue().ToString() == "3C" || this.DAT03_KBJKCD.GetValue().ToString() == "3D")
                    {
                        if (this.DAT03_KBBUSEO.GetValue().ToString().Substring(0, 1) == "S" && this.DAT02_GIHUGACD.GetValue().ToString().Trim() != "")
                        {
                            bresult = false;
                        }
                    }

                }

            }
            else
            {              
                //평일
                bresult = true;
            }

            //철야퇴근만 있으면 등록 안함
            if (this.DAT02_GIHUGACD.GetValue().ToString() == "510")
            {
                //토,일요일
                if (this.DAT02_GIYOIL.GetValue().ToString() == "1" || this.DAT02_GIYOIL.GetValue().ToString() == "7")
                {
                    bresult = false;
                }
            }

            //임원은 휴무코드가 있는경우 등록한다.
            if (this.DAT03_KBJKCD.GetValue().ToString() == "01" && (this.DAT02_GIHUGACD.GetValue().ToString() == "" || this.DAT02_GIHUGACD.GetValue().ToString() == "900"))
            {
                bresult = false;
            }

            if (bresult)
            {
                //휴일 판단
                if (this.DAT02_GIYOIL.GetValue().ToString() == "1" || this.DAT02_GIYOIL.GetValue().ToString() == "7" || this.DAT02_GIHUMUCD.GetValue().ToString() != "")
                {
                    //교대조
                    if (this.DAT03_GDROTGN.GetValue().ToString() != "0")
                    {
                        //법정공휴일, 대근자
                        if (this.DAT02_GIHUMUCD.GetValue().ToString() != "" || this.DAT03_DAEKEUN.GetValue().ToString() == "Y")
                        {
                            this.DAT02_GIHUILGN.SetValue("Y");
                        }
                    }
                    else
                    {
                        this.DAT02_GIHUILGN.SetValue("Y");
                    }
                }
                else
                {
                    if (this.DAT03_GDROTGN.GetValue().ToString() != "0")
                    {
                        //법정공휴일, 대근자
                        if (this.DAT02_GIHUMUCD.GetValue().ToString() != "" || this.DAT03_DAEKEUN.GetValue().ToString() == "Y")
                        {
                            this.DAT02_GIHUILGN.SetValue("Y");
                        }
                    }
                }                


                this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_HR_62PE8566", this.DAT02_GIDATE.GetValue().ToString(), this.DAT02_GISABUN.GetValue().ToString());
                this.DbConnector.Attach("TY_P_HR_4BQJV569", this.DAT02_GIDATE.GetValue().ToString(), this.DAT02_GISABUN.GetValue().ToString());
                DataTable dgl = this.DbConnector.ExecuteDataTable();

                if (dgl.Rows.Count > 0)
                {
                     if (DAT02_GIHUMUCD.GetValue().ToString() != dgl.Rows[0]["GIHUMUCD"].ToString())
                     {
                         DAT02_GIHUMUCD.SetValue(dgl.Rows[0]["GIHUMUCD"].ToString());
                     }

                     if (DAT02_GIHUGACD.GetValue().ToString() != dgl.Rows[0]["GIHUGACD"].ToString())
                     {
                         //하기휴가, 년차,반년차
                         if (DAT02_GIHUGACD.GetValue().ToString() != "120" && DAT02_GIHUGACD.GetValue().ToString() != "130" && DAT02_GIHUGACD.GetValue().ToString() != "140")
                         {
                             DAT02_GIHUGACD.SetValue(dgl.Rows[0]["GIHUGACD"].ToString());
                         }

                         //기존 데이타가 결근이라도 다시 체크 한다.
                         if (dgl.Rows[0]["GIHUGACD"].ToString() == "900")
                         {
                             if (
                                 int.Parse(this.DAT03_GDROTGN.GetValue().ToString()) != 0 ||
                                 int.Parse(this.DAT02_GICHLTIME.GetValue().ToString()) != 0 ||
                                 int.Parse(this.DAT02_GIENDTIME.GetValue().ToString()) != 0 ||
                                 int.Parse(this.DAT02_GIYACHLTM.GetValue().ToString()) != 0 ||
                                 int.Parse(this.DAT02_GIYAENDTM.GetValue().ToString()) != 0 ||
                                 int.Parse(this.DAT02_GIJOCHLST.GetValue().ToString()) != 0 ||
                                 int.Parse(this.DAT02_GIYUNJGST.GetValue().ToString()) != 0
                                 )
                                {
                                    this.DAT02_GIHUGACD.SetValue("");
                                }
                         }
                     }

                     if (DAT02_GIYGCHUL.GetValue().ToString() != dgl.Rows[0]["GIYGCHUL"].ToString())
                     {
                         DAT02_GIYGCHUL.SetValue(dgl.Rows[0]["GIYGCHUL"].ToString());
                     }

                     //최초 등록 상태가 아니면 값을 비교하여 저장한다.
                     if (dgl.Rows[0]["GIHIGB"].ToString() != "A")
                     {
                         if (int.Parse(dgl.Rows[0]["GICHLTIME"].ToString()) > 0)
                         {
                             DAT02_GICHLTIME.SetValue(int.Parse(Get_Numeric(this.DAT02_GICHLTIME.GetValue().ToString())) != int.Parse(dgl.Rows[0]["GICHLTIME"].ToString()) ? int.Parse(dgl.Rows[0]["GICHLTIME"].ToString()) : int.Parse(Get_Numeric(this.DAT02_GICHLTIME.GetValue().ToString())));
                         }

                         if (int.Parse(dgl.Rows[0]["GIENDTIME"].ToString()) > 0)
                         {
                             DAT02_GIENDTIME.SetValue(int.Parse(Get_Numeric(this.DAT02_GIENDTIME.GetValue().ToString())) != int.Parse(dgl.Rows[0]["GIENDTIME"].ToString()) ? int.Parse(dgl.Rows[0]["GIENDTIME"].ToString()) : int.Parse(Get_Numeric(this.DAT02_GIENDTIME.GetValue().ToString())));
                         }

                         if (int.Parse(dgl.Rows[0]["GIINCHLTM"].ToString()) > 0)
                         {
                             DAT02_GIINCHLTM.SetValue(int.Parse(Get_Numeric(this.DAT02_GIINCHLTM.GetValue().ToString())) != int.Parse(dgl.Rows[0]["GIINCHLTM"].ToString()) ? int.Parse(dgl.Rows[0]["GIINCHLTM"].ToString()) : int.Parse(Get_Numeric(this.DAT02_GIINCHLTM.GetValue().ToString())));
                         }

                         if (int.Parse(dgl.Rows[0]["GIINENDTM"].ToString()) > 0)
                         {
                             DAT02_GIINENDTM.SetValue(int.Parse(Get_Numeric(this.DAT02_GIINENDTM.GetValue().ToString())) != int.Parse(dgl.Rows[0]["GIINENDTM"].ToString()) ? int.Parse(dgl.Rows[0]["GIINENDTM"].ToString()) : int.Parse(Get_Numeric(this.DAT02_GIINENDTM.GetValue().ToString())));
                         }
                         if (int.Parse(dgl.Rows[0]["GIYACHLTM"].ToString()) > 0)
                         {
                             DAT02_GIYACHLTM.SetValue(int.Parse(Get_Numeric(this.DAT02_GIYACHLTM.GetValue().ToString())) != int.Parse(dgl.Rows[0]["GIYACHLTM"].ToString()) ? int.Parse(dgl.Rows[0]["GIYACHLTM"].ToString()) : int.Parse(Get_Numeric(this.DAT02_GIYACHLTM.GetValue().ToString())));
                         }
                         if (int.Parse(dgl.Rows[0]["GIYAENDTM"].ToString()) > 0)
                         {
                             DAT02_GIYAENDTM.SetValue(int.Parse(Get_Numeric(this.DAT02_GIYAENDTM.GetValue().ToString())) != int.Parse(dgl.Rows[0]["GIYAENDTM"].ToString()) ? int.Parse(dgl.Rows[0]["GIYAENDTM"].ToString()) : int.Parse(Get_Numeric(this.DAT02_GIYAENDTM.GetValue().ToString())));
                         }
                         if (int.Parse(dgl.Rows[0]["GIYAINCHL"].ToString()) > 0)
                         {
                             DAT02_GIYAINCHL.SetValue(int.Parse(Get_Numeric(this.DAT02_GIYAINCHL.GetValue().ToString())) != int.Parse(dgl.Rows[0]["GIYAINCHL"].ToString()) ? int.Parse(dgl.Rows[0]["GIYAINCHL"].ToString()) : int.Parse(Get_Numeric(this.DAT02_GIYAINCHL.GetValue().ToString())));
                         }
                         if (int.Parse(dgl.Rows[0]["GIYAINEND"].ToString()) > 0)
                         {
                             DAT02_GIYAINEND.SetValue(int.Parse(Get_Numeric(this.DAT02_GIYAINEND.GetValue().ToString())) != int.Parse(dgl.Rows[0]["GIYAINEND"].ToString()) ? int.Parse(dgl.Rows[0]["GIYAINEND"].ToString()) : int.Parse(Get_Numeric(this.DAT02_GIYAINEND.GetValue().ToString())));
                         }

                         if (int.Parse(dgl.Rows[0]["GIJOCHLST"].ToString()) > 0)
                         {
                             DAT02_GIJOCHLST.SetValue(int.Parse(Get_Numeric(this.DAT02_GIJOCHLST.GetValue().ToString())) != int.Parse(dgl.Rows[0]["GIJOCHLST"].ToString()) ? int.Parse(dgl.Rows[0]["GIJOCHLST"].ToString()) : int.Parse(Get_Numeric(this.DAT02_GIJOCHLST.GetValue().ToString())));
                         }
                         if (int.Parse(dgl.Rows[0]["GIJOCHLED"].ToString()) > 0)
                         {
                             DAT02_GIJOCHLED.SetValue(int.Parse(Get_Numeric(this.DAT02_GIJOCHLED.GetValue().ToString())) != int.Parse(dgl.Rows[0]["GIJOCHLED"].ToString()) ? int.Parse(dgl.Rows[0]["GIJOCHLED"].ToString()) : int.Parse(Get_Numeric(this.DAT02_GIJOCHLED.GetValue().ToString())));
                         }
                         if (int.Parse(dgl.Rows[0]["GIYUNJGST"].ToString()) > 0)
                         {
                             DAT02_GIYUNJGST.SetValue(int.Parse(Get_Numeric(this.DAT02_GIYUNJGST.GetValue().ToString())) != int.Parse(dgl.Rows[0]["GIYUNJGST"].ToString()) ? int.Parse(dgl.Rows[0]["GIYUNJGST"].ToString()) : int.Parse(Get_Numeric(this.DAT02_GIYUNJGST.GetValue().ToString())));
                         }
                         if (int.Parse(dgl.Rows[0]["GIYUNJGED"].ToString()) > 0)
                         {
                             DAT02_GIYUNJGED.SetValue(int.Parse(Get_Numeric(this.DAT02_GIYUNJGED.GetValue().ToString())) != int.Parse(dgl.Rows[0]["GIYUNJGED"].ToString()) ? int.Parse(dgl.Rows[0]["GIYUNJGED"].ToString()) : int.Parse(Get_Numeric(this.DAT02_GIYUNJGED.GetValue().ToString())));
                         }

                         if (float.Parse(Get_Numeric(dgl.Rows[0]["GIJOTIME"].ToString())) > 0)
                         {
                             DAT02_GIJOTIME.SetValue(float.Parse(Get_Numeric(this.DAT02_GIJOTIME.GetValue().ToString())) != float.Parse(dgl.Rows[0]["GIJOTIME"].ToString()) ? float.Parse(dgl.Rows[0]["GIJOTIME"].ToString()) : float.Parse(Get_Numeric(this.DAT02_GIJOTIME.GetValue().ToString())));
                         }
                         if (float.Parse(Get_Numeric(dgl.Rows[0]["GIHTTIME"].ToString())) > 0)
                         {
                             DAT02_GIHTTIME.SetValue(float.Parse(Get_Numeric(this.DAT02_GIHTTIME.GetValue().ToString())) != float.Parse(dgl.Rows[0]["GIHTTIME"].ToString()) ? float.Parse(dgl.Rows[0]["GIHTTIME"].ToString()) : float.Parse(Get_Numeric(this.DAT02_GIHTTIME.GetValue().ToString())));
                         }
                         if (float.Parse(Get_Numeric(dgl.Rows[0]["GIOTTIME"].ToString())) > 0)
                         {
                             DAT02_GIOTTIME.SetValue(float.Parse(Get_Numeric(this.DAT02_GIOTTIME.GetValue().ToString())) != float.Parse(dgl.Rows[0]["GIOTTIME"].ToString()) ? float.Parse(dgl.Rows[0]["GIOTTIME"].ToString()) : float.Parse(Get_Numeric(this.DAT02_GIOTTIME.GetValue().ToString())));
                         }
                         if (float.Parse(Get_Numeric(dgl.Rows[0]["GINTTIME"].ToString())) > 0)
                         {
                             DAT02_GINTTIME.SetValue(float.Parse(Get_Numeric(this.DAT02_GINTTIME.GetValue().ToString())) != float.Parse(dgl.Rows[0]["GINTTIME"].ToString()) ? float.Parse(dgl.Rows[0]["GINTTIME"].ToString()) : float.Parse(Get_Numeric(this.DAT02_GINTTIME.GetValue().ToString())));
                         }
                         if (float.Parse(Get_Numeric(dgl.Rows[0]["GIHUTIME"].ToString())) > 0)
                         {
                             DAT02_GIHUTIME.SetValue(float.Parse(Get_Numeric(this.DAT02_GIHUTIME.GetValue().ToString())) != float.Parse(dgl.Rows[0]["GIHUTIME"].ToString()) ? float.Parse(dgl.Rows[0]["GIHUTIME"].ToString()) : float.Parse(Get_Numeric(this.DAT02_GIHUTIME.GetValue().ToString())));
                         }
                         if (float.Parse(Get_Numeric(dgl.Rows[0]["GIGOTIME"].ToString())) > 0)
                         {
                             DAT02_GIGOTIME.SetValue(float.Parse(Get_Numeric(this.DAT02_GIGOTIME.GetValue().ToString())) != float.Parse(dgl.Rows[0]["GIGOTIME"].ToString()) ? float.Parse(dgl.Rows[0]["GIGOTIME"].ToString()) : float.Parse(Get_Numeric(this.DAT02_GIGOTIME.GetValue().ToString())));
                         }
                         if (float.Parse(Get_Numeric(dgl.Rows[0]["GISATIME"].ToString())) > 0)
                         {
                             DAT02_GISATIME.SetValue(float.Parse(Get_Numeric(this.DAT02_GISATIME.GetValue().ToString())) != float.Parse(dgl.Rows[0]["GISATIME"].ToString()) ? float.Parse(dgl.Rows[0]["GISATIME"].ToString()) : float.Parse(Get_Numeric(this.DAT02_GISATIME.GetValue().ToString())));
                         }
                         if (float.Parse(Get_Numeric(dgl.Rows[0]["GIJITIME"].ToString())) > 0)
                         {
                             DAT02_GIJITIME.SetValue(float.Parse(Get_Numeric(this.DAT02_GIJITIME.GetValue().ToString())) != float.Parse(dgl.Rows[0]["GIJITIME"].ToString()) ? float.Parse(dgl.Rows[0]["GIJITIME"].ToString()) : float.Parse(Get_Numeric(this.DAT02_GIJITIME.GetValue().ToString())));
                         }
                         if (float.Parse(Get_Numeric(dgl.Rows[0]["GIJTTIME"].ToString())) > 0)
                         {
                             DAT02_GIJTTIME.SetValue(float.Parse(Get_Numeric(this.DAT02_GIJTTIME.GetValue().ToString())) != float.Parse(dgl.Rows[0]["GIJTTIME"].ToString()) ? float.Parse(dgl.Rows[0]["GIJTTIME"].ToString()) : float.Parse(Get_Numeric(this.DAT02_GIJTTIME.GetValue().ToString())));
                         }
                         if (float.Parse(Get_Numeric(dgl.Rows[0]["GIINTIME"].ToString())) > 0)
                         {
                             DAT02_GIINTIME.SetValue(float.Parse(Get_Numeric(this.DAT02_GIINTIME.GetValue().ToString())) != float.Parse(dgl.Rows[0]["GIINTIME"].ToString()) ? float.Parse(dgl.Rows[0]["GIINTIME"].ToString()) : float.Parse(Get_Numeric(this.DAT02_GIINTIME.GetValue().ToString())));
                         }
                     }                    

                     if (DAT02_GICARDGB.GetValue().ToString() != dgl.Rows[0]["GICARDGB"].ToString())
                     {
                         DAT02_GICARDGB.SetValue(dgl.Rows[0]["GICARDGB"].ToString());
                     }
                     if (DAT02_GIHUILGN.GetValue().ToString() != dgl.Rows[0]["GIHUILGN"].ToString())
                     {
                         DAT02_GIHUILGN.SetValue(dgl.Rows[0]["GIHUILGN"].ToString());
                     }

                    //수정
                    datas_GTILREF_Edit.Add(new object[] {
                     DAT02_GIYOIL.GetValue().ToString(),
                     DAT02_GIHUMUCD.GetValue().ToString(),
                     DAT02_GIHUGACD.GetValue().ToString(),
                     DAT02_GIYGCHUL.GetValue().ToString(),
                     DAT02_GICHLTIME.GetValue().ToString(),
                     DAT02_GIENDTIME.GetValue().ToString(),
                     DAT02_GIINCHLTM.GetValue().ToString(),
                     DAT02_GIINENDTM.GetValue().ToString(),
                     DAT02_GIYACHLTM.GetValue().ToString(),
                     DAT02_GIYAENDTM.GetValue().ToString(),
                     DAT02_GIYAINCHL.GetValue().ToString(),
                     DAT02_GIYAINEND.GetValue().ToString(),
                     DAT02_GIJOCHLST.GetValue().ToString(),
                     DAT02_GIJOCHLED.GetValue().ToString(),
                     DAT02_GIYUNJGST.GetValue().ToString(),
                     DAT02_GIYUNJGED.GetValue().ToString(),
                     DAT02_GIJOTIME.GetValue().ToString(),
                     DAT02_GIHTTIME.GetValue().ToString(),
                     DAT02_GIOTTIME.GetValue().ToString(),
                     DAT02_GINTTIME.GetValue().ToString(),
                     DAT02_GIHUTIME.GetValue().ToString(),
                     DAT02_GIGJTIME.GetValue().ToString(),
                     DAT02_GIGOTIME.GetValue().ToString(),
                     DAT02_GISATIME.GetValue().ToString(),
                     DAT02_GIJITIME.GetValue().ToString(),
                     DAT02_GIJTTIME.GetValue().ToString(),
                     DAT02_GIINTIME.GetValue().ToString(),
                     DAT02_GICARDGB.GetValue().ToString(),
                     DAT02_GIHUILGN.GetValue().ToString(),
                     DAT02_GIHISAB.GetValue().ToString(),
                     DAT02_GIDATE.GetValue().ToString(),
                     DAT02_GISABUN.GetValue().ToString()	
                    });
                }
                else
                {                 

                    //등록
                     datas_GTILREF.Add(new object[] {
                     DAT02_GIDATE.GetValue().ToString(),
                     DAT02_GISABUN.GetValue().ToString(),
                     DAT02_GIYOIL.GetValue().ToString(),
                     DAT02_GIHUMUCD.GetValue().ToString(),
                     DAT02_GIHUGACD.GetValue().ToString(),
                     DAT02_GIYGCHUL.GetValue().ToString(),
                     DAT02_GICHLTIME.GetValue().ToString(),
                     DAT02_GIENDTIME.GetValue().ToString(),
                     DAT02_GIINCHLTM.GetValue().ToString(),
                     DAT02_GIINENDTM.GetValue().ToString(),
                     DAT02_GIYACHLTM.GetValue().ToString(),
                     DAT02_GIYAENDTM.GetValue().ToString(),
                     DAT02_GIYAINCHL.GetValue().ToString(),
                     DAT02_GIYAINEND.GetValue().ToString(),
                     DAT02_GIJOCHLST.GetValue().ToString(),
                     DAT02_GIJOCHLED.GetValue().ToString(),
                     DAT02_GIYUNJGST.GetValue().ToString(),
                     DAT02_GIYUNJGED.GetValue().ToString(),
                     DAT02_GIJOTIME.GetValue().ToString(),
                     DAT02_GIHTTIME.GetValue().ToString(),
                     DAT02_GIOTTIME.GetValue().ToString(),
                     DAT02_GINTTIME.GetValue().ToString(),
                     DAT02_GIHUTIME.GetValue().ToString(),
                     DAT02_GIGJTIME.GetValue().ToString(),
                     DAT02_GIGOTIME.GetValue().ToString(),
                     DAT02_GISATIME.GetValue().ToString(),
                     DAT02_GIJITIME.GetValue().ToString(),
                     DAT02_GIJTTIME.GetValue().ToString(),
                     DAT02_GIINTIME.GetValue().ToString(),
                     DAT02_GICARDGB.GetValue().ToString(),
                     DAT02_GIHUILGN.GetValue().ToString(),
	                 DAT02_GIHISAB.GetValue().ToString()});	
                }
                
            }
        }
        #endregion

        #region  Description : 근태관련 DB 처리 함수
        private void UP_GoOutTime_DEL(string sGJDATE1, string sGJDATE2, string sGJSABUN)
        {
            //계산용
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51FEV158", sGJDATE1, sGJDATE2, sGJSABUN);
            this.DbConnector.ExecuteTranQuery();
            //관리용
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51FF9159", sGJDATE1, sGJDATE2, sGJSABUN);
            this.DbConnector.ExecuteTranQuery();
        }

        private void UP_WorkAllNight_ADD(string sGCDATE, string sGCSABUN, string sGCTIME)
        {
            //철야관리
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_HR_51FDJ156", sGCDATE, sGCSABUN, sGCTIME);
            //this.DbConnector.ExecuteTranQuery();
            if (this.DAT03_GDDATE.GetValue().ToString() != sGCDATE)
            {
                datas_GTCHREF.Add(new object[] { sGCDATE, sGCSABUN, sGCTIME });
            }
            this.DAT03_GDDATE.SetValue(sGCDATE);
        }

        private void UP_WorkAllNight_DEL(string sSGDDATE, string sEGDDATE, string sSABUN)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51FDG155", sSGDDATE, sEGDDATE, sSABUN);
            this.DbConnector.ExecuteTranQuery();
        }

        private void UP_WorkHourData_ADD(string sSGDDATE, string sEGDDATE, string sCHULIB, string sSABUN)
        {
            if (sCHULIB == "2") sCHULIB = "9";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51FB3153", sSGDDATE, sEGDDATE, sCHULIB, sSABUN);
            this.DbConnector.ExecuteTranQuery();
        }

        private void UP_WorkHourData_Del(string sGDDATE, string sSABUN)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51F9R152", sGDDATE, this.CBO01_KBGUNMU.GetValue().ToString(), sSABUN);
            this.DbConnector.ExecuteTranQuery();
        }

        private void UP_Set_GTILREF_Add()
        {
            
            this.DbConnector.CommandClear();
            if (datas_GTILREF.Count > 0)
            {                
                foreach (object[] data in datas_GTILREF)
                {
                    this.DbConnector.Attach("TY_P_HR_51JF8180", data);
                }                
            }
            if (datas_GTILREF_Edit.Count > 0)
            {                
                foreach (object[] data in datas_GTILREF_Edit)
                {
                    this.DbConnector.Attach("TY_P_HR_4BQJT567", data);
                }                
            }

            if (datas_GTOIREF.Count > 0)
            {
                foreach (object[] data in datas_GTOIREF)
                {
                    this.DbConnector.Attach("TY_P_HR_51FF0160", data);
                }
            }

            if (datas_GTOJREF.Count > 0)
            {
                foreach (object[] data in datas_GTOJREF)
                {
                    this.DbConnector.Attach("TY_P_HR_51FED157", data);
                }
            }

            if (datas_GTCHREF.Count > 0)
            {
                foreach (object[] data in datas_GTCHREF)
                {
                    this.DbConnector.Attach("TY_P_HR_51FDJ156", data);
                }
            }

            if (this.DbConnector.CommandCount > 0)
            {
                this.DbConnector.ExecuteTranQueryList();
            }

            datas_GTILREF.Clear();
            datas_GTOIREF.Clear();
            datas_GTOJREF.Clear();
            datas_GTCHREF.Clear();

        }
        #endregion

        #region  Description : 이전날, 다음 날짜 구하기
        private string UP_Calendar_NextDay(string sDate)
        {
            DateTime TmNextDay = new DateTime();
            TmNextDay = Convert.ToDateTime(sDate);
            TmNextDay = TmNextDay.AddDays(1);

            return TmNextDay.Year.ToString() + Set_Fill2(TmNextDay.Month.ToString()) + Set_Fill2(TmNextDay.Day.ToString());
        }

        private string UP_Calendar_PreDay(string sDate)
        {
            DateTime TmNextDay = new DateTime();
            TmNextDay = Convert.ToDateTime(sDate);
            TmNextDay = TmNextDay.AddDays(-1);

            return TmNextDay.Year.ToString() + Set_Fill2(TmNextDay.Month.ToString()) + Set_Fill2(TmNextDay.Day.ToString());
        }
        #endregion 

        #region  Description : 교대조, 연장근무신청서 Db 조회
        private void UP_Set_WkGroupOtDoc_Sel()
        {
            //교대조
            this.dt_WorkGroup = new DataTable();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51G9Q166", this.DTP01_STDATE.GetString().ToString(), this.DTP01_EDDATE.GetString().ToString(), this.CBH01_GISABUN.GetValue().ToString());
            dt_WorkGroup = this.DbConnector.ExecuteDataTable();
                        
            //연장근무
            this.dt_OtDoc = new DataTable();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_51GBJ170", this.DTP01_STDATE.GetString().ToString(), this.DTP01_EDDATE.GetString().ToString(), this.CBH01_GISABUN.GetValue().ToString(), "");
            dt_OtDoc = this.DbConnector.ExecuteDataTable();
        }
        #endregion      


        #region  Description :  일일근태관리관리 데이타 리턴
        private string UP_Get_GTILREFToReturn(DataTable dt, string sCharValue, string sNowValue)
        {
            string sReturnValue = string.Empty;

            if (dt.Rows.Count > 0)
            {
                switch (sCharValue)
                {
                    case "GICHLTIME":
                        if (sNowValue != dt.Rows[0]["GICHLTIME"].ToString().Trim() && Convert.ToInt16(dt.Rows[0]["GICHLTIME"].ToString().Trim()) > 0)
                        {
                            sReturnValue = Set_Fill4(dt.Rows[0]["GICHLTIME"].ToString().Trim());
                        }
                        else
                        {
                            sReturnValue = sNowValue;
                        }
                        break;
                    case "GIENDTIME":
                        if (sNowValue != dt.Rows[0]["GIENDTIME"].ToString().Trim() && Convert.ToInt16(dt.Rows[0]["GIENDTIME"].ToString().Trim()) > 0)
                        {
                            sReturnValue = Set_Fill4(dt.Rows[0]["GIENDTIME"].ToString().Trim());
                        }
                        else
                        {
                            sReturnValue = sNowValue;
                        }
                        break;
                    case "GIINCHLTM":
                        if (sNowValue != dt.Rows[0]["GIINCHLTM"].ToString().Trim() && Convert.ToInt16(dt.Rows[0]["GIINCHLTM"].ToString().Trim()) > 0)
                        {
                            sReturnValue = Set_Fill4(dt.Rows[0]["GIINCHLTM"].ToString().Trim());
                        }
                        else
                        {
                            sReturnValue = sNowValue;
                        }
                        break;
                    case "GIINENDTM":
                        if (sNowValue != dt.Rows[0]["GIINENDTM"].ToString().Trim() && Convert.ToInt16(dt.Rows[0]["GIINENDTM"].ToString().Trim()) > 0)
                        {
                            sReturnValue = Set_Fill4(dt.Rows[0]["GIINENDTM"].ToString().Trim());
                        }
                        else
                        {
                            sReturnValue = sNowValue;
                        }
                        break;
                    case "GIYACHLTM":
                        if (sNowValue != dt.Rows[0]["GIYACHLTM"].ToString().Trim() && Convert.ToInt16(dt.Rows[0]["GIYACHLTM"].ToString().Trim()) > 0)
                        {
                            sReturnValue = Set_Fill4(dt.Rows[0]["GIYACHLTM"].ToString().Trim());
                        }
                        else
                        {
                            sReturnValue = sNowValue;
                        }
                        break;
                    case "GIYAENDTM":
                        if (sNowValue != dt.Rows[0]["GIYAENDTM"].ToString().Trim() && Convert.ToInt16(dt.Rows[0]["GIYAENDTM"].ToString().Trim()) > 0)
                        {
                            sReturnValue = Set_Fill4(dt.Rows[0]["GIYAENDTM"].ToString().Trim());
                        }
                        else
                        {
                            sReturnValue = sNowValue;
                        }
                        break;
                    case "GIYAINCHL":
                        if (sNowValue != dt.Rows[0]["GIYAINCHL"].ToString().Trim() && Convert.ToInt16(dt.Rows[0]["GIYAINCHL"].ToString().Trim()) > 0)
                        {
                            sReturnValue = Set_Fill4(dt.Rows[0]["GIYAINCHL"].ToString().Trim());
                        }
                        else
                        {
                            sReturnValue = sNowValue;
                        }
                        break;
                    case "GIYAINEND":
                        if (sNowValue != dt.Rows[0]["GIYAINEND"].ToString().Trim() && Convert.ToInt16(dt.Rows[0]["GIYAINEND"].ToString().Trim()) > 0)
                        {
                            sReturnValue = Set_Fill4(dt.Rows[0]["GIYAINEND"].ToString().Trim());
                        }
                        else
                        {
                            sReturnValue = sNowValue;
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                sReturnValue = sNowValue;
            }

            return sReturnValue;
        }
        #endregion
       
    }
}
