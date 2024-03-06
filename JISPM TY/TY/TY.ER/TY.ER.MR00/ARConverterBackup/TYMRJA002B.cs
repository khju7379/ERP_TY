using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using DataDynamics.ActiveReports;
using TY.ER.GB00;
using TY.ER.AC00;

namespace TY.ER.MR00
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
    ///  TY_S_MR_32J7S131 : 선급자재 DETAIL 하위 조회
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
    public partial class TYMRJA002B : TYBase
    {
        private string fsFXDNUM = string.Empty;

        private string fsJASANNUM = string.Empty;
        private string fsPONUM    = string.Empty;
        private string fsRRNUM    = string.Empty;
        private string fsVEND     = string.Empty;
        private string fsITEMCODE = string.Empty;
        private string fsCGVEND   = string.Empty;
        private string fsCHGUBUN  = string.Empty;
        private string fsGUBUN    = string.Empty;

        #region Description : 미승인 자료 처리
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
        #endregion

        #region Description : 페이지 로드
        public TYMRJA002B()
        {
            InitializeComponent();

            #region Description : 미승인 자료 처리
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
            #endregion

            // 고정자산번호
            this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_32J7S131, "FXYSJASAN", "FXSNAME", "FXYSJASAN");

            //// 부가세
            //this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_32J7S131, "FXYSVATGB", "TXDESC1",    "FXYSVATGB");

            // 단위
            this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_32J7M130, "FXSUNIT",   "UTDESC1",    "FXSUNIT");
            // 자산분류코드
            this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_32J7M130, "FXSCLASS",  "FXSCLASSNM", "FXSCLASS");
            // 구입처
            this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_32J7M130, "FXSBUYCOM", "VNSANGHO",   "FXSBUYCOM");
            // 설치위치
            this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_32J7M130, "FXSSITE",   "PODESC",     "FXSSITE");
            //사용부서
            this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_32J7M130, "FXSBUSEO",  "DTDESC",     "FXSBUSEO");

            // 청구거래처
            this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_32J7M130, "FXSCGVEND", "FXSCGVENDNM", "FXSCGVEND");
            // 부가세구분
            this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_32J7M130, "FXSVATGB", "TXDESC1", "FXSVATGB");

            
            //// 설치위치
            //this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_32J7S131, "FXTFITSITE",  "PODESC",     "FXTFITSITE");
            //// 구조
            //this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_32J7S131, "FXTSTRUCT",   "SUDESC",     "FXTSTRUCT");
            //// 재질
            //this.SetSpreadCodeHelper(this.FPS91_TY_S_MR_32J7S131, "FXTMATERIAL", "MEDESC",     "FXTMATERIAL");
        }

        private void TYMRJA002B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_CREATE.ProcessCheck += new TButton.CheckHandler(BTN61_CREATE_ProcessCheck);
            this.BTN61_CANCEL.ProcessCheck += new TButton.CheckHandler(BTN61_CANCEL_ProcessCheck);

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);
            this.BTN61_SUNGUB_CANCEL.ProcessCheck += new TButton.CheckHandler(BTN61_SUNGUB_CANCEL_ProcessCheck);

            //this.BTN61_JPNO_CRE.ProcessCheck += new TButton.CheckHandler(BTN61_JPNO_CRE_ProcessCheck);
            //this.BTN61_JPNO_DEL.ProcessCheck += new TButton.CheckHandler(BTN61_JPNO_DEL_ProcessCheck);

            this.BTN61_SAV_JASAN.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_JASAN_ProcessCheck);

            this.BTN61_BATCH_JASAN.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_JASAN_ProcessCheck);
            this.BTN62_SUNGUB_CANCEL.ProcessCheck += new TButton.CheckHandler(BTN62_SUNGUB_CANCEL_ProcessCheck);
            
            this.BTN61_JPNO_CRE.ProcessCheck += new TButton.CheckHandler(BTN61_JPNO_CRE_Click_ProcessCheck);
            this.BTN61_JPNO_DEL.ProcessCheck += new TButton.CheckHandler(BTN61_JPNO_DEL_Click_ProcessCheck);


            #region Description :미승인 처리
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
            #endregion

            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_MR_32J7S131, "FXYSCDAC");

            // 귀속부서 검토부서
            this.CBH01_FXDDPMK.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            this.TXT01_FXDSAUPNM.SetReadOnly(true);

            this.BTN61_CREATE.Visible = true;
            this.BTN61_CANCEL.Visible = false;

            tabControl1_Enable("QUERY");

            SetStartingFocus(this.CBH01_FXDDPMK.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            if (this.CBO01_GGUBUN.GetValue().ToString() == "A") // 선급자재 생성할 자료 조회
            {
                this.TXT01_FXDSAUP.SetValue("");     // 선급자재번호
                this.TXT01_FXDGETDATE.SetValue("");  // 취득일자
                this.TXT01_GDAESANGHAP.SetValue(""); // 대상총액

                fsGUBUN = "";

                // 청구구분
                fsCHGUBUN = "";
                fsCHGUBUN = this.CBO01_PRM6020.GetValue().ToString();

                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();

                tabControl1_Enable("MANAGEMENT");

                this.BTN61_CREATE.Visible = true;
                this.BTN61_CANCEL.Visible = false;
                this.BTN61_SAV_JASAN.Visible = false;




                this.BTN61_BATCH.Visible         = false;
                this.BTN61_SUNGUB_CANCEL.Visible = false;



                // 선급자재 미생성 조회
                this.FPS91_TY_S_MR_32J7C129.Visible = true;
                // 선급자재 생성 조회
                this.FPS91_TY_S_MR_3381M258.Visible = false;

                if (this.TXT01_GSTDATE.GetValue().ToString() == "" && this.TXT01_GEDDATE.GetValue().ToString() == "")
                {
                    // 품목별 입고 완료시 조회 2021.11.17
                    this.DbConnector.Attach
                    (
                    "TY_P_MR_BBHFA751",
                    this.CBO01_PRM6020.GetValue().ToString(),
                    this.CBH01_FXDDPMK.GetValue().ToString()
                    );

                    //원본
                    //this.DbConnector.Attach
                    //(
                    //"TY_P_MR_32J79125",
                    //this.CBO01_PRM6020.GetValue().ToString(),
                    //this.CBH01_FXDDPMK.GetValue().ToString()
                    //);
                }
                else
                {
                    // 품목별 입고 완료시 조회 2021.11.17
                    this.DbConnector.Attach
                    (
                    "TY_P_MR_BCAA3903",
                    this.CBO01_PRM6020.GetValue().ToString(),
                    this.CBH01_FXDDPMK.GetValue().ToString(),
                    this.TXT01_GSTDATE.GetValue().ToString(),
                    this.TXT01_GEDDATE.GetValue().ToString()
                    );
                    
                    //원본
                    //this.DbConnector.Attach
                    //(
                    //"TY_P_MR_3381U259",
                    //this.CBO01_PRM6020.GetValue().ToString(),
                    //this.CBH01_FXDDPMK.GetValue().ToString(),
                    //this.TXT01_GSTDATE.GetValue().ToString(),
                    //this.TXT01_GEDDATE.GetValue().ToString()
                    //);
                }

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.FPS91_TY_S_MR_32J7C129.SetValue(dt);

                    DataTable NewDt = new DataTable();
                    NewDt.Clear();

                    // 선급자재 예산
                    this.FPS91_TY_S_MR_32J7S131.SetValue(NewDt);
                    // 선급자재 내역
                    this.FPS91_TY_S_MR_32J7M130.SetValue(NewDt);
                }
                else
                {
                    // 선급자재 마스터
                    this.FPS91_TY_S_MR_32J7C129.SetValue(dt);
                    // 선급자재 예산
                    this.FPS91_TY_S_MR_32J7S131.SetValue(dt);
                    // 선급자재 내역
                    this.FPS91_TY_S_MR_32J7M130.SetValue(dt);
                }
            }
            else // 선급자재 생성된 자료 조회
            {
                fsGUBUN = "SUNGUB";

                tabControl1_Enable("QUERY");

                DataTable dt = new DataTable();

                // 선급자재 마스터
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_3385X260",
                    this.CBH01_FXDDPMK.GetValue().ToString(),
                    this.TXT01_FXDSAUP.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_MR_33860261.SetValue(dt);

                this.BTN61_CREATE.Visible    = false;
                this.BTN61_CANCEL.Visible    = true;
                this.BTN61_SAV_JASAN.Visible = true;

                this.BTN61_BATCH.Visible         = true;
                this.BTN61_SUNGUB_CANCEL.Visible = true;
            }
        }
        #endregion

        //#region Description : 선급자재조회 버튼
        //private void BTN61_JASAN_INQ_Click(object sender, EventArgs e)
        //{
        //    fsGUBUN = "SUNGUB";

        //    tabControl1_Enable("QUERY");

        //    DataTable dt = new DataTable();

        //    // 선급자재 마스터
        //    this.DbConnector.CommandClear();
        //    this.DbConnector.Attach
        //        (
        //        "TY_P_MR_3385X260",
        //        this.CBH01_FXDDPMK.GetValue().ToString(),
        //        this.TXT01_FXDSAUP.GetValue().ToString()
        //        );

        //    dt = this.DbConnector.ExecuteDataTable();

        //    this.FPS91_TY_S_MR_33860261.SetValue(dt);
        //}
        //#endregion

        #region Description : 선급자재 생성버튼
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            // 청구구분
            fsCHGUBUN = "";
            fsCHGUBUN = this.CBO01_PRM6020.GetValue().ToString();

            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            // 선급자재 마스터
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_MR_32L3B143", fsFXDNUM.ToString().Substring(0, 1),
                                                            fsFXDNUM.ToString().Substring(1, 1),
                                                            fsFXDNUM.ToString().Substring(2, 6),
                                                            fsFXDNUM.ToString().Substring(8, 4),
                                                            this.CBH01_FXDDPMK.GetValue().ToString(),
                                                            ds.Tables[0].Rows[i]["FXDPONUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXDRRNUM"].ToString(),
                                                            this.TXT01_FXDGETDATE.GetValue().ToString(),
                                                            ds.Tables[0].Rows[i]["FXDPOAMOUNT"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXDGETVEND"].ToString(),
                                                            this.fsCHGUBUN.ToString(),
                                                            ds.Tables[0].Rows[i]["FXDITEMCODE"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXDITEMSEQ"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXDRRAMOUNT"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXDCGVEND"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXDREPJPNO"].ToString(),
                                                            TYUserInfo.EmpNo);
            }

            this.DbConnector.ExecuteTranQueryList();

            // 선급자재 번호
            this.TXT01_FXDSAUP.SetValue(fsFXDNUM.ToString());

            tabControl1_Enable("MANAGEMENT");
            // 선급자재 생성 조회
            UP_SEL_ACFIXADMF(fsFXDNUM.ToString());

            this.ShowMessage("TY_M_GB_26E30875");
            SetStartingFocus(this.TXT01_FXDSAUP);
        }
        #endregion

        #region Description : 선급자재 취소버튼
        private void BTN61_CANCEL_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            // 선급자재 마스터
            this.DbConnector.Attach("TY_P_MR_32L3D144", this.TXT01_FXDSAUP.GetValue().ToString().Substring(0, 1),
                                                        this.TXT01_FXDSAUP.GetValue().ToString().Substring(1, 1),
                                                        this.TXT01_FXDSAUP.GetValue().ToString().Substring(2, 6),
                                                        this.TXT01_FXDSAUP.GetValue().ToString().Substring(8, 4));

            //// 선급자재 예산
            //this.DbConnector.Attach("TY_P_MR_33C9Y274", this.TXT01_FXDSAUP.GetValue().ToString().Substring(0, 1),
            //                                            this.TXT01_FXDSAUP.GetValue().ToString().Substring(1, 1),
            //                                            this.TXT01_FXDSAUP.GetValue().ToString().Substring(2, 6),
            //                                            this.TXT01_FXDSAUP.GetValue().ToString().Substring(8, 4));

            //// 선급자재 내역
            //this.DbConnector.Attach("TY_P_MR_32L6G153", this.TXT01_FXDSAUP.GetValue().ToString().Substring(0, 1),
            //                                            this.TXT01_FXDSAUP.GetValue().ToString().Substring(1, 1),
            //                                            this.TXT01_FXDSAUP.GetValue().ToString().Substring(2, 6),
            //                                            this.TXT01_FXDSAUP.GetValue().ToString().Substring(8, 4));

            //// 선급자재 하위 내역
            //this.DbConnector.Attach("TY_P_MR_32L6G154", this.TXT01_FXDSAUP.GetValue().ToString().Substring(0, 1),
            //                                            this.TXT01_FXDSAUP.GetValue().ToString().Substring(1, 1),
            //                                            this.TXT01_FXDSAUP.GetValue().ToString().Substring(2, 6),
            //                                            this.TXT01_FXDSAUP.GetValue().ToString().Substring(8, 4));

            this.DbConnector.ExecuteTranQueryList();

            // 선급자재 번호
            this.TXT01_FXDSAUP.SetValue("");

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_AC_2CDB1167");

            // 대상총액
            this.TXT01_GDAESANGHAP.SetValue("");

            SetStartingFocus(this.TXT01_FXDSAUP);
        }
        #endregion

        #region Description : 예산별 생성버튼
        private void BTN61_SAV_JASAN_Click(object sender, EventArgs e)
        {
            
            // 선급자재 내역
            this.DbConnector.CommandClear();
            // 합산 구분 필드 추가
            this.DbConnector.Attach("TY_P_MR_BC1GA859", TYUserInfo.EmpNo,
                                                        this.TXT01_FXDSAUP.GetValue().ToString().Substring(0, 1),
                                                        this.TXT01_FXDSAUP.GetValue().ToString().Substring(1, 1),
                                                        this.TXT01_FXDSAUP.GetValue().ToString().Substring(2, 6),
                                                        this.TXT01_FXDSAUP.GetValue().ToString().Substring(8, 4)
                                                        );

            // 원본
            //this.DbConnector.Attach("TY_P_MR_32L3S145", TYUserInfo.EmpNo,
            //                                            this.TXT01_FXDSAUP.GetValue().ToString().Substring(0, 1),
            //                                            this.TXT01_FXDSAUP.GetValue().ToString().Substring(1, 1),
            //                                            this.TXT01_FXDSAUP.GetValue().ToString().Substring(2, 6),
            //                                            this.TXT01_FXDSAUP.GetValue().ToString().Substring(8, 4)
            //                                            );

            this.DbConnector.ExecuteNonQuery();
            this.ShowMessage("TY_M_MR_33C5X282");

            tabControl1_Enable("MANAGEMENT");
            DataTable dt = new DataTable();

            // 선급자재 내역
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_32J7A127",
                TXT01_FXDSAUP.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_32J7M130.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_MR_32J7M130.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSJPNO").ToString() == "")
                {
                    this.FPS91_TY_S_MR_32J7M130_Sheet1.Cells[i, 17].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                }
            }
        }
        #endregion

        #region Description : 선급자재 예산 처리버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            // 청구구분
            fsCHGUBUN = "";
            fsCHGUBUN = this.CBO01_PRM6020.GetValue().ToString();

            string sFXYSCGVEND = string.Empty;

            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            // 저장
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sFXYSCGVEND = ds.Tables[0].Rows[i]["FXYSCGVEND"].ToString();

                if (ds.Tables[0].Rows[i]["FXYSCDAC"].ToString() == "12200100" ||
                    ds.Tables[0].Rows[i]["FXYSCDAC"].ToString() == "12200200" ||
                    ds.Tables[0].Rows[i]["FXYSCDAC"].ToString() == "12200300" ||
                    ds.Tables[0].Rows[i]["FXYSCDAC"].ToString() == "12200400" ||
                    ds.Tables[0].Rows[i]["FXYSCDAC"].ToString() == "12200500" ||
                    ds.Tables[0].Rows[i]["FXYSCDAC"].ToString() == "12200600" ||
                    ds.Tables[0].Rows[i]["FXYSCDAC"].ToString() == "12200700" ||
                    ds.Tables[0].Rows[i]["FXYSCDAC"].ToString() == "12200800" ||
                    ds.Tables[0].Rows[i]["FXYSCDAC"].ToString() == "12200900" ||
                    ds.Tables[0].Rows[i]["FXYSCDAC"].ToString() == "12210000"
                    )
                {
                    sFXYSCGVEND = "";
                }

                // 합계 필드 추가 (키로 변경)
                this.DbConnector.Attach("TY_P_MR_C159S987", ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(0, 1),
                                                            ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(1, 1),
                                                            ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(2, 6),
                                                            ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(8, 4),
                                                            ds.Tables[0].Rows[i]["FXYSPONUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXYSRRNUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXYSGETVEND"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXYSITEMCD"].ToString(),
                                                            sFXYSCGVEND.ToString(),
                                                            this.fsCHGUBUN.ToString(),
                                                            ds.Tables[0].Rows[i]["FXYSCDAC"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXYSYCNUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXYSJASAN"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXCOMBINE"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXYSAMOUNT"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXYSVATGB"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXYSJASANCD"].ToString(),
                                                            TYUserInfo.EmpNo
                                                            );

                //// 합계 필드 추가
                //this.DbConnector.Attach("TY_P_MR_BC1FV854", ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(0, 1),
                //                                            ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(1, 1),
                //                                            ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(2, 6),
                //                                            ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(8, 4),
                //                                            ds.Tables[0].Rows[i]["FXYSPONUM"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSRRNUM"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSGETVEND"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSITEMCD"].ToString(),
                //                                            sFXYSCGVEND.ToString(),
                //                                            this.fsCHGUBUN.ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSCDAC"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSYCNUM"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSJASAN"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSAMOUNT"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSVATGB"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSJASANCD"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXCOMBINE"].ToString(),
                //                                            TYUserInfo.EmpNo
                //                                            );

                //this.DbConnector.Attach("TY_P_MR_33C1F278", ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(0, 1),
                //                                            ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(1, 1),
                //                                            ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(2, 6),
                //                                            ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(8, 4),
                //                                            ds.Tables[0].Rows[i]["FXYSPONUM"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSRRNUM"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSGETVEND"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSITEMCD"].ToString(),
                //                                            sFXYSCGVEND.ToString(),
                //                                            this.fsCHGUBUN.ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSCDAC"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSYCNUM"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSJASAN"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSAMOUNT"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSVATGB"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSJASANCD"].ToString(),
                //                                            TYUserInfo.EmpNo
                //                                            );
            }

            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 합산 구분 추가 (키로 변경)
                this.DbConnector.Attach("TY_P_MR_C159Q985", ds.Tables[1].Rows[i]["FXYSAMOUNT"].ToString(),
                                                            ds.Tables[1].Rows[i]["FXYSVATGB"].ToString(),
                                                            ds.Tables[1].Rows[i]["FXYSJASANCD"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["FXYSNUM"].ToString().Substring(0, 1),
                                                            ds.Tables[1].Rows[i]["FXYSNUM"].ToString().Substring(1, 1),
                                                            ds.Tables[1].Rows[i]["FXYSNUM"].ToString().Substring(2, 6),
                                                            ds.Tables[1].Rows[i]["FXYSNUM"].ToString().Substring(8, 4),
                                                            ds.Tables[1].Rows[i]["FXYSPONUM"].ToString(),
                                                            ds.Tables[1].Rows[i]["FXYSRRNUM"].ToString(),
                                                            ds.Tables[1].Rows[i]["FXYSGETVEND"].ToString(),
                                                            ds.Tables[1].Rows[i]["FXYSITEMCD"].ToString(),
                                                            ds.Tables[1].Rows[i]["FXYSCGVEND"].ToString(),
                                                            this.fsCHGUBUN.ToString(),
                                                            ds.Tables[1].Rows[i]["FXYSCDAC"].ToString(),
                                                            ds.Tables[1].Rows[i]["FXYSYCNUM"].ToString(),
                                                            ds.Tables[1].Rows[i]["FXYSJASAN"].ToString(),
                                                            ds.Tables[1].Rows[i]["FXCOMBINE"].ToString()
                                                            );

                //// 합산 구분 필드 추가
                //this.DbConnector.Attach("TY_P_MR_BC1FY856", ds.Tables[1].Rows[i]["FXYSAMOUNT"].ToString(),
                //                                            ds.Tables[1].Rows[i]["FXYSVATGB"].ToString(),
                //                                            ds.Tables[1].Rows[i]["FXYSJASANCD"].ToString(),
                //                                            ds.Tables[1].Rows[i]["FXCOMBINE"].ToString(),
                //                                            TYUserInfo.EmpNo,
                //                                            ds.Tables[1].Rows[i]["FXYSNUM"].ToString().Substring(0, 1),
                //                                            ds.Tables[1].Rows[i]["FXYSNUM"].ToString().Substring(1, 1),
                //                                            ds.Tables[1].Rows[i]["FXYSNUM"].ToString().Substring(2, 6),
                //                                            ds.Tables[1].Rows[i]["FXYSNUM"].ToString().Substring(8, 4),
                //                                            ds.Tables[1].Rows[i]["FXYSPONUM"].ToString(),
                //                                            ds.Tables[1].Rows[i]["FXYSRRNUM"].ToString(),
                //                                            ds.Tables[1].Rows[i]["FXYSGETVEND"].ToString(),
                //                                            ds.Tables[1].Rows[i]["FXYSITEMCD"].ToString(),
                //                                            ds.Tables[1].Rows[i]["FXYSCGVEND"].ToString(),
                //                                            this.fsCHGUBUN.ToString(),
                //                                            ds.Tables[1].Rows[i]["FXYSCDAC"].ToString(),
                //                                            ds.Tables[1].Rows[i]["FXYSYCNUM"].ToString(),
                //                                            ds.Tables[1].Rows[i]["FXYSJASAN"].ToString()
                //                                            );

                // 원본
                //this.DbConnector.Attach("TY_P_MR_33C1J279", ds.Tables[1].Rows[i]["FXYSAMOUNT"].ToString(),
                //                                            ds.Tables[1].Rows[i]["FXYSVATGB"].ToString(),
                //                                            ds.Tables[1].Rows[i]["FXYSJASANCD"].ToString(),
                //                                            TYUserInfo.EmpNo,
                //                                            ds.Tables[1].Rows[i]["FXYSNUM"].ToString().Substring(0, 1),
                //                                            ds.Tables[1].Rows[i]["FXYSNUM"].ToString().Substring(1, 1),
                //                                            ds.Tables[1].Rows[i]["FXYSNUM"].ToString().Substring(2, 6),
                //                                            ds.Tables[1].Rows[i]["FXYSNUM"].ToString().Substring(8, 4),
                //                                            ds.Tables[1].Rows[i]["FXYSPONUM"].ToString(),
                //                                            ds.Tables[1].Rows[i]["FXYSRRNUM"].ToString(),
                //                                            ds.Tables[1].Rows[i]["FXYSGETVEND"].ToString(),
                //                                            ds.Tables[1].Rows[i]["FXYSITEMCD"].ToString(),
                //                                            ds.Tables[1].Rows[i]["FXYSCGVEND"].ToString(),
                //                                            this.fsCHGUBUN.ToString(),
                //                                            ds.Tables[1].Rows[i]["FXYSCDAC"].ToString(),
                //                                            ds.Tables[1].Rows[i]["FXYSYCNUM"].ToString(),
                //                                            ds.Tables[1].Rows[i]["FXYSJASAN"].ToString()
                //                                            );
            }

            //// 삭제
            //for (i = 0; i < ds.Tables[2].Rows.Count; i++)
            //{
            //    this.DbConnector.Attach("TY_P_MR_33C1J280", ds.Tables[2].Rows[i]["FXYSNUM"].ToString().Substring(0, 1),
            //                                                ds.Tables[2].Rows[i]["FXYSNUM"].ToString().Substring(1, 1),
            //                                                ds.Tables[2].Rows[i]["FXYSNUM"].ToString().Substring(2, 6),
            //                                                ds.Tables[2].Rows[i]["FXYSNUM"].ToString().Substring(8, 4),
            //                                                ds.Tables[2].Rows[i]["FXYSPONUM"].ToString(),
            //                                                ds.Tables[2].Rows[i]["FXYSRRNUM"].ToString(),
            //                                                ds.Tables[2].Rows[i]["FXYSCGVEND"].ToString(),
            //                                                this.fsCHGUBUN.ToString(),
            //                                                ds.Tables[2].Rows[i]["FXYSCDAC"].ToString(),
            //                                                ds.Tables[2].Rows[i]["FXYSYCNUM"].ToString()
            //                                                );
            //}

            this.DbConnector.ExecuteNonQueryList();


            DataTable dt = new DataTable();

            // 선급자재 예산내역
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_33B4S269",
                fsJASANNUM.ToString(),
                fsPONUM.ToString(),
                fsRRNUM.ToString(),
                fsVEND.ToString(),
                fsITEMCODE.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_MR_32J7S131.SetValue(dt);

            double dFXYSAMOUNT = 0;
            string sFXYSAMOUNT = string.Empty;

            for (int j = 0; j < this.FPS91_TY_S_MR_32J7S131.ActiveSheet.RowCount; j++)
            {
                dFXYSAMOUNT = dFXYSAMOUNT + double.Parse(Get_Numeric(this.FPS91_TY_S_MR_32J7S131.GetValue(j, "FXYSAMOUNT").ToString()));
            }

            sFXYSAMOUNT = string.Format("{0:#,###}", dFXYSAMOUNT);



            // 선급자재 마스터 자산금액 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_35O4X738",
                Get_Numeric(sFXYSAMOUNT.ToString()),
                fsJASANNUM.ToString().Substring(0, 1),
                fsJASANNUM.ToString().Substring(1, 1),
                fsJASANNUM.ToString().Substring(2, 6),
                fsJASANNUM.ToString().Substring(8, 4),
                this.CBH01_FXDDPMK.GetValue().ToString(),
                fsPONUM.ToString(),
                fsRRNUM.ToString(),
                fsVEND.ToString(),
                fsITEMCODE.ToString()
                );

            this.DbConnector.ExecuteNonQueryList();

            // 선급자재 마스터 조회
            UP_SEL_ACFIXADMF(fsJASANNUM);

            this.ShowMessage("TY_M_MR_2BF50354"); // 처리완료 메세지

            // 선급자재 예산내역 존재체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_35S9S746",
                fsJASANNUM.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.BTN61_CANCEL.Visible = false;
            }
            else
            {
                this.BTN61_CANCEL.Visible = true;
            }

        }
        #endregion

        #region Description : 선급자재 예산 취소버튼
        private void BTN61_SUNGUB_CANCEL_Click(object sender, EventArgs e)
        {
            // 청구구분
            fsCHGUBUN = "";
            fsCHGUBUN = this.CBO01_PRM6020.GetValue().ToString();

            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            // 삭제
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 합산구분 추가 (키로 변경)
                this.DbConnector.Attach("TY_P_MR_C159U988", ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(0, 1),
                                                            ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(1, 1),
                                                            ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(2, 6),
                                                            ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(8, 4),
                                                            ds.Tables[0].Rows[i]["FXYSPONUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXYSRRNUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXYSGETVEND"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXYSITEMCD"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXYSCGVEND"].ToString(),
                                                            this.fsCHGUBUN.ToString(),
                                                            ds.Tables[0].Rows[i]["FXYSCDAC"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXYSYCNUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXYSJASAN"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXCOMBINE"].ToString()
                                                            );

                // 원본
                //this.DbConnector.Attach("TY_P_MR_33C1J280", ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(0, 1),
                //                                            ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(1, 1),
                //                                            ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(2, 6),
                //                                            ds.Tables[0].Rows[i]["FXYSNUM"].ToString().Substring(8, 4),
                //                                            ds.Tables[0].Rows[i]["FXYSPONUM"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSRRNUM"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSGETVEND"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSITEMCD"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSCGVEND"].ToString(),
                //                                            this.fsCHGUBUN.ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSCDAC"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXYSYCNUM"].ToString()
                //                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            
            
            
            DataTable dt = new DataTable();

            // 선급자재 예산내역
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_33B4S269",
                fsJASANNUM.ToString(),
                fsPONUM.ToString(),
                fsRRNUM.ToString(),
                fsVEND.ToString(),
                fsITEMCODE.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_MR_32J7S131.SetValue(dt);


            double dFXYSAMOUNT = 0;
            string sFXYSAMOUNT = string.Empty;

            for (int j = 0; j < this.FPS91_TY_S_MR_32J7S131.ActiveSheet.RowCount; j++)
            {
                dFXYSAMOUNT = dFXYSAMOUNT + double.Parse(Get_Numeric(this.FPS91_TY_S_MR_32J7S131.GetValue(j, "FXYSAMOUNT").ToString()));
            }

            sFXYSAMOUNT = string.Format("{0:#,###}", dFXYSAMOUNT);



            // 선급자재 마스터 자산금액 업데이트
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_35O4X738",
                Get_Numeric(sFXYSAMOUNT.ToString()),
                fsJASANNUM.ToString().Substring(0, 1),
                fsJASANNUM.ToString().Substring(1, 1),
                fsJASANNUM.ToString().Substring(2, 6),
                fsJASANNUM.ToString().Substring(8, 4),
                this.CBH01_FXDDPMK.GetValue().ToString(),
                fsPONUM.ToString(),
                fsRRNUM.ToString(),
                fsVEND.ToString(),
                fsITEMCODE.ToString()
                );

            this.DbConnector.ExecuteNonQueryList();

            // 선급자재 마스터 조회
            UP_SEL_ACFIXADMF(fsJASANNUM);

            this.ShowMessage("TY_M_MR_35O2G735"); // 취소완료 메세지

        }
        #endregion



        #region Description : 예산별 처리 버튼
        private void BTN61_BATCH_JASAN_Click(object sender, EventArgs e)
        {
            double dFXSAMOUNT = 0;
            double dFXSAMOUNT_NU = 0;
            double dFXTAMOUNT = 0;

            int iFXSQTY = 0;
            int i = 0;
            int j = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 내역 하위 삭제
            this.DbConnector.CommandClear();
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 합산 구분 필드 추가
                this.DbConnector.Attach("TY_P_MR_BC2A9864", ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(0, 1),
                                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(1, 1),
                                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(2, 6),
                                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(8, 4),
                                                            "1",
                                                            ds.Tables[0].Rows[i]["FXSCDAC"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSYCNUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSJASAN"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSYSSEQ"].ToString()
                                                            );

                #region Description : 원본
                // 내역하위 업데이트
                //if (ds.Tables[0].Rows[i]["FXSCRATEGB"].ToString().ToUpper() == "Y")
                //{
                //    dFXSAMOUNT = double.Parse(ds.Tables[0].Rows[i]["FXSAMOUNT"].ToString());
                //    iFXSQTY = int.Parse(string.Format("{0,9:N0}", UP_DotDelete(ds.Tables[0].Rows[i]["FXSQTY"].ToString())));

                //    dFXSAMOUNT_NU = dFXSAMOUNT / iFXSQTY;
                //    dFXSAMOUNT_NU = double.Parse(string.Format("{0,9:N0}", UP_DotDelete(Convert.ToString(dFXSAMOUNT_NU))));

                //    for (j = 1; j <= iFXSQTY; j++)
                //    {
                //        this.DbConnector.Attach("TY_P_MR_37178894", ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(0, 1),
                //                                                    ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(1, 1),
                //                                                    ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(2, 6),
                //                                                    ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(8, 4),
                //                                                    Convert.ToString(j),
                //                                                    ds.Tables[0].Rows[i]["FXSCDAC"].ToString(),
                //                                                    ds.Tables[0].Rows[i]["FXSYCNUM"].ToString(),
                //                                                    ds.Tables[0].Rows[i]["FXSJASAN"].ToString()
                //                                                    );
                        
                //    }
                //}
                //else
                //{
                //    this.DbConnector.Attach("TY_P_MR_37178894", ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(0, 1),
                //                                                ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(1, 1),
                //                                                ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(2, 6),
                //                                                ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(8, 4),
                //                                                "1",
                //                                                ds.Tables[0].Rows[i]["FXSCDAC"].ToString(),
                //                                                ds.Tables[0].Rows[i]["FXSYCNUM"].ToString(),
                //                                                ds.Tables[0].Rows[i]["FXSJASAN"].ToString()
                //                                                );
                //}
                #endregion
            }

            this.DbConnector.ExecuteTranQueryList();



            // 내역 업데이트 - 수정
            this.DbConnector.CommandClear();
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 합산 구분 필드 추가
                this.DbConnector.Attach("TY_P_MR_BC29J860", ds.Tables[0].Rows[i]["FXSAMOUNT"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSTASKAMT"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSNAME"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSSTAND"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSUNIT"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSQTY"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSBUSEO"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSSITE"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSBUYCOM"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSCLASS"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSCRATEGB"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSFIXNUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSVATGB"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSCGVEND"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(0, 1),
                                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(1, 1),
                                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(2, 6),
                                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(8, 4),
                                                            ds.Tables[0].Rows[i]["FXSCDAC"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSYCNUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSJASAN"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSYSSEQ"].ToString()
                                                            );

                // 원본
                //this.DbConnector.Attach("TY_P_MR_32Q34203", ds.Tables[0].Rows[i]["FXSAMOUNT"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXSNAME"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXSSTAND"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXSUNIT"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXSQTY"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXSBUSEO"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXSSITE"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXSBUYCOM"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXSCLASS"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXSCRATEGB"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXSFIXNUM"].ToString(),
                //                                            TYUserInfo.EmpNo,
                //                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(0, 1),
                //                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(1, 1),
                //                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(2, 6),
                //                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(8, 4),
                //                                            ds.Tables[0].Rows[i]["FXSCDAC"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXSYCNUM"].ToString(),
                //                                            ds.Tables[0].Rows[i]["FXSJASAN"].ToString()
                //                                            );

                // 합산 구분 필드 추가  
                this.DbConnector.Attach("TY_P_MR_BC29L861", ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(0, 1),
                                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(1, 1),
                                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(2, 6),
                                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(8, 4),
                                                            "1",
                                                            ds.Tables[0].Rows[i]["FXSCDAC"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSYCNUM"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSJASAN"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSYSSEQ"].ToString(),
                                                            "1",
                                                            ds.Tables[0].Rows[i]["FXSAMOUNT"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSTASKAMT"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSBUYCOM"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXSCOMBINE"].ToString(),
                                                            TYUserInfo.EmpNo);

                #region Description : 원본
                // 내역하위 업데이트
                //if (ds.Tables[0].Rows[i]["FXSCRATEGB"].ToString().ToUpper() == "Y")
                //{   
                //    dFXSAMOUNT = double.Parse(ds.Tables[0].Rows[i]["FXSAMOUNT"].ToString());
                //    iFXSQTY = int.Parse(string.Format("{0,9:N0}", UP_DotDelete(ds.Tables[0].Rows[i]["FXSQTY"].ToString())));

                //    dFXSAMOUNT_NU = dFXSAMOUNT / iFXSQTY;
                //    dFXSAMOUNT_NU = double.Parse(string.Format("{0,9:N0}", UP_DotDelete(Convert.ToString(dFXSAMOUNT_NU))));

                //    for (j = 1; j <= iFXSQTY; j++)
                //    {
                //        if (j == iFXSQTY)
                //        {
                //            dFXTAMOUNT = dFXSAMOUNT - (dFXSAMOUNT_NU * (j - 1));
                //        }
                //        else
                //        {
                //            dFXTAMOUNT = dFXSAMOUNT_NU;
                //        }

                //        this.DbConnector.Attach("TY_P_MR_32QAP197", ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(0, 1),
                //                                                    ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(1, 1),
                //                                                    ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(2, 6),
                //                                                    ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(8, 4),
                //                                                    Convert.ToString(j),
                //                                                    ds.Tables[0].Rows[i]["FXSCDAC"].ToString(),
                //                                                    ds.Tables[0].Rows[i]["FXSYCNUM"].ToString(),
                //                                                    ds.Tables[0].Rows[i]["FXSJASAN"].ToString(),
                //                                                    "1",
                //                                                    Convert.ToString(dFXTAMOUNT),
                //                                                    ds.Tables[0].Rows[i]["FXSBUYCOM"].ToString(),
                //                                                    TYUserInfo.EmpNo);
                //    }
                //}
                //else
                //{
                //    this.DbConnector.Attach("TY_P_MR_32QAP197", ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(0, 1),
                //                                                ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(1, 1),
                //                                                ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(2, 6),
                //                                                ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(8, 4),
                //                                                "1",
                //                                                ds.Tables[0].Rows[i]["FXSCDAC"].ToString(),
                //                                                ds.Tables[0].Rows[i]["FXSYCNUM"].ToString(),
                //                                                ds.Tables[0].Rows[i]["FXSJASAN"].ToString(),
                //                                                "1",
                //                                                ds.Tables[0].Rows[i]["FXSAMOUNT"].ToString(),
                //                                                ds.Tables[0].Rows[i]["FXSBUYCOM"].ToString(),
                //                                                TYUserInfo.EmpNo);
                //}
                #endregion
            }

            //// 내역 삭제
            //for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            //{
            //    // 선급자재 내역
            //    this.DbConnector.Attach("TY_P_MR_32L6G153", ds.Tables[1].Rows[i]["FXSNUM"].ToString().Substring(0, 1),
            //                                                ds.Tables[1].Rows[i]["FXSNUM"].ToString().Substring(1, 1),
            //                                                ds.Tables[1].Rows[i]["FXSNUM"].ToString().Substring(2, 6),
            //                                                ds.Tables[1].Rows[i]["FXSNUM"].ToString().Substring(8, 4));

            //    // 선급자재 하위 내역
            //    this.DbConnector.Attach("TY_P_MR_32L6G154", ds.Tables[1].Rows[i]["FXSNUM"].ToString().Substring(0, 1),
            //                                                ds.Tables[1].Rows[i]["FXSNUM"].ToString().Substring(1, 1),
            //                                                ds.Tables[1].Rows[i]["FXSNUM"].ToString().Substring(2, 6),
            //                                                ds.Tables[1].Rows[i]["FXSNUM"].ToString().Substring(8, 4));
            //}

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_MR_33D1Z300");
        }
        #endregion

        #region Description : 예산별 취소 버튼
        private void BTN62_SUNGUB_CANCEL_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 내역 삭제
            this.DbConnector.CommandClear();
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 선급자재 내역
                this.DbConnector.Attach("TY_P_MR_32L6G153", ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(0, 1),
                                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(1, 1),
                                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(2, 6),
                                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(8, 4));

                // 선급자재 하위 내역
                this.DbConnector.Attach("TY_P_MR_32L6G154", ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(0, 1),
                                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(1, 1),
                                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(2, 6),
                                                            ds.Tables[0].Rows[i]["FXSNUM"].ToString().Substring(8, 4));
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_MR_35O2G735");


            DataTable dt = new DataTable();

            // 선급자재 내역
            this.DbConnector.Attach
                (
                "TY_P_MR_32J7A127",
                TXT01_FXDSAUP.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_32J7M130.SetValue(dt);
        }
        #endregion



        #region Description : 자산생성 버튼
        private void BTN61_JASAN_CRE_Click(object sender, EventArgs e)
        {
            //string sOUTMSG = string.Empty;

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_MR_32Q3H205", this.TXT01_FXDSAUP.GetValue().ToString(),
            //                                            this.CBH01_FXDDPMK.GetValue().ToString(),
            //                                            this.TXT01_FXDGETDATE.GetValue().ToString(),
            //                                            "A",
            //                                            TYUserInfo.EmpNo,
            //                                            sOUTMSG.ToString()
            //                                            );

            //sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            //if (sOUTMSG.Substring(0, 2) != "OK")
            //{
            //    return;
            //}

            //DataTable dt = new DataTable();

            //// 선급자재 내역
            //this.DbConnector.Attach
            //    (
            //    "TY_P_MR_32J7A127",
            //    TXT01_FXDSAUP.GetValue().ToString()
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //this.FPS91_TY_S_MR_32J7M130.SetValue(dt);

            //for (int i = 0; i < this.FPS91_TY_S_MR_32J7M130.ActiveSheet.RowCount; i++)
            //{
            //    if (this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSJPNO").ToString() == "")
            //    {
            //        this.FPS91_TY_S_MR_32J7M130_Sheet1.Cells[i, 17].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            //    }
            //}

            ////this.BTN61_INQ_Click(null, null);
            //this.ShowMessage("TY_M_GB_26E30875");

            //// 버튼

            //this.BTN61_BATCH_JASAN.Visible   = false;
            //this.BTN62_SUNGUB_CANCEL.Visible = false;

            //this.BTN61_JASAN_CRE.Visible = false;
            //this.BTN61_JASAN_DEL.Visible = true;

            //this.BTN61_JPNO_CRE.Visible = true;
            //this.BTN61_JPNO_DEL.Visible = false;
        }
        #endregion

        #region Description : 자산삭제 버튼
        private void BTN61_JASAN_DEL_Click(object sender, EventArgs e)
        {
            //string sOUTMSG = string.Empty;

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_MR_32Q3H205", this.TXT01_FXDSAUP.GetValue().ToString(),
            //                                            this.CBH01_FXDDPMK.GetValue().ToString(),
            //                                            this.TXT01_FXDGETDATE.GetValue().ToString(),
            //                                            "D",
            //                                            TYUserInfo.EmpNo,
            //                                            sOUTMSG.ToString()
            //                                            );

            //sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            //if (sOUTMSG.Substring(0, 2) != "OK")
            //{
            //    return;
            //}

            //DataTable dt = new DataTable();

            //// 선급자재 내역
            //this.DbConnector.Attach
            //    (
            //    "TY_P_MR_32J7A127",
            //    TXT01_FXDSAUP.GetValue().ToString()
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //this.FPS91_TY_S_MR_32J7M130.SetValue(dt);

            //for (int i = 0; i < this.FPS91_TY_S_MR_32J7M130.ActiveSheet.RowCount; i++)
            //{
            //    if (this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSJPNO").ToString() == "")
            //    {
            //        this.FPS91_TY_S_MR_32J7M130_Sheet1.Cells[i, 17].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
            //    }
            //}

            ////this.BTN61_INQ_Click(null, null);
            //this.ShowMessage("TY_M_AC_2CDB1167");

            //// 버튼
            //this.BTN61_BATCH_JASAN.Visible   = true;
            //this.BTN62_SUNGUB_CANCEL.Visible = true;

            //this.BTN61_JASAN_CRE.Visible = true;
            //this.BTN61_JASAN_DEL.Visible = false;

            //this.BTN61_JPNO_CRE.Visible = false;
            //this.BTN61_JPNO_DEL.Visible = false;
        }
        #endregion


        #region Description : 전표생성 버튼
        private void BTN61_JPNO_CRE_Click(object sender, EventArgs e)
        {

            #region Description : 21.11.24일 수정 전 원본 소스

            /*

            #region Description : 자산 생성

            string sOUTMSG10 = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_32Q3H205", this.TXT01_FXDSAUP.GetValue().ToString(),
                                                        this.CBH01_FXDDPMK.GetValue().ToString(),
                                                        this.TXT01_FXDGETDATE.GetValue().ToString(),
                                                        "A",
                                                        TYUserInfo.EmpNo,
                                                        sOUTMSG10.ToString()
                                                        );

            sOUTMSG10 = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG10.Substring(0, 2) != "OK")
            {
                return;
            }

            #endregion

            bool bJunPyoFlag = false;

            string sCHUNGGB = string.Empty;
            string sB2SSID = string.Empty;
            string sW2RKAC = string.Empty;
            string sW2DPMK = string.Empty;
            string sMISUGB = string.Empty;
            string sJpno = string.Empty;

            int iCnt = 0;

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            System.Collections.Generic.List<object[]> dataFixU = new System.Collections.Generic.List<object[]>();

            DataTable dt = new DataTable();
            DataTable dtRkac = new DataTable();

            //청구 구분 구하기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_348CP461", this.TXT01_FXDSAUP.GetValue().ToString().Trim());  //ACFIXADMF
            DataTable dt_cggubn = this.DbConnector.ExecuteDataTable();
            if (dt_cggubn.Rows.Count != 0)
            { sCHUNGGB = dt_cggubn.Rows[0]["FXDCHGUBUN"].ToString().Trim(); }


            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            //작성사번 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_GB_4CVJ7024", this.TXT01_FXDGETDATE.GetValue().ToString(), Employer.EmpNo.ToString().Trim());  //INKIBNMF
            DataTable dt_sabun = this.DbConnector.ExecuteDataTable();
            if (dt_sabun.Rows.Count != 0)
            { sW2DPMK = dt_sabun.Rows[0]["KBBUSEO"].ToString().Trim(); }

            #region Description : 청구 없음

            if (sCHUNGGB == "3")
            {
                dataFixU.Clear();

                #region Description : 청구 없음

                //차변
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_MR_34422436", this.TXT01_FXDSAUP.GetValue().ToString());
                DataTable dcr = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dcr.Rows.Count; i++)
                {
                    #region Description : 차변

                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(dcr.Rows[i]["FXSCDAC"].ToString());
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); //  귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", dcr.Rows[i]["FXSCDAC"].ToString(), "");
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

                    // 관리항목 1 == 08 관리번호(고정자산번호)
                    // 관리항목 2 == 17 자동차번호(중기,차량)일때  
                    // 관리항목 4 == 35 예산세목 (년도(4)+부서(6)+순번(03)

                    string sVLMI4 = "";
                    sVLMI4 = this.TXT01_FXDGETDATE.GetValue().ToString().Trim().Substring(0, 4) + this.CBH01_FXDDPMK.GetValue().ToString().Trim() + dcr.Rows[i]["FXSYCNUM"].ToString();

                    if (dcr.Rows[i]["FXSCDAC"].ToString().Trim().Substring(0, 1) == "4")
                    {
                        this.DAT02_W2VLMI1.SetValue("");
                    }
                    else
                    {
                        this.DAT02_W2VLMI1.SetValue(dcr.Rows[i]["FXSFIXNUM"].ToString());
                    }

                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue(sVLMI4);
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    //sW2RKAC = "선급자재 대체(고정자산)";

                    this.DAT02_W2AMDR.SetValue(dcr.Rows[i]["FXSAMOUNT"].ToString());// 차변
                    this.DAT02_W2AMCR.SetValue("0"); // 대변

                    sW2RKAC = ""; // 2014.06.03 적요 발주명 에서 --> 자산명으로 수정(윤홍준)
                    sW2RKAC = dcr.Rows[i]["FXSNAME"].ToString(); 

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

                    #region Description : ACFIXADSF 전표번호 저장

                    dataFixU.Add(new object[] { dcr.Rows[i]["FXSSAUP"].ToString(),                   
                                                dcr.Rows[i]["FXSGUBN"].ToString(),
                                                dcr.Rows[i]["FXSYYMM"].ToString(),
                                                dcr.Rows[i]["FXSSEQ"].ToString(),
                                                dcr.Rows[i]["FXSCDAC"].ToString(),
                                                dcr.Rows[i]["FXSYCNUM"].ToString(),
                                                dcr.Rows[i]["FXTJASAN"].ToString(),
                                                iCnt.ToString()
                             });

                    #endregion
                }

                //대변
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_MR_34423437", this.TXT01_FXDSAUP.GetValue().ToString());
                DataTable dj = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dj.Rows.Count; i++)
                {
                    #region Description : 대변

                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 취득일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(dj.Rows[i]["B2CDAC"].ToString());
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(dj.Rows[i]["FXDDPMK"].ToString()); // 귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", dj.Rows[i]["B2CDAC"].ToString(), "");
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


                    // 관리항목 1 == 01 거래처 
                    this.DAT02_W2VLMI1.SetValue(dj.Rows[i]["FXDGETVEND"].ToString());
                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    //sW2RKAC = "선급자재 대체(고정자산)";


                    this.DAT02_W2AMDR.SetValue("0");

                    // 21.02.04 수정 후 소스
                    this.DAT02_W2AMCR.SetValue(dj.Rows[i]["B2AMDR"].ToString());

                    // 21.02.04 수정 전 소스
                    //this.DAT02_W2AMCR.SetValue(dj.Rows[i]["FXDRRAMOUNT"].ToString());

                    this.DAT02_W2CDFD.SetValue("");
                    this.DAT02_W2AMFD.SetValue("0");

                    sW2RKAC = ""; // 2014.06.03 적요 발주명 에서 --> 입고의 적요 수정(윤홍준)
                    sW2RKAC = dj.Rows[i]["B2RKAC"].ToString(); 

                    this.DAT02_W2RKAC.SetValue(sW2RKAC);
                    this.DAT02_W2RKCU.SetValue(dj.Rows[i]["GETVENDNM"].ToString());
                    this.DAT02_W2WCJP.SetValue(dj.Rows[i]["BANJP"].ToString()); // 원전천표 번호(반제 정리)
                    this.DAT02_W2PRGB.SetValue("");
                    this.DAT02_W2HIGB.SetValue("A");
                    this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
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

                if (datas.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    foreach (object[] data in datas)
                    {
                        this.DbConnector.Attach("TY_P_AC_29DA5966", data);
                    }
                }


                //미승인 SP호출 파일 입력
                this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, Employer.EmpNo, "A",
                                                            sW2DPMK, this.TXT01_FXDGETDATE.GetValue().ToString(), "", "",
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

                    this.ShowMessage("TY_M_AC_25O8K620");

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_29D5B004", sB2SSID);
                    DataTable dtresult = this.DbConnector.ExecuteDataTable();
                    if (dtresult.Rows.Count > 0)
                    {
                        if (dtresult.Rows[0]["AGRESULTCODE"].ToString().Trim() == "OK")
                        {
                            //전표번호 받아오기
                            sJpno = dtresult.Rows[0]["AGOUT_PARAM"].ToString();
                        }
                    }
                }

                if (bJunPyoFlag)
                {
                    //ACFIXADSF 전표번호 UPDATE
                    if (dataFixU.Count > 0)
                    {
                        this.DbConnector.CommandClear();
                        foreach (object[] data in dataFixU)
                        {
                            this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", "") + Set_Fill2(data[7].ToString()),
                                                                        data[0].ToString(),
                                                                        data[1].ToString(),
                                                                        data[2].ToString(),
                                                                        data[3].ToString(),
                                                                        data[4].ToString(),
                                                                        data[5].ToString(),
                                                                        data[6].ToString()                                                                        
                                                                        );
                        }
                        this.DbConnector.ExecuteTranQueryList();

                        //this.DbConnector.CommandClear();
                        //this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", ""), this.TXT01_FXDSAUP.GetValue().ToString());
                        //this.DbConnector.ExecuteTranQueryList();
                    }
                }

                #endregion
            }

            #endregion

            #region Description : 매출청구

            if (sCHUNGGB == "2")
            {
                dataFixU.Clear();

                #region Description : 수선비(화주사비용) 및 구축물(회사비용)

                //차변
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_MR_34422436", this.TXT01_FXDSAUP.GetValue().ToString());
                DataTable dcr = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dcr.Rows.Count; i++)
                {
                    #region Description : 차변

                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(dcr.Rows[i]["FXSCDAC"].ToString());
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); //  귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", dcr.Rows[i]["FXSCDAC"].ToString(), "");
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

                    // 관리항목 1 == 08 관리번호(고정자산번호)
                    // 관리항목 2 == 17 자동차번호(중기,차량)일때  
                    // 관리항목 4 == 35 예산세목 (년도(4)+부서(6)+순번(03)

                    string sVLMI4 = "";
                    sVLMI4 = this.TXT01_FXDGETDATE.GetValue().ToString().Trim().Substring(0, 4) + this.CBH01_FXDDPMK.GetValue().ToString().Trim() + dcr.Rows[i]["FXSYCNUM"].ToString();

                    if (dcr.Rows[i]["FXSCDAC"].ToString().Trim().Substring(0, 1) == "4")
                    {
                        this.DAT02_W2VLMI1.SetValue("");
                    }
                    else
                    {
                        this.DAT02_W2VLMI1.SetValue(dcr.Rows[i]["FXSFIXNUM"].ToString());
                    }

                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue(sVLMI4);
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    //sW2RKAC = "선급자재 대체(고정자산)";

                    this.DAT02_W2AMDR.SetValue(dcr.Rows[i]["FXSAMOUNT"].ToString());// 차변
                    this.DAT02_W2AMCR.SetValue("0"); // 대변

                    sW2RKAC = ""; // 2014.06.03 적요 발주명 에서 --> 자산명으로 수정(윤홍준)
                    sW2RKAC = dcr.Rows[i]["FXSNAME"].ToString(); 

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

                    #region Description : ACFIXADSF 전표번호 저장

                    dataFixU.Add(new object[] { dcr.Rows[i]["FXSSAUP"].ToString(),                   
                                                dcr.Rows[i]["FXSGUBN"].ToString(),
                                                dcr.Rows[i]["FXSYYMM"].ToString(),
                                                dcr.Rows[i]["FXSSEQ"].ToString(),
                                                dcr.Rows[i]["FXSCDAC"].ToString(),
                                                dcr.Rows[i]["FXSYCNUM"].ToString(),
                                                dcr.Rows[i]["FXTJASAN"].ToString(),
                                                iCnt.ToString()
                             });

                    #endregion
                }

                //대변
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_MR_34423437", this.TXT01_FXDSAUP.GetValue().ToString());
                DataTable dj = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dj.Rows.Count; i++)
                {
                    #region Description : 대변

                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 취득일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(dj.Rows[i]["B2CDAC"].ToString());
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(dj.Rows[i]["FXDDPMK"].ToString()); // 귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", dj.Rows[i]["B2CDAC"].ToString(), "");
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


                    // 관리항목 1 == 01 거래처 
                    this.DAT02_W2VLMI1.SetValue(dj.Rows[i]["FXDGETVEND"].ToString());
                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    //sW2RKAC = "선급자재 대체(고정자산)";


                    this.DAT02_W2AMDR.SetValue("0");

                    // 21.02.04 수정 후 소스
                    this.DAT02_W2AMCR.SetValue(dj.Rows[i]["B2AMDR"].ToString());

                    // 21.02.04 수정 전 소스
                    //this.DAT02_W2AMCR.SetValue(dj.Rows[i]["FXDPOAMOUNT"].ToString());

                    this.DAT02_W2CDFD.SetValue("");
                    this.DAT02_W2AMFD.SetValue("0");

                    sW2RKAC = ""; // 2014.06.03 적요 발주명 에서 --> 입고의 적요 수정(윤홍준)
                    sW2RKAC = dj.Rows[i]["B2RKAC"].ToString(); 

                    this.DAT02_W2RKAC.SetValue(sW2RKAC);
                    this.DAT02_W2RKCU.SetValue(dj.Rows[i]["GETVENDNM"].ToString());
                    this.DAT02_W2WCJP.SetValue(dj.Rows[i]["BANJP"].ToString()); // 원전천표 번호(반제 정리)
                    this.DAT02_W2PRGB.SetValue("");
                    this.DAT02_W2HIGB.SetValue("A");
                    this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
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

                if (datas.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    foreach (object[] data in datas)
                    {
                        this.DbConnector.Attach("TY_P_AC_29DA5966", data);
                    }
                }

                //미승인 SP호출 파일 입력
                this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, Employer.EmpNo, "A",
                                                            sW2DPMK, this.TXT01_FXDGETDATE.GetValue().ToString(), "", "",
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

                    this.ShowMessage("TY_M_AC_25O8K620");

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_29D5B004", sB2SSID);
                    DataTable dtresult = this.DbConnector.ExecuteDataTable();
                    if (dtresult.Rows.Count > 0)
                    {
                        if (dtresult.Rows[0]["AGRESULTCODE"].ToString().Trim() == "OK")
                        {
                            //전표번호 받아오기
                            sJpno = dtresult.Rows[0]["AGOUT_PARAM"].ToString();
                        }
                    }
                }

                if (bJunPyoFlag)
                {
                    //ACFIXADSF 전표번호 UPDATE

                    if (dataFixU.Count > 0)
                    {
                        this.DbConnector.CommandClear();
                        foreach (object[] data in dataFixU)
                        {
                            this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", "") + Set_Fill2(data[7].ToString()),
                                                                        data[0].ToString(),
                                                                        data[1].ToString(),
                                                                        data[2].ToString(),
                                                                        data[3].ToString(),
                                                                        data[4].ToString(),
                                                                        data[5].ToString(),
                                                                        data[6].ToString()
                                                                        );
                        }
                        this.DbConnector.ExecuteTranQueryList();

                        //this.DbConnector.CommandClear();
                        //this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", ""), this.TXT01_FXDSAUP.GetValue().ToString());
                        //this.DbConnector.ExecuteTranQueryList();
                    }
                }

                #endregion
            }

            #endregion

            #region Description : 미수금청구

            if (sCHUNGGB == "1")
            {
                dataFixU.Clear();

                //선급자재 미수금처리 구분 확인 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_MR_345AC446", this.TXT01_FXDSAUP.GetValue().ToString(), this.TXT01_FXDSAUP.GetValue().ToString());  //ACFIXADSF
                DataTable dt_prgubn = this.DbConnector.ExecuteDataTable();
                if (dt_sabun.Rows.Count != 0)
                {
                    if (Convert.ToDouble(dt_prgubn.Rows[0]["ACNT"].ToString().Trim()) > 0 && Convert.ToDouble(dt_prgubn.Rows[0]["BCNT"].ToString().Trim()) > 0)
                    {
                        sMISUGB = "B"; // 화주사 및 당사 처리
                    }
                    else
                    {
                        sMISUGB = "A"; // 화주사 수선비용 100%
                    }
                }

                if (sMISUGB == "A")
                {
                    #region Description : 화주사 수선비용 100%

                    string sCRAMT = string.Empty;
                    string sCRVAT = string.Empty;
                    string sCRTOT = string.Empty;
                    string sCGVEND = string.Empty;
                    string sCGVENDNM = string.Empty;
                    string sVATGB = string.Empty;

                    //미수금 합계 구하기
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_MR_34428438", this.TXT01_FXDSAUP.GetValue().ToString());
                    DataTable dcramt = this.DbConnector.ExecuteDataTable();
                    if (dcramt.Rows.Count > 0)
                    {
                        sCRAMT = dcramt.Rows[0]["FXSAMOUNT"].ToString().Trim();
                        sCRVAT = dcramt.Rows[0]["VAT71"].ToString().Trim();
                        sCRTOT = Convert.ToString(Convert.ToDouble(dcramt.Rows[0]["FXSAMOUNT"].ToString().Trim()) + Convert.ToDouble(dcramt.Rows[0]["VAT71"].ToString().Trim()));
                        sCGVEND = dcramt.Rows[0]["FXSCGVEND"].ToString().Trim();
                        sCGVENDNM = dcramt.Rows[0]["CGVENDNM"].ToString().Trim();
                        sVATGB = dcramt.Rows[0]["FXSVATGB"].ToString().Trim();
                    }

                    //차변(미수금)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_MR_34531453", this.TXT01_FXDSAUP.GetValue().ToString());
                    DataTable dcr = this.DbConnector.ExecuteDataTable();

                    for (int i = 0; i < dcr.Rows.Count; i++)
                    {
                        #region Description : 차변
                        iCnt = iCnt + 1;

                        dt.Clear();

                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                        this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 일자
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue("3");
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue(dcr.Rows[i]["FXSCDAC"].ToString()); // 기타 미수금
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); //  귀속부서

                        //관리항목 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach(dcr.Rows[i]["FXSCDAC"].ToString());
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

                        // 관리항목 1 == 01 거래처

                        this.DAT02_W2VLMI1.SetValue(dcr.Rows[i]["FXSCGVEND"].ToString()); // FXSCGVEND
                        this.DAT02_W2VLMI2.SetValue("");
                        this.DAT02_W2VLMI3.SetValue("");
                        this.DAT02_W2VLMI4.SetValue("");
                        this.DAT02_W2VLMI5.SetValue("");
                        this.DAT02_W2VLMI6.SetValue("");

                        //sW2RKAC = "선급자재 대체(고정자산)";

                        this.DAT02_W2AMDR.SetValue(sCRTOT);// 차변
                        this.DAT02_W2AMCR.SetValue("0");   // 대변

                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");

                        sW2RKAC = ""; // 2014.06.03 적요 발주명 에서 --> 자산명으로 수정(윤홍준)
                        sW2RKAC = dcr.Rows[i]["FXSNAME"].ToString(); 

                        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                        this.DAT02_W2RKCU.SetValue(dcr.Rows[i]["FXSYCNUM"].ToString());
                        this.DAT02_W2WCJP.SetValue("");
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
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

                        #region Description : ACFIXADSF 전표번호 저장

                        dataFixU.Add(new object[] { dcr.Rows[i]["FXSSAUP"].ToString(),                   
                                                dcr.Rows[i]["FXSGUBN"].ToString(),
                                                dcr.Rows[i]["FXSYYMM"].ToString(),
                                                dcr.Rows[i]["FXSSEQ"].ToString(),
                                                dcr.Rows[i]["FXSCDAC"].ToString(),
                                                dcr.Rows[i]["FXSYCNUM"].ToString(),
                                                dcr.Rows[i]["FXSJASAN"].ToString(),
                                                iCnt.ToString()
                             });

                        #endregion
                    }

                    //대변 (선급자재)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_MR_34423437", this.TXT01_FXDSAUP.GetValue().ToString());
                    DataTable dj = this.DbConnector.ExecuteDataTable();

                    for (int i = 0; i < dj.Rows.Count; i++)
                    {
                        #region Description : 대변
                        iCnt = iCnt + 1;

                        dt.Clear();

                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                        this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 취득일자
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue("3");
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue(dj.Rows[i]["B2CDAC"].ToString());
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(dj.Rows[i]["FXDDPMK"].ToString()); // 귀속부서

                        //관리항목 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_23N3M888", dj.Rows[i]["B2CDAC"].ToString(), "");
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

                        //거래처 코드
                        this.DAT02_W2VLMI1.SetValue(dj.Rows[i]["FXDGETVEND"].ToString());
                        this.DAT02_W2VLMI2.SetValue("");
                        this.DAT02_W2VLMI3.SetValue("");
                        this.DAT02_W2VLMI4.SetValue("");
                        this.DAT02_W2VLMI5.SetValue("");
                        this.DAT02_W2VLMI6.SetValue("");

                        //sW2RKAC = "선급자재 대체(고정자산)";

                        this.DAT02_W2AMDR.SetValue("0");

                        // 21.02.04 수정 후 소스
                        this.DAT02_W2AMCR.SetValue(dj.Rows[i]["B2AMDR"].ToString());

                        // 21.02.04 수정 전 소스
                        //this.DAT02_W2AMCR.SetValue(dj.Rows[i]["FXDPOAMOUNT"].ToString());

                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");

                        sW2RKAC = ""; // 2014.06.03 적요 발주명 에서 --> 입고의 적요 수정(윤홍준)
                        sW2RKAC = dj.Rows[i]["B2RKAC"].ToString(); 

                        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                        this.DAT02_W2RKCU.SetValue(dj.Rows[i]["GETVENDNM"].ToString());
                        this.DAT02_W2WCJP.SetValue(dj.Rows[i]["BANJP"].ToString()); // 원전천표 번호(반제 정리)
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
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

                    //대변
                    #region Description : 대변 (부가세)
                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue("21103101"); // 매출부가세 본점
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); //  귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", "21103101", "");
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

                    // 관리항목 1 == 11 세무구분
                    // 관리항목 2 == 01 거래처
                    // 관리항목 3 == 15 거래일자
                    // 관리항목 4 == 14 공급가액

                    this.DAT02_W2VLMI1.SetValue(sVATGB);
                    this.DAT02_W2VLMI2.SetValue(sCGVEND);
                    this.DAT02_W2VLMI3.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString());
                    this.DAT02_W2VLMI4.SetValue(sCRAMT);
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    sW2RKAC = "매출 부가세"; // +sW2RKAC.ToString();

                    this.DAT02_W2AMDR.SetValue("0");// 차변
                    this.DAT02_W2AMCR.SetValue(sCRVAT); // 대변

                    this.DAT02_W2CDFD.SetValue("");
                    this.DAT02_W2AMFD.SetValue("0");
                    this.DAT02_W2RKAC.SetValue(sW2RKAC);
                    this.DAT02_W2RKCU.SetValue(sCGVENDNM);
                    this.DAT02_W2WCJP.SetValue("");
                    this.DAT02_W2PRGB.SetValue("");
                    this.DAT02_W2HIGB.SetValue("A");
                    this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
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

                    #endregion

                    if (datas.Count > 0)
                    {
                        this.DbConnector.CommandClear();
                        foreach (object[] data in datas)
                        {
                            this.DbConnector.Attach("TY_P_AC_29DA5966", data);
                        }
                    }

                    //미승인 SP호출 파일 입력
                    this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, Employer.EmpNo, "A",
                                                                sW2DPMK, this.TXT01_FXDGETDATE.GetValue().ToString(), "", "",
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

                        this.ShowMessage("TY_M_AC_25O8K620");

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_29D5B004", sB2SSID);
                        DataTable dtresult = this.DbConnector.ExecuteDataTable();
                        if (dtresult.Rows.Count > 0)
                        {
                            if (dtresult.Rows[0]["AGRESULTCODE"].ToString().Trim() == "OK")
                            {
                                //전표번호 받아오기
                                sJpno = dtresult.Rows[0]["AGOUT_PARAM"].ToString();
                            }
                        }
                    }

                    if (bJunPyoFlag)
                    {
                        //ACFIXADSF 전표번호 UPDATE

                        if (dataFixU.Count > 0)
                        {
                            this.DbConnector.CommandClear();
                            foreach (object[] data in dataFixU)
                            {
                                this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", "") + Set_Fill2(data[7].ToString()),
                                                                            data[0].ToString(),
                                                                            data[1].ToString(),
                                                                            data[2].ToString(),
                                                                            data[3].ToString(),
                                                                            data[4].ToString(),
                                                                            data[5].ToString(),
                                                                            data[6].ToString()
                                                                            );
                            }
                            this.DbConnector.ExecuteTranQueryList();

                            //this.DbConnector.CommandClear();
                            //this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", ""), this.TXT01_FXDSAUP.GetValue().ToString());
                            //this.DbConnector.ExecuteTranQueryList();
                        }
                    }
                }

                // 화주사,우리회사 수선비용 공동 부담하고 우리회사 비용을 자산처리 전표 2개 처리

                if (sMISUGB == "B")
                {
                    dataFixU.Clear();

                    #region Description : 구축물 비용 처리

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_MR_34422436", this.TXT01_FXDSAUP.GetValue().ToString());
                    DataTable ddr = this.DbConnector.ExecuteDataTable();

                    for (int i = 0; i < ddr.Rows.Count; i++)
                    {
                        #region Description : 차변

                        iCnt = iCnt + 1;

                        dt.Clear();

                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                        this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 일자
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue("3");
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue(ddr.Rows[i]["FXSCDAC"].ToString());
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); //  귀속부서

                        //관리항목 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_23N3M888", ddr.Rows[i]["FXSCDAC"].ToString(), "");
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

                        // 관리항목 1 == 08 관리번호(고정자산번호)
                        // 관리항목 2 == 17 자동차번호(중기,차량)일때  
                        // 관리항목 4 == 35 예산세목 (년도(4)+부서(6)+순번(03)

                        string sVLMI4 = "";

                        sVLMI4 = this.TXT01_FXDGETDATE.GetValue().ToString().Trim().Substring(0, 4) + this.CBH01_FXDDPMK.GetValue().ToString().Trim() + ddr.Rows[i]["FXSYCNUM"].ToString();


                        this.DAT02_W2VLMI1.SetValue(ddr.Rows[i]["FXSFIXNUM"].ToString());
                        this.DAT02_W2VLMI2.SetValue("");
                        this.DAT02_W2VLMI3.SetValue("");
                        this.DAT02_W2VLMI4.SetValue(sVLMI4);
                        this.DAT02_W2VLMI5.SetValue("");
                        this.DAT02_W2VLMI6.SetValue("");

                        //sW2RKAC = "선급자재 대체(고정자산)";

                        this.DAT02_W2AMDR.SetValue(ddr.Rows[i]["FXSAMOUNT"].ToString());// 차변
                        this.DAT02_W2AMCR.SetValue("0"); // 대변

                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");

                        sW2RKAC = ""; // 2014.06.03 적요 발주명 에서 --> 자산명 수정(윤홍준)
                        sW2RKAC = ddr.Rows[i]["FXSNAME"].ToString(); 

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

                        #region Description : ACFIXADSF 전표번호 저장

                        dataFixU.Add(new object[] { ddr.Rows[i]["FXSSAUP"].ToString(),                   
                                                ddr.Rows[i]["FXSGUBN"].ToString(),
                                                ddr.Rows[i]["FXSYYMM"].ToString(),
                                                ddr.Rows[i]["FXSSEQ"].ToString(),
                                                ddr.Rows[i]["FXSCDAC"].ToString(),
                                                ddr.Rows[i]["FXSYCNUM"].ToString(),
                                                ddr.Rows[i]["FXTJASAN"].ToString(),
                                                iCnt.ToString()
                             });

                        #endregion
                    }

                    //대변
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_MR_345AZ447", this.TXT01_FXDSAUP.GetValue().ToString(), this.TXT01_FXDSAUP.GetValue().ToString());
                    DataTable dcr = this.DbConnector.ExecuteDataTable();

                    for (int i = 0; i < dcr.Rows.Count; i++)
                    {
                        #region Description : 대변

                        // 20181116 황성환 요청
                        // 20190101 이전부터 진행중인 공사건에 대해선
                        // 20190101일부터 선급자재(11101001)을 건설중인자산(12210000)으로 전표가 끊어져야 함

                        string sCDAC = string.Empty;

                        sCDAC = "11101001";

                        if (int.Parse(Get_Numeric(this.TXT01_FXDGETDATE.GetValue().ToString())) >= 20190101)
                        {
                            sCDAC = "12210000";
                        }

                        iCnt = iCnt + 1;

                        dt.Clear();

                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                        this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 일자
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue("3");
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue(sCDAC.ToString());// 계정과목 (선급자재)
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); //  귀속부서

                        //관리항목 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_23N3M888", sCDAC.ToString(), "");
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

                        // 관리항목 1 == 01 거래처


                        this.DAT02_W2VLMI1.SetValue(dcr.Rows[i]["FXDGETVEND"].ToString());
                        this.DAT02_W2VLMI2.SetValue("");
                        this.DAT02_W2VLMI3.SetValue("");
                        this.DAT02_W2VLMI4.SetValue("");
                        this.DAT02_W2VLMI5.SetValue("");
                        this.DAT02_W2VLMI6.SetValue("");

                        //sW2RKAC = "선급자재 대체(고정자산)";

                        this.DAT02_W2AMDR.SetValue("0");// 차변
                        this.DAT02_W2AMCR.SetValue(dcr.Rows[i]["FXYSAMOUNT"].ToString()); // 대변

                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");

                        sW2RKAC = ""; // 2014.06.03 적요 발주명 에서 --> 자산명 수정(윤홍준)
                        sW2RKAC = dcr.Rows[i]["FXSNAME"].ToString(); 

                        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                        this.DAT02_W2RKCU.SetValue(dcr.Rows[i]["GETVENDNM"].ToString());
                        this.DAT02_W2WCJP.SetValue(dcr.Rows[i]["FXDREPJPNO"].ToString());
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
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


                    //미승인 SP호출 파일 입력
                    this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, Employer.EmpNo, "A",
                                                                sW2DPMK, this.TXT01_FXDGETDATE.GetValue().ToString(), "", "",
                                                                "", "", Employer.EmpNo);
                    this.DbConnector.ExecuteTranQueryList();

                    //전표 생성 함수 호출
                    bJunPyoFlag = false;
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, "");
                    string sOUTMSG1 = Convert.ToString(this.DbConnector.ExecuteScalar());
                    if (sOUTMSG1.Substring(0, 2) == "ER")
                    {
                        this.ShowCustomMessage(sOUTMSG1, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                                sJpno = dtresult.Rows[0]["AGOUT_PARAM"].ToString();
                            }
                        }
                    }

                    if (bJunPyoFlag)
                    {
                        //ACFIXADSF 전표번호 UPDATE                       

                        if (dataFixU.Count > 0)
                        {
                            this.DbConnector.CommandClear();
                            foreach (object[] data in dataFixU)
                            {
                                this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", "") + Set_Fill2(data[7].ToString()),
                                                                            data[0].ToString(),
                                                                            data[1].ToString(),
                                                                            data[2].ToString(),
                                                                            data[3].ToString(),
                                                                            data[4].ToString(),
                                                                            data[5].ToString(),
                                                                            data[6].ToString()
                                                                            );
                            }
                            this.DbConnector.ExecuteTranQueryList();

                            //this.DbConnector.CommandClear();
                            //this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", ""), this.TXT01_FXDSAUP.GetValue().ToString());
                            //this.DbConnector.ExecuteTranQueryList();
                        }
                    }
                    #endregion

                    // 전표 분리
                    //--------------------------------------------------- //
                    //   전표 2 장 발생                                    //
                    //--------------------------------------------------- //

                    #region Description : 미수금 청구

                    //BATID번호 부여
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_29C7M958");
                    decimal dAutoSeq1 = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
                    sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq1.ToString();

                    string sCRAMT = string.Empty;
                    string sCRVAT = string.Empty;
                    string sCRTOT = string.Empty;
                    string sCGVEND = string.Empty;
                    string sCGVENDNM = string.Empty;
                    string sVATGB = string.Empty;

                    //미수금 합계 구하기
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_MR_34428438", this.TXT01_FXDSAUP.GetValue().ToString());
                    DataTable dcramt = this.DbConnector.ExecuteDataTable();
                    if (dcramt.Rows.Count > 0)
                    {
                        sCRAMT = dcramt.Rows[0]["FXSAMOUNT"].ToString().Trim();
                        sCRVAT = dcramt.Rows[0]["VAT71"].ToString().Trim();
                        sCRTOT = Convert.ToString(Convert.ToDouble(dcramt.Rows[0]["FXSAMOUNT"].ToString().Trim()) + Convert.ToDouble(dcramt.Rows[0]["VAT71"].ToString().Trim()));
                        sCGVEND = dcramt.Rows[0]["FXSCGVEND"].ToString().Trim();
                        sCGVENDNM = dcramt.Rows[0]["CGVENDNM"].ToString().Trim();
                        sVATGB = dcramt.Rows[0]["FXSVATGB"].ToString().Trim();
                    }
                    //차변
                    #region Description : 차변

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_MR_35K1T693", this.TXT01_FXDSAUP.GetValue().ToString());
                    DataTable ddr_1 = this.DbConnector.ExecuteDataTable();

                    for (int i = 0; i < dcr.Rows.Count; i++)
                    {
                        iCnt = iCnt + 1;

                        dt.Clear();

                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                        this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 일자
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue("3");
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue(dcr.Rows[i]["FXSCDAC"].ToString()); // 기타 미수금
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); //  귀속부서

                        //관리항목 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach(dcr.Rows[i]["FXSCDAC"].ToString());
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

                        // 관리항목 1 == 01 거래처

                        this.DAT02_W2VLMI1.SetValue(ddr_1.Rows[i]["FXSCGVEND"].ToString()); // FXSCGVEND
                        this.DAT02_W2VLMI2.SetValue("");
                        this.DAT02_W2VLMI3.SetValue("");
                        this.DAT02_W2VLMI4.SetValue("");
                        this.DAT02_W2VLMI5.SetValue("");
                        this.DAT02_W2VLMI6.SetValue("");

                        //sW2RKAC = "선급자재 대체(고정자산)";

                        this.DAT02_W2AMDR.SetValue(sCRTOT);// 차변
                        this.DAT02_W2AMCR.SetValue("0");   // 대변

                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");
                        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                        this.DAT02_W2RKCU.SetValue(ddr_1.Rows[i]["FXSYCNUM"].ToString());
                        this.DAT02_W2WCJP.SetValue("");
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
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

                        #region Description : ACFIXADSF 전표번호 저장

                        dataFixU.Add(new object[] { ddr_1.Rows[i]["FXSSAUP"].ToString(),                   
                                                ddr_1.Rows[i]["FXSGUBN"].ToString(),
                                                ddr_1.Rows[i]["FXSYYMM"].ToString(),
                                                ddr_1.Rows[i]["FXSSEQ"].ToString(),
                                                ddr_1.Rows[i]["FXSCDAC"].ToString(),
                                                ddr_1.Rows[i]["FXSYCNUM"].ToString(),
                                                ddr_1.Rows[i]["FXSJASAN"].ToString(),
                                                iCnt.ToString()
                             });

                        #endregion
                    }
                    #endregion

                    //대변 (선급자재)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_MR_345BN448", this.TXT01_FXDSAUP.GetValue().ToString(), this.TXT01_FXDSAUP.GetValue().ToString());
                    DataTable dcr_1 = this.DbConnector.ExecuteDataTable();

                    for (int i = 0; i < dcr_1.Rows.Count; i++)
                    {
                        #region Description : 대변

                        // 20181116 황성환 요청
                        // 20190101 이전부터 진행중인 공사건에 대해선
                        // 20190101일부터 선급자재(11101001)을 건설중인자산(12210000)으로 전표가 끊어져야 함

                        string sCDAC = string.Empty;

                        sCDAC = "11101001";

                        if (int.Parse(Get_Numeric(this.TXT01_FXDGETDATE.GetValue().ToString())) >= 20190101)
                        {
                            sCDAC = "12210000";
                        }

                        iCnt = iCnt + 1;

                        dt.Clear();

                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                        this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 일자
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue("3");
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue(sCDAC.ToString());// 계정과목 (선급자재)
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); //  귀속부서

                        //관리항목 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_23N3M888", sCDAC.ToString(), "");
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

                        // 관리항목 1 == 01 거래처


                        this.DAT02_W2VLMI1.SetValue(dcr_1.Rows[i]["FXDGETVEND"].ToString());
                        this.DAT02_W2VLMI2.SetValue("");
                        this.DAT02_W2VLMI3.SetValue("");
                        this.DAT02_W2VLMI4.SetValue("");
                        this.DAT02_W2VLMI5.SetValue("");
                        this.DAT02_W2VLMI6.SetValue("");

                        //sW2RKAC = "선급자재 대체(고정자산)";

                        this.DAT02_W2AMDR.SetValue("0");// 차변
                        this.DAT02_W2AMCR.SetValue(dcr_1.Rows[i]["FXYSAMOUNT"].ToString()); // 대변

                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");
                        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                        this.DAT02_W2RKCU.SetValue(dcr_1.Rows[i]["GETVENDNM"].ToString());
                        this.DAT02_W2WCJP.SetValue(dcr_1.Rows[i]["FXDREPJPNO"].ToString()); // 원천번호(반제 정리)
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
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

                    //대변
                    #region Description : 대변 (부가세)
                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue("21103101"); // 매출부가세 본점
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); //  귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", "21103101", "");
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

                    // 관리항목 1 == 11 세무구분
                    // 관리항목 2 == 01 거래처
                    // 관리항목 3 == 15 거래일자
                    // 관리항목 4 == 14 공급가액

                    this.DAT02_W2VLMI1.SetValue(sVATGB);
                    this.DAT02_W2VLMI2.SetValue(sCGVEND);
                    this.DAT02_W2VLMI3.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString());
                    this.DAT02_W2VLMI4.SetValue(sCRAMT);
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    sW2RKAC = "매출 부가세-" + sW2RKAC.ToString();

                    this.DAT02_W2AMDR.SetValue("0");// 차변
                    this.DAT02_W2AMCR.SetValue(sCRVAT); // 대변

                    this.DAT02_W2CDFD.SetValue("");
                    this.DAT02_W2AMFD.SetValue("0");
                    this.DAT02_W2RKAC.SetValue(sW2RKAC);
                    this.DAT02_W2RKCU.SetValue(sCGVENDNM);
                    this.DAT02_W2WCJP.SetValue("");
                    this.DAT02_W2PRGB.SetValue("");
                    this.DAT02_W2HIGB.SetValue("A");
                    this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
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

                    //미승인 SP호출 파일 입력
                    this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, Employer.EmpNo, "A",
                                                                sW2DPMK, this.TXT01_FXDGETDATE.GetValue().ToString(), "", "",
                                                                "", "", Employer.EmpNo);
                    this.DbConnector.ExecuteTranQueryList();

                    //전표 생성 함수 호출
                    bJunPyoFlag = false;
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, "");
                    string sOUTMSG2 = Convert.ToString(this.DbConnector.ExecuteScalar());
                    if (sOUTMSG2.Substring(0, 2) == "ER")
                    {
                        this.ShowCustomMessage(sOUTMSG2, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                                sJpno = dtresult.Rows[0]["AGOUT_PARAM"].ToString();
                            }
                        }
                    }

                    if (bJunPyoFlag)
                    {
                        //ACFIXADSF 전표번호 UPDATE
                        if (dataFixU.Count > 0)
                        {
                            this.DbConnector.CommandClear();
                            foreach (object[] data in dataFixU)
                            {
                                this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", "") + Set_Fill2(data[7].ToString()),
                                                                            data[0].ToString(),
                                                                            data[1].ToString(),
                                                                            data[2].ToString(),
                                                                            data[3].ToString(),
                                                                            data[4].ToString(),
                                                                            data[5].ToString(),
                                                                            data[6].ToString()
                                                                            );
                            }
                            this.DbConnector.ExecuteTranQueryList();

                            //this.DbConnector.CommandClear();
                            //this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", ""), this.TXT01_FXDSAUP.GetValue().ToString());
                            //this.DbConnector.ExecuteTranQueryList();
                        }
                    }


                    #endregion

                }
            }

            #endregion

            // 선급자재 내역
            this.DbConnector.Attach
                (
                "TY_P_MR_32J7A127",
                TXT01_FXDSAUP.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_32J7M130.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_MR_32J7M130.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSJPNO").ToString() == "")
                {
                    this.FPS91_TY_S_MR_32J7M130_Sheet1.Cells[i, 17].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                }

                if (this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSFIXNUM").ToString() != "" && this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSJPNO").ToString() != "")
                {
                    this.DbConnector.CommandClear(); //ACFIXASSETSF 고정자산 상세 화일에 전표번호 세팅
                    this.DbConnector.Attach("TY_P_MR_358BG631", this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSJPNO").ToString().Replace("-", ""), this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSFIXNUM").ToString());
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

            // 버튼
            //this.BTN61_JASAN_CRE.Visible = false;
            //this.BTN61_JASAN_DEL.Visible = false;

            this.BTN61_JPNO_CRE.Visible = false;
            this.BTN61_JPNO_DEL.Visible = true;
             
            */

            #endregion


            #region Description : 21.11.24일 수정 후 소스

            

            bool bJunPyoFlag = false;

            string sCHUNGGB = string.Empty;
            string sB2SSID  = string.Empty;
            string sW2RKAC  = string.Empty;
            string sW2DPMK  = string.Empty;
            string sMISUGB  = string.Empty;
            string sJpno    = string.Empty;

            string sB2NOLN  = string.Empty;

            int iCnt = 0;

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            System.Collections.Generic.List<object[]> dataFixU = new System.Collections.Generic.List<object[]>();

            DataTable dt = new DataTable();
            DataTable dtRkac = new DataTable();

            DataTable dcr = new DataTable();

            //청구 구분 구하기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_348CP461", this.TXT01_FXDSAUP.GetValue().ToString().Trim());  //ACFIXADMF
            DataTable dt_cggubn = this.DbConnector.ExecuteDataTable();
            if (dt_cggubn.Rows.Count != 0)
            { sCHUNGGB = dt_cggubn.Rows[0]["FXDCHGUBUN"].ToString().Trim(); }

            #region Description : 자산 생성

            string sOUTMSG = string.Empty;
            string sOUTMSG1 = string.Empty;
            string sOUTMSG10 = string.Empty;

            sOUTMSG10 = "";

            // 청구구분 코드 1:매출청구, 3:청구없음 인 경우만 자산생성
            if (sCHUNGGB != "2")
            {
                this.DbConnector.CommandClear();

                // 합산 구분 필드 추가
                this.DbConnector.Attach("TY_P_MR_BC7GZ891", this.TXT01_FXDSAUP.GetValue().ToString(),
                                                            this.CBH01_FXDDPMK.GetValue().ToString(),
                                                            this.TXT01_FXDGETDATE.GetValue().ToString(),
                                                            "A",
                                                            TYUserInfo.EmpNo,
                                                            sOUTMSG10.ToString()
                                                            );

                //원본
                //this.DbConnector.Attach("TY_P_MR_32Q3H205", this.TXT01_FXDSAUP.GetValue().ToString(),
                //                                            this.CBH01_FXDDPMK.GetValue().ToString(),
                //                                            this.TXT01_FXDGETDATE.GetValue().ToString(),
                //                                            "A",
                //                                            TYUserInfo.EmpNo,
                //                                            sOUTMSG10.ToString()
                //                                            );

                sOUTMSG10 = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUTMSG10.Substring(0, 2) != "OK")
                {
                    return;
                }
            }

            #endregion

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            //작성사번 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_GB_4CVJ7024", this.TXT01_FXDGETDATE.GetValue().ToString(), Employer.EmpNo.ToString().Trim());  //INKIBNMF
            DataTable dt_sabun = this.DbConnector.ExecuteDataTable();
            if (dt_sabun.Rows.Count != 0)
            { sW2DPMK = dt_sabun.Rows[0]["KBBUSEO"].ToString().Trim(); }

            #region Description : 청구 없음

            if (sCHUNGGB == "3")
            {
                dataFixU.Clear();

                #region Description : 청구 없음

                //차변
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_MR_34422436", this.TXT01_FXDSAUP.GetValue().ToString());
                dcr = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dcr.Rows.Count; i++)
                {
                    #region Description : 차변

                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(dcr.Rows[i]["FXSCDAC"].ToString());
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); //  귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", dcr.Rows[i]["FXSCDAC"].ToString(), "");
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

                    // 관리항목 1 == 08 관리번호(고정자산번호)
                    // 관리항목 2 == 17 자동차번호(중기,차량)일때  
                    // 관리항목 4 == 35 예산세목 (년도(4)+부서(6)+순번(03)

                    string sVLMI4 = "";
                    sVLMI4 = this.TXT01_FXDGETDATE.GetValue().ToString().Trim().Substring(0, 4) + this.CBH01_FXDDPMK.GetValue().ToString().Trim() + dcr.Rows[i]["FXSYCNUM"].ToString();

                    if (dcr.Rows[i]["FXSCDAC"].ToString().Trim().Substring(0, 1) == "4")
                    {
                        this.DAT02_W2VLMI1.SetValue("");
                    }
                    else
                    {
                        this.DAT02_W2VLMI1.SetValue(dcr.Rows[i]["FXSFIXNUM"].ToString());
                    }

                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue(sVLMI4);
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    //sW2RKAC = "선급자재 대체(고정자산)";

                    this.DAT02_W2AMDR.SetValue(dcr.Rows[i]["FXSAMOUNT"].ToString());// 차변
                    this.DAT02_W2AMCR.SetValue("0"); // 대변

                    sW2RKAC = ""; // 2014.06.03 적요 발주명 에서 --> 자산명으로 수정(윤홍준)
                    sW2RKAC = dcr.Rows[i]["FXSNAME"].ToString();

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

                    #region Description : ACFIXADSF 전표번호 저장

                    dataFixU.Add(new object[] { dcr.Rows[i]["FXSSAUP"].ToString(),                   
                                                dcr.Rows[i]["FXSGUBN"].ToString(),
                                                dcr.Rows[i]["FXSYYMM"].ToString(),
                                                dcr.Rows[i]["FXSSEQ"].ToString(),
                                                dcr.Rows[i]["FXSCDAC"].ToString(),
                                                dcr.Rows[i]["FXSYCNUM"].ToString(),
                                                dcr.Rows[i]["FXTJASAN"].ToString(),
                                                dcr.Rows[i]["FXSYSSEQ"].ToString(),
                                                iCnt.ToString()
                             });

                    #endregion
                }

                //대변
                this.DbConnector.CommandClear();
                // 원본소스(21.12.08)
                // this.DbConnector.Attach("TY_P_MR_34423437", this.TXT01_FXDSAUP.GetValue().ToString());
                // 수정소스(21.12.08)
                this.DbConnector.Attach("TY_P_MR_BC8CT896", this.TXT01_FXDSAUP.GetValue().ToString());
                DataTable dj = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dj.Rows.Count; i++)
                {
                    #region Description : 대변

                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 취득일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(dj.Rows[i]["B2CDAC"].ToString());
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(dj.Rows[i]["FXDDPMK"].ToString()); // 귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", dj.Rows[i]["B2CDAC"].ToString(), "");
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


                    // 관리항목 1 == 01 거래처 
                    this.DAT02_W2VLMI1.SetValue(dj.Rows[i]["FXDGETVEND"].ToString());
                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    //sW2RKAC = "선급자재 대체(고정자산)";


                    this.DAT02_W2AMDR.SetValue("0");

                    // 21.02.04 수정 후 소스
                    this.DAT02_W2AMCR.SetValue(dj.Rows[i]["B2AMDR"].ToString());

                    // 21.02.04 수정 전 소스
                    //this.DAT02_W2AMCR.SetValue(dj.Rows[i]["FXDRRAMOUNT"].ToString());

                    this.DAT02_W2CDFD.SetValue("");
                    this.DAT02_W2AMFD.SetValue("0");

                    sW2RKAC = ""; // 2014.06.03 적요 발주명 에서 --> 입고의 적요 수정(윤홍준)
                    sW2RKAC = dj.Rows[i]["B2RKAC"].ToString();

                    this.DAT02_W2RKAC.SetValue(sW2RKAC);
                    this.DAT02_W2RKCU.SetValue(dj.Rows[i]["GETVENDNM"].ToString());
                    this.DAT02_W2WCJP.SetValue(dj.Rows[i]["BANJP"].ToString()); // 원전천표 번호(반제 정리)
                    this.DAT02_W2PRGB.SetValue("");
                    this.DAT02_W2HIGB.SetValue("A");
                    this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
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

                if (datas.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    foreach (object[] data in datas)
                    {
                        this.DbConnector.Attach("TY_P_AC_29DA5966", data);
                    }
                }


                //미승인 SP호출 파일 입력
                this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, Employer.EmpNo, "A",
                                                            sW2DPMK, this.TXT01_FXDGETDATE.GetValue().ToString(), "", "",
                                                            "", "", Employer.EmpNo);
                this.DbConnector.ExecuteTranQueryList();

                //전표 생성 함수 호출
                sOUTMSG = "";
                bJunPyoFlag = false;
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, "");
                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
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
                            sJpno = dtresult.Rows[0]["AGOUT_PARAM"].ToString();
                        }
                    }
                }

                if (bJunPyoFlag)
                {
                    // 21.12.07 적용(김상권)
                    // 1. 미승인 전표 번호를 가져온다.
                    // 2. ACFIXADTF파일의 FXTFIXNUM번호를 가져온다.
                    // 3. 미승인 전표 TABLE에서 항목값1의 값에 FXTFIXNUM 데이터가 있는 라이번호를 가져온다.
                    // 5. ACFIXADSF테이블에 업데이트 할때 전표번호 + 라인번호를 업데이트 한다.

                    //ACFIXADSF 전표번호 UPDATE
                    if (dataFixU.Count > 0)
                    {
                        // 원본소스(21.12.07)
                        //this.DbConnector.CommandClear();
                        //foreach (object[] data in dataFixU)
                        //{
                            
                        //    this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", "") + Set_Fill2(data[7].ToString()),
                        //                                                data[0].ToString(),
                        //                                                data[1].ToString(),
                        //                                                data[2].ToString(),
                        //                                                data[3].ToString(),
                        //                                                data[4].ToString(),
                        //                                                data[5].ToString(),
                        //                                                data[6].ToString()
                        //                                                );
                        //}
                        //this.DbConnector.ExecuteTranQueryList();


                        
                        // 수정소스(21.12.07)
                        // 차변
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_MR_34422436", this.TXT01_FXDSAUP.GetValue().ToString());
                        dcr = this.DbConnector.ExecuteDataTable();

                        for (int i = 0; i < dcr.Rows.Count; i++)
                        {
                            sB2NOLN = "0";

                            sB2NOLN = GET_ADSLGLF_B2NOLN(sJpno.Replace("-", ""), dcr.Rows[i]["FXSFIXNUM"].ToString());

                            // 수정소스(21.12.07)
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", "") + Set_Fill2(sB2NOLN.ToString()),
                                                                        dcr.Rows[i]["FXSSAUP"].ToString(),
                                                                        dcr.Rows[i]["FXSGUBN"].ToString(),
                                                                        dcr.Rows[i]["FXSYYMM"].ToString(),
                                                                        dcr.Rows[i]["FXSSEQ"].ToString(),
                                                                        dcr.Rows[i]["FXSCDAC"].ToString(),
                                                                        dcr.Rows[i]["FXSYCNUM"].ToString(),
                                                                        dcr.Rows[i]["FXTJASAN"].ToString(),
                                                                        dcr.Rows[i]["FXSYSSEQ"].ToString()

                                                                        );
                            this.DbConnector.ExecuteTranQuery();
                        }

                        //this.DbConnector.CommandClear();
                        //this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", ""), this.TXT01_FXDSAUP.GetValue().ToString());
                        //this.DbConnector.ExecuteTranQueryList();
                    }
                }

                #endregion
            }

            #endregion

            #region Description : 매출청구

            if (sCHUNGGB == "1")
            {
                dataFixU.Clear();

                #region Description : 수선비(화주사비용) 및 구축물(회사비용)

                //차변
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_MR_34422436", this.TXT01_FXDSAUP.GetValue().ToString());
                dcr = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dcr.Rows.Count; i++)
                {
                    #region Description : 차변

                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(dcr.Rows[i]["FXSCDAC"].ToString());
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); //  귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", dcr.Rows[i]["FXSCDAC"].ToString(), "");
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

                    // 관리항목 1 == 08 관리번호(고정자산번호)
                    // 관리항목 2 == 17 자동차번호(중기,차량)일때  
                    // 관리항목 4 == 35 예산세목 (년도(4)+부서(6)+순번(03)

                    string sVLMI4 = "";
                    sVLMI4 = this.TXT01_FXDGETDATE.GetValue().ToString().Trim().Substring(0, 4) + this.CBH01_FXDDPMK.GetValue().ToString().Trim() + dcr.Rows[i]["FXSYCNUM"].ToString();

                    if (dcr.Rows[i]["FXSCDAC"].ToString().Trim().Substring(0, 1) == "4")
                    {
                        this.DAT02_W2VLMI1.SetValue("");
                    }
                    else
                    {
                        this.DAT02_W2VLMI1.SetValue(dcr.Rows[i]["FXSFIXNUM"].ToString());
                    }

                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue(sVLMI4);
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    //sW2RKAC = "선급자재 대체(고정자산)";

                    this.DAT02_W2AMDR.SetValue(dcr.Rows[i]["FXSAMOUNT"].ToString());// 차변
                    this.DAT02_W2AMCR.SetValue("0"); // 대변

                    sW2RKAC = ""; // 2014.06.03 적요 발주명 에서 --> 자산명으로 수정(윤홍준)
                    sW2RKAC = dcr.Rows[i]["FXSNAME"].ToString();

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

                    #region Description : ACFIXADSF 전표번호 저장

                    dataFixU.Add(new object[] { dcr.Rows[i]["FXSSAUP"].ToString(),                   
                                                dcr.Rows[i]["FXSGUBN"].ToString(),
                                                dcr.Rows[i]["FXSYYMM"].ToString(),
                                                dcr.Rows[i]["FXSSEQ"].ToString(),
                                                dcr.Rows[i]["FXSCDAC"].ToString(),
                                                dcr.Rows[i]["FXSYCNUM"].ToString(),
                                                dcr.Rows[i]["FXTJASAN"].ToString(),
                                                dcr.Rows[i]["FXSYSSEQ"].ToString(),
                                                iCnt.ToString()
                             });

                    #endregion
                }

                //대변
                this.DbConnector.CommandClear();
                // 원본소스(21.12.08)
                // this.DbConnector.Attach("TY_P_MR_34423437", this.TXT01_FXDSAUP.GetValue().ToString());
                // 수정소스(21.12.08)
                this.DbConnector.Attach("TY_P_MR_BC8CT896", this.TXT01_FXDSAUP.GetValue().ToString());
                DataTable dj = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dj.Rows.Count; i++)
                {
                    #region Description : 대변

                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 취득일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(dj.Rows[i]["B2CDAC"].ToString());
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(dj.Rows[i]["FXDDPMK"].ToString()); // 귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", dj.Rows[i]["B2CDAC"].ToString(), "");
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


                    // 관리항목 1 == 01 거래처 
                    this.DAT02_W2VLMI1.SetValue(dj.Rows[i]["FXDGETVEND"].ToString());
                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    //sW2RKAC = "선급자재 대체(고정자산)";


                    this.DAT02_W2AMDR.SetValue("0");

                    // 21.02.04 수정 후 소스
                    this.DAT02_W2AMCR.SetValue(dj.Rows[i]["B2AMDR"].ToString());

                    // 21.02.04 수정 전 소스
                    //this.DAT02_W2AMCR.SetValue(dj.Rows[i]["FXDPOAMOUNT"].ToString());

                    this.DAT02_W2CDFD.SetValue("");
                    this.DAT02_W2AMFD.SetValue("0");

                    sW2RKAC = ""; // 2014.06.03 적요 발주명 에서 --> 입고의 적요 수정(윤홍준)
                    sW2RKAC = dj.Rows[i]["B2RKAC"].ToString();

                    this.DAT02_W2RKAC.SetValue(sW2RKAC);
                    this.DAT02_W2RKCU.SetValue(dj.Rows[i]["GETVENDNM"].ToString());
                    this.DAT02_W2WCJP.SetValue(dj.Rows[i]["BANJP"].ToString()); // 원전천표 번호(반제 정리)
                    this.DAT02_W2PRGB.SetValue("");
                    this.DAT02_W2HIGB.SetValue("A");
                    this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
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

                if (datas.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    foreach (object[] data in datas)
                    {
                        this.DbConnector.Attach("TY_P_AC_29DA5966", data);
                    }
                }

                //미승인 SP호출 파일 입력
                this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, Employer.EmpNo, "A",
                                                            sW2DPMK, this.TXT01_FXDGETDATE.GetValue().ToString(), "", "",
                                                            "", "", Employer.EmpNo);
                this.DbConnector.ExecuteTranQueryList();

                //전표 생성 함수 호출
                sOUTMSG = "";
                bJunPyoFlag = false;
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, "");
                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
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
                            sJpno = dtresult.Rows[0]["AGOUT_PARAM"].ToString();
                        }
                    }
                }

                if (bJunPyoFlag)
                {
                    // 21.12.07 적용(김상권)
                    // 1. 미승인 전표 번호를 가져온다.
                    // 2. ACFIXADTF파일의 FXTFIXNUM번호를 가져온다.
                    // 3. 미승인 전표 TABLE에서 항목값1의 값에 FXTFIXNUM 데이터가 있는 라이번호를 가져온다.
                    // 5. ACFIXADSF테이블에 업데이트 할때 전표번호 + 라인번호를 업데이트 한다.

                    //ACFIXADSF 전표번호 UPDATE
                    if (dataFixU.Count > 0)
                    {
                        // 원본소스(21.12.07)
                        //this.DbConnector.CommandClear();
                        //foreach (object[] data in dataFixU)
                        //{
                        //    this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", "") + Set_Fill2(data[7].ToString()),
                        //                                                data[0].ToString(),
                        //                                                data[1].ToString(),
                        //                                                data[2].ToString(),
                        //                                                data[3].ToString(),
                        //                                                data[4].ToString(),
                        //                                                data[5].ToString(),
                        //                                                data[6].ToString()
                        //                                                );
                        //}
                        //this.DbConnector.ExecuteTranQueryList();

                        // 수정소스(21.12.07)
                        // 차변
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_MR_34422436", this.TXT01_FXDSAUP.GetValue().ToString());
                        dcr = this.DbConnector.ExecuteDataTable();

                        for (int i = 0; i < dcr.Rows.Count; i++)
                        {
                            sB2NOLN = "0";

                            sB2NOLN = GET_ADSLGLF_B2NOLN(sJpno.Replace("-", ""), dcr.Rows[i]["FXSFIXNUM"].ToString());

                            // 수정소스(21.12.07)
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", "") + Set_Fill2(sB2NOLN.ToString()),
                                                                        dcr.Rows[i]["FXSSAUP"].ToString(),
                                                                        dcr.Rows[i]["FXSGUBN"].ToString(),
                                                                        dcr.Rows[i]["FXSYYMM"].ToString(),
                                                                        dcr.Rows[i]["FXSSEQ"].ToString(),
                                                                        dcr.Rows[i]["FXSCDAC"].ToString(),
                                                                        dcr.Rows[i]["FXSYCNUM"].ToString(),
                                                                        dcr.Rows[i]["FXTJASAN"].ToString(),
                                                                        dcr.Rows[i]["FXSYSSEQ"].ToString()
                                                                        );
                            this.DbConnector.ExecuteTranQuery();
                        }

                        //this.DbConnector.CommandClear();
                        //this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", ""), this.TXT01_FXDSAUP.GetValue().ToString());
                        //this.DbConnector.ExecuteTranQueryList();
                    }
                }

                #endregion
            }

            #endregion

            #region Description : 미수금청구

            if (sCHUNGGB == "2")
            {
                dataFixU.Clear();

                // 21.11.24 수정 전 원본 소스
                ////선급자재 미수금처리 구분 확인 조회
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_MR_345AC446", this.TXT01_FXDSAUP.GetValue().ToString(), this.TXT01_FXDSAUP.GetValue().ToString());  //ACFIXADSF
                //DataTable dt_prgubn = this.DbConnector.ExecuteDataTable();
                //if (dt_sabun.Rows.Count != 0)
                //{
                //    if (Convert.ToDouble(dt_prgubn.Rows[0]["ACNT"].ToString().Trim()) > 0 && Convert.ToDouble(dt_prgubn.Rows[0]["BCNT"].ToString().Trim()) > 0)
                //    {
                //        sMISUGB = "B"; // 화주사 및 당사 처리
                //    }
                //    else
                //    {
                //        sMISUGB = "A"; // 화주사 수선비용 100%
                //    }
                //}

                sMISUGB = "A"; // 화주사 수선비용 100%

                if (sMISUGB == "A")
                {
                    #region Description : 화주사 수선비용 100%

                    string sCRAMT = string.Empty;
                    string sCRVAT = string.Empty;
                    string sCRTOT = string.Empty;
                    string sCGVEND = string.Empty;
                    string sCGVENDNM = string.Empty;
                    string sFXSTASKAMT = string.Empty;
                    string sVATGB = string.Empty;

                    //미수금 합계 구하기
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_MR_34428438", this.TXT01_FXDSAUP.GetValue().ToString());
                    DataTable dcramt = this.DbConnector.ExecuteDataTable();
                    if (dcramt.Rows.Count > 0)
                    {
                        sCRAMT = dcramt.Rows[0]["FXSAMOUNT"].ToString().Trim();
                        sFXSTASKAMT = dcramt.Rows[0]["FXSTASKAMT"].ToString().Trim(); // 공사관리 감독 비용
                        sCRVAT = dcramt.Rows[0]["VAT71"].ToString().Trim();
                        sCRTOT = Convert.ToString(Convert.ToDouble(dcramt.Rows[0]["FXSAMOUNT"].ToString().Trim()) + Convert.ToDouble(dcramt.Rows[0]["VAT71"].ToString().Trim()));
                        sCGVEND = dcramt.Rows[0]["FXSCGVEND"].ToString().Trim();
                        sCGVENDNM = dcramt.Rows[0]["CGVENDNM"].ToString().Trim();
                        sVATGB = dcramt.Rows[0]["FXSVATGB"].ToString().Trim();
                    }

                    //차변(미수금)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_MR_34531453", this.TXT01_FXDSAUP.GetValue().ToString());
                    dcr = this.DbConnector.ExecuteDataTable();

                    for (int i = 0; i < dcr.Rows.Count; i++)
                    {
                        #region Description : 차변
                        iCnt = iCnt + 1;

                        dt.Clear();

                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                        this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 일자
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue("3");
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue(dcr.Rows[i]["FXSCDAC"].ToString()); // 기타 미수금
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); //  귀속부서

                        //관리항목 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_23N3M888", dcr.Rows[i]["FXSCDAC"].ToString(), "");
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

                        // 관리항목 1 == 01 거래처

                        this.DAT02_W2VLMI1.SetValue(dcr.Rows[i]["FXSCGVEND"].ToString()); // FXSCGVEND
                        this.DAT02_W2VLMI2.SetValue("");
                        this.DAT02_W2VLMI3.SetValue("");
                        this.DAT02_W2VLMI4.SetValue("");
                        this.DAT02_W2VLMI5.SetValue("");
                        this.DAT02_W2VLMI6.SetValue("");

                        //sW2RKAC = "선급자재 대체(고정자산)";

                        this.DAT02_W2AMDR.SetValue(sCRTOT);// 차변
                        this.DAT02_W2AMCR.SetValue("0");   // 대변

                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");

                        sW2RKAC = ""; // 2014.06.03 적요 발주명 에서 --> 자산명으로 수정(윤홍준)
                        sW2RKAC = dcr.Rows[i]["FXSNAME"].ToString();

                        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                        this.DAT02_W2RKCU.SetValue(dcr.Rows[i]["FXSYCNUM"].ToString());
                        this.DAT02_W2WCJP.SetValue("");
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
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

                        #region Description : ACFIXADSF 전표번호 저장

                        dataFixU.Add(new object[] { dcr.Rows[i]["FXSSAUP"].ToString(),                   
                                                dcr.Rows[i]["FXSGUBN"].ToString(),
                                                dcr.Rows[i]["FXSYYMM"].ToString(),
                                                dcr.Rows[i]["FXSSEQ"].ToString(),
                                                dcr.Rows[i]["FXSCDAC"].ToString(),
                                                dcr.Rows[i]["FXSYCNUM"].ToString(),
                                                dcr.Rows[i]["FXSJASAN"].ToString(),
                                                dcr.Rows[i]["FXSYSSEQ"].ToString(),
                                                iCnt.ToString()
                             });

                        #endregion
                    }

                    #region Description : 대변(관리감독 비용 청구)

                    if (Get_Numeric(sFXSTASKAMT.ToString()) != "0")
                    {
                        iCnt = iCnt + 1;

                        dt.Clear();

                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                        this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 취득일자
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue("3");
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue("51002588"); // 기타 잡이익
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); // 귀속부서

                        //관리항목 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_23N3M888", "51002588", "");
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

                        //거래처 코드
                        this.DAT02_W2VLMI1.SetValue("");
                        this.DAT02_W2VLMI2.SetValue("");
                        this.DAT02_W2VLMI3.SetValue("");
                        this.DAT02_W2VLMI4.SetValue("");
                        this.DAT02_W2VLMI5.SetValue("");
                        this.DAT02_W2VLMI6.SetValue("");

                        this.DAT02_W2AMDR.SetValue("0");

                        this.DAT02_W2AMCR.SetValue(Get_Numeric(sFXSTASKAMT));

                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");

                        sW2RKAC = sW2RKAC + " 공사감독 대행비";

                        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                        this.DAT02_W2RKCU.SetValue("");
                        this.DAT02_W2WCJP.SetValue(""); // 원전천표 번호(반제 정리)
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
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

                    //대변 (선급자재)
                    this.DbConnector.CommandClear();
                    // 원본소스(21.12.08)
                    // this.DbConnector.Attach("TY_P_MR_34423437", this.TXT01_FXDSAUP.GetValue().ToString());
                    // 수정소스(21.12.08)
                    this.DbConnector.Attach("TY_P_MR_BC8CT896", this.TXT01_FXDSAUP.GetValue().ToString());
                    DataTable dj = this.DbConnector.ExecuteDataTable();

                    for (int i = 0; i < dj.Rows.Count; i++)
                    {
                        #region Description : 대변
                        iCnt = iCnt + 1;

                        dt.Clear();

                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                        this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 취득일자
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue("3");
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue(dj.Rows[i]["B2CDAC"].ToString());
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(dj.Rows[i]["FXDDPMK"].ToString()); // 귀속부서

                        //관리항목 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_23N3M888", dj.Rows[i]["B2CDAC"].ToString(), "");
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

                        //거래처 코드
                        this.DAT02_W2VLMI1.SetValue(dj.Rows[i]["FXDGETVEND"].ToString());
                        this.DAT02_W2VLMI2.SetValue("");
                        this.DAT02_W2VLMI3.SetValue("");
                        this.DAT02_W2VLMI4.SetValue("");
                        this.DAT02_W2VLMI5.SetValue("");
                        this.DAT02_W2VLMI6.SetValue("");

                        //sW2RKAC = "선급자재 대체(고정자산)";

                        this.DAT02_W2AMDR.SetValue("0");

                        // 21.02.04 수정 후 소스
                        this.DAT02_W2AMCR.SetValue(dj.Rows[i]["B2AMDR"].ToString());

                        // 21.02.04 수정 전 소스
                        //this.DAT02_W2AMCR.SetValue(dj.Rows[i]["FXDPOAMOUNT"].ToString());

                        this.DAT02_W2CDFD.SetValue("");
                        this.DAT02_W2AMFD.SetValue("0");

                        sW2RKAC = ""; // 2014.06.03 적요 발주명 에서 --> 입고의 적요 수정(윤홍준)
                        sW2RKAC = dj.Rows[i]["B2RKAC"].ToString();

                        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                        this.DAT02_W2RKCU.SetValue(dj.Rows[i]["GETVENDNM"].ToString());
                        this.DAT02_W2WCJP.SetValue(dj.Rows[i]["BANJP"].ToString()); // 원전천표 번호(반제 정리)
                        this.DAT02_W2PRGB.SetValue("");
                        this.DAT02_W2HIGB.SetValue("A");
                        this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
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

                    //대변
                    #region Description : 대변 (부가세)
                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue("21103101"); // 매출부가세 본점
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); //  귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", "21103101", "");
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

                    // 관리항목 1 == 11 세무구분
                    // 관리항목 2 == 01 거래처
                    // 관리항목 3 == 15 거래일자
                    // 관리항목 4 == 14 공급가액

                    this.DAT02_W2VLMI1.SetValue(sVATGB);
                    this.DAT02_W2VLMI2.SetValue(sCGVEND);
                    this.DAT02_W2VLMI3.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString());
                    this.DAT02_W2VLMI4.SetValue(sCRAMT);
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    sW2RKAC = "매출 부가세"; // +sW2RKAC.ToString();

                    this.DAT02_W2AMDR.SetValue("0");// 차변
                    this.DAT02_W2AMCR.SetValue(sCRVAT); // 대변

                    this.DAT02_W2CDFD.SetValue("");
                    this.DAT02_W2AMFD.SetValue("0");
                    this.DAT02_W2RKAC.SetValue(sW2RKAC);
                    this.DAT02_W2RKCU.SetValue(sCGVENDNM);
                    this.DAT02_W2WCJP.SetValue("");
                    this.DAT02_W2PRGB.SetValue("");
                    this.DAT02_W2HIGB.SetValue("A");
                    this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
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

                    #endregion

                    if (datas.Count > 0)
                    {
                        this.DbConnector.CommandClear();
                        foreach (object[] data in datas)
                        {
                            this.DbConnector.Attach("TY_P_AC_29DA5966", data);
                        }
                    }

                    //미승인 SP호출 파일 입력
                    this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, Employer.EmpNo, "A",
                                                                sW2DPMK, this.TXT01_FXDGETDATE.GetValue().ToString(), "", "",
                                                                "", "", Employer.EmpNo);
                    this.DbConnector.ExecuteTranQueryList();

                    //전표 생성 함수 호출
                    sOUTMSG = "";
                    bJunPyoFlag = false;
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, "");
                    sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
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
                                sJpno = dtresult.Rows[0]["AGOUT_PARAM"].ToString();
                            }
                        }
                    }

                    if (bJunPyoFlag)
                    {
                        //ACFIXADSF 전표번호 UPDATE

                        if (dataFixU.Count > 0)
                        {
                            this.DbConnector.CommandClear();
                            foreach (object[] data in dataFixU)
                            {
                                this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", "") + Set_Fill2(data[7].ToString()),
                                                                            data[0].ToString(),
                                                                            data[1].ToString(),
                                                                            data[2].ToString(),
                                                                            data[3].ToString(),
                                                                            data[4].ToString(),
                                                                            data[5].ToString(),
                                                                            data[6].ToString(),
                                                                            data[7].ToString()
                                                                            );
                            }
                            this.DbConnector.ExecuteTranQueryList();

                            //this.DbConnector.CommandClear();
                            //this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", ""), this.TXT01_FXDSAUP.GetValue().ToString());
                            //this.DbConnector.ExecuteTranQueryList();
                        }
                    }
                }

                // 화주사,우리회사 수선비용 공동 부담하고 우리회사 비용을 자산처리 전표 2개 처리

                //if (sMISUGB == "B")
                //{
                //    dataFixU.Clear();

                //    #region Description : 구축물 비용 처리

                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_MR_34422436", this.TXT01_FXDSAUP.GetValue().ToString());
                //    DataTable ddr = this.DbConnector.ExecuteDataTable();

                //    for (int i = 0; i < ddr.Rows.Count; i++)
                //    {
                //        #region Description : 차변

                //        iCnt = iCnt + 1;

                //        dt.Clear();

                //        this.DAT02_W2SSID.SetValue(sB2SSID);
                //        this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                //        this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 일자
                //        this.DAT02_W2NOSQ.SetValue("0");
                //        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                //        this.DAT02_W2IDJP.SetValue("3");
                //        this.DAT02_W2NOJP.SetValue("");
                //        this.DAT02_W2CDAC.SetValue(ddr.Rows[i]["FXSCDAC"].ToString());
                //        this.DAT02_W2DTAC.SetValue("");
                //        this.DAT02_W2DTLI.SetValue("");
                //        this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); //  귀속부서

                //        //관리항목 
                //        this.DbConnector.CommandClear();
                //        this.DbConnector.Attach("TY_P_AC_23N3M888", ddr.Rows[i]["FXSCDAC"].ToString(), "");
                //        dt = this.DbConnector.ExecuteDataTable();
                //        if (dt.Rows.Count > 0)
                //        {
                //            if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI1.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI2.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI3.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI4.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI5.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI6.SetValue("");
                //            }
                //        }

                //        // 관리항목 1 == 08 관리번호(고정자산번호)
                //        // 관리항목 2 == 17 자동차번호(중기,차량)일때  
                //        // 관리항목 4 == 35 예산세목 (년도(4)+부서(6)+순번(03)

                //        string sVLMI4 = "";

                //        sVLMI4 = this.TXT01_FXDGETDATE.GetValue().ToString().Trim().Substring(0, 4) + this.CBH01_FXDDPMK.GetValue().ToString().Trim() + ddr.Rows[i]["FXSYCNUM"].ToString();


                //        this.DAT02_W2VLMI1.SetValue(ddr.Rows[i]["FXSFIXNUM"].ToString());
                //        this.DAT02_W2VLMI2.SetValue("");
                //        this.DAT02_W2VLMI3.SetValue("");
                //        this.DAT02_W2VLMI4.SetValue(sVLMI4);
                //        this.DAT02_W2VLMI5.SetValue("");
                //        this.DAT02_W2VLMI6.SetValue("");

                //        //sW2RKAC = "선급자재 대체(고정자산)";

                //        this.DAT02_W2AMDR.SetValue(ddr.Rows[i]["FXSAMOUNT"].ToString());// 차변
                //        this.DAT02_W2AMCR.SetValue("0"); // 대변

                //        this.DAT02_W2CDFD.SetValue("");
                //        this.DAT02_W2AMFD.SetValue("0");

                //        sW2RKAC = ""; // 2014.06.03 적요 발주명 에서 --> 자산명 수정(윤홍준)
                //        sW2RKAC = ddr.Rows[i]["FXSNAME"].ToString();

                //        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                //        this.DAT02_W2RKCU.SetValue("");
                //        this.DAT02_W2WCJP.SetValue("");
                //        this.DAT02_W2PRGB.SetValue("");
                //        this.DAT02_W2HIGB.SetValue("A");
                //        this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
                //        this.DAT02_W2GUBUN.SetValue("");
                //        this.DAT02_W2TXAMT.SetValue("0");
                //        this.DAT02_W2TXVAT.SetValue("0");
                //        this.DAT02_W2HWAJU.SetValue("");

                //        datas.Add(new object[] {this.DAT02_W2SSID.GetValue().ToString(),
                //                        this.DAT02_W2DPMK.GetValue().ToString(),
                //                        this.DAT02_W2DTMK.GetValue().ToString(),
                //                        this.DAT02_W2NOSQ.GetValue().ToString(),
                //                        this.DAT02_W2NOLN.GetValue().ToString(),
                //                        this.DAT02_W2IDJP.GetValue().ToString(),
                //                        this.DAT02_W2NOJP.GetValue().ToString(),
                //                        this.DAT02_W2CDAC.GetValue().ToString(),
                //                        this.DAT02_W2DTAC.GetValue().ToString(),
                //                        this.DAT02_W2DTLI.GetValue().ToString(),
                //                        this.DAT02_W2DPAC.GetValue().ToString(),
                //                        this.DAT02_W2CDMI1.GetValue().ToString(),
                //                        this.DAT02_W2VLMI1.GetValue().ToString(),
                //                        this.DAT02_W2CDMI2.GetValue().ToString(),
                //                        this.DAT02_W2VLMI2.GetValue().ToString(),
                //                        this.DAT02_W2CDMI3.GetValue().ToString(),
                //                        this.DAT02_W2VLMI3.GetValue().ToString(),
                //                        this.DAT02_W2CDMI4.GetValue().ToString(),
                //                        this.DAT02_W2VLMI4.GetValue().ToString(),
                //                        this.DAT02_W2CDMI5.GetValue().ToString(),
                //                        this.DAT02_W2VLMI5.GetValue().ToString(),
                //                        this.DAT02_W2CDMI6.GetValue().ToString(),
                //                        this.DAT02_W2VLMI6.GetValue().ToString(),
                //                        this.DAT02_W2AMDR.GetValue().ToString(),
                //                        this.DAT02_W2AMCR.GetValue().ToString(),
                //                        this.DAT02_W2CDFD.GetValue().ToString(),
                //                        this.DAT02_W2AMFD.GetValue().ToString(),
                //                        this.DAT02_W2RKAC.GetValue().ToString(),
                //                        this.DAT02_W2RKCU.GetValue().ToString(),
                //                        this.DAT02_W2WCJP.GetValue().ToString(),
                //                        this.DAT02_W2PRGB.GetValue().ToString(),
                //                        this.DAT02_W2HIGB.GetValue().ToString(),
                //                        this.DAT02_W2HISAB.GetValue().ToString(),
                //                        this.DAT02_W2GUBUN.GetValue().ToString(),
                //                        this.DAT02_W2TXAMT.GetValue().ToString(),
                //                        this.DAT02_W2TXVAT.GetValue().ToString(),
                //                        this.DAT02_W2HWAJU.GetValue().ToString()});
                //        #endregion

                //        #region Description : ACFIXADSF 전표번호 저장

                //        dataFixU.Add(new object[] { ddr.Rows[i]["FXSSAUP"].ToString(),                   
                //                                ddr.Rows[i]["FXSGUBN"].ToString(),
                //                                ddr.Rows[i]["FXSYYMM"].ToString(),
                //                                ddr.Rows[i]["FXSSEQ"].ToString(),
                //                                ddr.Rows[i]["FXSCDAC"].ToString(),
                //                                ddr.Rows[i]["FXSYCNUM"].ToString(),
                //                                ddr.Rows[i]["FXTJASAN"].ToString(),
                //                                iCnt.ToString()
                //             });

                //        #endregion
                //    }

                //    //대변
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_MR_345AZ447", this.TXT01_FXDSAUP.GetValue().ToString(), this.TXT01_FXDSAUP.GetValue().ToString());
                //    DataTable dcr = this.DbConnector.ExecuteDataTable();

                //    for (int i = 0; i < dcr.Rows.Count; i++)
                //    {
                //        #region Description : 대변

                //        // 20181116 황성환 요청
                //        // 20190101 이전부터 진행중인 공사건에 대해선
                //        // 20190101일부터 선급자재(11101001)을 건설중인자산(12210000)으로 전표가 끊어져야 함

                //        string sCDAC = string.Empty;

                //        sCDAC = "11101001";

                //        if (int.Parse(Get_Numeric(this.TXT01_FXDGETDATE.GetValue().ToString())) >= 20190101)
                //        {
                //            sCDAC = "12210000";
                //        }

                //        iCnt = iCnt + 1;

                //        dt.Clear();

                //        this.DAT02_W2SSID.SetValue(sB2SSID);
                //        this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                //        this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 일자
                //        this.DAT02_W2NOSQ.SetValue("0");
                //        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                //        this.DAT02_W2IDJP.SetValue("3");
                //        this.DAT02_W2NOJP.SetValue("");
                //        this.DAT02_W2CDAC.SetValue(sCDAC.ToString());// 계정과목 (선급자재)
                //        this.DAT02_W2DTAC.SetValue("");
                //        this.DAT02_W2DTLI.SetValue("");
                //        this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); //  귀속부서

                //        //관리항목 
                //        this.DbConnector.CommandClear();
                //        this.DbConnector.Attach("TY_P_AC_23N3M888", sCDAC.ToString(), "");
                //        dt = this.DbConnector.ExecuteDataTable();
                //        if (dt.Rows.Count > 0)
                //        {
                //            if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI1.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI2.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI3.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI4.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI5.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI6.SetValue("");
                //            }
                //        }

                //        // 관리항목 1 == 01 거래처


                //        this.DAT02_W2VLMI1.SetValue(dcr.Rows[i]["FXDGETVEND"].ToString());
                //        this.DAT02_W2VLMI2.SetValue("");
                //        this.DAT02_W2VLMI3.SetValue("");
                //        this.DAT02_W2VLMI4.SetValue("");
                //        this.DAT02_W2VLMI5.SetValue("");
                //        this.DAT02_W2VLMI6.SetValue("");

                //        //sW2RKAC = "선급자재 대체(고정자산)";

                //        this.DAT02_W2AMDR.SetValue("0");// 차변
                //        this.DAT02_W2AMCR.SetValue(dcr.Rows[i]["FXYSAMOUNT"].ToString()); // 대변

                //        this.DAT02_W2CDFD.SetValue("");
                //        this.DAT02_W2AMFD.SetValue("0");

                //        sW2RKAC = ""; // 2014.06.03 적요 발주명 에서 --> 자산명 수정(윤홍준)
                //        sW2RKAC = dcr.Rows[i]["FXSNAME"].ToString();

                //        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                //        this.DAT02_W2RKCU.SetValue(dcr.Rows[i]["GETVENDNM"].ToString());
                //        this.DAT02_W2WCJP.SetValue(dcr.Rows[i]["FXDREPJPNO"].ToString());
                //        this.DAT02_W2PRGB.SetValue("");
                //        this.DAT02_W2HIGB.SetValue("A");
                //        this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
                //        this.DAT02_W2GUBUN.SetValue("");
                //        this.DAT02_W2TXAMT.SetValue("0");
                //        this.DAT02_W2TXVAT.SetValue("0");
                //        this.DAT02_W2HWAJU.SetValue("");

                //        datas.Add(new object[] {this.DAT02_W2SSID.GetValue().ToString(),
                //                        this.DAT02_W2DPMK.GetValue().ToString(),
                //                        this.DAT02_W2DTMK.GetValue().ToString(),
                //                        this.DAT02_W2NOSQ.GetValue().ToString(),
                //                        this.DAT02_W2NOLN.GetValue().ToString(),
                //                        this.DAT02_W2IDJP.GetValue().ToString(),
                //                        this.DAT02_W2NOJP.GetValue().ToString(),
                //                        this.DAT02_W2CDAC.GetValue().ToString(),
                //                        this.DAT02_W2DTAC.GetValue().ToString(),
                //                        this.DAT02_W2DTLI.GetValue().ToString(),
                //                        this.DAT02_W2DPAC.GetValue().ToString(),
                //                        this.DAT02_W2CDMI1.GetValue().ToString(),
                //                        this.DAT02_W2VLMI1.GetValue().ToString(),
                //                        this.DAT02_W2CDMI2.GetValue().ToString(),
                //                        this.DAT02_W2VLMI2.GetValue().ToString(),
                //                        this.DAT02_W2CDMI3.GetValue().ToString(),
                //                        this.DAT02_W2VLMI3.GetValue().ToString(),
                //                        this.DAT02_W2CDMI4.GetValue().ToString(),
                //                        this.DAT02_W2VLMI4.GetValue().ToString(),
                //                        this.DAT02_W2CDMI5.GetValue().ToString(),
                //                        this.DAT02_W2VLMI5.GetValue().ToString(),
                //                        this.DAT02_W2CDMI6.GetValue().ToString(),
                //                        this.DAT02_W2VLMI6.GetValue().ToString(),
                //                        this.DAT02_W2AMDR.GetValue().ToString(),
                //                        this.DAT02_W2AMCR.GetValue().ToString(),
                //                        this.DAT02_W2CDFD.GetValue().ToString(),
                //                        this.DAT02_W2AMFD.GetValue().ToString(),
                //                        this.DAT02_W2RKAC.GetValue().ToString(),
                //                        this.DAT02_W2RKCU.GetValue().ToString(),
                //                        this.DAT02_W2WCJP.GetValue().ToString(),
                //                        this.DAT02_W2PRGB.GetValue().ToString(),
                //                        this.DAT02_W2HIGB.GetValue().ToString(),
                //                        this.DAT02_W2HISAB.GetValue().ToString(),
                //                        this.DAT02_W2GUBUN.GetValue().ToString(),
                //                        this.DAT02_W2TXAMT.GetValue().ToString(),
                //                        this.DAT02_W2TXVAT.GetValue().ToString(),
                //                        this.DAT02_W2HWAJU.GetValue().ToString()});
                //        #endregion
                //    }


                //    //미승인 SP호출 파일 입력
                //    this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, Employer.EmpNo, "A",
                //                                                sW2DPMK, this.TXT01_FXDGETDATE.GetValue().ToString(), "", "",
                //                                                "", "", Employer.EmpNo);
                //    this.DbConnector.ExecuteTranQueryList();

                //    //전표 생성 함수 호출
                //    sOUTMSG1 = "";
                //    bJunPyoFlag = false;
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, "");
                //    sOUTMSG1 = Convert.ToString(this.DbConnector.ExecuteScalar());
                //    if (sOUTMSG1.Substring(0, 2) == "ER")
                //    {
                //        this.ShowCustomMessage(sOUTMSG1, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //    }
                //    else
                //    {
                //        bJunPyoFlag = true;

                //        this.ShowMessage("TY_M_AC_25O8K620");

                //        this.DbConnector.CommandClear();
                //        this.DbConnector.Attach("TY_P_AC_29D5B004", sB2SSID);
                //        DataTable dtresult = this.DbConnector.ExecuteDataTable();
                //        if (dtresult.Rows.Count > 0)
                //        {
                //            if (dtresult.Rows[0]["AGRESULTCODE"].ToString().Trim() == "OK")
                //            {
                //                //전표번호 받아오기
                //                sJpno = dtresult.Rows[0]["AGOUT_PARAM"].ToString();
                //            }
                //        }
                //    }

                //    if (bJunPyoFlag)
                //    {
                //        //ACFIXADSF 전표번호 UPDATE                       

                //        if (dataFixU.Count > 0)
                //        {
                //            this.DbConnector.CommandClear();
                //            foreach (object[] data in dataFixU)
                //            {
                //                this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", "") + Set_Fill2(data[7].ToString()),
                //                                                            data[0].ToString(),
                //                                                            data[1].ToString(),
                //                                                            data[2].ToString(),
                //                                                            data[3].ToString(),
                //                                                            data[4].ToString(),
                //                                                            data[5].ToString(),
                //                                                            data[6].ToString()
                //                                                            );
                //            }
                //            this.DbConnector.ExecuteTranQueryList();

                //            //this.DbConnector.CommandClear();
                //            //this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", ""), this.TXT01_FXDSAUP.GetValue().ToString());
                //            //this.DbConnector.ExecuteTranQueryList();
                //        }
                //    }
                //    #endregion

                //    // 전표 분리
                //    //--------------------------------------------------- //
                //    //   전표 2 장 발생                                    //
                //    //--------------------------------------------------- //

                //    #region Description : 미수금 청구

                //    //BATID번호 부여
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_AC_29C7M958");
                //    decimal dAutoSeq1 = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
                //    sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq1.ToString();

                //    string sCRAMT = string.Empty;
                //    string sCRVAT = string.Empty;
                //    string sCRTOT = string.Empty;
                //    string sCGVEND = string.Empty;
                //    string sCGVENDNM = string.Empty;
                //    string sVATGB = string.Empty;

                //    //미수금 합계 구하기
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_MR_34428438", this.TXT01_FXDSAUP.GetValue().ToString());
                //    DataTable dcramt = this.DbConnector.ExecuteDataTable();
                //    if (dcramt.Rows.Count > 0)
                //    {
                //        sCRAMT = dcramt.Rows[0]["FXSAMOUNT"].ToString().Trim();
                //        sCRVAT = dcramt.Rows[0]["VAT71"].ToString().Trim();
                //        sCRTOT = Convert.ToString(Convert.ToDouble(dcramt.Rows[0]["FXSAMOUNT"].ToString().Trim()) + Convert.ToDouble(dcramt.Rows[0]["VAT71"].ToString().Trim()));
                //        sCGVEND = dcramt.Rows[0]["FXSCGVEND"].ToString().Trim();
                //        sCGVENDNM = dcramt.Rows[0]["CGVENDNM"].ToString().Trim();
                //        sVATGB = dcramt.Rows[0]["FXSVATGB"].ToString().Trim();
                //    }
                //    //차변
                //    #region Description : 차변

                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_MR_35K1T693", this.TXT01_FXDSAUP.GetValue().ToString());
                //    DataTable ddr_1 = this.DbConnector.ExecuteDataTable();

                //    for (int i = 0; i < dcr.Rows.Count; i++)
                //    {
                //        iCnt = iCnt + 1;

                //        dt.Clear();

                //        this.DAT02_W2SSID.SetValue(sB2SSID);
                //        this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                //        this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 일자
                //        this.DAT02_W2NOSQ.SetValue("0");
                //        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                //        this.DAT02_W2IDJP.SetValue("3");
                //        this.DAT02_W2NOJP.SetValue("");
                //        this.DAT02_W2CDAC.SetValue(dcr.Rows[i]["FXSCDAC"].ToString()); // 기타 미수금
                //        this.DAT02_W2DTAC.SetValue("");
                //        this.DAT02_W2DTLI.SetValue("");
                //        this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); //  귀속부서

                //        //관리항목 
                //        this.DbConnector.CommandClear();
                //        this.DbConnector.Attach(dcr.Rows[i]["FXSCDAC"].ToString());
                //        dt = this.DbConnector.ExecuteDataTable();
                //        if (dt.Rows.Count > 0)
                //        {
                //            if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI1.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI2.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI3.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI4.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI5.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI6.SetValue("");
                //            }
                //        }

                //        // 관리항목 1 == 01 거래처

                //        this.DAT02_W2VLMI1.SetValue(ddr_1.Rows[i]["FXSCGVEND"].ToString()); // FXSCGVEND
                //        this.DAT02_W2VLMI2.SetValue("");
                //        this.DAT02_W2VLMI3.SetValue("");
                //        this.DAT02_W2VLMI4.SetValue("");
                //        this.DAT02_W2VLMI5.SetValue("");
                //        this.DAT02_W2VLMI6.SetValue("");

                //        //sW2RKAC = "선급자재 대체(고정자산)";

                //        this.DAT02_W2AMDR.SetValue(sCRTOT);// 차변
                //        this.DAT02_W2AMCR.SetValue("0");   // 대변

                //        this.DAT02_W2CDFD.SetValue("");
                //        this.DAT02_W2AMFD.SetValue("0");
                //        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                //        this.DAT02_W2RKCU.SetValue(ddr_1.Rows[i]["FXSYCNUM"].ToString());
                //        this.DAT02_W2WCJP.SetValue("");
                //        this.DAT02_W2PRGB.SetValue("");
                //        this.DAT02_W2HIGB.SetValue("A");
                //        this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
                //        this.DAT02_W2GUBUN.SetValue("");
                //        this.DAT02_W2TXAMT.SetValue("0");
                //        this.DAT02_W2TXVAT.SetValue("0");
                //        this.DAT02_W2HWAJU.SetValue("");

                //        datas.Add(new object[] {this.DAT02_W2SSID.GetValue().ToString(),
                //                        this.DAT02_W2DPMK.GetValue().ToString(),
                //                        this.DAT02_W2DTMK.GetValue().ToString(),
                //                        this.DAT02_W2NOSQ.GetValue().ToString(),
                //                        this.DAT02_W2NOLN.GetValue().ToString(),
                //                        this.DAT02_W2IDJP.GetValue().ToString(),
                //                        this.DAT02_W2NOJP.GetValue().ToString(),
                //                        this.DAT02_W2CDAC.GetValue().ToString(),
                //                        this.DAT02_W2DTAC.GetValue().ToString(),
                //                        this.DAT02_W2DTLI.GetValue().ToString(),
                //                        this.DAT02_W2DPAC.GetValue().ToString(),
                //                        this.DAT02_W2CDMI1.GetValue().ToString(),
                //                        this.DAT02_W2VLMI1.GetValue().ToString(),
                //                        this.DAT02_W2CDMI2.GetValue().ToString(),
                //                        this.DAT02_W2VLMI2.GetValue().ToString(),
                //                        this.DAT02_W2CDMI3.GetValue().ToString(),
                //                        this.DAT02_W2VLMI3.GetValue().ToString(),
                //                        this.DAT02_W2CDMI4.GetValue().ToString(),
                //                        this.DAT02_W2VLMI4.GetValue().ToString(),
                //                        this.DAT02_W2CDMI5.GetValue().ToString(),
                //                        this.DAT02_W2VLMI5.GetValue().ToString(),
                //                        this.DAT02_W2CDMI6.GetValue().ToString(),
                //                        this.DAT02_W2VLMI6.GetValue().ToString(),
                //                        this.DAT02_W2AMDR.GetValue().ToString(),
                //                        this.DAT02_W2AMCR.GetValue().ToString(),
                //                        this.DAT02_W2CDFD.GetValue().ToString(),
                //                        this.DAT02_W2AMFD.GetValue().ToString(),
                //                        this.DAT02_W2RKAC.GetValue().ToString(),
                //                        this.DAT02_W2RKCU.GetValue().ToString(),
                //                        this.DAT02_W2WCJP.GetValue().ToString(),
                //                        this.DAT02_W2PRGB.GetValue().ToString(),
                //                        this.DAT02_W2HIGB.GetValue().ToString(),
                //                        this.DAT02_W2HISAB.GetValue().ToString(),
                //                        this.DAT02_W2GUBUN.GetValue().ToString(),
                //                        this.DAT02_W2TXAMT.GetValue().ToString(),
                //                        this.DAT02_W2TXVAT.GetValue().ToString(),
                //                        this.DAT02_W2HWAJU.GetValue().ToString()});

                //        #region Description : ACFIXADSF 전표번호 저장

                //        dataFixU.Add(new object[] { ddr_1.Rows[i]["FXSSAUP"].ToString(),                   
                //                                ddr_1.Rows[i]["FXSGUBN"].ToString(),
                //                                ddr_1.Rows[i]["FXSYYMM"].ToString(),
                //                                ddr_1.Rows[i]["FXSSEQ"].ToString(),
                //                                ddr_1.Rows[i]["FXSCDAC"].ToString(),
                //                                ddr_1.Rows[i]["FXSYCNUM"].ToString(),
                //                                ddr_1.Rows[i]["FXSJASAN"].ToString(),
                //                                iCnt.ToString()
                //             });

                //        #endregion
                //    }
                //    #endregion

                //    //대변 (선급자재)
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_MR_345BN448", this.TXT01_FXDSAUP.GetValue().ToString(), this.TXT01_FXDSAUP.GetValue().ToString());
                //    DataTable dcr_1 = this.DbConnector.ExecuteDataTable();

                //    for (int i = 0; i < dcr_1.Rows.Count; i++)
                //    {
                //        #region Description : 대변

                //        // 20181116 황성환 요청
                //        // 20190101 이전부터 진행중인 공사건에 대해선
                //        // 20190101일부터 선급자재(11101001)을 건설중인자산(12210000)으로 전표가 끊어져야 함

                //        string sCDAC = string.Empty;

                //        sCDAC = "11101001";

                //        if (int.Parse(Get_Numeric(this.TXT01_FXDGETDATE.GetValue().ToString())) >= 20190101)
                //        {
                //            sCDAC = "12210000";
                //        }

                //        iCnt = iCnt + 1;

                //        dt.Clear();

                //        this.DAT02_W2SSID.SetValue(sB2SSID);
                //        this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                //        this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 일자
                //        this.DAT02_W2NOSQ.SetValue("0");
                //        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                //        this.DAT02_W2IDJP.SetValue("3");
                //        this.DAT02_W2NOJP.SetValue("");
                //        this.DAT02_W2CDAC.SetValue(sCDAC.ToString());// 계정과목 (선급자재)
                //        this.DAT02_W2DTAC.SetValue("");
                //        this.DAT02_W2DTLI.SetValue("");
                //        this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); //  귀속부서

                //        //관리항목 
                //        this.DbConnector.CommandClear();
                //        this.DbConnector.Attach("TY_P_AC_23N3M888", sCDAC.ToString(), "");
                //        dt = this.DbConnector.ExecuteDataTable();
                //        if (dt.Rows.Count > 0)
                //        {
                //            if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI1.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI2.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI3.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI4.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI5.SetValue("");
                //            }
                //            if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                //            {
                //                this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2));
                //            }
                //            else
                //            {
                //                this.DAT02_W2CDMI6.SetValue("");
                //            }
                //        }

                //        // 관리항목 1 == 01 거래처


                //        this.DAT02_W2VLMI1.SetValue(dcr_1.Rows[i]["FXDGETVEND"].ToString());
                //        this.DAT02_W2VLMI2.SetValue("");
                //        this.DAT02_W2VLMI3.SetValue("");
                //        this.DAT02_W2VLMI4.SetValue("");
                //        this.DAT02_W2VLMI5.SetValue("");
                //        this.DAT02_W2VLMI6.SetValue("");

                //        //sW2RKAC = "선급자재 대체(고정자산)";

                //        this.DAT02_W2AMDR.SetValue("0");// 차변
                //        this.DAT02_W2AMCR.SetValue(dcr_1.Rows[i]["FXYSAMOUNT"].ToString()); // 대변

                //        this.DAT02_W2CDFD.SetValue("");
                //        this.DAT02_W2AMFD.SetValue("0");
                //        this.DAT02_W2RKAC.SetValue(sW2RKAC);
                //        this.DAT02_W2RKCU.SetValue(dcr_1.Rows[i]["GETVENDNM"].ToString());
                //        this.DAT02_W2WCJP.SetValue(dcr_1.Rows[i]["FXDREPJPNO"].ToString()); // 원천번호(반제 정리)
                //        this.DAT02_W2PRGB.SetValue("");
                //        this.DAT02_W2HIGB.SetValue("A");
                //        this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
                //        this.DAT02_W2GUBUN.SetValue("");
                //        this.DAT02_W2TXAMT.SetValue("0");
                //        this.DAT02_W2TXVAT.SetValue("0");
                //        this.DAT02_W2HWAJU.SetValue("");

                //        datas.Add(new object[] {this.DAT02_W2SSID.GetValue().ToString(),
                //                        this.DAT02_W2DPMK.GetValue().ToString(),
                //                        this.DAT02_W2DTMK.GetValue().ToString(),
                //                        this.DAT02_W2NOSQ.GetValue().ToString(),
                //                        this.DAT02_W2NOLN.GetValue().ToString(),
                //                        this.DAT02_W2IDJP.GetValue().ToString(),
                //                        this.DAT02_W2NOJP.GetValue().ToString(),
                //                        this.DAT02_W2CDAC.GetValue().ToString(),
                //                        this.DAT02_W2DTAC.GetValue().ToString(),
                //                        this.DAT02_W2DTLI.GetValue().ToString(),
                //                        this.DAT02_W2DPAC.GetValue().ToString(),
                //                        this.DAT02_W2CDMI1.GetValue().ToString(),
                //                        this.DAT02_W2VLMI1.GetValue().ToString(),
                //                        this.DAT02_W2CDMI2.GetValue().ToString(),
                //                        this.DAT02_W2VLMI2.GetValue().ToString(),
                //                        this.DAT02_W2CDMI3.GetValue().ToString(),
                //                        this.DAT02_W2VLMI3.GetValue().ToString(),
                //                        this.DAT02_W2CDMI4.GetValue().ToString(),
                //                        this.DAT02_W2VLMI4.GetValue().ToString(),
                //                        this.DAT02_W2CDMI5.GetValue().ToString(),
                //                        this.DAT02_W2VLMI5.GetValue().ToString(),
                //                        this.DAT02_W2CDMI6.GetValue().ToString(),
                //                        this.DAT02_W2VLMI6.GetValue().ToString(),
                //                        this.DAT02_W2AMDR.GetValue().ToString(),
                //                        this.DAT02_W2AMCR.GetValue().ToString(),
                //                        this.DAT02_W2CDFD.GetValue().ToString(),
                //                        this.DAT02_W2AMFD.GetValue().ToString(),
                //                        this.DAT02_W2RKAC.GetValue().ToString(),
                //                        this.DAT02_W2RKCU.GetValue().ToString(),
                //                        this.DAT02_W2WCJP.GetValue().ToString(),
                //                        this.DAT02_W2PRGB.GetValue().ToString(),
                //                        this.DAT02_W2HIGB.GetValue().ToString(),
                //                        this.DAT02_W2HISAB.GetValue().ToString(),
                //                        this.DAT02_W2GUBUN.GetValue().ToString(),
                //                        this.DAT02_W2TXAMT.GetValue().ToString(),
                //                        this.DAT02_W2TXVAT.GetValue().ToString(),
                //                        this.DAT02_W2HWAJU.GetValue().ToString()});
                //        #endregion
                //    }

                //    //대변
                //    #region Description : 대변 (부가세)
                //    iCnt = iCnt + 1;

                //    dt.Clear();

                //    this.DAT02_W2SSID.SetValue(sB2SSID);
                //    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                //    this.DAT02_W2DTMK.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString()); // 일자
                //    this.DAT02_W2NOSQ.SetValue("0");
                //    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                //    this.DAT02_W2IDJP.SetValue("3");
                //    this.DAT02_W2NOJP.SetValue("");
                //    this.DAT02_W2CDAC.SetValue("21103101"); // 매출부가세 본점
                //    this.DAT02_W2DTAC.SetValue("");
                //    this.DAT02_W2DTLI.SetValue("");
                //    this.DAT02_W2DPAC.SetValue(this.CBH01_FXDDPMK.GetValue().ToString()); //  귀속부서

                //    //관리항목 
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_AC_23N3M888", "21103101", "");
                //    dt = this.DbConnector.ExecuteDataTable();
                //    if (dt.Rows.Count > 0)
                //    {
                //        if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                //        {
                //            this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2));
                //        }
                //        else
                //        {
                //            this.DAT02_W2CDMI1.SetValue("");
                //        }
                //        if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                //        {
                //            this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2));
                //        }
                //        else
                //        {
                //            this.DAT02_W2CDMI2.SetValue("");
                //        }
                //        if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                //        {
                //            this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2));
                //        }
                //        else
                //        {
                //            this.DAT02_W2CDMI3.SetValue("");
                //        }
                //        if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                //        {
                //            this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2));
                //        }
                //        else
                //        {
                //            this.DAT02_W2CDMI4.SetValue("");
                //        }
                //        if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                //        {
                //            this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2));
                //        }
                //        else
                //        {
                //            this.DAT02_W2CDMI5.SetValue("");
                //        }
                //        if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                //        {
                //            this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2));
                //        }
                //        else
                //        {
                //            this.DAT02_W2CDMI6.SetValue("");
                //        }
                //    }

                //    // 관리항목 1 == 11 세무구분
                //    // 관리항목 2 == 01 거래처
                //    // 관리항목 3 == 15 거래일자
                //    // 관리항목 4 == 14 공급가액

                //    this.DAT02_W2VLMI1.SetValue(sVATGB);
                //    this.DAT02_W2VLMI2.SetValue(sCGVEND);
                //    this.DAT02_W2VLMI3.SetValue(this.TXT01_FXDGETDATE.GetValue().ToString());
                //    this.DAT02_W2VLMI4.SetValue(sCRAMT);
                //    this.DAT02_W2VLMI5.SetValue("");
                //    this.DAT02_W2VLMI6.SetValue("");

                //    sW2RKAC = "매출 부가세-" + sW2RKAC.ToString();

                //    this.DAT02_W2AMDR.SetValue("0");// 차변
                //    this.DAT02_W2AMCR.SetValue(sCRVAT); // 대변

                //    this.DAT02_W2CDFD.SetValue("");
                //    this.DAT02_W2AMFD.SetValue("0");
                //    this.DAT02_W2RKAC.SetValue(sW2RKAC);
                //    this.DAT02_W2RKCU.SetValue(sCGVENDNM);
                //    this.DAT02_W2WCJP.SetValue("");
                //    this.DAT02_W2PRGB.SetValue("");
                //    this.DAT02_W2HIGB.SetValue("A");
                //    this.DAT02_W2HISAB.SetValue(Employer.EmpNo);
                //    this.DAT02_W2GUBUN.SetValue("");
                //    this.DAT02_W2TXAMT.SetValue("0");
                //    this.DAT02_W2TXVAT.SetValue("0");
                //    this.DAT02_W2HWAJU.SetValue("");

                //    datas.Add(new object[] {this.DAT02_W2SSID.GetValue().ToString(),
                //                        this.DAT02_W2DPMK.GetValue().ToString(),
                //                        this.DAT02_W2DTMK.GetValue().ToString(),
                //                        this.DAT02_W2NOSQ.GetValue().ToString(),
                //                        this.DAT02_W2NOLN.GetValue().ToString(),
                //                        this.DAT02_W2IDJP.GetValue().ToString(),
                //                        this.DAT02_W2NOJP.GetValue().ToString(),
                //                        this.DAT02_W2CDAC.GetValue().ToString(),
                //                        this.DAT02_W2DTAC.GetValue().ToString(),
                //                        this.DAT02_W2DTLI.GetValue().ToString(),
                //                        this.DAT02_W2DPAC.GetValue().ToString(),
                //                        this.DAT02_W2CDMI1.GetValue().ToString(),
                //                        this.DAT02_W2VLMI1.GetValue().ToString(),
                //                        this.DAT02_W2CDMI2.GetValue().ToString(),
                //                        this.DAT02_W2VLMI2.GetValue().ToString(),
                //                        this.DAT02_W2CDMI3.GetValue().ToString(),
                //                        this.DAT02_W2VLMI3.GetValue().ToString(),
                //                        this.DAT02_W2CDMI4.GetValue().ToString(),
                //                        this.DAT02_W2VLMI4.GetValue().ToString(),
                //                        this.DAT02_W2CDMI5.GetValue().ToString(),
                //                        this.DAT02_W2VLMI5.GetValue().ToString(),
                //                        this.DAT02_W2CDMI6.GetValue().ToString(),
                //                        this.DAT02_W2VLMI6.GetValue().ToString(),
                //                        this.DAT02_W2AMDR.GetValue().ToString(),
                //                        this.DAT02_W2AMCR.GetValue().ToString(),
                //                        this.DAT02_W2CDFD.GetValue().ToString(),
                //                        this.DAT02_W2AMFD.GetValue().ToString(),
                //                        this.DAT02_W2RKAC.GetValue().ToString(),
                //                        this.DAT02_W2RKCU.GetValue().ToString(),
                //                        this.DAT02_W2WCJP.GetValue().ToString(),
                //                        this.DAT02_W2PRGB.GetValue().ToString(),
                //                        this.DAT02_W2HIGB.GetValue().ToString(),
                //                        this.DAT02_W2HISAB.GetValue().ToString(),
                //                        this.DAT02_W2GUBUN.GetValue().ToString(),
                //                        this.DAT02_W2TXAMT.GetValue().ToString(),
                //                        this.DAT02_W2TXVAT.GetValue().ToString(),
                //                        this.DAT02_W2HWAJU.GetValue().ToString()});
                //    #endregion

                //    //미승인 SP호출 파일 입력
                //    this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, Employer.EmpNo, "A",
                //                                                sW2DPMK, this.TXT01_FXDGETDATE.GetValue().ToString(), "", "",
                //                                                "", "", Employer.EmpNo);
                //    this.DbConnector.ExecuteTranQueryList();

                //    //전표 생성 함수 호출
                //    bJunPyoFlag = false;
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, "");
                //    string sOUTMSG2 = Convert.ToString(this.DbConnector.ExecuteScalar());
                //    if (sOUTMSG2.Substring(0, 2) == "ER")
                //    {
                //        this.ShowCustomMessage(sOUTMSG2, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //    }
                //    else
                //    {
                //        bJunPyoFlag = true;

                //        this.ShowMessage("TY_M_AC_25O8K620");

                //        this.DbConnector.CommandClear();
                //        this.DbConnector.Attach("TY_P_AC_29D5B004", sB2SSID);
                //        DataTable dtresult = this.DbConnector.ExecuteDataTable();
                //        if (dtresult.Rows.Count > 0)
                //        {
                //            if (dtresult.Rows[0]["AGRESULTCODE"].ToString().Trim() == "OK")
                //            {
                //                //전표번호 받아오기
                //                sJpno = dtresult.Rows[0]["AGOUT_PARAM"].ToString();
                //            }
                //        }
                //    }

                //    if (bJunPyoFlag)
                //    {
                //        //ACFIXADSF 전표번호 UPDATE
                //        if (dataFixU.Count > 0)
                //        {
                //            this.DbConnector.CommandClear();
                //            foreach (object[] data in dataFixU)
                //            {
                //                this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", "") + Set_Fill2(data[7].ToString()),
                //                                                            data[0].ToString(),
                //                                                            data[1].ToString(),
                //                                                            data[2].ToString(),
                //                                                            data[3].ToString(),
                //                                                            data[4].ToString(),
                //                                                            data[5].ToString(),
                //                                                            data[6].ToString()
                //                                                            );
                //            }
                //            this.DbConnector.ExecuteTranQueryList();

                //            //this.DbConnector.CommandClear();
                //            //this.DbConnector.Attach("TY_P_MR_3451M449", sJpno.Replace("-", ""), this.TXT01_FXDSAUP.GetValue().ToString());
                //            //this.DbConnector.ExecuteTranQueryList();
                //        }
                //    }


                //    #endregion

                //}
            }

            #endregion

            // 선급자재 내역
            this.DbConnector.Attach
                (
                "TY_P_MR_32J7A127",
                TXT01_FXDSAUP.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_32J7M130.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_MR_32J7M130.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSJPNO").ToString() == "")
                {
                    this.FPS91_TY_S_MR_32J7M130_Sheet1.Cells[i, 28].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                }

                if (this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSCHGUBUN").ToString() != "2")
                {
                    this.FPS91_TY_S_MR_32J7M130_Sheet1.Cells[i, 23].Locked = true;
                    this.FPS91_TY_S_MR_32J7M130_Sheet1.Cells[i, 24].Locked = true;
                    this.FPS91_TY_S_MR_32J7M130_Sheet1.Cells[i, 25].Locked = true;
                    this.FPS91_TY_S_MR_32J7M130_Sheet1.Cells[i, 26].Locked = true;
                    this.FPS91_TY_S_MR_32J7M130_Sheet1.Cells[i, 27].Locked = true;
                }

                if (this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSFIXNUM").ToString() != "" && this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSJPNO").ToString() != "")
                {
                    this.DbConnector.CommandClear(); //ACFIXASSETSF 고정자산 상세 화일에 전표번호 세팅
                    this.DbConnector.Attach("TY_P_MR_358BG631", this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSJPNO").ToString().Replace("-", ""), this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSFIXNUM").ToString());
                    this.DbConnector.ExecuteTranQueryList();
                }
            }

            // 버튼
            //this.BTN61_JASAN_CRE.Visible = false;
            //this.BTN61_JASAN_DEL.Visible = false;

            this.BTN61_JPNO_CRE.Visible = false;
            this.BTN61_JPNO_DEL.Visible = true;

            #endregion

        }
        #endregion

        #region Description : 전표삭제 버튼
        private void BTN61_JPNO_DEL_Click(object sender, EventArgs e)
        {
            #region Description : 자삭 삭제

            string sOUTMSG1 = string.Empty;
            string sCHUNGGB = string.Empty;

            //청구 구분 구하기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_348CP461", this.TXT01_FXDSAUP.GetValue().ToString().Trim());  //ACFIXADMF
            DataTable dt_cggubn = this.DbConnector.ExecuteDataTable();
            if (dt_cggubn.Rows.Count != 0)
            { sCHUNGGB = dt_cggubn.Rows[0]["FXDCHGUBUN"].ToString().Trim(); }

            // 청구구분 코드 1:매출청구, 3:청구없음 인 경우만 자산생성
            if (sCHUNGGB != "2")
            {

                this.DbConnector.CommandClear();

                // 합산 구분 필드 추가
                this.DbConnector.Attach("TY_P_MR_BC7GZ891", this.TXT01_FXDSAUP.GetValue().ToString(),
                                                            this.CBH01_FXDDPMK.GetValue().ToString(),
                                                            this.TXT01_FXDGETDATE.GetValue().ToString(),
                                                            "D",
                                                            TYUserInfo.EmpNo,
                                                            sOUTMSG1.ToString()
                                                            );

                // 원본
                //this.DbConnector.Attach("TY_P_MR_32Q3H205", this.TXT01_FXDSAUP.GetValue().ToString(),
                //                                            this.CBH01_FXDDPMK.GetValue().ToString(),
                //                                            this.TXT01_FXDGETDATE.GetValue().ToString(),
                //                                            "D",
                //                                            TYUserInfo.EmpNo,
                //                                            sOUTMSG1.ToString()
                //                                            );

                sOUTMSG1 = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUTMSG1.Substring(0, 2) != "OK")
                {
                    return;
                }
            }

            #endregion

            string sJpno = string.Empty;
            string sB2SSID = string.Empty;
            string sW2DPMK = string.Empty;

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            //작성사번 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_GB_24G9S659", Employer.EmpNo.ToString().Trim());  //INKIBNMF
            DataTable dt_sabun = this.DbConnector.ExecuteDataTable();
            if (dt_sabun.Rows.Count != 0)
            { sW2DPMK = dt_sabun.Rows[0]["KBBUSEO"].ToString().Trim(); }

            // 삭제 전표번호 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_3451T450", this.TXT01_FXDSAUP.GetValue().ToString().Trim());
            DataTable dt_dellist = this.DbConnector.ExecuteDataTable();
            if (dt_dellist.Rows.Count > 0)
            {
                for (int i = 0; i < dt_dellist.Rows.Count; i++)
                {
                    sJpno = dt_dellist.Rows[i]["FXSJPNO"].ToString();

                    //미승인전표 -> 임시파일 입력 (전표삭제)
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_29C7K957", sB2SSID, sJpno.Substring(0, 6), sJpno.Substring(6, 8), sJpno.Substring(14, 3));
                    //this.DbConnector.ExecuteTranQueryList();
                    ////미승인 SP호출 파일 입력
                    //this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, Employer.EmpNo, "D",
                                                     sW2DPMK, this.TXT01_FXDGETDATE.GetValue().ToString().Trim(), "", "",
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
                        //ACFIXADSF 전표번호 UPDATE
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_MR_9COFA620", "", this.TXT01_FXDSAUP.GetValue().ToString());
                        this.DbConnector.ExecuteTranQueryList();

                        this.ShowMessage("TY_M_AC_25O8K620");
                    }
                }

                DataTable dt = new DataTable();

                // 선급자재 내역
                this.DbConnector.Attach
                    (
                    "TY_P_MR_32J7A127",
                    TXT01_FXDSAUP.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_MR_32J7M130.SetValue(dt);

                for (int i = 0; i < this.FPS91_TY_S_MR_32J7M130.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSJPNO").ToString() == "")
                    {
                        this.FPS91_TY_S_MR_32J7M130_Sheet1.Cells[i, 28].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }

                    if (this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSFIXNUM").ToString() != "" && this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSJPNO").ToString() == "")
                    {
                        this.DbConnector.CommandClear();  //ACFIXASSETSF 고정자산 상세 화일에 전표번호 세팅
                        this.DbConnector.Attach("TY_P_MR_358BG631", "", this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSFIXNUM").ToString());
                        this.DbConnector.ExecuteTranQueryList();
                    }
                }

                // 버튼
                this.BTN61_BATCH_JASAN.Visible = false;
                this.BTN62_SUNGUB_CANCEL.Visible = true;

                this.BTN61_JPNO_CRE.Visible = true;
                this.BTN61_JPNO_DEL.Visible = false;
            }
        }
        #endregion


        #region Description : 전표생성 생성 ProcessCheck 이벤트
        private void BTN61_JPNO_CRE_Click_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            for (int i = 0; i < this.FPS91_TY_S_MR_32J7M130.ActiveSheet.RowCount; i++)
            {
                // 고정자산 마스터 존재 유무 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_32L6A151",
                    this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSNUM").ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_32P59187");
                    e.Successed = false;
                    return;
                }

                // 미수금청구가 아닌경우 체크 
                if (this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSCHGUBUN").ToString() != "2")
                {
                    // 설치위치
                    if (this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSSITE").ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_BCHF0919");
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_AC_25O8J618"))
            {
                e.Successed = false;
                return;
            }

            //작성부서 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_GB_24G9S659", Employer.EmpNo.ToString().Trim());  //INKIBNMF
            DataTable dt_sabun = this.DbConnector.ExecuteDataTable();
            if (dt_sabun.Rows.Count == 0)
            {
                this.ShowCustomMessage("사번이 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            //청구 부분 구하기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_348CP461", this.TXT01_FXDSAUP.GetValue().ToString().Trim());  //ACFIXADMF
            DataTable dt_cggubn = this.DbConnector.ExecuteDataTable();
            if (dt_cggubn.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

        }
        #endregion

        #region Description : 전표삭제 생성 ProcessCheck 이벤트
        private void BTN61_JPNO_DEL_Click_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sJpno = string.Empty;

            if (!this.ShowMessage("TY_M_AC_25O8K619"))
            {
                e.Successed = false;
                return;
            }

            // 삭제 전표번호 조회
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_3451T450", this.TXT01_FXDSAUP.GetValue().ToString().Trim());
            DataTable dt_dellist = this.DbConnector.ExecuteDataTable();
            if (dt_dellist.Rows.Count > 0)
            {
                for (int i = 0; i < dt_dellist.Rows.Count; i++)
                {
                    sJpno = dt_dellist.Rows[i]["FXSJPNO"].ToString();

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2B7BT153", sJpno.Substring(0, 6), sJpno.Substring(6, 8), sJpno.Substring(14, 3)); // ADSLGLF
                    DataTable dt_adsl = this.DbConnector.ExecuteDataTable();
                    if (dt_adsl.Rows.Count > 0)
                    {
                        if (dt_adsl.Rows[0]["B2NOJP"].ToString().Trim() != "")
                        {
                            this.ShowCustomMessage("승인된 전표이므로 삭제 할수 없음 (승인해제후 작업)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            e.Successed = false;
                            return;
                        };
                    }
                    else
                    {
                        this.ShowCustomMessage("미승인전표 미존재 처리 할수 없음(전표번호 확인)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }
            }
            else
            {
                this.ShowCustomMessage("처리할 자료가 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

        }
        #endregion


        #region Description : 선급자재 생성 ProcessCheck 이벤트
        private void BTN61_CREATE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sOLDFXDCGVEND = string.Empty;
            string sOLDBUSEO     = string.Empty;

            int i    = 0;

            fsFXDNUM = "";

            DataSet ds   = new DataSet();
            DataTable dt = new DataTable();

            // 선택 - (선급자재번호, PO번호, RR번호, PO금액, 거래처, 품목, RR금액, 청구화주, 입고전표번호)
            ds.Tables.Add(this.FPS91_TY_S_MR_32J7C129.GetDataSourceInclude(TSpread.TActionType.Select, "RRN1040", "FXDPONUM", "FXDRRNUM", "FXDPOAMOUNT", "FXDGETVEND", "FXDITEMCODE", "FXDITEMSEQ", "FXDRRAMOUNT", "FXDCGVEND", "FXDREPJPNO"));

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        sOLDFXDCGVEND = ds.Tables[0].Rows[i]["FXDCGVEND"].ToString();
                        sOLDBUSEO = ds.Tables[0].Rows[i]["RRN1040"].ToString();
                    }

                    if (sOLDFXDCGVEND != ds.Tables[0].Rows[i]["FXDCGVEND"].ToString())
                    {
                        this.ShowMessage("TY_M_MR_33B6E270");
                        e.Successed = false;
                        return;
                    }

                    if (sOLDBUSEO != ds.Tables[0].Rows[i]["RRN1040"].ToString())
                    {
                        this.ShowMessage("TY_M_MR_3575V627");
                        e.Successed = false;
                        return;
                    }
                }

                // 귀속부서
                this.CBH01_FXDDPMK.SetValue(sOLDBUSEO.ToString());

                // 선급자재 번호
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_32L36142",
                    this.CBH01_FXDDPMK.GetValue().ToString().Substring(0,1),
                    "S",
                    this.TXT01_FXDGETDATE.GetValue().ToString().Substring(0,6)
                );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    // 선급자재번호
                    fsFXDNUM = this.CBH01_FXDDPMK.GetValue().ToString().Substring(0, 1) + "S" + this.TXT01_FXDGETDATE.GetValue().ToString().Substring(0, 6) + Set_Fill4(dt.Rows[0]["FXDSEQ"].ToString());
                }
            }
            else
            {
                this.ShowMessage("TY_M_MR_3176V530");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 선급자재 취소 ProcessCheck 이벤트
        private void BTN61_CANCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            // 고정자산 마스터 존재 유무 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_32L6A151",
                this.TXT01_FXDSAUP.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_MR_32P59187");
                e.Successed = false;
                return;
            }

            // 선급자재내역 전표번호 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_32L6A152",
                this.TXT01_FXDSAUP.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_MR_32I5B104");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_AC_2CDB0166"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion


        #region Description : 선급자재 예산 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            double dFXYSAMOUNT = 0;
            //double dOLD_AMOUNT = 0;
            //double dNEW_AMOUNT = 0;
            //double dJUN_AMOUNT = 0;

            DataTable dt = new DataTable();

            //// 예산별 입력된 금액 가져오기
            //this.DbConnector.Attach
            //    (
            //    "TY_P_MR_33CBQ276",
            //    fsJASANNUM.ToString(),
            //    fsPONUM.ToString(),
            //    fsRRNUM.ToString()
            //    );

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    dJUN_AMOUNT = double.Parse(dt.Rows[0]["AMOUNTHAP"].ToString());
            //}

            string sFXYSVATGB = string.Empty;

            DataSet ds = new DataSet();

            // 등록 - (선급자재번호, PO번호, RR번호, 계정, 순번, 고정자산번호, 금액, 부가세코드, 청구업체, 자산분류코드)
            ds.Tables.Add(this.FPS91_TY_S_MR_32J7S131.GetDataSourceInclude(TSpread.TActionType.New, "FXYSNUM", "FXYSPONUM", "FXYSRRNUM", "FXYSGETVEND", "FXYSITEMCD", "FXYSCDAC", "FXYSYCNUM", "FXYSJASAN", "FXYSAMOUNT", "FXYSVATGB", "FXYSCGVEND", "FXYSJASANCD", "FXCOMBINE"));
            ds.Tables.Add(this.FPS91_TY_S_MR_32J7S131.GetDataSourceInclude(TSpread.TActionType.Update, "FXYSNUM", "FXYSPONUM", "FXYSRRNUM", "FXYSGETVEND", "FXYSITEMCD", "FXYSCDAC", "FXYSYCNUM", "FXYSJASAN", "FXYSAMOUNT", "FXYSVATGB", "FXYSCGVEND", "FXYSJASANCD", "FXCOMBINE"));
            //ds.Tables.Add(this.FPS91_TY_S_MR_32J7S131.GetDataSourceInclude(TSpread.TActionType.Remove, "FXYSNUM", "FXYSPONUM", "FXYSRRNUM", "FXYSCGVEND", "FXYSCDAC", "FXYSYCNUM", "FXYSAMOUNT", "FXYSVATGB"));

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    // 부가세 코드 및 청구거래처 미사용
                    //if (i == 0)
                    //{
                    //    sFXYSVATGB = ds.Tables[0].Rows[i]["FXYSVATGB"].ToString();
                    //}

                    //if (ds.Tables[0].Rows[i]["FXYSCDAC"].ToString() == "11100888")
                    //{
                    //    if (ds.Tables[0].Rows[i]["FXYSVATGB"].ToString() == "")
                    //    {
                    //        this.ShowMessage("TY_M_MR_2C54B941");
                    //        e.Successed = false;
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        if (sFXYSVATGB.ToString() != ds.Tables[0].Rows[i]["FXYSVATGB"].ToString())
                    //        {
                    //            this.ShowMessage("TY_M_MR_34568457");
                    //            e.Successed = false;
                    //            return;
                    //        }
                    //    }
                    //}

                    if (Get_Numeric(ds.Tables[0].Rows[i]["FXYSAMOUNT"].ToString()) == "0")
                    {
                        this.ShowMessage("TY_M_MR_2CA6N029");
                        e.Successed = false;
                        return;
                    }

                    if (ds.Tables[0].Rows[i]["FXYSJASAN"].ToString() != "")
                    {
                        //// 자산수리시
                        //// 자산취득 했을때의 계정과목과 입력한 계정과목이 일치해야 함.
                        //this.DbConnector.CommandClear();
                        //this.DbConnector.Attach
                        //    (
                        //    "TY_P_MR_37266898",
                        //    ds.Tables[0].Rows[i]["FXYSJASAN"].ToString().Substring(0, 8),
                        //    ds.Tables[0].Rows[i]["FXYSJASAN"].ToString().Substring(0, 8)
                        //    );

                        //dt = this.DbConnector.ExecuteDataTable();

                        //if (dt.Rows.Count > 0)
                        //{
                        //    if (ds.Tables[0].Rows[i]["FXYSCDAC"].ToString() != dt.Rows[0]["CDAC"].ToString())
                        //    {
                        //        this.ShowMessage("TY_M_MR_37260900");
                        //        e.Successed = false;
                        //        return;
                        //    }
                        //}
                    }

                    // 선급자재 내역 존재 유무 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_35O2W736",
                        ds.Tables[0].Rows[i]["FXYSNUM"].ToString(),
                        ds.Tables[0].Rows[i]["FXYSCDAC"].ToString(),
                        ds.Tables[0].Rows[i]["FXYSYCNUM"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_35O2X737");
                        e.Successed = false;
                        return;
                    }

                    // 고정자산 마스터 존재 유무 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_32L6A151",
                        ds.Tables[0].Rows[i]["FXYSNUM"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_32P59187");
                        e.Successed = false;
                        return;
                    }

                    // 선급자재내역 전표번호 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_32L6A152",
                        ds.Tables[0].Rows[i]["FXYSNUM"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_32I5B104");
                        e.Successed = false;
                        return;
                    }

                    if (ds.Tables[0].Rows[i]["FXYSCDAC"].ToString().Substring(0, 5) == "12200" && ds.Tables[0].Rows[i]["FXYSJASANCD"].ToString() != "")
                    {
                        if (ds.Tables[0].Rows[i]["FXYSCDAC"].ToString().Substring(5, 1) != ds.Tables[0].Rows[i]["FXYSJASANCD"].ToString().Substring(0, 1))
                        {
                            this.ShowMessage("TY_M_MR_66NGQ383");
                            e.Successed = false;
                            return;
                        }
                    }

                    //// 등록 금액
                    //dNEW_AMOUNT = dNEW_AMOUNT + double.Parse(string.Format("{0:#,###}", ds.Tables[0].Rows[i]["FXYSAMOUNT"].ToString()));
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    // 부가세 코드 및 청구거래처 미사용
                    //if (ds.Tables[1].Rows[i]["FXYSCDAC"].ToString() == "11100888" && ds.Tables[1].Rows[i]["FXYSVATGB"].ToString() == "")
                    //{
                    //    this.ShowMessage("TY_M_MR_2C54B941");
                    //    e.Successed = false;
                    //    return;
                    //}

                    //// 원래금액 가져오기
                    //this.DbConnector.CommandClear();
                    //this.DbConnector.Attach
                    //    (
                    //    "TY_P_MR_33CBI275",
                    //    ds.Tables[1].Rows[i]["FXYSNUM"].ToString(),
                    //    ds.Tables[1].Rows[i]["FXYSPONUM"].ToString(),
                    //    ds.Tables[1].Rows[i]["FXYSRRNUM"].ToString(),
                    //    ds.Tables[1].Rows[i]["FXYSCDAC"].ToString(),
                    //    ds.Tables[1].Rows[i]["FXYSYCNUM"].ToString()
                    //    );

                    //dt = this.DbConnector.ExecuteDataTable();

                    //if (dt.Rows.Count > 0)
                    //{
                    //    dOLD_AMOUNT = dOLD_AMOUNT + double.Parse(string.Format("{0:#,###}", ds.Tables[1].Rows[i]["FXYSAMOUNT"].ToString()));
                    //}

                    // 선급자재 내역 존재 유무 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_35O2W736",
                        ds.Tables[1].Rows[i]["FXYSNUM"].ToString(),
                        ds.Tables[1].Rows[i]["FXYSCDAC"].ToString(),
                        ds.Tables[1].Rows[i]["FXYSYCNUM"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_35O2X737");
                        e.Successed = false;
                        return;
                    }

                    // 고정자산 마스터 존재 유무 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_32L6A151",
                        ds.Tables[1].Rows[i]["FXYSNUM"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_32P59187");
                        e.Successed = false;
                        return;
                    }

                    // 선급자재내역 전표번호 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_32L6A152",
                        ds.Tables[1].Rows[i]["FXYSNUM"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_32I5B104");
                        e.Successed = false;
                        return;
                    }

                    //// 수정 금액
                    //dNEW_AMOUNT = dNEW_AMOUNT + double.Parse(string.Format("{0:#,###}", ds.Tables[1].Rows[i]["FXYSAMOUNT"].ToString()));
                }
            }

            //if (ds.Tables[2].Rows.Count > 0)
            //{
            //    for (i = 0; i < ds.Tables[2].Rows.Count; i++)
            //    {
            //        // 원래금액 가져오기
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach
            //            (
            //            "TY_P_MR_33CBI275",
            //            ds.Tables[2].Rows[i]["FXYSNUM"].ToString(),
            //            ds.Tables[2].Rows[i]["FXYSPONUM"].ToString(),
            //            ds.Tables[2].Rows[i]["FXYSRRNUM"].ToString(),
            //            ds.Tables[2].Rows[i]["FXYSCDAC"].ToString(),
            //            ds.Tables[2].Rows[i]["FXYSYCNUM"].ToString()
            //            );

            //        dt = this.DbConnector.ExecuteDataTable();

            //        if (dt.Rows.Count > 0)
            //        {
            //            dOLD_AMOUNT = dOLD_AMOUNT + double.Parse(string.Format("{0:#,###}", ds.Tables[2].Rows[i]["FXYSAMOUNT"].ToString()));
            //        }

            //        // 고정자산 마스터 존재 유무 체크
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach
            //            (
            //            "TY_P_MR_32L6A151",
            //            ds.Tables[2].Rows[i]["FXYSNUM"].ToString()
            //            );

            //        dt = this.DbConnector.ExecuteDataTable();

            //        if (dt.Rows.Count > 0)
            //        {
            //            this.ShowMessage("TY_M_MR_32P59187");
            //            e.Successed = false;
            //            return;
            //        }

            //        // 선급자재내역 전표번호 체크
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach
            //            (
            //            "TY_P_MR_32L6A152",
            //            ds.Tables[2].Rows[i]["FXYSNUM"].ToString()
            //            );

            //        dt = this.DbConnector.ExecuteDataTable();

            //        if (dt.Rows.Count > 0)
            //        {
            //            this.ShowMessage("TY_M_MR_32I5B104");
            //            e.Successed = false;
            //            return;
            //        }
            //    }
            //}

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }
            else
            {
                for (int j = 0; j < this.FPS91_TY_S_MR_32J7S131.ActiveSheet.RowCount; j++)
                {
                    dFXYSAMOUNT = dFXYSAMOUNT + double.Parse(Get_Numeric(this.FPS91_TY_S_MR_32J7S131.GetValue(j, "FXYSAMOUNT").ToString()));
                }

                //dFXYSAMOUNT = dJUN_AMOUNT + dNEW_AMOUNT - dOLD_AMOUNT;

                if (dFXYSAMOUNT != double.Parse(this.TXT01_GDAESANGHAP.GetValue().ToString()))
                {
                    this.ShowMessage("TY_M_MR_33C15277");
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

        #region Description : 선급자재 예산 취소 ProcessCheck 이벤트
        private void BTN61_SUNGUB_CANCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataTable dt = new DataTable();

            DataSet ds = new DataSet();

            // 삭제 - (선급자재번호, PO번호, RR번호, 청구업체, 계정, 순번, 금액, 부가세코드)
            ds.Tables.Add(this.FPS91_TY_S_MR_32J7S131.GetDataSourceInclude(TSpread.TActionType.Remove, "FXYSNUM", "FXYSPONUM", "FXYSRRNUM", "FXYSGETVEND", "FXYSITEMCD", "FXYSCGVEND", "FXYSCDAC", "FXYSYCNUM", "FXYSAMOUNT", "FXYSVATGB", "FXYSJASAN", "FXCOMBINE"));

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    // 선급자재 내역 존재 유무 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_35O2W736",
                        ds.Tables[0].Rows[i]["FXYSNUM"].ToString(),
                        ds.Tables[0].Rows[i]["FXYSCDAC"].ToString(),
                        ds.Tables[0].Rows[i]["FXYSYCNUM"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_35O2X737");
                        e.Successed = false;
                        return;
                    }

                    // 고정자산 마스터 존재 유무 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_32L6A151",
                        ds.Tables[0].Rows[i]["FXYSNUM"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_32P59187");
                        e.Successed = false;
                        return;
                    }

                    // 선급자재내역 전표번호 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_32L6A152",
                        ds.Tables[0].Rows[i]["FXYSNUM"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_32I5B104");
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_35O22734");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_MR_35O21733"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion


        #region Description : 예산별 생성 ProcessCheck 이벤트
        private void BTN61_SAV_JASAN_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            double dFXDRRAMOUNT = 0;
            double dFXYSAMOUNT  = 0;

            DataTable dt = new DataTable();

            // 선급자재 마스터 입고 총금액 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_33C64283",
                this.TXT01_FXDSAUP.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dFXDRRAMOUNT = double.Parse(string.Format("{0:#,###}", dt.Rows[0]["FXDRRAMOUNT"].ToString()));
            }

            // 선급자재 예산 총금액 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_33C68284",
                this.TXT01_FXDSAUP.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dFXYSAMOUNT = double.Parse(string.Format("{0:#,###}", dt.Rows[0]["FXYSAMOUNT"].ToString()));
            }

            if (dFXDRRAMOUNT != dFXYSAMOUNT)
            {
                this.ShowMessage("TY_M_MR_33C61285");
                e.Successed = false;
                return;
            }

            // 선급자재 내역 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_33C64286",
                this.TXT01_FXDSAUP.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_MR_32P4Y186");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_MR_33C6A287"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion


        #region Description : 예산별 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_JASAN_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            // 등록 - (선급자재번호, PO번호, RR번호, 청구업체, 계정, 순번, 자산번호, 금액, 부가세코드)
            ds.Tables.Add(this.FPS91_TY_S_MR_32J7M130.GetDataSourceInclude(TSpread.TActionType.Update, "FXSNUM", "FXSCDAC", "A1NMAC", "FXSYCNUM", "FXSJASAN", "FXSAMOUNT", "FXSNAME", "FXSSTAND", "FXSUNIT", "FXSQTY", "FXSBUSEO", "FXSSITE", "FXSBUYCOM", "FXSCLASS", "FXSCRATEGB", "FXSFIXNUM", "FXSYSSEQ", "FXSTASKAMT", "FXSCOMBINE", "FXSCGVEND", "FXSVATGB", "FXSCHGUBUN"));

            DataTable dt = new DataTable();

            // 수정
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["FXSCDAC"].ToString().Substring(0, 5) == "12200" && ds.Tables[0].Rows[i]["FXSCLASS"].ToString() != "")
                    {
                        if (ds.Tables[0].Rows[i]["FXSCDAC"].ToString().Substring(5, 1) != ds.Tables[0].Rows[i]["FXSCLASS"].ToString().Substring(0, 1))
                        {
                            this.ShowMessage("TY_M_MR_66NGQ383");
                            e.Successed = false;
                            return;
                        }
                    }

                    // 금액
                    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["FXSAMOUNT"].ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_MR_32Q1D201");
                        e.Successed = false;
                        return;
                    }

                    // 자산명
                    if (ds.Tables[0].Rows[i]["FXSNAME"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_33D2Y304");
                        e.Successed = false;
                        return;
                    }

                    // 규격
                    if (ds.Tables[0].Rows[i]["FXSSTAND"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_2CA6M026");
                        e.Successed = false;
                        return;
                    }

                    // 단위
                    if (ds.Tables[0].Rows[i]["FXSUNIT"].ToString() == "")
                    {
                        this.ShowMessage("TY_M_MR_33D2Z305");
                        e.Successed = false;
                        return;
                    }

                    // 수량
                    if (double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["FXSQTY"].ToString())) == 0)
                    {
                        this.ShowMessage("TY_M_MR_2CA6M027");
                        e.Successed = false;
                        return;
                    }

                    // 미수금청구가 아닌경우 체크 
                    if (ds.Tables[0].Rows[i]["FXSCHGUBUN"].ToString() != "2")
                    {
                        // 설치위치
                        if (ds.Tables[0].Rows[i]["FXSSITE"].ToString() == "")
                        {
                            this.ShowMessage("TY_M_MR_BCHF0919");
                            e.Successed = false;
                            return;
                        }
                        // 구입처
                        //if (ds.Tables[0].Rows[i]["FXSBUYCOM"].ToString() == "")
                        //{
                        //    this.ShowMessage("TY_M_MR_32QC2200");
                        //    e.Successed = false;
                        //    return;
                        //}
                        // 분류코드
                        if (ds.Tables[0].Rows[i]["FXSCLASS"].ToString() == "")
                        {
                            this.ShowMessage("TY_M_MR_BCHF0918");
                            e.Successed = false;
                            return;
                        }
                    }

                    // 자산분류코드
                    //if (ds.Tables[0].Rows[i]["FXSCLASS"].ToString() == "")
                    //{
                    //    this.ShowMessage("TY_M_MR_2CA6K023");
                    //    e.Successed = false;
                    //    return;
                    //}

                    // 고정자산 마스터 존재 유무 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_32L6A151",
                        ds.Tables[0].Rows[i]["FXSNUM"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_32P59187");
                        e.Successed = false;
                        return;
                    }

                    // 
                    if (ds.Tables[0].Rows[i]["FXSCHGUBUN"].ToString() != "2")
                    {
                        ds.Tables[0].Rows[i]["FXSCGVEND"] = "";
                        ds.Tables[0].Rows[i]["FXSVATGB"] = "";
                    }
                }
            }

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_MR_33D1Y299"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 예산별 취소 ProcessCheck 이벤트
        private void BTN62_SUNGUB_CANCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_MR_32J7M130.GetDataSourceInclude(TSpread.TActionType.Remove, "FXSNUM", "FXSCDAC", "A1NMAC", "FXSYCNUM", "FXSAMOUNT", "FXSNAME", "FXSSTAND", "FXSUNIT", "FXSQTY", "FXSBUYCOM", "FXSCLASS", "FXSCRATEGB", "FXSFIXNUM"));

            DataTable dt = new DataTable();

            // 삭제
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    // 고정자산 마스터 존재 유무 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_32L6A151",
                        ds.Tables[0].Rows[i]["FXSNUM"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_MR_32P59187");
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_35O22734");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_MR_35O21733"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion


        #region Description : 전표생성 ProcessCheck 이벤트
        private void BTN61_JPNO_CRE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            for (int i = 0; i < this.FPS91_TY_S_MR_32J7M130.ActiveSheet.RowCount; i++)
            {
                // 고정자산 마스터 존재 유무 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_32L6A151",
                    this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSNUM").ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_32P59187");
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        //#region Description : 전표삭제 ProcessCheck 이벤트
        //private void BTN61_JPNO_DEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        //{
        //    DataTable dt = new DataTable();

        //    for (int i = 0; i < this.FPS91_TY_S_MR_32J7M130.ActiveSheet.RowCount; i++)
        //    {
        //        if (this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSJPNO").ToString() != "")
        //        {
        //            this.ShowMessage("TY_M_MR_3174H522");
        //            e.Successed = false;
        //            return;
        //        }
        //    }
        //}
        //#endregion


        #region Description : 취득일자 이벤트
        private void TXT01_FXDGETDATE_TextChanged(object sender, EventArgs e)
        {
            this.CBH01_FXDDPMK.DummyValue = this.TXT01_FXDGETDATE.GetValue().ToString();
        }
        #endregion

        #region Description : 선급자재 스프레드 이벤트
        private void FPS91_TY_S_MR_33860261_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            tabControl1_Enable("MANAGEMENT");

            // 귀속부서
            this.CBH01_FXDDPMK.SetValue(this.FPS91_TY_S_MR_33860261.GetValue("FXDDPMK").ToString());

            // 선급자재 번호
            this.TXT01_FXDSAUP.SetValue(this.FPS91_TY_S_MR_33860261.GetValue("FXDNUM").ToString());
            // 취급일자
            this.TXT01_FXDGETDATE.SetValue(this.FPS91_TY_S_MR_33860261.GetValue("FXDGETDATE").ToString());

            // 선급자재 생성 조회
            UP_SEL_ACFIXADMF(this.FPS91_TY_S_MR_33860261.GetValue("FXDNUM").ToString());

            DataTable NewDt = new DataTable();
            NewDt.Clear();

            // 선급자재 예산
            this.FPS91_TY_S_MR_32J7S131.SetValue(NewDt);
            // 선급자재 내역
            this.FPS91_TY_S_MR_32J7M130.SetValue(NewDt);


            DataTable dt = new DataTable();

            // 선급자재번호가 존재하면 버튼 안 보이게 함
            this.DbConnector.Attach
                (
                "TY_P_MR_32J7A127",
                TXT01_FXDSAUP.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_32J7M130.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["FXSFIXNUM"].ToString() != "")
                {
                    this.BTN61_CANCEL.Visible        = false;
                    this.BTN61_SAV_JASAN.Visible     = false;
                    this.BTN61_BATCH.Visible         = false;
                    this.BTN61_SUNGUB_CANCEL.Visible = false;
                }
                this.CBO01_PRM6020.SetValue(dt.Rows[0]["FXSCHGUBUN"].ToString());
            }

            // 선급자재 예산내역 존재체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_35S9S746",
                TXT01_FXDSAUP.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.BTN61_CANCEL.Visible = false;
                
            }
            else
            {
                this.BTN61_CANCEL.Visible = true;
            }
        }
        #endregion

        #region Description : 선급자재 생성 조회
        private void UP_SEL_ACFIXADMF(string sFXDNUM)
        {
            this.BTN61_SAV_JASAN.Visible = true;

            // 더블클릭시 내용
            this.BTN61_CREATE.Visible = false;
            this.BTN61_CANCEL.Visible = true;

            this.FPS91_TY_S_MR_32J7C129.Visible = false;
            this.FPS91_TY_S_MR_3381M258.Visible = true;

            DataTable dt = new DataTable();

            // 선급자재 마스터
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_32J7A126",
                sFXDNUM.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_3381M258.SetValue(dt);
        }
        #endregion

        #region Description : 자산번호 텍스트박스 이벤트
        private void TXT01_FXYEAR_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                BTN61_FXYEAR_Click(null, null);
            }
        }
        #endregion

        #region Description : 자산번호 버튼
        private void BTN61_FXYEAR_Click(object sender, EventArgs e)
        {
            TYMRGB010S popup = new TYMRGB010S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_FXYEAR.SetValue(popup.fsFXSYEAR + popup.fsSAUPBU + popup.fsFXSSEQ + popup.fsFXSSUBNUM); // 자산년도
                this.TXT01_FXYEARNM.SetValue(popup.fsFXSNAME);                                    // 자산명
                this.TXT01_FXDSAUP.SetValue(popup.fsFXSTOCKNUM);                                  // 선급자재번호

                this.SetFocus(TXT01_FXDSAUP);
            }
        }
        #endregion

        #region Description : 선급자재 텍스트박스 이벤트
        private void TXT01_FXDSAUP_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.TXT01_FXDSAUP.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    BTN61_FXDSAUP_Click(null, null);
                }
            }
        }
        #endregion

        #region Description : 선급자재 버튼
        private void BTN61_FXDSAUP_Click(object sender, EventArgs e)
        {
            TYMRGB015S popup = new TYMRGB015S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_FXDSAUP.SetValue(popup.fsFXDNUM);    // 선급자재번호
                this.TXT01_FXDSAUPNM.SetValue(popup.fsFXSNAME); // 자산명

                this.CBO01_GGUBUN.SetValue("C");

                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region Description : 선급자재 예산 코드헬프
        private void FPS91_TY_S_MR_32J7S131_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == System.Windows.Forms.Keys.F1) && this.FPS91_TY_S_MR_32J7S131.ActiveSheet.ActiveColumnIndex == 7)
            {
                if (this.CBH01_FXDDPMK.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_MR_32P4P185");
                    return;
                }

                DataTable dt = new DataTable();

                // 선급자재 번호
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_33C38281",
                    //fsJASANNUM.ToString()
                    this.FPS91_TY_S_MR_32J7S131.GetValue(this.FPS91_TY_S_MR_32J7S131.ActiveSheet.ActiveRowIndex, "FXYSNUM").ToString()
                );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_32P4Y186");
                    return;
                }

                string sYYMM = string.Empty;

                if (this.TXT01_FXDGETDATE.GetValue().ToString() != "" && this.TXT01_FXDGETDATE.GetValue().ToString().Length > 4)
                {
                    sYYMM = this.TXT01_FXDGETDATE.GetValue().ToString().Substring(0, 4);
                }
                else
                {
                    sYYMM = DateTime.Now.ToString("yyyyMMdd").Substring(0, 4);
                }

                this.FPS91_TY_S_MR_32J7S131.SetValue(this.FPS91_TY_S_MR_32J7S131.ActiveSheet.ActiveRowIndex, "FXYSVATGB", "");

                TYMRGB016S popup = new TYMRGB016S(sYYMM.ToString(), this.CBH01_FXDDPMK.GetValue().ToString(), "");

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.FPS91_TY_S_MR_32J7S131.SetValue(this.FPS91_TY_S_MR_32J7S131.ActiveSheet.ActiveRowIndex, "FXYSCDAC",  popup.fsCDAC);
                    this.FPS91_TY_S_MR_32J7S131.SetValue(this.FPS91_TY_S_MR_32J7S131.ActiveSheet.ActiveRowIndex, "A1NMAC",    popup.fsCDACNM);
                    this.FPS91_TY_S_MR_32J7S131.SetValue(this.FPS91_TY_S_MR_32J7S131.ActiveSheet.ActiveRowIndex, "FXYSYCNUM", popup.fsSEQ);

                    if (popup.fsCDAC == "11100888")
                    {
                        // 기타미수금일 경우에만 청구업체 옴
                        this.FPS91_TY_S_MR_32J7S131.SetValue(this.FPS91_TY_S_MR_32J7S131.ActiveSheet.ActiveRowIndex, "FXYSCGVEND", fsCGVEND.ToString());
                        this.FPS91_TY_S_MR_32J7S131.SetValue(this.FPS91_TY_S_MR_32J7S131.ActiveSheet.ActiveRowIndex, "FXYSCGVENDNM", "");

                        // 선급자재 번호
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_37451957",
                            fsRRNUM.ToString().Substring(0, 1),
                            fsRRNUM.ToString().Substring(1, 1),
                            fsRRNUM.ToString().Substring(2, 6),
                            fsRRNUM.ToString().Substring(8, 4)
                            );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["RRM6020"].ToString() == "2")
                            {
                                // 기타미수금일 경우에만 청구업체 옴
                                this.FPS91_TY_S_MR_32J7S131.SetValue(this.FPS91_TY_S_MR_32J7S131.ActiveSheet.ActiveRowIndex, "FXYSVATGB", "69");
                            }
                        }
                    }
                    else
                    {
                        this.FPS91_TY_S_MR_32J7S131.SetValue(this.FPS91_TY_S_MR_32J7S131.ActiveSheet.ActiveRowIndex, "FXYSCGVEND", "");
                        this.FPS91_TY_S_MR_32J7S131.SetValue(this.FPS91_TY_S_MR_32J7S131.ActiveSheet.ActiveRowIndex, "FXYSCGVENDNM", "");
                    }

                }
            }
            else if ((e.KeyCode == System.Windows.Forms.Keys.F1) && this.FPS91_TY_S_MR_32J7S131.ActiveSheet.ActiveColumnIndex == 10 &&
                      this.FPS91_TY_S_MR_32J7S131.ActiveSheet.RowHeader.Cells[this.FPS91_TY_S_MR_32J7S131.ActiveSheet.ActiveRowIndex, 0].Text == "N")
            {
                DataTable dt = new DataTable();

                // 선급자재 번호
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_33C38281",
                    //fsJASANNUM.ToString()
                    this.FPS91_TY_S_MR_32J7S131.GetValue(this.FPS91_TY_S_MR_32J7S131.ActiveSheet.ActiveRowIndex, "FXYSNUM").ToString()
                );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_32P4Y186");
                    return;
                }

                TYMRGB010S popup = new TYMRGB010S();

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    // 자산번호
                    this.FPS91_TY_S_MR_32J7S131.SetValue(this.FPS91_TY_S_MR_32J7S131.ActiveSheet.ActiveRowIndex, "FXYSJASAN", popup.fsFXSYEAR + popup.fsFXSSEQ + popup.fsFXSSUBNUM);
                    // 자산명
                    this.FPS91_TY_S_MR_32J7S131.SetValue(this.FPS91_TY_S_MR_32J7S131.ActiveSheet.ActiveRowIndex, "FXSNAME", popup.fsFXSNAME);
                    // 고정자산분류코드
                    this.FPS91_TY_S_MR_32J7S131.SetValue(this.FPS91_TY_S_MR_32J7S131.ActiveSheet.ActiveRowIndex, "FXYSJASANCD", popup.fsJASAN + popup.fsSAUPBU + popup.fsLARGE + popup.fsMIDDLE + popup.fsSMALL);
                }
            }
        }
        #endregion

        #region Description : 선급자재 생성 스프레드
        private void FPS91_TY_S_MR_3381M258_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsJASANNUM = "";
            fsPONUM    = "";
            fsRRNUM    = "";
            fsVEND     = "";
            fsITEMCODE = "";
            fsCGVEND   = "";

            DataTable dt = new DataTable();

            // 선급자재 예산내역
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_33B4S269",
                this.FPS91_TY_S_MR_3381M258.GetValue("FXDNUM").ToString(),
                this.FPS91_TY_S_MR_3381M258.GetValue("FXDPONUM").ToString(),
                this.FPS91_TY_S_MR_3381M258.GetValue("FXDRRNUM").ToString(),
                this.FPS91_TY_S_MR_3381M258.GetValue("FXDGETVEND").ToString(),
                this.FPS91_TY_S_MR_3381M258.GetValue("FXDITEMCODE").ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_32J7S131.SetValue(dt);

            fsJASANNUM = this.FPS91_TY_S_MR_3381M258.GetValue("FXDNUM").ToString();
            fsPONUM    = this.FPS91_TY_S_MR_3381M258.GetValue("FXDPONUM").ToString();
            fsRRNUM    = this.FPS91_TY_S_MR_3381M258.GetValue("FXDRRNUM").ToString();
            fsVEND     = this.FPS91_TY_S_MR_3381M258.GetValue("FXDGETVEND").ToString();
            fsITEMCODE = this.FPS91_TY_S_MR_3381M258.GetValue("FXDITEMCODE").ToString();
            fsCGVEND   = this.FPS91_TY_S_MR_3381M258.GetValue("FXDCGVEND").ToString();

            this.TXT01_GDAESANGHAP.SetValue(this.FPS91_TY_S_MR_3381M258.GetValue("FXDRRAMOUNT").ToString());

            if (dt.Rows.Count <= 0)
            {
                string sFXYSAMOUNT = string.Empty;

                double dAMOUNT = 0;
                double dGDAESANGHAP = 0;

                this.FPS91_TY_S_MR_32J7S131_Sheet1.AddRows(0, 1);

                this.FPS91_TY_S_MR_32J7S131.ActiveSheet.RowHeader.Cells[0, 0].Text = "N";


                this.FPS91_TY_S_MR_32J7S131.SetValue(0, "FXYSNUM",     fsJASANNUM);
                this.FPS91_TY_S_MR_32J7S131.SetValue(0, "FXYSPONUM",   fsPONUM);
                this.FPS91_TY_S_MR_32J7S131.SetValue(0, "FXYSRRNUM",   fsRRNUM);
                this.FPS91_TY_S_MR_32J7S131.SetValue(0, "FXYSGETVEND", fsVEND);
                this.FPS91_TY_S_MR_32J7S131.SetValue(0, "FXYSITEMCD",  fsITEMCODE);
                this.FPS91_TY_S_MR_32J7S131.SetValue(0, "FXYSCGVEND",  fsCGVEND);

                if (this.FPS91_TY_S_MR_32J7S131.ActiveSheet.RowCount > 1)
                {
                    for (int i = 1; i < this.FPS91_TY_S_MR_32J7S131.ActiveSheet.RowCount; i++)
                    {
                        dAMOUNT = dAMOUNT + double.Parse(Get_Numeric(this.FPS91_TY_S_MR_32J7S131.GetValue(i, "FXYSAMOUNT").ToString()));
                    }

                    dGDAESANGHAP = double.Parse(this.TXT01_GDAESANGHAP.GetValue().ToString());

                    sFXYSAMOUNT = string.Format("{0:#,###}", dGDAESANGHAP - dAMOUNT);

                    this.FPS91_TY_S_MR_32J7S131.SetValue(0, "FXYSAMOUNT", sFXYSAMOUNT);
                }
                else if (this.FPS91_TY_S_MR_32J7S131.ActiveSheet.RowCount == 1)
                {
                    sFXYSAMOUNT = string.Format("{0:#,###}", Get_Numeric(this.TXT01_GDAESANGHAP.GetValue().ToString()));

                    this.FPS91_TY_S_MR_32J7S131.SetValue(0, "FXYSAMOUNT", sFXYSAMOUNT);
                }
            }


            // 선급자재번호가 존재하면 버튼 안 보이게 함
            this.DbConnector.Attach
                (
                "TY_P_MR_32J7A127",
                TXT01_FXDSAUP.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_MR_32J7M130.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["FXSFIXNUM"].ToString() != "")
                {
                    this.BTN61_CANCEL.Visible        = false;
                    this.BTN61_SAV_JASAN.Visible     = false;
                    this.BTN61_BATCH.Visible         = false;
                    this.BTN61_SUNGUB_CANCEL.Visible = false;
                }
                else
                {
                    this.BTN61_SAV_JASAN.Visible     = true;
                    this.BTN61_BATCH.Visible         = true;
                    this.BTN61_SUNGUB_CANCEL.Visible = true;
                }
            }
            else
            {
                this.BTN61_SAV_JASAN.Visible     = true;
                this.BTN61_BATCH.Visible         = true;
                this.BTN61_SUNGUB_CANCEL.Visible = true;
            }

            // 선급자재 예산내역 존재체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_35S9S746",
                TXT01_FXDSAUP.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.BTN61_CANCEL.Visible = false;
            }
            else
            {
                this.BTN61_CANCEL.Visible = true;
            }
        }
        #endregion

        #region Description : 선급자재 예산 레코드 추가 이벤트
        private void FPS91_TY_S_MR_32J7S131_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            string sFXYSAMOUNT  = string.Empty;

            double dAMOUNT      = 0;
            double dGDAESANGHAP = 0;

            this.FPS91_TY_S_MR_32J7S131.SetValue(e.RowIndex, "FXYSNUM",     fsJASANNUM);
            this.FPS91_TY_S_MR_32J7S131.SetValue(e.RowIndex, "FXYSPONUM",   fsPONUM);
            this.FPS91_TY_S_MR_32J7S131.SetValue(e.RowIndex, "FXYSRRNUM",   fsRRNUM);
            this.FPS91_TY_S_MR_32J7S131.SetValue(e.RowIndex, "FXYSGETVEND", fsVEND);
            this.FPS91_TY_S_MR_32J7S131.SetValue(e.RowIndex, "FXYSITEMCD",  fsITEMCODE);
            this.FPS91_TY_S_MR_32J7S131.SetValue(e.RowIndex, "FXYSCGVEND",  fsCGVEND);

            //DataTable dt = new DataTable();

            //this.DbConnector.Attach
            //        (
            //        "TY_P_MR_35O1S732",
            //        fsJASANNUM,
            //        fsPONUM,
            //        fsRRNUM,
            //        fsCGVEND,
            //        fsCHGUBUN
            //        );

            //dt = this.DbConnector.ExecuteDataTable();

            //if (dt.Rows.Count > 0)
            //{
            //    if (double.Parse(dt.Rows[0]["FXYSAMOUNT"].ToString()) != 0)
            //    {
            //    }
                
            //}

            if (this.FPS91_TY_S_MR_32J7S131.ActiveSheet.RowCount > 1)
            {
                for (int i = 1; i < this.FPS91_TY_S_MR_32J7S131.ActiveSheet.RowCount; i++)
                {
                    dAMOUNT = dAMOUNT + double.Parse(Get_Numeric(this.FPS91_TY_S_MR_32J7S131.GetValue(i, "FXYSAMOUNT").ToString()));
                }

                dGDAESANGHAP = double.Parse(this.TXT01_GDAESANGHAP.GetValue().ToString());

                sFXYSAMOUNT = string.Format("{0:#,##0}", dGDAESANGHAP - dAMOUNT);

                this.FPS91_TY_S_MR_32J7S131.SetValue(e.RowIndex, "FXYSAMOUNT", sFXYSAMOUNT);
            }
            else if (this.FPS91_TY_S_MR_32J7S131.ActiveSheet.RowCount == 1)
            {
                sFXYSAMOUNT = string.Format("{0:#,###}", Get_Numeric(this.TXT01_GDAESANGHAP.GetValue().ToString()));

                this.FPS91_TY_S_MR_32J7S131.SetValue(e.RowIndex, "FXYSAMOUNT", sFXYSAMOUNT);
            }
            
        }
        #endregion

        #region Description : 탭 컨트롤 이벤트
        private void tabControl1_Enable(string sGUBUN)
        {
            if (sGUBUN == "MANAGEMENT")
            {
                if (this.tabControl1.TabPages.Contains(this.tabPage1))
                    this.tabControl1.TabPages.Remove(this.tabPage1);

                if (!this.tabControl1.TabPages.Contains(this.tabPage2))
                    this.tabControl1.TabPages.Add(this.tabPage2);

                if (!this.tabControl1.TabPages.Contains(this.tabPage3))
                    this.tabControl1.TabPages.Add(this.tabPage3);
            }
            else if (sGUBUN == "QUERY")
            {
                if (this.tabControl1.TabPages.Contains(this.tabPage3))
                    this.tabControl1.TabPages.Remove(this.tabPage3);

                if (this.tabControl1.TabPages.Contains(this.tabPage2))
                    this.tabControl1.TabPages.Remove(this.tabPage2);

                if (!this.tabControl1.TabPages.Contains(this.tabPage1))
                    this.tabControl1.TabPages.Add(this.tabPage1);
            }
        }
        #endregion

        #region Description : 탭 이벤트
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 귀속부서 검토부서
            this.CBH01_FXDDPMK.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            if (tabControl1.SelectedIndex == 0) // 선급자재관리
            {
                fsJASANNUM = "";
                fsPONUM    = "";
                fsRRNUM    = "";
                fsCGVEND   = "";

                //tabControl1_Enable("MANAGEMENT");

                // 선급자재 생성 조회
                UP_SEL_ACFIXADMF(this.TXT01_FXDSAUP.GetValue().ToString());

                DataTable NewDt = new DataTable();
                NewDt.Clear();

                // 선급자재 예산
                this.FPS91_TY_S_MR_32J7S131.SetValue(NewDt);
                // 선급자재 내역
                this.FPS91_TY_S_MR_32J7M130.SetValue(NewDt);

                this.BTN61_SAV_JASAN.Visible     = false;
                this.BTN61_BATCH.Visible         = false;
                this.BTN61_SUNGUB_CANCEL.Visible = false;


                // 선급자재번호가 존재하면 버튼 안 보이게 함
                this.DbConnector.Attach
                    (
                    "TY_P_MR_32J7A127",
                    TXT01_FXDSAUP.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_MR_32J7M130.SetValue(dt);

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["FXSFIXNUM"].ToString() != "")
                    {
                        this.BTN61_CREATE.Visible = false;
                        this.BTN61_CANCEL.Visible = false;

                        this.BTN61_SAV_JASAN.Visible = false;

                        this.BTN61_BATCH.Visible         = false;
                        this.BTN61_SUNGUB_CANCEL.Visible = false;
                    }
                }

                // 선급자재 예산내역 존재체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_35S9S746",
                    TXT01_FXDSAUP.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.BTN61_CANCEL.Visible = false;
                }
                else
                {
                    this.BTN61_CANCEL.Visible = true;
                }
            }
            else if (tabControl1.SelectedIndex == 1) // 선급자재 예산별
            {
                tabControl1_Enable("MANAGEMENT");

                // 선급자재 내역
                this.DbConnector.Attach
                    (
                    "TY_P_MR_32J7A127",
                    TXT01_FXDSAUP.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_MR_32J7M130.SetValue(dt);

                for (int i = 0; i < this.FPS91_TY_S_MR_32J7M130.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSJPNO").ToString() == "")
                    {
                        this.FPS91_TY_S_MR_32J7M130_Sheet1.Cells[i, 28].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    }

                    if (this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSCHGUBUN").ToString() != "2")
                    {
                        this.FPS91_TY_S_MR_32J7M130_Sheet1.Cells[i, 23].Locked = true;
                        this.FPS91_TY_S_MR_32J7M130_Sheet1.Cells[i, 24].Locked = true;
                        this.FPS91_TY_S_MR_32J7M130_Sheet1.Cells[i, 25].Locked = true;
                        this.FPS91_TY_S_MR_32J7M130_Sheet1.Cells[i, 26].Locked = true;
                        this.FPS91_TY_S_MR_32J7M130_Sheet1.Cells[i, 27].Locked = true;
                    }

                    string year = this.FPS91_TY_S_MR_32J7M130.GetValue(i, "FXSNUM").ToString();

                    year = year.ToString().Substring(2, 6) + "01";
                    TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_MR_32J7M130, "FXSBUSEO");
                    if (tyCodeBox != null)
                        tyCodeBox.DummyValue = year;
                }

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["FXSFIXNUM"].ToString() == "" && dt.Rows[0]["FXSJPNO"].ToString() == "")
                    {
                        //this.BTN61_JASAN_CRE.Visible     = true;

                        this.BTN61_BATCH_JASAN.Visible   = true;
                        this.BTN62_SUNGUB_CANCEL.Visible = true;

                        //this.BTN61_JASAN_DEL.Visible = false;

                        this.BTN61_JPNO_CRE.Visible = true;
                        this.BTN61_JPNO_DEL.Visible = false;
                    }
                    else
                    {
                        //this.BTN61_JASAN_CRE.Visible = false;

                        this.BTN61_BATCH_JASAN.Visible   = false;
                        this.BTN62_SUNGUB_CANCEL.Visible = false;

                        this.BTN61_JPNO_CRE.Visible = false;
                        this.BTN61_JPNO_DEL.Visible = true;

                        //if (dt.Rows[0]["FXSJPNO"].ToString() == "")
                        //{
                        //    this.BTN61_JASAN_DEL.Visible = true;

                        //    this.BTN61_JPNO_CRE.Visible = true;
                        //    this.BTN61_JPNO_DEL.Visible = false;
                        //}
                        //else
                        //{
                        //    this.BTN61_JASAN_DEL.Visible = false;

                        //    this.BTN61_JPNO_CRE.Visible = false;
                        //    this.BTN61_JPNO_DEL.Visible = true;
                        //}
                    }
                }
                else
                {
                    this.BTN61_BATCH_JASAN.Visible   = false;
                    this.BTN62_SUNGUB_CANCEL.Visible = false;

                    //this.BTN61_JASAN_CRE.Visible = false;
                    //this.BTN61_JASAN_DEL.Visible = false;

                    this.BTN61_JPNO_CRE.Visible = false;
                    this.BTN61_JPNO_DEL.Visible = false;
                }
            }
        }
        #endregion

        #region Description : 전표 출력 버튼
        private void FPS91_TY_S_MR_32J7M130_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column.ToString() == "28")
            {
                if (this.FPS91_TY_S_MR_32J7M130.GetValue("FXSJPNO").ToString() != "")
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach
                        (
                        "TY_P_AC_2AU2M916",
                        this.FPS91_TY_S_MR_32J7M130.GetValue("FXSJPNO").ToString().Substring(0, 6),
                        this.FPS91_TY_S_MR_32J7M130.GetValue("FXSJPNO").ToString().Substring(6, 8),
                        this.FPS91_TY_S_MR_32J7M130.GetValue("FXSJPNO").ToString().Substring(14, 3),
                        this.FPS91_TY_S_MR_32J7M130.GetValue("FXSJPNO").ToString().Substring(14, 3)
                        );

                    if (Convert.ToDouble(this.FPS91_TY_S_MR_32J7M130.GetValue("FXSJPNO").ToString().Substring(6, 4)) > 2014)
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
                else
                {
                    this.ShowMessage("TY_M_MR_31933569");
                    return;
                }
            }
        }
        #endregion

        #region Description : 선급자재 전표 생성시 자산번호 있는 순번 가져오기
        private string GET_ADSLGLF_B2NOLN(string sB2NOJP, string sB2VLMI1)
        {
            DataTable dt = new DataTable();

            string sB2NOLN = string.Empty;

            sB2NOLN = "0";

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_MR_BC7BK884", sB2NOJP.ToString().Substring(0, 6),
                                                        sB2NOJP.ToString().Substring(6, 8),
                                                        sB2NOJP.ToString().Substring(14, 3),
                                                        sB2VLMI1.ToString());
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sB2NOLN = Set_Fill2(dt.Rows[0]["B2NOLN"].ToString());
            }

            return sB2NOLN;
        }
        #endregion
    }
}