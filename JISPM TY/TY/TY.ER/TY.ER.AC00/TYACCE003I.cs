using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.GB00;

namespace TY.ER.AC00
{
    /// <summary>
    /// 신용카드결재관리 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.10 17:14
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29A5T907 : 신용카드결재관리 차변 조회
    ///  TY_P_AC_29A63909 : 신용카드결재관리 대변 조회
    ///  TY_P_AC_29BBB913 : 신용카드결재관리 전표 차변 조회
    ///  TY_P_AC_29B3K918 : 신용카드결재관리 결재일자 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29A61911 : 신용카드결재관리 대변 조회
    ///  TY_S_AC_29A65910 : 신용카드결재관리 차변 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_25O8J618 : 전표생성 작업을 하시겠습니까?
    ///  TY_M_AC_25O8K619 : 전표취소 작업을 하시겠습니까?
    ///  TY_M_AC_25O8K620 : 전표처리가  완료되었습니다!
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CANCEL : 취소
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  A6CDBK : 은행코드
    ///  A6NOAC : 계좌번호
    ///  A6ENDD : 결제일
    ///  A6HISAB : 사번
    ///  B2DPMK : 작성부서
    ///  B2DTMK : 작성일자
    ///  B2NOSQ :  일련번호
    /// </summary>
    public partial class TYACCE003I : TYBase
    {
        private string fsB2DPMK;
        private string fsB2DTMK;
        private string fsB2NOSQ;

        private bool _Isloaded = false;
        private bool _IsJpyGubn = false;

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
        //private TYData DAT02_W2HIDAT;
        //private TYData DAT02_W2HITIM;
        private TYData DAT02_W2HISAB;
        private TYData DAT02_W2GUBUN;
        private TYData DAT02_W2TXAMT;
        private TYData DAT02_W2TXVAT;
        private TYData DAT02_W2HWAJU; 

