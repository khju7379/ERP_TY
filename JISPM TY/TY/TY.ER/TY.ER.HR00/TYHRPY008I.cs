using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Drawing;

namespace TY.ER.HR00
{
    /// <summary>
    /// 개인 급여관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.12.24 13:39
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4CNJ2950 : 개인 급여 통합 조회(급여결과마스타)
    ///  TY_P_HR_4CODU958 : 급여결과내역관리 조회
    ///  TY_P_HR_4CODW959 : 급여결과내역관리 등록
    ///  TY_P_HR_4CODY960 : 급여결과내역관리 수정
    ///  TY_P_HR_4CODZ961 : 급여결과내역관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CODZ962 : 급여결과내역관리
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
    ///  CLO : 닫기
    ///  REM : 삭제
    ///  SAV : 저장
    ///  KBJKCD : 직급
    ///  KBSABUN : 사번
    ///  PAYGUBN : 급여구분
    ///  PTBUSEO : 부서
    ///  PTSAUP : 사업부
    ///  EDDATE : 종료일자
    ///  PAYJIDATE : 지급일자
    ///  PAYYYMM : 급여년월
    ///  STDATE : 시작일자
    ///  KBHOBN : 호봉
    ///  PTPAYRATE : 지급율
    /// </summary>
    public partial class TYHRPY008I : TYBase
    {
        string fsPMGUBN = string.Empty;
        string fsPMYYMM = string.Empty;
        string fsPMJIDATE = string.Empty;
        string fsPMSABUN = string.Empty;
        string fsCallGubn = string.Empty;

        string fsPYBUNUSCODE = string.Empty;

        #region Description : 페이지 로드
        public TYHRPY008I(string sPMGUBN, string sPMYYMM, string sPMJIDATE, string sPMSABUN, string sCallGubn)
        {
            InitializeComponent();

            fsPMGUBN = sPMGUBN;
            fsPMYYMM = sPMYYMM;
            fsPMJIDATE = sPMJIDATE;
            fsPMSABUN = sPMSABUN;
            fsCallGubn = sCallGubn;
            CBH01_PTTEAM.DummyValue = sPMJIDATE;
            CBH01_PTBUSEO.DummyValue = sPMJIDATE;
            //개인급여기준관리
            //this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_4CODZ962, "PSPAYCODE", "PSPAYCODENM", "PSPAYCODE");
        }

        private void TYHRPY008I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_4CODZ962, "JIGUBN", "PSPAYCODE", "PSPAYCODENM");


            //정기상여금 급여코드 받아오기            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5C2D0257", "PY_FIXBONUSCODE", DTP01_PAYJIDATE.GetString().ToString());
            DataTable dtbus = this.DbConnector.ExecuteDataTable();
            if (dtbus.Rows.Count > 0)
            {
                fsPYBUNUSCODE = dtbus.Rows[0]["USRCDVALUE"].ToString();
            }

            this.DTP01_PMEXSDATE.SetValue("");
            this.DTP01_PMEXEDATE.SetValue("");

            UP_FieldLock(true);

            UP_Master_DataBinding();

            UP_Detail_DataBinding();

