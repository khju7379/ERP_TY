using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using DataDynamics.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.ER.GB00;
using TY.ER.AC00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 선급자재 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.19 09:59    
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_32J79125 : 선급자재 미생성 조회
    ///  TY_P_MR_32J7A126 : 선급자재 생성 조회
    ///  TY_P_MR_32J7A127 : 선급자재 DETAIL 조회
    ///  TY_P_MR_32J7A128 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_32J7C129 : 선급자재 생성 조회
    ///  TY_S_MR_32J7M130 : 선급자재 DETAIL 조회
    ///  TY_S_UT_71VAB604 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CANCEL : 취소
    ///  CREATE : 생성
    ///  INQ : 조회
    ///  JASAN_CRE : 자산생성
    ///  JASAN_DEL : 자산삭제
    ///  JPNO_CRE : 전표생성
    ///  JPNO_DEL : 전표삭제
    ///  FXDDPMK : 귀속부서
    ///  FXDSAUP : 선급사업부
    ///  FXDGETDATE : 취득일
    ///  GCDACGHAP : 계정총액
    ///  GDAESANGHAP : 대상총액
    ///  GJANGHAP : 잔액
    /// </summary>
    public partial class TYUTME036B : TYBase
    {
        double fdAMOUNT = 0;

        private string fsIPHANG   = string.Empty;
        private string fsBONSUN   = string.Empty;
        private string fsHWAJU    = string.Empty;
        private string fsHJDESC1  = string.Empty;
        private string fsHWAMUL   = string.Empty;
        private string fsHMDESC1  = string.Empty;
        private string fsTANKNO   = string.Empty;
        private string fsBLNO     = string.Empty;
        private string fsMSNSEQ   = string.Empty;
        private string fsHSNSEQ   = string.Empty;
        private string fsCUSTIL   = string.Empty;
        private string fsCHASU    = string.Empty;

        private string fsCMHWAPE  = string.Empty;
        private string fsCMFACT   = string.Empty;

        private string fsSVBIJUNG = string.Empty;
        private string fsSVMOGB   = string.Empty;
        private string fsSHOREQTY = string.Empty;

        private string fsCOCONTNO = string.Empty;
        private string fsCOOVAM   = string.Empty;

        private string fsIPSINOYY = string.Empty;
        private string fsIPSINO   = string.Empty;

        private string fsSEQCH    = string.Empty;
        private string fsSEQGB    = string.Empty;


        #region Description : 페이지 로드
        public TYUTME036B()
        {
            InitializeComponent();
        }

        private void TYUTME036B_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_UT_7BL9Z065.Sheets[0].Columns[17].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_UT_7BL9Z065, "BTN");
            (this.FPS91_TY_S_UT_7BL9Z065.Sheets[0].Columns[18].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_UT_7BL9Z065, "BTN");

            this.BTN61_JPNO_CRE.ProcessCheck += new TButton.CheckHandler(BTN61_JPNO_CRE_ProcessCheck);
            this.BTN61_JUNPYO_CANCEL.ProcessCheck += new TButton.CheckHandler(BTN61_JUNPYO_CANCEL_ProcessCheck);
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.FPS91_TY_S_UT_7BL9Z065.Initialize();
            this.FPS91_TY_S_UT_7BL9V064.Initialize();

            this.DTP01_STDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyyMMdd"));
            this.CBO01_INQOPTION.SetValue("N");

            // 조회
            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            double dMI_AMOUNT  = 0;
            double dMI_COUNT   = 0;
            double dGAMOUNT    = 0;
            double dREAD_COUNT = 0;

            this.TXT01_MI_AMOUNT.SetValue("");
            this.TXT01_MI_COUNT.SetValue("");

            this.TXT01_GAMOUNT.SetValue("");
            this.TXT01_READ_COUNT.SetValue("");

            fdAMOUNT = 0;

            if (this.CBO01_GGUBUN.GetValue().ToString() == "U")      // 미발행
            {
                this.BTN61_JPNO_CRE.Visible = true;
                this.BTN61_JUNPYO_CANCEL.Visible = false;
            }
            else if (this.CBO01_GGUBUN.GetValue().ToString() == "P") // 발행
            {
                this.BTN61_JPNO_CRE.Visible = false;
                this.BTN61_JUNPYO_CANCEL.Visible = true;
            }
            else // 전체
            {
                this.BTN61_JPNO_CRE.Visible = false;
                this.BTN61_JUNPYO_CANCEL.Visible = false;
            }

            string sProcedure = string.Empty;

            if (this.CBO01_GGUBUN.GetValue().ToString() == "U")      // 전표 미발행
            {
                sProcedure = "TY_P_UT_7BL9D062";
            }
            else if (this.CBO01_GGUBUN.GetValue().ToString() == "P") // 전표 발행
            {
                sProcedure = "TY_P_UT_7BNCS104";
            }
            
            DataTable dt = new DataTable();

            this.FPS91_TY_S_UT_7BL9Z065.Initialize();
            this.FPS91_TY_S_UT_7BL9V064.Initialize();

            this.DbConnector.CommandClear();

            if (this.CBO01_GGUBUN.GetValue().ToString() == "U") // 전표 미발행
            {
                this.DbConnector.Attach
                    (
                    "TY_P_UT_C7J9V722",
                    Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                    Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                    this.CBO01_INQOPTION.GetValue().ToString()
                    );

                // 202207 수정전
                //this.DbConnector.Attach
                //    (
                //    "TY_P_UT_7BL9D062",
                //    Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                //    Get_Date(this.DTP01_EDDATE.GetValue().ToString())
                //    );
            }
            else if (this.CBO01_GGUBUN.GetValue().ToString() == "P") // 전표 발행
            {
                this.DbConnector.Attach
                    (
                    "TY_P_UT_BA5GD597",
                    Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                    Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                    Get_Date(this.DTP01_SDATE.GetValue().ToString()),
                    Get_Date(this.DTP01_EDATE.GetValue().ToString())
                    );
            }
            else // 전체
            {
                this.DbConnector.Attach
                    (
                    "TY_P_UT_C7J9Z723",
                    Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                    Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                    this.CBO01_INQOPTION.GetValue().ToString(),
                    Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                    Get_Date(this.DTP01_EDDATE.GetValue().ToString())
                    );

                // 202207 수정전
                //this.DbConnector.Attach
                //    (
                //    "TY_P_UT_7BSFW128",
                //    Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                //    Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                //    Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                //    Get_Date(this.DTP01_EDDATE.GetValue().ToString())
                //    );
            }

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_7BL9Z065.SetValue(dt);

            if (this.CBO01_GGUBUN.GetValue().ToString() == "P" || this.CBO01_GGUBUN.GetValue().ToString() == "A") // 발행 또는 전체
            {
                for (int i = 0; i < FPS91_TY_S_UT_7BL9Z065.CurrentRowCount; i++)
                {
                    if (this.CBO01_GGUBUN.GetValue().ToString() == "A")
                    {
                        if (this.FPS91_TY_S_UT_7BL9Z065.GetValue(i, "HMJPNO").ToString() != "")
                        {
                            dGAMOUNT = dGAMOUNT + double.Parse(String.Format("{0,9:N0}", Get_Numeric(this.FPS91_TY_S_UT_7BL9Z065.GetValue(i, "HMCHARGEAMT").ToString())));
                            dGAMOUNT = dGAMOUNT + double.Parse(String.Format("{0,9:N0}", Get_Numeric(this.FPS91_TY_S_UT_7BL9Z065.GetValue(i, "HMSECHARGEAMT").ToString())));
                            dREAD_COUNT++;
                        }
                        else
                        {
                            dMI_AMOUNT = dMI_AMOUNT + double.Parse(String.Format("{0,9:N0}", Get_Numeric(this.FPS91_TY_S_UT_7BL9Z065.GetValue(i, "HMCHARGEAMT").ToString())));
                            dMI_AMOUNT = dMI_AMOUNT + double.Parse(String.Format("{0,9:N0}", Get_Numeric(this.FPS91_TY_S_UT_7BL9Z065.GetValue(i, "HMSECHARGEAMT").ToString())));
                            dMI_COUNT++;
                        }
                    }

                    this.FPS91_TY_S_UT_7BL9Z065_Sheet1.Cells[i, 11].Locked = true;  //청구화물료
                    this.FPS91_TY_S_UT_7BL9Z065_Sheet1.Cells[i, 14].Locked = true;  //청구보안료
                }
            }

            if (this.CBO01_GGUBUN.GetValue().ToString() == "A")
            {
                this.TXT01_MI_AMOUNT.SetValue((dMI_AMOUNT).ToString("0"));
                this.TXT01_MI_COUNT.SetValue((dMI_COUNT).ToString("0"));

                this.TXT01_GAMOUNT.SetValue((dGAMOUNT).ToString("0"));
                this.TXT01_READ_COUNT.SetValue((dREAD_COUNT).ToString("0"));
            }
        }
        #endregion

        #region Description : 전표생성 버튼
        private void BTN61_JPNO_CRE_Click(object sender, EventArgs e)
        {
            string sHMDATE        = string.Empty;
            string sHMCHDAT       = string.Empty;
            string sHMPAYDAT      = string.Empty;
	        string sHMIPHANG      = string.Empty;
	        string sHMBONSUN      = string.Empty;
	        string sHMHWAJU       = string.Empty;
            string sHMIPGOGB      = string.Empty;
            string sHMSHQTY       = string.Empty;
 	        string sHMDANGA       = string.Empty;
	        string sHMYUL         = string.Empty;
	        string sHMBBLS        = string.Empty;
	        string sHMCOMPBBLS    = string.Empty;
	        string sHMCOMPAMT     = string.Empty;
	        string sHMCHARGEAMT   = string.Empty;
            string sHMRPCODE      = string.Empty;
            string sHMSEBBLS      = string.Empty;
            string sHMSECOMPBBLS  = string.Empty;
            string sHMSECOMPAMT   = string.Empty;
            string sHMSECHARGEAMT = string.Empty;
            string sHMSEDANGA     = string.Empty;

            string sJPNOID        = string.Empty;

            int i;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 화물료 전표 테이블 생성
            this.DbConnector.CommandClear();

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (this.DTP01_HMDATE.GetValue().ToString() == "")
                {
                    sHMDATE = Get_Date(ds.Tables[0].Rows[i]["HMCHDAT"].ToString());
                }
                else
                {
                    sHMDATE = Get_Date(this.DTP01_HMDATE.GetValue().ToString());
                }
	            sHMCHDAT       = Get_Date(ds.Tables[0].Rows[i]["HMCHDAT"].ToString());
                sHMPAYDAT      = Get_Date(ds.Tables[0].Rows[i]["HMPAYDAT"].ToString());
	            sHMIPHANG      = Get_Date(ds.Tables[0].Rows[i]["HMIPHANG"].ToString());
	            sHMBONSUN      = ds.Tables[0].Rows[i]["HMBONSUN"].ToString();
	            sHMHWAJU       = ds.Tables[0].Rows[i]["HMHWAJU"].ToString();
                sHMSHQTY       = ds.Tables[0].Rows[i]["HMSHQTY"].ToString();
 	            sHMDANGA       = ds.Tables[0].Rows[i]["HMDANGA"].ToString();
	            sHMYUL         = ds.Tables[0].Rows[i]["HMYUL"].ToString();
	            sHMBBLS        = ds.Tables[0].Rows[i]["HMBBLS"].ToString();
	            sHMCOMPBBLS    = ds.Tables[0].Rows[i]["HMCOMPBBLS"].ToString();
	            sHMCOMPAMT     = ds.Tables[0].Rows[i]["HMCOMPAMT"].ToString();
	            sHMCHARGEAMT   = ds.Tables[0].Rows[i]["HMCHARGEAMT"].ToString();
                sHMRPCODE      = ds.Tables[0].Rows[i]["HMRPCODE"].ToString();
                sHMSEBBLS      = ds.Tables[0].Rows[i]["HMSEBBLS"].ToString();
                sHMSECOMPBBLS  = ds.Tables[0].Rows[i]["HMSECOMPBBLS"].ToString();
                sHMSECOMPAMT   = ds.Tables[0].Rows[i]["HMSECOMPAMT"].ToString();
                sHMSECHARGEAMT = ds.Tables[0].Rows[i]["HMSECHARGEAMT"].ToString();
                sHMSEDANGA     = ds.Tables[0].Rows[i]["HMSEDANGA"].ToString();

                // 20180518일 명화 수정 요청
                // 입고구분에 따라 고지서가 오름
                // 입항일자, 본선, 화주, 화물이 같고 입고, 선적 및 재선적일 경우가 있을 경우
                // 고지서가 2장 옴 => 전표도 2장으로 처리되도록 요청
                if (ds.Tables[0].Rows[i]["HMIPGOGB"].ToString() == "입고")
                {
                    sHMIPGOGB = "";
                }
                else
                {
                    sHMIPGOGB = "S";
                }

                // "TY_P_UT_7BMFE094" 보안료 적용전 프로시저
                this.DbConnector.Attach("TY_P_UT_91EHE501", sHMDATE.ToString(),
                                                            sHMCHDAT.ToString(),
                                                            sHMPAYDAT.ToString(),
                                                            sHMIPHANG.ToString(),
                                                            sHMBONSUN.ToString(),
                                                            sHMHWAJU.ToString(),
                                                            sHMIPGOGB.ToString(),
                                                            sHMSHQTY.ToString(),
                                                            sHMDANGA.ToString(),
                                                            sHMYUL.ToString(),
                                                            sHMBBLS.ToString(),
                                                            sHMCOMPBBLS.ToString(),
                                                            sHMCOMPAMT.ToString(),
                                                            sHMCHARGEAMT.ToString(),
                                                            sHMRPCODE.ToString(),
                                                            sHMSEBBLS.ToString(),       // 보안료 BBLS
                                                            sHMSECOMPBBLS.ToString(),   // 보안료 계산 BBLS
                                                            sHMSECOMPAMT.ToString(),    // 보안료
                                                            sHMSECHARGEAMT.ToString(),  // 청구 보안료
                                                            sHMSEDANGA.ToString(),      // 보안료 단가
                                                            TYUserInfo.EmpNo.ToString().Trim().ToUpper()
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            // 화물료 전표테이블의 전표생성
            string sOUTMSG = string.Empty;
            string sB2SSID = string.Empty;

            // BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958"); 
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            // 화물료 전표테이블의 전표생성 SP 수행
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_UT_7BMI1101", 보안료 적용전 SP
            this.DbConnector.Attach("TY_P_UT_91EHK502", sB2SSID.ToString(),
                                                        sB2SSID.Length,
                                                        sJPNOID.ToString(),
                                                        "TYUTME036B",
                                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                                        "A",
                                                        sOUTMSG.ToString()
                                                        );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 2) == "OK")
            {
                this.ShowMessage("TY_M_AC_25O8K620"); // 저장 메세지
            }
            else
            {
                this.ShowMessage("TY_M_UT_73D99886"); // 저장 메세지
            }


            // 발행구분
            this.CBO01_GGUBUN.SetValue("P");

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 전표취소 버튼
        private void BTN61_JUNPYO_CANCEL_Click(object sender, EventArgs e)
        {
            string sJPNOID = string.Empty;

            string sOUTMSG = string.Empty;
            string sB2SSID = string.Empty;

            string sHMJPNO = string.Empty;


            sJPNOID = this.IPAdresss + Employer.EmpNo;


            // TEMP 삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_7BRDB124", sJPNOID.ToString());
            this.DbConnector.ExecuteNonQuery();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 한전표씩 전표가 취소 됨

                if (sHMJPNO.ToString() == "")
                {
                    sHMJPNO = ds.Tables[0].Rows[i]["HMJPNO"].ToString().Substring(0, 17).Trim();

                    // 화물료 전표 TEMP생성
                    this.DbConnector.Attach("TY_P_UT_7BRDD125", sJPNOID.ToString(),
                                                                sHMJPNO.ToString());
                }

                if (sHMJPNO != ds.Tables[0].Rows[i]["HMJPNO"].ToString().Substring(0, 17).Trim())
                {
                    sHMJPNO = ds.Tables[0].Rows[i]["HMJPNO"].ToString().Substring(0, 17).Trim();

                    // 화물료 전표 TEMP생성
                    this.DbConnector.Attach("TY_P_UT_7BRDD125", sJPNOID.ToString(),
                                                                sHMJPNO.ToString());
                }
            }

            this.DbConnector.ExecuteTranQueryList();

            // BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            // 화물료 전표테이블의 전표취소 SP 수행
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_UT_7BMI1101", 보안료 적용 전 SP
            this.DbConnector.Attach("TY_P_UT_91EHK502", sB2SSID.ToString(),
                                                        sB2SSID.Length,
                                                        sJPNOID.ToString(),
                                                        "TYUTME036B",
                                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                                        "D",
                                                        sOUTMSG.ToString()
                                                        );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 2) == "OK")
            {
                this.ShowMessage("TY_M_AC_25O8K620"); // 저장 메세지
            }
            else
            {
                this.ShowMessage("TY_M_UT_73D99886"); // 저장 메세지
            }

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 항내운송처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_UT_839A4674",
                                        ds.Tables[0].Rows[i]["CMTRANSPORT"].ToString().ToUpper(),
                                        ds.Tables[0].Rows[i]["HMDIPHANG"].ToString(),
                                        ds.Tables[0].Rows[i]["HMDBONSUN"].ToString(),
                                        ds.Tables[0].Rows[i]["HMDHWAJU"].ToString(),
                                        ds.Tables[0].Rows[i]["HMDHWAMUL"].ToString()
                                        ); // 항내운송 업데이트
            }

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_MR_2BF50354");
        }
        #endregion

        #region Description : 전표생성 ProcessCheck
        private void BTN61_JPNO_CRE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            double dAMOUNT = 0;
            double dCNT    = 0;
            string sHMIPGOGB = string.Empty;


            DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_UT_7BL9Z065.GetDataSourceInclude(TSpread.TActionType.Select, "HMCHDAT", "HMPAYDAT", "HMIPHANG", "HMBONSUN", "HMHWAJU", "HMIPGOGB", "HMSHQTY", "HMDANGA", "HMYUL", "HMBBLS", "HMCOMPBBLS", "HMCOMPAMT", "HMCHARGEAMT", "HMRPCODE", "HMSEBBLS", "HMSECOMPBBLS", "HMSECOMPAMT", "HMSECHARGEAMT", "HMSEDANGA"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_UT_7BME2089");
                e.Successed = false;
                return;
            }


            if (ds.Tables[0].Rows.Count > 0)
            {
                this.TXT01_READ_COUNT.SetValue(ds.Tables[0].Rows.Count);

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["HMCHARGEAMT"].ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_UT_7BME6090");
                        e.Successed = false;
                        return;
                    }
                    else
                    {
                        //if ((double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["HMCHARGEAMT"].ToString())) - double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["HMCOMPAMT"].ToString())) >= 1000) ||
                        //    (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["HMCOMPAMT"].ToString())) - double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["HMCHARGEAMT"].ToString())) >= 1000))
                        //{
                        //    this.ShowMessage("TY_M_UT_7CQHE362");
                        //    e.Successed = false;
                        //    return;
                        //}
                    }

                    if (ds.Tables[0].Rows[i]["HMIPGOGB"].ToString() == "입고")
                    {
                        sHMIPGOGB = "";
                    }
                    else
                    {
                        sHMIPGOGB = "S";
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_7BMFP095",
                        Get_Date(ds.Tables[0].Rows[i]["HMCHDAT"].ToString()),
                        Get_Date(ds.Tables[0].Rows[i]["HMPAYDAT"].ToString()),
                        Get_Date(ds.Tables[0].Rows[i]["HMIPHANG"].ToString()),
                        ds.Tables[0].Rows[i]["HMBONSUN"].ToString(),
                        ds.Tables[0].Rows[i]["HMHWAJU"].ToString(),
                        sHMIPGOGB.ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_UT_7BMFV096");
                        e.Successed = false;
                        return;
                    }

                    dAMOUNT = dAMOUNT + double.Parse(String.Format("{0,9:N0}", Get_Numeric(ds.Tables[0].Rows[i]["HMCHARGEAMT"].ToString())));
                    dAMOUNT = dAMOUNT + double.Parse(String.Format("{0,9:N0}", Get_Numeric(ds.Tables[0].Rows[i]["HMSECHARGEAMT"].ToString())));
                }

                this.TXT01_GAMOUNT.SetValue((dAMOUNT).ToString("0"));
            }

            //if (ds.Tables[0].Rows.Count > 96)
            //{
            //    this.ShowMessage("TY_M_UT_7BME5091");
            //    e.Successed = false;
            //    return;
            //}

            if (!this.ShowMessage("TY_M_MR_31943577"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 전표취소 ProcessCheck
        private void BTN61_JUNPYO_CANCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_UT_7BL9Z065.GetDataSourceInclude(TSpread.TActionType.Select, "HMJPNO"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_UT_7BME2089");
                e.Successed = false;
                return;
            }


            if (ds.Tables[0].Rows.Count > 0)
            {
                string sHMJPNO = string.Empty;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["HMJPNO"].ToString().Trim() == "")
                    {
                        this.ShowMessage("TY_M_UT_7BMFY098");
                        e.Successed = false;
                        return;
                    }

                    // 화물료 전표테이블 전표 삭제시 체크(화물료 생성일자 존재 체크)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_7BMFZ099",
                        ds.Tables[0].Rows[i]["HMJPNO"].ToString().Substring(0,17).Trim()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_UT_7BMG0100");
                        e.Successed = false;
                        return;
                    }

                    // 한전표씩 전표가 취소 됨

                    //if (sHMJPNO.ToString() == "")
                    //{
                    //    sHMJPNO = ds.Tables[0].Rows[i]["HMJPNO"].ToString().Substring(0, 17).Trim();

                    //    fsHMJPNO = sHMJPNO.ToString();
                    //}

                    //if (sHMJPNO != ds.Tables[0].Rows[i]["HMJPNO"].ToString().Substring(0, 17).Trim())
                    //{
                    //    this.ShowMessage("TY_M_UT_7BN8O103");
                    //    e.Successed = false;
                    //    return;
                    //}
                }
            }
            
            if (!this.ShowMessage("TY_M_UT_7BMFX097"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 항내운송처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_UT_7BL9V064.GetDataSourceInclude(TSpread.TActionType.Update, "HMDIPHANG", "HMDBONSUN", "HMDHWAJU", "HMDHWAMUL", "CMTRANSPORT"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["CMTRANSPORT"].ToString() != "" && ds.Tables[0].Rows[i]["CMTRANSPORT"].ToString().ToUpper() != "Y")
                {
                    this.ShowMessage("TY_M_UT_8399W673");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 마스터 그리드 더블클릭 이벤트
        private void FPS91_TY_S_UT_7BL9Z065_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.FPS91_TY_S_UT_7BL9V064.Initialize();

            string sHMIPHANG = string.Empty;
            string sHMBONSUN = string.Empty;
            string sHMHWAJU  = string.Empty;
            string sHMIPGOGB = string.Empty;
            string sHMJPNO   = string.Empty;

            string sProcedure = string.Empty;

            sHMIPHANG = Get_Date(this.FPS91_TY_S_UT_7BL9Z065.GetValue("HMIPHANG").ToString());
            sHMBONSUN = this.FPS91_TY_S_UT_7BL9Z065.GetValue("HMBONSUN").ToString();
            sHMHWAJU  = this.FPS91_TY_S_UT_7BL9Z065.GetValue("HMHWAJU").ToString();
            sHMIPGOGB = this.FPS91_TY_S_UT_7BL9Z065.GetValue("HMIPGOGB").ToString();
            sHMJPNO   = this.FPS91_TY_S_UT_7BL9Z065.GetValue("HMJPNO").ToString();


            if (sHMIPGOGB == "입고")
            {
                sHMIPGOGB = "";
            }
            else
            {
                sHMIPGOGB = "S,T";
            }

            if (sHMJPNO.ToString() == "")
            {
                sProcedure = "TY_P_UT_7BL9D063";
            }
            else
            {
                sProcedure = "TY_P_UT_7BNDJ107";
            }

            

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                sProcedure.ToString(),
                sHMIPHANG.ToString(),
                sHMBONSUN.ToString(),
                sHMHWAJU.ToString(),
                sHMIPGOGB.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_7BL9V064.SetValue(dt);
        }
        #endregion

        #region Description : 마스터 그리드 클릭 이벤트
        private void FPS91_TY_S_UT_7BL9Z065_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Column == 0)
            {
                if (this.FPS91_TY_S_UT_7BL9Z065.ActiveSheet.Cells[e.Row, this.FPS91_TY_S_UT_7BL9Z065.ActiveSheet.Columns["GUBUN"].Index].CellType == null)
                {
                    fdAMOUNT = fdAMOUNT + double.Parse(String.Format("{0,9:N0}", this.FPS91_TY_S_UT_7BL9Z065.GetValue(e.Row, "HMCHARGEAMT").ToString()));
                    fdAMOUNT = fdAMOUNT + double.Parse(String.Format("{0,9:N0}", this.FPS91_TY_S_UT_7BL9Z065.GetValue(e.Row, "HMSECHARGEAMT").ToString()));

                    TButtonCellType tButtonCellType = new TButtonCellType();

                    tButtonCellType.Text = "선택";
                    tButtonCellType.TextAlign = FarPoint.Win.ButtonTextAlign.TextTopPictBottom;
                    tButtonCellType.TextOrientation = FarPoint.Win.TextOrientation.TextHorizontal;
                    this.FPS91_TY_S_UT_7BL9Z065.ActiveSheet.Cells[e.Row, this.FPS91_TY_S_UT_7BL9Z065.ActiveSheet.Columns["GUBUN"].Index].CellType = tButtonCellType;
                    this.FPS91_TY_S_UT_7BL9Z065.ActiveSheet.Cells[e.Row, this.FPS91_TY_S_UT_7BL9Z065.ActiveSheet.Columns["GUBUN"].Index].Locked = true;

                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_UT_7BL9Z065.ActiveSheet.Cells[e.Row, this.FPS91_TY_S_UT_7BL9Z065.ActiveSheet.Columns["HMCHARGEAMT"].Index].Font = new Font("굴림", 9, FontStyle.Bold);
                    this.FPS91_TY_S_UT_7BL9Z065.ActiveSheet.Cells[e.Row, this.FPS91_TY_S_UT_7BL9Z065.ActiveSheet.Columns["HMSECHARGEAMT"].Index].Font = new Font("굴림", 9, FontStyle.Bold);

                    // 특정 칼럼 색깔 입히기
                    this.FPS91_TY_S_UT_7BL9Z065.ActiveSheet.Rows[e.Row].BackColor = Color.SkyBlue;
                }
                else
                {
                    fdAMOUNT = fdAMOUNT - double.Parse(String.Format("{0,9:N0}", this.FPS91_TY_S_UT_7BL9Z065.GetValue(e.Row, "HMCHARGEAMT").ToString()));
                    fdAMOUNT = fdAMOUNT - double.Parse(String.Format("{0,9:N0}", this.FPS91_TY_S_UT_7BL9Z065.GetValue(e.Row, "HMSECHARGEAMT").ToString()));

                    this.FPS91_TY_S_UT_7BL9Z065.ActiveSheet.Cells[e.Row, this.FPS91_TY_S_UT_7BL9Z065.ActiveSheet.Columns["GUBUN"].Index].CellType = null;

                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_UT_7BL9Z065.ActiveSheet.Cells[e.Row, this.FPS91_TY_S_UT_7BL9Z065.ActiveSheet.Columns["HMCHARGEAMT"].Index].Font = new Font("굴림", 9);
                    this.FPS91_TY_S_UT_7BL9Z065.ActiveSheet.Cells[e.Row, this.FPS91_TY_S_UT_7BL9Z065.ActiveSheet.Columns["HMSECHARGEAMT"].Index].Font = new Font("굴림", 9);

                    // 특정 칼럼 색깔 입히기
                    this.FPS91_TY_S_UT_7BL9Z065.ActiveSheet.Rows[e.Row].BackColor = Color.White;
                }

                this.TXT01_GAMOUNT.SetValue((fdAMOUNT).ToString("0"));
            }
        }
        #endregion        

        #region Description : 전표출력
        private void FPS91_TY_S_UT_7BL9Z065_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "17")
            {
                if (this.FPS91_TY_S_UT_7BL9Z065.GetValue("HMJPNO").ToString() != "")
                {
                    string sB2DPMK = this.FPS91_TY_S_UT_7BL9Z065.GetValue("HMJPNO").ToString().Substring(0, 6);
                    string sB2DTMK = this.FPS91_TY_S_UT_7BL9Z065.GetValue("HMJPNO").ToString().Substring(6, 8);
                    string sB2NOSQ = this.FPS91_TY_S_UT_7BL9Z065.GetValue("HMJPNO").ToString().Substring(14, 3);

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_AC_2AU2M916",
                        sB2DPMK,
                        sB2DTMK,
                        sB2NOSQ, // 시작 번호
                        sB2NOSQ  // 종료 번호
                        );

                    if (Convert.ToDouble(sB2DTMK.Substring(0, 4)) > 2014)
                    {
                        ActiveReport rpt = new TYACBJ0012R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }
                    else
                    {
                        ActiveReport rpt = new TYACBJ001R();
                        // 세로 출력
                        rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;
                        DataTable dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                        }
                    }

                }
            }
            if (e.Column.ToString() == "18")
            {
                if (this.FPS91_TY_S_UT_7BL9Z065.GetValue("HMJPNO").ToString() != "")
                {
                    // 2018-11-30 성명화 대리 요청 각 건에 해당하는 자료만 조회하도록 수정
                    this.DbConnector.Attach
                        (
                        "TY_P_UT_81N9B511",
                        this.FPS91_TY_S_UT_7BL9Z065.GetValue("HMJPNO").ToString().Substring(6, 8),
                        this.FPS91_TY_S_UT_7BL9Z065.GetValue("HMJPNO").ToString().Substring(0, 17)
                        );

                    //this.DbConnector.Attach
                    //    (
                    //    "TY_P_UT_8BUBN255",
                    //    this.FPS91_TY_S_UT_7BL9Z065.GetValue("HMJPNO").ToString().Substring(6,8),
                    //    this.FPS91_TY_S_UT_7BL9Z065.GetValue("HMIPHANG").ToString().Replace("-",""),
                    //    this.FPS91_TY_S_UT_7BL9Z065.GetValue("HMBONSUN").ToString(),
                    //    this.FPS91_TY_S_UT_7BL9Z065.GetValue("HMHWAJU").ToString()
                    //    );

                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    ActiveReport rpt = new TYUTME036R();
                    // 세로 출력
                    rpt.PageSettings.Orientation = DataDynamics.ActiveReports.Document.PageOrientation.Portrait;

                    if (dt.Rows.Count > 0)
                    {
                        (new TYERGB001P(rpt, dt)).ShowDialog();
                    }
                }
            }
        }
        #endregion

        #region Description : 발행구분 이벤트
        private void CBO01_GGUBUN_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 조회
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 그리드 청구 화물료, 청구 보안료 값 변경 이벤트
        private void FPS91_TY_S_UT_7BL9Z065_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            string sHMCHARGEAMT = this.FPS91_TY_S_UT_7BL9Z065.GetValue("HMCHARGEAMT").ToString();
            string sHMSECHARGEAMT = this.FPS91_TY_S_UT_7BL9Z065.GetValue("HMSECHARGEAMT").ToString();

            if (sHMCHARGEAMT == "") sHMCHARGEAMT = "0";
            if (sHMSECHARGEAMT == "") sHMSECHARGEAMT = "0";

            string sHMTOTALAMT = Convert.ToString(Convert.ToDouble(sHMCHARGEAMT) + Convert.ToDouble(sHMSECHARGEAMT));

            this.FPS91_TY_S_UT_7BL9Z065.SetValue("HMTOTALAMT", sHMTOTALAMT);
        }
        #endregion
    }
}