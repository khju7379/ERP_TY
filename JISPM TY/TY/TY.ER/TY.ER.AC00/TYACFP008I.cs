using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.GB00;

namespace TY.ER.AC00
{
    /// <summary>
    /// 화물료 납부전표관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.02.23 16:02
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_82NGC636 : 화물료 납부전표 발행관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_MR_31943577 : 전표생성 작업을 하시겠습니까?
    ///  TY_M_MR_31945578 : 전표생성 작업이 완료 되었습니다.
    ///  TY_M_MR_3194D580 : 전표취소 작업을 하시겠습니까?
    ///  TY_M_MR_3194D581 : 전표취소 작업이 완료 되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  REM : 삭제
    ///  SAV : 저장
    ///  B2CDBK : 은행코드
    ///  B2DPMK : 작성부서
    ///  B2HISAB : 작성사번
    ///  B2NOAC : 계좌번호
    ///  B2IDJP :  전표구분
    ///  ARJUNNO :  전표번호
    ///  B2DTMK : 작성일자
    /// </summary>
    public partial class TYACFP008I : TYBase
    {
        private DataTable ftdt;
        private string fsGubn;
        private bool _Isloaded = false;

        private TYData DAT02_W2SSID;
        private TYData DAT02_W2DPMK;
        private TYData DAT02_W2DTMK;
        private TYData DAT02_W2NOSQ;
        private TYData DAT02_W2NOLN;
        private TYData DAT02_W2IDJP;
        private TYData DAT02_W2NOJP;
        private TYData DAT02_W2CDAC;
        private TYData DAT02_W2DTAC;
        private TYData DAT02_W2DTLI;
        private TYData DAT02_W2DPAC;
        private TYData DAT02_W2CDMI1;
        private TYData DAT02_W2VLMI1;
        private TYData DAT02_W2CDMI2;
        private TYData DAT02_W2VLMI2;
        private TYData DAT02_W2CDMI3;
        private TYData DAT02_W2VLMI3;
        private TYData DAT02_W2CDMI4;
        private TYData DAT02_W2VLMI4;
        private TYData DAT02_W2CDMI5;
        private TYData DAT02_W2VLMI5;
        private TYData DAT02_W2CDMI6;
        private TYData DAT02_W2VLMI6;
        private TYData DAT02_W2AMDR;
        private TYData DAT02_W2AMCR;
        private TYData DAT02_W2CDFD;
        private TYData DAT02_W2AMFD;
        private TYData DAT02_W2RKAC;
        private TYData DAT02_W2RKCU;
        private TYData DAT02_W2WCJP;
        private TYData DAT02_W2PRGB;
        private TYData DAT02_W2HIGB;
        private TYData DAT02_W2HISAB;

        #region  Description : 폼 로드 이벤트
        public TYACFP008I(DataTable dt, string sGubn)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.DAT02_W2SSID = new TYData("DAT02_W2SSID", null);
            this.DAT02_W2DPMK = new TYData("DAT02_W2DPMK", null);
            this.DAT02_W2DTMK = new TYData("DAT02_W2DTMK", null);
            this.DAT02_W2NOSQ = new TYData("DAT02_W2NOSQ", null);
            this.DAT02_W2NOLN = new TYData("DAT02_W2NOLN", null);
            this.DAT02_W2IDJP = new TYData("DAT02_W2IDJP", null);
            this.DAT02_W2NOJP = new TYData("DAT02_W2NOJP", null);
            this.DAT02_W2CDAC = new TYData("DAT02_W2CDAC", null);
            this.DAT02_W2DTAC = new TYData("DAT02_W2DTAC", null);
            this.DAT02_W2DTLI = new TYData("DAT02_W2DTLI", null);
            this.DAT02_W2DPAC = new TYData("DAT02_W2DPAC", null);
            this.DAT02_W2CDMI1 = new TYData("DAT02_W2CDMI1", null);
            this.DAT02_W2VLMI1 = new TYData("DAT02_W2VLMI1", null);
            this.DAT02_W2CDMI2 = new TYData("DAT02_W2CDMI2", null);
            this.DAT02_W2VLMI2 = new TYData("DAT02_W2VLMI2", null);
            this.DAT02_W2CDMI3 = new TYData("DAT02_W2CDMI3", null);
            this.DAT02_W2VLMI3 = new TYData("DAT02_W2VLMI3", null);
            this.DAT02_W2CDMI4 = new TYData("DAT02_W2CDMI4", null);
            this.DAT02_W2VLMI4 = new TYData("DAT02_W2VLMI4", null);
            this.DAT02_W2CDMI5 = new TYData("DAT02_W2CDMI5", null);
            this.DAT02_W2VLMI5 = new TYData("DAT02_W2VLMI5", null);
            this.DAT02_W2CDMI6 = new TYData("DAT02_W2CDMI6", null);
            this.DAT02_W2VLMI6 = new TYData("DAT02_W2VLMI6", null);
            this.DAT02_W2AMDR = new TYData("DAT02_W2AMDR", null);
            this.DAT02_W2AMCR = new TYData("DAT02_W2AMCR", null);
            this.DAT02_W2CDFD = new TYData("DAT02_W2CDFD", null);
            this.DAT02_W2AMFD = new TYData("DAT02_W2AMFD", null);
            this.DAT02_W2RKAC = new TYData("DAT02_W2RKAC", null);
            this.DAT02_W2RKCU = new TYData("DAT02_W2RKCU", null);
            this.DAT02_W2WCJP = new TYData("DAT02_W2WCJP", null);
            this.DAT02_W2PRGB = new TYData("DAT02_W2PRGB", null);
            this.DAT02_W2HIGB = new TYData("DAT02_W2HIGB", null);
            this.DAT02_W2HISAB = new TYData("DAT02_W2HISAB", null);

