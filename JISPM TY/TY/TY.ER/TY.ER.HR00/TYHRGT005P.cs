using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;


namespace TY.ER.HR00
{
    /// <summary>
    /// 일일근태관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.11.26 19:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BQJP565 : 일일근태관리 조회
    ///  TY_P_HR_4BQJS566 : 일일근태관리 등록
    ///  TY_P_HR_4BQJT567 : 일일근태관리 수정
    ///  TY_P_HR_4BQJU568 : 일일근태관리 삭제
    ///  TY_P_HR_4BQJV569 : 일일근태관리 확인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4BQJV571 : 일일근태관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  SAV : 저장
    ///  GIHUMUCD : 회사휴무
    ///  GISABUN : 사원번호
    ///  GICHLTIME : 주간출근시간
    ///  GIDATE : 연장일자
    ///  GIENDTIME : 주간퇴근시간
    ///  GIGOTIME : 공적외출
    ///  GIHUTIME : 특근인정시간
    ///  GIINCHLTM : 주간출인시간
    ///  GIINENDTM : 주간퇴인시간
    ///  GIINTIME : 인정시간
    ///  GIJITIME : 지각시간
    ///  GIJOCHLED : 조출시간END
    ///  GIJOCHLST : 조출시간STD
    ///  GIJOTIME : 조출인정시간
    ///  GIJTTIME : 조퇴시간
    ///  GINTTIME : 철야인정시간
    ///  GIOTTIME : 연장인정시간
    ///  GISATIME : 사적외출
    ///  GIYACHLTM : 야간출근시간
    ///  GIYAENDTM : 야간퇴근시간
    ///  GIYAINCHL : 야간출인시간
    ///  GIYAINEND : 야간퇴인시간
    ///  GIYOIL : 요일코드
    ///  GIYUNJGED : 연장시간END
    ///  GIYUNJGST : 연장시간STD
    /// </summary>
    public partial class TYHRGT005P : TYBase
    {
        private string fsGIDATE;
        private string fsGISABUN;

        #region  Description : 폼 로드 이벤트
        public TYHRGT005P(string sGIDATE, string sGISABUN)
        {
            InitializeComponent();

            fsGIDATE = sGIDATE;
            fsGISABUN = sGISABUN;
        }

        private void TYHRGT005P_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
           
            this.UP_FieldClear();

            UP_Run(fsGIDATE, fsGISABUN);

            this.UP_FieldLock(true);

            this.SetStartingFocus(this.MTB02_GICHLTIME);

            this.UP_ButtonCheck(true, true);
                        
        }
        #endregion     
      

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BQJU568", this.DTP02_GIDATE.GetString(), this.CBH02_GISABUN.GetValue() );
            this.DbConnector.ExecuteTranQuery();

            this.UP_FieldClear();

            this.UP_ButtonCheck(false, false);

            this.ShowMessage("TY_M_GB_23NAD874");
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sDate = string.Empty;

            if (int.Parse(this.DTP02_GIDATE.GetString().ToString().Substring(6, 2)) >= 21)
            {
                sDate = this.DTP02_GIDATE.GetString().ToString().Substring(0, 4) + "-" +
                        this.DTP02_GIDATE.GetString().ToString().Substring(4, 2) + "-" +
                        this.DTP02_GIDATE.GetString().ToString().Substring(6, 2);
                sDate = Convert.ToDateTime(sDate).AddMonths(1).Year.ToString() +
                        Set_Fill2(Convert.ToDateTime(sDate).AddMonths(1).Month.ToString());
            }
            else
            {
                sDate = this.DTP02_GIDATE.GetString().ToString().Substring(0, 6);                        
            }

