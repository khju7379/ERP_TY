using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 할인어음관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.05.18 13:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25L3K577 : 할인어음 확인
    ///  TY_P_AC_25L3Q578 : 할인어음 등록
    ///  TY_P_AC_25L3X579 : 할인어음 수정
    ///  TY_P_AC_25L45582 : 받을어음 수정
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  E6CDCL : 거래처코드
    ///  E6NMBK : 결제은행
    ///  E6GUBUN : 어음구분
    ///  E6IDBS : 발생구분
    ///  E6DTCO : 입금일자
    ///  E6DTED : 만기일자
    ///  E6AMNR : 어음금액
    ///  E7AMCH : 추심료
    ///  E7CDCM : 관리부서
    ///  E7CDGL : 금융기관
    ///  E7CDSB : 관리사번
    ///  E7DSDT : 할인일수
    ///  E7DSIN : 기준일수
    ///  E7DSNR : 할인료
    ///  E7DSTR1 : 할인율１
    ///  E7DSTR2 : 할인율２
    ///  E7DSYN : 전표발행
    ///  E7DTBG : 상태변경일
    ///  E7IDBG : 상태구분
    ///  E7NOACB : 계좌번호
    ///  E7NOACI : 입금계정코드
    ///  E7NONR : 어음번호
    /// </summary>
    public partial class TYACEI006I : TYBase
    {
        private string fsE7NONR;
        private string fsE7IDBG;
        private string fsE7DTBG;
        private bool _Isloaded = false;

        private TYData DAT03_E7NONR;
        private TYData DAT03_E7IDBG;
        private TYData DAT03_E7DTBG;
        private TYData DAT03_E7CDGL;
        private TYData DAT03_E7CDCM;
        private TYData DAT03_E7CDSB;
        private TYData DAT03_E7CDCL;
        private TYData DAT03_E7DSTR1;
        private TYData DAT03_E7DSTR2;
        private TYData DAT03_E7DSDT;
        private TYData DAT03_E7DSIN;
        private TYData DAT03_E7DSNR;
        private TYData DAT03_E7DSYN;
        private TYData DAT03_E7AMCH;
        private TYData DAT03_E7INNR;
        private TYData DAT03_E7NOACB;
        private TYData DAT03_E7NOACI;
        private TYData DAT03_E7HDAC;
        private TYData DAT03_E7HIDBG;
        private TYData DAT03_E7HDTBG;
        private TYData DAT03_E7HCDGL;
        private TYData DAT03_E7CAUSE;

        private TYData DAT04_E6NONR;
        private TYData DAT04_E6IDBG;
        private TYData DAT04_E6DTBG;
        private TYData DAT04_E6CDGL;

        #region Description : 폼 로드 이벤트
        public TYACEI006I(string sE7NONR, string sE7IDBG, string sE7DTBG)
        {
            InitializeComponent();
            this.SetPopupStyle();
            this.fsE7NONR = sE7NONR;
            this.fsE7IDBG = sE7IDBG;
            this.fsE7DTBG = sE7DTBG;

            this.DAT03_E7NONR = new TYData("DAT03_E7NONR", null);
            this.DAT03_E7IDBG = new TYData("DAT03_E7IDBG", null);
            this.DAT03_E7DTBG = new TYData("DAT03_E7DTBG", null);
            this.DAT03_E7CDGL = new TYData("DAT03_E7CDGL", null);
            this.DAT03_E7CDCM = new TYData("DAT03_E7CDCM", null);
            this.DAT03_E7CDSB = new TYData("DAT03_E7CDSB", null);
            this.DAT03_E7CDCL = new TYData("DAT03_E7CDCL", null);
            this.DAT03_E7DSTR1 = new TYData("DAT03_E7DSTR1", null);
            this.DAT03_E7DSTR2 = new TYData("DAT03_E7DSTR2", null);
            this.DAT03_E7DSDT = new TYData("DAT03_E7DSDT", null);
            this.DAT03_E7DSIN = new TYData("DAT03_E7DSIN", null);
            this.DAT03_E7DSNR = new TYData("DAT03_E7DSNR", null);
            this.DAT03_E7DSYN = new TYData("DAT03_E7DSYN", null);
            this.DAT03_E7AMCH = new TYData("DAT03_E7AMCH", null);
            this.DAT03_E7INNR = new TYData("DAT03_E7INNR", null);
            this.DAT03_E7NOACB = new TYData("DAT03_E7NOACB", null);
            this.DAT03_E7NOACI = new TYData("DAT03_E7NOACI", null);
            this.DAT03_E7HDAC = new TYData("DAT03_E7HDAC", null);
            this.DAT03_E7HIDBG = new TYData("DAT03_E7HIDBG", null);
            this.DAT03_E7HDTBG = new TYData("DAT03_E7HDTBG", null);
            this.DAT03_E7HCDGL = new TYData("DAT03_E7HCDGL", null);
            this.DAT03_E7CAUSE = new TYData("DAT03_E7CAUSE", null);

            this.DAT04_E6NONR = new TYData("DAT04_E6NONR", null);
            this.DAT04_E6IDBG = new TYData("DAT04_E6IDBG", null);
            this.DAT04_E6DTBG = new TYData("DAT04_E6DTBG", null);
            this.DAT04_E6CDGL = new TYData("DAT04_E6CDGL", null);
  
        }        

        private void TYACEI006I_Load(object sender, System.EventArgs e)
        {           
            this.CBH01_E7CDGL.CodeBoxDataBinded += new TCodeBox.TCodeBoxEventHandler(CBH01_E7CDGL_CodeBoxDataBinded);
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.ControlFactory.Add(this.DAT03_E7NONR);
            this.ControlFactory.Add(this.DAT03_E7IDBG);
            this.ControlFactory.Add(this.DAT03_E7DTBG);
            this.ControlFactory.Add(this.DAT03_E7CDGL);
            this.ControlFactory.Add(this.DAT03_E7CDCM);
            this.ControlFactory.Add(this.DAT03_E7CDSB);
            this.ControlFactory.Add(this.DAT03_E7CDCL);
            this.ControlFactory.Add(this.DAT03_E7DSTR1);
            this.ControlFactory.Add(this.DAT03_E7DSTR2);
            this.ControlFactory.Add(this.DAT03_E7DSDT);
            this.ControlFactory.Add(this.DAT03_E7DSIN);
            this.ControlFactory.Add(this.DAT03_E7DSNR);
            this.ControlFactory.Add(this.DAT03_E7DSYN);
            this.ControlFactory.Add(this.DAT03_E7AMCH);
            this.ControlFactory.Add(this.DAT03_E7INNR);
            this.ControlFactory.Add(this.DAT03_E7NOACB);
            this.ControlFactory.Add(this.DAT03_E7NOACI);
            this.ControlFactory.Add(this.DAT03_E7HDAC);
            this.ControlFactory.Add(this.DAT03_E7HIDBG);
            this.ControlFactory.Add(this.DAT03_E7HDTBG);
            this.ControlFactory.Add(this.DAT03_E7HCDGL);
            this.ControlFactory.Add(this.DAT03_E7CAUSE);

            this.ControlFactory.Add(this.DAT04_E6NONR);
            this.ControlFactory.Add(this.DAT04_E6IDBG);
            this.ControlFactory.Add(this.DAT04_E6DTBG);
            this.ControlFactory.Add(this.DAT04_E6CDGL);
    

            if (this.fsE7IDBG.ToString().Trim() != "12" )
            {
                this.TXT01_E7NONR.SetReadOnly(true);
                this.CBO01_E7IDBG.SetReadOnly(true);
                this.DTP01_E7DTBG.SetReadOnly(false);

                TXT01_E7NONR.SetValue(fsE7NONR);                                  

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_25L3K577", this.fsE7NONR, this.fsE7IDBG, this.fsE7DTBG);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                CBH01_E7CDCM.DummyValue = dt.Rows[0]["E7DTBG"].ToString();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "02");
                }

                this.CBO01_E7IDBG.SetValue("12");                
                this.DTP01_E7DTBG.SetValue(DateTime.Now.ToString("yyyyMMdd"));

                this.SetStartingFocus(DTP01_E7DTBG);
                
            }
            else
            {
                TXT01_E7NONR.SetValue(fsE7NONR);
                this.CBO01_E7IDBG.SetValue(fsE7IDBG);
                this.DTP01_E7DTBG.SetValue(fsE7DTBG);
                
                this.TXT01_E7NONR.SetReadOnly(true);
                this.CBO01_E7IDBG.SetReadOnly(true);
                this.DTP01_E7DTBG.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_25L3K577", this.fsE7NONR, this.fsE7IDBG, this.fsE7DTBG);
                DataTable dt = this.DbConnector.ExecuteDataTable();               
                
                if (dt.Rows.Count > 0)
                {
                    this.CBH01_E7CDCM.DummyValue = dt.Rows[0]["E7DTBG"].ToString();
                    this.CBH01_E7CDGL.SetValue(dt.Rows[0]["E7CDGL"]);
                    this.CurrentDataTableRowMapping(dt, "01");
                    this.CurrentDataTableRowMapping(dt, "02");
                }

                this.SetStartingFocus(CBH01_E7CDGL);
            }

            this.DTP02_E6DTCO.SetReadOnly(true);
            this.DTP02_E6DTED.SetReadOnly(true);
            this.CBO02_E6IDBS.SetReadOnly(true);
            this.CBO02_E6GUBUN.SetReadOnly(true);
            this.TXT02_E6AMNR.SetReadOnly(true);
            this.CBH02_E6CDCL.SetReadOnly(true);
            this.CBH02_E6NMBK.SetReadOnly(true);
            this.CBH01_E7NOACI.SetReadOnly(true);
            this.CBO02_E6IDBG.SetReadOnly(true);
            this.DTP01_E6DTBG.SetReadOnly(true);
            this.CBH02_E6CDGL.SetReadOnly(true);        

            this._Isloaded = true; 

        }
        #endregion

        #region Description : 종료 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (this.fsE7IDBG.ToString().Trim() != "12")
            {
                //어음내역 저장
                this.DbConnector.Attach("TY_P_AC_25L3Q578", this.ControlFactory,"03" );
            }
            else
            {
                //어음내역 수정
                this.DbConnector.Attach("TY_P_AC_25L3X579", CBH01_E7CDGL.GetValue(), CBH01_E7CDCM.GetValue(), CBH01_E7CDSB.GetValue(),
                                                            CBH02_E6CDCL.GetValue(), Get_Numeric(TXT01_E7DSTR1.GetValue().ToString()), Get_Numeric(TXT01_E7DSTR2.GetValue().ToString()),
                                                            Get_Numeric(TXT01_E7DSDT.GetValue().ToString()), Get_Numeric(TXT01_E7DSIN.GetValue().ToString()), Get_Numeric(TXT01_E7DSNR.GetValue().ToString()),
                                                            TXT01_E7DSYN.GetValue(), Get_Numeric(TXT01_E7AMCH.GetValue().ToString()), CBH01_E7NOACB.GetValue(),
                                                            CBH01_E7NOACI.GetValue(), TXT01_E7NONR.GetValue(), CBO01_E7IDBG.GetValue(),
                                                            DTP01_E7DTBG.GetString() );
            }
            //받을어음마스타 수정
            this.DbConnector.Attach("TY_P_AC_25L45582", this.ControlFactory, "04");

            this.DbConnector.ExecuteTranQueryList();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //전표유무 체크
            if (this.TXT01_E7DSYN.GetValue().ToString() != "Y" && this.TXT01_E7DSYN.GetValue().ToString() != "")
            {
                this.ShowMessage("TY_M_AC_25M7B594");
                e.Successed = false;
                return;
            }

            //어음 내역 데이타 저장
            this.DAT03_E7NONR.SetValue(TXT01_E7NONR.GetValue());
            this.DAT03_E7IDBG.SetValue(CBO01_E7IDBG.GetValue());
            this.DAT03_E7DTBG.SetValue(DTP01_E7DTBG.GetString());
            this.DAT03_E7CDGL.SetValue(CBH01_E7CDGL.GetValue());
            this.DAT03_E7CDCM.SetValue(CBH01_E7CDCM.GetValue());
            this.DAT03_E7CDSB.SetValue(CBH01_E7CDSB.GetValue());
            this.DAT03_E7CDCL.SetValue(CBH02_E6CDCL.GetValue());
            this.DAT03_E7DSTR1.SetValue(Get_Numeric(TXT01_E7DSTR1.GetValue().ToString()));            
            this.DAT03_E7DSTR2.SetValue(Get_Numeric(TXT01_E7DSTR2.GetValue().ToString()));
            this.DAT03_E7DSDT.SetValue(Get_Numeric(TXT01_E7DSDT.GetValue().ToString()));
            this.DAT03_E7DSIN.SetValue(Get_Numeric(TXT01_E7DSIN.GetValue().ToString()));
            this.DAT03_E7DSNR.SetValue(Get_Numeric(TXT01_E7DSNR.GetValue().ToString()));
            this.DAT03_E7DSYN.SetValue(TXT01_E7DSYN.GetValue());
            this.DAT03_E7AMCH.SetValue(Get_Numeric(TXT01_E7AMCH.GetValue().ToString()));
            this.DAT03_E7INNR.SetValue("0");
            this.DAT03_E7NOACB.SetValue(CBH01_E7NOACB.GetValue());
            this.DAT03_E7NOACI.SetValue(CBH01_E7NOACI.GetValue());
            this.DAT03_E7HDAC.SetValue("");
            this.DAT03_E7HIDBG.SetValue(CBO02_E6IDBG.GetValue());
            this.DAT03_E7HDTBG.SetValue(DTP01_E6DTBG.GetValue());
            this.DAT03_E7HCDGL.SetValue(CBH02_E6CDGL.GetValue());
            this.DAT03_E7CAUSE.SetValue("");

            //어음 마스타 데이타 저장
            this.DAT04_E6NONR.SetValue(TXT01_E7NONR.GetValue());
            this.DAT04_E6IDBG.SetValue(CBO01_E7IDBG.GetValue());
            this.DAT04_E6DTBG.SetValue(DTP01_E7DTBG.GetValue());
            this.DAT04_E6CDGL.SetValue(CBH01_E7CDGL.GetValue());
            
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : E7CDGL_CodeBoxDataBinded 이벤트
        private void CBH01_E7CDGL_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_E7CDGL.GetValue().ToString();
            this.CBH01_E7NOACB.DummyValue = groupCode;
            this.CBH01_E7NOACB.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_E7NOACB.Initialize();

        }
        #endregion

        #region Description : E7DTBG_ValueChanged 이벤트
        private void DTP01_E7DTBG_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_E7CDCM.DummyValue = this.DTP01_E7DTBG.GetString(); 
        }
        #endregion

        #region Description : E7NOACB_CodeBoxDataBinded 이벤트
        private void CBH01_E7NOACB_CodeBoxDataBinded(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_24G14670", this.CBH01_E7NOACB.GetValue());
            if( this.DbConnector.ExecuteDataTable().Rows.Count > 0 ) 
                this.CBH01_E7NOACI.SetValue(this.DbConnector.ExecuteDataTable().Rows[0]["E3CDAC"].ToString());
        }
        #endregion


    }
}
