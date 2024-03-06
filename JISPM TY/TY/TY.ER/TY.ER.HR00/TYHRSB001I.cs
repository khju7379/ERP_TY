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
    ///  TY_S_HR_5BHGN178 : 급여결과내역관리
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
    public partial class TYHRSB001I : TYBase
    {
        string fsSMGUBN = string.Empty;
        string fsSMYYMM = string.Empty;
        string fsSMJIDATE = string.Empty;
        string fsSMSABUN = string.Empty;

        #region Description : 페이지 로드
        public TYHRSB001I(string sSMJIDATE, string sSMSABUN)
        {
            InitializeComponent();

            fsSMJIDATE = sSMJIDATE;
            fsSMSABUN  = sSMSABUN;

            CBH01_SMTEAM.DummyValue  = sSMJIDATE;
            CBH01_SMBUSEO.DummyValue = sSMJIDATE;
            
            //개인급여기준관리
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_5BHGN178, "SSPAYCODE", "SSPAYCODENM", "SSPAYCODE");
        }

        private void TYHRSB001I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_5BHGN178, "JIGUBN", "SSPAYCODE", "SSPAYCODENM");

            UP_FieldLock(true);

            this.FPS91_TY_S_HR_5BHGN178.Initialize();

            UP_Master_DataBinding();

            UP_PyDataCheck();
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            int i = 0;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 내역 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_5BJAZ191", fsSMGUBN,
                                                            fsSMYYMM,
                                                            fsSMJIDATE,
                                                            fsSMSABUN,
                                                            ds.Tables[0].Rows[i]["SSPAYCODE"].ToString(),
                                                            ds.Tables[0].Rows[i]["SSSTAMOUNT"].ToString(),   // 기준금액
                                                            ds.Tables[0].Rows[i]["SSPAYAMOUNT"].ToString(),  // 지급금액
                                                            ds.Tables[0].Rows[i]["SSPAYGUBN"].ToString(),    // 실지급처리유무
                                                            ds.Tables[0].Rows[i]["SSBIGO"].ToString(),       // 비고
                                                            TYUserInfo.EmpNo
                                                            );

                this.DbConnector.ExecuteTranQueryList();
            }


            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 내역 수정
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_5BJBK196", ds.Tables[1].Rows[i]["SSSTAMOUNT"].ToString(),   // 기준금액
                                                            ds.Tables[1].Rows[i]["SSPAYAMOUNT"].ToString(),  // 지급금액
                                                            ds.Tables[1].Rows[i]["SSPAYGUBN"].ToString(),    // 실지급처리유무
                                                            ds.Tables[1].Rows[i]["SSBIGO"].ToString(),       // 비고
                                                            TYUserInfo.EmpNo,
                                                            fsSMGUBN,
                                                            fsSMYYMM,
                                                            fsSMJIDATE,
                                                            fsSMSABUN,
                                                            ds.Tables[1].Rows[i]["SSPAYCODE"].ToString()
                                                            );

                this.DbConnector.ExecuteTranQueryList();
            }


            // 소급집계 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5BJAX189", fsSMJIDATE.Substring(0,6),   // 소급년월
                                                        fsSMJIDATE,                  // 소급급여일자
                                                        fsSMSABUN,                   // 사번
                                                        fsSMGUBN,                    // 급여구분
                                                        fsSMYYMM                     // 급여년월
                                                        );
            this.DbConnector.ExecuteTranQueryList();

            double dPSPAYAMOUNT = 0; // 기준금액
            double dSSPAYAMOUNT = 0; // 지급금액
            double dSTSOAMOUNT  = 0; // 소급금액

            for (i = 0; i < this.FPS91_TY_S_HR_5BHGN178.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_HR_5BHGN178.GetValue(i, "SSPAYCODENM").ToString() != "[소  계]") // 소계가 아닐 경우
                {
                    // 기준금액
                    dPSPAYAMOUNT = double.Parse(Get_Numeric(this.FPS91_TY_S_HR_5BHGN178.GetValue(i, "PSPAYAMOUNT").ToString()));
                    // 지급금액
                    dSSPAYAMOUNT = double.Parse(Get_Numeric(this.FPS91_TY_S_HR_5BHGN178.GetValue(i, "SSPAYAMOUNT").ToString()));
                    // 소급금액
                    dSTSOAMOUNT = dSTSOAMOUNT + (dSSPAYAMOUNT - dPSPAYAMOUNT);
                }
            }

            // 소급집계 등록
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5BJAZ190", fsSMJIDATE.Substring(0, 6),    // 소급년월
                                                        fsSMJIDATE,                    // 소급급여일자
                                                        fsSMSABUN,                     // 사번
                                                        fsSMGUBN,                      // 급여구분
                                                        fsSMYYMM,                      // 급여년월
                                                        Convert.ToString(dSTSOAMOUNT), // 소급금액
                                                        "",                            // 비고
                                                        TYUserInfo.EmpNo               // 작업사번
                                                        );
            this.DbConnector.ExecuteTranQueryList();


            double dSMORDPAY   = 0;
            double dSMORDOTPAY = 0;
            double dSMPAYTOTAL = 0;

            dSSPAYAMOUNT = 0;

            // 지급내역 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5BJDO198", fsSMGUBN, fsSMYYMM, fsSMJIDATE, fsSMSABUN, "");

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    dSSPAYAMOUNT = double.Parse(Get_Numeric(dt.Rows[i]["SSPAYAMOUNT"].ToString()));

                    if (fsSMGUBN == "S1") // 상여
                    {
                        dSMORDPAY = dSMORDPAY + dSSPAYAMOUNT;
                    }
                    else                  // 급여
                    {
                        if (dt.Rows[i]["SSPAYCODE"].ToString() == "1601") // 상여
                        {
                            dSMORDOTPAY = dSSPAYAMOUNT;
                        }
                        else                                               // 급여
                        {
                            dSMORDPAY = dSMORDPAY + dSSPAYAMOUNT;
                        }
                    }
                }

                dSMPAYTOTAL = dSMORDPAY + dSMORDOTPAY;

                // 마스터 업데이트
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_5BJB7192", Convert.ToString(dSMORDPAY),    // 통상임금
                                                            Convert.ToString(dSMORDOTPAY),  // OT상여금액
                                                            Convert.ToString(dSMPAYTOTAL),  // 지급합계
                                                            Convert.ToString(dSMPAYTOTAL),  // 차인지급액
                                                            TYUserInfo.EmpNo,               // 작업사번
                                                            fsSMGUBN,                       // 급여구분
                                                            fsSMYYMM,                       // 급여년월
                                                            fsSMJIDATE,                     // 지급일자
                                                            fsSMSABUN                       // 사번
                                                            );

                this.DbConnector.ExecuteTranQueryList();            
            }

            UP_Master_DataBinding();
            UP_Detail_DataBinding(fsSMGUBN, fsSMYYMM, fsSMJIDATE, fsSMSABUN);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            int i = 0;

            this.DbConnector.CommandClear();
            for (i = 0; i < dt.Rows.Count; i++)
            {
                // 소급 내역 삭제
                this.DbConnector.Attach("TY_P_HR_5BJAX188", dt.Rows[i]["SSGUBN"].ToString(),
                                                            dt.Rows[i]["SSYYMM"].ToString(),
                                                            dt.Rows[i]["SSJIDATE"].ToString(),
                                                            dt.Rows[i]["SSSABUN"].ToString(),
                                                            dt.Rows[i]["SSPAYCODE"].ToString()
                                                            );

                fsSMGUBN   = dt.Rows[i]["SSGUBN"].ToString();
                fsSMYYMM   = dt.Rows[i]["SSYYMM"].ToString();
                fsSMJIDATE = dt.Rows[i]["SSJIDATE"].ToString();
                fsSMSABUN  = dt.Rows[i]["SSSABUN"].ToString();
            }
            this.DbConnector.ExecuteTranQueryList();

            UP_Detail_DataBinding(fsSMGUBN, fsSMYYMM, fsSMJIDATE, fsSMSABUN);

            // 소급집계 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5BJAX189", fsSMJIDATE.Substring(0, 6),   // 소급년월
                                                        fsSMJIDATE,                   // 소급급여일자
                                                        fsSMSABUN,                    // 사번
                                                        fsSMGUBN,                     // 급여구분
                                                        fsSMYYMM                      // 급여년월
                                                        );
            this.DbConnector.ExecuteTranQueryList();

            // 지급내역 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5BJDO198", fsSMGUBN, fsSMYYMM, fsSMJIDATE, fsSMSABUN, "");

            DataTable dtSO = this.DbConnector.ExecuteDataTable();

            if (dtSO.Rows.Count > 0)
            {
                double dPSPAYAMOUNT = 0; // 기준금액
                double dSSPAYAMOUNT = 0; // 지급금액
                double dSTSOAMOUNT = 0;  // 소급금액

                for (i = 0; i < this.FPS91_TY_S_HR_5BHGN178.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_HR_5BHGN178.GetValue(i, "SSPAYCODENM").ToString() != "[소  계]") // 소계가 아닐 경우
                    {
                        // 기준금액
                        dPSPAYAMOUNT = double.Parse(Get_Numeric(this.FPS91_TY_S_HR_5BHGN178.GetValue(i, "PSPAYAMOUNT").ToString()));
                        // 지급금액
                        dSSPAYAMOUNT = double.Parse(Get_Numeric(this.FPS91_TY_S_HR_5BHGN178.GetValue(i, "SSPAYAMOUNT").ToString()));
                        // 소급금액
                        dSTSOAMOUNT = dSTSOAMOUNT + (dSSPAYAMOUNT - dPSPAYAMOUNT);
                    }
                }

                // 소급집계 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_5BJAZ190", fsSMJIDATE.Substring(0, 6),    // 소급년월
                                                            fsSMJIDATE,                    // 소급급여일자
                                                            fsSMSABUN,                     // 사번
                                                            fsSMGUBN,                      // 급여구분
                                                            fsSMYYMM,                      // 급여년월
                                                            Convert.ToString(dSTSOAMOUNT), // 소급금액
                                                            "",                            // 비고
                                                            TYUserInfo.EmpNo               // 작업사번
                                                            );
                this.DbConnector.ExecuteTranQueryList();

                double dSMORDPAY   = 0;
                double dSMORDOTPAY = 0;
                double dSMPAYTOTAL = 0;

                dSSPAYAMOUNT       = 0;

                for (i = 0; i < dtSO.Rows.Count; i++)
                {
                    dSSPAYAMOUNT = double.Parse(Get_Numeric(dtSO.Rows[i]["SSPAYAMOUNT"].ToString()));

                    if (fsSMGUBN == "S1") // 상여
                    {
                        dSMORDPAY = dSMORDPAY + dSSPAYAMOUNT;
                    }
                    else                  // 급여
                    {
                        if (dtSO.Rows[i]["SSPAYCODE"].ToString() == "1601") // 상여
                        {
                            dSMORDOTPAY = dSSPAYAMOUNT;
                        }
                        else                                               // 급여
                        {
                            dSMORDPAY = dSMORDPAY + dSSPAYAMOUNT;
                        }
                    }
                }

                dSMPAYTOTAL = dSMORDPAY + dSMORDOTPAY;

                // 마스터 업데이트
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_5BJB7192", Convert.ToString(dSMORDPAY),    // 통상임금
                                                            Convert.ToString(dSMORDOTPAY),  // OT상여금액
                                                            Convert.ToString(dSMPAYTOTAL),  // 지급합계
                                                            Convert.ToString(dSMPAYTOTAL),  // 차인지급액
                                                            TYUserInfo.EmpNo,               // 작업사번
                                                            fsSMGUBN,                       // 급여구분
                                                            fsSMYYMM,                       // 급여년월
                                                            fsSMJIDATE,                     // 지급일자
                                                            fsSMSABUN                       // 사번
                                                            );

                this.DbConnector.ExecuteTranQueryList();
            }
            else
            {
                // 소급 결과 마스터 삭제
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_5BJE1199", fsSMGUBN, fsSMYYMM, fsSMJIDATE, fsSMSABUN);
                this.DbConnector.ExecuteTranQueryList();
            }

            UP_Master_DataBinding();
            UP_Detail_DataBinding(fsSMGUBN, fsSMYYMM, fsSMJIDATE, fsSMSABUN);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;
            int j = 0;

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_5BHGN178.GetDataSourceInclude(TSpread.TActionType.New,    "SSGUBN", "SSYYMM", "SSJIDATE", "SSSABUN", "SSPAYCODE", "SSSTAMOUNT", "SSPAYAMOUNT", "SSPAYGUBN", "SSBIGO"));
            ds.Tables.Add(this.FPS91_TY_S_HR_5BHGN178.GetDataSourceInclude(TSpread.TActionType.Update, "SSGUBN", "SSYYMM", "SSJIDATE", "SSSABUN", "SSPAYCODE", "SSSTAMOUNT", "SSPAYAMOUNT", "SSPAYGUBN", "SSBIGO"));

            // 등록
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 동일 자료 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_5BJDO198", ds.Tables[0].Rows[i]["SSGUBN"].ToString(),
                                                            ds.Tables[0].Rows[i]["SSYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["SSJIDATE"].ToString(),
                                                            ds.Tables[0].Rows[i]["SSSABUN"].ToString(),
                                                            ds.Tables[0].Rows[i]["SSPAYCODE"].ToString());

                dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_387AG357");
                    e.Successed = false;
                    return;
                }

                // 급여 생성 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_5BHB0172", ds.Tables[0].Rows[i]["SSJIDATE"].ToString(),
                                                            ds.Tables[0].Rows[i]["SSSABUN"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_HR_5BBHO142");
                    e.Successed = false;
                    return;
                }

                for (j = 0; j < ds.Tables[0].Rows.Count; j++)
                {
                    if (i != j)
                    {
                        if (ds.Tables[0].Rows[i]["SSGUBN"].ToString() == ds.Tables[0].Rows[j]["SSGUBN"].ToString() && ds.Tables[0].Rows[i]["SSYYMM"].ToString() == ds.Tables[0].Rows[j]["SSYYMM"].ToString() &&
                            ds.Tables[0].Rows[i]["SSJIDATE"].ToString() == ds.Tables[0].Rows[j]["SSJIDATE"].ToString() && ds.Tables[0].Rows[i]["SSSABUN"].ToString() == ds.Tables[0].Rows[j]["SSSABUN"].ToString() &&
                            ds.Tables[0].Rows[i]["SSPAYCODE"].ToString() == ds.Tables[0].Rows[j]["SSPAYCODE"].ToString())
                        {
                            this.ShowMessage("TY_M_AC_387AG357");
                            e.Successed = false;
                            return;
                        }
                    }
                }
            }



            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 급여 생성 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_5BHB0172", ds.Tables[1].Rows[i]["SSJIDATE"].ToString(),
                                                            ds.Tables[1].Rows[i]["SSSABUN"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_HR_5BBHO142");
                    e.Successed = false;
                    return;
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
            DataTable dt = this.FPS91_TY_S_HR_5BHGN178.GetDataSourceInclude(TSpread.TActionType.Remove, "SSGUBN", "SSYYMM", "SSJIDATE", "SSSABUN", "SSPAYCODE", "SSPAYAMOUNT");

            DataTable dt1 = new DataTable();

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_5BHB0172", dt.Rows[i]["SSJIDATE"].ToString(),
                                                                dt.Rows[i]["SSSABUN"].ToString());

                    dt1 = this.DbConnector.ExecuteDataTable();

                    if (dt1.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_HR_5BBHO142");
                        e.Successed = false;
                        return;
                    }

                    if (dt.Rows[i]["SSGUBN"].ToString() == "")
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

            e.ArgData = dt;

        }
        #endregion

        

        #region Description : 소급결과 마스타 조회
        private void UP_Master_DataBinding()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5BHFH173", fsSMJIDATE, fsSMSABUN);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_5BHG1176.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_HR_5BHG1176.ActiveSheet.RowCount; i++)
                {
                    this.FPS91_TY_S_HR_5BHG1176.ActiveSheet.Columns[12].BackColor = Color.LightBlue;
                    this.FPS91_TY_S_HR_5BHG1176.ActiveSheet.Columns[12].Font = new Font("굴림", 9, FontStyle.Bold);
                }
            }
        }
        #endregion

        #region Description : 소급결과 내역 조회
        private void UP_Detail_DataBinding(string sSSGUBN, string sSSYYMM, string sSSJIDATE, string sSSSABUN)
        {
            fsSMGUBN = sSSGUBN.ToString();
            fsSMYYMM = sSSYYMM.ToString();

            this.FPS91_TY_S_HR_5BHGN178.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5BHH0179", sSSGUBN, sSSYYMM, sSSJIDATE, sSSSABUN);

            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_HR_5BHGN178.SetValue(UP_DatatableChange(dt));

                for (int i = 0; i < this.FPS91_TY_S_HR_5BHGN178.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_HR_5BHGN178.GetValue(i, "SSPAYCODE").ToString() == "1700") //정기상여금
                    {
                        this.FPS91_TY_S_HR_5BHGN178_Sheet1.Rows[i].Locked = true;
                    }
                    if (this.FPS91_TY_S_HR_5BHGN178.GetValue(i, "SSPAYCODENM").ToString() == "[소  계]")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_HR_5BHGN178_Sheet1.Rows[i].Locked = true;
                        this.FPS91_TY_S_HR_5BHGN178.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 244);
                    }
                }

                this.CBH01_SMGUBN.SetValue(dt.Rows[0]["SMGUBN"].ToString());
                this.DTP01_SMYYMM.SetValue(dt.Rows[0]["SMYYMM"].ToString());
                this.DTP01_SMJIDATE.SetValue(dt.Rows[0]["SMJIDATE"].ToString());
                this.CBH01_SMSABUN.SetValue(dt.Rows[0]["SMSABUN"].ToString());
                this.CBH01_SMJKCD.SetValue(dt.Rows[0]["SMJKCD"].ToString());
                this.TXT01_SMHOBN.SetValue(dt.Rows[0]["SMHOBN"].ToString());
                this.CBH01_SMTEAM.SetValue(dt.Rows[0]["SMTEAM"].ToString());
                this.CBH01_SMBUSEO.SetValue(dt.Rows[0]["SMBUSEO"].ToString());
                this.DTP01_SMWKSDATE.SetValue(dt.Rows[0]["SMWKSDATE"].ToString());
                this.DTP01_SMWKEDATE.SetValue(dt.Rows[0]["SMWKEDATE"].ToString());
                this.TXT01_SMORDPAY.SetValue(dt.Rows[0]["SMORDPAY"].ToString());
                this.TXT01_SMORDOTPAY.SetValue(dt.Rows[0]["SMORDOTPAY"].ToString());
                this.TXT01_SMHFOTTIME.SetValue(dt.Rows[0]["SMHFOTTIME"].ToString());
                this.TXT01_SMWKOTTIME.SetValue(dt.Rows[0]["SMWKOTTIME"].ToString());
                this.TXT01_SMNTOTTIME.SetValue(dt.Rows[0]["SMNTOTTIME"].ToString());
                this.TXT01_SMHTOTTIME.SetValue(dt.Rows[0]["SMHTOTTIME"].ToString());
                this.TXT01_SMHFAMOUNT.SetValue(dt.Rows[0]["SMHFAMOUNT"].ToString());
                this.TXT01_SMOTAMOUNT.SetValue(dt.Rows[0]["SMOTAMOUNT"].ToString());
                this.TXT01_SMNTAMOUNT.SetValue(dt.Rows[0]["SMNTAMOUNT"].ToString());
                this.TXT01_SMHTAMOUNT.SetValue(dt.Rows[0]["SMHTAMOUNT"].ToString());
                this.TXT01_SMWTAMOUNT.SetValue(dt.Rows[0]["SMWTAMOUNT"].ToString());
                this.TXT01_SMWTAMOUNT.SetValue(dt.Rows[0]["SMWTAMOUNT"].ToString());
                this.TXT01_SMGJOTTIME.SetValue(dt.Rows[0]["SMGJOTTIME"].ToString());
                this.TXT01_SMGJAMOUNT.SetValue(dt.Rows[0]["SMGJAMOUNT"].ToString());

                for (int i = 0; i < this.FPS91_TY_S_HR_5BHGN178.ActiveSheet.RowCount; i++)
                {
                    this.FPS91_TY_S_HR_5BHGN178.ActiveSheet.Columns[10].BackColor = Color.LightBlue;
                    this.FPS91_TY_S_HR_5BHGN178.ActiveSheet.Columns[10].Font      = new Font("굴림", 9, FontStyle.Bold);
                }

                UP_Auto_Compute();
            }
            else
            {
                this.FPS91_TY_S_HR_5BHGN178.SetValue(dt);
            }
        }
        #endregion

        #region Description : 특정 Row와 Column 값 변경
        private void UP_Auto_Compute()
        {
            this.SpreadSumRowAdd(this.FPS91_TY_S_HR_5BHGN178, "TITLE1", "합 계", Color.LightSkyBlue);

            for (int i = 0; i < this.FPS91_TY_S_HR_5BHGN178.ActiveSheet.RowCount; i++)
            {
                // 차인지급액
                this.FPS91_TY_S_HR_5BHGN178_Sheet1.SetFormula(
                    i,
                    10,
                    "R[0]C[-1] - R[0]C[-2]");
            }

            this.FPS91_TY_S_HR_5BHGN178.ActiveSheet.Rows[FPS91_TY_S_HR_5BHGN178.CurrentRowCount - 1].Visible = false;
        }
        #endregion

        #region Description : 행 추가 이벤트
        private void FPS91_TY_S_HR_5BHGN178_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_5BHGN178.SetValue(e.RowIndex, "SSGUBN", this.CBH01_SMGUBN.GetValue().ToString());
            this.FPS91_TY_S_HR_5BHGN178.SetValue(e.RowIndex, "SSYYMM", this.DTP01_SMYYMM.GetString().Substring(0,6));
            this.FPS91_TY_S_HR_5BHGN178.SetValue(e.RowIndex, "SSJIDATE", this.DTP01_SMJIDATE.GetString());
            this.FPS91_TY_S_HR_5BHGN178.SetValue(e.RowIndex, "SSSABUN", this.CBH01_SMSABUN.GetValue().ToString());
        }
        #endregion

        #region Description : 그리드 소계/합계 추가
        private DataTable UP_DatatableChange(DataTable dt)
        {
            DataTable rtnDt = new DataTable();

            DataRow row;

            double dSSSTAMOUNT1_HAP = 0;

            double dPSPAYAMOUNT_HAP  = 0;
            double dSSPAYAMOUNT1_HAP = 0;
            double dSSPAYAMOUNT2_HAP = 0;

            double dSOGUBAMT_HAP = 0;

            rtnDt.Columns.Add("SSGUBN",      typeof(System.String));
            rtnDt.Columns.Add("SSYYMM",      typeof(System.String));
            rtnDt.Columns.Add("SSJIDATE",    typeof(System.String));
            rtnDt.Columns.Add("SSSABUN",     typeof(System.String));
            rtnDt.Columns.Add("JIGUBN",      typeof(System.String));
            rtnDt.Columns.Add("SSPAYCODE",   typeof(System.String));
            rtnDt.Columns.Add("SSPAYCODENM", typeof(System.String));
            rtnDt.Columns.Add("SSSTAMOUNT",  typeof(System.String));
            rtnDt.Columns.Add("PSPAYAMOUNT", typeof(System.String));
            rtnDt.Columns.Add("SSPAYAMOUNT", typeof(System.String));
            rtnDt.Columns.Add("SSPAYGUBN",   typeof(System.String));
            rtnDt.Columns.Add("SOGUBAMT",    typeof(System.String));
            rtnDt.Columns.Add("SSBIGO",      typeof(System.String));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (i > 0)
                {
                    if (dt.Rows[i - 1]["JIGUBN"].ToString() != dt.Rows[i]["JIGUBN"].ToString())
                    {
                        row = rtnDt.NewRow();

                        row["SSGUBN"]      = DBNull.Value;
                        row["SSYYMM"]      = DBNull.Value;
                        row["SSJIDATE"]    = DBNull.Value;
                        row["SSSABUN"]     = DBNull.Value;
                        row["JIGUBN"]      = DBNull.Value;
                        row["SSPAYCODE"]   = DBNull.Value;
                        row["SSPAYCODENM"] = "[소  계]";
                        row["SSSTAMOUNT"]  = string.Format("{0:#,##0.0}", dSSSTAMOUNT1_HAP);
                        row["PSPAYAMOUNT"] = string.Format("{0:#,##0.0}", dPSPAYAMOUNT_HAP);
                        row["SSPAYAMOUNT"] = string.Format("{0:#,##0.0}", dSSPAYAMOUNT1_HAP);
                        row["SOGUBAMT"]    = string.Format("{0:#,##0.0}", dSOGUBAMT_HAP);
                        row["SSPAYGUBN"]   = DBNull.Value;
                        row["SSBIGO"]      = DBNull.Value;

                        rtnDt.Rows.Add(row);


                        dSSPAYAMOUNT2_HAP  = dSSPAYAMOUNT1_HAP;
                            
                        dSSSTAMOUNT1_HAP   = double.Parse(dt.Rows[i]["SSSTAMOUNT"].ToString());
                        dPSPAYAMOUNT_HAP   = double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString());
                        dSSPAYAMOUNT1_HAP  = double.Parse(dt.Rows[i]["SSPAYAMOUNT"].ToString());

                        dSOGUBAMT_HAP      = double.Parse(dt.Rows[i]["SOGUBAMT"].ToString());
                    }
                    else
                    {
                        dSSSTAMOUNT1_HAP   = dSSSTAMOUNT1_HAP + double.Parse(dt.Rows[i]["SSSTAMOUNT"].ToString());
                        dPSPAYAMOUNT_HAP   = dPSPAYAMOUNT_HAP + double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString());
                        dSSPAYAMOUNT1_HAP  = dSSPAYAMOUNT1_HAP + double.Parse(dt.Rows[i]["SSPAYAMOUNT"].ToString());

                        dSOGUBAMT_HAP      = dSOGUBAMT_HAP + double.Parse(dt.Rows[i]["SOGUBAMT"].ToString());
                    }
                }
                else
                {
                    dSSSTAMOUNT1_HAP  = double.Parse(dt.Rows[i]["SSSTAMOUNT"].ToString());
                    dPSPAYAMOUNT_HAP  = double.Parse(dt.Rows[i]["PSPAYAMOUNT"].ToString());
                    dSSPAYAMOUNT1_HAP = double.Parse(dt.Rows[i]["SSPAYAMOUNT"].ToString());

                    dSOGUBAMT_HAP      = double.Parse(dt.Rows[i]["SOGUBAMT"].ToString());
                }

                row = rtnDt.NewRow();

                row["SSGUBN"]      = dt.Rows[i]["SSGUBN"].ToString();
                row["SSYYMM"]      = dt.Rows[i]["SSYYMM"].ToString();
                row["SSJIDATE"]    = dt.Rows[i]["SSJIDATE"].ToString();
                row["SSSABUN"]     = dt.Rows[i]["SSSABUN"].ToString();
                row["JIGUBN"]      = dt.Rows[i]["JIGUBN"].ToString();
                row["SSPAYCODE"]   = dt.Rows[i]["SSPAYCODE"].ToString();
                row["SSPAYCODENM"] = dt.Rows[i]["SSPAYCODENM"].ToString();
                row["SSSTAMOUNT"]  = dt.Rows[i]["SSSTAMOUNT"].ToString();
                row["PSPAYAMOUNT"] = dt.Rows[i]["PSPAYAMOUNT"].ToString();
                row["SSPAYAMOUNT"] = dt.Rows[i]["SSPAYAMOUNT"].ToString();
                row["SSPAYGUBN"]   = dt.Rows[i]["SSPAYGUBN"].ToString();
                row["SOGUBAMT"]    = dt.Rows[i]["SOGUBAMT"].ToString();
                row["SSBIGO"]      = dt.Rows[i]["SSBIGO"].ToString();

                rtnDt.Rows.Add(row);
            }

            row = rtnDt.NewRow();

            row["SSGUBN"]      = DBNull.Value;
            row["SSYYMM"]      = DBNull.Value;
            row["SSJIDATE"]    = DBNull.Value;
            row["SSSABUN"]     = DBNull.Value;
            row["JIGUBN"]      = DBNull.Value;
            row["SSPAYCODE"]   = DBNull.Value;
            row["SSPAYCODENM"] = "[소  계]";
            row["SSSTAMOUNT"]  = string.Format("{0:#,##0.0}", dSSSTAMOUNT1_HAP);
            row["PSPAYAMOUNT"] = string.Format("{0:#,##0.0}", dPSPAYAMOUNT_HAP);
            row["SSPAYAMOUNT"] = string.Format("{0:#,##0.0}", dSSPAYAMOUNT1_HAP);
            row["SOGUBAMT"]    = string.Format("{0:#,##0.0}", dSOGUBAMT_HAP);
            row["SSPAYGUBN"]   = DBNull.Value;
            row["SSBIGO"]      = DBNull.Value;

            rtnDt.Rows.Add(row);

            //row = rtnDt.NewRow();

            //row["SSGUBN"]      = DBNull.Value;
            //row["SSYYMM"]      = DBNull.Value;
            //row["SSJIDATE"]    = DBNull.Value;
            //row["SSSABUN"]     = DBNull.Value;
            //row["JIGUBN"]      = "[차인지급액]";
            //row["SSPAYCODE"]   = DBNull.Value;
            //row["SSPAYCODENM"] = DBNull.Value;
            //row["SSSTAMOUNT"]  = DBNull.Value;

            //row["PSPAYAMOUNT"] = string.Format("{0:#,##0.0}", dPSPAYAMOUNT_HAP);
            
            //if (dSSPAYAMOUNT2_HAP > 0)
            //{
            //    row["SSPAYAMOUNT"] = string.Format("{0:#,##0.0}", dSSPAYAMOUNT2_HAP - dSSPAYAMOUNT1_HAP);
            //}
            //else
            //{
            //    row["SSPAYAMOUNT"] = string.Format("{0:#,##0.0}", dSSPAYAMOUNT1_HAP);
            //}

            //row["SOGUBAMT"]  = string.Format("{0:#,##0.0}", dSOGUBAMT_HAP);

            //row["SSPAYGUBN"] = DBNull.Value;
            //row["SSBIGO"]    = DBNull.Value;

            //rtnDt.Rows.Add(row);

            return rtnDt;
        }
        #endregion

        #region Description : 급여코드 코드박스 팝업
        private void FPS91_TY_S_HR_5BHGN178_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1 && FPS91_TY_S_HR_5BHGN178.ActiveSheet.ActiveColumnIndex == 5)
            {
                TYHRPY08C1 popup = new TYHRPY08C1(this.FPS91_TY_S_HR_5BHGN178.GetValue("SSJIDATE").ToString(), this.FPS91_TY_S_HR_5BHGN178.GetValue("SSSABUN").ToString(), this.FPS91_TY_S_HR_5BHGN178.GetValue("SSGUBN").ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    FPS91_TY_S_HR_5BHGN178.SetValue(FPS91_TY_S_HR_5BHGN178.ActiveSheet.ActiveRowIndex, "SSPAYCODE", popup.fscode);

                    FPS91_TY_S_HR_5BHGN178.SetValue(FPS91_TY_S_HR_5BHGN178.ActiveSheet.ActiveRowIndex, "SSPAYCODENM", popup.fsname);

                    FPS91_TY_S_HR_5BHGN178.SetValue(FPS91_TY_S_HR_5BHGN178.ActiveSheet.ActiveRowIndex, "SSSTAMOUNT", popup.fsamt);
                }
            }
        }
        #endregion

        #region Description : 급여 생성 체크
        private void UP_PyDataCheck()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5BHB0172", fsSMJIDATE, fsSMSABUN);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.BTN61_SAV.SetReadOnly(true);
                this.BTN61_REM.SetReadOnly(true); 
            }
        }
        #endregion

        #region Description : 필드 Lock
        private void UP_FieldLock(bool value)
        {
            this.CBH01_SMGUBN.SetReadOnly(value);
            this.DTP01_SMYYMM.SetReadOnly(value);
            this.DTP01_SMJIDATE.SetReadOnly(value);
            this.CBH01_SMSABUN.SetReadOnly(value);
            this.CBH01_SMJKCD.SetReadOnly(value);
            this.TXT01_SMHOBN.SetReadOnly(value);
            this.CBH01_SMTEAM.SetReadOnly(value);
            this.CBH01_SMBUSEO.SetReadOnly(value);
            this.DTP01_SMWKSDATE.SetReadOnly(value);
            this.DTP01_SMWKEDATE.SetReadOnly(value);
            this.TXT01_SMPAYRATE.SetReadOnly(value);

            this.TXT01_SMORDPAY.SetReadOnly(value);
            this.TXT01_SMORDOTPAY.SetReadOnly(value);
            this.TXT01_SMHFOTTIME.SetReadOnly(value);
            this.TXT01_SMWKOTTIME.SetReadOnly(value);
            this.TXT01_SMNTOTTIME.SetReadOnly(value);
            this.TXT01_SMHTOTTIME.SetReadOnly(value);
            this.TXT01_SMHFAMOUNT.SetReadOnly(value);
            this.TXT01_SMOTAMOUNT.SetReadOnly(value);
            this.TXT01_SMNTAMOUNT.SetReadOnly(value);
            this.TXT01_SMHTAMOUNT.SetReadOnly(value);
        }
        #endregion

        private void FPS91_TY_S_HR_5BHG1176_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Detail_DataBinding(this.FPS91_TY_S_HR_5BHG1176.GetValue("SMGUBN").ToString(),
                                  this.FPS91_TY_S_HR_5BHG1176.GetValue("SMYYMM").ToString(),
                                  this.FPS91_TY_S_HR_5BHG1176.GetValue("SMJIDATE").ToString(),
                                  this.FPS91_TY_S_HR_5BHG1176.GetValue("SMSABUN").ToString()
                                  );
        }
    }
}