            UP_BankDataCheck();
        }
        #endregion      

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //급여 내역 삭제

                this.DbConnector.Attach("TY_P_HR_4CODZ961", ds.Tables[0].Rows[i]["PSGUBN"].ToString(),
                                                            ds.Tables[0].Rows[i]["PSYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["PSJIDATE"].ToString(),
                                                            ds.Tables[0].Rows[i]["PSSABUN"].ToString(),
                                                            ds.Tables[0].Rows[i]["PSPAYCODE"].ToString());
                

                //#region Description : 급여 마스타 update(삭제)
                ////급여 마스타 조회
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_HR_4CNJ2950", fsPMGUBN, fsPMYYMM, fsPMJIDATE, fsPMSABUN, "", "");
                //DataTable dt2 = this.DbConnector.ExecuteDataTable();

                ////소득세
                //if (dt.Rows[i]["PSPAYCODE"].ToString() == "2100") 
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_HR_53BG7650", dt2.Rows[0]["PMPAYTOTAL"].ToString(),   //지급합계
                //                                                double.Parse(dt2.Rows[0]["PMTAXTOTAL"].ToString()) - double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()),   //공제합계
                //                                                double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) + double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
                //                                                double.Parse(dt2.Rows[0]["PMINCOMETAX"].ToString()) - double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()),  //소득세
                //                                                dt2.Rows[0]["PMRESTAX"].ToString(),     //주민세
                //                                                dt2.Rows[0]["PMNATIONAMT"].ToString(),  //국민연금
                //                                                dt2.Rows[0]["PMHEALTHAMT"].ToString(),  //건강보험료
                //                                                dt2.Rows[0]["PMEMPLOYAMT"].ToString(),  //고용보험
                //                                                dt2.Rows[0]["PMLTERMAMT"].ToString(),   //장기기요양보험
                //                                                TYUserInfo.EmpNo,
                //                                                fsPMGUBN,
                //                                                fsPMYYMM,
                //                                                fsPMJIDATE,
                //                                                fsPMSABUN);
                //    this.DbConnector.ExecuteTranQueryList();
                //}
                ////주민세
                //else if (dt.Rows[i]["PSPAYCODE"].ToString() == "2110")
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_HR_53BG7650", dt2.Rows[0]["PMPAYTOTAL"].ToString(),   //지급합계
                //                                                double.Parse(dt2.Rows[0]["PMTAXTOTAL"].ToString()) - double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()),   //공제합계
                //                                                double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) + double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
                //                                                dt2.Rows[0]["PMINCOMETAX"].ToString(),  //소득세
                //                                                double.Parse(dt2.Rows[0]["PMRESTAX"].ToString()) - double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()),     //주민세
                //                                                dt2.Rows[0]["PMNATIONAMT"].ToString(),  //국민연금
                //                                                dt2.Rows[0]["PMHEALTHAMT"].ToString(),  //건강보험료
                //                                                dt2.Rows[0]["PMEMPLOYAMT"].ToString(),  //고용보험
                //                                                dt2.Rows[0]["PMLTERMAMT"].ToString(),   //장기기요양보험
                //                                                TYUserInfo.EmpNo,
                //                                                fsPMGUBN,
                //                                                fsPMYYMM,
                //                                                fsPMJIDATE,
                //                                                fsPMSABUN);
                //    this.DbConnector.ExecuteTranQueryList();
                //}
                ////국민연금
                //else if (dt.Rows[i]["PSPAYCODE"].ToString() == "2500")
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_HR_53BG7650", dt2.Rows[0]["PMPAYTOTAL"].ToString(),   //지급합계
                //                                                double.Parse(dt2.Rows[0]["PMTAXTOTAL"].ToString()) - double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()),   //공제합계
                //                                                double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) + double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
                //                                                dt2.Rows[0]["PMINCOMETAX"].ToString(),  //소득세
                //                                                dt2.Rows[0]["PMRESTAX"].ToString(),     //주민세
                //                                                double.Parse(dt2.Rows[0]["PMNATIONAMT"].ToString()) - double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()),  //국민연금
                //                                                dt2.Rows[0]["PMHEALTHAMT"].ToString(),  //건강보험료
                //                                                dt2.Rows[0]["PMEMPLOYAMT"].ToString(),  //고용보험
                //                                                dt2.Rows[0]["PMLTERMAMT"].ToString(),   //장기기요양보험
                //                                                TYUserInfo.EmpNo,
                //                                                fsPMGUBN,
                //                                                fsPMYYMM,
                //                                                fsPMJIDATE,
                //                                                fsPMSABUN);
                //    this.DbConnector.ExecuteTranQueryList();
                //}
                ////건강보험료
                //else if (dt.Rows[i]["PSPAYCODE"].ToString() == "2301")
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_HR_53BG7650", dt2.Rows[0]["PMPAYTOTAL"].ToString(),   //지급합계
                //                                                double.Parse(dt2.Rows[0]["PMTAXTOTAL"].ToString()) - double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()),   //공제합계
                //                                                double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) + double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
                //                                                dt2.Rows[0]["PMINCOMETAX"].ToString(),  //소득세
                //                                                dt2.Rows[0]["PMRESTAX"].ToString(),     //주민세
                //                                                dt2.Rows[0]["PMNATIONAMT"].ToString(),  //국민연금
                //                                                double.Parse(dt2.Rows[0]["PMHEALTHAMT"].ToString()) - double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()),  //건강보험료
                //                                                dt2.Rows[0]["PMEMPLOYAMT"].ToString(),  //고용보험
                //                                                dt2.Rows[0]["PMLTERMAMT"].ToString(),   //장기기요양보험
                //                                                TYUserInfo.EmpNo,
                //                                                fsPMGUBN,
                //                                                fsPMYYMM,
                //                                                fsPMJIDATE,
                //                                                fsPMSABUN);
                //    this.DbConnector.ExecuteTranQueryList();
                //}
                ////고용보험
                //else if (dt.Rows[i]["PSPAYCODE"].ToString() == "2401")
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_HR_53BG7650", dt2.Rows[0]["PMPAYTOTAL"].ToString(),   //지급합계
                //                                                double.Parse(dt2.Rows[0]["PMTAXTOTAL"].ToString()) - double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()),   //공제합계
                //                                                double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) + double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
                //                                                dt2.Rows[0]["PMINCOMETAX"].ToString(),  //소득세
                //                                                dt2.Rows[0]["PMRESTAX"].ToString(),     //주민세
                //                                                dt2.Rows[0]["PMNATIONAMT"].ToString(),  //국민연금
                //                                                dt2.Rows[0]["PMHEALTHAMT"].ToString(),  //건강보험료
                //                                                double.Parse(dt2.Rows[0]["PMEMPLOYAMT"].ToString()) - double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()),  //고용보험
                //                                                dt2.Rows[0]["PMLTERMAMT"].ToString(),   //장기요양보험
                //                                                TYUserInfo.EmpNo,
                //                                                fsPMGUBN,
                //                                                fsPMYYMM,
                //                                                fsPMJIDATE,
                //                                                fsPMSABUN);
                //    this.DbConnector.ExecuteTranQueryList();
                //}
                ////장기요양보험
                //else if (dt.Rows[i]["PSPAYCODE"].ToString() == "2303")
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_HR_53BG7650", dt2.Rows[0]["PMPAYTOTAL"].ToString(),   //지급합계
                //                                                double.Parse(dt2.Rows[0]["PMTAXTOTAL"].ToString()) - double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()),   //공제합계
                //                                                double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) + double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
                //                                                dt2.Rows[0]["PMINCOMETAX"].ToString(),  //소득세
                //                                                dt2.Rows[0]["PMRESTAX"].ToString(),     //주민세
                //                                                dt2.Rows[0]["PMNATIONAMT"].ToString(),  //국민연금
                //                                                dt2.Rows[0]["PMHEALTHAMT"].ToString(),  //건강보험료
                //                                                dt2.Rows[0]["PMEMPLOYAMT"].ToString(),  //고용보험
                //                                                double.Parse(dt2.Rows[0]["PMLTERMAMT"].ToString()) - double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()),   //장기기요양보험
                //                                                TYUserInfo.EmpNo,
                //                                                fsPMGUBN,
                //                                                fsPMYYMM,
                //                                                fsPMJIDATE,
                //                                                fsPMSABUN);
                //    this.DbConnector.ExecuteTranQueryList();
                //}
                ////지급코드
                //else if (dt.Rows[i]["PSPAYCODE"].ToString().Substring(0, 1) == "1")
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_HR_53BG7650", double.Parse(dt2.Rows[0]["PMPAYTOTAL"].ToString()) - double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()),   //지급합계
                //                                                dt2.Rows[0]["PMTAXTOTAL"].ToString(),   //공제합계
                //                                                double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) - double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
                //                                                dt2.Rows[0]["PMINCOMETAX"].ToString(),  //소득세
                //                                                dt2.Rows[0]["PMRESTAX"].ToString(),     //주민세
                //                                                dt2.Rows[0]["PMNATIONAMT"].ToString(),  //국민연금
                //                                                dt2.Rows[0]["PMHEALTHAMT"].ToString(),  //건강보험료
                //                                                dt2.Rows[0]["PMEMPLOYAMT"].ToString(),  //고용보험
                //                                                dt2.Rows[0]["PMLTERMAMT"].ToString(),   //장기기요양보험
                //                                                TYUserInfo.EmpNo,
                //                                                fsPMGUBN,
                //                                                fsPMYYMM,
                //                                                fsPMJIDATE,
                //                                                fsPMSABUN);
                //    this.DbConnector.ExecuteTranQueryList();
                //}
                //#endregion
            }


            //급여계산결과산출내역 삭제
            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_BBN9K783",
                                            ds.Tables[1].Rows[i]["PWGUBN"].ToString(),
                                            ds.Tables[1].Rows[i]["PWYYMM"].ToString(),
                                            ds.Tables[1].Rows[i]["PWJIDATE"].ToString(),
                                            ds.Tables[1].Rows[i]["PWSABUN"].ToString(),
                                            ds.Tables[1].Rows[i]["PWSEQ"].ToString()
                                            );
                }
            }

            if (this.DbConnector.CommandCount > 0)
            {
                this.DbConnector.ExecuteTranQueryList();
            }

            this.DbConnector.CommandClear();
            //급여결과마스타 최종 UPDATE
            this.DbConnector.Attach("TY_P_HR_548JL131", TYUserInfo.EmpNo,
                                                        this.CBH01_PAYGUBN.GetValue(),
                                                        this.DTP01_PAYYYMM.GetString().Substring(0, 6),
                                                        this.DTP01_PAYJIDATE.GetString(),
                                                        this.CBH01_KBSABUN.GetValue());
            this.DbConnector.ExecuteTranQueryList();

            UP_Detail_DataBinding();

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_4CODZ962.GetDataSourceInclude(TSpread.TActionType.Remove, "PSGUBN", "PSYYMM", "PSJIDATE", "PSSABUN", "PSPAYCODE", "PSPAYAMOUNT"));
            ds.Tables.Add(this.FPS91_TY_S_HR_BBJF0761.GetDataSourceInclude(TSpread.TActionType.Remove, "PWGUBN", "PWYYMM", "PWJIDATE", "PWSABUN", "PWSEQ"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    //if (Convert.ToInt16(dt.Rows[i]["PSPAYCODE"].ToString()) >= 2910 && Convert.ToInt16(dt.Rows[i]["PSPAYCODE"].ToString()) <= 2915)
                    //{
                    //    this.ShowCustomMessage("불우이웃돕기 및 동호회 급여코드는 삭제 할수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    //    e.Successed = false;
                    //    return;
                    //}

                    if (ds.Tables[0].Rows[i]["PSGUBN"].ToString() == "")
                    {
                        this.ShowCustomMessage("소계 또는 합계항목은 삭제 할수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;

        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            
            //#region Description : 급여 마스타 update(등록)
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{   
            //    //급여 마스타 조회
            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach("TY_P_HR_4CNJ2950", fsPMGUBN, fsPMYYMM, fsPMJIDATE, fsPMSABUN, "", "");
            //    DataTable dt2 = this.DbConnector.ExecuteDataTable();

            //    //소득세
            //    if (ds.Tables[0].Rows[i]["PSPAYCODE"].ToString() == "2100")
            //    {
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_HR_53BG7650", dt2.Rows[0]["PMPAYTOTAL"].ToString(),   //지급합계
            //                                                    double.Parse(dt2.Rows[0]["PMTAXTOTAL"].ToString()) + double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()),   //공제합계
            //                                                    double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) - double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
            //                                                    double.Parse(dt2.Rows[0]["PMINCOMETAX"].ToString()) + double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()),  //소득세
            //                                                    dt2.Rows[0]["PMRESTAX"].ToString(),     //주민세
            //                                                    dt2.Rows[0]["PMNATIONAMT"].ToString(),  //국민연금
            //                                                    dt2.Rows[0]["PMHEALTHAMT"].ToString(),  //건강보험료
            //                                                    dt2.Rows[0]["PMEMPLOYAMT"].ToString(),  //고용보험
            //                                                    dt2.Rows[0]["PMLTERMAMT"].ToString(),   //장기기요양보험
            //                                                    TYUserInfo.EmpNo,
            //                                                    fsPMGUBN,
            //                                                    fsPMYYMM,
            //                                                    fsPMJIDATE,
            //                                                    fsPMSABUN);
            //        this.DbConnector.ExecuteTranQueryList();
            //    }
            //    //주민세
            //    else if (ds.Tables[0].Rows[i]["PSPAYCODE"].ToString() == "2110")
            //    {
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_HR_53BG7650", dt2.Rows[0]["PMPAYTOTAL"].ToString(),   //지급합계
            //                                                    double.Parse(dt2.Rows[0]["PMTAXTOTAL"].ToString()) + double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()),   //공제합계
            //                                                    double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) - double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
            //                                                    dt2.Rows[0]["PMINCOMETAX"].ToString(),  //소득세
            //                                                    double.Parse(dt2.Rows[0]["PMRESTAX"].ToString()) + double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()),     //주민세
            //                                                    dt2.Rows[0]["PMNATIONAMT"].ToString(),  //국민연금
            //                                                    dt2.Rows[0]["PMHEALTHAMT"].ToString(),  //건강보험료
            //                                                    dt2.Rows[0]["PMEMPLOYAMT"].ToString(),  //고용보험
            //                                                    dt2.Rows[0]["PMLTERMAMT"].ToString(),   //장기기요양보험
            //                                                    TYUserInfo.EmpNo,
            //                                                    fsPMGUBN,
            //                                                    fsPMYYMM,
            //                                                    fsPMJIDATE,
            //                                                    fsPMSABUN);
            //        this.DbConnector.ExecuteTranQueryList();
            //    }
            //    //국민연금
            //    else if (ds.Tables[0].Rows[i]["PSPAYCODE"].ToString() == "2500")
            //    {
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_HR_53BG7650", dt2.Rows[0]["PMPAYTOTAL"].ToString(),   //지급합계
            //                                                    double.Parse(dt2.Rows[0]["PMTAXTOTAL"].ToString()) + double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()),   //공제합계
            //                                                    double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) - double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
            //                                                    dt2.Rows[0]["PMINCOMETAX"].ToString(),  //소득세
            //                                                    dt2.Rows[0]["PMRESTAX"].ToString(),     //주민세
            //                                                    double.Parse(dt2.Rows[0]["PMNATIONAMT"].ToString()) + double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()),  //국민연금
            //                                                    dt2.Rows[0]["PMHEALTHAMT"].ToString(),  //건강보험료
            //                                                    dt2.Rows[0]["PMEMPLOYAMT"].ToString(),  //고용보험
            //                                                    dt2.Rows[0]["PMLTERMAMT"].ToString(),   //장기기요양보험
            //                                                    TYUserInfo.EmpNo,
            //                                                    fsPMGUBN,
            //                                                    fsPMYYMM,
            //                                                    fsPMJIDATE,
            //                                                    fsPMSABUN);
            //        this.DbConnector.ExecuteTranQueryList();
            //    }
            //    //건강보험료
            //    else if (ds.Tables[0].Rows[i]["PSPAYCODE"].ToString() == "2301")
            //    {
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_HR_53BG7650", dt2.Rows[0]["PMPAYTOTAL"].ToString(),   //지급합계
            //                                                    double.Parse(dt2.Rows[0]["PMTAXTOTAL"].ToString()) + double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()),   //공제합계
            //                                                    double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) - double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
            //                                                    dt2.Rows[0]["PMINCOMETAX"].ToString(),  //소득세
            //                                                    dt2.Rows[0]["PMRESTAX"].ToString(),     //주민세
            //                                                    dt2.Rows[0]["PMNATIONAMT"].ToString(),  //국민연금
            //                                                    double.Parse(dt2.Rows[0]["PMHEALTHAMT"].ToString()) + double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()),  //건강보험료
            //                                                    dt2.Rows[0]["PMEMPLOYAMT"].ToString(),  //고용보험
            //                                                    dt2.Rows[0]["PMLTERMAMT"].ToString(),   //장기기요양보험
            //                                                    TYUserInfo.EmpNo,
            //                                                    fsPMGUBN,
            //                                                    fsPMYYMM,
            //                                                    fsPMJIDATE,
            //                                                    fsPMSABUN);
            //        this.DbConnector.ExecuteTranQueryList();
            //    }
            //    //고용보험
            //    else if (ds.Tables[0].Rows[i]["PSPAYCODE"].ToString() == "2401")
            //    {
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_HR_53BG7650", dt2.Rows[0]["PMPAYTOTAL"].ToString(),   //지급합계
            //                                                    double.Parse(dt2.Rows[0]["PMTAXTOTAL"].ToString()) + double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()),   //공제합계
            //                                                    double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) - double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
            //                                                    dt2.Rows[0]["PMINCOMETAX"].ToString(),  //소득세
            //                                                    dt2.Rows[0]["PMRESTAX"].ToString(),     //주민세
            //                                                    dt2.Rows[0]["PMNATIONAMT"].ToString(),  //국민연금
            //                                                    dt2.Rows[0]["PMHEALTHAMT"].ToString(),  //건강보험료
            //                                                    double.Parse(dt2.Rows[0]["PMEMPLOYAMT"].ToString()) + double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()),  //고용보험
            //                                                    dt2.Rows[0]["PMLTERMAMT"].ToString(),   //장기요양보험
            //                                                    TYUserInfo.EmpNo,
            //                                                    fsPMGUBN,
            //                                                    fsPMYYMM,
            //                                                    fsPMJIDATE,
            //                                                    fsPMSABUN);
            //        this.DbConnector.ExecuteTranQueryList();
            //    }
            //    //장기요양보험
            //    else if (ds.Tables[0].Rows[i]["PSPAYCODE"].ToString() == "2303")
            //    {
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_HR_53BG7650", dt2.Rows[0]["PMPAYTOTAL"].ToString(),   //지급합계
            //                                                    double.Parse(dt2.Rows[0]["PMTAXTOTAL"].ToString()) + double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()),   //공제합계
            //                                                    double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) - double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
            //                                                    dt2.Rows[0]["PMINCOMETAX"].ToString(),  //소득세
            //                                                    dt2.Rows[0]["PMRESTAX"].ToString(),     //주민세
            //                                                    dt2.Rows[0]["PMNATIONAMT"].ToString(),  //국민연금
            //                                                    dt2.Rows[0]["PMHEALTHAMT"].ToString(),  //건강보험료
            //                                                    dt2.Rows[0]["PMEMPLOYAMT"].ToString(),  //고용보험
            //                                                    double.Parse(dt2.Rows[0]["PMLTERMAMT"].ToString()) + double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()),   //장기기요양보험
            //                                                    TYUserInfo.EmpNo,
            //                                                    fsPMGUBN,
            //                                                    fsPMYYMM,
            //                                                    fsPMJIDATE,
            //                                                    fsPMSABUN);
            //        this.DbConnector.ExecuteTranQueryList();
            //    }
            //    //지급코드
            //    else if (ds.Tables[0].Rows[i]["PSPAYCODE"].ToString().Substring(0, 1) == "1")
            //    {
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_HR_53BG7650", double.Parse(dt2.Rows[0]["PMPAYTOTAL"].ToString()) + double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()),   //지급합계
            //                                                    dt2.Rows[0]["PMTAXTOTAL"].ToString(),   //공제합계
            //                                                    double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) + double.Parse(ds.Tables[0].Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
            //                                                    dt2.Rows[0]["PMINCOMETAX"].ToString(),  //소득세
            //                                                    dt2.Rows[0]["PMRESTAX"].ToString(),     //주민세
            //                                                    dt2.Rows[0]["PMNATIONAMT"].ToString(),  //국민연금
            //                                                    dt2.Rows[0]["PMHEALTHAMT"].ToString(),  //건강보험료
            //                                                    dt2.Rows[0]["PMEMPLOYAMT"].ToString(),  //고용보험
            //                                                    dt2.Rows[0]["PMLTERMAMT"].ToString(),   //장기기요양보험
            //                                                    TYUserInfo.EmpNo,
            //                                                    fsPMGUBN,
            //                                                    fsPMYYMM,
            //                                                    fsPMJIDATE,
            //                                                    fsPMSABUN);
            //        this.DbConnector.ExecuteTranQueryList();
            //    }
            //}
            //#endregion

            //#region Description : 급여 마스타 update(수정)
            //for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            //{
            //    //급여 마스타 조회
            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach("TY_P_HR_4CNJ2950", fsPMGUBN, fsPMYYMM, fsPMJIDATE, fsPMSABUN, "", "");
            //    DataTable dt2 = this.DbConnector.ExecuteDataTable();

            //    //소득세
            //    if (ds.Tables[1].Rows[i]["PSPAYCODE"].ToString() == "2100")
            //    {
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_HR_53BG7650", dt2.Rows[0]["PMPAYTOTAL"].ToString(),   //지급합계
            //                                                    double.Parse(dt2.Rows[0]["PMTAXTOTAL"].ToString()) - double.Parse(dt2.Rows[0]["PMINCOMETAX"].ToString()) + double.Parse(ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString()),   //공제합계
            //                                                    double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) + double.Parse(dt2.Rows[0]["PMINCOMETAX"].ToString()) - double.Parse(ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
            //                                                    ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString(),  //소득세
            //                                                    dt2.Rows[0]["PMRESTAX"].ToString(),     //주민세
            //                                                    dt2.Rows[0]["PMNATIONAMT"].ToString(),  //국민연금
            //                                                    dt2.Rows[0]["PMHEALTHAMT"].ToString(),  //건강보험료
            //                                                    dt2.Rows[0]["PMEMPLOYAMT"].ToString(),  //고용보험
            //                                                    dt2.Rows[0]["PMLTERMAMT"].ToString(),   //장기기요양보험
            //                                                    TYUserInfo.EmpNo,
            //                                                    fsPMGUBN,
            //                                                    fsPMYYMM,
            //                                                    fsPMJIDATE,
            //                                                    fsPMSABUN);
            //        this.DbConnector.ExecuteTranQueryList();
            //    }
            //    //주민세
            //    else if (ds.Tables[1].Rows[i]["PSPAYCODE"].ToString() == "2110")
            //    {
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_HR_53BG7650", dt2.Rows[0]["PMPAYTOTAL"].ToString(),   //지급합계
            //                                                    double.Parse(dt2.Rows[0]["PMTAXTOTAL"].ToString()) - double.Parse(dt2.Rows[0]["PMINCOMETAX"].ToString()) + double.Parse(ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString()),   //공제합계
            //                                                    double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) + double.Parse(dt2.Rows[0]["PMINCOMETAX"].ToString()) - double.Parse(ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
            //                                                    dt2.Rows[0]["PMINCOMETAX"].ToString(),  //소득세
            //                                                    ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString(),     //주민세
            //                                                    dt2.Rows[0]["PMNATIONAMT"].ToString(),  //국민연금
            //                                                    dt2.Rows[0]["PMHEALTHAMT"].ToString(),  //건강보험료
            //                                                    dt2.Rows[0]["PMEMPLOYAMT"].ToString(),  //고용보험
            //                                                    dt2.Rows[0]["PMLTERMAMT"].ToString(),   //장기기요양보험
            //                                                    TYUserInfo.EmpNo,
            //                                                    fsPMGUBN,
            //                                                    fsPMYYMM,
            //                                                    fsPMJIDATE,
            //                                                    fsPMSABUN);
            //        this.DbConnector.ExecuteTranQueryList();
            //    }
            //    //국민연금
            //    else if (ds.Tables[1].Rows[i]["PSPAYCODE"].ToString() == "2500")
            //    {
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_HR_53BG7650", dt2.Rows[0]["PMPAYTOTAL"].ToString(),   //지급합계
            //                                                    double.Parse(dt2.Rows[0]["PMTAXTOTAL"].ToString()) - double.Parse(dt2.Rows[0]["PMINCOMETAX"].ToString()) + double.Parse(ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString()),   //공제합계
            //                                                    double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) + double.Parse(dt2.Rows[0]["PMINCOMETAX"].ToString()) - double.Parse(ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
            //                                                    dt2.Rows[0]["PMINCOMETAX"].ToString(),  //소득세
            //                                                    dt2.Rows[0]["PMRESTAX"].ToString(),     //주민세
            //                                                    ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString(),  //국민연금
            //                                                    dt2.Rows[0]["PMHEALTHAMT"].ToString(),  //건강보험료
            //                                                    dt2.Rows[0]["PMEMPLOYAMT"].ToString(),  //고용보험
            //                                                    dt2.Rows[0]["PMLTERMAMT"].ToString(),   //장기기요양보험
            //                                                    TYUserInfo.EmpNo,
            //                                                    fsPMGUBN,
            //                                                    fsPMYYMM,
            //                                                    fsPMJIDATE,
            //                                                    fsPMSABUN);
            //        this.DbConnector.ExecuteTranQueryList();
            //    }
            //    //건강보험료
            //    else if (ds.Tables[1].Rows[i]["PSPAYCODE"].ToString() == "2301")
            //    {
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_HR_53BG7650", dt2.Rows[0]["PMPAYTOTAL"].ToString(),   //지급합계
            //                                                    double.Parse(dt2.Rows[0]["PMTAXTOTAL"].ToString()) - double.Parse(dt2.Rows[0]["PMINCOMETAX"].ToString()) + double.Parse(ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString()),   //공제합계
            //                                                    double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) + double.Parse(dt2.Rows[0]["PMINCOMETAX"].ToString()) - double.Parse(ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
            //                                                    dt2.Rows[0]["PMINCOMETAX"].ToString(),  //소득세
            //                                                    dt2.Rows[0]["PMRESTAX"].ToString(),     //주민세
            //                                                    dt2.Rows[0]["PMNATIONAMT"].ToString(),  //국민연금
            //                                                    ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString(),  //건강보험료
            //                                                    dt2.Rows[0]["PMEMPLOYAMT"].ToString(),  //고용보험
            //                                                    dt2.Rows[0]["PMLTERMAMT"].ToString(),   //장기기요양보험
            //                                                    TYUserInfo.EmpNo,
            //                                                    fsPMGUBN,
            //                                                    fsPMYYMM,
            //                                                    fsPMJIDATE,
            //                                                    fsPMSABUN);
            //        this.DbConnector.ExecuteTranQueryList();
            //    }
            //    //고용보험
            //    else if (ds.Tables[1].Rows[i]["PSPAYCODE"].ToString() == "2401")
            //    {
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_HR_53BG7650", dt2.Rows[0]["PMPAYTOTAL"].ToString(),   //지급합계
            //                                                    double.Parse(dt2.Rows[0]["PMTAXTOTAL"].ToString()) - double.Parse(dt2.Rows[0]["PMINCOMETAX"].ToString()) + double.Parse(ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString()),   //공제합계
            //                                                    double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) + double.Parse(dt2.Rows[0]["PMINCOMETAX"].ToString()) - double.Parse(ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
            //                                                    dt2.Rows[0]["PMINCOMETAX"].ToString(),  //소득세
            //                                                    dt2.Rows[0]["PMRESTAX"].ToString(),     //주민세
            //                                                    dt2.Rows[0]["PMNATIONAMT"].ToString(),  //국민연금
            //                                                    dt2.Rows[0]["PMHEALTHAMT"].ToString(),  //건강보험료
            //                                                    ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString(),  //고용보험
            //                                                    dt2.Rows[0]["PMLTERMAMT"].ToString(),   //장기요양보험
            //                                                    TYUserInfo.EmpNo,
            //                                                    fsPMGUBN,
            //                                                    fsPMYYMM,
            //                                                    fsPMJIDATE,
            //                                                    fsPMSABUN);
            //        this.DbConnector.ExecuteTranQueryList();
            //    }
            //    //장기요양보험
            //    else if (ds.Tables[1].Rows[i]["PSPAYCODE"].ToString() == "2303")
            //    {
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_HR_53BG7650", dt2.Rows[0]["PMPAYTOTAL"].ToString(),   //지급합계
            //                                                    double.Parse(dt2.Rows[0]["PMTAXTOTAL"].ToString()) - double.Parse(dt2.Rows[0]["PMINCOMETAX"].ToString()) + double.Parse(ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString()),   //공제합계
            //                                                    double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) + double.Parse(dt2.Rows[0]["PMINCOMETAX"].ToString()) - double.Parse(ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
            //                                                    dt2.Rows[0]["PMINCOMETAX"].ToString(),  //소득세
            //                                                    dt2.Rows[0]["PMRESTAX"].ToString(),     //주민세
            //                                                    dt2.Rows[0]["PMNATIONAMT"].ToString(),  //국민연금
            //                                                    dt2.Rows[0]["PMHEALTHAMT"].ToString(),  //건강보험료
            //                                                    dt2.Rows[0]["PMEMPLOYAMT"].ToString(),  //고용보험
            //                                                    ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString(),   //장기기요양보험
            //                                                    TYUserInfo.EmpNo,
            //                                                    fsPMGUBN,
            //                                                    fsPMYYMM,
            //                                                    fsPMJIDATE,
            //                                                    fsPMSABUN);
            //        this.DbConnector.ExecuteTranQueryList();
            //    }
            //    //지급코드
            //    else if (ds.Tables[1].Rows[i]["PSPAYCODE"].ToString().Substring(0, 1) == "1")
            //    {
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_HR_4CODU958", fsPMGUBN, fsPMYYMM, fsPMJIDATE, fsPMSABUN, ds.Tables[1].Rows[i]["PSPAYCODE"].ToString());
            //        DataTable dt = this.DbConnector.ExecuteDataTable();

            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_HR_53BG7650", double.Parse(dt2.Rows[0]["PMPAYTOTAL"].ToString()) - double.Parse(dt.Rows[0]["PSPAYAMOUNT"].ToString()) + double.Parse(ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString()),   //지급합계
            //                                                    dt2.Rows[0]["PMTAXTOTAL"].ToString(),   //공제합계
            //                                                    double.Parse(dt2.Rows[0]["PMAFTERTOTAL"].ToString()) - double.Parse(dt.Rows[0]["PSPAYAMOUNT"].ToString()) + double.Parse(ds.Tables[1].Rows[i]["PSPAYAMOUNT"].ToString()), //차인지급액
            //                                                    dt2.Rows[0]["PMINCOMETAX"].ToString(),  //소득세
            //                                                    dt2.Rows[0]["PMRESTAX"].ToString(),     //주민세
            //                                                    dt2.Rows[0]["PMNATIONAMT"].ToString(),  //국민연금
            //                                                    dt2.Rows[0]["PMHEALTHAMT"].ToString(),  //건강보험료
            //                                                    dt2.Rows[0]["PMEMPLOYAMT"].ToString(),  //고용보험
            //                                                    dt2.Rows[0]["PMLTERMAMT"].ToString(),   //장기기요양보험
            //                                                    TYUserInfo.EmpNo,
            //                                                    fsPMGUBN,
            //                                                    fsPMYYMM,
            //                                                    fsPMJIDATE,
            //                                                    fsPMSABUN);
            //        this.DbConnector.ExecuteTranQueryList();
            //    }
            //}
            //#endregion
             

            this.DbConnector.CommandClear();

            this.DataTableColumnAdd(ds.Tables[0], "PSHISAB", TYUserInfo.EmpNo);
            this.DataTableColumnAdd(ds.Tables[1], "PSHISAB", TYUserInfo.EmpNo);

            this.DbConnector.Attach("TY_P_HR_4CODW959", ds.Tables[0]);
            this.DbConnector.Attach("TY_P_HR_4CODY960", ds.Tables[1]);

            //급여결과마스타 최종 UPDATE
            this.DbConnector.Attach("TY_P_HR_548JL131", TYUserInfo.EmpNo,
                                                        this.CBH01_PAYGUBN.GetValue(),
                                                        this.DTP01_PAYYYMM.GetString().Substring(0,6),
                                                        this.DTP01_PAYJIDATE.GetString(),
                                                        this.CBH01_KBSABUN.GetValue());
            this.DbConnector.ExecuteTranQueryList();

            if (ds.Tables[1].Rows.Count > 0)
            {
                //상여총액 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4CNJ2950",
                                            "S1",
                                            this.DTP01_PAYYYMM.GetString().Substring(0, 6),
                                            this.DTP01_PAYJIDATE.GetString(), this.CBH01_KBSABUN.GetValue(), "", "");
                DataTable dr = this.DbConnector.ExecuteDataTable();
                if (dr.Rows.Count > 0)
                {
                     string sBunusTotal = dr.Rows[0]["PMPAYTOTAL"].ToString();

                     this.DbConnector.Attach("TY_P_HR_5C2DN258",
                                             sBunusTotal,
                                             TYUserInfo.EmpNo,
                                             "M1",
                                             this.DTP01_PAYYYMM.GetString().Substring(0, 6),
                                             this.DTP01_PAYJIDATE.GetString(),
                                             this.CBH01_KBSABUN.GetValue(),
                                             fsPYBUNUSCODE);

                     this.DbConnector.Attach("TY_P_HR_548JL131", TYUserInfo.EmpNo,
                                                                 "M1",
                                                                 this.DTP01_PAYYYMM.GetString().Substring(0, 6),
                                                                 this.DTP01_PAYJIDATE.GetString(),
                                                                 this.CBH01_KBSABUN.GetValue());
                }
                this.DbConnector.ExecuteTranQueryList(); 
            }

            //급여계산결과산출내역 등록
            if (ds.Tables[2].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_BBN9K781",
                                            ds.Tables[2].Rows[i]["PWGUBN"].ToString(),
                                            ds.Tables[2].Rows[i]["PWYYMM"].ToString(),
                                            ds.Tables[2].Rows[i]["PWJIDATE"].ToString(),
                                            ds.Tables[2].Rows[i]["PWSABUN"].ToString(),
                                            ds.Tables[2].Rows[i]["PWSEQ"].ToString(),
                                            ds.Tables[2].Rows[i]["PWITEM"].ToString(),
                                            ds.Tables[2].Rows[i]["PWCOMTEXT"].ToString(),
                                            ds.Tables[2].Rows[i]["PWCOMAMOUNT"].ToString(),
                                            TYUserInfo.EmpNo
                                            );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            //급여계산결과산출내역 수정
            if (ds.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_BBN9K782",
                                            ds.Tables[3].Rows[i]["PWCOMTEXT"].ToString(),
                                            ds.Tables[3].Rows[i]["PWCOMAMOUNT"].ToString(),
                                            TYUserInfo.EmpNo,
                                            ds.Tables[3].Rows[i]["PWGUBN"].ToString(),
                                            ds.Tables[3].Rows[i]["PWYYMM"].ToString(),
                                            ds.Tables[3].Rows[i]["PWJIDATE"].ToString(),
                                            ds.Tables[3].Rows[i]["PWSABUN"].ToString(),
                                            ds.Tables[3].Rows[i]["PWSEQ"].ToString()
                                            );
                }
                this.DbConnector.ExecuteTranQueryList();
            }


            UP_Detail_DataBinding();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {           

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_4CODZ962.GetDataSourceInclude(TSpread.TActionType.New, "PSGUBN", "PSYYMM", "PSJIDATE", "PSSABUN", "PSPAYCODE", "PSSTAMOUNT", "PSPAYAMOUNT", "PSPAYGUBN", "PSBIGO"));
            ds.Tables.Add(this.FPS91_TY_S_HR_4CODZ962.GetDataSourceInclude(TSpread.TActionType.Update, "PSGUBN", "PSYYMM", "PSJIDATE", "PSSABUN", "PSPAYCODE", "PSSTAMOUNT", "PSPAYAMOUNT", "PSPAYGUBN", "PSBIGO"));

            ds.Tables.Add(this.FPS91_TY_S_HR_BBJF0761.GetDataSourceInclude(TSpread.TActionType.New, "PWGUBN", "PWYYMM", "PWJIDATE", "PWSABUN", "PWSEQ", "PWITEM", "PWCOMTEXT", "PWCOMAMOUNT"));
            ds.Tables.Add(this.FPS91_TY_S_HR_BBJF0761.GetDataSourceInclude(TSpread.TActionType.Update, "PWGUBN", "PWYYMM", "PWJIDATE", "PWSABUN", "PWSEQ", "PWITEM", "PWCOMTEXT", "PWCOMAMOUNT"));
           

            //등록
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                if (Convert.ToInt16(ds.Tables[0].Rows[i]["PSPAYCODE"].ToString()) >= 2910 && Convert.ToInt16(ds.Tables[0].Rows[i]["PSPAYCODE"].ToString()) <= 2915 )
                {
                    this.ShowCustomMessage("불우이웃돕기 및 동호회 급여코드는 추가 할수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }

                if ((fsPYBUNUSCODE == ds.Tables[0].Rows[i]["PSPAYCODE"].ToString()) && this.CBH01_PAYGUBN.GetValue().ToString() == "S1")
                {
                    this.ShowCustomMessage("상여에서 정기상여금코드는 사용할수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_4CODU958", ds.Tables[0].Rows[i]["PSGUBN"].ToString(),
                                                            ds.Tables[0].Rows[i]["PSYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["PSJIDATE"].ToString(),
                                                            ds.Tables[0].Rows[i]["PSSABUN"].ToString(),
                                                            ds.Tables[0].Rows[i]["PSPAYCODE"].ToString());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_387AG357");
                    e.Successed = false;
                    return;
                }
                for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    if (i != j)
                    {
                        if (ds.Tables[0].Rows[i]["PSGUBN"].ToString() == ds.Tables[0].Rows[j]["PSGUBN"].ToString() && ds.Tables[0].Rows[i]["PSYYMM"].ToString() == ds.Tables[0].Rows[j]["PSYYMM"].ToString() &&
                            ds.Tables[0].Rows[i]["PSJIDATE"].ToString() == ds.Tables[0].Rows[j]["PSJIDATE"].ToString() && ds.Tables[0].Rows[i]["PSSABUN"].ToString() == ds.Tables[0].Rows[j]["PSSABUN"].ToString() &&
                            ds.Tables[0].Rows[i]["PSPAYCODE"].ToString() == ds.Tables[0].Rows[j]["PSPAYCODE"].ToString())
                        {
                            this.ShowMessage("TY_M_AC_387AG357");
                            e.Successed = false;
                            return;
                        }
                    }
                }
            }

            //수정
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if ((fsPYBUNUSCODE == ds.Tables[1].Rows[i]["PSPAYCODE"].ToString()) && this.CBH01_PAYGUBN.GetValue().ToString() == "M1" )
                {
                    this.ShowCustomMessage("정기상여금은 상여에서만 수정 가능합니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }

                if (Convert.ToInt16(ds.Tables[1].Rows[i]["PSPAYCODE"].ToString()) >= 2910 && Convert.ToInt16(ds.Tables[1].Rows[i]["PSPAYCODE"].ToString()) <= 2915)
                {
                    this.ShowCustomMessage("불우이웃돕기 및 동호회 급여코드는 수정 할수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0 && ds.Tables[3].Rows.Count == 0 )
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

        #region Description : 급여결과 마스타 조회
        private void UP_Master_DataBinding()
        {   
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4COFT964", fsPMGUBN, fsPMYYMM, fsPMJIDATE, fsPMSABUN);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CBH01_PAYGUBN.SetValue(dt.Rows[0]["PMGUBN"].ToString());
                this.DTP01_PAYYYMM.SetValue(dt.Rows[0]["PMYYMM"].ToString());
                this.DTP01_PAYJIDATE.SetValue(dt.Rows[0]["PMJIDATE"].ToString());
                this.CBH01_KBSABUN.SetValue(dt.Rows[0]["PMSABUN"].ToString());
                this.CBH01_KBJKCD.SetValue(dt.Rows[0]["PMJKCD"].ToString());
                this.TXT01_KBHOBN.SetValue(dt.Rows[0]["PMHOBN"].ToString());
                this.CBH01_PTTEAM.SetValue(dt.Rows[0]["PMTEAM"].ToString());
                this.CBH01_PTBUSEO.SetValue(dt.Rows[0]["PMBUSEO"].ToString());
                this.DTP01_STDATE.SetValue(dt.Rows[0]["PMWKSDATE"].ToString());
                this.DTP01_EDDATE.SetValue(dt.Rows[0]["PMWKEDATE"].ToString());
                this.TXT01_PTPAYRATE.SetValue(dt.Rows[0]["PMPAYRATE"].ToString());

                this.TXT01_PMORDPAY.SetValue(dt.Rows[0]["PMORDPAY"].ToString());
                this.TXT01_PMORDOTPAY.SetValue(dt.Rows[0]["PMORDOTPAY"].ToString());

                
                this.TXT01_PMHFOTTIME.SetValue(dt.Rows[0]["PMHFOTTIME"].ToString());
                this.TXT01_PMWKOTTIME.SetValue(dt.Rows[0]["PMWKOTTIME"].ToString());
                this.TXT01_PMNTOTTIME.SetValue(dt.Rows[0]["PMNTOTTIME"].ToString());
                this.TXT01_PMHTOTTIME.SetValue(dt.Rows[0]["PMHTOTTIME"].ToString());
                this.TXT01_PMWTOTTIME.SetValue(dt.Rows[0]["PMWTOTTIME"].ToString());
                this.TXT01_PMGJOTTIME.SetValue(dt.Rows[0]["PMGJOTTIME"].ToString());

                this.TXT01_PMHFAMOUNT.SetValue(dt.Rows[0]["PMHFAMOUNT"].ToString());
                this.TXT01_PMOTAMOUNT.SetValue(dt.Rows[0]["PMOTAMOUNT"].ToString());
                this.TXT01_PMNTAMOUNT.SetValue(dt.Rows[0]["PMNTAMOUNT"].ToString());
                this.TXT01_PMHTAMOUNT.SetValue(dt.Rows[0]["PMHTAMOUNT"].ToString());
                this.TXT01_PMWTAMOUNT.SetValue(dt.Rows[0]["PMWTAMOUNT"].ToString());
                this.TXT01_PMGJAMOUNT.SetValue(dt.Rows[0]["PMGJAMOUNT"].ToString());

                this.CBH01_PMEXCD.SetValue(dt.Rows[0]["PMEXCD"].ToString());
                this.DTP01_PMEXSDATE.SetValue(dt.Rows[0]["PMEXSDATE"].ToString());
                this.DTP01_PMEXEDATE.SetValue(dt.Rows[0]["PMEXEDATE"].ToString());
                this.TXT01_PMEXRATE.SetValue(dt.Rows[0]["PMEXRATE"].ToString());
                this.TXT01_PMEXMEMO.SetValue(dt.Rows[0]["PMEXMEMO"].ToString());

                this.TXT01_PMWKDAYS.SetValue(dt.Rows[0]["PMWKDAYS"].ToString());
                this.TXT01_PMWKTIMES.SetValue(dt.Rows[0]["PMWKTIMES"].ToString());              

            }
        }
        #endregion

        #region Description : 그리드 조회
        private void UP_Detail_DataBinding()
        {
            this.FPS91_TY_S_HR_4CODZ962.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4CODU958", fsPMGUBN, fsPMYYMM, fsPMJIDATE, fsPMSABUN,"");
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_HR_4CODZ962.SetValue(UP_DatatableChange(dt));

                for (int i = 0; i < this.FPS91_TY_S_HR_4CODZ962.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_HR_4CODZ962.GetValue(i, "PSPAYCODE").ToString() == "1700") //정기상여금
                    {
                        this.FPS91_TY_S_HR_4CODZ962_Sheet1.Rows[i].Locked = true;
                    }
                    if (this.FPS91_TY_S_HR_4CODZ962.GetValue(i, "PSPAYCODENM").ToString() == "[소  계]")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_HR_4CODZ962_Sheet1.Rows[i].Locked = true;
                        this.FPS91_TY_S_HR_4CODZ962.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                    else if (this.FPS91_TY_S_HR_4CODZ962.GetValue(i, "JIGUBN").ToString() == "[차인지급액]")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_HR_4CODZ962_Sheet1.Rows[i].Locked = true;
                        this.FPS91_TY_S_HR_4CODZ962.ActiveSheet.Rows[i].BackColor = Color.FromArgb(242, 231, 147);
                        this.FPS91_TY_S_HR_4CODZ962.ActiveSheet.Rows[i].ForeColor = Color.Red;
                        this.FPS91_TY_S_HR_4CODZ962.ActiveSheet.Rows[i].Font = new Font("굴림체", 10, FontStyle.Bold);
                    }
                }
            }
            else
            {
                this.FPS91_TY_S_HR_4CODZ962.SetValue(dt);
            }

            //산출식
            this.FPS91_TY_S_HR_BBJF0761.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_BBJF2763", fsPMGUBN, fsPMYYMM, fsPMJIDATE, fsPMSABUN );
            DataTable dk = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_HR_BBJF0761.SetValue(dk);

        }
        #endregion

        #region Description : 행 추가 이벤트
        private void FPS91_TY_S_HR_4CODZ962_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_4CODZ962.SetValue(e.RowIndex, "PSGUBN", this.CBH01_PAYGUBN.GetValue().ToString());
            this.FPS91_TY_S_HR_4CODZ962.SetValue(e.RowIndex, "PSYYMM", this.DTP01_PAYYYMM.GetString().Substring(0,6));
            this.FPS91_TY_S_HR_4CODZ962.SetValue(e.RowIndex, "PSJIDATE", this.DTP01_PAYJIDATE.GetString());
            this.FPS91_TY_S_HR_4CODZ962.SetValue(e.RowIndex, "PSSABUN", this.CBH01_KBSABUN.GetValue().ToString());
        }
        #endregion

        #region Description : 그리드 소계/합계 추가
        private DataTable UP_DatatableChange(DataTable dt)
        {
            DataTable rtnDt = new DataTable();

            DataRow row;

            double dPSSTAMOUNT1_HAP = 0;
            double dPSPAYAMOUNT1_HAP = 0;
            double dPSPAYAMOUNT2_HAP = 0;

            rtnDt.Columns.Add("PSGUBN", typeof(System.String));
            rtnDt.Columns.Add("PSYYMM", typeof(System.String));
            rtnDt.Columns.Add("PSJIDATE", typeof(System.String));
            rtnDt.Columns.Add("PSSABUN", typeof(System.String));
            rtnDt.Columns.Add("JIGUBN", typeof(System.String));
            rtnDt.Columns.Add("PSPAYCODE", typeof(System.String));
            rtnDt.Columns.Add("PSPAYCODENM", typeof(System.String));
            rtnDt.Columns.Add("PSSTAMOUNT", typeof(System.String));
            rtnDt.Columns.Add("PSPAYAMOUNT", typeof(System.String));
            rtnDt.Columns.Add("PSPAYGUBN", typeof(System.String));
            rtnDt.Columns.Add("PSBIGO", typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    if (dt.Rows[i - 1]["JIGUBN"].ToString() != dt.Rows[i]["JIGUBN"].ToString())
                    {
                        row = rtnDt.NewRow();

                        row["PSGUBN"] = DBNull.Value;
                        row["PSYYMM"] = DBNull.Value;
                        row["PSJIDATE"] = DBNull.Value;
                        row["PSSABUN"] = DBNull.Value;
                        row["JIGUBN"] = DBNull.Value;
                        row["PSPAYCODE"] = DBNull.Value;
                        row["PSPAYCODENM"] = "[소  계]";
                        row["PSSTAMOUNT"] = string.Format("{0:#,##0.0}", dPSSTAMOUNT1_HAP);
                        row["PSPAYAMOUNT"] = string.Format("{0:#,##0.0}", dPSPAYAMOUNT1_HAP);
                        row["PSPAYGUBN"] = DBNull.Value;
                        row["PSBIGO"] = DBNull.Value;

                        rtnDt.Rows.Add(row);
                        dPSPAYAMOUNT2_HAP = dPSPAYAMOUNT1_HAP;
                            
                        dPSSTAMOUNT1_HAP = double.Parse(dt.Rows[i]["PSSTAMOUNT"].ToString());
                        dPSPAYAMOUNT1_HAP = double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString());
                    }
                    else
                    {
                        dPSSTAMOUNT1_HAP = dPSSTAMOUNT1_HAP + double.Parse(dt.Rows[i]["PSSTAMOUNT"].ToString());
                        dPSPAYAMOUNT1_HAP = dPSPAYAMOUNT1_HAP + double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString());
                    }
                }
                else
                {
                    dPSSTAMOUNT1_HAP = double.Parse(dt.Rows[i]["PSSTAMOUNT"].ToString());
                    dPSPAYAMOUNT1_HAP = double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString());
                }

                row = rtnDt.NewRow();

                row["PSGUBN"] = dt.Rows[i]["PSGUBN"].ToString();
                row["PSYYMM"] = dt.Rows[i]["PSYYMM"].ToString();
                row["PSJIDATE"] = dt.Rows[i]["PSJIDATE"].ToString();
                row["PSSABUN"] = dt.Rows[i]["PSSABUN"].ToString();
                row["JIGUBN"] = dt.Rows[i]["JIGUBN"].ToString();
                row["PSPAYCODE"] = dt.Rows[i]["PSPAYCODE"].ToString();
                row["PSPAYCODENM"] = dt.Rows[i]["PSPAYCODENM"].ToString();
                row["PSSTAMOUNT"] = dt.Rows[i]["PSSTAMOUNT"].ToString();
                row["PSPAYAMOUNT"] = dt.Rows[i]["PSPAYAMOUNT"].ToString();
                row["PSPAYGUBN"] = dt.Rows[i]["PSPAYGUBN"].ToString();
                row["PSBIGO"] = dt.Rows[i]["PSBIGO"].ToString();

                rtnDt.Rows.Add(row);
            }

            row = rtnDt.NewRow();

            row["PSGUBN"] = DBNull.Value;
            row["PSYYMM"] = DBNull.Value;
            row["PSJIDATE"] = DBNull.Value;
            row["PSSABUN"] = DBNull.Value;
            row["JIGUBN"] = DBNull.Value;
            row["PSPAYCODE"] = DBNull.Value;
            row["PSPAYCODENM"] = "[소  계]";
            row["PSSTAMOUNT"] = string.Format("{0:#,##0.0}", dPSSTAMOUNT1_HAP);
            row["PSPAYAMOUNT"] = string.Format("{0:#,##0.0}", dPSPAYAMOUNT1_HAP);
            row["PSPAYGUBN"] = DBNull.Value;
            row["PSBIGO"] = DBNull.Value;

            rtnDt.Rows.Add(row);

            row = rtnDt.NewRow();

            row["PSGUBN"] = DBNull.Value;
            row["PSYYMM"] = DBNull.Value;
            row["PSJIDATE"] = DBNull.Value;
            row["PSSABUN"] = DBNull.Value;
            row["JIGUBN"] = "[차인지급액]";
            row["PSPAYCODE"] = DBNull.Value;
            row["PSPAYCODENM"] = DBNull.Value;
            row["PSSTAMOUNT"] = DBNull.Value;
            
            if (dPSPAYAMOUNT2_HAP > 0)
            {
                row["PSPAYAMOUNT"] = string.Format("{0:#,##0.0}", dPSPAYAMOUNT2_HAP - dPSPAYAMOUNT1_HAP);
            }
            else
            {
                row["PSPAYAMOUNT"] = string.Format("{0:#,##0.0}", dPSPAYAMOUNT1_HAP);
            }

            row["PSPAYGUBN"] = DBNull.Value;
            row["PSBIGO"] = DBNull.Value;

            rtnDt.Rows.Add(row);

            return rtnDt;
        }
        #endregion

        #region Description : 급여코드 코드박스 팝업
        private void FPS91_TY_S_HR_4CODZ962_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1 && FPS91_TY_S_HR_4CODZ962.ActiveSheet.ActiveColumnIndex == 5)
            {
                TYHRPY08C1 popup = new TYHRPY08C1(this.FPS91_TY_S_HR_4CODZ962.GetValue("PSJIDATE").ToString(), this.FPS91_TY_S_HR_4CODZ962.GetValue("PSSABUN").ToString(), this.FPS91_TY_S_HR_4CODZ962.GetValue("PSGUBN").ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    FPS91_TY_S_HR_4CODZ962.SetValue(FPS91_TY_S_HR_4CODZ962.ActiveSheet.ActiveRowIndex, "PSPAYCODE", popup.fscode);

                    FPS91_TY_S_HR_4CODZ962.SetValue(FPS91_TY_S_HR_4CODZ962.ActiveSheet.ActiveRowIndex, "PSPAYCODENM", popup.fsname);

                    FPS91_TY_S_HR_4CODZ962.SetValue(FPS91_TY_S_HR_4CODZ962.ActiveSheet.ActiveRowIndex, "PSSTAMOUNT", popup.fsamt);
                }
            }
        }
        #endregion

        #region Description : 급여 전표 체크
        private void UP_BankDataCheck()
        {
            string sPMGUBN = string.Empty;

            sPMGUBN = fsPMGUBN;
            if (Convert.ToInt32(fsPMYYMM) >= 201501)
            {
                if (fsPMGUBN == "S1")
                {
                    sPMGUBN = "M1";
                }
                else
                {
                    sPMGUBN = fsPMGUBN;
                }
            }

            if (fsCallGubn == "READ")
            {
                this.BTN61_SAV.SetReadOnly(true);
                this.BTN61_REM.SetReadOnly(true);
            }
            else
            {
                this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_HR_53K94751", fsPMYYMM, sPMGUBN, fsPMJIDATE);
                this.DbConnector.Attach("TY_P_HR_5AJFI004", fsPMYYMM, "1", sPMGUBN, "1", fsPMJIDATE);

                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    this.BTN61_SAV.SetReadOnly(true);
                    this.BTN61_REM.SetReadOnly(true);
                }
            }
        }
        #endregion

        #region Description : 필드 Lock
        private void UP_FieldLock(bool value)
        {
            this.CBH01_PAYGUBN.SetReadOnly(value);
            this.DTP01_PAYYYMM.SetReadOnly(value);
            this.DTP01_PAYJIDATE.SetReadOnly(value);
            this.CBH01_KBSABUN.SetReadOnly(value);
            this.CBH01_KBJKCD.SetReadOnly(value);
            this.TXT01_KBHOBN.SetReadOnly(value);
            this.CBH01_PTTEAM.SetReadOnly(value);
            this.CBH01_PTBUSEO.SetReadOnly(value);
            this.DTP01_STDATE.SetReadOnly(value);
            this.DTP01_EDDATE.SetReadOnly(value);
            this.TXT01_PTPAYRATE.SetReadOnly(value);

            this.TXT01_PMORDPAY.SetReadOnly(value);
            this.TXT01_PMORDOTPAY.SetReadOnly(value);
            this.TXT01_PMHFOTTIME.SetReadOnly(value);
            this.TXT01_PMWKOTTIME.SetReadOnly(value);
            this.TXT01_PMNTOTTIME.SetReadOnly(value);
            this.TXT01_PMHTOTTIME.SetReadOnly(value);
            this.TXT01_PMGJOTTIME.SetReadOnly(value);
            this.TXT01_PMHFAMOUNT.SetReadOnly(value);
            this.TXT01_PMOTAMOUNT.SetReadOnly(value);
            this.TXT01_PMNTAMOUNT.SetReadOnly(value);
            this.TXT01_PMHTAMOUNT.SetReadOnly(value);
            this.TXT01_PMGJAMOUNT.SetReadOnly(value);
            this.CBH01_PMEXCD.SetReadOnly(value);
            this.DTP01_PMEXSDATE.SetReadOnly(value);
            this.DTP01_PMEXEDATE.SetReadOnly(value);
            this.TXT01_PMEXRATE.SetReadOnly(value);
            this.TXT01_PMEXMEMO.SetReadOnly(value);            
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : FPS91_TY_S_HR_BBJF0761_RowInserted 이벤트
        private void FPS91_TY_S_HR_BBJF0761_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_BBJF0761.SetValue(e.RowIndex, "PWGUBN", fsPMGUBN);
            this.FPS91_TY_S_HR_BBJF0761.SetValue(e.RowIndex, "PWYYMM", fsPMYYMM);
            this.FPS91_TY_S_HR_BBJF0761.SetValue(e.RowIndex, "PWJIDATE", fsPMJIDATE);
            this.FPS91_TY_S_HR_BBJF0761.SetValue(e.RowIndex, "PWSABUN", fsPMSABUN);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_BBN9K784", fsPMGUBN, fsPMYYMM, fsPMJIDATE, fsPMSABUN);
            string sSeq = this.DbConnector.ExecuteScalar().ToString();

            this.FPS91_TY_S_HR_BBJF0761.SetValue(e.RowIndex, "PWSEQ", sSeq);
        }
        #endregion
    }
}