            //급여이체자료 존재시 삭제 할수 없다.
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53IEH701", sDate, sDate, "M1");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt16(dt.Rows[0]["PAYCNT"].ToString()) > 0)
                {
                    this.ShowCustomMessage("급여이체자료가 존재합니다. 삭제할수 없습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BQJV569", this.DTP02_GIDATE.GetString(), this.CBH02_GISABUN.GetValue());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            this.DbConnector.CommandClear();
            if (dt.Rows.Count > 0)
            {
                this.DbConnector.Attach("TY_P_HR_4BQJT567", this.TXT02_GIYOIL.GetValue(),
                                                            this.CBH02_GIHUMUCD.GetValue(),
                                                            this.CBH02_GIHUGACD.GetValue(),
                                                            "",
                                                            this.MTB02_GICHLTIME.GetValue().ToString().Replace(":", "").Trim(),
                                                            this.MTB02_GIENDTIME.GetValue().ToString().Replace(":", "").Trim(),
                                                            this.MTB02_GIINCHLTM.GetValue().ToString().Replace(":", "").Trim(),
                                                            this.MTB02_GIINENDTM.GetValue().ToString().Replace(":", "").Trim(),
                                                            this.MTB02_GIYACHLTM.GetValue().ToString().Replace(":", "").Trim(),
                                                            this.MTB02_GIYAENDTM.GetValue().ToString().Replace(":", "").Trim(),
                                                            this.MTB02_GIYAINCHL.GetValue().ToString().Replace(":", "").Trim(),
                                                            this.MTB02_GIYAINEND.GetValue().ToString().Replace(":", "").Trim(),
                                                            this.MTB02_GIJOCHLST.GetValue().ToString().Replace(":", "").Trim(),
                                                            this.MTB02_GIJOCHLED.GetValue().ToString().Replace(":", "").Trim(),
                                                            this.MTB02_GIYUNJGST.GetValue().ToString().Replace(":", "").Trim(),
                                                            this.MTB02_GIYUNJGED.GetValue().ToString().Replace(":", "").Trim(),
                                                            this.TXT02_GIJOTIME.GetValue(),
                                                            this.TXT02_GIHTTIME.GetValue(),
                                                            this.TXT02_GIOTTIME.GetValue(),
                                                            this.TXT02_GINTTIME.GetValue(),
                                                            this.TXT02_GIHUTIME.GetValue(),
                                                            this.TXT02_GINUTIME.GetValue(),
                                                            this.TXT02_GIGJTIME.GetValue(),
                                                            this.TXT02_GIGOTIME.GetValue(),
                                                            this.TXT02_GISATIME.GetValue(),
                                                            this.TXT02_GIJITIME.GetValue(),
                                                            this.TXT02_GIJTTIME.GetValue(),
                                                            this.TXT02_GIINTIME.GetValue(),
                                                            this.CBO02_GICARDGB.GetValue(),
                                                            this.TXT02_GIBIGO.GetValue(),
                                                            this.CBO02_GIHUILGN.GetValue(),
                                                            this.CBO02_GIWKCHECK.GetValue(),
                                                            this.TXT02_GIWKTIME.GetValue(),
                                                            TYUserInfo.EmpNo,
                                                            this.DTP02_GIDATE.GetString(),
                                                            this.CBH02_GISABUN.GetValue()
                                                            );

            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_4BQJS566", this.DTP02_GIDATE.GetString(), 
                                                            this.CBH02_GISABUN.GetValue(),
                                                            this.TXT02_GIYOIL.GetValue(),
                                                            this.CBH02_GIHUMUCD.GetValue(),
                                                            this.CBH02_GIHUGACD.GetValue(),
                                                            "",
                                                            this.MTB02_GICHLTIME.GetValue().ToString().Replace(":","").Trim(),
                                                            this.MTB02_GIENDTIME.GetValue().ToString().Replace(":","").Trim(),
                                                            this.MTB02_GIINCHLTM.GetValue().ToString().Replace(":","").Trim(),
                                                            this.MTB02_GIINENDTM.GetValue().ToString().Replace(":","").Trim(),
                                                            this.MTB02_GIYACHLTM.GetValue().ToString().Replace(":","").Trim(),
                                                            this.MTB02_GIYAENDTM.GetValue().ToString().Replace(":","").Trim(),
                                                            this.MTB02_GIYAINCHL.GetValue().ToString().Replace(":","").Trim(),
                                                            this.MTB02_GIYAINEND.GetValue().ToString().Replace(":","").Trim(),
                                                            this.MTB02_GIJOCHLST.GetValue().ToString().Replace(":","").Trim(),
                                                            this.MTB02_GIJOCHLED.GetValue().ToString().Replace(":","").Trim(),
                                                            this.MTB02_GIYUNJGST.GetValue().ToString().Replace(":","").Trim(),
                                                            this.MTB02_GIYUNJGED.GetValue().ToString().Replace(":","").Trim(),
                                                            this.TXT02_GIJOTIME.GetValue(),
                                                            this.TXT02_GIHTTIME.GetValue(),
                                                            this.TXT02_GIOTTIME.GetValue(),
                                                            this.TXT02_GINTTIME.GetValue(),
                                                            this.TXT02_GIHUTIME.GetValue(),
                                                            this.TXT02_GINUTIME.GetValue(),
                                                            this.TXT02_GIGJTIME.GetValue(),
                                                            this.TXT02_GIGOTIME.GetValue(),
                                                            this.TXT02_GISATIME.GetValue(),
                                                            this.TXT02_GIJITIME.GetValue(),
                                                            this.TXT02_GIJTTIME.GetValue(),
                                                            this.TXT02_GIINTIME.GetValue(),                                                            
                                                            "",
                                                            this.TXT02_GIBIGO.GetValue(),
                                                            this.CBO02_GIHUILGN.GetValue(),
                                                            this.CBO02_GIWKCHECK.GetValue(),
                                                            this.TXT02_GIWKTIME.GetValue(),
                                                            TYUserInfo.EmpNo
                                                            );
            }
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");

