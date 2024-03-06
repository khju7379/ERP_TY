using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;


namespace TY.ER.AC00
{
    /// <summary>
    /// 고정자산관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.12.05 14:17
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2C352805 : 고정자산 Master 조회
    ///  TY_P_AC_2C532926 : 고정자산 상세내역 조회
    ///  TY_P_AC_2C62T962 : 고정자산 마스타 저장
    ///  TY_P_AC_2C62V963 : 고정자산 마스타 수정
    ///  TY_P_AC_2C62Y964 : 고정자산 마스타 삭제
    ///  TY_P_AC_2C63N965 : 고정자산 자산순번 생성
    ///  TY_P_AC_2C664971 : 고정자산 디테일 저장
    ///  TY_P_AC_2C66B972 : 고정자산 디테일 수정
    ///  TY_P_AC_2C66C973 : 고정자산 디테일 삭제
    ///  TY_P_AC_2C75X983 : 고정자산 가족번호 생성
    ///  TY_P_AC_2CA37009 : 고정자산 마스타 수정
    ///  TY_P_AC_2CA6K021 : 고정자산 마스타 확인
    ///  TY_P_AC_2CB19061 : 고정자산 층별면적 조회
    ///  TY_P_AC_2CB40071 : 고정자산 층별면적 삭제
    ///  TY_P_AC_2CB47069 : 고정자산 층별면적 등록
    ///  TY_P_AC_2CB49070 : 고정자산 층별면적 수정
    ///  TY_P_AC_2CB4B072 : 고정자산 층별면적 순번 생성
    ///  TY_P_AC_2CB6A075 : 고정자산 디테일 수정
    ///  TY_P_AC_2CB7K078 : 고정자산 층별면적 존재체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2C533929 : 고정자산 층별면적 조회
    ///  TY_S_AC_2C535927 : 고정자산 상세내역 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2584Y096 : 승인일자를 확인하세요
    ///  TY_M_AC_2C65V970 : 승인사번을 확인하세요!
    ///  TY_M_AC_2CB7M081 : 층별면적 자료가 존재합니다 삭제후 작업하세요!
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_23S40973 : 동일한 코드가 존재합니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  NEW : 신규
    ///  REM : 삭제
    ///  SAV : 저장
    ///  FXAFULLNUM : 자산번호
    ///  FXAPPSABUN : 승인사번
    ///  FXASCLASS : 자산분류코드
    ///  FXBUYDPMK : 요청부서
    ///  FXITEMCODE : 품목코드
    ///  FXSAUP : 귀속사업부
    ///  FXSBUYCOM : 구입처
    ///  FXSCHGCODE : 최종변경코드
    ///  FXSCHGDPMK : 최종변경부서
    ///  FXSFITSITE : 설치위치
    ///  FXSMATERIAL : 재질
    ///  FXSSTRUCT : 구조
    ///  FXSUNIT : 단위
    ///  FXUNIT : 단위
    ///  FXAPPGUBN : 승인유무
    ///  FXREPAYWAY : 상각방식
    ///  FXSBUYGN : 구매구분
    ///  FXAPPDATE : 승인일자
    ///  FXAPPNUM : 결재번호
    ///  FXGETAMOUNT : 취득금액
    ///  FXGETDATE : 취득일자
    ///  FXJPNO : 전표번호
    ///  FXLIFEYEAR : 내용년수
    ///  FXMAMMALAMOUNT : 기말잔액
    ///  FXMDECAMOUNT : 당기감소액
    ///  FXMINCAMOUNT : 당기증가액
    ///  FXMJUNKIDECSUM : 전기누계감소액
    ///  FXMJUNKIINCSUM : 전기누계증가액
    ///  FXMJUNKIREPAMOUNT : 전기상각누계액
    ///  FXMREPAMOUNT : 당월상각액
    ///  FXMREPAMOUNTSUM : 상각금액계
    ///  FXMREPJANAMOUNT : 미상각잔액
    ///  FXMYYMM : 상각년월
    ///  FXNAME : 자산명
    ///  FXORGDATE : 당초취득일자
    ///  FXQTY : 수량
    ///  FXSASDATE1 : 보증기간S
    ///  FXSASDATE2 : 보증기간E
    ///  FXSBIGO : 비고
    ///  FXSCHGDATE : 최종변경일자
    ///  FXSCHGSITE : 최종변경위치
    ///  FXSCHGTEXT : 최종변경사유
    ///  FXSEQ : 자산순번
    ///  FXSEXDATE : 검사일
    ///  FXSGETAMOUNT : 취득금액
    ///  FXSMAKERCOM : 제조사
    ///  FXSNAME : 자산명
    ///  FXSQTY : 수량(면적)
    ///  FXSSEQ : 자산순번
    ///  FXSSUBNUM : 가족코드
    ///  FXSTAND : 규격
    ///  FXSTOCKNUM : 입고번호
    ///  FXSUSETEXT : 사용용도
    ///  FXSYEAR : 자산년도
    ///  FXUSED : 용도(입고명)
    ///  FXYEAR : 자산년도
    /// </summary>

    public partial class TYACHF004I : TYBase
    {
        private string sSABUN = string.Empty;

        private TYData DAT10_FXYEAR;
        private TYData DAT10_FXSEQ;
        //private TYData DAT10_FXASCLASS;
        private TYData DAT10_FXNAME;
        private TYData DAT10_FXSTAND;
        private TYData DAT10_FXUNIT;
        private TYData DAT10_FXQTY;
        private TYData DAT10_FXGETAMOUNT;
        private TYData DAT10_FXSAUP;
        private TYData DAT10_FXBUYDPMK;
        private TYData DAT10_FXGETDATE;
        private TYData DAT10_FXORGDATE;
        private TYData DAT10_FXLIFEYEAR;
        private TYData DAT10_FXSTOCKNUM;
        private TYData DAT10_FXAPPNUM;
        private TYData DAT10_FXJPNO;
        private TYData DAT10_FXITEMCODE;
        private TYData DAT10_FXUSED;
        private TYData DAT10_FXREPAYWAY;
        private TYData DAT10_FXAPPGUBN;
        private TYData DAT10_FXAPPSABUN;
        private TYData DAT10_FXAPPDATE;
        private TYData DAT10_FXHISAB;

