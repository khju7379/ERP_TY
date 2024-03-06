using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.GB00;
namespace TY.ER.AC00
{
    /// <summary>
    /// 고정자산 자산이력 등록 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.12.11 21:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2CBAN093 : 고정자산 자산이력 확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  FXLCDAC : 계정과목
    ///  FXLMOVDPMK : 이동부서
    ///  FXLMOVSITE : 이동위치
    ///  FXLNOWDPMK : 현부서
    ///  FXLNOWSITE : 현위치
    ///  FXLRECCDBK : 입금은행
    ///  FXLRECNOAC : 입금계좌
    ///  FXLSETGN : 설정구분
    ///  FXLVEND : 거래처
    ///  FXLAMOUNT : 금액
    ///  FXLDSDATE1 : 설정기간S
    ///  FXLDSDATE2 : 설정기간E
    ///  FXLDWAMOUNT : 충당금감소액
    ///  FXLGRURL : 그룹웨어주소
    ///  FXLJASANNUM : 생성자산번호
    ///  FXLQTY : 수량
    ///  FXLRECAMOUNT : 입금금액
    ///  FXLRECGN : 입금형태
    ///  FXLSEQ : 자산순번
    ///  FXLSETDATE : 설정일자
    ///  FXLSETTEXT : 사유
    ///  FXLSUBNUM : 가족코드
    ///  FXLUSER : 사용자
    ///  FXLUWJPNO : 증감전표번호
    ///  FXLYEAR : 자산년도
    /// </summary>
    public partial class TYACHF005I : TYBase
    {
        private string fsFXLYEAR;
	    private string fsFXLSEQ;
	    private string fsFXLSUBNUM;
        private string fsFXLNUM;
	    private string fsFXLSETDATE;
	    private string fsFXLSETGN;

        #region Description : 자산이력  자료 처리(ACFIXASSETLOGF)
        private TYData DAT10_FXLYEAR;
        private TYData DAT10_FXLSEQ;
        private TYData DAT10_FXLSUBNUM;
        private TYData DAT10_FXLNUM;
        private TYData DAT10_FXLSETDATE;
        private TYData DAT10_FXLSETGN;
        private TYData DAT10_FXLSETTEXT;
        private TYData DAT10_FXLVEND;
        private TYData DAT10_FXLQTY;
        private TYData DAT10_FXLAMOUNT;
        private TYData DAT10_FXLDWAMOUNT;
        private TYData DAT10_FXLSTOCKNUM;
        private TYData DAT10_FXLGRURL;
        private TYData DAT10_FXLCDAC;
        private TYData DAT10_FXLDSDATE1;
        private TYData DAT10_FXLDSDATE2;
        private TYData DAT10_FXLUSER;
        private TYData DAT10_FXLJASANNUM;
        private TYData DAT10_FXLRECGN;
        private TYData DAT10_FXLRECAMOUNT;
        private TYData DAT10_FXLRECCDBK;
        private TYData DAT10_FXLRECNOAC;
        private TYData DAT10_FXLJPNODATE;     // 전표생성일자
        private TYData DAT10_FXLUWJPNO;
        private TYData DAT10_FXLMOVDPMK;
        private TYData DAT10_FXLMOVSITE;
        private TYData DAT10_FXLNOWDPMK;
        private TYData DAT10_FXLNOWSITE;
        private TYData DAT10_FXLBIPM;          //   비품코드
        private TYData DAT10_FXLYSSEQ;         //   순　　번
        private TYData DAT10_FXLUNDEBAMT;      //  미상각잔액
        private TYData DAT10_FXLBIGO;
        private TYData DAT10_FXLFILED1;   //카드번호  (신용카드시)
        private TYData DAT10_FXLFILED2;   //거래처코드(신용카드시) 
        private TYData DAT10_FXLHISAB;
        #endregion


        #region Description : 미승인 자료 처리 (TMAC1102F)
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

        private string fsJPNO = "";
        private string fsGubn = "A";

        private bool _Isloaded = false;

        #region  Description : 폼 로드 이벤트
        public TYACHF005I(string sFXLYEAR, string sFXLSEQ, string sFXLSUBNUM, string sFXLSETDATE, string sFXLSETGN, string sFXLNUM)
        {
            InitializeComponent();

            this.SetPopupStyle(); 
            
            fsFXLYEAR = sFXLYEAR;
            fsFXLSEQ = sFXLSEQ;
            fsFXLSUBNUM = sFXLSUBNUM;
            fsFXLNUM = sFXLNUM;
            fsFXLSETDATE = sFXLSETDATE;
            fsFXLSETGN = sFXLSETGN;

            #region Description : 자산 이력 내역
            this.DAT10_FXLYEAR      = new TYData("DAT10_FXLYEAR", null);
            this.DAT10_FXLSEQ       = new TYData("DAT10_FXLSEQ", null);
            this.DAT10_FXLSUBNUM    = new TYData("DAT10_FXLSUBNUM", null);
            this.DAT10_FXLNUM       = new TYData("DAT10_FXLNUM", null);
            this.DAT10_FXLSETDATE   = new TYData("DAT10_FXLSETDATE", null);
            this.DAT10_FXLSETGN     = new TYData("DAT10_FXLSETGN", null);
            this.DAT10_FXLSETTEXT   = new TYData("DAT10_FXLSETTEXT", null);
            this.DAT10_FXLVEND      = new TYData("DAT10_FXLVEND", null);
            this.DAT10_FXLQTY       = new TYData("DAT10_FXLQTY", null);
            this.DAT10_FXLAMOUNT    = new TYData("DAT10_FXLAMOUNT", null);
            this.DAT10_FXLDWAMOUNT  = new TYData("DAT10_FXLDWAMOUNT", null);
            this.DAT10_FXLSTOCKNUM  = new TYData("DAT10_FXLSTOCKNUM", null);
            this.DAT10_FXLGRURL     = new TYData("DAT10_FXLGRURL", null);
            this.DAT10_FXLCDAC      = new TYData("DAT10_FXLCDAC", null);
            this.DAT10_FXLDSDATE1   = new TYData("DAT10_FXLDSDATE1", null);
            this.DAT10_FXLDSDATE2   = new TYData("DAT10_FXLDSDATE2", null);
            this.DAT10_FXLUSER      = new TYData("DAT10_FXLUSER", null);
            this.DAT10_FXLJASANNUM  = new TYData("DAT10_FXLJASANNUM", null);
            this.DAT10_FXLRECGN     = new TYData("DAT10_FXLRECGN", null);
            this.DAT10_FXLRECAMOUNT = new TYData("DAT10_FXLRECAMOUNT", null);
            this.DAT10_FXLRECCDBK   = new TYData("DAT10_FXLRECCDBK", null);
            this.DAT10_FXLRECNOAC   = new TYData("DAT10_FXLRECNOAC", null);
            this.DAT10_FXLJPNODATE  = new TYData("DAT10_FXLJPNODATE", null); // 전표생성일자
            this.DAT10_FXLUWJPNO    = new TYData("DAT10_FXLUWJPNO", null);
            this.DAT10_FXLMOVDPMK   = new TYData("DAT10_FXLMOVDPMK", null);
            this.DAT10_FXLMOVSITE   = new TYData("DAT10_FXLMOVSITE", null);
            this.DAT10_FXLNOWDPMK   = new TYData("DAT10_FXLNOWDPMK", null);
            this.DAT10_FXLNOWSITE   = new TYData("DAT10_FXLNOWSITE", null);
            this.DAT10_FXLBIPM      = new TYData("DAT10_FXLBIPM", null);          //   비품코드
            this.DAT10_FXLYSSEQ     = new TYData("DAT10_FXLYSSEQ", null);         //   순　　번
            this.DAT10_FXLUNDEBAMT  = new TYData("DAT10_FXLUNDEBAMT", null);      //  미상각잔액
            this.DAT10_FXLBIGO      = new TYData("DAT10_FXLBIGO", null);
            this.DAT10_FXLFILED1 = new TYData("DAT10_FXLFILED1", null);
            this.DAT10_FXLFILED2 = new TYData("DAT10_FXLFILED2", null);
            this.DAT10_FXLHISAB     = new TYData("DAT10_FXLHISAB", null); 
            #endregion


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

        }

        private void TYACHF005I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_BTNJUNPYO.ProcessCheck += new TButton.CheckHandler(BTN61_BTNJUNPYO_ProcessCheck);
            this.BTN61_BTNJUNPRT.ProcessCheck += new TButton.CheckHandler(BTN61_BTNJUNPRT_ProcessCheck);
            this.BTN61_INQ_BUDGET.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_BUDGET_ProcessCheck);
            //BTN61_INQ_BUDGET_Click

            this.CBH01_FXLRECCDBK.CodeBoxDataBinded += new TCodeBox.TCodeBoxEventHandler(CBH01_FXLRECCDBK_CodeBoxDataBinded);

            UP_Set_CardControl(false, true);


            #region Description : 자산 이력 관리
            this.ControlFactory.Add(this.DAT10_FXLYEAR);
            this.ControlFactory.Add(this.DAT10_FXLSEQ);
            this.ControlFactory.Add(this.DAT10_FXLSUBNUM);
            this.ControlFactory.Add(this.DAT10_FXLNUM);
            this.ControlFactory.Add(this.DAT10_FXLSETDATE);
            this.ControlFactory.Add(this.DAT10_FXLSETGN);
            this.ControlFactory.Add(this.DAT10_FXLSETTEXT);
            this.ControlFactory.Add(this.DAT10_FXLVEND);
            this.ControlFactory.Add(this.DAT10_FXLQTY);
            this.ControlFactory.Add(this.DAT10_FXLAMOUNT);
            this.ControlFactory.Add(this.DAT10_FXLDWAMOUNT);
            this.ControlFactory.Add(this.DAT10_FXLSTOCKNUM);
            this.ControlFactory.Add(this.DAT10_FXLGRURL);
            this.ControlFactory.Add(this.DAT10_FXLCDAC);
            this.ControlFactory.Add(this.DAT10_FXLDSDATE1);
            this.ControlFactory.Add(this.DAT10_FXLDSDATE2);
            this.ControlFactory.Add(this.DAT10_FXLUSER);
            this.ControlFactory.Add(this.DAT10_FXLJASANNUM);
            this.ControlFactory.Add(this.DAT10_FXLRECGN);
            this.ControlFactory.Add(this.DAT10_FXLRECAMOUNT);
            this.ControlFactory.Add(this.DAT10_FXLRECCDBK);
            this.ControlFactory.Add(this.DAT10_FXLRECNOAC);
            this.ControlFactory.Add(this.DAT10_FXLJPNODATE);  // 전표생성일자
            this.ControlFactory.Add(this.DAT10_FXLUWJPNO);
            this.ControlFactory.Add(this.DAT10_FXLMOVDPMK);
            this.ControlFactory.Add(this.DAT10_FXLMOVSITE);
            this.ControlFactory.Add(this.DAT10_FXLNOWDPMK);
            this.ControlFactory.Add(this.DAT10_FXLNOWSITE);
            this.ControlFactory.Add(this.DAT10_FXLBIPM);          //   비품코드
            this.ControlFactory.Add(this.DAT10_FXLYSSEQ);         //   순　　번
            this.ControlFactory.Add(this.DAT10_FXLUNDEBAMT);      //  미상각잔액
            this.ControlFactory.Add(this.DAT10_FXLBIGO);
            this.ControlFactory.Add(this.DAT10_FXLFILED1);
            this.ControlFactory.Add(this.DAT10_FXLFILED2);
            this.ControlFactory.Add(this.DAT10_FXLHISAB); 
            #endregion


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


            this.BTN61_INQ_FXL.Image = global::TY.Service.Library.Properties.Resources.magnifier;
            this.BTN62_INQ_FXL.Image = global::TY.Service.Library.Properties.Resources.magnifier;

            //this._Isloaded = true;

            //LBL51_GSTXTBOX.Text = "수 선,증 가";
            //LBL52_GSTXTBOX.Text = "감       소";
            //LBL53_GSTXTBOX.Text = "저당,임대,보험";
            //LBL54_GSTXTBOX.Text = "이       동";
            
            this.DTP01_FXLSETDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH02_FXSAUP.DummyValue = this.DTP01_FXLSETDATE.GetString();
            this.CBH02_FXBUYDPMK.DummyValue = this.DTP01_FXLSETDATE.GetString();
            this.CBH01_FXLMOVDPMK.DummyValue = this.DTP01_FXLSETDATE.GetString();
            this.CBH01_FXLNOWDPMK.DummyValue = this.DTP01_FXLSETDATE.GetString(); 

            if (string.IsNullOrEmpty(this.fsFXLYEAR))  //신규
            {
                this.Initialize_Controls("01");
                this.Initialize_Controls("02");

                this.DTP01_FXLSETDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

                this.TXT03_FXLNUM.SetReadOnly(true);  

                UP_Set_ControlsLock();

                this.SetStartingFocus(this.TXT03_FXLYEAR); 
            }
            else //수정
            {
                this.TXT03_FXLYEAR.SetValue(fsFXLYEAR);
                this.TXT03_FXLSEQ.SetValue(fsFXLSEQ);
                this.TXT03_FXLSUBNUM.SetValue(fsFXLSUBNUM);
                this.TXT03_FXLNUM.SetValue(fsFXLNUM);
                this.BTN61_INQ_FXL.SetReadOnly(true);

                this.DTP01_FXLSETDATE.SetValue(fsFXLSETDATE);

                this.CBH01_FXLSETGN.SetValue(fsFXLSETGN); 

                UP_Get_SearchMaster(fsFXLYEAR, fsFXLSEQ, fsFXLSUBNUM);

                UP_Get_SearchAsHistory();

                UP_Set_ControlsLock();

                this.TXT03_FXLYEAR.SetReadOnly(true);
                this.TXT03_FXLSEQ.SetReadOnly(true);
                this.TXT03_FXLSUBNUM.SetReadOnly(true);
                this.DTP01_FXLSETDATE.SetReadOnly(true);
                this.CBH01_FXLSETGN.SetReadOnly(true);
                this.TXT03_FXLNUM.SetReadOnly(true);  
                
                this.SetStartingFocus(this.TXT01_FXLSETTEXT); 
            }

            this._Isloaded = true;

            //this.CBH01_FXLRECCDBK_CodeBoxDataBinded(null, null);
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sFXLYEAR   = string.Empty;
            string sFXLSEQ    = string.Empty;
            string sFXLSUBNUM = string.Empty;

