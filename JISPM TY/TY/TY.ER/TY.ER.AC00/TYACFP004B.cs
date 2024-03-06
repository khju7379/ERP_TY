using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 미지급금전표발행 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.20 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29C7K957 : 미승인전표 임시파일 입력
    ///  TY_P_AC_29C7O959 : 미승인전표 SP호출 이력 저장
    ///  TY_P_AC_29C80960 : 미승인전표 SP 호출
    ///  TY_P_AC_29DA5966 : 미승인전표 임시파일 등록
    ///  TY_P_AC_29K40214 : 미지급금 전표번호 조회
    ///  TY_P_AC_29K48213 : 미지급금 전표생성 조회
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  M1GUBN : 지급형태
    ///  M1WNJP : 설정전표번호
    ///  M1DTED : 지급일자
    ///  B2DPMK : 작성부서
    ///  BO1HISAB : 생성사번
    ///  GOKCR : 생성구분
    /// </summary>
    public partial class TYACFP004B : TYBase
    {
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
        //private TYData DAT02_W2HIDAT;
        //private TYData DAT02_W2HITIM;
        private TYData DAT02_W2HISAB;
        private TYData DAT02_W2GUBUN;
        private TYData DAT02_W2TXAMT;
        private TYData DAT02_W2TXVAT;
        private TYData DAT02_W2HWAJU;

        #region Description : 폼로드 버튼 이벤트
        public TYACFP004B()
        {
            InitializeComponent();

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

        private void TYACFP004B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

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

            this.CBH01_B2CDBK.CodeBoxDataBinded += new TYCodeBox.TCodeBoxEventHandler(CBH01_B2CDBK_CodeBoxDataBinded);

            this.CBO01_M1GUBN.SelectedIndex = 0;
            this.CBO01_M1GUBN_SelectedIndexChanged(null, null); 
 
            this.DTP01_M1DTED.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_B2DPMK.DummyValue = this.DTP01_M1DTED.GetValue();            
            
        }
        #endregion

        #region Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {

            int iroop = 0;
            int idtCnt = 0;
            string sEndFlag = "";

            if (this.CBO01_GOKCR.GetValue().ToString() == "A")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29K48213", this.DTP01_M1DTED.GetValue(), this.CBO01_M1GUBN.GetValue());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                idtCnt = dt.Rows.Count;

                if (idtCnt <= 0)
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                    return;
                }

                if (idtCnt > 49)
                {
                    iroop = Convert.ToInt16(Math.Round(Convert.ToDouble(idtCnt / 49) + 0.5, 0, MidpointRounding.AwayFromZero));
                }
                else
                {
                    iroop = 1;
                }

                int iStartPoint = 0;
                int iEndPoint = 0;
                int iRowCount = 0;
                sEndFlag = "S";

                for (int i = 1; i <= iroop; i++)
                {
                    if (iRowCount <= 0)
                    {
                        iStartPoint = i + iRowCount - 1;
                    }
                    else
                    {
                        iStartPoint = iRowCount + 1;
                    }
                    iEndPoint = i * 48;
                    if (iEndPoint >= idtCnt)
                    {
                        iEndPoint = idtCnt - 1;
                        sEndFlag = "E";
                    }

                    bool bresult = UP_Set_JpCreate(dt, iStartPoint, iEndPoint, sEndFlag);

                    if (bresult == false) return;

                    iRowCount = iRowCount + 48;
                }

                this.ShowMessage("TY_M_AC_25O8K620");
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29OA4241", this.DTP01_M1DTED.GetValue(), this.CBO01_M1GUBN.GetValue(), this.CBO01_M1WNJP.SelectedValue.ToString());
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    bool bresult = UP_Set_JpCanCel(dt);

                    if (bresult)
                    {
                        this.ShowMessage("TY_M_AC_25O8K620");
                    }
                }
                else
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                    return;
                }
            }
        }
        #endregion

        #region Description : 전표 생성 이벤트
        private bool UP_Set_JpCreate(DataTable dj, int iSInx, int iEInx, string sEndFlag)
        {
            bool bresult = false;
            int iCnt = 0;
            string sB2SSID = "";
            string sW2RKAC = "";

            double dB2AMDR_Hap = 0;

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();
            //System.Collections.Generic.List<object[]> datasCR = new System.Collections.Generic.List<object[]>();

            DataTable dt = new DataTable();

            try
            {
                //BATID번호 부여
                sB2SSID = UP_Get_BatChSSID();

                if (dj.Rows.Count > 0)
                {
                    //차변
                    #region Description : 차변 생성
                    for (int i = iSInx; i <= iEInx; i++)
                    {
                        iCnt = iCnt + 1;

                        dt.Clear();

                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(this.CBH01_B2DPMK.GetValue());
                        this.DAT02_W2DTMK.SetValue(this.DTP01_M1DTED.GetString());
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue("3");
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue(dj.Rows[i]["M1WNCDAC"].ToString());
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(this.CBH01_B2DPMK.GetValue());

                        //관리항목 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_23N3M888", dj.Rows[i]["M1WNCDAC"].ToString(), "");
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

                        //거래처
                        this.DAT02_W2VLMI1.SetValue(dj.Rows[i]["M1VNCD"].ToString());
                        this.DAT02_W2VLMI2.SetValue("");
                        this.DAT02_W2VLMI3.SetValue("");
                        this.DAT02_W2VLMI4.SetValue("");
                        this.DAT02_W2VLMI5.SetValue("");
                        this.DAT02_W2VLMI6.SetValue("");

                        dB2AMDR_Hap = dB2AMDR_Hap + Convert.ToDouble(dj.Rows[i]["M1AMT"].ToString());  

                        this.DAT02_W2AMDR.SetValue(dj.Rows[i]["M1AMT"].ToString());
                        this.DAT02_W2AMCR.SetValue("0");
                        sW2RKAC = dj.Rows[i]["M1RKAC"].ToString();
                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");
                        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                        this.DAT02_W2RKCU.SetValue(dj.Rows[i]["M1VNCDNM"].ToString());
                        this.DAT02_W2WCJP.SetValue(dj.Rows[i]["M1WNJP"].ToString());
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(this.CBH01_BO1HISAB.GetValue());
                        this.DAT02_W2GUBUN.SetValue("");
                        this.DAT02_W2TXAMT.SetValue("0");
                        this.DAT02_W2TXVAT.SetValue("0");
                        this.DAT02_W2HWAJU.SetValue("");

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
                    #endregion

                    //대변  
                    if (CBO01_M1GUBN.GetValue().ToString() == "1") //현금
                    {
                        #region Description : 대변 생성 (현금)
                        for (int i = iSInx; i <= iSInx; i++)
                        {
                            iCnt = iCnt + 1;

                            dt.Clear();

                            string sW2CDAC = "";

                            if (CBO01_M1GUBN.GetValue().ToString() == "1") //현금
                            {
                                sW2CDAC = this.CBH01_B2CDAC.GetValue().ToString();
                            }
                            else //어음
                            {
                                sW2CDAC = dj.Rows[i]["B2CDAC"].ToString();
                            }

                            this.DAT02_W2SSID.SetValue(sB2SSID);
                            this.DAT02_W2DPMK.SetValue(this.CBH01_B2DPMK.GetValue());
                            this.DAT02_W2DTMK.SetValue(this.DTP01_M1DTED.GetString());
                            this.DAT02_W2NOSQ.SetValue("0");
                            this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                            this.DAT02_W2IDJP.SetValue("3");
                            this.DAT02_W2NOJP.SetValue("");
                            this.DAT02_W2CDAC.SetValue(sW2CDAC);
                            this.DAT02_W2DTAC.SetValue("");
                            this.DAT02_W2DTLI.SetValue("");
                            this.DAT02_W2DPAC.SetValue(this.CBH01_B2DPMK.GetValue());

                            //관리항목 
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_23N3M888", sW2CDAC, "");
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

                            if (sW2CDAC.Substring(0, 6) == "211002") //어음
                            {
                                //금융기관
                                this.DAT02_W2VLMI1.SetValue(dj.Rows[i]["F3BKPY"].ToString());
                                //거래처
                                this.DAT02_W2VLMI2.SetValue(dj.Rows[i]["M1VNCD"].ToString());
                                //지급어음번호
                                this.DAT02_W2VLMI3.SetValue(dj.Rows[i]["M1NONY"].ToString());
                                this.DAT02_W2VLMI4.SetValue("");
                                this.DAT02_W2VLMI5.SetValue("");
                                this.DAT02_W2VLMI6.SetValue("");
                            }
                            else  //현금
                            {
                                //금융기관
                                this.DAT02_W2VLMI1.SetValue(this.CBH01_B2CDBK.GetValue());
                                //계좌번호
                                this.DAT02_W2VLMI2.SetValue(this.CBH01_B2NOAC.GetValue());
                                this.DAT02_W2VLMI3.SetValue("");
                                this.DAT02_W2VLMI4.SetValue("");
                                this.DAT02_W2VLMI5.SetValue("");
                                this.DAT02_W2VLMI6.SetValue("");
                            }

                            this.DAT02_W2AMDR.SetValue("0");
                            this.DAT02_W2AMCR.SetValue(dB2AMDR_Hap.ToString());

                            this.DAT02_W2CDFD.SetValue("");
                            this.DAT02_W2AMFD.SetValue("0");
                            sW2RKAC = "미지급금 지급";
                            this.DAT02_W2RKAC.SetValue(sW2RKAC);
                            this.DAT02_W2RKCU.SetValue("");
                            //원천번호
                            this.DAT02_W2WCJP.SetValue("");
                            this.DAT02_W2PRGB.SetValue("");
                            this.DAT02_W2HIGB.SetValue("A");
                            this.DAT02_W2HISAB.SetValue(this.CBH01_BO1HISAB.GetValue());
                            this.DAT02_W2GUBUN.SetValue("");
                            this.DAT02_W2TXAMT.SetValue("0");
                            this.DAT02_W2TXVAT.SetValue("0");
                            this.DAT02_W2HWAJU.SetValue("");

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
                        #endregion
                    }
                    else
                    {
                        #region Description : 대변 생성(어음)
                        for (int i = iSInx; i <= iEInx; i++)
                        {
                            iCnt = iCnt + 1;

                            dt.Clear();

                            string sW2CDAC = "";

                            if (CBO01_M1GUBN.GetValue().ToString() == "1") //현금
                            {
                                sW2CDAC = this.CBH01_B2CDAC.GetValue().ToString();
                            }
                            else //어음
                            {
                                sW2CDAC = dj.Rows[i]["B2CDAC"].ToString();
                            }

                            this.DAT02_W2SSID.SetValue(sB2SSID);
                            this.DAT02_W2DPMK.SetValue(this.CBH01_B2DPMK.GetValue());
                            this.DAT02_W2DTMK.SetValue(this.DTP01_M1DTED.GetString());
                            this.DAT02_W2NOSQ.SetValue("0");
                            this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                            this.DAT02_W2IDJP.SetValue("3");
                            this.DAT02_W2NOJP.SetValue("");
                            this.DAT02_W2CDAC.SetValue(sW2CDAC);
                            this.DAT02_W2DTAC.SetValue("");
                            this.DAT02_W2DTLI.SetValue("");
                            this.DAT02_W2DPAC.SetValue(this.CBH01_B2DPMK.GetValue());

                            //관리항목 
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_23N3M888", sW2CDAC, "");
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

                            if (sW2CDAC.Substring(0, 6) == "211002") //어음
                            {
                                //금융기관
                                this.DAT02_W2VLMI1.SetValue(dj.Rows[i]["F3BKPY"].ToString());
                                //거래처
                                this.DAT02_W2VLMI2.SetValue(dj.Rows[i]["M1VNCD"].ToString());
                                //지급어음번호
                                this.DAT02_W2VLMI3.SetValue(dj.Rows[i]["M1NONY"].ToString());
                                this.DAT02_W2VLMI4.SetValue("");
                                this.DAT02_W2VLMI5.SetValue("");
                                this.DAT02_W2VLMI6.SetValue("");
                            }
                            else  //현금
                            {
                                //금융기관
                                this.DAT02_W2VLMI1.SetValue(this.CBH01_B2CDBK.GetValue());
                                //계좌번호
                                this.DAT02_W2VLMI2.SetValue(this.CBH01_B2NOAC.GetValue());
                                this.DAT02_W2VLMI3.SetValue("");
                                this.DAT02_W2VLMI4.SetValue("");
                                this.DAT02_W2VLMI5.SetValue("");
                                this.DAT02_W2VLMI6.SetValue("");
                            }

                            this.DAT02_W2AMDR.SetValue("0");
                            this.DAT02_W2AMCR.SetValue(dj.Rows[i]["M1AMT"].ToString());

                            this.DAT02_W2CDFD.SetValue("");
                            this.DAT02_W2AMFD.SetValue("0");
                            sW2RKAC = dj.Rows[i]["M1RKAC"].ToString();
                            this.DAT02_W2RKAC.SetValue(sW2RKAC);
                            this.DAT02_W2RKCU.SetValue(dj.Rows[i]["M1VNCDNM"].ToString());
                            //원천번호
                            //this.DAT02_W2WCJP.SetValue("");
                            this.DAT02_W2WCJP.SetValue(dj.Rows[i]["M1WNJP"].ToString());

                            this.DAT02_W2PRGB.SetValue("");
                            this.DAT02_W2HIGB.SetValue("A");
                            this.DAT02_W2HISAB.SetValue(this.CBH01_BO1HISAB.GetValue());
                            this.DAT02_W2GUBUN.SetValue("");
                            this.DAT02_W2TXAMT.SetValue("0");
                            this.DAT02_W2TXVAT.SetValue("0");
                            this.DAT02_W2HWAJU.SetValue("");

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
                        #endregion
                    }
                }

                if (datas.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    foreach (object[] data in datas)
                    {
                        this.DbConnector.Attach("TY_P_AC_29DA5966", data);
                    }
                }

                //미승인 SP호출 파일 입력
                this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, CBH01_BO1HISAB.GetValue(), "A",
                                                            this.CBH01_B2DPMK.GetValue(), this.DTP01_M1DTED.GetValue().ToString(), "", "",
                                                            "", "", CBH01_BO1HISAB.GetValue().ToString());
                this.DbConnector.ExecuteTranQueryList();

                //전표 생성 함수 호출
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, "");
                string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                if (sOUTMSG.Substring(0, 2) == "ER")
                {
                    this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    bresult = false;
                }
                else
                {                    
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

                                DataTable dk = dj.Clone(); 
                                dk.TableName = "TABLE0";
                                for (int k = iSInx; k <= iEInx; k++)
                                {
                                    string sM1DTED = dj.Rows[k]["M1DTED"].ToString();
                                    string sM1VNCD = dj.Rows[k]["M1VNCD"].ToString();
                                    string sM1NOSQ = dj.Rows[k]["M1NOSQ"].ToString();

                                    foreach (DataRow dr in dj.Select("M1DTED = '"+sM1DTED+"' AND M1VNCD = '"+sM1VNCD+"' AND M1NOSQ = "+sM1NOSQ+"" ))
                                        dk.Rows.Add(dr.ItemArray);                                   
                                }
                                                                
                                bresult = UP_Set_ANTPMEF(dk, sJpno.Replace("-", "").Trim());

                                if (bresult)
                                {
                                    UP_Set_JPCombo(this.DTP01_M1DTED.GetString().ToString(), this.CBO01_M1GUBN.GetValue().ToString());

                                    // 지급어음 업데이트
                                    if (this.CBO01_M1GUBN.GetValue().ToString() == "2") // 어음일 경우
                                    {
                                        string sNOJP   = string.Empty;
                                        string sF3NONY = string.Empty;
                                        string sF3NOJP = string.Empty;

                                        sNOJP = sJpno.ToString().Replace("-", "").ToString();

                                        this.DbConnector.CommandClear();
                                        this.DbConnector.Attach("TY_P_AC_88TDO650", sNOJP.ToString().Substring(0, 6),
                                                                                    sNOJP.ToString().Substring(6, 8),
                                                                                    sNOJP.ToString().Substring(14, 3)
                                                                                    );

                                        dt = this.DbConnector.ExecuteDataTable();

                                        if (dt.Rows.Count > 0)
                                        {
                                            this.DbConnector.CommandClear();

                                            for (int j = 0; j < dt.Rows.Count; j++)
                                            {
                                                sF3NONY = dt.Rows[j]["F3NONY"].ToString();
                                                sF3NOJP = dt.Rows[j]["F3NOJP"].ToString();

                                                // 지급어음 마스터
                                                this.DbConnector.Attach("TY_P_AC_88TDR651", sF3NOJP.ToString(), sF3NONY.ToString());
                                                // 지급어음 내역
                                                this.DbConnector.Attach("TY_P_AC_88TDS652", sF3NOJP.ToString(), sF3NONY.ToString());
                                            }
                                            this.DbConnector.ExecuteTranQueryList();
                                        }
                                    }
                                }
                                else 
                                { 
                                      this.ShowMessage("TY_M_AC_29L6B240");
                                      return false; 
                                };
                            }
                        }
                    }

                    bresult = true;
                }
                
            }
            catch(Exception ex) 
            {
                string dd = ex.Message; 
                bresult = false; 
            }

            return bresult;
        }
        #endregion

        #region Description : 전표 취소 이벤트
        private bool UP_Set_JpCanCel(DataTable dj)
        {
            bool bresult = false;
            string sB2SSID = "";
            string sW2JPNO = "";

            try
            {
                //BATID번호 부여
                sB2SSID = UP_Get_BatChSSID();

                if (dj.Rows.Count > 0)
                {
                    sW2JPNO = dj.Rows[0]["M1JPNO"].ToString().Substring(0,17);                       
                }

                //미승인 SP호출 파일 입력
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29C7K957", sB2SSID, sW2JPNO.Substring(0, 6), sW2JPNO.Substring(6, 8), sW2JPNO.Substring(14,3) );
                this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, CBH01_BO1HISAB.GetValue().ToString(), "D",
                                                            sW2JPNO.Substring(0, 6), sW2JPNO.Substring(7,8), "", "",
                                                            "", "", CBH01_BO1HISAB.GetValue().ToString());
                this.DbConnector.ExecuteTranQueryList();

                //전표 생성 함수 호출
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, "");
                string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                if (sOUTMSG.Substring(0, 2) == "ER")
                {
                    this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    bresult = false;
                }
                else
                {
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
                                bresult = UP_Set_ANTPMEF(dj, "");

                                if (bresult)
                                {
                                    UP_Set_JPCombo(this.DTP01_M1DTED.GetValue().ToString(), this.CBO01_M1GUBN.GetValue().ToString());

                                    // 지급어음 업데이트
                                    if (this.CBO01_M1GUBN.GetValue().ToString() == "2") // 어음일 경우
                                    {
                                        string sF3NONY = string.Empty;
                                        string sF3NOJP = string.Empty;

                                        this.DbConnector.CommandClear();
                                        for (int j = 0; j < dj.Rows.Count; j++)
                                        {
                                            sF3NONY = dj.Rows[j]["M1NONY"].ToString();
                                            sF3NOJP = "";

                                            // 지급어음 마스터
                                            this.DbConnector.Attach("TY_P_AC_88TDR651", sF3NOJP.ToString(), sF3NONY.ToString());
                                            // 지급어음 내역
                                            this.DbConnector.Attach("TY_P_AC_88TDS652", sF3NOJP.ToString(), sF3NONY.ToString());
                                        }
                                        this.DbConnector.ExecuteTranQueryList();   
                                    }
                                }
                                else
                                {
                                    this.ShowMessage("TY_M_AC_29L6B240");
                                    return false;
                                };
                            }
                        }
                    }

                    bresult = true;
                }

            }
            catch
            {
                bresult = false;
            }

            return bresult;
        }
        #endregion       

        #region Description :  BATID번호 함수
        private string UP_Get_BatChSSID()
        {
            string sB2SSID = "";

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            return sB2SSID;
        }
        #endregion

        #region Description : 미지급금 전표번호 표기 함수
        private bool  UP_Set_ANTPMEF(DataTable dj, string  sJPNO)
        {
            bool bresult = false; 
            try
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < dj.Rows.Count; i++)
                {
                    if (sJPNO != "")
                    {
                        this.DbConnector.Attach("TY_P_AC_29K63215", sJPNO + (i + 1).ToString("00"), dj.Rows[i]["M1DTED"].ToString(), dj.Rows[i]["M1VNCD"].ToString(), dj.Rows[i]["M1NOSQ"].ToString());
                    }
                    else
                    {
                        this.DbConnector.Attach("TY_P_AC_29K63215", sJPNO, dj.Rows[i]["M1DTED"].ToString(), dj.Rows[i]["M1VNCD"].ToString(), dj.Rows[i]["M1NOSQ"].ToString());
                    }
                }
                this.DbConnector.ExecuteTranQueryList();
                bresult = true; 
            }
            catch
            {
                bresult = false; 
            }

            return bresult;
        }
        #endregion

        #region Description : 콤보박스에 전표번호 ADD
        private void UP_Set_JPCombo(string sM1DTED, string sM1GUBN)
        {
           this.CBO01_M1WNJP.Initialize(); 
           this.DbConnector.CommandClear();
           this.DbConnector.Attach("TY_P_AC_29K40214", sM1DTED, sM1GUBN);
           this.CBO01_M1WNJP.DataBind(this.DbConnector.ExecuteDataTable());

           if (this.CBO01_M1WNJP.Items.Count > 0)
           {
               this.CBO01_M1WNJP.SelectedIndex = 0;
           }
        }
        #endregion

        #region Description : DTP01_M1DTED_ValueChanged 이벤트
        private void DTP01_M1DTED_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_B2DPMK.DummyValue = this.DTP01_M1DTED.GetValue();
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 처리 체크
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.CBO01_GOKCR.GetValue().ToString() == "A")
            {
                if (this.CBO01_M1GUBN.GetValue().ToString().Trim() == "1" )
                {
                    if (this.CBH01_B2CDAC.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_AC_27P4C280");
                        e.Successed = false;
                        return;
                    }
                    if (this.CBH01_B2CDBK.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_AC_2445M440");
                        e.Successed = false;
                        return;

                    }
                    if (this.CBH01_B2NOAC.GetValue().ToString() == "")
                    {
                        this.ShowMessage("TY_M_AC_2445M441");
                        e.Successed = false;
                        return;
                    }
                }
            }
            else
            {
                if (this.CBO01_M1WNJP.GetValue().ToString().Trim() == "")
                {
                    this.ShowMessage("TY_M_AC_2422N250");
                    e.Successed = false;
                    return;
                }
            }

            if( this.CBO01_GOKCR.GetValue().ToString() == "A")
            {
                if (!this.ShowMessage("TY_M_AC_25O8J618"))
                {
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (!this.ShowMessage("TY_M_AC_25O8K619"))
                {
                    e.Successed = false;
                    return;
                }
            }
           
        }
        #endregion
         
        #region Description : CBO01_M1GUBN_SelectedIndexChanged 이벤트
        private void CBO01_M1GUBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CBO01_M1GUBN.GetValue().ToString().Trim() == "1")
            {
                this.CBH01_B2CDAC.SetValue("11100301");
                this.CBH01_B2CDBK.SetValue("127543");
                this.CBH01_B2NOAC.SetValue("101-50212-413");
            }
            else
            {
                this.CBH01_B2CDAC.SetValue("");
                this.CBH01_B2CDBK.SetValue("");
                this.CBH01_B2NOAC.SetValue("");
            }

            UP_Set_JPCombo(this.DTP01_M1DTED.GetString().ToString(), this.CBO01_M1GUBN.GetValue().ToString());
        }
        #endregion

        #region Description : CBH01_B2CDBK_CodeBoxDataBinded 이벤트
        private void CBH01_B2CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_B2CDBK.GetValue().ToString();
            this.CBH01_B2NOAC.DummyValue = groupCode;
            this.CBH01_B2NOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_B2NOAC.Initialize();
        }
        #endregion

        #region Description : CBO01_GOKCR_SelectedIndexChanged 이벤트
        private void CBO01_GOKCR_SelectedIndexChanged(object sender, EventArgs e)
        {
            UP_Set_JPCombo(this.DTP01_M1DTED.GetString().ToString(), this.CBO01_M1GUBN.GetValue().ToString());     
        }
        #endregion
       

    }
}
