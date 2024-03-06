using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 받을어음기타관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.20 11:36
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25B6N350 : 받을어음 수정
    ///  TY_P_AC_25E4M427 : 받을어음 확인
    ///  TY_P_AC_25F8M478 : 받을어음 내역 수정
    ///  TY_P_AC_25F8N480 : 받을어음 내역 등록
    ///  TY_P_AC_25F8N481 : 받을어음 내역 삭제
    ///  TY_P_AC_25G2Z500 : 받을어음 내역 존재 체크
    ///  TY_P_AC_28K2H434 : 받을어음 내역 확인  
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_25G14492 : 거래처 여신관리 등록을 확인하세요!
    ///  TY_M_AC_25G2S497 : 동일 발행인 2000만원 초과!
    ///  TY_M_AC_25G2W499 : 무역 월마감 작업이 완료되었습니다!
    ///  TY_M_AC_25G34501 : 받을어음 내역이 존재합니다!
    ///  TY_M_AC_25G3B502 : 받을어음 상태가 발생인 경우만 처리 가능합니다!
    ///  TY_M_AC_25G3D503 : 받을어음 내역이 존재하지 않습니다!
    ///  TY_M_AC_25G3E504 : 입금일자와 상태변경일자가 다르면 처리가 불가합니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_25F8V482 : 전표번호가 존재합니다!
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  NEW : 신규
    ///  SAV : 저장
    ///  E6CDCL : 거래처코드
    ///  E6CDGL : 금융기관
    ///  E6CDSO : 발생부서
    ///  E6NMBK : 결제은행
    ///  E7CDCM : 관리부서
    ///  E7CDSB : 관리사번
    ///  E6GUBUN : 어음구분
    ///  E6IDBS : 발생구분
    ///  E6IDNR : 어음종류
    ///  E7IDBG : 상태구분
    ///  E6DTAP : 결재일자
    ///  E6DTCO : 입금일자
    ///  E6DTED : 만기일자
    ///  E6DTIS : 발행일자
    ///  E7DTBG : 상태변경일
    ///  E6AMNR : 어음금액
    ///  E6NMBS : 어음배서인
    ///  E6NMIS : 어음발행자
    ///  E6NONR : 어음번호
    /// </summary>
    public partial class TYACEI004I : TYBase
    {
        private string fsE7NONR;
        private string fsE7DTBG;
        private string fsE7IDBG;

        private TYData DAT03_E7NONR;
        private TYData DAT03_E7IDBG;
        private TYData DAT03_E7DTBG;
        private TYData DAT03_E7CDGL;
        private TYData DAT03_E7CDCM;
        private TYData DAT03_E7CDSB;
        private TYData DAT03_E7CDCL;
        private TYData DAT03_E7HIDBG;
        private TYData DAT03_E7HDTBG;
        private TYData DAT03_E7HCDGL;
        private TYData DAT03_E7NOACB;
        private TYData DAT03_E7NOACI;

        private TYData DAT04_E6NONR;
        private TYData DAT04_E6IDBG;
        private TYData DAT04_E6DTBG;
        private TYData DAT04_E6CDGL;

        #region Description : 폼로드 이벤트 
        public TYACEI004I(string sE7NONR, string sE7DTBG, string sE7IDBG)
        {
            InitializeComponent();

            this.SetPopupStyle();
            this.fsE7NONR = sE7NONR;
            this.fsE7DTBG = sE7DTBG;
            this.fsE7IDBG = sE7IDBG;

            this.DAT03_E7NONR = new TYData("DAT03_E7NONR", null);
            this.DAT03_E7IDBG = new TYData("DAT03_E7IDBG", null);
            this.DAT03_E7DTBG = new TYData("DAT03_E7DTBG", null);
            this.DAT03_E7CDGL = new TYData("DAT03_E7CDGL", null);
            this.DAT03_E7CDCM = new TYData("DAT03_E7CDCM", null);
            this.DAT03_E7CDSB = new TYData("DAT03_E7CDSB", null);
            this.DAT03_E7CDCL = new TYData("DAT03_E7CDCL", null);
            this.DAT03_E7HIDBG = new TYData("DAT03_E7HIDBG", null);
            this.DAT03_E7HDTBG = new TYData("DAT03_E7HDTBG", null);
            this.DAT03_E7HCDGL = new TYData("DAT03_E7HCDGL", null);
            this.DAT03_E7NOACB = new TYData("DAT03_E7NOACB", null);
            this.DAT03_E7NOACI = new TYData("DAT03_E7NOACI", null);

            this.DAT04_E6NONR = new TYData("DAT04_E6NONR", null);
            this.DAT04_E6IDBG = new TYData("DAT04_E6IDBG", null);
            this.DAT04_E6DTBG = new TYData("DAT04_E6DTBG", null);
            this.DAT04_E6CDGL = new TYData("DAT04_E6CDGL", null);

        }

        private void TYACEI004I_Load(object sender, System.EventArgs e)
        {
            this.ControlFactory.Add(this.DAT03_E7NONR);
            this.ControlFactory.Add(this.DAT03_E7IDBG);
            this.ControlFactory.Add(this.DAT03_E7DTBG);
            this.ControlFactory.Add(this.DAT03_E7CDGL);
            this.ControlFactory.Add(this.DAT03_E7CDCM);
            this.ControlFactory.Add(this.DAT03_E7CDSB);
            this.ControlFactory.Add(this.DAT03_E7CDCL);
            this.ControlFactory.Add(this.DAT03_E7HIDBG);
            this.ControlFactory.Add(this.DAT03_E7HDTBG);
            this.ControlFactory.Add(this.DAT03_E7HCDGL);
            this.ControlFactory.Add(this.DAT03_E7NOACB);
            this.ControlFactory.Add(this.DAT03_E7NOACI);

            this.ControlFactory.Add(this.DAT04_E6NONR);
            this.ControlFactory.Add(this.DAT04_E6IDBG);
            this.ControlFactory.Add(this.DAT04_E6DTBG);
            this.ControlFactory.Add(this.DAT04_E6CDGL);

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.TXT01_E7NONR.SetReadOnly(true);
            this.DTP01_E7DTBG.SetReadOnly(false);
            this.CBO01_E7IDBG.SetReadOnly(false);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_28K2H434", this.fsE7NONR, this.fsE7IDBG, this.fsE7DTBG);
            DataTable dt = this.DbConnector.ExecuteDataTable();

            CBH01_E7CDCM.DummyValue = dt.Rows[0]["E7DTBG"].ToString();
            CBH01_E6CDCM.DummyValue = dt.Rows[0]["E7DTBG"].ToString();

            if (dt.Rows.Count > 0)
                this.CurrentDataTableRowMapping(dt, "01");

            UP_Set_FiledLock();

            this.SetStartingFocus(DTP01_E7DTBG);
            
        }
        #endregion

        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_28K2H434", TXT01_E7NONR.GetValue(), CBO01_E7IDBG.GetValue(), DTP01_E7DTBG.GetString());
            DataTable dt = this.DbConnector.ExecuteDataTable();            
            if (dt.Rows.Count > 0)
            {
                // 수정
                this.DbConnector.Attach("TY_P_AC_25L45582", this.ControlFactory, "04");
                
                this.ControlFactory.Remove(this.DAT03_E7CDGL); 
                this.ControlFactory.Remove(this.DAT03_E7CDCL); 
                this.ControlFactory.Remove(this.DAT03_E7HIDBG); 
                this.ControlFactory.Remove(this.DAT03_E7HDTBG);
                this.ControlFactory.Remove(this.DAT03_E7HCDGL); 

                this.DbConnector.Attach("TY_P_AC_28K4V443", this.ControlFactory, "03");
            }
            else 
            {
                // 입력
                this.DbConnector.Attach("TY_P_AC_25L45582", this.ControlFactory, "04");

                this.DbConnector.Attach("TY_P_AC_25F8N480", this.ControlFactory, "03");
            }

            this.DbConnector.ExecuteTranQueryList();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();

        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int iCnt = 0;

            //무역 여신한도 체크
            //if (CBH01_E6CDCM.GetValue().ToString().Substring(0, 1) == "B")
            //{              
            //    //무역 마감 체크
            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach("TY_P_AC_25G2U498", DTP01_E6DTCO.GetValue().ToString().Substring(0, 6));                
            //    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
            //    if (iCnt > 0)
            //    {
            //        this.ShowMessage("TY_M_AC_25G2W499");
            //        e.Successed = false;
            //        return;
            //    }
            //}

            //상태변경일 이후에 자료가 존재하는지 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_25O4E640", TXT01_E7NONR.GetValue(), DTP01_E7DTBG.GetString());
            iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
            if (iCnt > 0)
            {
                this.ShowMessage("TY_M_AC_25G34501");
                e.Successed = false;
                return;
            }

            //데이타 저장
            this.DAT03_E7NONR.SetValue(TXT01_E7NONR.GetValue());
            this.DAT03_E7IDBG.SetValue(CBO01_E7IDBG.GetValue());
            this.DAT03_E7DTBG.SetValue(DTP01_E7DTBG.GetString());
            this.DAT03_E7CDGL.SetValue(CBH01_E6NMBK.GetValue());
            this.DAT03_E7CDCM.SetValue(CBH01_E7CDCM.GetValue());
            this.DAT03_E7CDSB.SetValue(CBH01_E7CDSB.GetValue());
            this.DAT03_E7CDCL.SetValue(CBH01_E6CDCL.GetValue());
            this.DAT03_E7HIDBG.SetValue(CBO01_E6IDBG.GetValue());
            this.DAT03_E7HDTBG.SetValue(DTP01_E6DTBG.GetValue());
            this.DAT03_E7HCDGL.SetValue(CBH01_E6CDGL.GetValue());
            this.DAT03_E7NOACB.SetValue("");
            this.DAT03_E7NOACI.SetValue("");

            //어음 마스타 데이타 저장
            this.DAT04_E6NONR.SetValue(TXT01_E7NONR.GetValue());
            this.DAT04_E6IDBG.SetValue(CBO01_E7IDBG.GetValue());
            this.DAT04_E6DTBG.SetValue(DTP01_E7DTBG.GetValue());
            this.DAT04_E6CDGL.SetValue(DAT03_E7HCDGL.GetValue());
            
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 사용자 정의 함수
        private void UP_Set_FiledLock()
        {
            CBO01_E6IDBG.SetReadOnly(true);
            CBH01_E6CDCL.SetReadOnly(true);
            CBH01_E6NMBK.SetReadOnly(true);
            CBO01_E6IDNR.SetReadOnly(true);
            DTP01_E6DTCO.SetReadOnly(true);
            DTP01_E6DTED.SetReadOnly(true);
            TXT01_E6AMNR.SetReadOnly(true);
            DTP01_E6DTBG.SetReadOnly(true);
            CBH01_E6CDCM.SetReadOnly(true);
            CBH01_E6CDGL.SetReadOnly(true);
        }
        #endregion     

       
    }
}
