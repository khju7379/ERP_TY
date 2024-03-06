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
    /// 지급어음만기등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.25 09:10
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_23N3M888 : 계정 과목 코드 조회
    ///  TY_P_AC_29C7K957 : 미승인전표 임시파일 입력
    ///  TY_P_AC_29C7M958 : 자동순번 가져오기
    ///  TY_P_AC_29C7O959 : 미승인전표 SP호출 이력 저장
    ///  TY_P_AC_29C80960 : 미승인전표 SP 호출
    ///  TY_P_AC_29D5B004 : 전표호출 파라메타 파일 조회
    ///  TY_P_AC_29DA5966 : 미승인전표 임시파일 등록
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29P92273 : 지급어음만기등록 전표처리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_25O8J618 : 전표생성 작업을 하시겠습니까?
    ///  TY_M_AC_25O8K619 : 전표취소 작업을 하시겠습니까?
    ///  TY_M_AC_25O8K620 : 전표처리가  완료되었습니다!
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BTNJUNPYO : 설정전표발행
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  F4CDDP : 부서코드
    ///  F4HISAB : 작성사번
    ///  F4DTED : 만기일자
    ///  F4NOJP : 전표번호
    /// </summary>
    public partial class TYACEI016I : TYBase
    {
        //private bool _Isloaded = false;
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

        private string fsJPNO = "";
        private string fsGubn = "";

        #region Description : 폼 로드 이벤트
        public TYACEI016I(DataSet ds, string sJpno, string sGubn)
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

            fsJPNO = sJpno;
            fsGubn = sGubn;

            if (fsGubn == "A")
            {
                DataTable Condt = new DataTable();
                DataRow row;
                Condt.Columns.Add("F3NONY", typeof(System.String));
                Condt.Columns.Add("F3DTIS", typeof(System.String));
                Condt.Columns.Add("F3DTED", typeof(System.String));
                Condt.Columns.Add("F3CLNY", typeof(System.String));
                Condt.Columns.Add("F3CLNYNM", typeof(System.String));
                Condt.Columns.Add("SANGTAE", typeof(System.String));
                Condt.Columns.Add("F3AMNY", typeof(System.Double));
                Condt.Columns.Add("F3DPAC", typeof(System.String));
                Condt.Columns.Add("F3RPYY", typeof(System.String));
                Condt.Columns.Add("F3BKPY", typeof(System.String));
                Condt.Columns.Add("F3SSYN", typeof(System.String));
                Condt.Columns.Add("F3JPNO", typeof(System.String));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    row = Condt.NewRow();

                    row["F3NONY"] = ds.Tables[0].Rows[i]["F3NONY"].ToString();
                    row["F3DTIS"] = ds.Tables[0].Rows[i]["F3DTIS"].ToString();
                    row["F3DTED"] = ds.Tables[0].Rows[i]["F3DTED"].ToString();
                    row["F3CLNY"] = ds.Tables[0].Rows[i]["F3CLNY"].ToString();
                    row["F3CLNYNM"] = ds.Tables[0].Rows[i]["F3CLNYNM"].ToString();
                    row["SANGTAE"] = ds.Tables[0].Rows[i]["SANGTAE"].ToString();
                    row["F3AMNY"] = Convert.ToDouble(ds.Tables[0].Rows[i]["F3AMNY"].ToString());
                    row["F3DPAC"] = ds.Tables[0].Rows[i]["F3DPAC"].ToString();
                    row["F3RPYY"] = ds.Tables[0].Rows[i]["F3RPYY"].ToString();
                    row["F3BKPY"] = ds.Tables[0].Rows[i]["F3BKPY"].ToString();
                    row["F3SSYN"] = ds.Tables[0].Rows[i]["F3SSYN"].ToString();
                    row["F3JPNO"] = ds.Tables[0].Rows[i]["F3JPNO"].ToString();

                    Condt.Rows.Add(row);
                }

                Condt.TableName = "TableNames";
                fsds.Tables.Add(Condt);
            }
            else
            {
                this.TXT01_F4NOJP.SetValue(fsJPNO.Substring(0, 6) + "-" + fsJPNO.Substring(6, 8) + "-" + fsJPNO.Substring(14, 3));
            }      
        }

        private void TYACEI016I_Load(object sender, System.EventArgs e)
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
            this.BTN61_BTNJUNPYO.ProcessCheck += new TButton.CheckHandler(BTN61_BTNJUNPYO_ProcessCheck);
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(BTN61_PRT_ProcessCheck);


            if (fsGubn == "A")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_BTNJUNPYO.Visible = false;

                this.FPS91_TY_S_AC_29P92273.SetValue(fsds.Tables[0]);

                if (this.FPS91_TY_S_AC_29P92273.CurrentRowCount > 0)
                {
                    this.SpreadSumRowAdd(this.FPS91_TY_S_AC_29P92273, "F3NONY", "합  계", SumRowType.Sum, "F3AMNY");
                }

                for (int i = 0; i < this.FPS91_TY_S_AC_29P92273.CurrentRowCount - 1; i++)
                {
                    this.FPS91_TY_S_AC_29P92273.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";
                }

                if (this.FPS91_TY_S_AC_29P92273.CurrentRowCount > 0)
                {
                    this.DTP01_F4DTED.SetValue(fsds.Tables[0].Rows[0]["F3DTED"].ToString());

                    for (int i = 0; i < this.FPS91_TY_S_AC_29P92273.CurrentRowCount - 1; i++)
                    {
                        int row = i;

                        this.FPS91_TY_S_AC_29P92273.SetValue(row, "F3DPAC", this.DTP01_F4DTED.GetValue());
                    }
                }
                else
                {
                    this.DTP01_F4DTED.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                }
                this.CBH01_F4CDDP.DummyValue = this.DTP01_F4DTED.GetValue();

                this.BTN61_BTNJUNPYO.Text = "전표발행"; 
            }
            else
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_BTNJUNPYO.Visible = true;

                this.DTP01_F4DTED.SetValue(this.TXT01_F4NOJP.GetValue().ToString().Replace("-", "").Substring(6,8));

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29P55304", "20", this.DTP01_F4DTED.GetString(),this.TXT01_F4NOJP.GetValue().ToString().Replace("-", ""));
                DataTable dt = this.DbConnector.ExecuteDataTable();
                this.FPS91_TY_S_AC_29P92273.SetValue(dt);

                if (this.FPS91_TY_S_AC_29P92273.CurrentRowCount > 0)
                {
                    this.SpreadSumRowAdd(this.FPS91_TY_S_AC_29P92273, "F3NONY", "합  계", SumRowType.Sum, "F3AMNY");
                }

                if (dt.Rows.Count > 0)
                {
                    this.DTP01_F4DTED.SetValue(dt.Rows[0]["F4DTIS"].ToString());
                    this.CBH01_F4CDDP.DummyValue = this.DTP01_F4DTED.GetValue();
                    this.CBH01_F4CDDP.SetValue(dt.Rows[0]["F4CDDP"].ToString());
                    this.CBH01_F4HISAB.SetValue(dt.Rows[0]["F4HISAB"].ToString());

                    this.DTP01_F4DTED.SetReadOnly(true);
                    this.CBH01_F4CDDP.SetReadOnly(true);
                    this.CBH01_F4HISAB.SetReadOnly(true);
                }

                this.BTN61_BTNJUNPYO.Text = "전표취소"; 
            }

            this.TXT01_F4NOJP.SetReadOnly(true);
            this.CBH01_F4CDDP.SetReadOnly(true);
            this.SetStartingFocus(this.DTP01_F4DTED);
        }
        #endregion

        #region Description : 전표처리 버튼 이벤트
        private void BTN61_BTNJUNPYO_Click(object sender, EventArgs e)
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
            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            if (fsGubn == "A")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29P45301", "20", this.DTP01_F4DTED.GetString().ToString());
                DataTable dj = this.DbConnector.ExecuteDataTable();

                if (dj.Rows.Count > 0)
                {
                    //차변
                    for (int i = 0; i < dj.Rows.Count; i++)
                    {
                        iCnt = iCnt + 1;

                        dt.Clear();

                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(this.CBH01_F4CDDP.GetValue());
                        this.DAT02_W2DTMK.SetValue(this.DTP01_F4DTED.GetValue());
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue("3");
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue(dj.Rows[i]["F3CDAC"].ToString());
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(dj.Rows[i]["F4CDDP"].ToString());

                        //관리항목 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_23N3M888", dj.Rows[i]["F3CDAC"].ToString(), "");
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

                        //지급어음계정
                        if (dj.Rows[i]["F3CDAC"].ToString() == "21100201" || dj.Rows[i]["F3CDAC"].ToString() == "21100202" )
                        {
                            //은행
                            this.DAT02_W2VLMI1.SetValue(dj.Rows[i]["F3BKPY"].ToString());
                            //거래처
                            this.DAT02_W2VLMI2.SetValue(dj.Rows[i]["F3CLNY"].ToString());
                            //지급어음번호
                            this.DAT02_W2VLMI3.SetValue(dj.Rows[i]["F3NONY"].ToString());
                            this.DAT02_W2VLMI4.SetValue("");
                            this.DAT02_W2VLMI5.SetValue("");
                            this.DAT02_W2VLMI6.SetValue("");
                        }

                        sW2RKAC = dj.Rows[i]["F3RPYY"].ToString();

                        this.DAT02_W2AMDR.SetValue(dj.Rows[i]["F3AMNY"].ToString());
                        this.DAT02_W2AMCR.SetValue("0");

                        this.DAT02_W2CDFD.SetValue("21700");
                        this.DAT02_W2AMFD.SetValue(dj.Rows[i]["F3AMNY"].ToString());
                        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                        this.DAT02_W2RKCU.SetValue(dj.Rows[i]["F3CLNYNM"].ToString());
                        this.DAT02_W2WCJP.SetValue("");
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(CBH01_F4HISAB.GetValue().ToString());
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
                    //대변                
                    for (int i = 0; i < dj.Rows.Count; i++)
                    {
                        iCnt = iCnt + 1;

                        dt.Clear();

                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(this.CBH01_F4CDDP.GetValue());
                        this.DAT02_W2DTMK.SetValue(this.DTP01_F4DTED.GetValue());
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue("3");
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue(dj.Rows[i]["F3ACCR"].ToString());
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(dj.Rows[i]["F4CDDP"].ToString());

                        //관리항목 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_23N3M888", dj.Rows[i]["F3ACCR"].ToString(), "");
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

                        //당좌예금, 보통예금
                        //은행
                        this.DAT02_W2VLMI1.SetValue(dj.Rows[i]["F3BKPY"].ToString());
                        //계좌번호
                        this.DAT02_W2VLMI2.SetValue(dj.Rows[i]["F3NOAC"].ToString());
                        this.DAT02_W2VLMI3.SetValue("");
                        this.DAT02_W2VLMI4.SetValue("");
                        this.DAT02_W2VLMI5.SetValue("");
                        this.DAT02_W2VLMI6.SetValue("");

                        sW2RKAC = dj.Rows[i]["F3RPYY"].ToString();

                        this.DAT02_W2AMDR.SetValue("0");
                        this.DAT02_W2AMCR.SetValue(dj.Rows[i]["F3AMNY"].ToString());

                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");
                        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                        this.DAT02_W2RKCU.SetValue(dj.Rows[i]["F3BKPYNM"].ToString());
                        //원천번호
                        this.DAT02_W2WCJP.SetValue("");
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(this.CBH01_F4HISAB.GetValue().ToString());
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
                this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, this.CBH01_F4HISAB.GetValue(), "A",
                                                            this.CBH01_F4CDDP.GetValue(), this.DTP01_F4DTED.GetValue(), "", "",
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
                                TXT01_F4NOJP.SetValue(sJpno);
                                this.BTN61_BTNJUNPYO.Visible = false;
                            }
                        }
                    }
                }

                if (bJunPyoFlag)
                {
                    if (dj.Rows.Count > 0)
                    {
                        string sF4NOJP = TXT01_F4NOJP.GetValue().ToString().Replace("-", "");
                        datas.Clear();
                        for (int i = 0; i < dj.Rows.Count; i++)
                        {
                            datas.Add(new object[] { sF4NOJP, dj.Rows[i]["F3NONY"].ToString(), dj.Rows[i]["SANGTAE"].ToString() });
                        }
                        if (datas.Count > 0)
                        {
                            this.DbConnector.CommandClear();
                            foreach (object[] data in datas)
                            {
                                this.DbConnector.Attach("TY_P_AC_29P52303", data);
                            }
                            this.DbConnector.ExecuteTranQueryList();
                        }
                    }
                    //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    //this.Close();
                }
            }
            else
            {
                //미승인전표 -> 임시파일 입력
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29C7K957", sB2SSID, this.TXT01_F4NOJP.GetValue().ToString().Substring(0, 6), this.TXT01_F4NOJP.GetValue().ToString().Substring(7, 8), this.TXT01_F4NOJP.GetValue().ToString().Substring(16, 3));
                //미승인 SP호출 파일 입력
                this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, CBH01_F4HISAB.GetValue(), "D",
                                                            this.CBH01_F4CDDP.GetValue(), this.DTP01_F4DTED.GetValue(), "", "",
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
                    UP_Set_STATUS();
                    //this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);
                    this.ShowMessage("TY_M_AC_25O8K620");
                }
                //this.DialogResult = System.Windows.Forms.DialogResult.OK;
                //this.Close();
            }
        }
        #endregion

        #region Description : 지급어음 상태변경
        private void UP_Set_STATUS()
        {
            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29P55304", "20", this.DTP01_F4DTED.GetString().ToString() , this.TXT01_F4NOJP.GetValue().ToString().Replace("-", ""));
            DataTable dj = this.DbConnector.ExecuteDataTable();

            if (dj.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < dj.Rows.Count; i++)
                {
                    datas.Add(new object[] { "", "", CBH01_F4HISAB.GetValue().ToString(), dj.Rows[i]["F3NONY"].ToString() });
                }
                if (datas.Count > 0)
                {
                    foreach (object[] data in datas)
                    {
                        this.DbConnector.Attach("TY_P_AC_29PBO291", data);
                    }
                }
                datas.Clear();
                for (int i = 0; i < dj.Rows.Count; i++)
                {
                    datas.Add(new object[] { dj.Rows[i]["F3NONY"].ToString(), dj.Rows[i]["SANGTAE"].ToString() });
                }
                if (datas.Count > 0)
                {
                    foreach (object[] data in datas)
                    {
                        this.DbConnector.Attach("TY_P_AC_25T6D703", data);
                    }
                }
                this.DbConnector.ExecuteTranQueryList();
            }
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            System.Collections.Generic.List<object[]> dataM = new System.Collections.Generic.List<object[]>();
            System.Collections.Generic.List<object[]> dataS = new System.Collections.Generic.List<object[]>();


            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                dataM.Add(new object[] { "20", ds.Tables[0].Rows[i]["F3DPAC"].ToString(), this.CBH01_F4HISAB.GetValue(), ds.Tables[0].Rows[i]["F3NONY"].ToString() });

                dataS.Add(new object[] {ds.Tables[0].Rows[i]["F3NONY"].ToString(),
                                        "20", 
                                        this.CBH01_F4CDDP.GetValue(),
                                        ds.Tables[0].Rows[i]["F3BKPY"].ToString(),
                                        ds.Tables[0].Rows[i]["F3DPAC"].ToString(),                                        
                                        ds.Tables[0].Rows[i]["F3DTED"].ToString(),
                                        ds.Tables[0].Rows[i]["F3CLNY"].ToString(),
                                        ds.Tables[0].Rows[i]["F3AMNY"].ToString(),
                                        this.CBH01_F4HISAB.GetValue()
                                        });
            }


            if (dataM.Count > 0 && dataS.Count > 0)
            {
                this.DbConnector.CommandClear();
                foreach (object[] data in dataM)
                {
                    this.DbConnector.Attach("TY_P_AC_29PBO291", data);
                }
                foreach (object[] data in dataS)
                {
                    this.DbConnector.Attach("TY_P_AC_25T6B701", data);
                }
            }
            this.DbConnector.ExecuteTranQueryList();

            //만기자료 다시 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29P45301", "20", this.DTP01_F4DTED.GetString().ToString());
            this.FPS91_TY_S_AC_29P92273.SetValue(this.DbConnector.ExecuteDataTable());
            if (this.FPS91_TY_S_AC_29P92273.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_AC_29P92273, "F3NONY", "합  계", SumRowType.Sum, "F3AMNY");
            }
            this.DTP01_F4DTED.SetReadOnly(true);
            this.CBH01_F4CDDP.SetReadOnly(true);
            this.CBH01_F4HISAB.SetReadOnly(true);

            this.BTN61_SAV.Visible = false;
            this.BTN61_BTNJUNPYO.Visible = true;

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_29P92273.GetDataSourceInclude(TSpread.TActionType.New, "F3NONY", "F3DTIS", "F3DTED", "F3CLNY", "F3CLNYNM", "SANGTAE", "F3AMNY", "F3DPAC", "F3RPYY", "F3BKPY", "F3SSYN", "F3JPNO"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            UP_Get_BUSEO();

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 전표발행 ProcessCheck 이벤트
        private void BTN61_BTNJUNPYO_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.FPS91_TY_S_AC_29P92273.CurrentRowCount <= 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (fsGubn == "A")
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

        #region Description : DTP01_F4DTED_ValueChanged 이벤트
        private void DTP01_F4DTED_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_F4CDDP.DummyValue = this.DTP01_F4DTED.GetValue();

            for (int i = 0; i < this.FPS91_TY_S_AC_29P92273.CurrentRowCount - 1; i++)
            {
                int row = i;

                this.FPS91_TY_S_AC_29P92273.SetValue(row, "F3DPAC", this.DTP01_F4DTED.GetValue());
            }

        }
        #endregion

        #region Description : CBH01_F4HISAB_TextChanged 이벤트
        private void CBH01_F4HISAB_TextChanged(object sender, EventArgs e)
        {
            UP_Get_BUSEO();
        }
        #endregion

        #region Description : 부서 체크
        private void UP_Get_BUSEO()
        {
            this.CBH01_F4CDDP.SetValue("");
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_GB_24G9S659", this.CBH01_F4HISAB.GetValue().ToString());
            this.DbConnector.Attach("TY_P_GB_4CVJ7024", this.DTP01_F4DTED.GetString(), this.CBH01_F4HISAB.GetValue().ToString());
            this.CBH01_F4CDDP.SetValue(this.DbConnector.ExecuteDataTable().Rows[0]["KBBUSEO"].ToString());
        }
        #endregion

        #region Description : 전표출력
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sJPNO = TXT01_F4NOJP.GetValue().ToString().Replace("-", "");

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

            this.SetFocus(this.DTP01_F4DTED);
        }
        #endregion

        #region Description : 전표출력 ProcessCheck 이벤트
        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.TXT01_F4NOJP.GetValue().ToString().Trim() == "" && TXT01_F4NOJP.GetValue().ToString().Replace("-", "").Trim().Length < 17)
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

    }
}