        private TYData DAT11_FXSYEAR;
        private TYData DAT11_FXSSEQ;
        private TYData DAT11_FXSSUBNUM;
        private TYData DAT11_FXSNAME;
        private TYData DAT11_FXSCLASS;
        private TYData DAT11_FXSGETDATE;
        private TYData DAT11_FXSLIFEYEAR;
        private TYData DAT11_FXSGETAMOUNT;
        private TYData DAT11_FXSQTY;
        private TYData DAT11_FXSUNIT;
        private TYData DAT11_FXSFITSITE;
        private TYData DAT11_FXSBUYGN;
        private TYData DAT11_FXSMAKERCOM;
        private TYData DAT11_FXSBUYCOM;
        private TYData DAT11_FXSASDATE1;
        private TYData DAT11_FXSASDATE2;
        private TYData DAT11_FXSEXDATE;
        private TYData DAT11_FXSCHGCODE;
        private TYData DAT11_FXSCHGTEXT;
        private TYData DAT11_FXSCHGDATE;
        private TYData DAT11_FXSCHGDPMK;
        private TYData DAT11_FXSCHGSITE;
        private TYData DAT11_FXSUSETEXT;
        private TYData DAT11_FXSSTRUCT;
        private TYData DAT11_FXSMATERIAL;
        private TYData DAT11_FXSBIGO;
        private TYData DAT11_FXSHISAB;
        private TYData DAT11_FXSJPNO;


        private string fsFXYEAR;
        private string fsFXSEQ;
        private string fsTabCtl;


        #region  Description : 폼 로드 이벤트
        public TYACHF004I(string sYEAR, string sSEQ)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.fsFXYEAR = sYEAR;
            this.fsFXSEQ = sSEQ;

            this.DAT10_FXYEAR = new TYData("DAT10_FXYEAR", null);
            this.DAT10_FXSEQ = new TYData("DAT10_FXSEQ", null);
            //this.DAT10_FXASCLASS = new TYData("DAT10_FXASCLASS", null);
            this.DAT10_FXNAME = new TYData("DAT10_FXNAME", null);
            this.DAT10_FXSTAND = new TYData("DAT10_FXSTAND", null);
            this.DAT10_FXUNIT = new TYData("DAT10_FXUNIT", null);
            this.DAT10_FXQTY = new TYData("DAT10_FXQTY", null);
            this.DAT10_FXGETAMOUNT = new TYData("DAT10_FXGETAMOUNT", null);
            this.DAT10_FXSAUP = new TYData("DAT10_FXSAUP", null);
            this.DAT10_FXBUYDPMK = new TYData("DAT10_FXBUYDPMK", null);
            this.DAT10_FXGETDATE = new TYData("DAT10_FXGETDATE", null);
            this.DAT10_FXORGDATE = new TYData("DAT10_FXORGDATE", null);
            this.DAT10_FXLIFEYEAR = new TYData("DAT10_FXLIFEYEAR", null);
            this.DAT10_FXSTOCKNUM = new TYData("DAT10_FXSTOCKNUM", null);
            this.DAT10_FXAPPNUM = new TYData("DAT10_FXAPPNUM", null);
            this.DAT10_FXJPNO = new TYData("DAT10_FXJPNO", null);
            this.DAT10_FXITEMCODE = new TYData("DAT10_FXITEMCODE", null);
            this.DAT10_FXUSED = new TYData("DAT10_FXUSED", null);
            this.DAT10_FXREPAYWAY = new TYData("DAT10_FXREPAYWAY", null);
            this.DAT10_FXAPPGUBN = new TYData("DAT10_FXAPPGUBN", null);
            this.DAT10_FXAPPSABUN = new TYData("DAT10_FXAPPSABUN", null);
            this.DAT10_FXAPPDATE = new TYData("DAT10_FXAPPDATE", null);
            this.DAT10_FXHISAB = new TYData("DAT10_FXHISAB", null);

            this.DAT11_FXSYEAR = new TYData("DAT11_FXSYEAR", null);
            this.DAT11_FXSSEQ = new TYData("DAT11_FXSSEQ", null);
            this.DAT11_FXSSUBNUM = new TYData("DAT11_FXSSUBNUM", null);
            this.DAT11_FXSNAME = new TYData("DAT11_FXSNAME", null);
            this.DAT11_FXSCLASS = new TYData("DAT11_FXSCLASS", null);

            this.DAT11_FXSGETDATE = new TYData("DAT11_FXSGETDATE", null);
            this.DAT11_FXSLIFEYEAR = new TYData("DAT11_FXSLIFEYEAR", null);

            this.DAT11_FXSGETAMOUNT = new TYData("DAT11_FXSGETAMOUNT", null);
            this.DAT11_FXSQTY = new TYData("DAT11_FXSQTY", null);
            this.DAT11_FXSUNIT = new TYData("DAT11_FXSUNIT", null);
            this.DAT11_FXSFITSITE = new TYData("DAT11_FXSFITSITE", null);
            this.DAT11_FXSBUYGN = new TYData("DAT11_FXSBUYGN", null);
            this.DAT11_FXSMAKERCOM = new TYData("DAT11_FXSMAKERCOM", null);
            this.DAT11_FXSBUYCOM = new TYData("DAT11_FXSBUYCOM", null);
            this.DAT11_FXSASDATE1 = new TYData("DAT11_FXSASDATE1", null);
            this.DAT11_FXSASDATE2 = new TYData("DAT11_FXSASDATE2", null);
            this.DAT11_FXSEXDATE = new TYData("DAT11_FXSEXDATE", null);
            this.DAT11_FXSCHGCODE = new TYData("DAT11_FXSCHGCODE", null);
            this.DAT11_FXSCHGTEXT = new TYData("DAT11_FXSCHGTEXT", null);
            this.DAT11_FXSCHGDATE = new TYData("DAT11_FXSCHGDATE", null);
            this.DAT11_FXSCHGDPMK = new TYData("DAT11_FXSCHGDPMK", null);
            this.DAT11_FXSCHGSITE = new TYData("DAT11_FXSCHGSITE", null);
            this.DAT11_FXSUSETEXT = new TYData("DAT11_FXSUSETEXT", null);
            this.DAT11_FXSSTRUCT = new TYData("DAT11_FXSSTRUCT", null);
            this.DAT11_FXSMATERIAL = new TYData("DAT11_FXSMATERIAL", null);
            this.DAT11_FXSBIGO = new TYData("DAT11_FXSBIGO", null);
            this.DAT11_FXSHISAB = new TYData("DAT11_FXSHISAB", null);
            this.DAT11_FXSJPNO = new TYData("DAT11_FXSJPNO", null);