            this.UP_ButtonCheck(false, false);
        }

        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sDate = string.Empty;

            if (int.Parse(this.DTP02_GIDATE.GetString().ToString().Substring(6, 2)) >= 21)
            {
                sDate = this.DTP02_GIDATE.GetString().ToString().Substring(0, 4) + "-" +
                        this.DTP02_GIDATE.GetString().ToString().Substring(4, 2) + "-" +
                        this.DTP02_GIDATE.GetString().ToString().Substring(6, 2);
                sDate = Convert.ToDateTime(sDate).AddMonths(1).Year.ToString() +
                        Set_Fill2(Convert.ToDateTime(sDate).AddMonths(1).Month.ToString());
            }
            else
            {
                sDate = this.DTP02_GIDATE.GetString().ToString().Substring(0, 6);
            }

            //급여이체자료 존재시 삭제 할수 없다.
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_HR_53IEH701", sDate, sDate, "M1");
            //DataTable dt = this.DbConnector.ExecuteDataTable();
            //if (dt.Rows.Count > 0)
            //{
            //    if (Convert.ToInt16(dt.Rows[0]["PAYCNT"].ToString()) > 0)
            //    {
            //        this.ShowCustomMessage("급여이체자료가 존재합니다. 수정할수 없습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //        e.Successed = false;
            //        return;
            //    }
            //}
            
            //시간 포맷 체크   
            if (this.MTB02_GICHLTIME.GetValue().ToString().Replace(":", "").Trim().Length < 4 && this.MTB02_GICHLTIME.GetValue().ToString().Replace(":", "").Trim().Length > 0)
            {
                this.ShowCustomMessage("출근시간을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (this.MTB02_GIINCHLTM.GetValue().ToString().Replace(":", "").Trim().Length < 4 && this.MTB02_GIINCHLTM.GetValue().ToString().Replace(":", "").Trim().Length > 0)
            {
                this.ShowCustomMessage("출근인정시간을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (this.MTB02_GIENDTIME.GetValue().ToString().Replace(":", "").Trim().Length < 4 && this.MTB02_GIENDTIME.GetValue().ToString().Replace(":", "").Trim().Length > 0)
            {
                this.ShowCustomMessage("퇴근시간을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (this.MTB02_GIINENDTM.GetValue().ToString().Replace(":", "").Trim().Length < 4 && this.MTB02_GIINENDTM.GetValue().ToString().Replace(":", "").Trim().Length > 0)
            {
                this.ShowCustomMessage("퇴근인정시간을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (this.MTB02_GIYACHLTM.GetValue().ToString().Replace(":", "").Trim().Length < 4 && this.MTB02_GIYACHLTM.GetValue().ToString().Replace(":", "").Trim().Length > 0)
            {
                this.ShowCustomMessage("야근출근시간을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (this.MTB02_GIYAINCHL.GetValue().ToString().Replace(":", "").Trim().Length < 4 && this.MTB02_GIYAINCHL.GetValue().ToString().Replace(":", "").Trim().Length > 0)
            {
                this.ShowCustomMessage("야근출인시간을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (this.MTB02_GIYAENDTM.GetValue().ToString().Replace(":", "").Trim().Length < 4 && this.MTB02_GIYAENDTM.GetValue().ToString().Replace(":", "").Trim().Length > 0)
            {
                this.ShowCustomMessage("야근퇴근시간을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (this.MTB02_GIYAINEND.GetValue().ToString().Replace(":", "").Trim().Length < 4 && this.MTB02_GIYAINEND.GetValue().ToString().Replace(":", "").Trim().Length > 0)
            {
                this.ShowCustomMessage("야근퇴인시간을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (this.MTB02_GIJOCHLST.GetValue().ToString().Replace(":", "").Trim().Length < 4 && this.MTB02_GIJOCHLST.GetValue().ToString().Replace(":", "").Trim().Length > 0)
            {
                this.ShowCustomMessage("조출시간을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
            if (this.MTB02_GIJOCHLED.GetValue().ToString().Replace(":", "").Trim().Length < 4 && this.MTB02_GIJOCHLED.GetValue().ToString().Replace(":", "").Trim().Length > 0)
            {
                this.ShowCustomMessage("조출시간을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (this.MTB02_GIYUNJGST.GetValue().ToString().Replace(":", "").Trim().Length < 4 && this.MTB02_GIYUNJGST.GetValue().ToString().Replace(":", "").Trim().Length > 0)
            {
                this.ShowCustomMessage("연장시간을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
            if (this.MTB02_GIYUNJGED.GetValue().ToString().Replace(":", "").Trim().Length < 4 && this.MTB02_GIYUNJGED.GetValue().ToString().Replace(":", "").Trim().Length > 0)
            {
                this.ShowCustomMessage("연장시간을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }


            this.TXT02_GIJOTIME.SetValue(String.Format("{0:#0.0}", Convert.ToDecimal(Get_Numeric(this.TXT02_GIJOTIME.GetValue().ToString()))));
            this.TXT02_GIHTTIME.SetValue(String.Format("{0:#0.0}", Convert.ToDecimal(Get_Numeric(this.TXT02_GIHTTIME.GetValue().ToString()))));
            this.TXT02_GIOTTIME.SetValue(String.Format("{0:#0.0}", Convert.ToDecimal(Get_Numeric(this.TXT02_GIOTTIME.GetValue().ToString()))));
            this.TXT02_GINTTIME.SetValue(String.Format("{0:#0.0}", Convert.ToDecimal(Get_Numeric(this.TXT02_GINTTIME.GetValue().ToString()))));
            this.TXT02_GIHUTIME.SetValue(String.Format("{0:#0.0}", Convert.ToDecimal(Get_Numeric(this.TXT02_GIHUTIME.GetValue().ToString()))));
            this.TXT02_GINUTIME.SetValue(String.Format("{0:#0.0}", Convert.ToDecimal(Get_Numeric(this.TXT02_GINUTIME.GetValue().ToString()))));
            this.TXT02_GIINTIME.SetValue(String.Format("{0:#0.0}", Convert.ToDecimal(Get_Numeric(this.TXT02_GIINTIME.GetValue().ToString()))));

            this.TXT02_GIGOTIME.SetValue(String.Format("{0:#0.0}", Convert.ToDecimal(Get_Numeric(this.TXT02_GIGOTIME.GetValue().ToString()))));
            this.TXT02_GISATIME.SetValue(String.Format("{0:#0.0}", Convert.ToDecimal(Get_Numeric(this.TXT02_GISATIME.GetValue().ToString()))));
            this.TXT02_GIJITIME.SetValue(String.Format("{0:#0.0}", Convert.ToDecimal(Get_Numeric(this.TXT02_GIJITIME.GetValue().ToString()))));
            this.TXT02_GIJTTIME.SetValue(String.Format("{0:#0.0}", Convert.ToDecimal(Get_Numeric(this.TXT02_GIJTTIME.GetValue().ToString()))));            

            
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion     

        #region  Description : 확인 이벤트
        private void UP_Run(string sDATE, string sSABUN)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BQJV569", sDATE, sSABUN);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "02");
            }
            
            //개인휴무 
            this.FPS91_TY_S_HR_4BQKT573.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4C1HI621", sDATE, sSABUN);
            this.FPS91_TY_S_HR_4BQKT573.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 필드 클리어 이벤트
        private void UP_FieldClear()
        {
            this.Initialize_Controls("02");
            this.DTP02_GIDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.TXT02_GIJOTIME.SetValue("0.0");
            this.TXT02_GIHTTIME.SetValue("0.0");
            this.TXT02_GIOTTIME.SetValue("0.0");
            this.TXT02_GINTTIME.SetValue("0.0");
            this.TXT02_GIHUTIME.SetValue("0.0");
            this.TXT02_GINUTIME.SetValue("0.0");
            this.TXT02_GIINTIME.SetValue("0.00");
            this.TXT02_GIGOTIME.SetValue("0.0");
            this.TXT02_GISATIME.SetValue("0.0");
            this.TXT02_GIJITIME.SetValue("0.0");
            this.TXT02_GIJTTIME.SetValue("0.0");
            this.CBO02_GIHUILGN.SetValue("N");
            this.CBO02_GIWKCHECK.SetValue("N");
            this.TXT02_GIWKTIME.SetValue("0.0");

            this.FPS91_TY_S_HR_4BQKT573.Initialize();
        }
        #endregion

        #region  Description : 버튼 처리 이벤트
        private void UP_ButtonCheck(bool bvalueSav, bool bvalueRem)
        {
            this.BTN61_SAV.Visible = bvalueSav;
            this.BTN61_REM.Visible = bvalueRem;
        }
        #endregion

        #region  Description : 필드 lock 처리 이벤트
        private void UP_FieldLock(bool bvalue)
        {
            this.DTP02_GIDATE.SetReadOnly(bvalue);
            this.CBH02_GISABUN.SetReadOnly(bvalue);
        }
        #endregion

        #region  Description : 날짜 처리 이벤트
        private void DTP02_GIDATE_ValueChanged(object sender, EventArgs e)
        {
            this.UP_Day_Yoil();
        }

        private void DTP02_GIDATE_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            this.UP_Day_Yoil();
        }

        private void DTP02_GIDATE_Leave(object sender, EventArgs e)
        {
            this.UP_Day_Yoil();
        }

        private void UP_Day_Yoil()
        {
            DateTime dtime = Convert.ToDateTime(this.DTP02_GIDATE.GetYearString() + "-" + this.DTP02_GIDATE.GetMonthString() + "-" + this.DTP02_GIDATE.GetDayString());

            int iDayYoild = Convert.ToInt16(dtime.DayOfWeek.ToString("d")) + 1;

            string sDay = "";

            switch (dtime.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    sDay = "월요일";
                    break;
                case DayOfWeek.Tuesday:
                    sDay = "화요일";
                    break;
                case DayOfWeek.Wednesday:
                    sDay = "수요일";
                    break;
                case DayOfWeek.Thursday:
                    sDay = "목요일";
                    break;
                case DayOfWeek.Friday:
                    sDay = "금요일";
                    break;
                case DayOfWeek.Saturday:
                    sDay = "토요일";
                    break;
                case DayOfWeek.Sunday:
                    sDay = "일요일";
                    break;
            }

            this.TXT02_GIYOIL.SetValue(iDayYoild.ToString());  
            this.TXT02_GIYOILNM.SetValue(sDay);
        }
        #endregion       

        #region  Description : 포커스 처리 이벤트
        private void MTB02_GICHLTIME_Enter(object sender, EventArgs e)
        {
            UP_FocusControl(MTB02_GICHLTIME);
        }

        private void MTB02_GIENDTIME_Enter(object sender, EventArgs e)
        {
            UP_FocusControl(MTB02_GIENDTIME);
        }

        private void MTB02_GIYACHLTM_Enter(object sender, EventArgs e)
        {
            UP_FocusControl(MTB02_GIYACHLTM);
        }

        private void MTB02_GIYAENDTM_Enter(object sender, EventArgs e)
        {
            UP_FocusControl(MTB02_GIYAENDTM);
        }

        private void MTB02_GIINCHLTM_Enter(object sender, EventArgs e)
        {
            UP_FocusControl(MTB02_GIINCHLTM);
        }

        private void MTB02_GIINENDTM_Enter(object sender, EventArgs e)
        {
            UP_FocusControl(MTB02_GIINENDTM);
        }

        private void MTB02_GIYAINCHL_Enter(object sender, EventArgs e)
        {
            UP_FocusControl(MTB02_GIYAINCHL);
        }

        private void MTB02_GIYAINEND_Enter(object sender, EventArgs e)
        {
            UP_FocusControl(MTB02_GIYAINEND);
        }

        private void MTB02_GIJOCHLST_Enter(object sender, EventArgs e)
        {
            UP_FocusControl(MTB02_GIJOCHLST);
        }

        private void MTB02_GIJOCHLED_Enter(object sender, EventArgs e)
        {
            UP_FocusControl(MTB02_GIJOCHLED);
        }

        private void MTB02_GIYUNJGST_Enter(object sender, EventArgs e)
        {
            UP_FocusControl(MTB02_GIYUNJGST);
        }

        private void MTB02_GIYUNJGED_Enter(object sender, EventArgs e)
        {
            UP_FocusControl(MTB02_GIYUNJGED);
        }

        private void UP_FocusControl(TYMaskedTextBox MTBText )
        {
            MTBText.SelectAll();
        }
        #endregion

        #region  Description : 인정시간 처리 이벤트
        private void TXT02_GIJOTIME_Leave(object sender, EventArgs e)
        {
            //조출
            UP_Set_OverTimeCompute();
        }        

        private void TXT02_GIHTTIME_Leave(object sender, EventArgs e)
        {
            //심야
            UP_Set_OverTimeCompute();
        }

        private void TXT02_GIOTTIME_Leave(object sender, EventArgs e)
        {
            //연장
            UP_Set_OverTimeCompute();
        }

        private void TXT02_GINTTIME_Leave(object sender, EventArgs e)
        {
            //철야
            UP_Set_OverTimeCompute();
        }

        private void TXT02_GIHUTIME_Leave(object sender, EventArgs e)
        {
            //특근
            UP_Set_OverTimeCompute();
        }

        private void TXT02_GINUTIME_Leave(object sender, EventArgs e)
        {
            //심야특근
            UP_Set_OverTimeCompute();
        }

        private void TXT02_GIGJTIME_Leave(object sender, EventArgs e)
        {
            //교대인정
            UP_Set_OverTimeCompute();
        }

        private void UP_Set_OverTimeCompute()
        {
            double dGIJOTIME = 0;
            double dGIHTTIME = 0;
            double dGIOTTIME = 0;
            double dGINTTIME = 0;
            double dGIHUTIME = 0;
            double dGINUTIME = 0;
            double dGIGJTIME = 0;

            double dGIINTIME = 0;

            dGIJOTIME = Convert.ToDouble(string.Format("{0:#0.00}", Convert.ToDouble(TXT02_GIJOTIME.GetValue().ToString()))) * 1.5;
            dGIHTTIME = Convert.ToDouble(string.Format("{0:#0.00}", Convert.ToDouble(TXT02_GIHTTIME.GetValue().ToString()))) * 0.5;
            dGIOTTIME = Convert.ToDouble(string.Format("{0:#0.00}", Convert.ToDouble(TXT02_GIOTTIME.GetValue().ToString()))) * 1.5;
            dGINTTIME = Convert.ToDouble(string.Format("{0:#0.00}", Convert.ToDouble(TXT02_GINTTIME.GetValue().ToString()))) * 2;
            dGIHUTIME = Convert.ToDouble(string.Format("{0:#0.00}", Convert.ToDouble(TXT02_GIHUTIME.GetValue().ToString()))) * 2.5;
            dGINUTIME = Convert.ToDouble(string.Format("{0:#0.00}", Convert.ToDouble(TXT02_GINUTIME.GetValue().ToString()))) * 3;
            dGIGJTIME = Convert.ToDouble(string.Format("{0:#0.00}", Convert.ToDouble(TXT02_GIGJTIME.GetValue().ToString())));

            dGIINTIME = Convert.ToDouble(string.Format("{0:#0.00}", (dGIJOTIME + dGIHTTIME + dGIOTTIME + dGINTTIME + dGIHUTIME + dGINUTIME + dGIGJTIME)));  

            TXT02_GIINTIME.SetValue(dGIINTIME.ToString());
        }
        #endregion

        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
      


    }
}