            //자산이력에 설정일자보다 뒤에 자료가 있으면지 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2CD48139", this.DAT10_FXLYEAR.GetValue(), this.DAT10_FXLSEQ.GetValue(), this.DAT10_FXLSUBNUM.GetValue(), this.DAT10_FXLSETDATE.GetValue() );
            Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsFXLYEAR))
            {
                sFXLYEAR   = this.DAT10_FXLYEAR.GetValue().ToString();
                sFXLSEQ    = this.DAT10_FXLSEQ.GetValue().ToString();
                sFXLSUBNUM = this.DAT10_FXLSUBNUM.GetValue().ToString();

                this.DbConnector.Attach("TY_P_AC_2CCA0097", this.ControlFactory, "10");
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_2CCAM100", this.ControlFactory, "10");
            }
            this.DbConnector.ExecuteTranQueryList();

            // 증감파일에 등록 
            // 자본적지출(31)
            if (this.CBH01_FXLSETGN.GetValue().ToString() == "31" )
            {

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_A43AZ196", this.TXT03_FXLYEAR.GetValue().ToString(),
                                                            this.TXT03_FXLSEQ.GetValue().ToString(),
                                                            this.TXT03_FXLSUBNUM.GetValue().ToString(),
                                                            this.DTP01_FXLSETDATE.GetString().ToString());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    //double dFXDEPLAMT = Convert.ToDouble(dt.Rows[0]["FXDEPLAMT"].ToString()) + Convert.ToDouble(this.TXT01_FXLAMOUNT.GetValue().ToString());

                    double dFXDEPLAMT = Convert.ToDouble(this.TXT01_FXLAMOUNT.GetValue().ToString());

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_A43B5197", dFXDEPLAMT.ToString(), // 증가금액
                                                                this.TXT03_FXLYEAR.GetValue().ToString(),
                                                                this.TXT03_FXLSEQ.GetValue().ToString(),
                                                                this.TXT03_FXLSUBNUM.GetValue().ToString(),
                                                                this.DTP01_FXLSETDATE.GetString().ToString()
                                                                );
                    this.DbConnector.ExecuteTranQueryList();
                }
                else
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_35N8Y727", this.TXT03_FXLYEAR.GetValue().ToString(),
                                                                this.TXT03_FXLSEQ.GetValue().ToString(),
                                                                this.TXT03_FXLSUBNUM.GetValue().ToString(),
                                                                this.DTP01_FXLSETDATE.GetString().ToString(),
                                                                "",
                                                                this.CBH02_FXITEMCODE.GetValue().ToString(),
                                                                this.TXT01_FXLAMOUNT.GetValue().ToString(), // 증가금액
                                                                "0",  // 감소금액
                                                                this.TXT01_FXLSETTEXT.GetValue().ToString()
                                                                );
                    this.DbConnector.ExecuteTranQueryList();
                }
            }


            //고정자산 디테일에 변경상태 저장
            //자산이력에 설정일자보다 뒤에 자료가 있으면 수정안한다.
            //혹시나 같은 설정일자에 2개이상 설정구분이 있을경우 아무거나 한다.
            if (iCnt <= 0)
            {
                string sFXSCHGCODE = string.Empty; // -- 변경코드
                string sFXSCHGTEXT = string.Empty; // -- 변경사유
                string sFXSCHGDATE = string.Empty; // -- 변경일자
                string sFXSCHGDPMK = string.Empty; // -- 변경부서
                string sFXSCHGSITE = string.Empty; // -- 변경위치
                string sFXSFITSITE = string.Empty; //  -- 설치위치

                if (this.DAT10_FXLSETGN.GetValue().ToString() == "63")
                {
                    this.DbConnector.CommandClear(); // ACFIXASSETSF
                    this.DbConnector.Attach("TY_P_AC_495DX775", sFXLYEAR.ToString(), sFXLSEQ.ToString(), sFXLSUBNUM.ToString());
                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    sFXSCHGCODE = dt.Rows[0]["FXSCHGCODE"].ToString();
                    sFXSCHGTEXT = dt.Rows[0]["FXSCHGTEXT"].ToString();
                    sFXSCHGDATE = dt.Rows[0]["FXSCHGDATE"].ToString();
                    sFXSCHGDPMK = dt.Rows[0]["FXSCHGDPMK"].ToString();
                    sFXSCHGSITE = dt.Rows[0]["FXSCHGSITE"].ToString();
                    sFXSFITSITE = dt.Rows[0]["FXSFITSITE"].ToString();
                }

                this.DbConnector.CommandClear();
                if (this.DAT10_FXLSETGN.GetValue().ToString() == "63")
                {
                    //ACFIXASSETSF 
                    this.DbConnector.Attach("TY_P_AC_2CD3Y138", this.DAT10_FXLSETGN.GetValue(), this.DAT10_FXLSETTEXT.GetValue(), this.DAT10_FXLSETDATE.GetValue(),
                                                                this.DAT10_FXLMOVDPMK.GetValue(), this.DAT10_FXLMOVSITE.GetValue(), this.DAT10_FXLHISAB.GetValue(),
                                                                this.DAT10_FXLYEAR.GetValue(), this.DAT10_FXLSEQ.GetValue(), this.DAT10_FXLSUBNUM.GetValue());

                    // 현재 위치 변경 ACFIXASSETSF
                    this.DbConnector.Attach("TY_P_AC_37H6R143", this.DAT10_FXLMOVSITE.GetValue(),
                                             this.DAT10_FXLYEAR.GetValue(), this.DAT10_FXLSEQ.GetValue(), this.DAT10_FXLSUBNUM.GetValue());

                    // 이전정보 저장 (삭제 처리시 사용)  ACFIXASSETLOGF
                    if (string.IsNullOrEmpty(this.fsFXLYEAR))
                    {
                        this.DbConnector.Attach("TY_P_AC_495ET778",
                                                        sFXSCHGCODE,
                                                        sFXSCHGTEXT,
                                                        sFXSCHGDATE,
                                                        sFXSCHGDPMK,
                                                        sFXSCHGSITE,
                                                        sFXSFITSITE,
                                                        this.DAT10_FXLYEAR.GetValue(),
                                                        this.DAT10_FXLSEQ.GetValue(),
                                                        this.DAT10_FXLSUBNUM.GetValue(),
                                                        this.DAT10_FXLNUM.GetValue(),
                                                        this.DAT10_FXLSETDATE.GetValue(),
                                                        this.DAT10_FXLSETGN.GetValue());
                    }

                }
                else
                {
                    this.DbConnector.Attach("TY_P_AC_2CD3Y138", this.DAT10_FXLSETGN.GetValue(), this.DAT10_FXLSETTEXT.GetValue(), this.DAT10_FXLSETDATE.GetValue(),
                                            this.DAT10_FXLMOVDPMK.GetValue(), this.DAT10_FXLMOVSITE.GetValue(), this.DAT10_FXLHISAB.GetValue(),
                                            this.DAT10_FXLYEAR.GetValue(), this.DAT10_FXLSEQ.GetValue(), this.DAT10_FXLSUBNUM.GetValue()); 
                }

                this.DbConnector.ExecuteTranQueryList();
            }

            this.TXT03_FXLYEAR.SetReadOnly(true);
            this.TXT03_FXLSEQ.SetReadOnly(true);
            this.TXT03_FXLSUBNUM.SetReadOnly(true);
            this.DTP01_FXLSETDATE.SetReadOnly(true);
            this.CBH01_FXLSETGN.SetReadOnly(true);
            this.TXT03_FXLNUM.SetReadOnly(true);

            this.SetFocus(this.TXT01_FXLSETTEXT); 

            this.ShowMessage("TY_M_GB_23NAD873");            
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();  

            //this.UP_SETJUPYO();
        }
        #endregion

        private void UP_SETJUPYO()
        {
            string sFXASCLASS = "";

            string sB2SSID = "";
            string sW2RKAC = "";

            string sW2DPMK = string.Empty;
            string sW2DPAC = string.Empty;
            string sW2CDAC_1 = string.Empty;
            string sW2CDAC_2 = string.Empty;
            string sW2CDAC_3 = string.Empty;
            string sW2CDAC_22 = string.Empty;
            string sW2CDAC_21 = string.Empty;
            string s부가세계정 = string.Empty;

            string sREPAMOUNTSUM = string.Empty;
            string sREPJANAMOUNT = string.Empty;

            bool bJunPyoFlag = false;

            int iCnt = 0;

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            DataTable dt = new DataTable();

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            sB2SSID = this.IPAdresss + "0377-M" + dAutoSeq.ToString();

            //사번 조회
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_GB_24G9S659", Employer.EmpNo.ToString().Trim());  //INKIBNMF
            this.DbConnector.Attach("TY_P_GB_4CVJ7024", "20151231", "0377-M");  //INKIBNMF
            DataTable dt_sabun = this.DbConnector.ExecuteDataTable();
            if (dt_sabun.Rows.Count != 0)
            { sW2DPMK = dt_sabun.Rows[0]["KBBUSEO"].ToString().Trim(); }

            if (fsGubn == "A")
            {
                #region Description : 전표 생성

                #region Description : 폐기(41),기증(44)


                //dt.Clear();
                string s자산번호 = string.Empty;
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_615GP405");
                DataTable dm = this.DbConnector.ExecuteDataTable();
                if (dm.Rows.Count > 0)
                {

                    for (int i = 0; i < dm.Rows.Count; i++)
                    {
                        if (this.CBH01_FXLSETGN.GetValue().ToString() == "41" || this.CBH01_FXLSETGN.GetValue().ToString() == "44")
                        {

                            sFXASCLASS = dm.Rows[i]["FXASCLASS"].ToString();

                            if (sFXASCLASS.ToString().Substring(0, 1) == "2")
                            {
                                sW2CDAC_1 = "12200299";
                                sW2CDAC_2 = "52000702";
                                sW2CDAC_3 = "12200200";
                            }
                            else if (sFXASCLASS.ToString().Substring(0, 1) == "3")
                            {
                                sW2CDAC_1 = "12200399";
                                sW2CDAC_2 = "52000703";
                                sW2CDAC_3 = "12200300";
                            }
                            else if (sFXASCLASS.ToString().Substring(0, 1) == "4")
                            {
                                sW2CDAC_1 = "12200499";
                                sW2CDAC_2 = "52000704";
                                sW2CDAC_3 = "12200400";
                            }
                            else if (sFXASCLASS.ToString().Substring(0, 1) == "5")
                            {
                                sW2CDAC_1 = "12200599";
                                sW2CDAC_2 = "52000788";
                                sW2CDAC_3 = "12200500";
                            }
                            else if (sFXASCLASS.ToString().Substring(0, 1) == "6")
                            {
                                sW2CDAC_1 = "12200699";
                                sW2CDAC_2 = "52000788";
                                sW2CDAC_3 = "12200600";
                            }
                            else if (sFXASCLASS.ToString().Substring(0, 1) == "7")
                            {
                                sW2CDAC_1 = "12200799";
                                sW2CDAC_2 = "52000788";
                                sW2CDAC_3 = "12200700";
                            }
                            else if (sFXASCLASS.ToString().Substring(0, 1) == "8")
                            {
                                sW2CDAC_1 = "12200899";
                                sW2CDAC_2 = "52000788";
                                sW2CDAC_3 = "12200800";
                            }
                            else if (sFXASCLASS.ToString().Substring(0, 1) == "9")
                            {
                                sW2CDAC_1 = "12200999";
                                sW2CDAC_2 = "52000705";
                                sW2CDAC_3 = "12200900";
                            }

                            // ------------------------------------ Line 번호 : 1  감가상각충당금 (차변)
                            iCnt = iCnt + 1;

                            dt.Clear();

                            this.DAT02_W2SSID.SetValue(sB2SSID);
                            this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                            this.DAT02_W2DTMK.SetValue("20151231"); // 작성일자
                            this.DAT02_W2NOSQ.SetValue("0");
                            this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                            this.DAT02_W2IDJP.SetValue("3");
                            this.DAT02_W2NOJP.SetValue("");
                            this.DAT02_W2CDAC.SetValue(sW2CDAC_1);
                            this.DAT02_W2DTAC.SetValue("");
                            this.DAT02_W2DTLI.SetValue("");
                            this.DAT02_W2DPAC.SetValue(UP_Set_DPAC(dm.Rows[i]["FXSAUP"].ToString())); // 귀속부서

                            //관리항목 
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_23N3M888", sW2CDAC_1, "");
                            dt = this.DbConnector.ExecuteDataTable();
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                                { this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI1.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                                { this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI2.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                                { this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI3.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                                { this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI4.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                                { this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI5.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                                { this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI6.SetValue(""); }
                            }

                            this.DAT02_W2VLMI1.SetValue("");
                            this.DAT02_W2VLMI2.SetValue("");
                            this.DAT02_W2VLMI3.SetValue("");
                            this.DAT02_W2VLMI4.SetValue("");
                            this.DAT02_W2VLMI5.SetValue("");
                            this.DAT02_W2VLMI6.SetValue("");

                            // 구자산번호 가져오기
                            dt.Clear();
                            //string s자산번호 = string.Empty;
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_44TEA300", dm.Rows[i]["FXSYEAR"].ToString(), dm.Rows[i]["FXSSEQ"].ToString());
                            dt = this.DbConnector.ExecuteDataTable();
                            if (dt.Rows.Count > 0)
                            {
                                s자산번호 = dt.Rows[0]["FXOLDNUM"].ToString().Trim();
                                if (s자산번호 == "")
                                {
                                    s자산번호 = dm.Rows[i]["FXSYEAR"].ToString().Trim() + "-" + dm.Rows[i]["FXSSEQ"].ToString().Trim() + "-" + dm.Rows[i]["FXSSUBNUM"].ToString();
                                }
                                else
                                {
                                    s자산번호 = s자산번호 + " : " + dm.Rows[i]["FXSYEAR"].ToString().Trim() + "-" + dm.Rows[i]["FXSSEQ"].ToString().Trim() + "-" + dm.Rows[i]["FXSSUBNUM"].ToString();
                                }
                            }

                            sW2RKAC = dm.Rows[i]["FXSNAME"].ToString().Trim().Trim() + "손망실(" + s자산번호 + ")";

                            // 월상각 테이블에서 자료 가지고 옮
                            double dFXLAMOUNT = 0;
                            double dFXLDWAMOUNT = 0;
                            double dFXLUNDEBAMT = 0;

                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_343BI420", dm.Rows[i]["FXSYEAR"].ToString(),
                                                                        dm.Rows[i]["FXSSEQ"].ToString(),
                                                                        dm.Rows[i]["FXSSUBNUM"].ToString()); // ACFIXREPAYMMF
                            DataTable dt_reyymm = this.DbConnector.ExecuteDataTable();
                            if (dt_reyymm.Rows.Count > 0)
                            {
                                dFXLAMOUNT = Convert.ToDouble(dt_reyymm.Rows[0]["GETAMT"].ToString().Trim());
                                dFXLDWAMOUNT = Convert.ToDouble(dt_reyymm.Rows[0]["FXMREPAMOUNTSUM"].ToString().Trim());
                                dFXLUNDEBAMT = Convert.ToDouble(dt_reyymm.Rows[0]["FXMREPJANAMOUNT"].ToString().Trim());
                            }

                            this.DAT02_W2AMDR.SetValue(dFXLDWAMOUNT.ToString().Trim()); // 차변금액 (충당금감소소액 필더)
                            this.DAT02_W2AMCR.SetValue("0");

                            this.DAT02_W2CDFD.SetValue("");
                            this.DAT02_W2AMFD.SetValue("0");
                            this.DAT02_W2RKAC.SetValue(sW2RKAC);
                            this.DAT02_W2RKCU.SetValue("");
                            this.DAT02_W2WCJP.SetValue("");
                            this.DAT02_W2PRGB.SetValue("");
                            this.DAT02_W2HIGB.SetValue("A");
                            this.DAT02_W2HISAB.SetValue("0377-M");
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


                            // ------------------------------------ Line 번호 : 2  처분손실 (차변)
                            iCnt = iCnt + 1;

                            dt.Clear();

                            string s귀속부서 = string.Empty;
                            s귀속부서 = UP_Set_DPAC(dm.Rows[i]["FXSAUP"].ToString());

                            this.DAT02_W2SSID.SetValue(sB2SSID);
                            this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                            this.DAT02_W2DTMK.SetValue("20151231"); // 작성일자
                            this.DAT02_W2NOSQ.SetValue("0");
                            this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                            this.DAT02_W2IDJP.SetValue("3");
                            this.DAT02_W2NOJP.SetValue("");
                            this.DAT02_W2CDAC.SetValue(sW2CDAC_2);
                            this.DAT02_W2DTAC.SetValue("");
                            this.DAT02_W2DTLI.SetValue("");
                            this.DAT02_W2DPAC.SetValue(s귀속부서); // 귀속부서

                            //관리항목 
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_23N3M888", sW2CDAC_2, "");
                            dt = this.DbConnector.ExecuteDataTable();
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                                { this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI1.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                                { this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI2.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                                { this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI3.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                                { this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI4.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                                { this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI5.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                                { this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI6.SetValue(""); }
                            }

                            this.DAT02_W2VLMI1.SetValue("");
                            this.DAT02_W2VLMI2.SetValue("");
                            this.DAT02_W2VLMI3.SetValue("");
                            this.DAT02_W2VLMI4.SetValue("");
                            this.DAT02_W2VLMI5.SetValue("");
                            this.DAT02_W2VLMI6.SetValue("");

                            //sW2RKAC = this.TXT01_FXLSETTEXT.GetValue().ToString();

                            sW2RKAC = dm.Rows[i]["FXSNAME"].ToString() + "손망실(" + s자산번호 + ")";

                            sREPJANAMOUNT = "0";
                            // this.TXT01_FXLAMOUNT(금액) - this.TXT01_FXLDWAMOUNT(충당금감소액)
                            sREPJANAMOUNT = Convert.ToString(dFXLAMOUNT - dFXLDWAMOUNT);

                            this.DAT02_W2AMDR.SetValue(sREPJANAMOUNT); // 차변금액 this.TXT01_FXLDWAMOUNT.GetValue().ToString().Trim()
                            this.DAT02_W2AMCR.SetValue("0");

                            this.DAT02_W2CDFD.SetValue("");
                            this.DAT02_W2AMFD.SetValue("0");
                            this.DAT02_W2RKAC.SetValue(sW2RKAC);
                            this.DAT02_W2RKCU.SetValue("");
                            this.DAT02_W2WCJP.SetValue("");
                            this.DAT02_W2PRGB.SetValue("");
                            this.DAT02_W2HIGB.SetValue("A");
                            this.DAT02_W2HISAB.SetValue("0377-M");
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

                            // ------------------------------------ Line 번호 : 3  기계장치,차량운반구,집기비품.... (대변)
                            iCnt = iCnt + 1;

                            dt.Clear();

                            this.DAT02_W2SSID.SetValue(sB2SSID);
                            this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                            this.DAT02_W2DTMK.SetValue("20151231"); // 작성일자
                            this.DAT02_W2NOSQ.SetValue("0");
                            this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                            this.DAT02_W2IDJP.SetValue("3");
                            this.DAT02_W2NOJP.SetValue("");
                            this.DAT02_W2CDAC.SetValue(sW2CDAC_3);
                            this.DAT02_W2DTAC.SetValue("");
                            this.DAT02_W2DTLI.SetValue("");
                            this.DAT02_W2DPAC.SetValue(UP_Set_DPAC(dm.Rows[i]["FXSAUP"].ToString())); // 귀속부서

                            //관리항목 
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_23N3M888", sW2CDAC_3, "");
                            dt = this.DbConnector.ExecuteDataTable();
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                                { this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI1.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                                { this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI2.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                                { this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI3.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                                { this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI4.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                                { this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI5.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                                { this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI6.SetValue(""); }
                            }

                            this.DAT02_W2VLMI1.SetValue("");
                            this.DAT02_W2VLMI2.SetValue("");
                            this.DAT02_W2VLMI3.SetValue("");
                            this.DAT02_W2VLMI4.SetValue("");
                            this.DAT02_W2VLMI5.SetValue("");
                            this.DAT02_W2VLMI6.SetValue("");

                            //sW2RKAC = this.TXT01_FXLSETTEXT.GetValue().ToString();
                            sW2RKAC = dm.Rows[i]["FXSNAME"].ToString() + "손망실(" + s자산번호 + ")";

                            this.DAT02_W2AMDR.SetValue("0"); // 차변금액 
                            this.DAT02_W2AMCR.SetValue(dFXLAMOUNT.ToString().Trim()); // 대변금액

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
                        }

                    } //for...end
                }                

                #endregion            

                if (datas.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    foreach (object[] data in datas)
                    {
                        this.DbConnector.Attach("TY_P_AC_29DA5966", data);
                    }
                }

                
                ////미승인 SP호출 파일 입력
                //if (this.CBH01_FXLSETGN.GetValue().ToString() == "41" || this.CBH01_FXLSETGN.GetValue().ToString() == "42" || this.CBH01_FXLSETGN.GetValue().ToString() == "44")
                //{
                //    this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, "0377-M", "A",
                //                                                sW2DPMK, "20151231", "", "",
                //                                                "", "", "0377-M");
                //}
                //else
                //{
                //    this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, "0377-M", "A",
                //                            sW2DPMK, this.DTP01_FXLSETDATE.GetString(), "", "",
                //                            "", "", "0377-M");
                //};

                //this.DbConnector.ExecuteTranQueryList();

                ////전표 생성 함수 호출
                //bJunPyoFlag = false;
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_AC_29C80960", "A", sB2SSID, "");
                //string sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());
                //if (sOUTMSG.Substring(0, 2) == "ER")
                //{
                //    this.ShowCustomMessage(sOUTMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //}
                //else
                //{
                //    bJunPyoFlag = true;
                //    //this.ShowCustomMessage(sOUTMSG, "완료", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);
                //    this.ShowMessage("TY_M_AC_25O8K620");
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_AC_29D5B004", sB2SSID);
                //    DataTable dtresult = this.DbConnector.ExecuteDataTable();
                //    if (dtresult.Rows.Count > 0)
                //    {
                //        if (dtresult.Rows[0]["AGRESULTCODE"].ToString().Trim() == "OK")
                //        {
                //            //전표번호 받아오기
                //            string sJpno = dtresult.Rows[0]["AGOUT_PARAM"].ToString();
                //            if (sJpno.Trim() != "")
                //            {
                //                this.TXT01_FXLUWJPNO.SetValue(sJpno);
                //                //this.BTN61_BTNJUNPYO.Visible = false;
                //            }
                //        }
                //    }
                //}                 

                //if (bJunPyoFlag)
                //{
                //    this.DbConnector.CommandClear();
                //    this.DbConnector.Attach("TY_P_AC_3423Y416", this.TXT01_FXLUWJPNO.GetValue().ToString(),
                //                                                this.TXT03_FXLYEAR.GetValue().ToString(),
                //                                                this.TXT03_FXLSEQ.GetValue().ToString(),
                //                                                this.TXT03_FXLSUBNUM.GetValue().ToString(),
                //                                                this.DTP01_FXLSETDATE.GetString().ToString(),
                //                                                this.CBH01_FXLSETGN.GetValue().ToString());
                //    this.DbConnector.ExecuteTranQueryList();

                //    // 증감파일에 등록 
                //    // 폐기(41),매각(42),기증(44)
                //    if (this.CBH01_FXLSETGN.GetValue().ToString() == "41" || this.CBH01_FXLSETGN.GetValue().ToString() == "42" || this.CBH01_FXLSETGN.GetValue().ToString() == "44")
                //    {
                //        this.DbConnector.CommandClear();
                //        this.DbConnector.Attach("TY_P_AC_35N8Y727", this.TXT03_FXLYEAR.GetValue().ToString(),
                //                                                    this.TXT03_FXLSEQ.GetValue().ToString(),
                //                                                    this.TXT03_FXLSUBNUM.GetValue().ToString(),
                //                                                    this.DTP01_FXLSETDATE.GetString().ToString(),
                //                                                    "",
                //                                                    this.CBH02_FXITEMCODE.GetValue().ToString(),
                //                                                    "0",
                //                                                    this.TXT01_FXLAMOUNT.GetValue().ToString(),
                //                                                    this.TXT01_FXLSETTEXT.GetValue().ToString()
                //                                                    );
                //        this.DbConnector.ExecuteTranQueryList();
                //    }


                //    // 합필 , 분할 처리
                //    if (this.CBH01_FXLSETGN.GetValue().ToString() == "35" || this.CBH01_FXLSETGN.GetValue().ToString() == "46")
                //    {
                //        string sSETGN = "";

                //        if (this.CBH01_FXLSETGN.GetValue().ToString() == "35")
                //        {
                //            sSETGN = "45";
                //        }
                //        else
                //        {
                //            sSETGN = "36";
                //        };

                //        this.DbConnector.CommandClear();
                //        this.DbConnector.Attach("TY_P_AC_34N55557", this.TXT01_FXLUWJPNO.GetValue().ToString(),
                //                                                    this.TXT03_FXLYEAR.GetValue().ToString() + Set_Fill4(this.TXT03_FXLSEQ.GetValue().ToString()) + Set_Fill3(this.TXT03_FXLSUBNUM.GetValue().ToString()),
                //                                                    sSETGN);
                //        this.DbConnector.ExecuteTranQueryList();
                //    }


                //    fsGubn = "D";
                //    this.BTN61_BTNJUNPYO.Text = "전표취소";
                //    this.BTN61_BTNJUNPRT.Visible = true;
                //}
                #endregion
            }
            else
            {
                #region Description : 전표 취소

                //미승인전표 -> 임시파일 입력 (전표삭제)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29C7K957", sB2SSID, this.TXT01_FXLUWJPNO.GetValue().ToString().Replace("-","").Substring(0, 6), this.TXT01_FXLUWJPNO.GetValue().ToString().Replace("-","").Substring(6, 8), this.TXT01_FXLUWJPNO.GetValue().ToString().Replace("-","").Substring(14, 3));
                //this.DbConnector.ExecuteTranQueryList();
                ////미승인 SP호출 파일 입력
                //this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, Employer.EmpNo, "D",
                                                 sW2DPMK, this.DTP01_FXLSETDATE.GetString(), "", "",
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
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3423Y416", "",
                                                                "",
                                                                TYUserInfo.EmpNo,
                                                                this.TXT03_FXLYEAR.GetValue().ToString(),
                                                                this.TXT03_FXLSEQ.GetValue().ToString(),
                                                                this.TXT03_FXLSUBNUM.GetValue().ToString(),
                                                                this.DTP01_FXLSETDATE.GetString().ToString(),
                                                                this.CBH01_FXLSETGN.GetValue().ToString());
                    this.DbConnector.ExecuteTranQueryList();


                    // 합필 , 분할 처리
                    if (this.CBH01_FXLSETGN.GetValue().ToString() == "35" || this.CBH01_FXLSETGN.GetValue().ToString() == "46")
                    {
                        string sSETGN = "";

                        if (this.CBH01_FXLSETGN.GetValue().ToString() == "35")
                        {
                            sSETGN = "45";
                        }
                        else
                        {
                            sSETGN = "36";
                        };

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_34N55557", "",
                                                                     this.TXT03_FXLYEAR.GetValue().ToString() + Set_Fill4(this.TXT03_FXLSEQ.GetValue().ToString()) + Set_Fill3(this.TXT03_FXLSUBNUM.GetValue().ToString()),
                                                                     sSETGN);
                        this.DbConnector.ExecuteTranQueryList();
                    }


                    // 증감파일에 삭제 
                    // 폐기(41),매각(42),기증(44)
                    if (this.CBH01_FXLSETGN.GetValue().ToString() == "41" || this.CBH01_FXLSETGN.GetValue().ToString() == "42" || this.CBH01_FXLSETGN.GetValue().ToString() == "44")
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_35N8Z728", this.TXT03_FXLYEAR.GetValue().ToString(),
                                                                    this.TXT03_FXLSEQ.GetValue().ToString(),
                                                                    this.TXT03_FXLSUBNUM.GetValue().ToString(),
                                                                    this.DTP01_FXLSETDATE.GetString().ToString()
                                                                    );
                        this.DbConnector.ExecuteTranQueryList();
                    }

                    this.ShowMessage("TY_M_AC_25O8K620");

                    // this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    fsGubn = "A";
                    this.TXT01_FXLUWJPNO.SetValue("");
                    this.BTN61_BTNJUNPYO.Text = "전표생성";

                    this.BTN61_BTNJUNPRT.Visible = false;

                    // this.Close();
                }

                #endregion
            }

        }


        #region  Description : 고정자산 자산이력 조회
        private void UP_Get_SearchAsHistory()
        {
            fsGubn = "";
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2CDBS126", this.fsFXLYEAR.ToString(), this.fsFXLSEQ.ToString(), this.fsFXLSUBNUM.ToString(), this.fsFXLSETDATE.ToString(), this.fsFXLSETGN.ToString(), this.fsFXLNUM.ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
                        
            this.CBH01_FXLMOVDPMK.DummyValue = fsFXLSETDATE;
            this.CBH01_FXLNOWDPMK.DummyValue = fsFXLSETDATE;

            this.CBH01_FXLRECCDBK.SetValue(dt.Rows[0]["FXLRECCDBK"].ToString());

            this.CurrentDataTableRowMapping(dt, "01");

            if (this.TXT01_FXLUWJPNO.GetValue().ToString().Trim() == "")
            {
                fsGubn = "A";

                this.BTN61_BTNJUNPRT.Visible = false;
            }
            else
            {
                fsGubn = "D";
                this.BTN61_BTNJUNPYO.Text = "전표취소";
                this.TXT01_FXLUWJPNO.SetReadOnly(true);

                this.BTN61_BTNJUNPRT.Visible = true;
            }

            // 예산 명 조회

            if (this.CBH01_FXLCDAC.GetValue().ToString().Trim() != "")
            {
                string sBUDGB = string.Empty;
                sBUDGB = UP_Set_BudGetTab(this.CBH01_FXLCDAC.GetValue().ToString().Trim());

                // 기타세목 : 11 , 13 , 14, 15 , 16  
                if (sBUDGB == "11" || sBUDGB == "13" || sBUDGB == "14" || sBUDGB == "15" || sBUDGB == "16")
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_AC_36E2P849", this.DTP01_FXLSETDATE.GetString().ToString().Substring(0, 4),
                                                                this.CBH02_FXSAUP.GetValue(),
                                                                this.CBH01_FXLCDAC.GetValue().ToString().Trim(),
                                                                this.TXT01_FXLYSSEQ.GetValue().ToString().Trim());
                    DataTable dt_gb11 = this.DbConnector.ExecuteDataTable();
                    if (dt_gb11.Rows.Count > 0)
                    {
                        this.TXT01_BUDNAME.SetValue(dt_gb11.Rows[0]["P1RKAC"].ToString());
                    }

                }

                // 소모성 비품 : 31
                if (sBUDGB == "31")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_36E2Q850", this.DTP01_FXLSETDATE.GetString().ToString().Substring(0, 4),
                                                                this.CBH02_FXSAUP.GetValue(),
                                                                this.CBH01_FXLCDAC.GetValue().ToString().Trim(),
                                                                this.CBH01_FXLBIPM.GetValue().ToString().Trim(),
                                                                this.TXT01_FXLYSSEQ.GetValue().ToString().Trim());

                    DataTable dt_gb31 = this.DbConnector.ExecuteDataTable();
                    if (dt_gb31.Rows.Count > 0)
                    {
                        this.TXT01_BUDNAME.SetValue(dt_gb31.Rows[0]["J1RKAC"].ToString());
                    }

                }
            }

        }
        #endregion

        #region  Description : 고정자산 기본사항 조회
        private void UP_Get_SearchMaster(string sYEAR,string sSEQ,string sNUM )
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2CC9F096", sYEAR.ToString(), sSEQ.ToString(), sNUM.ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            CBH02_FXSAUP.DummyValue = dt.Rows[0]["FXGETDATE"].ToString();
            CBH02_FXBUYDPMK.DummyValue = dt.Rows[0]["FXGETDATE"].ToString();

            this.CurrentDataTableRowMapping(dt, "02");
        }
        #endregion

        #region  Description : 02 컨트롤 화면 잠금
        private void UP_Set_ControlsLock()
        {
            this.CBH02_FXASCLASS.SetReadOnly(true);
            this.CBH02_FXITEMCODE.SetReadOnly(true);
            this.TXT02_FXSNAME.SetReadOnly(true);
            this.MTB02_FXGETDATE.SetReadOnly(true);
            this.CBH02_FXSAUP.SetReadOnly(true);
            this.CBH02_FXBUYDPMK.SetReadOnly(true);
            this.TXT02_FXSQTY.SetReadOnly(true);
            this.TXT02_FXSGETAMOUNT.SetReadOnly(true);
            this.CBH02_FXSFITSITE.SetReadOnly(true);
            this.CBH01_FXLNOWDPMK.SetReadOnly(true);
            this.CBH01_FXLNOWSITE.SetReadOnly(true);
            this.TXT01_FXLGRURL.SetReadOnly(true);
        }
        #endregion

        #region  Description : DTP01_FXLSETDATE_ValueChanged
        private void DTP01_FXLSETDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_FXLMOVDPMK.DummyValue = this.DTP01_FXLSETDATE.GetString();
            this.CBH01_FXLNOWDPMK.DummyValue = this.DTP01_FXLSETDATE.GetString();
        }
        #endregion

        #region  Description : BTN61_INQ_FXL_Click
        private void BTN61_INQ_FXL_Click(object sender, EventArgs e)
        {
            TYAZHF05C1 popup = new TYAZHF05C1();
            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT03_FXLYEAR.SetValue(popup.fsASNUM.Substring(0,4) );
                this.TXT03_FXLSEQ.SetValue(popup.fsASNUM.Substring(5, 4));
                this.TXT03_FXLSUBNUM.SetValue(popup.fsASNUM.Substring(10,3));

                UP_Get_AsNumCheck();
            }            
        }
        #endregion 

        #region  Description : UP_Get_AsNumCheck 고정자산 세부내역 찾기
        private void UP_Get_AsNumCheck()
        {
            //자산번호 자리수 체크
            if (string.IsNullOrEmpty(this.fsFXLYEAR) == false)  //신규
            {
                if (this.TXT03_FXLYEAR.GetValue().ToString().Length < 4 && this.TXT03_FXLSEQ.GetValue().ToString().Length < 4 && this.TXT03_FXLSUBNUM.GetValue().ToString().Length < 3)
                {
                    this.ShowMessage("TY_M_AC_2CCAS102");
                    this.SetFocus(this.TXT03_FXLYEAR);
                    return;
                }
            }

            if (this.TXT03_FXLYEAR.GetValue().ToString().Length >= 4 && this.TXT03_FXLSEQ.GetValue().ToString().Length >= 1 && this.TXT03_FXLSUBNUM.GetValue().ToString().Length >= 1)
            {
                //유효한 자산번호인지 체크
                string sYEAR = this.TXT03_FXLYEAR.GetValue().ToString();
                string sSEQ = this.TXT03_FXLSEQ.GetValue().ToString();
                string sNUM = Set_Fill3(this.TXT03_FXLSUBNUM.GetValue().ToString());

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2CC9F096", sYEAR.ToString(), sSEQ.ToString(), sNUM.ToString());
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CBH02_FXSAUP.DummyValue = dt.Rows[0]["FXGETDATE"].ToString();
                    this.CBH02_FXBUYDPMK.DummyValue = dt.Rows[0]["FXGETDATE"].ToString();

                    this.CurrentDataTableRowMapping(dt, "02");
                }
                else
                {
                    // 폐기,매각일때 월상각 자료의 마지막 자료 (상각누계액 , 취득가액+증가액+감소소액 을 가지고 온다)

                    this.ShowMessage("TY_M_AC_2CCAS102");
                    this.SetFocus(this.TXT03_FXLYEAR);
                    return;
                }

                FPS91_TY_S_AC_87CFI376.Initialize();
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_87CES370", this.TXT03_FXLYEAR.GetValue(), this.TXT03_FXLSEQ.GetValue(), this.TXT03_FXLSUBNUM.GetValue());
                FPS91_TY_S_AC_87CFI376.SetValue(this.DbConnector.ExecuteDataTable());
            }
        }
        #endregion

        #region  Description : 자산의 현재위치 찾기
        private void UP_Get_NowLocation()
        {         
            //유효한 자산번호인지 체크
            string sYEAR = this.TXT03_FXLYEAR.GetValue().ToString();
            string sSEQ = this.TXT03_FXLSEQ.GetValue().ToString();
            string sNUM = this.TXT03_FXLSUBNUM.GetValue().ToString();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2CBAN093", sYEAR.ToString(), sSEQ.ToString(), sNUM.ToString(), "63");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            //자산 이력 등록이 없으면 현위치, 현부서는 자사 마스타에서 가져온다
            if (dt.Rows.Count > 0)
            {
                string sFXLNOWDPMK = "";
                string sFXLNOWSITE = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //sFXLNOWDPMK = dt.Rows[i]["FXLMOVDPMK"].ToString();  
                    //sFXLNOWSITE = dt.Rows[i]["FXLMOVSITE"].ToString();

                    this.CBH01_FXLNOWDPMK.DummyValue = dt.Rows[i]["FXLSETDATE"].ToString();

                    this.CBH01_FXLNOWDPMK.SetValue(dt.Rows[i]["FXLMOVDPMK"].ToString()); // 2014.09.04 변경
                    this.CBH01_FXLNOWSITE.SetValue(dt.Rows[i]["FXLMOVSITE"].ToString());
                }
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2CC9F096", sYEAR.ToString(), sSEQ.ToString(), sNUM.ToString());
                DataTable dm = this.DbConnector.ExecuteDataTable();
                if (dm.Rows.Count > 0)
                {
                    this.CBH01_FXLNOWDPMK.DummyValue = dm.Rows[0]["FXGETDATE"].ToString();   // 2015.01.19 추가

                    this.CBH01_FXLNOWDPMK.SetValue(dm.Rows[0]["FXSAUP"].ToString());
                    this.CBH01_FXLNOWSITE.SetValue(dm.Rows[0]["FXSFITSITE"].ToString()); 
                }
            }

        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //자산번호 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2CC9F096", this.TXT03_FXLYEAR.GetValue().ToString(), this.TXT03_FXLSEQ.GetValue().ToString(), this.TXT03_FXLSUBNUM.GetValue().ToString());
            DataTable dk = this.DbConnector.ExecuteDataTable();
            if (dk.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_AC_2CCAS102");
                this.SetFocus(this.TXT03_FXLYEAR);
                e.Successed = false;
                return;
            }

            // 폐기,매각,기증 일때 월상각 자료의 마지막 자료 (상각누계액 , 취득가액+증가액+감소소액 을 가지고 온다)
            //if (this.CBH01_FXLSETGN.GetValue().ToString() == "41" || this.CBH01_FXLSETGN.GetValue().ToString() == "42" || this.CBH01_FXLSETGN.GetValue().ToString() == "44")
            //{
            //    // 월상각 테이블에서 자료 가지고 옮
            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach("TY_P_AC_343BI420", this.TXT03_FXLYEAR.GetValue().ToString(),
            //                                                this.TXT03_FXLSEQ.GetValue().ToString(),
            //                                                this.TXT03_FXLSUBNUM.GetValue().ToString()); // ACFIXREPAYMMF
            //    DataTable dt_reyymm = this.DbConnector.ExecuteDataTable();
            //    if (dt_reyymm.Rows.Count > 0)
            //    {
            //        this.TXT01_FXLAMOUNT.SetValue(dt_reyymm.Rows[0]["GETAMT"].ToString().Trim());
            //        this.TXT01_FXLDWAMOUNT.SetValue(dt_reyymm.Rows[0]["FXMREPAMOUNTSUM"].ToString().Trim());
            //        // this.TXT01_FXLAMOUNT( 취득가액+증가액+감소소액<=> 금액) - this.TXT01_FXLDWAMOUNT(상각누계액<=>충당금감소액)
            //    }
            //}

            // 폐기(41),매각(42),기증(44),양도(44),비용수선(61),보험(62),이동(63),자본적지출(31) 일때 --> 이전에 폐기,매각,기증,양도가 존재 하면 추가등록을 못함.
            if (this.CBH01_FXLSETGN.GetValue().ToString() == "41" || this.CBH01_FXLSETGN.GetValue().ToString() == "42" || this.CBH01_FXLSETGN.GetValue().ToString() == "44" ||
                this.CBH01_FXLSETGN.GetValue().ToString() == "44" || 
                this.CBH01_FXLSETGN.GetValue().ToString() == "61" || this.CBH01_FXLSETGN.GetValue().ToString() == "62" || this.CBH01_FXLSETGN.GetValue().ToString() == "63" ||
                this.CBH01_FXLSETGN.GetValue().ToString() == "31")
            {
                // 자산상세 테이블에서 자료 가지고 옮
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_37H5A139", this.TXT03_FXLYEAR.GetValue().ToString(),
                                                            this.TXT03_FXLSEQ.GetValue().ToString(),
                                                            this.TXT03_FXLSUBNUM.GetValue().ToString()); // ACFIXASSETSF
                DataTable dt_CHK = this.DbConnector.ExecuteDataTable();
                if (dt_CHK.Rows.Count > 0)
                { 
                    // 이미 폐기,매각,기증 처리된 자산입니다.
                    this.ShowMessage("TY_M_AC_37H5G140");
                    this.SetFocus(this.TXT03_FXLYEAR);
                    e.Successed = false;
                    return;
                }
            }

            // 입금형태가 신용카드일 경우
            if (this.CBO01_FXLRECGN.GetValue().ToString() == "03")
            {
                CBH01_FXLRECCDBK.SetValue("");
                CBH01_FXLRECNOAC.SetValue("");
            }
            else
            {
                CBH01_FXLFILED1.SetValue("");
                CBH01_FXLFILED2.SetValue("");
            }
           

            this.DAT10_FXLYEAR.SetValue(this.TXT03_FXLYEAR.GetValue().ToString());
            this.DAT10_FXLSEQ.SetValue(this.TXT03_FXLSEQ.GetValue().ToString());
            this.DAT10_FXLSUBNUM.SetValue(this.TXT03_FXLSUBNUM.GetValue().ToString());

            this.DAT10_FXLSETDATE.SetValue(this.DTP01_FXLSETDATE.GetString());
            this.DAT10_FXLSETGN.SetValue(this.CBH01_FXLSETGN.GetValue());
            this.DAT10_FXLSETTEXT.SetValue(this.TXT01_FXLSETTEXT.GetValue());
            this.DAT10_FXLVEND.SetValue(this.CBH01_FXLVEND.GetValue());
            this.DAT10_FXLQTY.SetValue(this.TXT01_FXLQTY.GetValue());
            this.DAT10_FXLAMOUNT.SetValue(this.TXT01_FXLAMOUNT.GetValue());
            this.DAT10_FXLDWAMOUNT.SetValue(this.TXT01_FXLDWAMOUNT.GetValue());
            this.DAT10_FXLSTOCKNUM.SetValue(this.TXT01_FXLSTOCKNUM.GetValue());
            this.DAT10_FXLGRURL.SetValue(this.TXT01_FXLGRURL.GetValue());
            this.DAT10_FXLCDAC.SetValue(this.CBH01_FXLCDAC.GetValue());
            this.DAT10_FXLDSDATE1.SetValue(this.MTB01_FXLDSDATE1.GetValue().ToString().Replace("-", "").ToString());
            this.DAT10_FXLDSDATE2.SetValue(this.MTB01_FXLDSDATE2.GetValue().ToString().Replace("-", "").ToString());
            this.DAT10_FXLUSER.SetValue(this.TXT01_FXLUSER.GetValue());
            this.DAT10_FXLJASANNUM.SetValue(this.TXT01_FXLJASANNUM.GetValue());
            this.DAT10_FXLRECGN.SetValue(this.CBO01_FXLRECGN.GetValue());
            this.DAT10_FXLRECAMOUNT.SetValue(this.TXT01_FXLRECAMOUNT.GetValue());
            this.DAT10_FXLRECCDBK.SetValue(this.CBH01_FXLRECCDBK.GetValue());
            this.DAT10_FXLRECNOAC.SetValue(this.CBH01_FXLRECNOAC.GetValue());           
            
            this.DAT10_FXLJPNODATE.SetValue(this.DTP01_FXLSETDATE.GetString().ToString()); // 전표생성일자
            this.DAT10_FXLUWJPNO.SetValue(this.TXT01_FXLUWJPNO.GetValue());

            this.DAT10_FXLMOVDPMK.SetValue(this.CBH01_FXLMOVDPMK.GetValue());
            this.DAT10_FXLMOVSITE.SetValue(this.CBH01_FXLMOVSITE.GetValue());
            this.DAT10_FXLNOWDPMK.SetValue(this.CBH01_FXLNOWDPMK.GetValue());
            this.DAT10_FXLNOWSITE.SetValue(this.CBH01_FXLNOWSITE.GetValue());

            this.DAT10_FXLBIPM.SetValue(this.CBH01_FXLBIPM.GetValue());          //   비품코드
            this.DAT10_FXLYSSEQ.SetValue(this.TXT01_FXLYSSEQ.GetValue());        //   순　　번
            this.DAT10_FXLUNDEBAMT.SetValue(this.TXT01_FXLUNDEBAMT.GetValue());  //   미상각잔액

            this.DAT10_FXLBIGO.SetValue(this.TXT01_FXLBIGO.GetValue());
            this.DAT10_FXLFILED1.SetValue(this.CBH01_FXLFILED1.GetValue().ToString());
            this.DAT10_FXLFILED2.SetValue(this.CBH01_FXLFILED2.GetValue().ToString());
            this.DAT10_FXLHISAB.SetValue(TYUserInfo.EmpNo);

            //동일코드 체크
            if (string.IsNullOrEmpty(this.fsFXLYEAR))  //신규
            {
                // 자산번호 + 설정일자+설정구분 에 대한 마지막 순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_35A1I650", this.DAT10_FXLYEAR.GetValue(), this.DAT10_FXLSEQ.GetValue(), this.DAT10_FXLSUBNUM.GetValue(), this.DAT10_FXLSETDATE.GetValue(), this.DAT10_FXLSETGN.GetValue());
                Int16 iSEQ = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                this.DAT10_FXLNUM.SetValue(Convert.ToString(iSEQ));
                this.TXT03_FXLNUM.SetValue(this.DAT10_FXLNUM.GetValue().ToString());

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2CDBS126", this.DAT10_FXLYEAR.GetValue(), this.DAT10_FXLSEQ.GetValue(), this.DAT10_FXLSUBNUM.GetValue(),
                                                            this.DAT10_FXLSETDATE.GetValue(), this.DAT10_FXLSETGN.GetValue(),this.DAT10_FXLNUM.GetValue());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_GB_23S40973");
                    this.SetFocus(this.TXT03_FXLYEAR);
                    e.Successed = false;
                    return;
                }
            }

            this.DAT10_FXLNUM.SetValue(this.TXT03_FXLNUM.GetValue().ToString());

            //이동
            if (this.CBH01_FXLSETGN.GetValue().ToString() == "63")
            {
                if (this.CBH01_FXLMOVDPMK.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_2CD6N149");
                    this.SetFocus(this.CBH01_FXLMOVDPMK.CodeText);  
                    e.Successed = false;
                    return;
                }
                if (CBH01_FXLMOVSITE.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_2CD6O150");
                    this.SetFocus(this.CBH01_FXLMOVSITE.CodeText);  
                    e.Successed = false;
                    return;
                }
            }
            
            //합필(출),분할(출)
            if (this.CBH01_FXLSETGN.GetValue().ToString() == "45" || this.CBH01_FXLSETGN.GetValue().ToString() == "36")
            {
                if (this.TXT01_FXLJASANNUM.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_34C3P498");
                    this.SetFocus(this.TXT01_FXLJASANNUM);  
                    e.Successed = false;
                    return;
                }
            }

            //매각 입금금액 체크
            if (this.CBH01_FXLSETGN.GetValue().ToString() == "42")
            {
                // 입금금액
                if (this.TXT01_FXLRECAMOUNT.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_43VFS081");
                    this.SetFocus(this.TXT01_FXLRECAMOUNT);
                    e.Successed = false;
                    return;
                }

                // 입금형태
                if (this.CBO01_FXLRECGN.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_43VFT082");
                    this.SetFocus(this.CBO01_FXLRECGN);
                    e.Successed = false;
                    return;
                }

                // 제예금
                if (this.CBO01_FXLRECGN.GetValue().ToString() == "01")
                {
                    // 입금은행
                    if (this.CBH01_FXLRECCDBK.GetValue().ToString().Trim() == "")
                    {
                        this.ShowMessage("TY_M_AC_2445M440");
                        this.SetFocus(this.CBH01_FXLRECCDBK);
                        e.Successed = false;
                        return;
                    }

                    // 계좌번호
                    if (this.CBH01_FXLRECNOAC.GetValue().ToString().Trim() == "" )
                    {
                        this.ShowMessage("TY_M_AC_2445M441");
                        this.SetFocus(this.CBH01_FXLRECNOAC);
                        e.Successed = false;
                        return;
                    }

                }
            }


            //31(자본적 지출-- 상각완료 체크)
            if (this.CBH01_FXLSETGN.GetValue().ToString() == "31" )
            {
                

                // 입금형태가 신용카드일 경우
                if (this.CBO01_FXLRECGN.GetValue().ToString() == "03")
                {
                    if (CBH01_FXLFILED1.GetValue().ToString() == "")
                    {
                        this.ShowCustomMessage("신용카드번호를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_FXLFILED1);
                        e.Successed = false;
                        return;
                    }
                    if (CBH01_FXLFILED2.GetValue().ToString() == "")
                    {
                        this.ShowCustomMessage("거래처코드를 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_FXLFILED2);
                        e.Successed = false;
                        return;
                    }
                }
                else
                {
                    if (this.CBH01_FXLVEND.GetValue().ToString() == "" && this.CBH01_FXLRECCDBK.GetValue().ToString() == "" && this.CBH01_FXLRECNOAC.GetValue().ToString() == "")
                    {
                        this.ShowCustomMessage("거래처,은행,계좌번호중 입력자료 미존재(전표생성안됨)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_FXLVEND);
                        e.Successed = false;
                        return;
                    }

                    if (this.CBH01_FXLRECCDBK.GetValue().ToString() != "" && this.CBH01_FXLRECNOAC.GetValue().ToString() == "")
                    {
                        this.ShowCustomMessage("계좌번호를 입력하세요(제예금 전표생성시)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_FXLRECNOAC);
                        e.Successed = false;
                        return;
                    }

                    if (this.TXT01_FXLAMOUNT.GetValue().ToString() == "")
                    {
                        this.ShowCustomMessage("금액을 입력하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_FXLAMOUNT);
                        e.Successed = false;
                        return;
                    }
                }

                /*
                this.DbConnector.CommandClear(); //ACFIXASSETSF
                this.DbConnector.Attach("TY_P_AC_35OCB731", this.TXT03_FXLYEAR.GetValue(), this.TXT03_FXLSEQ.GetValue(), this.TXT03_FXLSUBNUM.GetValue());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["FXSKESAN"].ToString().Trim() == "9")
                    {
                        this.ShowCustomMessage("상각완료자산입니다.(등록불가)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.TXT01_FXLJASANNUM);
                        e.Successed = false;
                        return;
                    }
                } */

                /* 미상각잔액 1000원 이하면 자본적지출을 할수 없다  */
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_87CES370", this.TXT03_FXLYEAR.GetValue(), this.TXT03_FXLSEQ.GetValue(), this.TXT03_FXLSUBNUM.GetValue());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    if ( Convert.ToDouble(dt.Rows[0]["FXMREPJANAMOUNT"].ToString().Trim()) <= 1000 )
                    {
                        this.ShowCustomMessage("상각완료자산입니다.(등록불가)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_FXLSETGN.CodeText);
                        e.Successed = false;
                        return;
                    }
                }

                if (this.CBH01_FXLCDAC.GetValue().ToString().Trim() == "")
                {
                    this.ShowCustomMessage("자본적 지출 등록시 자산계정 필수등록", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_FXLCDAC);
                    e.Successed = false;
                    return;
                }

                string sBUDGB = string.Empty;

                sBUDGB = UP_Set_BudGetTab(this.CBH01_FXLCDAC.GetValue().ToString().Trim());
                if (sBUDGB != "11")
                {
                    this.ShowCustomMessage("자본적 지출 등록시 자산계정등록", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    this.SetFocus(this.CBH01_FXLCDAC);
                    e.Successed = false;
                    return;
                }

                if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "2")
                {
                    if (this.CBH01_FXLCDAC.GetValue().ToString() != "12200200")
                    {
                        this.ShowCustomMessage("자산 계정과목 오류", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_FXLCDAC);
                        e.Successed = false;
                        return;
                    }
                }
                if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "3")
                {
                    if (this.CBH01_FXLCDAC.GetValue().ToString() != "12200300")
                    {
                        this.ShowCustomMessage("자산 계정과목 오류", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_FXLCDAC);
                        e.Successed = false;
                        return;
                    }
                }
                if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "4")
                {
                    if (this.CBH01_FXLCDAC.GetValue().ToString() != "12200400")
                    {
                        this.ShowCustomMessage("자산 계정과목 오류", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_FXLCDAC);
                        e.Successed = false;
                        return;
                    }
                }
                if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "5")
                {
                    if (this.CBH01_FXLCDAC.GetValue().ToString() != "12200500")
                    {
                        this.ShowCustomMessage("자산 계정과목 오류", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_FXLCDAC);
                        e.Successed = false;
                        return;
                    }
                }
                if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "6")
                {
                    if (this.CBH01_FXLCDAC.GetValue().ToString() != "12200600")
                    {
                        this.ShowCustomMessage("자산 계정과목 오류", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_FXLCDAC);
                        e.Successed = false;
                        return;
                    }
                }
                if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "7")
                {
                    if (this.CBH01_FXLCDAC.GetValue().ToString() != "12200700")
                    {
                        this.ShowCustomMessage("자산 계정과목 오류", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_FXLCDAC);
                        e.Successed = false;
                        return;
                    }
                }
                if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "8")
                {
                    if (this.CBH01_FXLCDAC.GetValue().ToString() != "12200800")
                    {
                        this.ShowCustomMessage("자산 계정과목 오류", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_FXLCDAC);
                        e.Successed = false;
                        return;
                    }
                }
                if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "9")
                {
                    if (this.CBH01_FXLCDAC.GetValue().ToString() != "12200900")
                    {
                        this.ShowCustomMessage("자산 계정과목 오류", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        this.SetFocus(this.CBH01_FXLCDAC);
                        e.Successed = false;
                        return;
                    }
                }
            }

            //사유
            if (this.TXT01_FXLSETTEXT.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_AC_34P1B566");
                this.SetFocus(this.TXT01_FXLSETTEXT);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : CBH01_FXLSETGN_CodeBoxDataBinded 이벤트
        private void CBH01_FXLSETGN_CodeBoxDataBinded(object sender, EventArgs e)
        {            
            if (this.TXT03_FXLYEAR.GetValue().ToString() != "" && this.TXT03_FXLSEQ.GetValue().ToString() != "" && this.TXT03_FXLSUBNUM.GetValue().ToString() != "")
            {
                UP_Get_AsNumCheck();

                UP_Get_NowLocation();

               
                if (string.IsNullOrEmpty(this.fsFXLYEAR) == true)  //신규
                {
                    // 폐기,매각일때 월상각 자료의 마지막 자료 (상각누계액 , 취득가액+증가액+감소소액 을 가지고 온다)
                    if (this.CBH01_FXLSETGN.GetValue().ToString() == "41" || this.CBH01_FXLSETGN.GetValue().ToString() == "42" || this.CBH01_FXLSETGN.GetValue().ToString() == "44")
                    {
                        // 월상각 테이블에서 자료 가지고 옮
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_343BI420", this.TXT03_FXLYEAR.GetValue().ToString(),
                                                                    this.TXT03_FXLSEQ.GetValue().ToString(),
                                                                    this.TXT03_FXLSUBNUM.GetValue().ToString()); // ACFIXREPAYMMF
                        DataTable dt_reyymm = this.DbConnector.ExecuteDataTable();
                        if (dt_reyymm.Rows.Count > 0)
                        {
                            this.TXT01_FXLAMOUNT.SetValue(dt_reyymm.Rows[0]["GETAMT"].ToString().Trim());
                            this.TXT01_FXLDWAMOUNT.SetValue(dt_reyymm.Rows[0]["FXMREPAMOUNTSUM"].ToString().Trim());
                            this.TXT01_FXLUNDEBAMT.SetValue(dt_reyymm.Rows[0]["FXMREPJANAMOUNT"].ToString().Trim()); // 미상각잔액
                            // this.TXT01_FXLAMOUNT( 취득가액+증가액+감소소액<=> 금액) - this.TXT01_FXLDWAMOUNT(상각누계액<=>충당금감소액)
                        }
                    }
                }
            }

        }
        #endregion

        #region Description : CBH01_FXLSETGN_CodeBoxDataBinded 이벤트
        private void CBH01_FXLMOVSITE_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            this.SetFocus(this.BTN61_SAV); 
        }
        #endregion

        #region Description : 계좌번호 코드 헬프 처리
        private void CBH01_FXLRECCDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_FXLRECCDBK.GetValue().ToString();
            this.CBH01_FXLRECNOAC.DummyValue = groupCode;
            this.CBH01_FXLRECNOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_FXLRECNOAC.Initialize();
        }
        #endregion

        #region Description : TXT03_FXLYEAR_KeyDown
        private void TXT03_FXLYEAR_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.TXT03_FXLYEAR.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    BTN61_INQ_FXL_Click(null, null);
                }
            }  
        }
        #endregion

        #region Description : TXT03_FXLSEQ_KeyDown
        private void TXT03_FXLSEQ_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.TXT03_FXLSEQ.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    BTN61_INQ_FXL_Click(null, null);
                }
            }
        }
        #endregion

        #region Description : TXT03_FXLSUBNUM_KeyDown
        private void TXT03_FXLSUBNUM_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (this.TXT03_FXLSUBNUM.ReadOnly == false)
            {
                if (e.KeyCode == System.Windows.Forms.Keys.F1)
                {
                    BTN61_INQ_FXL_Click(null, null);
                }

                if (e.KeyCode == System.Windows.Forms.Keys.Enter)
                {
                    UP_Get_AsNumCheck();

                    //BTN61_INQ_FXL_Click(null, null);
                }
            }
        }
        #endregion

        #region Description : 그룹웨어 주소 버튼 Click
        private void BTN62_INQ_FXL_Click(object sender, EventArgs e)
        {
            if (this.TXT01_FXLGRURL.GetValue().ToString() != "")
            {
                if ((new TYERGB012P(this.TXT01_FXLGRURL.GetValue().ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                }
            }
        }
        #endregion

        #region Description : KeyPress
        private void CBH01_FXLSETGN_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            this.SetFocus(this.TXT01_FXLSETTEXT);
        }

        private void TXT01_FXLQTY_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //this.SetFocus(this.TXT01_FXLDWAMOUNT);
        }

        private void TXT01_FXLDWAMOUNT_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            this.SetFocus(this.TXT01_FXLBIGO);
        } 
        #endregion


        #region Description : 전표발행 버튼 이벤트
        private void BTN61_BTNJUNPYO_Click(object sender, EventArgs e)
        {
            string sB2SSID = "";
            string sW2RKAC = "";

            string sW2DPMK = string.Empty;
            string sW2DPAC = string.Empty;
            string sW2CDAC_1 = string.Empty;
            string sW2CDAC_2 = string.Empty;
            string sW2CDAC_3 = string.Empty;
            string sW2CDAC_22 = string.Empty;
            string sW2CDAC_21 = string.Empty;
            string s부가세계정 = string.Empty;

            string sREPAMOUNTSUM = string.Empty;
            string sREPJANAMOUNT = string.Empty;

            bool bJunPyoFlag = false;

            int iCnt = 0;

            System.Collections.Generic.List<object[]> datas = new System.Collections.Generic.List<object[]>();

            DataTable dt = new DataTable();

            //BATID번호 부여
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29C7M958");
            decimal dAutoSeq = Convert.ToDecimal(this.DbConnector.ExecuteScalar());
            sB2SSID = this.IPAdresss + Employer.EmpNo + dAutoSeq.ToString();

            //사번 조회
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_GB_24G9S659", Employer.EmpNo.ToString().Trim());  //INKIBNMF
            this.DbConnector.Attach("TY_P_GB_4CVJ7024",this.MTB01_FXLJPNODATE.GetValue().ToString().Replace("-",""), Employer.EmpNo.ToString().Trim());  //INKIBNMF
            DataTable dt_sabun = this.DbConnector.ExecuteDataTable();
            if (dt_sabun.Rows.Count != 0)
            { sW2DPMK = dt_sabun.Rows[0]["KBBUSEO"].ToString().Trim();  }

            if (fsGubn == "A")
            {
                #region Description : 전표 생성 

                #region Description : 폐기(41),기증(44)

                if (this.CBH01_FXLSETGN.GetValue().ToString() == "41" || this.CBH01_FXLSETGN.GetValue().ToString() == "44")
                {
                    if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "2")
                    {
                        sW2CDAC_1 = "12200299";
                        sW2CDAC_2 = "52000702";
                        sW2CDAC_3 = "12200200";
                    }
                    else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "3")
                    {
                        sW2CDAC_1 = "12200399";
                        sW2CDAC_2 = "52000703";
                        sW2CDAC_3 = "12200300";
                    }
                    else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "4")
                    {
                        sW2CDAC_1 = "12200499";
                        sW2CDAC_2 = "52000704";
                        sW2CDAC_3 = "12200400";
                    }
                    else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "5")
                    {
                        sW2CDAC_1 = "12200599";
                        sW2CDAC_2 = "52000788";
                        sW2CDAC_3 = "12200500";
                    }
                    else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "6")
                    {
                        sW2CDAC_1 = "12200699";
                        sW2CDAC_2 = "52000788";
                        sW2CDAC_3 = "12200600";
                    }
                    else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "7")
                    {
                        sW2CDAC_1 = "12200799";
                        sW2CDAC_2 = "52000788";
                        sW2CDAC_3 = "12200700";
                    }
                    else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "8")
                    {
                        sW2CDAC_1 = "12200899";
                        sW2CDAC_2 = "52000788";
                        sW2CDAC_3 = "12200800";
                    }
                    else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "9")
                    {
                        sW2CDAC_1 = "12200999";
                        sW2CDAC_2 = "52000705";
                        sW2CDAC_3 = "12200900";
                    }

                    // ------------------------------------ Line 번호 : 1  감가상각충당금 (차변)
                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.MTB01_FXLJPNODATE.GetValue().ToString().Replace("-", "").ToString()); // 작성일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(sW2CDAC_1);
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH02_FXSAUP.GetValue().ToString().Trim()); // 귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", sW2CDAC_1, "");
                    dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                        { this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI1.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                        { this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI2.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                        { this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI3.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                        { this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI4.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                        { this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI5.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                        { this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI6.SetValue(""); }
                    }

                    this.DAT02_W2VLMI1.SetValue("");
                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    // 구자산번호 가져오기
                    dt.Clear();
                    string s자산번호 = string.Empty;
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_44TEA300", this.TXT03_FXLYEAR.GetValue().ToString().Trim(), this.TXT03_FXLSEQ.GetValue().ToString().Trim());
                    dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                       s자산번호 =  dt.Rows[0]["FXOLDNUM"].ToString().Trim() ;
                       if (s자산번호 == "")
                       {
                           s자산번호 = this.TXT03_FXLYEAR.GetValue().ToString().Trim() + "-" + this.TXT03_FXLSEQ.GetValue().ToString().Trim() + "-" + this.TXT03_FXLSUBNUM.GetValue().ToString().Trim();
                       }
                       else
                       {
                           s자산번호 = s자산번호 + " : " + this.TXT03_FXLYEAR.GetValue().ToString().Trim() + "-" + this.TXT03_FXLSEQ.GetValue().ToString().Trim() + "-" + this.TXT03_FXLSUBNUM.GetValue().ToString().Trim();
                       }
                    }

                    sW2RKAC = this.TXT02_FXSNAME.GetValue().ToString().Trim() + "손망실(" + s자산번호+")";

                    this.DAT02_W2AMDR.SetValue(this.TXT01_FXLDWAMOUNT.GetValue().ToString().Trim()); // 차변금액 (충당금감소소액 필더)
                    this.DAT02_W2AMCR.SetValue("0");

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


                    // ------------------------------------ Line 번호 : 2  처분손실 (차변)
                    iCnt = iCnt + 1;

                    dt.Clear();

                    string s귀속부서 = string.Empty;
                    s귀속부서 = UP_Set_DPAC(this.CBH02_FXSAUP.GetValue().ToString().Trim());
                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.MTB01_FXLJPNODATE.GetValue().ToString().Replace("-", "").ToString()); // 작성일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(sW2CDAC_2);
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(s귀속부서); // 귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", sW2CDAC_2, "");
                    dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                        { this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI1.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                        { this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI2.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                        { this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI3.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                        { this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI4.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                        { this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI5.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                        { this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI6.SetValue(""); }
                    }

                    this.DAT02_W2VLMI1.SetValue("");
                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    //sW2RKAC = this.TXT01_FXLSETTEXT.GetValue().ToString();

                    sW2RKAC = this.TXT02_FXSNAME.GetValue().ToString().Trim() + "손망실(" + s자산번호 + ")";

                    sREPJANAMOUNT = "0";
                    // this.TXT01_FXLAMOUNT(금액) - this.TXT01_FXLDWAMOUNT(충당금감소액)
                    sREPJANAMOUNT = Convert.ToString(Convert.ToDouble(this.TXT01_FXLAMOUNT.GetValue().ToString().Trim()) - Convert.ToDouble(this.TXT01_FXLDWAMOUNT.GetValue().ToString().Trim()));
                    this.DAT02_W2AMDR.SetValue(sREPJANAMOUNT); // 차변금액 this.TXT01_FXLDWAMOUNT.GetValue().ToString().Trim()
                    this.DAT02_W2AMCR.SetValue("0");

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

                    // ------------------------------------ Line 번호 : 3  기계장치,차량운반구,집기비품.... (대변)
                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.MTB01_FXLJPNODATE.GetValue().ToString().Replace("-", "").ToString()); // 작성일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(sW2CDAC_3);
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH02_FXSAUP.GetValue().ToString().Trim()); // 귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", sW2CDAC_3, "");
                    dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                        { this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI1.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                        { this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI2.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                        { this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI3.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                        { this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI4.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                        { this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI5.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                        { this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI6.SetValue(""); }
                    }

                    this.DAT02_W2VLMI1.SetValue("");
                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    //sW2RKAC = this.TXT01_FXLSETTEXT.GetValue().ToString();
                    sW2RKAC = this.TXT02_FXSNAME.GetValue().ToString().Trim() + "손망실(" + s자산번호 + ")";

                    this.DAT02_W2AMDR.SetValue("0"); // 차변금액 
                    this.DAT02_W2AMCR.SetValue(this.TXT01_FXLAMOUNT.GetValue().ToString().Trim()); // 대변금액

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
                }

                #endregion

                #region Description : 매각 (42)

                if (this.CBH01_FXLSETGN.GetValue().ToString() == "42")
                {
                    if (sW2DPMK != "A10400")
                    {
                        s부가세계정 = "21103101"; // 매출부가세 본점 21103101
                    }
                    else
                    {
                        s부가세계정 = "21103102"; // 매출부가세 지점
                    }

                    if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "2")
                    {
                        sW2CDAC_1 = "12200299";
                        sW2CDAC_22 = "52000702";
                        sW2CDAC_21 = "51000802";
                        sW2CDAC_3 = "12200200";

                    }
                    else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "3")
                    {
                        sW2CDAC_1 = "12200399";
                        sW2CDAC_22 = "52000703";
                        sW2CDAC_21 = "51000803";
                        sW2CDAC_3 = "12200300";
                    }
                    else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "4")
                    {
                        sW2CDAC_1 = "12200499";
                        sW2CDAC_22 = "52000704";
                        sW2CDAC_21 = "51000804";
                        sW2CDAC_3 = "12200400";
                    }
                    else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "5")
                    {
                        sW2CDAC_1 = "12200599";
                        sW2CDAC_22 = "52000788";
                        sW2CDAC_21 = "51000888";
                        sW2CDAC_3 = "12200500";
                    }
                    else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "6")
                    {
                        sW2CDAC_1 = "12200699";
                        sW2CDAC_22 = "52000788";
                        sW2CDAC_21 = "51000888";
                        sW2CDAC_3 = "12200600";
                    }
                    else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "7")
                    {
                        sW2CDAC_1 = "12200799";
                        sW2CDAC_22 = "52000788";
                        sW2CDAC_21 = "51000888";
                        sW2CDAC_3 = "12200700";
                    }
                    else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "8")
                    {
                        sW2CDAC_1 = "12200899";
                        sW2CDAC_22 = "52000788";
                        sW2CDAC_21 = "51000888";
                        sW2CDAC_3 = "12200800";
                    }
                    else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "9")
                    {
                        sW2CDAC_1  = "12200999";
                        sW2CDAC_22 = "52000705";
                        sW2CDAC_21 = "51000805";
                        sW2CDAC_3  = "12200900";
                    }


                    // ------------------------------------ Line 번호 : 1  감가상각충당금 누계액 (차변)
                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.MTB01_FXLJPNODATE.GetValue().ToString().Replace("-", "").ToString()); // 작성일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(sW2CDAC_1);
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH02_FXSAUP.GetValue().ToString().Trim()); // 귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", sW2CDAC_1, "");
                    dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                        { this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI1.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                        { this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI2.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                        { this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI3.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                        { this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI4.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                        { this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI5.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                        { this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI6.SetValue(""); }
                    }

                    this.DAT02_W2VLMI1.SetValue("");
                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    // 구자산번호 가져오기
                    dt.Clear();
                    string s자산번호 = string.Empty;
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_44TEA300", this.TXT03_FXLYEAR.GetValue().ToString().Trim(), this.TXT03_FXLSEQ.GetValue().ToString().Trim());
                    dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        s자산번호 = dt.Rows[0]["FXOLDNUM"].ToString().Trim();
                        if (s자산번호 == "")
                        {
                            s자산번호 = this.TXT03_FXLYEAR.GetValue().ToString().Trim() + "-" + this.TXT03_FXLSEQ.GetValue().ToString().Trim() + "-" + this.TXT03_FXLSUBNUM.GetValue().ToString().Trim();
                        }
                        else
                        {
                            s자산번호 = s자산번호 + " : " + this.TXT03_FXLYEAR.GetValue().ToString().Trim() + "-" + this.TXT03_FXLSEQ.GetValue().ToString().Trim() + "-" + this.TXT03_FXLSUBNUM.GetValue().ToString().Trim();
                        }
                    }

                    sW2RKAC = this.TXT02_FXSNAME.GetValue().ToString().Trim() + "매각처리(" + s자산번호 + ")";

                    //sW2RKAC = this.TXT01_FXLSETTEXT.GetValue().ToString();

                    this.DAT02_W2AMDR.SetValue(this.TXT01_FXLDWAMOUNT.GetValue().ToString().Trim()); // 차변금액
                    this.DAT02_W2AMCR.SetValue("0");

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

                    // 11100302 (제예금) 01--> 02(은행)    02 ---->07(계좌번호)
                    // ------------------------------------ Line 번호 : 2  제예금 (차변)


                        iCnt = iCnt + 1;

                        dt.Clear();

                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                        this.DAT02_W2DTMK.SetValue(this.MTB01_FXLJPNODATE.GetValue().ToString().Replace("-", "").ToString()); // 작성일자
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue("3");
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue("11100302");
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(this.CBH02_FXSAUP.GetValue().ToString().Trim()); // 귀속부서

                        //관리항목 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_23N3M888", "11100302", "");
                        dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                            { this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2)); }
                            else
                            { this.DAT02_W2CDMI1.SetValue(""); }
                            if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                            { this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2)); }
                            else
                            { this.DAT02_W2CDMI2.SetValue(""); }
                            if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                            { this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2)); }
                            else
                            { this.DAT02_W2CDMI3.SetValue(""); }
                            if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                            { this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2)); }
                            else
                            { this.DAT02_W2CDMI4.SetValue(""); }
                            if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                            { this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2)); }
                            else
                            { this.DAT02_W2CDMI5.SetValue(""); }
                            if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                            { this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2)); }
                            else
                            { this.DAT02_W2CDMI6.SetValue(""); }
                        }

                        this.DAT02_W2VLMI1.SetValue(this.CBH01_FXLRECCDBK.GetValue().ToString().Trim());
                        this.DAT02_W2VLMI2.SetValue(this.CBH01_FXLRECNOAC.GetValue().ToString().Trim());
                        this.DAT02_W2VLMI3.SetValue("");
                        this.DAT02_W2VLMI4.SetValue("");
                        this.DAT02_W2VLMI5.SetValue("");
                        this.DAT02_W2VLMI6.SetValue("");

                       // sW2RKAC = this.TXT01_FXLSETTEXT.GetValue().ToString();

                        sW2RKAC = this.TXT02_FXSNAME.GetValue().ToString().Trim() + "매각대금(" + s자산번호 + ")";

                        string s합계금액 = Get_Numeric(Convert.ToString(Convert.ToDouble(this.TXT01_FXLRECAMOUNT.GetValue().ToString().Trim()) +
                                                      Convert.ToDouble(this.TXT01_FXLRECAMOUNT.GetValue().ToString().Trim()) * 0.1));

                        this.DAT02_W2AMDR.SetValue(s합계금액); // 차변금액 (입금금액+ 부가세 0.1 %)
                        this.DAT02_W2AMCR.SetValue("0");// 대변금액 

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

                        // ------------------------------------  Line 번호 : 3  유현자산처분손실 , 유형자산처분이익
                    

                    sREPJANAMOUNT = "0";
                    sW2CDAC_2 = "";

                    ////// 감소금액 - (충담금감소액 + 입금금액^부가세포함함) > 0 -- > 차변 (유현자산처분손실)
                    ////// 감소금액 - (충담금감소액 + 입금금액^부가세포함함) < 0 -- > 대변 (유형자산처분이익)
                    ////// this.TXT01_FXLAMOUNT(금액) - this.TXT01_FXLDWAMOUNT(충당금감소액)

                    // ( TXT01_FXLDWAMOUNT(충당금감소액) +  s합계금액 ) - (TXT02_FXSGETAMOUNT (취득가액 ) + 부가세) 
                    // 차변 - 대변 

                    sREPJANAMOUNT = Convert.ToString( (Convert.ToDouble(this.TXT01_FXLDWAMOUNT.GetValue().ToString().Trim()) + Convert.ToDouble(s합계금액)) -
                                                      (Convert.ToDouble(this.TXT02_FXSGETAMOUNT.GetValue().ToString().Trim()) + Convert.ToDouble(this.TXT01_FXLRECAMOUNT.GetValue().ToString().Trim()) * 0.1) );


                    if (Convert.ToDouble(sREPJANAMOUNT) > 0)
                    {
                        sW2CDAC_2 = sW2CDAC_21;
                    }
                    else
                    {
                        sW2CDAC_2 = sW2CDAC_22;
                    }

                    if (Convert.ToDouble(sREPJANAMOUNT) != 0)
                    {
                        iCnt = iCnt + 1;

                        dt.Clear();

                        string s귀속부서 = string.Empty;
                        s귀속부서 = UP_Set_DPAC(this.CBH02_FXSAUP.GetValue().ToString().Trim());

                        this.DAT02_W2SSID.SetValue(sB2SSID);
                        this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                        this.DAT02_W2DTMK.SetValue(this.MTB01_FXLJPNODATE.GetValue().ToString().Replace("-", "").ToString()); // 작성일자
                        this.DAT02_W2NOSQ.SetValue("0");
                        this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                        this.DAT02_W2IDJP.SetValue("3");
                        this.DAT02_W2NOJP.SetValue("");
                        this.DAT02_W2CDAC.SetValue(sW2CDAC_2);
                        this.DAT02_W2DTAC.SetValue("");
                        this.DAT02_W2DTLI.SetValue("");
                        this.DAT02_W2DPAC.SetValue(s귀속부서); // 귀속부서

                        //관리항목 
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_23N3M888", sW2CDAC_2, "");
                        dt = this.DbConnector.ExecuteDataTable();
                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                            { this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2)); }
                            else
                            { this.DAT02_W2CDMI1.SetValue(""); }
                            if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                            { this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2)); }
                            else
                            { this.DAT02_W2CDMI2.SetValue(""); }
                            if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                            { this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2)); }
                            else
                            { this.DAT02_W2CDMI3.SetValue(""); }
                            if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                            { this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2)); }
                            else
                            { this.DAT02_W2CDMI4.SetValue(""); }
                            if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                            { this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2)); }
                            else
                            { this.DAT02_W2CDMI5.SetValue(""); }
                            if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                            { this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2)); }
                            else
                            { this.DAT02_W2CDMI6.SetValue(""); }
                        }

                        this.DAT02_W2VLMI1.SetValue("");
                        this.DAT02_W2VLMI2.SetValue("");
                        this.DAT02_W2VLMI3.SetValue("");
                        this.DAT02_W2VLMI4.SetValue("");
                        this.DAT02_W2VLMI5.SetValue("");
                        this.DAT02_W2VLMI6.SetValue("");

                        //sW2RKAC = this.TXT01_FXLSETTEXT.GetValue().ToString();
                        sW2RKAC = this.TXT02_FXSNAME.GetValue().ToString().Trim() + "매각처리(" + s자산번호 + ")";

                        if (Convert.ToDouble(sREPJANAMOUNT) > 0)
                        {
                            this.DAT02_W2AMDR.SetValue("0"); // 차변금액 
                            this.DAT02_W2AMCR.SetValue(sREPJANAMOUNT);  // 대변금액 
                        }
                        else
                        {
                            this.DAT02_W2AMDR.SetValue(sREPJANAMOUNT); // 차변금액 
                            this.DAT02_W2AMCR.SetValue("0");  // 대변금액 
                        }

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
                    }

                    // ------------------------------------  Line 번호 : 4  기계장치,차량운반구,집기비품.... (대변)
                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.MTB01_FXLJPNODATE.GetValue().ToString().Replace("-", "").ToString()); // 작성일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(sW2CDAC_3);
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH02_FXSAUP.GetValue().ToString().Trim()); // 귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", sW2CDAC_3, "");
                    dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                        { this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI1.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                        { this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI2.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                        { this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI3.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                        { this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI4.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                        { this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI5.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                        { this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI6.SetValue(""); }
                    }

                    this.DAT02_W2VLMI1.SetValue("");
                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    //sW2RKAC = this.TXT01_FXLSETTEXT.GetValue().ToString();

                    sW2RKAC = this.TXT02_FXSNAME.GetValue().ToString().Trim() + "매각처리(" + s자산번호 + ")";

                    this.DAT02_W2AMDR.SetValue("0"); // 차변금액 
                    this.DAT02_W2AMCR.SetValue(this.TXT01_FXLAMOUNT.GetValue().ToString().Trim()); // 대변금액

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

                    // ------------------------------------  Line 번호 : 5  매출부가세 .... (대변)
                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.MTB01_FXLJPNODATE.GetValue().ToString().Replace("-", "").ToString()); // 작성일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(s부가세계정);
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH02_FXSAUP.GetValue().ToString().Trim()); // 귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", s부가세계정, "");
                    dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                        { this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI1.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                        { this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI2.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                        { this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI3.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                        { this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI4.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                        { this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI5.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                        { this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI6.SetValue(""); }
                    }

                    string s공급가액 = Get_Numeric(this.TXT01_FXLRECAMOUNT.GetValue().ToString().Trim());
                    string sVatamt = Get_Numeric(Convert.ToString(Convert.ToDouble(this.TXT01_FXLRECAMOUNT.GetValue().ToString().Trim()) * 0.1));
                    this.DAT02_W2VLMI1.SetValue("69"); // 부가세 구분  (전자)고정자산매각,기타수익
                    this.DAT02_W2VLMI2.SetValue(this.CBH01_FXLVEND.GetValue().ToString().Trim()); // 매각거래처
                    this.DAT02_W2VLMI3.SetValue(this.MTB01_FXLJPNODATE.GetValue().ToString().Replace("-", "").ToString()); // 계산서 일자
                    this.DAT02_W2VLMI4.SetValue(s공급가액); // 공급가액
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                   // sW2RKAC = this.TXT01_FXLSETTEXT.GetValue().ToString();
                    sW2RKAC = "매출부가세";
                    this.DAT02_W2AMDR.SetValue("0"); // 차변금액 
                    this.DAT02_W2AMCR.SetValue(sVatamt); // 대변금액

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
                }

                #endregion


                #region Description : 자본적지출(31)

                if (this.CBH01_FXLSETGN.GetValue().ToString() == "31")
                {
                    sW2CDAC_1 = this.CBH01_FXLCDAC.GetValue().ToString().Trim();

                    //if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "2")
                    //{
                    //    sW2CDAC_1 = "12200200";
                    //}
                    //else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "3")
                    //{
                    //    sW2CDAC_1 = "12200300";
                    //}
                    //else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "4")
                    //{
                    //    sW2CDAC_1 = "12200400";
                    //}
                    //else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "5")
                    //{
                    //    sW2CDAC_1 = "12200500";
                    //}
                    //else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "6")
                    //{
                    //    sW2CDAC_1 = "12200600";
                    //}
                    //else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "7")
                    //{
                    //    sW2CDAC_1 = "12200700";
                    //}
                    //else if (this.CBH02_FXASCLASS.GetValue().ToString().Substring(0, 1) == "8")
                    //{
                    //    sW2CDAC_1 = "12200800";
                    //}

                    // ------------------------------------ Line 번호 : 1  고정자산 (차변)
                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.DTP01_FXLSETDATE.GetString()); // 작성일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(sW2CDAC_1);
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH02_FXSAUP.GetValue().ToString().Trim()); // 귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", sW2CDAC_1, "");
                    dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                        { this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI1.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                        { this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI2.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                        { this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI3.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                        { this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI4.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                        { this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI5.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                        { this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI6.SetValue(""); }
                    }


                    this.DAT02_W2VLMI1.SetValue(this.TXT03_FXLYEAR.GetValue().ToString() + Set_Fill4(this.TXT03_FXLSEQ.GetValue().ToString()) +Set_Fill3( this.TXT03_FXLSUBNUM.GetValue().ToString())); // 자산관리번호
                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue(this.DTP01_FXLSETDATE.GetString().ToString().Trim().Substring(0, 4) + this.CBH02_FXSAUP.GetValue().ToString() + Set_Fill3(this.TXT01_FXLYSSEQ.GetValue().ToString())); // 예산세목
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    sW2RKAC = this.TXT01_FXLSETTEXT.GetValue().ToString();

                    this.DAT02_W2AMDR.SetValue(Get_Numeric(this.TXT01_FXLAMOUNT.GetValue().ToString())); // 차변금액
                    this.DAT02_W2AMCR.SetValue("0");// 차변금액

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


                    // ------------------------------------ Line 번호 : 미지급금,제예금 (대변)
                    iCnt = iCnt + 1;

                    dt.Clear();

                    //if (this.CBH01_FXLRECNOAC.GetValue().ToString() == "")
                    //{
                    //    sW2CDAC_2 = "21100601"; // 미지급본점(관1:거래처)
                    //}
                    //else
                    //{
                    //    sW2CDAC_2 = "11100302"; // 제예금 (관1 : 126004(은행) . 관2:140-000-984537(계좌번호))
                    //}

                    if (this.CBO01_FXLRECGN.GetValue().ToString() == "01") //제예금
                    {
                        sW2CDAC_2 = "11100302"; // 제예금 (관1 : 126004(은행) . 관2:140-000-984537(계좌번호))
                    }
                    else if(this.CBO01_FXLRECGN.GetValue().ToString() == "02") //미지급본점
                    {
                        sW2CDAC_2 = "21100601"; // 미지급본점(관1:거래처)
                    }
                    else
                    {
                        sW2CDAC_2 = "21101008";  //신용카드-지방세(관1 : 카드번호 . 관2:거래일자 과3: 거래처코드)
                    }

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.DTP01_FXLSETDATE.GetString()); // 작성일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(sW2CDAC_2);
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH02_FXSAUP.GetValue().ToString().Trim()); // 귀속부서

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", sW2CDAC_2, "");
                    dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                        { this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI1.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                        { this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI2.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                        { this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI3.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                        { this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI4.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                        { this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI5.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                        { this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI6.SetValue(""); }
                    }

                    string sW2RKCU = string.Empty;

                    if (sW2CDAC_2 == "11100302") // 미지급본점(관1:거래처)
                    {
                        this.DAT02_W2VLMI1.SetValue(this.CBH01_FXLRECCDBK.GetValue().ToString()); //보통예금 (관1:)은행코드
                        this.DAT02_W2VLMI2.SetValue(this.CBH01_FXLRECNOAC.GetValue().ToString()); //보통예금 (관2:)계좌번호

                        // 은행명가져 오기
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_243BQ336", this.CBH01_FXLRECCDBK.GetValue().ToString());  //ACODEMF
                        DataTable dt_T = this.DbConnector.ExecuteDataTable();
                        if (dt_T.Rows.Count > 0)
                        {
                            sW2RKCU = StringTransfer(dt_T.Rows[0]["CDDESC1"].ToString().Trim(), 20);
                        }
                        this.DAT02_W2VLMI3.SetValue("");
                    }
                    else if (sW2CDAC_2 == "21100601") // 미지급본점(관1:거래처)
                    {
                        this.DAT02_W2VLMI1.SetValue(this.CBH01_FXLVEND.GetValue().ToString());
                        this.DAT02_W2VLMI2.SetValue("");
                        this.DAT02_W2VLMI3.SetValue("");

                        sW2RKCU = "";
                    }
                    else
                    {
                        //신용카드

                        this.DAT02_W2VLMI1.SetValue(this.CBH01_FXLFILED1.GetValue().ToString()); //카드번호
                        this.DAT02_W2VLMI2.SetValue(this.DTP01_FXLSETDATE.GetString().ToString()); //거래일자
                        this.DAT02_W2VLMI3.SetValue(this.CBH01_FXLFILED2.GetValue().ToString()); //거래처코드
                    }
                    
                    this.DAT02_W2VLMI4.SetValue("");
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    sW2RKAC = this.TXT01_FXLSETTEXT.GetValue().ToString();

                    this.DAT02_W2AMDR.SetValue("0"); // 차변금액
                    this.DAT02_W2AMCR.SetValue(Get_Numeric(this.TXT01_FXLAMOUNT.GetValue().ToString())); // 대변금액

                    this.DAT02_W2CDFD.SetValue("");
                    this.DAT02_W2AMFD.SetValue("0");
                    this.DAT02_W2RKAC.SetValue(sW2RKAC);
                    this.DAT02_W2RKCU.SetValue(sW2RKCU);
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
                }

                #endregion


                #region Description : 합필 (35) ,분할 (46) 

                if (this.CBH01_FXLSETGN.GetValue().ToString() == "35" || this.CBH01_FXLSETGN.GetValue().ToString() == "46")
                {
                     sW2CDAC_1 = "12200100";
 
                    // ------------------------------------ Line 번호 : 1  (차변)
                    iCnt = iCnt + 1;

                    dt.Clear();

                    this.DAT02_W2SSID.SetValue(sB2SSID);
                    this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                    this.DAT02_W2DTMK.SetValue(this.DTP01_FXLSETDATE.GetString()); // 작성일자
                    this.DAT02_W2NOSQ.SetValue("0");
                    this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                    this.DAT02_W2IDJP.SetValue("3");
                    this.DAT02_W2NOJP.SetValue("");
                    this.DAT02_W2CDAC.SetValue(sW2CDAC_1);
                    this.DAT02_W2DTAC.SetValue("");
                    this.DAT02_W2DTLI.SetValue("");
                    this.DAT02_W2DPAC.SetValue(this.CBH02_FXSAUP.GetValue().ToString().Trim()); // 귀속부서 

                    //관리항목 
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_23N3M888", sW2CDAC_1, "");
                    dt = this.DbConnector.ExecuteDataTable();
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                        { this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI1.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                        { this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI2.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                        { this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI3.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                        { this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI4.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                        { this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI5.SetValue(""); }
                        if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                        { this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2)); }
                        else
                        { this.DAT02_W2CDMI6.SetValue(""); }
                    }

                    

                    //예산순번 가져 오기
                    string sP1SEQ = "";
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_34OAF563",this.DTP01_FXLSETDATE.GetString().Trim().Substring(0,4) ,this.CBH02_FXSAUP.GetValue().ToString().Trim(), sW2CDAC_1);
                    DataTable dt_scd = this.DbConnector.ExecuteDataTable();
                    if (dt_scd.Rows.Count > 0)
                    {
                        sP1SEQ = dt.Rows[0]["P1SEQ"].ToString().Trim();
                    }

                    this.DAT02_W2VLMI1.SetValue(this.TXT03_FXLYEAR.GetValue().ToString() + Set_Fill4(this.TXT03_FXLSEQ.GetValue().ToString()) + Set_Fill3(this.TXT03_FXLSUBNUM.GetValue().ToString())) ;
                    this.DAT02_W2VLMI2.SetValue("");
                    this.DAT02_W2VLMI3.SetValue("");
                    this.DAT02_W2VLMI4.SetValue(this.DTP01_FXLSETDATE.GetString().Trim().Substring(0, 4) + this.CBH02_FXSAUP.GetValue().ToString().Trim() + sP1SEQ);
                    this.DAT02_W2VLMI5.SetValue("");
                    this.DAT02_W2VLMI6.SetValue("");

                    sW2RKAC = this.TXT01_FXLSETTEXT.GetValue().ToString();

                    this.DAT02_W2AMDR.SetValue(this.TXT01_FXLAMOUNT.GetValue().ToString().Trim()); // 차변금액(금액 필드)
                    this.DAT02_W2AMCR.SetValue("0");

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

                    // 대변 생성
                    string sSETGN = "";
                    if (this.CBH01_FXLSETGN.GetValue().ToString() == "35")
                    {
                        sSETGN = "45";
                    }
                    else
                    {
                        sSETGN = "36";
                    }

                    DataSet ds = new DataSet();
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(
                                           "TY_P_AC_34N5H558",
                                           this.TXT03_FXLYEAR.GetValue().ToString() + Set_Fill4(this.TXT03_FXLSEQ.GetValue().ToString()) + Set_Fill3(this.TXT03_FXLSUBNUM.GetValue().ToString()),
                                           sSETGN
                                           );

                    ds = this.DbConnector.ExecuteDataSet();
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            iCnt = iCnt + 1;

                            dt.Clear();

                            this.DAT02_W2SSID.SetValue(sB2SSID);
                            this.DAT02_W2DPMK.SetValue(sW2DPMK); // 작성부서
                            this.DAT02_W2DTMK.SetValue(this.DTP01_FXLSETDATE.GetString()); // 작성일자
                            this.DAT02_W2NOSQ.SetValue("0");
                            this.DAT02_W2NOLN.SetValue(iCnt.ToString());
                            this.DAT02_W2IDJP.SetValue("3");
                            this.DAT02_W2NOJP.SetValue("");
                            this.DAT02_W2CDAC.SetValue(sW2CDAC_1);
                            this.DAT02_W2DTAC.SetValue("");
                            this.DAT02_W2DTLI.SetValue("");
                            this.DAT02_W2DPAC.SetValue(this.CBH02_FXSAUP.GetValue().ToString().Trim()); // 귀속부서

                            //관리항목 
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_AC_23N3M888", sW2CDAC_1, "");
                            dt = this.DbConnector.ExecuteDataTable();
                            if (dt.Rows.Count > 0)
                            {
                                if (dt.Rows[0]["A1CDMI1"].ToString().Length > 0)
                                { this.DAT02_W2CDMI1.SetValue(dt.Rows[0]["A1CDMI1"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI1.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI2"].ToString().Length > 0)
                                { this.DAT02_W2CDMI2.SetValue(dt.Rows[0]["A1CDMI2"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI2.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI3"].ToString().Length > 0)
                                { this.DAT02_W2CDMI3.SetValue(dt.Rows[0]["A1CDMI3"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI3.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI4"].ToString().Length > 0)
                                { this.DAT02_W2CDMI4.SetValue(dt.Rows[0]["A1CDMI4"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI4.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI5"].ToString().Length > 0)
                                { this.DAT02_W2CDMI5.SetValue(dt.Rows[0]["A1CDMI5"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI5.SetValue(""); }
                                if (dt.Rows[0]["A1CDMI6"].ToString().Length > 0)
                                { this.DAT02_W2CDMI6.SetValue(dt.Rows[0]["A1CDMI6"].ToString().Substring(0, 2)); }
                                else
                                { this.DAT02_W2CDMI6.SetValue(""); }
                            }

                            this.DAT02_W2VLMI1.SetValue("");
                            this.DAT02_W2VLMI2.SetValue("");
                            this.DAT02_W2VLMI3.SetValue("");
                            this.DAT02_W2VLMI4.SetValue("");
                            this.DAT02_W2VLMI5.SetValue("");
                            this.DAT02_W2VLMI6.SetValue("");

                            sW2RKAC = ds.Tables[0].Rows[i]["FXLSETTEXT"].ToString().Trim();

                            this.DAT02_W2AMDR.SetValue("0"); // 차변금액 
                            this.DAT02_W2AMCR.SetValue(ds.Tables[0].Rows[i]["FXLAMOUNT"].ToString().Trim());// 대변금액 

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
                        }

                    }                            

                }

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
                if (this.CBH01_FXLSETGN.GetValue().ToString() == "41" || this.CBH01_FXLSETGN.GetValue().ToString() == "42" || this.CBH01_FXLSETGN.GetValue().ToString() == "44")
                {
                    this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, Employer.EmpNo, "A",
                                                                sW2DPMK, this.MTB01_FXLJPNODATE.GetValue().ToString().Replace("-", "").ToString(), "", "",
                                                                "", "", Employer.EmpNo);
                }
                else
                {
                    this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, Employer.EmpNo, "A",
                                            sW2DPMK, this.DTP01_FXLSETDATE.GetString(), "", "",
                                            "", "", Employer.EmpNo);
                };

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
                                this.TXT01_FXLUWJPNO.SetValue(sJpno.Replace("-","").ToString()+"01");
                                //this.BTN61_BTNJUNPYO.Visible = false;
                            }
                        }
                    }
                }

                if (bJunPyoFlag)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3423Y416", this.TXT01_FXLUWJPNO.GetValue().ToString(),
                                                                this.TXT01_FXLUWJPNO.GetValue().ToString().Replace("-","").ToString().Substring(6,8),  
                                                                TYUserInfo.EmpNo,
                                                                this.TXT03_FXLYEAR.GetValue().ToString(),
                                                                this.TXT03_FXLSEQ.GetValue().ToString(),
                                                                this.TXT03_FXLSUBNUM.GetValue().ToString(),
                                                                this.DTP01_FXLSETDATE.GetString().ToString(),
                                                                this.CBH01_FXLSETGN.GetValue().ToString());
                    this.DbConnector.ExecuteTranQueryList();

                    // 증감파일에 등록 
                    // 폐기(41),매각(42),기증(44)
                    if (this.CBH01_FXLSETGN.GetValue().ToString() == "41" || this.CBH01_FXLSETGN.GetValue().ToString() == "42" || this.CBH01_FXLSETGN.GetValue().ToString() == "44")
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_35N8Y727", this.TXT03_FXLYEAR.GetValue().ToString(),
                                                                    this.TXT03_FXLSEQ.GetValue().ToString(),
                                                                    this.TXT03_FXLSUBNUM.GetValue().ToString(),
                                                                    this.DTP01_FXLSETDATE.GetString().ToString(),
                                                                    "",
                                                                    this.CBH02_FXITEMCODE.GetValue().ToString(),
                                                                    "0",
                                                                    this.TXT01_FXLAMOUNT.GetValue().ToString(),
                                                                    this.TXT01_FXLSETTEXT.GetValue().ToString()
                                                                    );
                        this.DbConnector.ExecuteTranQueryList();
                    }


                    // 합필 , 분할 처리
                    if (this.CBH01_FXLSETGN.GetValue().ToString() == "35" || this.CBH01_FXLSETGN.GetValue().ToString() == "46")
                    {
                        string sSETGN = "";

                        if (this.CBH01_FXLSETGN.GetValue().ToString() == "35")
                        {
                            sSETGN = "45";
                        }
                        else
                        {
                            sSETGN = "36";
                        };

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_34N55557", this.TXT01_FXLUWJPNO.GetValue().ToString().Replace("-",""),
                                                                    this.TXT03_FXLYEAR.GetValue().ToString() + Set_Fill4(this.TXT03_FXLSEQ.GetValue().ToString()) + Set_Fill3(this.TXT03_FXLSUBNUM.GetValue().ToString()),
                                                                    sSETGN);
                        this.DbConnector.ExecuteTranQueryList();
                    }


                    fsGubn = "D";
                    this.BTN61_BTNJUNPYO.Text = "전표취소";
                    this.BTN61_BTNJUNPRT.Visible = true;
                } 
                #endregion
            }
            else
            {
                #region Description : 전표 취소

                //미승인전표 -> 임시파일 입력 (전표삭제)
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29C7K957", sB2SSID, this.TXT01_FXLUWJPNO.GetValue().ToString().Replace("-", "").Substring(0, 6), this.TXT01_FXLUWJPNO.GetValue().ToString().Replace("-", "").Substring(6, 8), this.TXT01_FXLUWJPNO.GetValue().ToString().Replace("-", "").Substring(14, 3));
                //this.DbConnector.ExecuteTranQueryList();
                ////미승인 SP호출 파일 입력
                //this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_29C7O959", sB2SSID, this.ProgramNo, Employer.EmpNo, "D",
                                                 sW2DPMK, this.DTP01_FXLSETDATE.GetString(), "", "",
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
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_3423Y416", "",
                                                                "",
                                                                TYUserInfo.EmpNo,
                                                                this.TXT03_FXLYEAR.GetValue().ToString(),
                                                                this.TXT03_FXLSEQ.GetValue().ToString(),
                                                                this.TXT03_FXLSUBNUM.GetValue().ToString(),
                                                                this.DTP01_FXLSETDATE.GetString().ToString(),
                                                                this.CBH01_FXLSETGN.GetValue().ToString());
                    this.DbConnector.ExecuteTranQueryList();

                    
                    // 합필 , 분할 처리
                    if (this.CBH01_FXLSETGN.GetValue().ToString() == "35" || this.CBH01_FXLSETGN.GetValue().ToString() == "46")
                    {
                        string sSETGN = "";

                        if (this.CBH01_FXLSETGN.GetValue().ToString() == "35" )
                        {
                            sSETGN = "45";
                        }
                        else
                        {
                            sSETGN = "36";
                        };

                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_34N55557", "",
                                                                     this.TXT03_FXLYEAR.GetValue().ToString() + Set_Fill4(this.TXT03_FXLSEQ.GetValue().ToString()) + Set_Fill3( this.TXT03_FXLSUBNUM.GetValue().ToString()),
                                                                     sSETGN);
                        this.DbConnector.ExecuteTranQueryList();
                    }


                    // 증감파일에 삭제 
                    // 폐기(41),매각(42),기증(44)
                    if (this.CBH01_FXLSETGN.GetValue().ToString() == "41" || this.CBH01_FXLSETGN.GetValue().ToString() == "42" || this.CBH01_FXLSETGN.GetValue().ToString() == "44")
                    {
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_AC_35N8Z728", this.TXT03_FXLYEAR.GetValue().ToString(),
                                                                    this.TXT03_FXLSEQ.GetValue().ToString(),
                                                                    this.TXT03_FXLSUBNUM.GetValue().ToString(),
                                                                    this.DTP01_FXLSETDATE.GetString().ToString()
                                                                    );
                        this.DbConnector.ExecuteTranQueryList();
                    }

                    this.ShowMessage("TY_M_AC_25O8K620");

                    // this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    fsGubn = "A";
                    this.TXT01_FXLUWJPNO.SetValue("");
                    this.BTN61_BTNJUNPYO.Text = "전표생성";

                    this.BTN61_BTNJUNPRT.Visible = false;

                    // this.Close();
                } 

                #endregion
            }
        }
        #endregion


        #region Description : 전표발행 ProcessCheck 이벤트
        private void BTN61_BTNJUNPYO_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            if (this.CBH01_FXLSETGN.GetValue().ToString().Substring(0, 2) == "36" || this.CBH01_FXLSETGN.GetValue().ToString().Substring(0, 2) == "45")
            {
                this.ShowCustomMessage("전표를 처리할수 없는 자료 입니다.(합필=35,분할=46) 가능", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            // 폐기,매각 일때 (그룹웨어 결재 처리가 우선임)
            if (this.CBH01_FXLSETGN.GetValue().ToString() == "41" || this.CBH01_FXLSETGN.GetValue().ToString() == "42" || this.CBH01_FXLSETGN.GetValue().ToString() == "44")
            {
                if (this.TXT01_FXLGRURL.GetValue().ToString() == "")
                {
                    this.ShowCustomMessage("전표를 처리할수 없음(결재 처리후 작업가능).", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                if (this.MTB01_FXLJPNODATE.GetValue().ToString().Replace("-", "").ToString() == "")
                {
                    this.ShowCustomMessage("전표를 처리할수 없음(전표결재 일자를 입력하세요).", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            // 폐기,매각 일때 (그룹웨어 결재 처리가 우선임)
            if (this.CBH01_FXLSETGN.GetValue().ToString() == "41" || this.CBH01_FXLSETGN.GetValue().ToString() == "42" || this.CBH01_FXLSETGN.GetValue().ToString() == "44")
            {

                if (this.MTB01_FXLJPNODATE.GetValue().ToString().Replace("-", "").ToString() == "")
                {
                    this.MTB01_FXLJPNODATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                }
            }


            if (this.CBH01_FXLSETGN.GetValue().ToString().Substring(0, 2) == "63" )
            {
                this.ShowCustomMessage("전표를 처리할수 없는 자료 입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (this.CBH01_FXLSETGN.GetValue().ToString().Substring(0, 1) != "3" && this.CBH01_FXLSETGN.GetValue().ToString().Substring(0, 1) != "4")
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (this.TXT01_FXLUWJPNO.GetValue().ToString() == "")  // 생성
            {
                if (!this.ShowMessage("TY_M_AC_25O8J618"))
                {
                    e.Successed = false;
                    return;
                }

                if (this.CBH01_FXLSETGN.GetValue().ToString() == "35" || this.CBH01_FXLSETGN.GetValue().ToString() == "46")
                {
                    string sSETGN = "";
                    if (this.CBH01_FXLSETGN.GetValue().ToString() == "35")
                    {
                        sSETGN = "45";
                    }
                    else
                    {
                        sSETGN = "36";
                    }

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(
                                           "TY_P_AC_34N5H558",
                                           this.TXT03_FXLYEAR.GetValue().ToString() + Set_Fill4(this.TXT03_FXLSEQ.GetValue().ToString()) + Set_Fill3(this.TXT03_FXLSUBNUM.GetValue().ToString()),
                                           sSETGN
                                           );

                    DataSet ds = this.DbConnector.ExecuteDataSet();
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        if (sSETGN == "35")
                        {
                            this.ShowCustomMessage("합필(출)자료가 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                        else
                        {
                            this.ShowCustomMessage("분할 자료가 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                        e.Successed = false;
                        return;
                    }

                    //예산순번 가져 오기
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_34OAF563",this.DTP01_FXLSETDATE.GetString().Trim().Substring(0,4) ,this.CBH02_FXSAUP.GetValue().ToString().Trim(), "12200100");
                    DataTable dt_scd = this.DbConnector.ExecuteDataTable();
                    if (dt_scd.Rows.Count != 0)
                    {
                        string sMSG ="토지관련 예산이 미존재("+this.DTP01_FXLSETDATE.GetString().Trim().Substring(0,4)+"-"+this.CBH02_FXSAUP.GetValue().ToString().Trim()+"-12200100)";
                        this.ShowCustomMessage(sMSG, "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
                }

                //사번 조회
                this.DbConnector.CommandClear();
                //this.DbConnector.Attach("TY_P_GB_24G9S659", Employer.EmpNo.ToString().Trim());  //INKIBNMF
                this.DbConnector.Attach("TY_P_GB_4CVJ7024", this.MTB01_FXLJPNODATE.GetValue().ToString().Replace("-", ""), Employer.EmpNo.ToString().Trim());  //INKIBNMF

                DataTable dt_sabun = this.DbConnector.ExecuteDataTable();
                if (dt_sabun.Rows.Count == 0)
                {
                    this.ShowCustomMessage("사번이 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }
            else // 취소
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B7BT153", this.TXT01_FXLUWJPNO.GetValue().ToString().Replace("-", "").Substring(0, 6), this.TXT01_FXLUWJPNO.GetValue().ToString().Replace("-", "").Substring(6, 8), this.TXT01_FXLUWJPNO.GetValue().ToString().Replace("-", "").Substring(14, 3)); // ADSLGLF
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

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_3439D418", 
                                                            this.TXT03_FXLYEAR.GetValue().ToString(),
                                                            this.TXT03_FXLSEQ.GetValue().ToString(),
                                                            this.TXT03_FXLSUBNUM.GetValue().ToString(),
                                                            this.TXT01_FXLUWJPNO.GetValue().ToString().Replace("-", "").Substring(6, 6));
                DataTable dt_repaymm = this.DbConnector.ExecuteDataTable();
                if (dt_repaymm.Rows.Count > 0)
                {
                    this.ShowCustomMessage("전표생성일 이후 월상각 자료 존재 전표삭제 불가", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }

                if (!this.ShowMessage("TY_M_AC_25O8K619"))
                {
                    e.Successed = false;
                    return;
                }

            }

        }
        #endregion

        #region Description : 자산번호 팝업 버튼 처리
        private void BTN61_INQ_FXM_Click(object sender, EventArgs e)
        {
            TYAZHF05C1 popup = new TYAZHF05C1();
            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_FXLJASANNUM.SetValue(popup.fsASNUM);
            }
        } 
        #endregion


        #region Description : 전표 출력  ProcessCheck 이벤트
        private void BTN61_BTNJUNPRT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.TXT01_FXLUWJPNO.GetValue().ToString() != "")
            {
                string sJPNO = this.TXT01_FXLUWJPNO.GetValue().ToString().Replace("-", "");

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B7BT153", sJPNO.Substring(0, 6), sJPNO.Substring(6, 8), sJPNO.Substring(14, 3)); // ADSLGLF
                DataTable dt_adsl = this.DbConnector.ExecuteDataTable();
                if (dt_adsl.Rows.Count == 0)
                {
                    this.ShowCustomMessage("미승인전표 미존재 처리 할수 없음(전표번호 확인)", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region Description : 전표 출력 
        private void BTN61_BTNJUNPRT_Click(object sender, EventArgs e)
        {
            if (this.TXT01_FXLUWJPNO.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_2AU2M916",
                    this.TXT01_FXLUWJPNO.GetValue().ToString().Replace("-", "").Substring(0, 6),
                    this.TXT01_FXLUWJPNO.GetValue().ToString().Replace("-", "").Substring(6, 8),
                    this.TXT01_FXLUWJPNO.GetValue().ToString().Replace("-", "").Substring(14, 3),
                    this.TXT01_FXLUWJPNO.GetValue().ToString().Replace("-", "").Substring(14, 3)
                    );


                if (Convert.ToDouble(this.TXT01_FXLUWJPNO.GetValue().ToString().Replace("-", "").Substring(6, 4)) > 2014)
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
            else
            {
                this.ShowMessage("TY_M_MR_31933569");
                return;
            }

        }
        #endregion

        private void BTN61_INQ_BUDGET_Click(object sender, EventArgs e)
        {
            TYAZHF06C1 popup = new TYAZHF06C1(this.DTP01_FXLSETDATE.GetValue().ToString().Trim(), 
                                              this.CBH02_FXSAUP.GetValue().ToString().Trim(),
                                              this.CBH01_FXLCDAC.GetValue().ToString().Trim());

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_FXLYSSEQ.SetValue(popup.fsRt_num);
                this.TXT01_FXLYSSEQ.SetValue(popup.fsRt_seq);
                this.TXT01_BUDNAME.SetValue(popup.fsRt_name);

            }

            this.SetFocus(this.TXT01_FXLYSSEQ);
        }

        #region Description : 전표 출력  ProcessCheck 이벤트
        private void BTN61_INQ_BUDGET_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string sBUDGB = string.Empty;

            if (this.DTP01_FXLSETDATE.GetValue().ToString().Trim() == "" || this.CBH02_FXSAUP.GetValue().ToString().Trim() == "" 
                 || this.CBH01_FXLCDAC.GetValue().ToString().Trim() == "")
            {
                this.ShowCustomMessage("예산조회시 (설정일자,사업부,계정과목) 입력 필수", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                this.SetFocus(this.CBH01_FXLCDAC);
                return;
            }

            if (this.CBH01_FXLCDAC.GetValue().ToString().Trim() != "")
            {
                sBUDGB = UP_Set_BudGetTab(this.CBH01_FXLCDAC.GetValue().ToString().Trim());

                if (sBUDGB == "00")
                {
                    this.ShowCustomMessage("세목 예산계정이 아닙니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    this.SetFocus(this.CBH01_FXLCDAC);
                    return;
                }
            }
        }
        #endregion

        #region Description : 귀속부서 세팅
        private string UP_Set_DPAC(string sDPAC)
        {
            string sValue = "";
            //접대비 체크
            switch (sDPAC.Substring(0, 2))
            {
                case "G1":
                case "A1":
                    return "A10000";

                case "C1":
                    return "C10000";

                case "A5":
                case "B8":
                case "B7":
                case "B6":
                case "B1":
                case "O1":
                case "O2":
                case "O3":
                case "O4":
                    return "A50000";

                case "S1":
                    return "S10000";
                case "S3":
                    return "S40000";

                case "T1":
                    return "T10000";
                case "T4":
                    return "T40000";

                default:
                    sValue = sDPAC;
                    break;
            }

            return sValue;
        }
        #endregion

        #region Description : 계정과목 예산 Tab 선택 함수
        private string UP_Set_BudGetTab(string sCDAC)
        {
            string sValue = "";

            //접대비 체크
            switch (sCDAC.Substring(0, 6))
            {
                case "442120":
                case "424120":
                case "441120":
                case "441220":
                    return "01";
                case "442121":
                case "424121":
                case "441121":
                case "441221":
                    return "02";
                default:
                    sValue = "00";
                    break;
            }

            // 운영비 및 분임 토의비

            switch (sCDAC)
            {
                case "44211110":
                case "42411110":
                case "44111110":
                case "44121110":
                    return "03";
                case "44212903":
                case "42412903":
                case "44112903":
                case "44122903":
                    return "04";
                default:
                    sValue = "00";
                    break;
            }

            //기타 세목
            switch (sCDAC)
            {
                case "12200100":
                case "12200200":
                case "12200300":
                case "12200400":
                case "12200500":
                case "12200600":
                case "12200700":
                case "12200800":
                case "12200900":
                case "12210000":
                    return "11";
                case "42411503":
                case "44121503":
                case "44111503":
                case "44211503":
                    return "13";
                case "42412901":
                case "44122901":
                case "44112901":
                case "44212901":
                    return "15";
                case "42412803":
                case "44122803":
                case "44112803":
                case "44212803":
                    return "16";
                default:
                    if (Convert.ToDouble(sCDAC) > 52001500 &&
                        Convert.ToDouble(sCDAC) < 52001599)
                    {
                        sValue = "12";
                    }
                    else if (Convert.ToDouble(sCDAC) > 42411800 &&
                        Convert.ToDouble(sCDAC) < 42411899)
                    {
                        sValue = "14";
                    }
                    else if (Convert.ToDouble(sCDAC) > 44121800 &&
                        Convert.ToDouble(sCDAC) < 44121899)
                    {
                        sValue = "14";
                    }
                    else if (Convert.ToDouble(sCDAC) > 44111800 &&
                        Convert.ToDouble(sCDAC) < 44111899)
                    {
                        sValue = "14";
                    }
                    else if (Convert.ToDouble(sCDAC) > 44211800 &&
                        Convert.ToDouble(sCDAC) < 44211899)
                    {
                        sValue = "14";
                    }
                    if (sValue != "00") return sValue;
                    break;
            }

            //여비교통비
            switch (sCDAC)
            {
                case "42411201":
                case "44121201":
                case "44111201":
                case "44211201":
                    return "21";
                case "42411202":
                case "44121202":
                case "44111202":
                case "44211202":
                    return "22";
            }
            // 소모성 비품
            switch (sCDAC)
            {
                case "42413301":
                case "44123301":
                case "44113301":
                case "44213301":
                    return "31";
                case "42413388": // 기타소모성 피품
                case "44123388":
                case "44113388":
                case "44213388":
                    return "32";
            }


            /*  전산관련  --->   35 :소프트웨어개발 ,전산기기판매,소프트웨어개발외상매출 ,전산기기판매외상매출금 ,소프트웨어 매출원가
            *                     ( 41300100 : 41300200 : 11100485 ,11100486 : 42300000 ) */

            switch (sCDAC)
            {
                case "41300100":
                case "41300200":
                case "11100485":
                case "11100486":
                case "42300000":
                    return "35";
            }
            return sValue;
        }
        #endregion
        
        #region Description : 입금형태에 따라 카드컨트롤 세팅
        private void UP_Set_CardControl(bool bValue1, bool bValue2)
        {
            LBL51_FXLFILED1.Visible = bValue1;
            LBL51_FXLFILED2.Visible = bValue1;
            CBH01_FXLFILED1.Visible = bValue1;
            CBH01_FXLFILED2.Visible = bValue1;

            LBL51_FXLRECCDBK.Visible = bValue2;
            LBL51_FXLRECNOAC.Visible = bValue2;
            CBH01_FXLRECCDBK.Visible = bValue2;
            CBH01_FXLRECNOAC.Visible = bValue2;
        }
        #endregion

        #region Description : 입금형태 CBO01_FXLRECGN_SelectedIndexChanged
        private void CBO01_FXLRECGN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBO01_FXLRECGN.GetValue().ToString() != "03")
            {
                UP_Set_CardControl(false, true);
            }
            else
            {
                UP_Set_CardControl(true, false);
            }
        }
        #endregion
    }
}