            ftdt = dt;
            fsGubn = sGubn;
        }

        private void TYACFP008I_Load(object sender, System.EventArgs e)
        {
            string sFilter = string.Empty;

            this.ControlFactory.Add(this.DAT02_W2SSID);
            this.ControlFactory.Add(this.DAT02_W2DPMK);
            this.ControlFactory.Add(this.DAT02_W2DTMK);
            this.ControlFactory.Add(this.DAT02_W2NOSQ);
            this.ControlFactory.Add(this.DAT02_W2NOLN);
            this.ControlFactory.Add(this.DAT02_W2IDJP);
            this.ControlFactory.Add(this.DAT02_W2NOJP);
            this.ControlFactory.Add(this.DAT02_W2CDAC);
            this.ControlFactory.Add(this.DAT02_W2DTAC);
            this.ControlFactory.Add(this.DAT02_W2DTLI);
            this.ControlFactory.Add(this.DAT02_W2DPAC);
            this.ControlFactory.Add(this.DAT02_W2CDMI1);
            this.ControlFactory.Add(this.DAT02_W2VLMI1);
            this.ControlFactory.Add(this.DAT02_W2CDMI2);
            this.ControlFactory.Add(this.DAT02_W2VLMI2);
            this.ControlFactory.Add(this.DAT02_W2CDMI3);
            this.ControlFactory.Add(this.DAT02_W2VLMI3);
            this.ControlFactory.Add(this.DAT02_W2CDMI4);
            this.ControlFactory.Add(this.DAT02_W2VLMI4);
            this.ControlFactory.Add(this.DAT02_W2CDMI5);
            this.ControlFactory.Add(this.DAT02_W2VLMI5);
            this.ControlFactory.Add(this.DAT02_W2CDMI6);
            this.ControlFactory.Add(this.DAT02_W2VLMI6);
            this.ControlFactory.Add(this.DAT02_W2AMDR);
            this.ControlFactory.Add(this.DAT02_W2AMCR);
            this.ControlFactory.Add(this.DAT02_W2CDFD);
            this.ControlFactory.Add(this.DAT02_W2AMFD);
            this.ControlFactory.Add(this.DAT02_W2RKAC);
            this.ControlFactory.Add(this.DAT02_W2RKCU);
            this.ControlFactory.Add(this.DAT02_W2WCJP);
            this.ControlFactory.Add(this.DAT02_W2PRGB);
            this.ControlFactory.Add(this.DAT02_W2HIGB);
            this.ControlFactory.Add(this.DAT02_W2HISAB);

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.CBH01_B2CDBK.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(CBH01_B2CDBK_CodeBoxDataBinded);

            this.DTP01_B2DTMK.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.CBH01_B2DPMK.DummyValue = this.DTP01_B2DTMK.GetString();

            this.CBH01_B2DPMK.SetValue(TYUserInfo.DeptCode);
            this.CBH01_B2HISAB.SetValue(TYUserInfo.EmpNo);

            this.CBH01_B2CDBK.OnCodeBoxDataBinded(null, null);

            this.FPS91_TY_S_AC_82NGC636.Initialize();
            this.FPS91_TY_S_AC_82NGC636.SetValue(ftdt);

            double dTotal = 0;
            TXT01_MDCOUNT.SetValue(ftdt.Rows.Count.ToString());

            // 청구 보안화물료
            for (int i = 0; i < ftdt.Rows.Count; i++)
            {
                dTotal += Convert.ToDouble(ftdt.Rows[i]["HMCHARGEAMT"].ToString());
            }
            this.TXT01_HMCHARGEAMT.SetValue(dTotal);

            dTotal = 0;

            // 청구 보안료
            for (int i = 0; i < ftdt.Rows.Count; i++)
            {
                dTotal += Convert.ToDouble(ftdt.Rows[i]["HMSECHARGEAMT"].ToString());
            }
            this.TXT01_HMSECHARGEAMT.SetValue(dTotal);

            if (fsGubn != "A")
            {
                TXT01_ARJUNNO.SetValue(ftdt.Rows[0]["HMBJJPNO"].ToString().Substring(0, 17).Substring(0, 6) + "-" + ftdt.Rows[0]["HMBJJPNO"].ToString().Substring(0, 17).Substring(6, 8) + "-" + ftdt.Rows[0]["HMBJJPNO"].ToString().Substring(0, 17).Substring(14, 3));
            }

            this.SetStartingFocus(CBH01_B2CDBK.CodeText);

            UP_Set_DspControl();

        }
        #endregion