        #region Description : 폼 로드 이벤트
        public TYACCE003I(string sB2DPMK, string sB2DTMK, string sB2NOSQ)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.fsB2DPMK = sB2DPMK;
            this.fsB2DTMK = sB2DTMK;
            this.fsB2NOSQ = sB2NOSQ;
            //차변
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_29A65910, "B4CDAC", "B4CDACNM", "B4CDAC");  //계정과목
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_29A65910, "B4DPAC", "B4DPACNM", "B4DPAC");  //귀속부서
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_29A65910, "B4VLMI1", "B4VLMI1");  //신용카드
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_29A65910, "B4CDSB", "B4CDSB");  //사번
            //대변
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_29A61911, "B2CDAC", "A1NMAC", "B2CDAC");  //계정코드
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_29A61911, "B2CDBK", "B2NMBK", "B2CDBK");  //은행코드
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_29A61911, "B2NOAC", "B2NOAC");  //계좌번호
            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_29A61911, "B2DPAC", "B2DPACNM", "B2DPAC");  //귀속부서

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
            //this.DAT02_W2HIDAT = new TYData("DAT02_W2HIDAT", null);
            //this.DAT02_W2HITIM = new TYData("DAT02_W2HITIM", null);
            this.DAT02_W2HISAB = new TYData("DAT02_W2HISAB", null);
            this.DAT02_W2GUBUN = new TYData("DAT02_W2GUBUN", null);
            this.DAT02_W2TXAMT = new TYData("DAT02_W2TXAMT", null);
            this.DAT02_W2TXVAT = new TYData("DAT02_W2TXVAT", null);
            this.DAT02_W2HWAJU = new TYData("DAT02_W2HWAJU", null);
            
        }

        private void TYACCE003I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_CANCEL.ProcessCheck += new TButton.CheckHandler(BTN61_CANCEL_ProcessCheck);

            this.CBH01_A6CDBK.CodeBoxDataBinded += new TCodeBox.TCodeBoxEventHandler(CBH01_A6CDBK_CodeBoxDataBinded);

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
            //this.ControlFactory.Add(this.DAT02_W2HIDAT);
            //this.ControlFactory.Add(this.DAT02_W2HITIM);
            this.ControlFactory.Add(this.DAT02_W2HISAB);
            this.ControlFactory.Add(this.DAT02_W2GUBUN);
            this.ControlFactory.Add(this.DAT02_W2TXAMT);
            this.ControlFactory.Add(this.DAT02_W2TXVAT);
            this.ControlFactory.Add(this.DAT02_W2HWAJU); 

            //신규등록시
            if (string.IsNullOrEmpty(this.fsB2DPMK))
            {
                UP_Set_NotExistJunPyo();
            }
            else //전표존재시
            {
                UP_Set_ExistJunPyo();
            }
        }
        #endregion

        #region Description : 전표발행 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sB2SSID = "";
            string sW2RKAC = "";

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            int iCnt = 0;

            DataTable dt;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());

            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            this.DbConnector.CommandClear();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                iCnt = iCnt + 1;

                this.DAT02_W2SSID.SetValue(sB2SSID);
                this.DAT02_W2DPMK.SetValue(this.CBH01_B2DPMK.GetValue());
                this.DAT02_W2DTMK.SetValue(this.DTP01_B2DTMK.GetValue());
                this.DAT02_W2NOSQ.SetValue("0");
                this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                this.DAT02_W2IDJP.SetValue("3");
                this.DAT02_W2NOJP.SetValue("");
                this.DAT02_W2CDAC.SetValue(ds.Tables[0].Rows[i]["B4CDAC"].ToString());
                this.DAT02_W2DTAC.SetValue("");
                this.DAT02_W2DTLI.SetValue("");
                this.DAT02_W2DPAC.SetValue(ds.Tables[0].Rows[i]["B4DPAC"].ToString());
                this.DAT02_W2DPAC.GetValue().ToString();

                //관리항목 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_23N3M888", ds.Tables[0].Rows[i]["B4CDAC"].ToString(), "");
                dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                    {
                        this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2));
                    }
                    else
                    {
                        this.DAT02_W2CDMI1.SetValue("");
                    }
                    if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                    {
                        this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2));
                    }
                    else
                    {
                        this.DAT02_W2CDMI2.SetValue("");
                    }
                    if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                    {
                        this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2));
                    }
                    else
                    {
                        this.DAT02_W2CDMI3.SetValue("");
                    }
                    if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                    {
                        this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                    }
                    else
                    {
                        this.DAT02_W2CDMI4.SetValue("");
                    }
                    if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                    {
                        this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2));
                    }
                    else
                    {
                        this.DAT02_W2CDMI5.SetValue("");
                    }
                    if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                    {
                        this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2));
                    }
                    else
                    {
                        this.DAT02_W2CDMI6.SetValue("");
                    }
                }
                dt.Clear();

                //신용카드
                if (ds.Tables[0].Rows[i]["B4CDAC"].ToString() == "21101006")
                {
                    //카드
                    this.DAT02_W2VLMI1.SetValue(ds.Tables[0].Rows[i]["B4VLMI1"].ToString());
                    //일자
                    this.DAT02_W2VLMI2.SetValue(ds.Tables[0].Rows[i]["B4DATE"].ToString());
                    //거래처
                    this.DAT02_W2VLMI3.SetValue(ds.Tables[0].Rows[i]["B4VLMI3"].ToString());
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    sW2RKAC = "카드사용대금결제";
                }

                //외환차손
                if (ds.Tables[0].Rows[i]["B4CDAC"].ToString() == "52000300")
                {
                    this.DAT02_W2VLMI1.SetValue("");
                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");  //자금배부
                    this.DAT02_W2VLMI6.SetValue("");

                    sW2RKAC = "외환차손";
                }

                //잡손실
                if (ds.Tables[0].Rows[i]["B4CDAC"].ToString() == "52002588")
                {
                    this.DAT02_W2VLMI1.SetValue("");
                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");  //자금배부
                    this.DAT02_W2VLMI6.SetValue("");

                    sW2RKAC = "잡손실";
                }

                //송금수수료
                if (ds.Tables[0].Rows[i]["B4CDAC"].ToString() == "42412801" || ds.Tables[0].Rows[i]["B4CDAC"].ToString() == "44122801" ||
                    ds.Tables[0].Rows[i]["B4CDAC"].ToString() == "44112801" || ds.Tables[0].Rows[i]["B4CDAC"].ToString() == "44212801")
                {
                    this.DAT02_W2VLMI1.SetValue("");
                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    sW2RKAC = "송금수수료";
                }

                //기타지급수수료
                if (ds.Tables[0].Rows[i]["B4CDAC"].ToString() == "42412888" || ds.Tables[0].Rows[i]["B4CDAC"].ToString() == "44122888" ||
                    ds.Tables[0].Rows[i]["B4CDAC"].ToString() == "44112888" || ds.Tables[0].Rows[i]["B4CDAC"].ToString() == "44212888")
                {
                    this.DAT02_W2VLMI1.SetValue("");
                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    sW2RKAC = "기타지급수수료";
                }

                //업무가지급금,소송가지급금, 기타가지급금 일경우 사번 체크
                if (ds.Tables[0].Rows[i]["B4CDAC"].ToString() == "11103001" ||
                    ds.Tables[0].Rows[i]["B4CDAC"].ToString() == "11103002" || 
                    ds.Tables[0].Rows[i]["B4CDAC"].ToString() == "11103088")
                {
                    this.DAT02_W2VLMI1.SetValue(ds.Tables[0].Rows[i]["B4CDSB"].ToString()); //사번
                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    sW2RKAC = "카드사용대금";
                }

                this.DAT02_W2AMDR.SetValue(ds.Tables[0].Rows[i]["B4AMT"].ToString());
                this.DAT02_W2AMCR.SetValue("0");

                this.DAT02_W2CDFD.SetValue("");
                this.DAT02_W2AMFD.SetValue("0");
                this.DAT02_W2RKAC.SetValue(sW2RKAC);
                this.DAT02_W2RKCU.SetValue("");
                this.DAT02_W2WCJP.SetValue(ds.Tables[0].Rows[i]["B4NOJP"].ToString());
                this.DAT02_W2PRGB.SetValue("");
                this.DAT02_W2HIGB.SetValue("A");
                this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
                this.DAT02_W2GUBUN.SetValue("");
                this.DAT02_W2TXAMT.SetValue("0");
                this.DAT02_W2TXVAT.SetValue("0");
                this.DAT02_W2HWAJU.SetValue("");

                //this.DbConnector.Attach("TY_P_AC_29DA5966", this.ControlFactory, "02"); //차변 저장

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
                                        this.DAT02_W2GUBUN.GetValue().ToString(),
                                        this.DAT02_W2TXAMT.GetValue().ToString(),
                                        this.DAT02_W2TXVAT.GetValue().ToString(),
                                        this.DAT02_W2HWAJU.GetValue().ToString()});
            }

            // 대변
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                iCnt = iCnt + 1;

                this.DAT02_W2SSID.SetValue(sB2SSID);
                this.DAT02_W2DPMK.SetValue(this.CBH01_B2DPMK.GetValue());
                this.DAT02_W2DTMK.SetValue(this.DTP01_B2DTMK.GetValue());
                this.DAT02_W2NOSQ.SetValue("0");
                this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                this.DAT02_W2IDJP.SetValue("3");
                this.DAT02_W2NOJP.SetValue("");
                this.DAT02_W2CDAC.SetValue(ds.Tables[1].Rows[i]["B2CDAC"].ToString());
                this.DAT02_W2DTAC.SetValue("");
                this.DAT02_W2DTLI.SetValue("");
                this.DAT02_W2DPAC.SetValue(ds.Tables[1].Rows[i]["B2DPAC"].ToString());

                //관리항목 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_23N3M888", ds.Tables[1].Rows[i]["B2CDAC"].ToString(), "");
                dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                    {
                        this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2));
                    }
                    else
                    {
                        this.DAT02_W2CDMI1.SetValue("");
                    }
                    if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                    {
                        this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2));
                    }
                    else
                    {
                        this.DAT02_W2CDMI2.SetValue("");
                    }
                    if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                    {
                        this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2));
                    }
                    else
                    {
                        this.DAT02_W2CDMI3.SetValue("");
                    }
                    if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                    {
                        this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                    }
                    else
                    {
                        this.DAT02_W2CDMI4.SetValue("");
                    }
                    if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                    {
                        this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2));
                    }
                    else
                    {
                        this.DAT02_W2CDMI5.SetValue("");
                    }
                    if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                    {
                        this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2));
                    }
                    else
                    {
                        this.DAT02_W2CDMI6.SetValue("");
                    }
                }
                dt.Clear();

                //전도금
                if (ds.Tables[1].Rows[i]["B2CDAC"].ToString() == "11100200")
                {
                    //부서코드
                    this.DAT02_W2VLMI1.SetValue(ds.Tables[1].Rows[i]["B2DPAC"].ToString());
                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    sW2RKAC = "카드사용대금결제";
                }
                //제예금(보통예금,당좌예금)
                if (ds.Tables[1].Rows[i]["B2CDAC"].ToString() == "11100302" || ds.Tables[1].Rows[i]["B2CDAC"].ToString() == "11100301")
                {
                    //은행
                    this.DAT02_W2VLMI1.SetValue(ds.Tables[1].Rows[i]["B2CDBK"].ToString());
                    //계좌
                    this.DAT02_W2VLMI2.SetValue(ds.Tables[1].Rows[i]["B2NOAC"].ToString());
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    sW2RKAC = "카드사용대금결제";
                }

                // 외환차익
                if (ds.Tables[1].Rows[i]["B2CDAC"].ToString() == "51000500")
                {
                    this.DAT02_W2VLMI1.SetValue("");
                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");  //자금배부
                    this.DAT02_W2VLMI6.SetValue("");

                    sW2RKAC = "외환차익";
                }


                this.DAT02_W2AMDR.SetValue("0");
                this.DAT02_W2AMCR.SetValue(ds.Tables[1].Rows[i]["B2AMT"].ToString());

                this.DAT02_W2CDFD.SetValue("");
                this.DAT02_W2AMFD.SetValue("0");
                this.DAT02_W2RKAC.SetValue(sW2RKAC);
                this.DAT02_W2RKCU.SetValue("");
                this.DAT02_W2WCJP.SetValue("");
                this.DAT02_W2PRGB.SetValue("");
                this.DAT02_W2HIGB.SetValue("A");
                this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
                this.DAT02_W2GUBUN.SetValue("");
                this.DAT02_W2TXAMT.SetValue("0");
                this.DAT02_W2TXVAT.SetValue("0");
                this.DAT02_W2HWAJU.SetValue("");
                //this.DbConnector.Attach("TY_P_AC_29DA5966", this.ControlFactory, "02"); //대변 저장                

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
                                        this.DAT02_W2GUBUN.GetValue().ToString(),
                                        this.DAT02_W2TXAMT.GetValue().ToString(),
                                        this.DAT02_W2TXVAT.GetValue().ToString(),
                                        this.DAT02_W2HWAJU.GetValue().ToString()});

            }

            if (datas.Count > 0)
            {
                this.DbConnector.CommandClear();
                foreach (object[] data in datas)
                {
                    this.DbConnector.Attach("TY_P_AC_29DA5966", data); //대변 저장                
                }
            }

            //미승인 SP호출 파일 입력
            this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, CBH01_A6HISAB.GetValue(), "A",
                                                        this.CBH01_B2DPMK.GetValue(), this.DTP01_B2DTMK.GetValue(), "", "",
                                                        "", "", Employer.EmpNo);
            this.DbConnector.ExecuteTranQueryList();

            //전표 생성 함수 호출
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, "");
            string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
            if (sOUTMSG.Substring(0, 2) == "ER")
            {
                this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            else
            {
                //this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);
                this.ShowMessage("TY_M_AC_25O8K620");
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
                            fsB2DPMK = sJpno.Substring(0, 6);
                            fsB2DTMK = sJpno.Substring(7, 8);
                            fsB2NOSQ = sJpno.Substring(16, 3);

                            UP_Set_ExistJunPyo(); 
                        } 
                    }
                }
            }
        }
        #endregion

        #region Description : 전표 취소 버튼 이벤트
        private void BTN61_CANCEL_Click(object sender, EventArgs e)
        {
            string sB2SSID = "";

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());

            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            //미승인전표 -> 임시파일 입력
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7K957", sB2SSID, this.CBH01_B2DPMK.GetValue(), this.DTP01_B2DTMK.GetValue(), this.TXT01_B2NOSQ.GetValue());
            //미승인 SP호출 파일 입력
            this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, CBH01_A6HISAB.GetValue(), "D", 
                                                        this.CBH01_B2DPMK.GetValue(), this.DTP01_B2DTMK.GetValue(),"","",
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
                //this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);
                this.ShowMessage("TY_M_AC_25O8K620");
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();  

        }
        #endregion        

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            //차변 조회
            string[] sA6ENDATE = UP_Get_AppSection(this.CBH01_A6CDBK.GetValue().ToString(), this.CBH01_A6NOAC.GetValue().ToString(), this.DTP01_A6ENDD.GetString().ToString());      

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29A5T907", sA6ENDATE[0].ToString(), sA6ENDATE[1].ToString(), this.CBH01_A6CDBK.GetValue(), this.CBH01_A6NOAC.GetValue(), this.DTP01_A6ENDD.GetString().Substring(6, 2));
            this.FPS91_TY_S_AC_29A65910.SetValue(this.DbConnector.ExecuteDataTable());

            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_29A65910, "B4CDACNM", "합 계", SumRowType.Total, "B4AMT");

            for (int i = 0; i < this.FPS91_TY_S_AC_29A65910.CurrentRowCount - 1; i++)
            {
                this.FPS91_TY_S_AC_29A65910.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";
            }

            //대변조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29A63909", fsB2DPMK, fsB2DTMK, fsB2NOSQ);
            this.FPS91_TY_S_AC_29A61911.SetValue(this.DbConnector.ExecuteDataTable());
            
        }
        #endregion               

        #region Description : 사용자 정의 함수
        private void UP_SetReadOnly(bool TrueAndFalse)
        {
            this.DTP01_B2DTMK.SetReadOnly(TrueAndFalse);
            this.CBH01_B2DPMK.SetReadOnly(TrueAndFalse);
            this.TXT01_B2NOSQ.SetReadOnly(TrueAndFalse);
            this.CBH01_A6CDBK.SetReadOnly(TrueAndFalse);
            this.CBH01_A6NOAC.SetReadOnly(TrueAndFalse);
            this.DTP01_A6ENDD.SetReadOnly(TrueAndFalse);
            this.CBH01_A6HISAB.SetReadOnly(TrueAndFalse);
        }

        private void UP_Select_JunPyo(string sB2DPMK, string sB2DTMK, string sB2NOSQ)
        {
            //차변 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29BBB913", sB2DPMK, sB2DTMK, sB2NOSQ);
            this.FPS91_TY_S_AC_29A65910.SetValue(this.DbConnector.ExecuteDataTable());

            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_29A65910, "B4CDACNM", "합 계", SumRowType.Total, "B4AMT");

            //대변 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29A63909", sB2DPMK, sB2DTMK, sB2NOSQ);
            DataTable dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_AC_29A61911.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                this.CBH01_A6CDBK.SetValue(dt.Rows[0]["B2CDBK"].ToString());
                this.CBH01_A6NOAC.SetValue(dt.Rows[0]["B2NOAC"].ToString());                
                this.DTP01_A6ENDD.SetValue(dt.Rows[0]["B2DTMK"].ToString());
                this.CBH01_A6HISAB.SetValue(dt.Rows[0]["B2HISAB"].ToString());  
            }
        }
        
        public string[] UP_Get_AppSection(string sA6CDBK, string sA6NOAC, string sA6ENDD)
        {
            string[] s결제일자구간 = new string[2];

            int iYYYY1 = 0, iYYYY2 = 0;
            int iMM1 = 0, iMM2 = 0;
            int iDD1 = 0, iDD2 = 0;

            string s시작년월 = string.Empty;
            string s종료년월 = string.Empty;

            string s시작월구분 = string.Empty;
            string s시작결제일 = string.Empty;
            string s종료월구분 = string.Empty;
            string s종료결제일 = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29B3K918", sA6CDBK, sA6NOAC, sA6ENDD.ToString().Substring(6,2));
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if( dt.Rows.Count > 0 )
            {
                s시작월구분 = dt.Rows[0]["A6STGB"].ToString();
                s시작결제일 = dt.Rows[0]["A6GJST"].ToString();
                s종료월구분 = dt.Rows[0]["A6EDGB"].ToString();
                s종료결제일 = dt.Rows[0]["A6GJED"].ToString();
            }

            if (sA6ENDD.Trim().Length >= 8 && sA6CDBK.Trim().Length >= 4)
            {
                iYYYY1 = Convert.ToInt16(sA6ENDD.Substring(0, 4));
                iMM1 = Convert.ToInt16(sA6ENDD.Substring(4, 2));

                iYYYY2 = Convert.ToInt16(sA6ENDD.Substring(0, 4));
                iMM2 = Convert.ToInt16(sA6ENDD.Substring(4, 2));

                //----------------------------------------------------------------------------//
                //                                 시작일 세팅                                //
                //----------------------------------------------------------------------------//
                #region 시작일
                if (s시작월구분 == "1") //전전월
                {
                    if (iMM2 == 1)
                    {
                        iYYYY2 = iYYYY2 - 1;
                        iMM2 = 11;
                    }
                    else if (iMM2 == 2)
                    {
                        iYYYY2 = iYYYY2 - 1;
                        iMM2 = 12;
                    }
                    else
                    {
                        iMM2 = iMM2 - 2;
                    }
                }

                if (s시작월구분 == "2") //전월
                {
                    if (iMM2 == 1)
                    {
                        iYYYY2 = iYYYY2 - 1;
                        iMM2 = 12;
                    }
                    else
                    {
                        iMM2 = iMM2 - 1;
                    }
                }

                s시작결제일 = Get_Numeric(s시작결제일);

                iDD2 = Convert.ToInt16(s시작결제일.ToString());

                s시작년월 = iYYYY2.ToString() + iMM2.ToString("0#") + iDD2.ToString("0#");
                #endregion

                //----------------------------------------------------------------------------//
                //                                 종료일 세팅                                //
                //----------------------------------------------------------------------------//
                #region 종료일
                if (s종료월구분 == "1") //전전월
                {
                    if (iMM1 == 1)
                    {
                        iYYYY1 = iYYYY1 - 1;
                        iMM1 = 11;
                    }
                    else if (iMM1 == 2)
                    {
                        iYYYY1 = iYYYY1 - 1;
                        iMM1 = 12;
                    }
                    else
                    {
                        iMM1 = iMM1 - 2;
                    }
                }

                if (s종료월구분 == "2") //전월
                {
                    if (iMM1 == 1)
                    {
                        iYYYY1 = iYYYY1 - 1;
                        iMM1 = 12;
                    }
                    else
                    {
                        iMM1 = iMM1 - 1;
                    }
                }

                s종료결제일 = Get_Numeric(s종료결제일);

                iDD1 = Convert.ToInt16(s종료결제일.ToString());

                s종료년월 = iYYYY1.ToString() + iMM1.ToString("0#") + iDD1.ToString("0#");
                #endregion

            }
            s결제일자구간[0] = s시작년월;
            s결제일자구간[1] = s종료년월;

            return s결제일자구간;
        }

        private void UP_Set_NotExistJunPyo()
        {
            _IsJpyGubn = false;

            UP_SetReadOnly(false);

            this.BTN61_INQ.Visible = true;
            this.BTN61_SAV.Visible = true;
            this.BTN61_CANCEL.Visible = false;
            this.BTN61_PRT.Visible = false;

            this.DTP01_B2DTMK.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_A6ENDD.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_B2DPMK.DummyValue = this.DTP01_B2DTMK.GetValue();

            //this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.DTP01_B2DTMK);
        }

        private void UP_Set_ExistJunPyo()
        {
            _IsJpyGubn = true;

            this.DTP01_B2DTMK.SetValue(fsB2DTMK);
            this.CBH01_B2DPMK.DummyValue = this.DTP01_B2DTMK.GetValue();
            this.CBH01_B2DPMK.SetValue(fsB2DPMK);
            this.TXT01_B2NOSQ.SetValue(fsB2NOSQ);

            UP_Select_JunPyo(fsB2DPMK, fsB2DTMK, fsB2NOSQ);

            UP_SetReadOnly(true);

            this.BTN61_INQ.Visible = false;
            this.BTN61_SAV.Visible = false;
            this.BTN61_CANCEL.Visible = true;
            this.BTN61_PRT.Visible = true;
        }
        #endregion

        #region Description : 계좌번호 코드 헬프 처리
        private void CBH01_A6CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_A6CDBK.GetValue().ToString();
            this.CBH01_A6NOAC.DummyValue = groupCode;
            this.CBH01_A6NOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_A6NOAC.Initialize();
        }
        #endregion

        #region Description : DTP01_B2DTMK_ValueChanged 이벤트
        private void DTP01_B2DTMK_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_B2DPMK.DummyValue = this.DTP01_B2DTMK.GetValue();
        }
        #endregion

        #region Description : FPS91_TY_S_AC_29A61911_EnterCell(대변) 이벤트
        private void FPS91_TY_S_AC_29A61911_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column == 8)
            {
                string groupCode = FPS91_TY_S_AC_29A61911.GetValue(e.Row, "B2CDBK").ToString();
                TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_29A61911, "B2NOAC");
                if (tyCodeBox != null)
                    tyCodeBox.DummyValue = groupCode;
                    tyCodeBox.SetReadOnly(string.IsNullOrEmpty(groupCode));
                if (this._Isloaded) tyCodeBox.Initialize();
            }

            if (e.Column == 12)
            {
                string year =  this.DTP01_A6ENDD.GetString().ToString();
                TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_29A61911, "B2DPAC");
                if (tyCodeBox != null)
                    tyCodeBox.DummyValue = year;
            }

            return;
        }
        #endregion

        #region Description : FPS91_TY_S_AC_29A61911_RowInserted(대변) 이벤트
        private void FPS91_TY_S_AC_29A61911_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            //신규등록시
            if (_IsJpyGubn == false)
            {
                int row = this.FPS91_TY_S_AC_29A65910.CurrentRowCount -1;

                double dB2AMT = 0;

                for (int i = 0; i < this.FPS91_TY_S_AC_29A65910.CurrentRowCount - 1; i++)
                {
                    dB2AMT += Convert.ToDouble(this.FPS91_TY_S_AC_29A65910.GetValue(i, "B4AMT").ToString());
                }

                this.FPS91_TY_S_AC_29A61911.SetValue(e.RowIndex, "B2AMT", dB2AMT.ToString());
            }
        }
        #endregion

        #region Description : FPS91_TY_S_AC_29A65910_EnterCell(차변) 이벤트
        private void FPS91_TY_S_AC_29A65910_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column == 12)
            {
                string groupCode = this.CBH01_A6CDBK.GetValue().ToString();
                TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_29A65910, "B4VLMI1");
                if (tyCodeBox != null)
                    tyCodeBox.DummyValue = groupCode;
                    tyCodeBox.SetReadOnly(string.IsNullOrEmpty(groupCode));
                if (this._Isloaded) tyCodeBox.Initialize();
            }

            if (e.Column == 9)
            {
                string year = this.DTP01_A6ENDD.GetString().ToString();
                TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_29A65910, "B4DPAC");
                if (tyCodeBox != null)
                    tyCodeBox.DummyValue = year;

            }
        

            return; 
        }
        #endregion

        #region Description : FPS91_TY_S_AC_29A65910_RowInserted(차변) 이벤트
        private void FPS91_TY_S_AC_29A65910_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            if (_IsJpyGubn == false)
            {
                if (this.CBH01_A6CDBK.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_2445M440");
                    return;
                }
            }
        }
        #endregion

        #region Description : FPS91_TY_S_AC_29A65910_KeyDown(차변) 이벤트
        private void FPS91_TY_S_AC_29A65910_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            int iRowIndex = 0;

            if (e.KeyCode == System.Windows.Forms.Keys.F1 && FPS91_TY_S_AC_29A65910.ActiveSheet.ActiveColumnIndex == 11)
            {
                TYAZCE03C1 popup = new TYAZCE03C1(FPS91_TY_S_AC_29A65910.GetValue(FPS91_TY_S_AC_29A65910.ActiveSheet.ActiveRowIndex, "B4CDAC").ToString(), CBH01_A6CDBK.GetValue().ToString(), CBH01_A6NOAC.GetValue().ToString());

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //FPS91_TY_S_AC_29A65910.SetValue(FPS91_TY_S_AC_29A65910.ActiveSheet.ActiveRowIndex, "B4NOJP", popup.fsSJJPNO);
                    //FPS91_TY_S_AC_29A65910.SetValue(FPS91_TY_S_AC_29A65910.ActiveSheet.ActiveRowIndex, "B4AMT", popup.fsB7AMAT);
                    //FPS91_TY_S_AC_29A65910.SetValue(FPS91_TY_S_AC_29A65910.ActiveSheet.ActiveRowIndex, "B4VLMI1", popup.fsB7CRDT);
                    //FPS91_TY_S_AC_29A65910.SetValue(FPS91_TY_S_AC_29A65910.ActiveSheet.ActiveRowIndex, "B4DATE", popup.fsB7DTAC);
                    //FPS91_TY_S_AC_29A65910.SetValue(FPS91_TY_S_AC_29A65910.ActiveSheet.ActiveRowIndex, "B4VLMI3", popup.fsB7VEND);

                    //this.FPS91_TY_S_AC_296AJ855.ActiveSheet.AddRows(0, iRowCnt);
                    //for (int i = 0; i < iRowCnt; i++)
                    //{
                    //    this.FPS91_TY_S_AC_296AJ855.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";
                    //}

                    DataTable dt = new DataTable();

                    dt = popup.ftDataTable;
                    
                    if (dt.Rows.Count > 0)
                    {
                        iRowIndex = FPS91_TY_S_AC_29A65910.ActiveSheet.ActiveRowIndex;

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (i > 0)
                            {
                                this.FPS91_TY_S_AC_29A65910.ActiveSheet.AddRows(iRowIndex + i, 1);
                                this.FPS91_TY_S_AC_29A65910.ActiveSheet.RowHeader.Cells[iRowIndex + i, 0].Text = "N";
                            }                            

                            this.FPS91_TY_S_AC_29A65910.ActiveSheet.Cells[iRowIndex + i, 6].Text = FPS91_TY_S_AC_29A65910.GetValue(FPS91_TY_S_AC_29A65910.ActiveSheet.ActiveRowIndex, "B4CDAC").ToString();
                            this.FPS91_TY_S_AC_29A65910.ActiveSheet.Cells[iRowIndex + i, 7].Text = FPS91_TY_S_AC_29A65910.GetValue(FPS91_TY_S_AC_29A65910.ActiveSheet.ActiveRowIndex, "B4CDACNM").ToString();

                            this.FPS91_TY_S_AC_29A65910.ActiveSheet.Cells[iRowIndex + i, 9].Text = FPS91_TY_S_AC_29A65910.GetValue(FPS91_TY_S_AC_29A65910.ActiveSheet.ActiveRowIndex, "B4DPAC").ToString();
                            this.FPS91_TY_S_AC_29A65910.ActiveSheet.Cells[iRowIndex + i, 10].Text = FPS91_TY_S_AC_29A65910.GetValue(FPS91_TY_S_AC_29A65910.ActiveSheet.ActiveRowIndex, "B4DPACNM").ToString();

                            this.FPS91_TY_S_AC_29A65910.ActiveSheet.Cells[iRowIndex + i, 11].Text = dt.Rows[i]["SJJPNO"].ToString();
                            this.FPS91_TY_S_AC_29A65910.ActiveSheet.Cells[iRowIndex + i, 16].Text = dt.Rows[i]["B7AMAT"].ToString();
                            this.FPS91_TY_S_AC_29A65910.ActiveSheet.Cells[iRowIndex + i, 12].Text = dt.Rows[i]["B2VLMI1"].ToString();
                            this.FPS91_TY_S_AC_29A65910.ActiveSheet.Cells[iRowIndex + i, 14].Text = Set_Date(dt.Rows[i]["B7DTAC"].ToString());
                            this.FPS91_TY_S_AC_29A65910.ActiveSheet.Cells[iRowIndex + i, 13].Text = dt.Rows[i]["B7VEND"].ToString();
                        }
                    }
                }
            }
        }
        #endregion

        #region Description : 전표발행 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sB4DATE = string.Empty;

            double dDRAmt = 0;
            double dCRAmt = 0;
            bool bCheck = false;

            DataSet ds = new DataSet();

            // 차변
            ds.Tables.Add(this.FPS91_TY_S_AC_29A65910.GetDataSourceInclude(TSpread.TActionType.New, "B4CDAC", "B4DPAC", "B4NOJP", "B4VLMI1", "B4VLMI3", "B4DATE", "B4AMT" ,"B4CDSB"));
            // 대변
            ds.Tables.Add(this.FPS91_TY_S_AC_29A61911.GetDataSourceInclude(TSpread.TActionType.New, "B2CDAC", "B2CDBK", "B2NOAC", "B2DPAC", "B2AMT"));

            if (ds.Tables[0].Rows.Count == 0 )
            {
                this.ShowMessage("TY_M_AC_29C5D948");
                e.Successed = false;
                return;
            }

            if (ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_29C5D949");
                e.Successed = false;
                return;
            }

            // 20180726 수정(황성환 요청)
            // 반제일자가 마지막 사용일자보다 커야 함
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    sB4DATE = ds.Tables[0].Rows[i]["B4DATE"].ToString();
                }
                else
                {
                    if (int.Parse(sB4DATE.ToString()) < int.Parse(ds.Tables[0].Rows[i]["B4DATE"].ToString()))
                    {
                        sB4DATE = ds.Tables[0].Rows[i]["B4DATE"].ToString();
                    }
                }
            }

            if (int.Parse(sB4DATE.ToString()) > int.Parse(Get_Date(this.DTP01_B2DTMK.GetValue().ToString())))
            {
                this.ShowMessage("TY_M_AC_882CF498");

                this.SetFocus(this.DTP01_B2DTMK);

                e.Successed = false;
                return;
            }
          
            // 차변 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dDRAmt = dDRAmt + Convert.ToDouble(ds.Tables[0].Rows[i]["B4AMT"].ToString()); 

                //카드계정일경우 원천번호, 카드번호 체크
                if (ds.Tables[0].Rows[i]["B4CDAC"].ToString() == "21101006")
                {
                    if (ds.Tables[0].Rows[i]["B4NOJP"].ToString() == "") //원천번호
                    {
                        this.ShowMessage("TY_M_AC_37FBY101");
                        e.Successed = false;
                        return;
                    }
                    if (ds.Tables[0].Rows[i]["B4VLMI1"].ToString() == "") //카드번호
                    {
                        this.ShowMessage("TY_M_AC_37FBZ102");
                        e.Successed = false;
                        return;
                    }
                    if (ds.Tables[0].Rows[i]["B4VLMI3"].ToString() == "") //거래처
                    {
                        this.ShowMessage("TY_M_MR_2CA6N031");
                        e.Successed = false;
                        return;
                    }

                    //원천번호 존재시 반제잔액을 초과하는지 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_6C6A4959", ds.Tables[0].Rows[i]["B4NOJP"].ToString());
                    DataTable dwc = this.DbConnector.ExecuteDataTable();
                    if (dwc.Rows.Count > 0)
                    {
                        if (Convert.ToDouble(ds.Tables[0].Rows[i]["B4AMT"].ToString()) > Convert.ToDouble(dwc.Rows[0]["B7AMJN"].ToString()))
                        {
                            this.ShowCustomMessage("원천번호: "+ds.Tables[0].Rows[i]["B4NOJP"].ToString() + "의 잔액을 확인하세요! ", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            e.Successed = false;
                            return;
                        }
                    }
                    else
                    {
                        this.ShowCustomMessage("존재하지 않는 원천번호 입니다 (" + ds.Tables[0].Rows[i]["B4NOJP"].ToString() + ")", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
                //업무가지급금,소송가지급금, 기타가지급금 일경우 사번 체크
                if (ds.Tables[0].Rows[i]["B4CDAC"].ToString() == "11103001" ||
                    ds.Tables[0].Rows[i]["B4CDAC"].ToString() == "11103002" || 
                    ds.Tables[0].Rows[i]["B4CDAC"].ToString() == "11103088")
                {
                    if (ds.Tables[0].Rows[i]["B4CDSB"].ToString() == "") //사번
                    {
                        this.ShowMessage("TY_M_AC_3AB5A047");
                        e.Successed = false;
                        return;
                    }
                }

                //차변에 같은 원천번호가 동시 존재 할수 없다.
                bCheck = UP_WNJPDataTableCheck(ds.Tables[0], ds.Tables[0].Rows[i]["B4NOJP"].ToString(), i);
                if (bCheck)
                {
                    this.ShowMessage("TY_M_AC_6BABW707");
                    e.Successed = false;
                    return;
                }
            }

            // 대변 체크
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                dCRAmt = dCRAmt + Convert.ToDouble(ds.Tables[1].Rows[i]["B2AMT"].ToString()); 
            }

            //차/대 금액 비교
            if (dDRAmt != dCRAmt)
            {
                this.ShowMessage("TY_M_AC_29D5Z005");
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

        #region Description : 전표취소 ProcessCheck 이벤트
        private void BTN61_CANCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_AC_25O8K619"))
            {
                e.Successed = false;
                return;
            }         
        }
        #endregion

        #region Description : 전표출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sJPNO = this.CBH01_B2DPMK.GetValue().ToString() + this.DTP01_B2DTMK.GetString().ToString() + Set_Fill3(this.TXT01_B2NOSQ.GetValue().ToString());

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_2AU2M916",
                sJPNO.Substring(0, 6),
                sJPNO.Substring(6, 8),
                sJPNO.Substring(14, 3), // 시작 번호
                sJPNO.Substring(14, 3)  // 종료 번호
                );

            if (Convert.ToDouble(sJPNO.Substring(6, 4)) > 2014)
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
            this.SetFocus(this.TXT01_B2NOSQ);
        }
        #endregion

        #region Description : 동일 원천번호 체크 함수
        private bool UP_WNJPDataTableCheck(DataTable dt, string sNOJP, int iIndex)
        {
            bool bCheck = false;
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if( i != iIndex )
                {
                    if (dt.Rows[i]["B4NOJP"].ToString() == sNOJP)
                    {
                        bCheck = true;
                        return bCheck; 
                    }
                }
            }

            return bCheck;
        }
        #endregion

        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion


    }
}
