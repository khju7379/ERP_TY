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

    public partial class TYACSE003I : TYBase
    {
        private string sSABUN = string.Empty;

        private TYData DAT03_AINTAGUBN;
        private TYData DAT03_AINTAYEAR;
        private TYData DAT03_AINTASEQ;
        private TYData DAT03_AIDATEACQ;
        private TYData DAT03_AIREVCDDP;
        private TYData DAT03_AINTAUNIT;
        private TYData DAT03_AINTASTAN;
        private TYData DAT03_AINTAQUAN;
        private TYData DAT03_AICOSTACQ;
        private TYData DAT03_AIASSETNM;
        private TYData DAT03_AIASSETCD;
        private TYData DAT03_AIUSEINT;
        private TYData DAT03_AIDURABLE;
        private TYData DAT03_AIASSEINF;

        private string fsAINTAGUBN;
        private string fsAINTAYEAR;
        private string fsAINTASEQ;
        private string fsGUBUN;

        #region  Description : 폼 로드 이벤트
        public TYACSE003I(string sAINTAGUBN, string sAINTAYEAR, string sAINTASEQ, string sGUBUN)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.fsAINTAGUBN = sAINTAGUBN;
            this.fsAINTAYEAR = sAINTAYEAR;
            this.fsAINTASEQ  = sAINTASEQ;
            this.fsGUBUN = sGUBUN;

            this.DAT03_AINTAGUBN = new TYData("DAT03_AINTAGUBN", null);
            this.DAT03_AINTAYEAR = new TYData("DAT03_AINTAYEAR", null);
            this.DAT03_AINTASEQ  = new TYData("DAT03_AINTASEQ",  null);
            this.DAT03_AIDATEACQ = new TYData("DAT03_AIDATEACQ", null);
            this.DAT03_AIREVCDDP = new TYData("DAT03_AIREVCDDP", null);
            this.DAT03_AINTAUNIT = new TYData("DAT03_AINTAUNIT", null);
            this.DAT03_AINTASTAN = new TYData("DAT03_AINTASTAN", null);
            this.DAT03_AINTAQUAN = new TYData("DAT03_AINTAQUAN", null);
            this.DAT03_AICOSTACQ = new TYData("DAT03_AICOSTACQ", null);
            this.DAT03_AIASSETNM = new TYData("DAT03_AIASSETNM", null);
            this.DAT03_AIASSETCD = new TYData("DAT03_AIASSETCD", null);
            this.DAT03_AIUSEINT  = new TYData("DAT03_AIUSEINT",  null);
            this.DAT03_AIDURABLE = new TYData("DAT03_AIDURABLE", null);
            this.DAT03_AIASSEINF = new TYData("DAT03_AIASSEINF", null);
        }

        private void TYACSE003I_Load(object sender, System.EventArgs e)
        {
            // 로그인 사번 가져오기
            this.sSABUN = TYUserInfo.EmpNo.Trim().ToUpper();

            this.ControlFactory.Add(this.DAT03_AINTAGUBN);
            this.ControlFactory.Add(this.DAT03_AINTAYEAR);
            this.ControlFactory.Add(this.DAT03_AINTASEQ);
            this.ControlFactory.Add(this.DAT03_AIDATEACQ);
            this.ControlFactory.Add(this.DAT03_AIREVCDDP);
            this.ControlFactory.Add(this.DAT03_AINTAUNIT);
            this.ControlFactory.Add(this.DAT03_AINTASTAN);
            this.ControlFactory.Add(this.DAT03_AINTAQUAN);
            this.ControlFactory.Add(this.DAT03_AICOSTACQ);
            this.ControlFactory.Add(this.DAT03_AIASSETNM);
            this.ControlFactory.Add(this.DAT03_AIASSETCD);
            this.ControlFactory.Add(this.DAT03_AIUSEINT);
            this.ControlFactory.Add(this.DAT03_AIDURABLE);
            this.ControlFactory.Add(this.DAT03_AIASSEINF);

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.TXT01_AINTAYEAR.SetValue(DateTime.Now.ToString("yyyy"));

            this.CBH01_AIREVCDDP.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            //신규
            if (string.IsNullOrEmpty(this.fsGUBUN))
            {
                this.CBO01_AINTAGUBN.SetReadOnly(false);
                this.TXT01_AINTAYEAR.SetReadOnly(false);
                this.TXT01_AINTASEQ.SetReadOnly(false);

                SetFocus(this.CBO01_AINTAGUBN);
            }
            else
            {
                this.CBO01_AINTAGUBN.SetValue(fsAINTAGUBN);
                this.TXT01_AINTAYEAR.SetValue(fsAINTAYEAR);
                this.TXT01_AINTASEQ.SetValue(Set_Fill4(fsAINTASEQ));

                this.CBO01_AINTAGUBN.SetReadOnly(true);
                this.TXT01_AINTAYEAR.SetReadOnly(true);
                this.TXT01_AINTASEQ.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_MR_43H5K755",
                    this.CBO01_AINTAGUBN.GetValue().ToString(),
                    this.TXT01_AINTAYEAR.GetValue().ToString(),
                    this.TXT01_AINTASEQ.GetValue().ToString()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");

                SetFocus(this.DTP01_AIDATEACQ);
            }

            UP_SET_ReadOnly();
        }
        #endregion

        private void UP_SET_ReadOnly()
        {
            this.TXT01_ARYLASTAMT.SetReadOnly(true);
            this.TXT01_ARMLASTAMT.SetReadOnly(true);
            this.TXT01_ARMFIXDAMT.SetReadOnly(true);
            this.TXT01_ARTFIXDAMT.SetReadOnly(true);
            this.TXT01_AROFIXDAMT.SetReadOnly(true);
            this.TXT01_ARJFIXDAMT.SetReadOnly(true);
        }

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            //고정자산 마스타 저장
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsGUBUN))
            {
                this.DbConnector.Attach("TY_P_AC_43H3P750", this.ControlFactory, "03"); // 저장
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_43H3Q751", this.ControlFactory, "03"); // 수정
            }
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion        

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region Description : 저장 BTN61_SAV ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (string.IsNullOrEmpty(this.fsGUBUN))
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43I2C762",
                    this.CBO01_AINTAGUBN.GetValue().ToString(),
                    this.TXT01_AINTAYEAR.GetValue().ToString()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_AINTASEQ.SetValue(dt.Rows[0][0].ToString());
                }
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_43E5K730",
                    this.CBO01_AINTAGUBN.GetValue().ToString(),
                    this.TXT01_AINTAYEAR.GetValue().ToString(),
                    this.TXT01_AINTASEQ.GetValue().ToString()
                    );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_43E5N733");
                    e.Successed = false;
                    return;
                }
            }

            this.DAT03_AINTAGUBN.SetValue(this.CBO01_AINTAGUBN.GetValue().ToString());
            this.DAT03_AINTAYEAR.SetValue(this.TXT01_AINTAYEAR.GetValue().ToString());
            this.DAT03_AINTASEQ.SetValue(this.TXT01_AINTASEQ.GetValue().ToString());
            this.DAT03_AIDATEACQ.SetValue(this.DTP01_AIDATEACQ.GetValue().ToString());
            this.DAT03_AIREVCDDP.SetValue(this.CBH01_AIREVCDDP.GetValue().ToString());
            this.DAT03_AINTAUNIT.SetValue(this.TXT01_AINTAUNIT.GetValue().ToString());
            this.DAT03_AINTASTAN.SetValue(this.TXT01_AINTASTAN.GetValue().ToString());
            this.DAT03_AINTAQUAN.SetValue(this.TXT01_AINTAQUAN.GetValue().ToString());
            this.DAT03_AICOSTACQ.SetValue(this.TXT01_AICOSTACQ.GetValue().ToString());
            this.DAT03_AIASSETNM.SetValue(this.TXT01_AIASSETNM.GetValue().ToString());
            this.DAT03_AIASSETCD.SetValue(this.CBH01_AIASSETCD.GetValue().ToString());
            this.DAT03_AIUSEINT.SetValue(this.TXT01_AIUSEINT.GetValue().ToString());
            this.DAT03_AIDURABLE.SetValue(this.TXT01_AIDURABLE.GetValue().ToString());
            this.DAT03_AIASSEINF.SetValue(this.CBH01_AIASSEINF.GetValue().ToString());

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

        }
        #endregion

        #region  Description : 취득일자 이벤트
        private void DTP01_AIDATEACQ_Leave(object sender, EventArgs e)
        {
            this.CBH01_AIREVCDDP.DummyValue = this.DTP01_AIDATEACQ.GetValue().ToString().Replace("-", "");
        }
        #endregion
    }
}