        #region  Description : 화면,버튼 잠금 처리
        private void UP_Set_DspControl()
        {
            if (fsGubn == "A")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_REM.Visible = false;

                DTP01_B2DTMK.SetReadOnly(false);
                CBH01_B2DPMK.SetReadOnly(false);
                CBH01_B2HISAB.SetReadOnly(false);
                CBH01_B2CDBK.SetReadOnly(false);
                CBH01_B2NOAC.SetReadOnly(false);
                CBO01_B2IDJP.SetReadOnly(false);
            }
            else
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_REM.Visible = true;

                DTP01_B2DTMK.SetReadOnly(true);
                CBH01_B2DPMK.SetReadOnly(true);
                CBH01_B2HISAB.SetReadOnly(true);
                CBH01_B2CDBK.SetReadOnly(true);
                CBH01_B2NOAC.SetReadOnly(true);
                CBO01_B2IDJP.SetReadOnly(true);
            }
        }
        #endregion


        #region  Description : 계좌번호 CBH01_B2CDBK_CodeBoxDataBinded 이벤트
        private void CBH01_B2CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_B2CDBK.GetValue().ToString();
            this.CBH01_B2NOAC.DummyValue = groupCode;
            this.CBH01_B2NOAC.SetValue("");
            this.CBH01_B2NOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_B2NOAC.Initialize();
        }
        #endregion

        #region  Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            if( TXT01_ARJUNNO.GetValue().ToString().Trim() != "" )
            {
                string sB2DPMK = this.TXT01_ARJUNNO.GetValue().ToString().Substring(0, 6);
                string sB2DTMK = this.TXT01_ARJUNNO.GetValue().ToString().Substring(7, 8);
                string sB2NOSQ = this.TXT01_ARJUNNO.GetValue().ToString().Substring(16, 3);

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
                    SectionReport rpt = new TYACBJ0012R();
                    // 세로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                    }
                }
                else
                {
                    SectionReport rpt = new TYACBJ001R();
                    // 세로 출력
                    rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
                    DataTable dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        (new TYERGB001P(rpt, UP_ConvertJunPyo(dt))).ShowDialog();
                    }
                }
            }
        }
        #endregion

        #region  Description : 전표삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            string sB2SSID = "";            

            //전표삭제
            if (this.TXT01_ARJUNNO.GetValue().ToString() != "")
            {
                //BATID번호 부여
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29C7M958");
                decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
                sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

                //미승인전표 -> 임시파일 입력
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29C7K957", sB2SSID, this.TXT01_ARJUNNO.GetValue().ToString().Substring(0, 6), this.TXT01_ARJUNNO.GetValue().ToString().Substring(7, 8), this.TXT01_ARJUNNO.GetValue().ToString().Substring(16, 3));
                //미승인 SP호출 파일 입력
                this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, CBH01_B2HISAB.GetValue(), "D",
                                                            this.CBH01_B2DPMK.GetValue(), this.DTP01_B2DTMK.GetString(), "", "",
                                                            "", "", Employer.EmpNo);
                this.DbConnector.ExecuteTranQueryList();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, ""); // SP CALL
                string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar()); // SP의 OUTPUT 값 가져오는 부분
                if (sOUTMSG.Substring(0, 2) == "ER")
                {
                    this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                else
                {
                    UP_Set_JunPyoNumSave("", "D");
                    this.ShowMessage("TY_M_AC_25O8K620");
                }
            }
            else
            {
                this.ShowMessage("TY_M_AC_25O8K620");
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();  
        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {            
            //전표 승인 유무 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_25N1J600", this.TXT01_ARJUNNO.GetValue().ToString().Substring(0, 6), this.TXT01_ARJUNNO.GetValue().ToString().Substring(7, 8), this.TXT01_ARJUNNO.GetValue().ToString().Substring(16, 3));
            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count > 0)
            {
                if (dk.Rows[0]["B1NOJP"].ToString().Trim() != "")
                {
                    this.ShowCustomMessage("승인된 전표는 삭제 할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_MR_31943577"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 전표생성 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {         
            string sB2SSID = "";
            string sW2AMDR = "0";
            bool bJunPyoFlag = false;
            double dAMCRTotal = 0;

            int iCnt = 0;

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            if (ftdt.Rows.Count > 0)
            {
                //전표생성
                if (fsGubn == "A")
                {
                    //BATID번호 부여
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_29C7M958");
                    decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
                    sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

                    //차변
                    for (int i = 0; i < ftdt.Rows.Count; i++)
                    {
                        iCnt = iCnt + 1;  //라인번호

                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(this.CBH01_B2DPMK.GetValue());
                        this.DAT02_W2DTMK.SetValue(this.DTP01_B2DTMK.GetString());
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue(CBO01_B2IDJP.GetValue().ToString());
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue("21101007");
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(this.CBH01_B2DPMK.GetValue());

                        //관리항목
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_23N3M888", "21101007", "");
                        DataTable dk = this.DbConnector.ExecuteDataTable();
                        if (dk.Rows.Count > 0)
                        {
                            if (dk.Rows[0]["A1CDMI1"].ToString().Length > 0)
                            {
                                this.DAT02_W2CDMI1.SetValue(dk.Rows[0]["A1CDMI1"].ToString().Substring(0, 2));
                            }
                            else
                            {
                                this.DAT02_W2CDMI1.SetValue("");
                            }
                            if (dk.Rows[0]["A1CDMI2"].ToString().Length > 0)
                            {
                                this.DAT02_W2CDMI2.SetValue(dk.Rows[0]["A1CDMI2"].ToString().Substring(0, 2));
                            }
                            else
                            {
                                this.DAT02_W2CDMI2.SetValue("");
                            }
                            if (dk.Rows[0]["A1CDMI3"].ToString().Length > 0)
                            {
                                this.DAT02_W2CDMI3.SetValue(dk.Rows[0]["A1CDMI3"].ToString().Substring(0, 2));
                            }
                            else
                            {
                                this.DAT02_W2CDMI3.SetValue("");
                            }
                            if (dk.Rows[0]["A1CDMI4"].ToString().Length > 0)
                            {
                                this.DAT02_W2CDMI4.SetValue(dk.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                            }
                            else
                            {
                                this.DAT02_W2CDMI4.SetValue("");
                            }
                            if (dk.Rows[0]["A1CDMI5"].ToString().Length > 0)
                            {
                                this.DAT02_W2CDMI5.SetValue(dk.Rows[0]["A1CDMI5"].ToString().Substring(0, 2));
                            }
                            else
                            {
                                this.DAT02_W2CDMI5.SetValue("");
                            }
                            if (dk.Rows[0]["A1CDMI6"].ToString().Length > 0)
                            {
                                this.DAT02_W2CDMI6.SetValue(dk.Rows[0]["A1CDMI6"].ToString().Substring(0, 2));
                            }
                            else
                            {
                                this.DAT02_W2CDMI6.SetValue("");
                            }

                            //거래처
                            if (this.DAT02_W2CDMI1.GetValue().ToString() == "01")
                            {
                                this.DAT02_W2VLMI1.SetValue(ftdt.Rows[i]["VNCODE"].ToString());
                            }
                            //납부기한
                            if (this.DAT02_W2CDMI2.GetValue().ToString() == "52")
                            {
                                this.DAT02_W2VLMI2.SetValue(ftdt.Rows[i]["HMPAYDAT"].ToString().Replace("-", ""));
                            }

                            //본선
                            if (this.DAT02_W2CDMI3.GetValue().ToString() == "50")
                            {
                                this.DAT02_W2VLMI3.SetValue(ftdt.Rows[i]["HMBONSUN"].ToString());
                            }
                            
                            this.DAT02_W2VLMI4.SetValue("");
                            this.DAT02_W2VLMI5.SetValue("");
                            this.DAT02_W2VLMI6.SetValue("");
                        }

                        // 청구 화물료
                        dAMCRTotal += Convert.ToDouble(ftdt.Rows[i]["HMCHARGEAMT"].ToString());

                        // 청구 보안료
                        dAMCRTotal += Convert.ToDouble(ftdt.Rows[i]["HMSECHARGEAMT"].ToString());

                        sW2AMDR = "0";

                        sW2AMDR = Convert.ToString(Convert.ToDouble(ftdt.Rows[i]["HMCHARGEAMT"].ToString()) + Convert.ToDouble(ftdt.Rows[i]["HMSECHARGEAMT"].ToString()));

                        this.DAT02_W2AMDR.SetValue(sW2AMDR.ToString());
                        this.DAT02_W2AMCR.SetValue("0");

                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");
                        if (ftdt.Rows[i]["HMSEJPNO"].ToString() == "")
                        {
                            this.DAT02_W2RKAC.SetValue("화물 입,출항료");
                        }
                        else
                        {
                            this.DAT02_W2RKAC.SetValue("화물료 및 항만시설보안료");
                        }
                        this.DAT02_W2RKCU.SetValue(ftdt.Rows[i]["VNSANGHO"].ToString());

                        //원천번호
                        string sWCJP = string.Empty;

                        if (ftdt.Rows[i]["HMSEJPNO"].ToString() == "")
                        {
                            // 화물료 및 보안료 생성 전표의 대변계정을 반제
                            sWCJP = ftdt.Rows[i]["HMJPNO"].ToString().Substring(0, 17) + Set_Fill2((Convert.ToInt16(ftdt.Rows[i]["HMJPNO"].ToString().Substring(17, 2)) + 1).ToString());
                        }
                        else
                        {
                            // 화물료 및 보안료 생성 전표의 대변계정을 반제
                            sWCJP = ftdt.Rows[i]["HMSEJPNO"].ToString().Substring(0, 17) + Set_Fill2((Convert.ToInt16(ftdt.Rows[i]["HMSEJPNO"].ToString().Substring(17, 2)) + 1).ToString());
                        }
                        this.DAT02_W2WCJP.SetValue(sWCJP);
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(Employer.EmpNo);

                        datas.Add(new object[] {this.DAT02_W2SSID.GetValue().ToString(),
                                        this.DAT02_W2DPMK.GetValue().ToString(),
                                        this.DAT02_W2DTMK.GetValue().ToString(),
                                        this.DAT02_W2NOSQ.GetValue().ToString(),
                                        this.DAT02_W2NOLN.GetValue().ToString(),
                                        this.DAT02_W2IDJP.GetValue().ToString(),
                                        this.DAT02_W2NOJP.GetValue().ToString(),
                                        this.DAT02_W2CDAC.GetValue().ToString(),
                                        this.DAT02_W2DTAC.GetValue().ToString(),
                                        this.DAT02_W2DTLI.GetValue().ToString(),
                                        this.DAT02_W2DPAC.GetValue().ToString(),
                                        this.DAT02_W2CDMI1.GetValue().ToString(),
                                        this.DAT02_W2VLMI1.GetValue().ToString(),
                                        this.DAT02_W2CDMI2.GetValue().ToString(),
                                        this.DAT02_W2VLMI2.GetValue().ToString(),
                                        this.DAT02_W2CDMI3.GetValue().ToString(),
                                        this.DAT02_W2VLMI3.GetValue().ToString(),
                                        this.DAT02_W2CDMI4.GetValue().ToString(),
                                        this.DAT02_W2VLMI4.GetValue().ToString(),
                                        this.DAT02_W2CDMI5.GetValue().ToString(),
                                        this.DAT02_W2VLMI5.GetValue().ToString(),
                                        this.DAT02_W2CDMI6.GetValue().ToString(),
                                        this.DAT02_W2VLMI6.GetValue().ToString(),
                                        this.DAT02_W2AMDR.GetValue().ToString(),
                                        this.DAT02_W2AMCR.GetValue().ToString(),
                                        this.DAT02_W2CDFD.GetValue().ToString(),
                                        this.DAT02_W2AMFD.GetValue().ToString(),
                                        this.DAT02_W2RKAC.GetValue().ToString(),
                                        this.DAT02_W2RKCU.GetValue().ToString(),
                                        this.DAT02_W2WCJP.GetValue().ToString(),
                                        this.DAT02_W2PRGB.GetValue().ToString(),
                                        this.DAT02_W2HIGB.GetValue().ToString(),
                                        this.DAT02_W2HISAB.GetValue().ToString(),
                                         "",
                                        "0",
                                        "0",
                                        ""
                                        });
                    } //for...end


                    //대변                   
                    if (CBO01_B2IDJP.GetValue().ToString() != "2")
                    {
                        iCnt = iCnt + 1;  //라인번호

                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(this.CBH01_B2DPMK.GetValue());
                        this.DAT02_W2DTMK.SetValue(this.DTP01_B2DTMK.GetString());
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue(CBO01_B2IDJP.GetValue().ToString());
                        this.DAT02_W2NOJP.SetValue("");

                        //은행,계좌번호에 해당하는 제예금 계정 가져오기
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_24GAG663", this.CBH01_B2NOAC.GetValue(), this.CBH01_B2CDBK.GetValue());
                        DataTable db = this.DbConnector.ExecuteDataTable();
                        if (db.Rows.Count > 0)
                        {
                            this.DAT02_W2CDAC.SetValue(db.Rows[0]["E3CDAC"].ToString().Substring(0, 8));
                        }
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(this.CBH01_B2DPMK.GetValue());

                        //관리항목 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_23N3M888", this.DAT02_W2CDAC.GetValue().ToString(), "");
                        DataTable dm = this.DbConnector.ExecuteDataTable();
                        if (dm.Rows.Count > 0)
                        {
                            if (dm.Rows[0]["A1CDMI1"].ToString().Length > 0)
                            {
                                this.DAT02_W2CDMI1.SetValue(dm.Rows[0]["A1CDMI1"].ToString().Substring(0, 2));
                            }
                            else
                            {
                                this.DAT02_W2CDMI1.SetValue("");
                            }
                            if (dm.Rows[0]["A1CDMI2"].ToString().Length > 0)
                            {
                                this.DAT02_W2CDMI2.SetValue(dm.Rows[0]["A1CDMI2"].ToString().Substring(0, 2));
                            }
                            else
                            {
                                this.DAT02_W2CDMI2.SetValue("");
                            }
                            if (dm.Rows[0]["A1CDMI3"].ToString().Length > 0)
                            {
                                this.DAT02_W2CDMI3.SetValue(dm.Rows[0]["A1CDMI3"].ToString().Substring(0, 2));
                            }
                            else
                            {
                                this.DAT02_W2CDMI3.SetValue("");
                            }
                            if (dm.Rows[0]["A1CDMI4"].ToString().Length > 0)
                            {
                                this.DAT02_W2CDMI4.SetValue(dm.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                            }
                            else
                            {
                                this.DAT02_W2CDMI4.SetValue("");
                            }
                            if (dm.Rows[0]["A1CDMI5"].ToString().Length > 0)
                            {
                                this.DAT02_W2CDMI5.SetValue(dm.Rows[0]["A1CDMI5"].ToString().Substring(0, 2));
                            }
                            else
                            {
                                this.DAT02_W2CDMI5.SetValue("");
                            }
                            if (dm.Rows[0]["A1CDMI6"].ToString().Length > 0)
                            {
                                this.DAT02_W2CDMI6.SetValue(dm.Rows[0]["A1CDMI6"].ToString().Substring(0, 2));
                            }
                            else
                            {
                                this.DAT02_W2CDMI6.SetValue("");
                            }

                            //은행
                            if (this.DAT02_W2CDMI1.GetValue().ToString() == "02")
                            {
                                this.DAT02_W2VLMI1.SetValue(this.CBH01_B2CDBK.GetValue());
                            }
                            //계좌번호
                            if (this.DAT02_W2CDMI2.GetValue().ToString() == "07")
                            {
                                this.DAT02_W2VLMI2.SetValue(this.CBH01_B2NOAC.GetValue());
                            }
                            this.DAT02_W2VLMI3.SetValue("");
                            this.DAT02_W2VLMI4.SetValue("");
                            this.DAT02_W2VLMI5.SetValue("");
                            this.DAT02_W2VLMI6.SetValue("");
                        }

                        this.DAT02_W2AMDR.SetValue("0");
                        this.DAT02_W2AMCR.SetValue(dAMCRTotal.ToString());

                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");
                        this.DAT02_W2RKAC.SetValue(TXT01_B2RKAC.GetValue().ToString());
                        this.DAT02_W2RKCU.SetValue("");
                        this.DAT02_W2WCJP.SetValue("");
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(Employer.EmpNo);

                        datas.Add(new object[] {this.DAT02_W2SSID.GetValue().ToString(),
                                        this.DAT02_W2DPMK.GetValue().ToString(),
                                        this.DAT02_W2DTMK.GetValue().ToString(),
                                        this.DAT02_W2NOSQ.GetValue().ToString(),
                                        this.DAT02_W2NOLN.GetValue().ToString(),
                                        this.DAT02_W2IDJP.GetValue().ToString(),
                                        this.DAT02_W2NOJP.GetValue().ToString(),
                                        this.DAT02_W2CDAC.GetValue().ToString(),
                                        this.DAT02_W2DTAC.GetValue().ToString(),
                                        this.DAT02_W2DTLI.GetValue().ToString(),
                                        this.DAT02_W2DPAC.GetValue().ToString(),
                                        this.DAT02_W2CDMI1.GetValue().ToString(),
                                        this.DAT02_W2VLMI1.GetValue().ToString(),
                                        this.DAT02_W2CDMI2.GetValue().ToString(),
                                        this.DAT02_W2VLMI2.GetValue().ToString(),
                                        this.DAT02_W2CDMI3.GetValue().ToString(),
                                        this.DAT02_W2VLMI3.GetValue().ToString(),
                                        this.DAT02_W2CDMI4.GetValue().ToString(),
                                        this.DAT02_W2VLMI4.GetValue().ToString(),
                                        this.DAT02_W2CDMI5.GetValue().ToString(),
                                        this.DAT02_W2VLMI5.GetValue().ToString(),
                                        this.DAT02_W2CDMI6.GetValue().ToString(),
                                        this.DAT02_W2VLMI6.GetValue().ToString(),
                                        this.DAT02_W2AMDR.GetValue().ToString(),
                                        this.DAT02_W2AMCR.GetValue().ToString(),
                                        this.DAT02_W2CDFD.GetValue().ToString(),
                                        this.DAT02_W2AMFD.GetValue().ToString(),
                                        this.DAT02_W2RKAC.GetValue().ToString(),
                                        this.DAT02_W2RKCU.GetValue().ToString(),
                                        this.DAT02_W2WCJP.GetValue().ToString(),
                                        this.DAT02_W2PRGB.GetValue().ToString(),
                                        this.DAT02_W2HIGB.GetValue().ToString(),
                                        this.DAT02_W2HISAB.GetValue().ToString(),
                                        "",
                                        "0",
                                        "0",
                                        ""
                                        });
                    }                   

                    if (datas.Count > 0)
                    {
                        this.DbConnector.CommandClear();
                        foreach (object[] data in datas)
                        {
                            this.DbConnector.Attach("TY_P_AC_29DA5966", data);
                        }

                        //미승인 SP호출 파일 입력
                        this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, CBH01_B2HISAB.GetValue(), "A",
                                                                    this.CBH01_B2DPMK.GetValue(), this.DTP01_B2DTMK.GetString(), "", "",
                                                                    "", "", Employer.EmpNo);
                        this.DbConnector.ExecuteTranQueryList();

                        //전표 생성 함수 호출
                        bJunPyoFlag = false;
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, "");
                        string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                        if (sOUTMSG.Substring(0, 2) == "ER")
                        {
                            this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                        else
                        {
                            bJunPyoFlag = true;                            

                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_29D5B004", sB2SSID);
                            DataTable dtresult = this.DbConnector.ExecuteDataTable();
                            if (dtresult.Rows.Count > 0)
                            {
                                if (dtresult.Rows[0]["AGRESULTCODE"].ToString().Trim() == "OK")
                                {
                                    //전표번호 받아오기
                                    string sJpno = dtresult.Rows[0]["AGOUT_PARAM"].ToString();
                                    if (sJpno.Trim() != "")
                                    {
                                        TXT01_ARJUNNO.SetValue(sJpno);
                                        this.BTN61_SAV.Visible = false;

                                        this.UP_Set_JunPyoNumSave(sJpno, "A");
                                    }
                                }
                            }

                            this.FPS91_TY_S_AC_82NGC636.Initialize();
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_82QG2651", TXT01_ARJUNNO.GetValue().ToString().Replace("-","") );
                            this.FPS91_TY_S_AC_82NGC636.SetValue(this.DbConnector.ExecuteDataTable());

                            this.ShowMessage("TY_M_AC_25O8K620");
                        }
                    }

                } //if (fsGubn == "A")..end
                else
                {
                    //전표삭제
                    if (this.TXT01_ARJUNNO.GetValue().ToString() != "")
                    {
                        //미승인전표 -> 임시파일 입력
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_29C7K957", sB2SSID, this.TXT01_ARJUNNO.GetValue().ToString().Substring(0, 6), this.TXT01_ARJUNNO.GetValue().ToString().Substring(7, 8), this.TXT01_ARJUNNO.GetValue().ToString().Substring(16, 3));
                        //미승인 SP호출 파일 입력
                        this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, CBH01_B2HISAB.GetValue(), "D",
                                                                    this.CBH01_B2DPMK.GetValue(), this.DTP01_B2DTMK.GetString(), "", "",
                                                                    "", "", Employer.EmpNo);
                        this.DbConnector.ExecuteTranQueryList();

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, ""); // SP CALL
                        string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar()); // SP의 OUTPUT 값 가져오는 부분
                        if (sOUTMSG.Substring(0, 2) == "ER")
                        {
                            this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                        else
                        {
                            UP_Set_JunPyoNumSave("", "D");
                            this.ShowMessage("TY_M_AC_25O8K620");
                        }
                    }
                    else
                    {
                        this.ShowMessage("TY_M_AC_25O8K620");
                    }
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();  
                }
            }            

            this.ShowMessage("TY_M_MR_31945578");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.CBO01_B2IDJP.GetValue().ToString() == "3")
            {
                if (this.CBH01_B2CDBK.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_2445M440");

                    this.SetFocus(this.CBH01_B2CDBK.CodeText);

                    e.Successed = false;
                    return;
                }


                if (this.CBH01_B2NOAC.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_2445M441");

                    this.SetFocus(this.CBH01_B2NOAC.CodeText);

                    e.Successed = false;
                    return;
                }
            }
            
            if (this.CBO01_B2IDJP.GetValue().ToString() == "1")
            {
                this.ShowCustomMessage("전표구분을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_MR_31943577"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 화물료 납부관리 납부전표번호 저장 이벤트
        private void UP_Set_JunPyoNumSave(string sJpno, string sBatchGubn)
        {
            string sDspJpno = string.Empty;

            if (sBatchGubn == "A")
            {
                sDspJpno = sJpno.Replace("-","").Substring(0, 17);
            }
            else
            {
                sDspJpno = "";
            }

            if (ftdt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < ftdt.Rows.Count; i++)
                {
                    if (sBatchGubn == "A")
                    {
                        // 20190114 수정전 소스
                        //this.DbConnector.Attach("TY_P_AC_82QEP649", sDspJpno + Set_Fill2((i + 1).ToString()),
                        //                                            TYUserInfo.EmpNo,
                        //                                            ftdt.Rows[i]["HMDATE"].ToString().Replace("-","").Trim(),
                        //                                            ftdt.Rows[i]["HMCHDAT"].ToString().Replace("-", "").Trim(),
                        //                                            ftdt.Rows[i]["HMPAYDAT"].ToString().Replace("-", "").Trim(),
                        //                                            ftdt.Rows[i]["HMIPHANG"].ToString().Replace("-", "").Trim(),
                        //                                            ftdt.Rows[i]["HMBONSUN"].ToString(),
                        //                                            ftdt.Rows[i]["HMHWAJU"].ToString()
                        //                       );

                        // 20190114 수정후 소스
                        this.DbConnector.Attach("TY_P_AC_91EH4500", sDspJpno + Set_Fill2((i + 1).ToString()),
                                                                    TYUserInfo.EmpNo,
                                                                    ftdt.Rows[i]["HMDATE"].ToString().Replace("-", "").Trim(),
                                                                    ftdt.Rows[i]["HMCHDAT"].ToString().Replace("-", "").Trim(),
                                                                    ftdt.Rows[i]["HMPAYDAT"].ToString().Replace("-", "").Trim(),
                                                                    ftdt.Rows[i]["HMIPHANG"].ToString().Replace("-", "").Trim(),
                                                                    ftdt.Rows[i]["HMBONSUN"].ToString(),
                                                                    ftdt.Rows[i]["HMHWAJU"].ToString(),
                                                                    ftdt.Rows[i]["HMIPGOGB"].ToString()
                                               );
                    }
                    else
                    {
                        // 20190114 수정전 소스
                        //this.DbConnector.Attach("TY_P_AC_82QEP649", "",
                        //                                            TYUserInfo.EmpNo,
                        //                                            ftdt.Rows[i]["HMDATE"].ToString().Replace("-", "").Trim(),
                        //                                            ftdt.Rows[i]["HMCHDAT"].ToString().Replace("-", "").Trim(),
                        //                                            ftdt.Rows[i]["HMPAYDAT"].ToString().Replace("-", "").Trim(),
                        //                                            ftdt.Rows[i]["HMIPHANG"].ToString().Replace("-", "").Trim(),
                        //                                            ftdt.Rows[i]["HMBONSUN"].ToString(),
                        //                                            ftdt.Rows[i]["HMHWAJU"].ToString()
                        //                       );

                        // 20190114 수정후 소스
                        this.DbConnector.Attach("TY_P_AC_91EH4500", "",
                                                                    TYUserInfo.EmpNo,
                                                                    ftdt.Rows[i]["HMDATE"].ToString().Replace("-", "").Trim(),
                                                                    ftdt.Rows[i]["HMCHDAT"].ToString().Replace("-", "").Trim(),
                                                                    ftdt.Rows[i]["HMPAYDAT"].ToString().Replace("-", "").Trim(),
                                                                    ftdt.Rows[i]["HMIPHANG"].ToString().Replace("-", "").Trim(),
                                                                    ftdt.Rows[i]["HMBONSUN"].ToString(),
                                                                    ftdt.Rows[i]["HMHWAJU"].ToString(),
                                                                    ftdt.Rows[i]["HMIPGOGB"].ToString()
                                               );
                    }
                    
                }

                if (this.DbConnector.CommandCount > 0)
                {
                    this.DbConnector.ExecuteTranQueryList();
                }
            }
        }
        #endregion

        #region  Description : DTP01_B2DTMK_ValueChanged 이벤트
        private void DTP01_B2DTMK_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_B2DPMK.DummyValue = this.DTP01_B2DTMK.GetString();
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion


    }
}