            this.SetSpreadCodeHelper(this.FPS91_TY_S_AC_2C533929, "FXAFULLNUM", "FXSNAME", "FXAFULLNUM");

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_2C533929, "FXAYEARSEQ");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_2C533929, "FXAFULLNUM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_2C533929, "FXSNAME");                       
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_2C533929, "FXAAUTONUM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_AC_2C533929, "FXAAREA");

        }

        private void TYACHF004I_Load(object sender, System.EventArgs e)
        {

            // 로그인 사번 가져오기
            this.sSABUN = TYUserInfo.EmpNo.Trim().ToUpper();

            CBH01_FXAFULLNUM.DummyValue = TXT04_FXYEAR.GetValue().ToString() + "-" + TXT04_FXSEQ.GetValue().ToString();
            
            this.ControlFactory.Add(this.DAT10_FXYEAR);
            this.ControlFactory.Add(this.DAT10_FXYEAR);
            this.ControlFactory.Add(this.DAT10_FXSEQ);
            //this.ControlFactory.Add(this.DAT10_FXASCLASS);
            this.ControlFactory.Add(this.DAT10_FXNAME);
            this.ControlFactory.Add(this.DAT10_FXSTAND);
            this.ControlFactory.Add(this.DAT10_FXUNIT);
            this.ControlFactory.Add(this.DAT10_FXQTY);
            this.ControlFactory.Add(this.DAT10_FXGETAMOUNT);
            this.ControlFactory.Add(this.DAT10_FXSAUP);
            this.ControlFactory.Add(this.DAT10_FXBUYDPMK);
            this.ControlFactory.Add(this.DAT10_FXGETDATE);
            this.ControlFactory.Add(this.DAT10_FXORGDATE);
            this.ControlFactory.Add(this.DAT10_FXLIFEYEAR);
            this.ControlFactory.Add(this.DAT10_FXSTOCKNUM);
            this.ControlFactory.Add(this.DAT10_FXAPPNUM);
            this.ControlFactory.Add(this.DAT10_FXJPNO);
            this.ControlFactory.Add(this.DAT10_FXITEMCODE);
            this.ControlFactory.Add(this.DAT10_FXUSED);
            this.ControlFactory.Add(this.DAT10_FXREPAYWAY);
            this.ControlFactory.Add(this.DAT10_FXAPPGUBN);
            this.ControlFactory.Add(this.DAT10_FXAPPSABUN);
            this.ControlFactory.Add(this.DAT10_FXAPPDATE);
            this.ControlFactory.Add(this.DAT10_FXHISAB);

            this.ControlFactory.Add(this.DAT11_FXSYEAR);
            this.ControlFactory.Add(this.DAT11_FXSSEQ);
            this.ControlFactory.Add(this.DAT11_FXSSUBNUM);
            this.ControlFactory.Add(this.DAT11_FXSNAME);
            this.ControlFactory.Add(this.DAT11_FXSCLASS);
            
            this.ControlFactory.Add(this.DAT11_FXSGETDATE);
            this.ControlFactory.Add(this.DAT11_FXSLIFEYEAR);

            this.ControlFactory.Add(this.DAT11_FXSGETAMOUNT);
            this.ControlFactory.Add(this.DAT11_FXSQTY);
            this.ControlFactory.Add(this.DAT11_FXSUNIT);
            this.ControlFactory.Add(this.DAT11_FXSFITSITE);
            this.ControlFactory.Add(this.DAT11_FXSBUYGN);
            this.ControlFactory.Add(this.DAT11_FXSMAKERCOM);
            this.ControlFactory.Add(this.DAT11_FXSBUYCOM);
            this.ControlFactory.Add(this.DAT11_FXSASDATE1);
            this.ControlFactory.Add(this.DAT11_FXSASDATE2);
            this.ControlFactory.Add(this.DAT11_FXSEXDATE);
            this.ControlFactory.Add(this.DAT11_FXSCHGCODE);
            this.ControlFactory.Add(this.DAT11_FXSCHGTEXT);
            this.ControlFactory.Add(this.DAT11_FXSCHGDATE);
            this.ControlFactory.Add(this.DAT11_FXSCHGDPMK);
            this.ControlFactory.Add(this.DAT11_FXSCHGSITE);
            this.ControlFactory.Add(this.DAT11_FXSUSETEXT);
            this.ControlFactory.Add(this.DAT11_FXSSTRUCT);
            this.ControlFactory.Add(this.DAT11_FXSMATERIAL);
            this.ControlFactory.Add(this.DAT11_FXSBIGO);
            this.ControlFactory.Add(this.DAT11_FXSHISAB);
            this.ControlFactory.Add(this.DAT11_FXSJPNO);    


            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);
            this.BTN63_SAV.ProcessCheck += new TButton.CheckHandler(BTN63_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.TXT04_FXYEAR.SetValue(DateTime.Now.ToString("yyyy"));
            this.TXT04_FXSEQ.SetValue(Set_Fill4(fsFXSEQ));            

            this.FPS91_TY_S_AC_2C535927.Initialize();           
                        
            //신규
            if (string.IsNullOrEmpty(this.fsFXYEAR))
            {
                UP_SET_NewProcess();
            }
            else
            {
                this.TXT04_FXYEAR.SetValue(fsFXYEAR);
                this.TXT04_FXSEQ.SetValue(Set_Fill4(fsFXSEQ));   

                //수정
                UP_SET_UpdateProcess();
            }

            this.TXT04_FXYEAR.SetReadOnly(true);
            this.TXT04_FXSEQ.SetReadOnly(true);
            this.TXT01_FXQTY.SetReadOnly(true);
            this.TXT01_FXGETAMOUNT.SetReadOnly(true);  

            this.TXT02_FXSYEAR.SetReadOnly(true);
            this.TXT02_FXSSEQ.SetReadOnly(true);
            this.TXT02_FXSSUBNUM.SetReadOnly(true);

        }
        #endregion

        #region  Description : 신규 상태 처리 이벤트
        private void UP_SET_NewProcess()
        {
            //fsTabCtl = "1";

            fsTabCtl = "2";

            this.UP_Set_TabControl();

            this.tabCtl.SelectedIndex = 0;

            this.UP_SET_BtnProCess("1");
            
            this.Initialize_Controls("01");

            this.CBO01_FXREPAYWAY.SetValue("1");
            this.CBO01_FXAPPGUBN.SetValue("N");    

            this.CBH01_FXSAUP.DummyValue = this.TXT04_FXYEAR.GetValue().ToString() + "0101";
            this.CBH01_FXBUYDPMK.DummyValue = this.TXT04_FXYEAR.GetValue().ToString() + "0101";

            UP_Set_MasterSpread();

            UP_Set_DetailSpread(); 

            this.UP_Set_ReadOnly(true); 

            this.SetStartingFocus(this.CBH02_FXSCLASS.CodeText);
            
        }
        #endregion

        #region  Description : 수정 상태 처리 이벤트
        private void UP_SET_UpdateProcess()
        {
            fsTabCtl = "2";

            this.Initialize_Controls("01");

            this.tabCtl.SelectedIndex = 0;            

            this.CBH01_FXSAUP.DummyValue = this.TXT04_FXYEAR.GetValue().ToString() + "0101";
            this.CBH01_FXBUYDPMK.DummyValue = this.TXT04_FXYEAR.GetValue().ToString() + "0101";
            
            this.TXT04_FXSEQ.SetReadOnly(true);

            this.UP_Set_ReadOnly(true); 

            //마스타 
            UP_Set_MasterScreen();
            //디테일
            UP_Set_DetailScreen();

            TXT02_FXSYEAR.SetValue(TXT04_FXYEAR.GetValue());
            TXT02_FXSSEQ.SetValue(Set_Fill4(TXT04_FXSEQ.GetValue().ToString()));

            if (CBH02_FXSCLASS.GetValue().ToString().Substring(0, 1) != "2")
            {
                fsTabCtl = "2";
            }
            else
            {
                fsTabCtl = "3";
            }

            UP_Set_MasterSpread();

            UP_Set_DetailSpread(); 

            this.UP_Set_TabControl();

            this.UP_SET_BtnProCess("1");
                                               
            this.SetStartingFocus(this.CBH02_FXSCLASS.CodeText);

        }
        #endregion

        #region Description : UP_Set_TabControl Tab Display 이벤트
        private void UP_Set_TabControl()
        {
            if (this.tabCtl.TabPages.Contains(this.Page_Master))
                this.tabCtl.TabPages.Remove(this.Page_Master);
            if (this.tabCtl.TabPages.Contains(this.Page_Detail))
                this.tabCtl.TabPages.Remove(this.Page_Detail);
            if (this.tabCtl.TabPages.Contains(this.Page_differ))
                this.tabCtl.TabPages.Remove(this.Page_differ);

            if (fsTabCtl == "1")
            {
                this.tabCtl.TabPages.Add(this.Page_Master);
            }
            else if (fsTabCtl == "2")
            {
                this.tabCtl.TabPages.Add(this.Page_Master);
                this.tabCtl.TabPages.Add(this.Page_Detail);
            }
            else if (fsTabCtl == "3")
            {
                this.tabCtl.TabPages.Add(this.Page_Master);
                this.tabCtl.TabPages.Add(this.Page_Detail);
                this.tabCtl.TabPages.Add(this.Page_differ);
            }
        }
        #endregion

        #region  Description : 버튼 상태 처리 이벤트
        private void UP_SET_BtnProCess(string sGubn)
        {
            //마스타
            if (sGubn == "1")
            {
                this.BTN61_SAV.Visible = true;
                this.BTN61_NEW.Visible = false;
                this.BTN61_REM.Visible = false;
                this.BTN62_SAV.Visible = false;
                this.BTN63_SAV.Visible = false;
            }
            //디테일
            if (sGubn == "2")
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_NEW.Visible = true;
                this.BTN61_REM.Visible = true;
                this.BTN62_SAV.Visible = true;
                this.BTN63_SAV.Visible = false;
            }
            //층별면적
            if (sGubn == "3")
            {
                this.BTN61_SAV.Visible = false;
                this.BTN61_NEW.Visible = false;
                this.BTN61_REM.Visible = true;
                this.BTN62_SAV.Visible = false;
                this.BTN63_SAV.Visible = true;
            } 
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            this.Initialize_Controls("02");

            this.TXT02_FXSYEAR.SetReadOnly(true);
            this.TXT02_FXSSEQ.SetReadOnly(true);
            this.TXT02_FXSSUBNUM.SetReadOnly(true);

            this.TXT02_FXSYEAR.SetValue(TXT04_FXYEAR.GetValue());
            this.TXT02_FXSSEQ.SetValue(Set_Fill4(TXT04_FXSEQ.GetValue().ToString()));

            this.SetFocus(this.TXT02_FXSNAME);

        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            if (tabCtl.SelectedIndex == 1)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2C66C973", this.TXT04_FXYEAR.GetValue(), this.TXT04_FXSEQ.GetValue(), this.TXT02_FXSSUBNUM.GetValue());
                this.DbConnector.Attach("TY_P_AC_A139X649", this.TXT04_FXYEAR.GetValue(), this.TXT04_FXSEQ.GetValue(), this.TXT02_FXSSUBNUM.GetValue());
                this.DbConnector.ExecuteTranQueryList();

                //고정자산 마스타 수량, 금액 UPDATE
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2CA37009", this.TXT04_FXYEAR.GetValue(), this.TXT04_FXSEQ.GetValue());
                this.DbConnector.ExecuteTranQuery();                

                UP_Set_MasterSpread();

                UP_Set_DetailSpread();

                this.BTN61_NEW_Click(null, null); 

            }

            if (tabCtl.SelectedIndex == 2)
            {
                DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_AC_2CB40071", dt.Rows[i]["FXAYEAR"].ToString(),
                                                                dt.Rows[i]["FXASEQ"].ToString(),
                                                                dt.Rows[i]["FXAFULLNUM"].ToString(),
                                                                dt.Rows[i]["FXAAUTONUM"].ToString());
                }
                this.DbConnector.ExecuteNonQueryList();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2CB6A075", dt.Rows[0]["FXAYEAR"].ToString(),
                                                            dt.Rows[0]["FXASEQ"].ToString(),
                                                            dt.Rows[0]["FXAFULLNUM"].ToString());
                this.DbConnector.ExecuteTranQuery();

                UP_Set_DiffAreaSpread(); 
            }

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            //고정자산 마스타 저장
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsFXYEAR))
            {
                this.DbConnector.Attach("TY_P_AC_2C62T962", this.ControlFactory, "10"); // 저장
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_2C62V963", this.ControlFactory, "10"); // 수정
            }
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
            
            UP_Set_MasterSpread();

            UP_Set_DetailSpread();

            if (string.IsNullOrEmpty(this.fsFXYEAR) != true )
            {
                //TAB 생성
                if (CBH02_FXSCLASS.GetValue().ToString().Substring(0, 1) == "2") //건물
                {
                    //층별면적 tab 을 만든다

                    fsTabCtl = "3";
                }
                else
                {
                    fsTabCtl = "2";
                }
            }

            this.UP_Set_TabControl();

            UP_SET_BtnProCess("1");

        }
        #endregion        

        #region  Description : 취득일자 MTB01_FXGETDATE_Leave 이벤트
        private void MTB01_FXGETDATE_Leave(object sender, EventArgs e)
        {
            this.CBH01_FXSAUP.DummyValue = this.MTB01_FXGETDATE.GetValue().ToString().Replace("-","");
            this.CBH01_FXBUYDPMK.DummyValue = this.MTB01_FXGETDATE.GetValue().ToString().Replace("-", "");
        }
        #endregion

        #region  Description : tabCtl_SelectedIndexChanged 이벤트
        private void tabCtl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabCtl.SelectedIndex == 0)  //마스타
            {
                UP_Set_MasterScreen(); 
                
                UP_SET_BtnProCess("1");

                this.SetFocus(this.CBH02_FXSCLASS); 
            }
            else if (tabCtl.SelectedIndex == 1) //디테일
            {
                TXT02_FXSYEAR.SetValue(TXT04_FXYEAR.GetValue().ToString());
                TXT02_FXSSEQ.SetValue(TXT04_FXSEQ.GetValue().ToString());

                UP_Set_DetailScreen();

                UP_SET_BtnProCess("2");

                this.SetFocus(this.TXT02_FXSNAME); 
            }
            else  //층별 면적
            {
                CBH01_FXAFULLNUM.DummyValue = TXT04_FXYEAR.GetValue().ToString() + "-" + TXT04_FXSEQ.GetValue().ToString();

                UP_SET_BtnProCess("3");

                UP_Set_DiffAreaSpread();
            }
        }
        #endregion

        #region  Description : tabCtl_SelectedIndexChanged 이벤트
        private void UP_Set_ReadOnly(bool bTrueAndFalse)
        {
            //마스타 tab
            TXT01_FXCLASSNUM.SetReadOnly(bTrueAndFalse);
            TXT01_FXAPPNUM.SetReadOnly(bTrueAndFalse);
            TXT01_FXAPPNUM1.SetReadOnly(bTrueAndFalse);
            TXT01_FXSTOCKNUM.SetReadOnly(bTrueAndFalse);
            TXT01_FXSTOCKNUM1.SetReadOnly(bTrueAndFalse);
            TXT01_FXJPNO.SetReadOnly(bTrueAndFalse);
            
            TXT01_FXMJUNKIINCSUM.SetReadOnly(bTrueAndFalse);
            TXT01_FXMJUNKIDECSUM.SetReadOnly(bTrueAndFalse);
            TXT01_FXMJUNKIREPAMOUNT.SetReadOnly(bTrueAndFalse);
            TXT01_FXMINCAMOUNT.SetReadOnly(bTrueAndFalse);
            TXT01_FXMDECAMOUNT.SetReadOnly(bTrueAndFalse);
            TXT01_FXMAMMALAMOUNT.SetReadOnly(bTrueAndFalse);
            TXT01_FXMREPAMOUNT.SetReadOnly(bTrueAndFalse);
            TXT01_FXMREPAMOUNTSUM.SetReadOnly(bTrueAndFalse);
            TXT01_FXMREPJANAMOUNT.SetReadOnly(bTrueAndFalse);

            //디테일 tab
            CBH02_FXSCHGCODE.SetReadOnly(bTrueAndFalse);
            MTB02_FXSCHGDATE.SetReadOnly(bTrueAndFalse);
            CBH02_FXSCHGSITE.SetReadOnly(bTrueAndFalse);
            CBH02_FXSCHGDPMK.SetReadOnly(bTrueAndFalse);
            TXT02_FXSCHGTEXT.SetReadOnly(bTrueAndFalse);
            MTB02_FXMYYMM.SetReadOnly(bTrueAndFalse);
            TXT02_FXMJUNKIINCSUM.SetReadOnly(bTrueAndFalse);
            TXT02_FXMJUNKIDECSUM.SetReadOnly(bTrueAndFalse);
            TXT02_FXMJUNKIREPAMOUNT.SetReadOnly(bTrueAndFalse);
            TXT02_FXMINCAMOUNT.SetReadOnly(bTrueAndFalse);
            TXT02_FXMDECAMOUNT.SetReadOnly(bTrueAndFalse);
            TXT02_FXMAMMALAMOUNT.SetReadOnly(bTrueAndFalse);
            TXT02_FXMREPAMOUNT.SetReadOnly(bTrueAndFalse);
            TXT02_FXMREPAMOUNTSUM.SetReadOnly(bTrueAndFalse);
            TXT02_FXMREPJANAMOUNT.SetReadOnly(bTrueAndFalse);
            TXT02_FXMJUNWOLREPAMOUNT.SetReadOnly(bTrueAndFalse);

        }
        #endregion

        #region  Description : 저장(디테일) 버튼 이벤트
        private void BTN62_SAV_Click(object sender, EventArgs e)
        {
            //고정자산 디테일 저장
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.TXT02_FXSSUBNUM.GetValue().ToString().Trim()))
            {
                //가족코드 순번 생성
                if (this.TXT02_FXSSUBNUM.GetValue().ToString() == "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_2C75X983", this.TXT04_FXYEAR.GetValue(), this.TXT04_FXSEQ.GetValue());
                    this.TXT02_FXSSUBNUM.SetValue(this.DbConnector.ExecuteScalar());

                    this.TXT02_FXSSUBNUM.SetValue(Set_Fill3(this.TXT02_FXSSUBNUM.GetValue().ToString()));

                    this.DAT11_FXSSUBNUM.SetValue(this.TXT02_FXSSUBNUM.GetValue());
                }

                // 고정자산 이력 존재 확인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_3536H598", this.TXT04_FXYEAR.GetValue().ToString(), this.TXT04_FXSEQ.GetValue().ToString(), this.TXT02_FXSSUBNUM.GetValue());
                DataTable dt =  this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count == 0)
                {
                    this.DAT11_FXSCHGCODE.SetValue("10");
                    this.DAT11_FXSCHGTEXT.SetValue("신규취득");
                    this.DAT11_FXSCHGDATE.SetValue(this.MTB01_FXGETDATE.GetValue().ToString().Replace("-", ""));
                    this.DAT11_FXSCHGDPMK.SetValue(this.CBH01_FXSAUP.GetValue());

                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_35376599", this.TXT04_FXYEAR.GetValue().ToString(), this.TXT04_FXSEQ.GetValue().ToString(), 
                                                                this.TXT02_FXSSUBNUM.GetValue(),
                                                                this.MTB02_FXSGETDATE.GetValue().ToString().Replace("-", ""),
                                                                "10", "신규취득",
                                                                this.CBH02_FXSBUYCOM.GetValue(), 
                                                                this.TXT02_FXSQTY.GetValue(), 
                                                                this.TXT02_FXSGETAMOUNT.GetValue() ,
                                                                //"0", "", "",
                                                                //"", "", "",
                                                                //"", "", "",
                                                                //"0", "", "", 
                                                                //"", "", "", 
                                                                //"", "", "",
                                                                 sSABUN);
                    this.DbConnector.ExecuteTranQuery();
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2C664971", this.ControlFactory, "11"); // 저장              
                this.DbConnector.ExecuteTranQueryList();

            }
            else
            {                
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2C66B972", this.ControlFactory, "11"); // 수정
                this.DbConnector.ExecuteTranQuery();
            }
            

            //고정자산 마스타 수량, 금액 UPDATE
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2CA37009", this.TXT04_FXYEAR.GetValue(), this.TXT04_FXSEQ.GetValue());
            this.DbConnector.ExecuteTranQuery();

            UP_Set_MasterSpread();

            UP_Set_DetailSpread();            

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region  Description : 저장(층별면적) 버튼 이벤트
        private void BTN63_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;            

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2CB4B072", ds.Tables[0].Rows[0]["FXAYEAR"].ToString(), 
                                                        ds.Tables[0].Rows[0]["FXASEQ"].ToString(),
                                                        ds.Tables[0].Rows[0]["FXAFULLNUM"].ToString());
            int iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
            
            this.DbConnector.CommandClear();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                iCnt = iCnt + 1;

                this.DbConnector.Attach("TY_P_AC_2CB47069", ds.Tables[0].Rows[i]["FXAYEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXASEQ"].ToString(),
                                                            ds.Tables[0].Rows[i]["FXAFULLNUM"].ToString(),
                                                            iCnt.ToString(),
                                                            ds.Tables[0].Rows[i]["FXAAREA"].ToString(),
                                                            TYUserInfo.EmpNo);
            }
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_2CB47069", ds.Tables[1].Rows[i]["FXAYEAR"].ToString(),
                                                            ds.Tables[1].Rows[i]["FXASEQ"].ToString(),
                                                            ds.Tables[1].Rows[i]["FXAFULLNUM"].ToString(),
                                                            ds.Tables[1].Rows[i]["FXAAUTONUM"].ToString(),
                                                            ds.Tables[1].Rows[i]["FXAAREA"].ToString(),
                                                            TYUserInfo.EmpNo);
            }
            this.DbConnector.ExecuteTranQueryList();

            //고정자산 Detail Update
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2CB6A075", ds.Tables[0].Rows[0]["FXAYEAR"].ToString(),
                                                        ds.Tables[0].Rows[0]["FXASEQ"].ToString(),
                                                        ds.Tables[0].Rows[0]["FXAFULLNUM"].ToString());
            this.DbConnector.ExecuteTranQuery();

            //고정자산 마스타 수량, 금액 UPDATE
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2CA37009", ds.Tables[0].Rows[0]["FXAYEAR"].ToString(), ds.Tables[0].Rows[0]["FXASEQ"].ToString());
            this.DbConnector.ExecuteTranQuery();

            UP_Set_DiffAreaSpread();

            UP_Set_MasterSpread();

            UP_Set_DetailSpread();            

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 저장 BTN61_SAV ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {           

            //화면에 있는 값 저장
            DAT10_FXYEAR.SetValue(TXT04_FXYEAR.GetValue());
            //신규이면 자산순번 생성
            if (string.IsNullOrEmpty(this.fsFXYEAR))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2C63N965", this.TXT04_FXYEAR.GetValue());
                this.TXT04_FXSEQ.SetValue(Set_Fill3(this.DbConnector.ExecuteScalar().ToString()));
            }
            DAT10_FXSEQ.SetValue(TXT04_FXSEQ.GetValue());
            //DAT10_FXASCLASS.SetValue("");
            DAT10_FXNAME.SetValue(TXT01_FXNAME.GetValue());
            DAT10_FXSTAND.SetValue(TXT01_FXSTAND.GetValue());
            DAT10_FXUNIT.SetValue(CBH01_FXUNIT.GetValue());
            DAT10_FXQTY.SetValue(TXT01_FXQTY.GetValue());
            DAT10_FXGETAMOUNT.SetValue(TXT01_FXGETAMOUNT.GetValue());
            DAT10_FXSAUP.SetValue(CBH01_FXSAUP.GetValue());
            DAT10_FXBUYDPMK.SetValue(CBH01_FXBUYDPMK.GetValue());
            DAT10_FXGETDATE.SetValue(MTB01_FXGETDATE.GetValue().ToString().Replace("-",""));
            DAT10_FXORGDATE.SetValue(MTB01_FXORGDATE.GetValue().ToString().Replace("-", ""));
            DAT10_FXLIFEYEAR.SetValue(TXT01_FXLIFEYEAR.GetValue());
            DAT10_FXSTOCKNUM.SetValue(TXT01_FXSTOCKNUM.GetValue());
            DAT10_FXAPPNUM.SetValue(TXT01_FXAPPNUM.GetValue());
            DAT10_FXJPNO.SetValue(TXT01_FXJPNO.GetValue());
            DAT10_FXITEMCODE.SetValue(CBH01_FXITEMCODE.GetValue());
            DAT10_FXUSED.SetValue(TXT01_FXUSED.GetValue());
            DAT10_FXREPAYWAY.SetValue(CBO01_FXREPAYWAY.GetValue());
            DAT10_FXAPPGUBN.SetValue(CBO01_FXAPPGUBN.GetValue());
            DAT10_FXAPPSABUN.SetValue(CBH01_FXAPPSABUN.GetValue());
            DAT10_FXAPPDATE.SetValue(MTB01_FXAPPDATE.GetValue().ToString().Replace("-",""));
            DAT10_FXHISAB.SetValue(TYUserInfo.EmpNo);

            //승인이면 승인일자, 승인사번 받는다
            if (this.CBO01_FXAPPGUBN.GetValue().ToString() == "Y")
            {
                if (MTB01_FXAPPDATE.GetValue().ToString().Replace("-", "") == "")
                {
                    this.ShowMessage("TY_M_AC_2584Y096");
                    this.SetFocus(this.MTB01_FXAPPDATE); 
                    e.Successed = false;
                    return;
                }

                if (CBH01_FXAPPSABUN.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_AC_2C65V970");
                    this.SetFocus(this.CBH01_FXAPPSABUN); 
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }           

        }
        #endregion

        #region Description : 저장 BTN62_SAV ProcessCheck 이벤트
        private void BTN62_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //동일키 체크
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_2C532926", this.TXT04_FXYEAR.GetValue().ToString(), Get_Numeric(this.TXT04_FXSEQ.GetValue().ToString()), Get_Numeric(this.TXT02_FXSSUBNUM.GetValue().ToString()));
            //DataTable dt = this.DbConnector.ExecuteDataTable();
            //if (dt.Rows.Count > 0)
            //{
            //    this.ShowMessage("TY_M_GB_23S40973");
            //    this.SetFocus(this.TXT02_FXSNAME);
            //    e.Successed = false;
            //    return;
            //}
            //dt.Dispose();

            this.DAT11_FXSYEAR.SetValue(TXT04_FXYEAR.GetValue());
            this.DAT11_FXSSEQ.SetValue(TXT04_FXSEQ.GetValue());
            if (this.TXT02_FXSSUBNUM.GetValue().ToString() != "")
            {
                this.DAT11_FXSSUBNUM.SetValue(this.TXT02_FXSSUBNUM.GetValue().ToString());
            }
            else
            {
                this.DAT11_FXSSUBNUM.SetValue("");
            }
            this.DAT11_FXSNAME.SetValue(TXT02_FXSNAME.GetValue());
            this.DAT11_FXSCLASS.SetValue(CBH02_FXSCLASS.GetValue());
            this.DAT11_FXSGETDATE.SetValue(MTB02_FXSGETDATE.GetValue().ToString().Replace("-", ""));
            this.DAT11_FXSLIFEYEAR.SetValue(TXT02_FXSLIFEYEAR.GetValue().ToString().Replace("-", ""));
            this.DAT11_FXSGETAMOUNT.SetValue(TXT02_FXSGETAMOUNT.GetValue());
            this.DAT11_FXSQTY.SetValue(TXT02_FXSQTY.GetValue());
            this.DAT11_FXSUNIT.SetValue(CBH02_FXSUNIT.GetValue());
            this.DAT11_FXSFITSITE.SetValue(CBH02_FXSFITSITE.GetValue());
            this.DAT11_FXSBUYGN.SetValue(CBO02_FXSBUYGN.GetValue());
            this.DAT11_FXSMAKERCOM.SetValue(TXT02_FXSMAKERCOM.GetValue());
            this.DAT11_FXSBUYCOM.SetValue(CBH02_FXSBUYCOM.GetValue());
            this.DAT11_FXSASDATE1.SetValue(MTB02_FXSASDATE1.GetValue().ToString().Replace("-",""));
            this.DAT11_FXSASDATE2.SetValue(MTB02_FXSASDATE2.GetValue().ToString().Replace("-", ""));
            this.DAT11_FXSEXDATE.SetValue(MTB02_FXSEXDATE.GetValue().ToString().Replace("-", ""));
            this.DAT11_FXSCHGCODE.SetValue(CBH02_FXSCHGCODE.GetValue());
            this.DAT11_FXSCHGTEXT.SetValue(TXT02_FXSCHGTEXT.GetValue());
            this.DAT11_FXSCHGDATE.SetValue(MTB02_FXSCHGDATE.GetValue().ToString().Replace("-", ""));
            this.DAT11_FXSCHGDPMK.SetValue(CBH02_FXSCHGDPMK.GetValue());
            this.DAT11_FXSCHGSITE.SetValue(CBH02_FXSCHGSITE.GetValue());
            this.DAT11_FXSUSETEXT.SetValue(TXT02_FXSUSETEXT.GetValue());
            this.DAT11_FXSSTRUCT.SetValue(CBH02_FXSSTRUCT.GetValue());
            this.DAT11_FXSMATERIAL.SetValue(CBH02_FXSMATERIAL.GetValue());
            this.DAT11_FXSBIGO.SetValue(TXT02_FXSBIGO.GetValue());
            this.DAT11_FXSHISAB.SetValue(TYUserInfo.EmpNo);
            this.DAT11_FXSJPNO.SetValue(TXT02_FXSJPNO.GetValue().ToString());  

            //자 자산번호중에 자산분류코드가 다른게 있는지 체크
            // 2018.07.11 자 자산번호중에 자산분류가 달라도 등록할수 있다. ( 임경화 )
            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_2C532926", this.TXT04_FXYEAR.GetValue().ToString(), Get_Numeric(this.TXT04_FXSEQ.GetValue().ToString()), "0" );
            //DataTable dt = this.DbConnector.ExecuteDataTable();
            //if (dt.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        if ( Convert.ToInt16(dt.Rows[i]["FXSSUBNUM"].ToString().Trim()) != Convert.ToInt16(Get_Numeric(this.TXT02_FXSSUBNUM.GetValue().ToString())))
            //        {
            //            if (dt.Rows[i]["FXSCLASS"].ToString().Trim().Substring(0, 1) != this.CBH02_FXSCLASS.GetValue().ToString().Substring(0, 1))
            //            {
            //                this.ShowCustomMessage("자산구분을 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //                e.Successed = false;
            //                return; 
            //            }
            //        }
            //    }
            //}


            //자산분류가 바뀌면 자산분류관리 화면에서 처리 한다.
            if (Convert.ToInt16(Get_Numeric(this.TXT02_FXSSUBNUM.GetValue().ToString())) > 0)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2C532926", this.TXT04_FXYEAR.GetValue().ToString(), this.TXT04_FXSEQ.GetValue().ToString(), this.TXT02_FXSSUBNUM.GetValue());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["FXSCLASS"].ToString().Substring(0, 1) != CBH02_FXSCLASS.GetValue().ToString().Substring(0, 1))
                    {
                        this.ShowCustomMessage("자산분류코드 변경은 변경관리 화면에서 할수 있습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
            }
                      
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }           

        }
        #endregion

        #region Description : 저장 BTN63_SAV ProcessCheck 이벤트
        private void BTN63_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_2C533929.GetDataSourceInclude(TSpread.TActionType.New, "FXAYEAR", "FXASEQ", "FXAFULLNUM", "FXAAUTONUM", "FXAAREA"));
            ds.Tables.Add(this.FPS91_TY_S_AC_2C533929.GetDataSourceInclude(TSpread.TActionType.Update, "FXAYEAR", "FXASEQ", "FXAFULLNUM", "FXAAUTONUM", "FXAAREA"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
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
            if (tabCtl.SelectedIndex == 1)
            {
                //층별 면적에 자료가 있는지 체크한다.
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2CB7K078", this.TXT02_FXSYEAR.GetValue().ToString(), Get_Numeric(this.TXT02_FXSSEQ.GetValue().ToString()), Get_Numeric(this.TXT02_FXSSUBNUM.GetValue().ToString()));
                Int16  iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                if (iCnt > 0)
                {
                    this.ShowMessage("TY_M_AC_2CB7M081");
                    e.Successed = false;
                    return;
                }


                if (!this.ShowMessage("TY_M_GB_23NAD872"))
                {
                    e.Successed = false;
                    return;
                }

            }
            if (tabCtl.SelectedIndex == 2)
            {
                DataTable dt = this.FPS91_TY_S_AC_2C533929.GetDataSourceInclude(TSpread.TActionType.Remove, "FXAYEAR", "FXASEQ", "FXAFULLNUM", "FXAAUTONUM");

                if (dt.Rows.Count == 0)
                {
                    this.ShowMessage("TY_M_GB_23NAD870");
                    e.Successed = false;
                    return;
                }

                if (!this.ShowMessage("TY_M_GB_23NAD872"))
                {
                    e.Successed = false;
                    return;
                }

                e.ArgData = dt;
            }
        }
        #endregion        

        #region Description : TXT04_FXYEAR_TextChanged 이벤트
        private void TXT04_FXYEAR_TextChanged(object sender, EventArgs e)
        {
            this.CBH01_FXSAUP.DummyValue = this.TXT04_FXYEAR.GetValue().ToString() + "0101";
        }
        #endregion

        #region Description : 고정자산 마스타 그리드 조회
        private void UP_Set_MasterSpread()
        {
            this.FPS91_TY_S_AC_2C535927.Initialize(); 
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2C532926", this.TXT04_FXYEAR.GetValue().ToString(), this.TXT04_FXSEQ.GetValue().ToString(), "0");
            this.FPS91_TY_S_AC_2C535927.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region Description : 고정자산 디테일 그리드 조회
        private void UP_Set_DetailSpread()
        {
            this.FPS92_TY_S_AC_2C535927.Initialize(); 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2C532926", this.TXT04_FXYEAR.GetValue().ToString(), this.TXT04_FXSEQ.GetValue().ToString(), "0"); // 
            this.FPS92_TY_S_AC_2C535927.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : FPS92_TY_S_AC_2C535927_CellDoubleClick 
        private void FPS92_TY_S_AC_2C535927_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2C532926", this.FPS92_TY_S_AC_2C535927.GetValue("FXSYEAR").ToString(),this.FPS92_TY_S_AC_2C535927.GetValue("FXSSEQ").ToString(), this.FPS92_TY_S_AC_2C535927.GetValue("FXSSUBNUM").ToString());
            DataTable dts = this.DbConnector.ExecuteDataTable();
            this.CurrentDataTableRowMapping(dts, "02");

            // 모번호 가지고 오기 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3594X640", this.FPS92_TY_S_AC_2C535927.GetValue("FXSYEAR").ToString()+ Set_Fill4(this.FPS92_TY_S_AC_2C535927.GetValue("FXSSEQ").ToString())+ Set_Fill3(this.FPS92_TY_S_AC_2C535927.GetValue("FXSSUBNUM").ToString()));
            dt = this.DbConnector.ExecuteDataTable();

            SetComboBox(this.CBO01_ASSETNUM, dt, 0);


            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3535L595", this.FPS92_TY_S_AC_2C535927.GetValue("FXSYEAR").ToString(), this.FPS92_TY_S_AC_2C535927.GetValue("FXSSEQ").ToString(), this.FPS92_TY_S_AC_2C535927.GetValue("FXSSUBNUM").ToString());
            DataTable dts1 = this.DbConnector.ExecuteDataTable();
            this.CurrentDataTableRowMapping(dts1, "02");


            this.SetFocus(this.TXT02_FXSNAME);
        }
        #endregion

        #region Description : 고정자산 마스타 확인 
        private void UP_Set_MasterScreen()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2CA6K021", this.TXT04_FXYEAR.GetValue(), this.TXT04_FXSEQ.GetValue()); //마스터 조회
            DataTable dtm = this.DbConnector.ExecuteDataTable();
            this.CurrentDataTableRowMapping(dtm, "01");
        }
        #endregion

        #region Description : 고정자산 디테일 확인
        private void UP_Set_DetailScreen()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2C532926", this.TXT04_FXYEAR.GetValue(), this.TXT04_FXSEQ.GetValue(), "0"); //디테일 조회
            DataTable dts = this.DbConnector.ExecuteDataTable();
            
            if( dts.Rows.Count > 0 )
            {                

                this.CBH02_FXSCHGDPMK.DummyValue = dts.Rows[0]["FXSYEAR"].ToString() + "0101";  
            }
            else
            {
                this.CBH02_FXSCHGDPMK.DummyValue = this.TXT04_FXYEAR.GetValue().ToString() + "0101";
            }
            this.CurrentDataTableRowMapping(dts, "02");

            //CBH02_FXSCHGCODE.SetValue(dts.Rows[0]["FXSCHGCODE"].ToString());

            //CBH02_FXSCHGSITE.SetValue(dts.Rows[0]["FXSCHGSITE"].ToString()); 
            //CBH02_FXSCHGDPMK.SetValue(dts.Rows[0]["FXSCHGDPMK"].ToString()); 
        }
        #endregion

        #region Description : 고정자산  층별면적 조회
        private void UP_Set_DiffAreaSpread()
        {
            this.FPS91_TY_S_AC_2C533929.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2CB19061", this.TXT04_FXYEAR.GetValue(), this.TXT04_FXSEQ.GetValue());
            this.FPS91_TY_S_AC_2C533929.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_2C533929_EnterCell 이벤트
        private void FPS91_TY_S_AC_2C533929_EnterCell(object sender, FarPoint.Win.Spread.EnterCellEventArgs e)
        {
            if (e.Column != 3)
                return;

            string sFXAFULLNUM = this.FPS91_TY_S_AC_2C533929.GetValue(e.Row, "FXAYEARSEQ").ToString();

            TYCodeBox tyCodeBox = this.GetSpreadCodeHelper(this.FPS91_TY_S_AC_2C533929, "FXAFULLNUM");
            if (tyCodeBox != null)
                tyCodeBox.DummyValue = sFXAFULLNUM;
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_2C533929_RowInserted 이벤트
        private void FPS91_TY_S_AC_2C533929_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_2C533929.SetValue(e.RowIndex, "FXAYEAR", this.TXT04_FXYEAR.GetValue().ToString() );
            this.FPS91_TY_S_AC_2C533929.SetValue(e.RowIndex, "FXASEQ",  this.TXT04_FXSEQ.GetValue().ToString());
            this.FPS91_TY_S_AC_2C533929.SetValue(e.RowIndex, "FXAYEARSEQ", this.TXT04_FXYEAR.GetValue().ToString() + "-" + this.TXT04_FXSEQ.GetValue().ToString());
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region Description : 승인구분 필터 변경시 처리
        private void CBO01_FXAPPGUBN_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (this.CBO01_FXAPPGUBN.GetValue().ToString() == "Y")
            {
                // 강경석,김계영,황성환
                if (sSABUN.ToUpper() == "0310-M" || sSABUN.ToUpper() == "0269-M" || sSABUN.ToUpper() == "0349-F" || sSABUN.ToUpper() == "0390-M")
                {
                    this.CBH01_FXAPPSABUN.SetValue(sSABUN);
                    this.MTB01_FXAPPDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
                }
            }
            else
            {
                this.CBH01_FXAPPSABUN.SetValue("");
                this.MTB01_FXAPPDATE.SetValue("");
            }
        }
        #endregion

        #region Description : 그룹웨어 url 처리
        private void BTN61_GW_Click(object sender, EventArgs e)
        {
            if (this.TXT01_FXAPPNUM.GetValue().ToString() != "")
            {
                if ((new TYERGB012P(this.TXT01_FXAPPNUM.GetValue().ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                }
            }
            else
            {
                this.ShowMessage("TY_M_MR_2BC51262");
                return;
            }
        }

        private void BTN61_GW1_Click(object sender, EventArgs e)
        {

        } 
        #endregion


        #region Description : 자산변경관리 팝업 처리
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if ((new TYACHF04C1(this.TXT04_FXYEAR.GetValue().ToString(), this.TXT04_FXSEQ.GetValue().ToString(), this.TXT02_FXSSUBNUM.GetValue().ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2C532926", this.FPS92_TY_S_AC_2C535927.GetValue("FXSYEAR").ToString(), this.FPS92_TY_S_AC_2C535927.GetValue("FXSSEQ").ToString(), this.FPS92_TY_S_AC_2C535927.GetValue("FXSSUBNUM").ToString());
                DataTable dts = this.DbConnector.ExecuteDataTable();
                this.CurrentDataTableRowMapping(dts, "02");
            }                
        }
        #endregion

       

    }
}
