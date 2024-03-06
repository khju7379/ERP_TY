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
    /// 할인어음 전표관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.05.23 13:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25N60614 : 할인어음 전표조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25N69617 : 할인어음 전표 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_25O8J618 : 전표생성 작업을 하시겠습니까?
    ///  TY_M_AC_25O8K619 : 전표취소 작업을 하시겠습니까?
    ///  TY_M_AC_25O8K620 : 전표처리가  완료되었습니다!
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  CBOJUNPYO : 전표구분
    ///  E7IDBG : 상태구분
    ///  E7DTBG : 상태변경일
    ///  E7HDAC : 미전표번호
    /// </summary>
    public partial class TYACEI006G : TYBase
    {
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
        public TYACEI006G()
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
            //this.DAT02_W2HIDAT = new TYData("DAT02_W2HIDAT", null);
            //this.DAT02_W2HITIM = new TYData("DAT02_W2HITIM", null);
            this.DAT02_W2HISAB = new TYData("DAT02_W2HISAB", null);
            this.DAT02_W2GUBUN = new TYData("DAT02_W2GUBUN", null);
            this.DAT02_W2TXAMT = new TYData("DAT02_W2TXAMT", null);
            this.DAT02_W2TXVAT = new TYData("DAT02_W2TXVAT", null);
            this.DAT02_W2HWAJU = new TYData("DAT02_W2HWAJU", null);   
        }

        private void TYACEI006G_Load(object sender, System.EventArgs e)
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


            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
            this.BTN61_PRT.ProcessCheck += new TButton.CheckHandler(BTN61_PRT_ProcessCheck);

            this.CBO01_CBOJUNPYO.SetValue("1");
            this.BTN61_BATCH.Text = "전표발행";
            this.TXT01_E7HDAC.SetValue("");
            
            
            this.DTP01_E7DTBG.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            this.CBO01_E7IDBG.SetValue("12");
            
            this.CBH01_B2DPMK.DummyValue = this.DTP01_E7DTBG.GetString();  

            this.CBH01_B2DPMK.SetValue(TYUserInfo.DeptCode);
            this.CBH01_B2HISAB.SetValue(TYUserInfo.EmpNo);     

            this.CBO01_E7IDBG.SetReadOnly(true); 
 
        }
        #endregion

        #region Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            //전표발행
            if (CBO01_CBOJUNPYO.GetValue().ToString() == "1")
            {
                UP_Set_CreateJunPyo(ds);

                this.CBO01_CBOJUNPYO.SetValue("2");

            }
            else //전표취소
            {
                UP_Set_CanCelJunPyo(ds);

                TXT01_E7HDAC.SetValue("");

                this.CBO01_CBOJUNPYO.SetValue("1");
            }

            this.BTN61_INQ_Click(null, null); 
        }
        #endregion

        #region Description : 종료 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        #endregion

        #region Description : 조회 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_25N69617.Initialize();

            if (CBO01_CBOJUNPYO.GetValue().ToString() == "1")
            {
                UP_Set_SearchJunPyo_Create();
            }
            else
            {
                UP_Set_SearchJunPyo_Cancel();
            }
        }
        #endregion

        #region Description : 조회 ProcessCheck 이벤트
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            this.FPS91_TY_S_AC_25N69617.Initialize();

            //전표취소
            if (this.CBO01_CBOJUNPYO.GetValue().ToString() == "2")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2CQ7H347", CBO01_E7IDBG.GetValue(), DTP01_E7DTBG.GetString());

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    TXT01_E7HDAC.SetValue(dt.Rows[0]["E7HDAC"].ToString());
                }
                else
                {
                    this.TXT01_E7HDAC.SetValue("");
                }

                if (this.TXT01_E7HDAC.GetValue().ToString().Trim().Length < 17)
                {
                    this.ShowMessage("TY_M_AC_25O9D621");
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                this.TXT01_E7HDAC.SetValue("");  
            }
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(this.FPS91_TY_S_AC_25N69617.GetDataSourceInclude(TSpread.TActionType.Select, "E7NONR", "E7IDBG", "E7DTBG", "E7DSTR1", "E7DSNR", "E7DSYN", "E7AMCH", "E7INNR", "E7CDGL", "E7CDCM", "E7CDSB", "E7CDCL", "E7NOACB", "E7NOACI", "E6AMNR"));                   
                        
            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                e.Successed = false;
                return;
            }

            if (CBO01_CBOJUNPYO.GetValue().ToString() == "1")
            {

                if (ds.Tables[0].Rows.Count > 49)
                {
                    this.ShowMessage("TY_M_AC_29I91156");
                    e.Successed = false;
                    return;
                }

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

            e.ArgData = ds;
  
        }
        #endregion

        #region Description : 전표 생성 이벤트
        private void UP_Set_CreateJunPyo(DataSet ds)
        {
            string sB2SSID = "";
            string sW2RKAC = "";
            
            double dE6AMNRTotal = 0;
            double dE7DSNRTotal = 0;
            double dB2AMDR = 0;


            bool bJunPyoFlag = false;

            //1-할인료미포함
            // 제예금(11100302)           / 할인어음 (21100404)
            // 매출채권처분손실(52000600)  /  제예금(11100302)

            //2-할인료포함
            // 제예금(11100302)           / 할인어음 (21100404)
            // 매출채권처분손실(52000600)  /               
             
            //UP_Set_CreateJunPyo

            DataTable table = new DataTable();
            table = ds.Tables[0].Clone();

            foreach (DataRow dr in ds.Tables[0].Select("E7NONR <> ''", "E7NONR ASC"))
                table.Rows.Add(dr.ItemArray);

            ds.Tables.Remove(ds.Tables[0]);

            ds.Tables.Add(table);  
            
            int iCnt = 0;

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            DataTable dt = new DataTable(); 

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            sB2SSID = this.IPAdresss + this.CBH01_B2HISAB.GetValue().ToString() + dAutoSeq.ToString();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //차변 제예금
                iCnt = iCnt + 1;

                //어음 총금액
                dE6AMNRTotal = UP_Get_FundTotal(ds, "1");
                //할인료 총금액
                dE7DSNRTotal = UP_Get_FundTotal(ds, "2");

                if (this.CBO01_GOKCR.GetValue().ToString() == "1")
                {
                    dB2AMDR = dE6AMNRTotal;
                }
                else
                {
                    dB2AMDR = dE6AMNRTotal - dE7DSNRTotal;
                }

                #region Description : 차변 제예금

                this.DAT02_W2SSID.SetValue(sB2SSID);
                this.DAT02_W2DPMK.SetValue(this.CBH01_B2DPMK.GetValue());
                this.DAT02_W2DTMK.SetValue(this.DTP01_E7DTBG.GetString());
                this.DAT02_W2NOSQ.SetValue("0");
                this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                this.DAT02_W2IDJP.SetValue("3");
                this.DAT02_W2NOJP.SetValue("");
                this.DAT02_W2CDAC.SetValue(ds.Tables[0].Rows[0]["E7NOACI"].ToString());
                this.DAT02_W2DTAC.SetValue("");
                this.DAT02_W2DTLI.SetValue("");
                this.DAT02_W2DPAC.SetValue(this.CBH01_B2DPMK.GetValue());

                //관리항목 
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_23N3M888", ds.Tables[0].Rows[0]["E7NOACI"].ToString(), "");
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
                if (ds.Tables[0].Rows[0]["E7NOACI"].ToString() == "11100301" || ds.Tables[0].Rows[0]["E7NOACI"].ToString() == "11100302")
                {
                    //은행
                    this.DAT02_W2VLMI1.SetValue(ds.Tables[0].Rows[0]["E7CDGL"].ToString());
                    //계좌
                    this.DAT02_W2VLMI2.SetValue(ds.Tables[0].Rows[0]["E7NOACB"].ToString());
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");
                }
                sW2RKAC = "어음할인";

                this.DAT02_W2AMDR.SetValue(dB2AMDR.ToString());
                this.DAT02_W2AMCR.SetValue("0");

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
                #endregion

                //차변 매출채권처분손실(52000600)
                #region Description : 차변 매출채권처분손실
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(this.CBH01_B2DPMK.GetValue());
                    this.DAT02_W2DTMK.SetValue(this.DTP01_E7DTBG.GetString());
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue("52000600");
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH01_B2DPMK.GetValue());

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", "52000600", "");
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

                        //은행
                        this.DAT02_W2VLMI1.SetValue(ds.Tables[0].Rows[i]["E7CDGL"].ToString());
                        //할인율
                        this.DAT02_W2VLMI2.SetValue(ds.Tables[0].Rows[i]["E7DSTR1"].ToString());
                        this.DAT02_W2VLMI3.SetValue("");
                        this.DAT02_W2VLMI4.SetValue("");
                        this.DAT02_W2VLMI5.SetValue("");
                        this.DAT02_W2VLMI6.SetValue("");

                    sW2RKAC = "어음할인";

                    this.DAT02_W2AMDR.SetValue(ds.Tables[0].Rows[i]["E7DSNR"].ToString());
                    this.DAT02_W2AMCR.SetValue("0");

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
                } // for..end
                #endregion

                //대변 할인어음(21100404)
                #region Description : 대변 할인어음
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(this.CBH01_B2DPMK.GetValue());
                    this.DAT02_W2DTMK.SetValue(this.DTP01_E7DTBG.GetString());
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue("21100404");
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH01_B2DPMK.GetValue());

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", "21100404", "");
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
                    this.DAT02_W2VLMI1.SetValue(ds.Tables[0].Rows[i]["E7CDCL"].ToString());
                    //은행
                    this.DAT02_W2VLMI2.SetValue(ds.Tables[0].Rows[i]["E7CDGL"].ToString());
                    //어음번호
                    this.DAT02_W2VLMI3.SetValue(ds.Tables[0].Rows[i]["E7NONR"].ToString());
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");
                  
                    sW2RKAC = "어음할인";

                    this.DAT02_W2AMDR.SetValue("0");
                    this.DAT02_W2AMCR.SetValue(ds.Tables[0].Rows[i]["E6AMNR"].ToString());

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
                } //for..end
                #endregion

                if (this.CBO01_GOKCR.GetValue().ToString() == "1")
                {
                    //대변 제예금 등록
                    iCnt = iCnt + 1;

                    #region Description : 대변 제예금

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(this.CBH01_B2DPMK.GetValue());
                    this.DAT02_W2DTMK.SetValue(this.DTP01_E7DTBG.GetString());
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(ds.Tables[0].Rows[0]["E7NOACI"].ToString());
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH01_B2DPMK.GetValue());

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", ds.Tables[0].Rows[0]["E7NOACI"].ToString(), "");
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
                    if (ds.Tables[0].Rows[0]["E7NOACI"].ToString() == "11100301" || ds.Tables[0].Rows[0]["E7NOACI"].ToString() == "11100302")
                    {
                        //은행
                        this.DAT02_W2VLMI1.SetValue(ds.Tables[0].Rows[0]["E7CDGL"].ToString());
                        //계좌
                        this.DAT02_W2VLMI2.SetValue(ds.Tables[0].Rows[0]["E7NOACB"].ToString());
                        this.DAT02_W2VLMI3.SetValue("");
                        this.DAT02_W2VLMI4.SetValue("");
                        this.DAT02_W2VLMI5.SetValue("");
                        this.DAT02_W2VLMI6.SetValue("");
                    }
                    sW2RKAC = "어음할인";

                    this.DAT02_W2AMDR.SetValue("0");
                    this.DAT02_W2AMCR.SetValue(dE7DSNRTotal.ToString());

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
            this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, CBH01_B2HISAB.GetValue(), "A",
                                this.CBH01_B2DPMK.GetValue(), this.DTP01_E7DTBG.GetString(), "", "",
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
                            TXT01_E7HDAC.SetValue(sJpno);
                        }
                    }
                }
            }

            if (bJunPyoFlag)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string sJPNO = TXT01_E7HDAC.GetValue().ToString().Replace("-", "");

                    UP_Set_JpnoStaus(sJPNO, ds);
                                        
                    this.ShowMessage("TY_M_AC_25O8K620");
                }
            } 

             
        }
        #endregion

        #region Description : 전표 취소 이벤트
        private void UP_Set_CanCelJunPyo(DataSet ds )
        {
            string sB2SSID = "";
            string sJpno = "";

            DataTable table = new DataTable();
            table = ds.Tables[0].Clone();

            foreach (DataRow dr in ds.Tables[0].Select("E7NONR <> ''", "E7NONR ASC"))
                table.Rows.Add(dr.ItemArray);

            ds.Tables.Remove(ds.Tables[0]);

            ds.Tables.Add(table);  

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            sB2SSID = this.IPAdresss + TYUserInfo.EmpNo + dAutoSeq.ToString();

            sJpno = this.TXT01_E7HDAC.GetValue().ToString().Replace("-", "");

            //미승인전표 -> 임시파일 입력
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7K957", sB2SSID, sJpno.ToString().Substring(0, 6), sJpno.ToString().Substring(6, 8), sJpno.ToString().Substring(14, 3));
            //미승인 SP호출 파일 입력
            this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, TYUserInfo.EmpNo, "D",
                                TYUserInfo.DeptCode, this.DTP01_E7DTBG.GetString(), "", "",
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
                UP_Set_JpnoStaus("", ds);
                
                this.ShowMessage("TY_M_AC_25O8K620");
            }
        }
        #endregion

        #region Description : 합계처리 UP_SumRowAdd 이벤트
        private DataTable UP_SumRowAdd(DataTable dt)
        {
            DataRow row;
            int i = dt.Rows.Count;

            if (i > 0)
            {
                row = dt.NewRow();
                dt.Rows.InsertAt(row, i);

                dt.Rows[i]["E7HDAC"] = "합 계";
                dt.Rows[i]["E7DSNR"] = dt.Compute("SUM(E7DSNR)", "").ToString();
                dt.Rows[i]["E7AMCH"] = dt.Compute("SUM(E7AMCH)", "").ToString();
                dt.Rows[i]["E7INNR"] = dt.Compute("SUM(E7INNR)", "").ToString();
            }

            return dt;
        }
        #endregion

        #region Description : DTP01_E7DTBG_ValueChanged 이벤트
        private void DTP01_E7DTBG_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_B2DPMK.DummyValue = this.DTP01_E7DTBG.GetString();
        }
        #endregion

        #region Description : CBO01_CBOJUNPYO_SelectedIndexChanged 이벤트
        private void CBO01_CBOJUNPYO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBO01_CBOJUNPYO.GetValue().ToString() == "1")
            {
                this.BTN61_BATCH.Text = "전표발행";
            }
            else
            {
                this.BTN61_BATCH.Text = "전표취소";
            }
        }
        #endregion

        #region Description : UP_Get_FundTotal 함수
        private double UP_Get_FundTotal(DataSet ds, string sGubn)
        {
            double dAMNR = 0;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (sGubn == "1")
                {
                    dAMNR += Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i]["E6AMNR"].ToString()));
                }
                else
                {
                    dAMNR += Convert.ToDouble(Get_Numeric(ds.Tables[0].Rows[i]["E7DSNR"].ToString()));
                }
            }

            return dAMNR;
        }
        #endregion

        #region Description : 전표번호 UPDATE 
        private void UP_Set_JpnoStaus(string sJPNO, DataSet ds)
        {
            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            if (ds.Tables[0].Rows.Count > 0)
            {
                datas.Clear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    datas.Add(new object[] { sJPNO, ds.Tables[0].Rows[i]["E7NONR"].ToString(), ds.Tables[0].Rows[i]["E7IDBG"].ToString(), ds.Tables[0].Rows[i]["E7DTBG"].ToString() });
                }
                if (datas.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    foreach (object[] data in datas)
                    {
                        this.DbConnector.Attach("TY_P_AC_29H6L147", data);
                    }
                    this.DbConnector.ExecuteTranQueryList();
                }
            }
        }
        #endregion

        #region Description : 전표발행 생성 조회
        private void UP_Set_SearchJunPyo_Create()
        {
            this.FPS91_TY_S_AC_25N69617.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_25N60614", CBO01_E7IDBG.GetValue(), DTP01_E7DTBG.GetString(), TXT01_E7HDAC.GetValue(), TXT01_E7HDAC.GetValue());

            DataTable dt = UP_SumRowAdd(this.DbConnector.ExecuteDataTable());

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
            this.FPS91_TY_S_AC_25N69617.SetValue(dt);
            this.SetSpreadSumRow(this.FPS91_TY_S_AC_25N69617, "E7HDAC", "합 계", SumRowType.Total);
            
            this.BTN61_BATCH.Text = "전표발행";
        }
        #endregion

        #region Description : 전표발행 취소 조회
        private void UP_Set_SearchJunPyo_Cancel()
        {
            this.FPS91_TY_S_AC_25N69617.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2CQ7H347", CBO01_E7IDBG.GetValue(), DTP01_E7DTBG.GetString());

            DataTable dt = UP_SumRowAdd(this.DbConnector.ExecuteDataTable());

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }

            this.FPS91_TY_S_AC_25N69617.SetValue(dt);

            this.SetSpreadSumRow(this.FPS91_TY_S_AC_25N69617, "E7HDAC", "합 계", SumRowType.Total);

            this.BTN61_BATCH.Text = "전표취소";
        }
        #endregion

        #region Description : 전표출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sJPNO = TXT01_E7HDAC.GetValue().ToString().Replace("-", "");

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

            this.SetFocus(this.CBO01_CBOJUNPYO);
        }
        #endregion

        #region Description : 전표출력 ProcessCheck 이벤트
        private void BTN61_PRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.TXT01_E7HDAC.GetValue().ToString().Trim() == "" && TXT01_E7HDAC.GetValue().ToString().Replace("-", "").Trim().Length < 17 )
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
