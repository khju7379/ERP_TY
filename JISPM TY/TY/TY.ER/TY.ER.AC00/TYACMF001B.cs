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
    /// 통장거래내역관리 전표발행 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.10.31 17:04
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29C7K957 : 미승인전표 임시파일 입력
    ///  TY_P_AC_29C7M958 : 자동순번 가져오기
    ///  TY_P_AC_29C7O959 : 미승인전표 SP호출 이력 저장
    ///  TY_P_AC_29C80960 : 미승인전표 SP 호출
    ///  TY_P_AC_29D5B004 : 전표호출 파라메타 파일 조회
    ///  TY_P_AC_29DA5966 : 미승인전표 임시파일 등록(TMAC1102)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2AV57964 : 통장거래내역 전표발행
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_25O8J618 : 전표생성 작업을 하시겠습니까?
    ///  TY_M_AC_25O8K620 : 전표처리가  완료되었습니다!
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  B1SUNO :  수표번호
    /// </summary>
    public partial class TYACMF001B : TYBase
    {
        private DataSet fsds = new DataSet();

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
        public TYACMF001B(DataSet ds)
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

            DataTable Condt = new DataTable();
            DataRow row;
            Condt.Columns.Add("E3CDAC", typeof(System.String));
            Condt.Columns.Add("E3CDACNM", typeof(System.String));
            Condt.Columns.Add("B1CDBK", typeof(System.String));
            Condt.Columns.Add("B1CDBKNM", typeof(System.String));
            Condt.Columns.Add("B1NOAC", typeof(System.String));
            Condt.Columns.Add("B1CDBKNOAC", typeof(System.String));
            Condt.Columns.Add("B1DATE", typeof(System.String));
            Condt.Columns.Add("B1NOSQ", typeof(System.String));
            Condt.Columns.Add("B1IOGB", typeof(System.String));
            Condt.Columns.Add("B1AMIO", typeof(System.Double));
            Condt.Columns.Add("B1NAME", typeof(System.String));
            Condt.Columns.Add("B1CRAMT", typeof(System.Double));
            Condt.Columns.Add("B1DRAMT", typeof(System.Double));
            Condt.Columns.Add("B1JPNO", typeof(System.String));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                row = Condt.NewRow();

                row["E3CDAC"] = ds.Tables[0].Rows[i]["E3CDAC"].ToString();
                row["E3CDACNM"] = ds.Tables[0].Rows[i]["E3CDACNM"].ToString();
                row["B1CDBK"] = ds.Tables[0].Rows[i]["B1CDBK"].ToString();
                row["B1CDBKNM"] = ds.Tables[0].Rows[i]["B1CDBKNM"].ToString();
                row["B1NOAC"] = ds.Tables[0].Rows[i]["B1NOAC"].ToString();
                row["B1CDBKNOAC"] = ds.Tables[0].Rows[i]["B1CDBKNOAC"].ToString();
                row["B1DATE"] = ds.Tables[0].Rows[i]["B1DATE"].ToString();
                row["B1NOSQ"] = ds.Tables[0].Rows[i]["B1NOSQ"].ToString();
                row["B1IOGB"] = ds.Tables[0].Rows[i]["B1IOGB"].ToString();
                row["B1AMIO"] = Convert.ToDouble(ds.Tables[0].Rows[i]["B1AMIO"].ToString());
                row["B1NAME"] = ds.Tables[0].Rows[i]["B1NAME"].ToString();                
                //입금
                if (ds.Tables[0].Rows[i]["B1IOGB"].ToString().Substring(0, 1) == "1")
                {
                    row["B1DRAMT"] = Convert.ToDouble(ds.Tables[0].Rows[i]["B1AMIO"].ToString());
                    row["B1CRAMT"] = 0;
                }
                else //출금
                {
                    row["B1DRAMT"] = 0;
                    row["B1CRAMT"] = Convert.ToDouble(ds.Tables[0].Rows[i]["B1AMIO"].ToString());
                }
                row["B1JPNO"] = ds.Tables[0].Rows[i]["B1JPNO"].ToString();

                Condt.Rows.Add(row);
            }

            Condt.TableName = "TableNames";
            fsds.Tables.Add(Condt);

        }

        private void TYACMF001B_Load(object sender, System.EventArgs e)
        {
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

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_CANCEL.ProcessCheck += new TButton.CheckHandler(BTN61_CANCEL_ProcessCheck);
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(BTN61_PRT_ProcessCheck);

            this.FPS91_TY_S_AC_2AV57964.Initialize(); 
            
            this.FPS91_TY_S_AC_2AV57964.SetValue(fsds.Tables[0]);

            if (this.FPS91_TY_S_AC_2AV57964.CurrentRowCount > 0)
            {
                this.DTP01_B2DTMK.SetValue(fsds.Tables[0].Rows[0]["B1DATE"].ToString());
                this.DTP01_B2DTMK.SetReadOnly(true);
                this.TXT01_B1JPNO.SetReadOnly(true);

                this.TXT01_B1JPNO.SetValue(fsds.Tables[0].Rows[0]["B1JPNO"].ToString());

                if (this.TXT01_B1JPNO.GetValue().ToString() != "")
                {
                    this.CBH01_B2HISAB.SetReadOnly(true);
                    this.CBH01_B2DPMK.SetReadOnly(true);

                    this.BTN61_CANCEL.Visible = true;
                    this.BTN61_SAV.Visible = false; 
   
                }
                else
                {
                    this.CBH01_B2DPMK.DummyValue = this.DTP01_B2DTMK.GetString().ToString();   
                    this.CBH01_B2HISAB.SetValue(TYUserInfo.EmpNo);
                    //this.CBH01_B2DPMK.SetValue(TYUserInfo.DeptCode);
                    this.CBH01_B2DPMK.SetReadOnly(true);

                    this.BTN61_CANCEL.Visible = false;
                    this.BTN61_SAV.Visible = true; 
                }                    
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_2AV57964, "E3CDACNM", "합  계", SumRowType.Sum, "B1DRAMT", "B1CRAMT");
            }            
        }
        #endregion

        #region Description : 전표 생성 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sB2SSID = "";
            string sW2RKAC = "";
            bool bJunPyoFlag = false;

            int iCnt = 0;

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            DataTable dt = new DataTable();

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            sB2SSID = this.IPAdresss + this.CBH01_B2HISAB.GetValue().ToString() + dAutoSeq.ToString();

            
            if (fsds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < fsds.Tables[0].Rows.Count - 1; i++)
                {
                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(this.CBH01_B2DPMK.GetValue());
                    this.DAT02_W2DTMK.SetValue(this.DTP01_B2DTMK.GetString());
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(fsds.Tables[0].Rows[i]["E3CDAC"].ToString());
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH01_B2DPMK.GetValue());

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", fsds.Tables[0].Rows[i]["E3CDAC"].ToString(), "");
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

                    // 20180904 이성재대리 기업신탁일 경우에도 은행과 계좌번호 들어가도록 요청함
                    //당좌예금, 보통예금, 기업신탁
                    if (fsds.Tables[0].Rows[i]["E3CDAC"].ToString() == "11100301" || fsds.Tables[0].Rows[i]["E3CDAC"].ToString() == "11100302" || fsds.Tables[0].Rows[i]["E3CDAC"].ToString() == "11100306")
                    {
                        //은행
                        this.DAT02_W2VLMI1.SetValue(fsds.Tables[0].Rows[i]["B1CDBKNOAC"].ToString());
                        //계좌
                        this.DAT02_W2VLMI2.SetValue(fsds.Tables[0].Rows[i]["B1NOAC"].ToString());
                        this.DAT02_W2VLMI3.SetValue("");
                        this.DAT02_W2VLMI4.SetValue("");
                        this.DAT02_W2VLMI5.SetValue("");
                        this.DAT02_W2VLMI6.SetValue("");
                    }
                    sW2RKAC = fsds.Tables[0].Rows[i]["B1NAME"].ToString();

                    this.DAT02_W2AMDR.SetValue(fsds.Tables[0].Rows[i]["B1DRAMT"].ToString());
                    this.DAT02_W2AMCR.SetValue(fsds.Tables[0].Rows[i]["B1CRAMT"].ToString());

                    this.DAT02_W2CDFD.SetValue("");
                    this.DAT02_W2AMFD.SetValue("0");
                    this.DAT02_W2RKAC.SetValue(sW2RKAC);
                    this.DAT02_W2RKCU.SetValue("");
                    this.DAT02_W2WCJP.SetValue("");
                    this.DAT02_W2PRGB.SetValue("");
                    this.DAT02_W2HIGB.SetValue("A");
                    this.DAT02_W2HISAB.SetValue(TYUserInfo.EmpNo);
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
            this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, CBH01_B2HISAB.GetValue(), "A",
                                                        this.CBH01_B2DPMK.GetValue(), this.DTP01_B2DTMK.GetString(), "", "",
                                                        "", "", TYUserInfo.EmpNo);
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
                            TXT01_B1JPNO.SetValue(sJpno);
                        }
                    }
                }
            }

            if (bJunPyoFlag)
            {                
                if ( fsds.Tables[0].Rows.Count > 0)
                {
                    string sJPNO = TXT01_B1JPNO.GetValue().ToString().Replace("-", "");

                    UP_Set_JpnoStaus(sJPNO);

                }

                //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                //this.Close();
            }
            
           
        }
        #endregion

        #region Description : 전표 취소 버튼 이벤트
        private void BTN61_CANCEL_Click(object sender, EventArgs e)
        {
            string sB2SSID = "";
            string sJpno = "";

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            sB2SSID = this.IPAdresss + TYUserInfo.EmpNo  + dAutoSeq.ToString();

            sJpno = this.TXT01_B1JPNO.GetValue().ToString().Replace("-", "");    

            //미승인전표 -> 임시파일 입력
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7K957", sB2SSID, sJpno.ToString().Substring(0, 6), sJpno.ToString().Substring(6, 8), sJpno.ToString().Substring(14, 3));
            //미승인 SP호출 파일 입력
            this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, TYUserInfo.EmpNo, "D",
                                                        TYUserInfo.DeptCode, this.DTP01_B2DTMK.GetString(), "", "",
                                                        "", "", TYUserInfo.EmpNo);
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
                UP_Set_JpnoStaus("");
                //this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);
                this.ShowMessage("TY_M_AC_25O8K620");
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 전표번호 처리 
        private void UP_Set_JpnoStaus(string sJPNO )
        {
            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            if (fsds.Tables[0].Rows.Count > 0)
            {
                datas.Clear();
                for (int i = 0; i < fsds.Tables[0].Rows.Count - 1; i++)
                {
                    datas.Add(new object[] { sJPNO, TYUserInfo.EmpNo, fsds.Tables[0].Rows[i]["B1CDBK"].ToString(), fsds.Tables[0].Rows[i]["B1NOAC"].ToString(), fsds.Tables[0].Rows[i]["B1DATE"].ToString(), fsds.Tables[0].Rows[i]["B1NOSQ"].ToString() });
                }
                if (datas.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    foreach (object[] data in datas)
                    {
                        this.DbConnector.Attach("TY_P_AC_2AV6O965", data);
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
            }            
        }
        #endregion

        #region Description : 전표발행 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.FPS91_TY_S_AC_2AV57964.CurrentRowCount <= 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_AC_25O8J618"))
            {
                e.Successed = false;
                return;
            }

        }
        #endregion

        #region Description : 전표취소 ProcessCheck 이벤트
        private void BTN61_CANCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.FPS91_TY_S_AC_2AV57964.CurrentRowCount <= 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }
         
            if (!this.ShowMessage("TY_M_AC_25O8K619"))
            {
                e.Successed = false;
                return;
            }         

        }
        #endregion

        #region Description : CBH01_B2HISAB_TextChanged 이벤트
        private void CBH01_B2HISAB_TextChanged(object sender, EventArgs e)
        {
            if (CBH01_B2HISAB.GetValue().ToString().Trim().Length == 6)
            {
                UP_Get_BUSEO();
            }
        }
        #endregion

        #region Description : 부서 체크
        private void UP_Get_BUSEO()
        {
            
            this.CBH01_B2DPMK.SetValue("");
            this.CBH01_B2DPMK.DummyValue = this.DTP01_B2DTMK.GetString();
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_GB_24G9S659", this.CBH01_B2HISAB.GetValue().ToString());
            this.DbConnector.Attach("TY_P_GB_4CVJ7024", this.DTP01_B2DTMK.GetString(), this.CBH01_B2HISAB.GetValue().ToString());
            this.CBH01_B2DPMK.SetValue(this.DbConnector.ExecuteDataTable().Rows[0]["KBBUSEO"].ToString());
        }
        #endregion

        #region Description : 전표 생성 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 출력 ProcessCheck 이벤트
        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if ( this.TXT01_B1JPNO.GetValue().ToString().Trim() == "")
            {
                this.ShowMessage("TY_M_AC_26D47852");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_2BN4U622"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sJPNO = TXT01_B1JPNO.GetValue().ToString().Replace("-", "");

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_2AU2M916",
                sJPNO.Substring(0,6),
                sJPNO.Substring(6,8),
                sJPNO.Substring(14,3), // 시작 번호
                sJPNO.Substring(14,3)  // 종료 번호
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

            this.SetFocus(this.DTP01_B2DTMK);
        }
        #endregion

    }
}
