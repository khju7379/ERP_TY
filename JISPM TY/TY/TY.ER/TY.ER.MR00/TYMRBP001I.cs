using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 비품마스터 관리 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.12.10 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2CA5D012 : 비품 마스터 삭제 - 이력 체크
    ///  TY_P_MR_2CA5E013 : 비품 마스터 삭제 - 수선 체크
    ///  TY_P_MR_2CA5Q018 : 비품 마스터 등록 - 순번 가져오기
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_MR_2CA6K022 : 관리 부서를 입력하세요.
    ///  TY_M_MR_2CA6K023 : 자산 분류 코드를 입력하세요.
    ///  TY_M_MR_2CA6L024 : 관리 비품명을 입력하세요.
    ///  TY_M_MR_2CA6M025 : 단위를 입력하세요.
    ///  TY_M_MR_2CA6M026 : 규격을 입력하세요.
    ///  TY_M_MR_2CA6M027 : 수량을 입력하세요.
    ///  TY_M_MR_2CA6N028 : 단가를 입력하세요.
    ///  TY_M_MR_2CA6N029 : 금액을 입력하세요.
    ///  TY_M_MR_2CA6N030 : 제조사를 입력하세요.
    ///  TY_M_MR_2CA6N031 : 거래처를 입력하세요.
    ///  TY_M_MR_2CA6O032 : 품목을 입력하세요.
    ///  TY_M_MR_2CA6O033 : 사용 부서를 입력하세요.
    ///  TY_M_MR_2CA6O034 : 설치 위치를 확인하세요
    ///  TY_M_MR_2CA6P035 : 구입 일자를 입력하세요.
    ///  TY_M_MR_2CA6P036 : 비품 이동 관리에 신규 구입 외 자료가 존재하므로 수정이 불가합니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  MABPCODE : 자산분류코드
    ///  MABUSEO : 관리부서
    ///  MACHBUSEO : 변경부서
    ///  MACHCODE : 변경코드
    ///  MACHWICHI : 변경위치
    ///  MAFRBUSEO : 사용부서
    ///  MAFRWICHI : 설치위치
    ///  MAPUMMOK : 품목코드
    ///  MAVEND : 구입처
    ///  MABUYDATE : 구입일자
    ///  MAENDATE : 무상보증기간E
    ///  MASTDATE : 무상보증기간S
    ///  MAAMOUNT : 금액
    ///  MABIGO : 비고
    ///  MABPDESC : 비품명
    ///  MACHDATE : 변경일자
    ///  MACHDESC : 변경사유
    ///  MACORP : 제조사
    ///  MAGWBUNHO : 그룹웨어문서
    ///  MAPRICE : 단가
    ///  MAQTY : 수량
    ///  MARRBUNHO : 구매입고번호
    ///  MASEQ : 순번
    ///  MASTAND1 : 규격1
    ///  MASTAND2 : 규격2
    ///  MAUNIT : 단위
    ///  MAUSE : 용도
    ///  MAYEAR : 내용년수
    ///  MAYYMM : 관리년월
    /// </summary>
    public partial class TYMRBP001I : TYBase
    {
        private TYData DAT01_MAHISAB;

        //private TYData DAT02_MOYYMM;
        //private TYData DAT02_MOSEQ;
        //private TYData DAT02_MODATE;
        //private TYData DAT02_MOBUSEO;
        //private TYData DAT02_MOFRBUSEO;
        //private TYData DAT02_MOFRWICHI;
        //private TYData DAT02_MOCHCODE;
        //private TYData DAT02_MOCHBUSEO;
        //private TYData DAT02_MOCHWICHI;
        //private TYData DAT02_MODESC;
        //private TYData DAT02_MOAMOUNT;
        //private TYData DAT02_MOVEND;
        //private TYData DAT02_MOGIVER;
        //private TYData DAT02_MODONOR;
        //private TYData DAT02_MOJPNO;
        //private TYData DAT02_MOSIGN;
        //private TYData DAT02_MOGWBUNHO;
        //private TYData DAT02_MOHISAB;

        public string fsMAYYMM = string.Empty;
        public string fsMASEQ  = string.Empty;

        #region Description : 페이지 로드
        public TYMRBP001I(string sMAYYMM, string sMASEQ)
        {
            InitializeComponent();

            // 파라미터값 가져오기
            this.fsMAYYMM = sMAYYMM;
            this.fsMASEQ  = sMASEQ;

            // 로그인 사번 가져오기
            this.DAT01_MAHISAB = new TYData("DAT01_MAHISAB", TYUserInfo.EmpNo);
        }

        private void TYMRBP001I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.ControlFactory.Add(this.DAT01_MAHISAB);
            //this.ControlFactory.Add(this.DAT02_MOHISAB);

            // 관리부서
            this.CBH01_MABUSEO.DummyValue   = DateTime.Now.ToString("yyyyMMdd");
            // 사용부서
            this.CBH01_MAFRBUSEO.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            // 변경부서
            this.CBH01_MACHBUSEO.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            this.TXT01_MASEQ.SetReadOnly(true);

            this.TXT01_MAQTY.SetReadOnly(true);
            //this.TXT01_MAAMOUNT.SetReadOnly(true);
            this.TXT01_MACHDATE.SetReadOnly(true);
            this.CBH01_MACHCODE.SetReadOnly(true);
            this.CBH01_MACHBUSEO.SetReadOnly(true);
            this.CBH01_MACHWICHI.SetReadOnly(true);
            this.TXT01_MACHDESC.SetReadOnly(true);
            this.TXT01_MARRBUNHO.SetReadOnly(true);
            this.TXT01_POM1180.SetReadOnly(true);
            this.TXT01_MAGWBUNHO.SetReadOnly(true);

            if (string.IsNullOrEmpty(this.fsMAYYMM) || string.IsNullOrEmpty(this.fsMASEQ))
            {
                this.TXT01_MAYYMM.SetReadOnly(false);

                this.SetFocus(this.TXT01_MAYYMM);
            }
            else
            {
                this.TXT01_MAYYMM.SetValue(this.fsMAYYMM.ToString());
                this.TXT01_MASEQ.SetValue(this.fsMASEQ.ToString());

                this.TXT01_MAYYMM.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_MR_2CBB4045", this.fsMAYYMM, this.fsMASEQ);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");

                this.SetFocus(this.CBH01_MABUSEO.CodeText);
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsMAYYMM) || string.IsNullOrEmpty(this.fsMASEQ)) // 등록
            {
                // 공사명 제외
                this.ControlFactory.Remove(this.TXT01_POM1180);

                this.DbConnector.Attach("TY_P_MR_2CBBD046",this.ControlFactory,"01");
            }
            else
            {
                // 공사명 제외
                this.ControlFactory.Remove(this.TXT01_POM1180);

                this.DbConnector.Attach("TY_P_MR_2CBBK047", this.ControlFactory, "01");
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            //this.DAT02_MOYYMM    = new TYData("DAT02_MOYYMM", "");
            //this.DAT02_MOSEQ     = new TYData("DAT02_MOSEQ", "");
            //this.DAT02_MODATE    = new TYData("DAT02_MODATE", "");
            //this.DAT02_MOBUSEO   = new TYData("DAT02_MOBUSEO", "");
            //this.DAT02_MOFRBUSEO = new TYData("DAT02_MOFRBUSEO", "");
            //this.DAT02_MOFRWICHI = new TYData("DAT02_MOFRWICHI", "");
            //this.DAT02_MOCHCODE  = new TYData("DAT02_MOCHCODE", "");
            //this.DAT02_MOCHBUSEO = new TYData("DAT02_MOCHBUSEO", "");
            //this.DAT02_MOCHWICHI = new TYData("DAT02_MOCHWICHI", "");
            //this.DAT02_MODESC    = new TYData("DAT02_MODESC", "");
            //this.DAT02_MOAMOUNT  = new TYData("DAT02_MOAMOUNT", "");
            //this.DAT02_MOVEND    = new TYData("DAT02_MOVEND", "");
            //this.DAT02_MOGIVER   = new TYData("DAT02_MOGIVER", "");
            //this.DAT02_MODONOR   = new TYData("DAT02_MODONOR", "");
            //this.DAT02_MOJPNO    = new TYData("DAT02_MOJPNO", "");
            //this.DAT02_MOSIGN    = new TYData("DAT02_MOSIGN", "");
            //this.DAT02_MOGWBUNHO = new TYData("DAT02_MOGWBUNHO", "");
            //this.DAT02_MOHISAB   = new TYData("DAT02_MOHISAB", "");

            if (this.TXT01_MAYYMM.GetValue().ToString().Length != 6)
            {
                this.SetFocus(this.TXT01_MAYYMM);
                this.ShowMessage("TY_M_MR_2CB3E068");
                e.Successed = false;
                return;
            }

            // 금액
            this.TXT01_MAAMOUNT.SetValue(this.TXT01_MAPRICE.GetValue().ToString());

            if (this.TXT01_MASTAND1.GetValue().ToString() == "" && this.TXT01_MASTAND2.GetValue().ToString() == "")
            {
                this.SetFocus(this.TXT01_MASTAND1);
                this.ShowMessage("TY_M_MR_2CA6M026");
                e.Successed = false;
                return;
            }

            this.fsMAYYMM = this.TXT01_MAYYMM.GetValue().ToString();

            DataTable dt = new DataTable();

            if (string.IsNullOrEmpty(this.fsMAYYMM) || string.IsNullOrEmpty(this.fsMASEQ)) // 등록
            {
                // 순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_MR_2CA5Q018", this.fsMAYYMM.ToString());
                
                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_MASEQ.SetValue(dt.Rows[0]["MASEQ"].ToString());
                }

                // 변경일자
                this.TXT01_MACHDATE.SetValue(this.DTP01_MABUYDATE.GetValue().ToString());
                // 변경코드
                this.CBH01_MACHCODE.SetValue("00");
                // 변경사유
                this.TXT01_MACHDESC.SetValue("신규구입");
                // 변경 부서
                this.CBH01_MACHBUSEO.SetValue(this.CBH01_MAFRBUSEO.GetValue().ToString());
                // 변경 위치
                this.CBH01_MACHWICHI.SetValue(this.CBH01_MAFRWICHI.GetValue().ToString());

                //this.DAT02_MOYYMM    = new TYData("DAT02_MOYYMM",    this.TXT01_MAYYMM.GetValue().ToString());
                //this.DAT02_MOSEQ     = new TYData("DAT02_MOSEQ",     this.TXT01_MASEQ.GetValue().ToString());
                //this.DAT02_MODATE    = new TYData("DAT02_MODATE",    this.DTP01_MABUYDATE.GetValue().ToString());
                //this.DAT02_MOBUSEO   = new TYData("DAT02_MOBUSEO",   this.CBH01_MABUSEO.GetValue().ToString());
                //this.DAT02_MOFRBUSEO = new TYData("DAT02_MOFRBUSEO", this.CBH01_MAFRBUSEO.GetValue().ToString());
                //this.DAT02_MOFRWICHI = new TYData("DAT02_MOFRWICHI", this.CBH01_MAFRWICHI.GetValue().ToString());
                //this.DAT02_MOCHCODE  = new TYData("DAT02_MOCHCODE",  this.CBH01_MACHCODE.GetValue().ToString());
                //this.DAT02_MOCHBUSEO = new TYData("DAT02_MOCHBUSEO", this.CBH01_MACHBUSEO.GetValue().ToString());
                //this.DAT02_MOCHWICHI = new TYData("DAT02_MOCHWICHI", this.CBH01_MACHWICHI.GetValue().ToString());
                //this.DAT02_MODESC    = new TYData("DAT02_MODESC",    this.TXT01_MACHDESC.GetValue().ToString());
                //this.DAT02_MOAMOUNT  = new TYData("DAT02_MOAMOUNT",  "0");
                //this.DAT02_MOVEND    = new TYData("DAT02_MOVEND",    "");
                //this.DAT02_MOGIVER   = new TYData("DAT02_MOGIVER",   "");
                //this.DAT02_MODONOR   = new TYData("DAT02_MODONOR",   "");
                //this.DAT02_MOJPNO    = new TYData("DAT02_MOJPNO",    "");
                //this.DAT02_MOSIGN    = new TYData("DAT02_MOSIGN",    "");
                //this.DAT02_MOGWBUNHO = new TYData("DAT02_MOGWBUNHO", "");
                //this.DAT02_MOHISAB   = new TYData("DAT02_MOHISAB", TYUserInfo.EmpNo);
            }
            else // 수정
            {
                //// 변경 위치
                //this.CBH01_MACHWICHI.SetValue(this.CBH01_MAFRWICHI.GetValue().ToString());

                // 이력 자료 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_MR_2CA5D012",
                                       this.TXT01_MAYYMM.GetValue().ToString(),
                                       this.TXT01_MASEQ.GetValue().ToString()
                                       );

                if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_MR_2CA6P036");
                    e.Successed = false;
                    return;
                }

                //// 수선 자료 존재 체크
                //this.DbConnector.CommandClear();
                //this.DbConnector.Attach(
                //                       "TY_P_MR_2CA5E013",
                //                       this.TXT01_MAYYMM.GetValue().ToString(),
                //                       this.TXT01_MASEQ.GetValue().ToString()
                //                       );

                //if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
                //{
                //    this.ShowMessage("TY_M_MR_2CB2B067");
                //    e.Successed = false;
                //    return;
                //}
            }
        }
        #endregion

        private void TXT01_MAYYMM_TextChanged(object sender, EventArgs e)
        {
            if (this.TXT01_MAYYMM.GetValue().ToString().Length == 6)
            {
                // 관리부서
                this.CBH01_MABUSEO.DummyValue   = this.TXT01_MAYYMM.GetValue().ToString() + "01";
                // 사용부서
                this.CBH01_MAFRBUSEO.DummyValue = this.TXT01_MAYYMM.GetValue().ToString() + "01";
                // 변경부서
                this.CBH01_MACHBUSEO.DummyValue = this.TXT01_MAYYMM.GetValue().ToString() + "01";
            }
        }
    }
}
