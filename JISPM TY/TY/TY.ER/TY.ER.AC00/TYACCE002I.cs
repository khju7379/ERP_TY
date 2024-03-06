using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 신용카드 등록 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.04.04 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_24422414 : 신용카드등록 등록
    ///  TY_P_AC_24425415 : 신용카드등록 수정
    ///  TY_P_AC_2443B420 : 신용카드등록 확인
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
    ///  A6DTED : 만기일자
    ///  A6ADRS : 주　　　　소
    ///  A6CDBK : 은행코드
    ///  A6CRDT : 신용카드번호
    ///  A6DTST : 발급일자
    ///  A6EDGB : 종료월구분
    ///  A6ENDD : 결제일
    ///  A6GJED : 사용종료
    ///  A6GJST : 사용시작
    ///  A6NMPD : 회원　　성명
    ///  A6NMSA : 카　드　명
    ///  A6NOAC : 계좌번호
    ///  A6NOPS : 비밀번호
    ///  A6NOSA : 사업자　번호
    ///  A6STGB : 시작월구분
    ///  BKNMS : 은행명
    ///  VNSANGHO : 거래처명
    /// </summary>
    public partial class TYACCE002I : TYBase
    {
        private string _A6CRDT;
        private TYData DAT01_A6HISAB;
        private bool _Isloaded = false;

        public TYACCE002I(string A6CRDT)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기
            this._A6CRDT = A6CRDT;

            // 로그인 사번 가져오기
            this.DAT01_A6HISAB = new TYData("DAT01_A6HISAB", TYUserInfo.EmpNo);
 
        }

        #region Description : Page Load()
        private void TYACCE002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.ControlFactory.Add(this.DAT01_A6HISAB);

            //////콤보박스 상위 항위 
            this.CBH01_A6CDBK.CodeBoxDataBinded += new TCodeBox.TCodeBoxEventHandler(CBH01_A6CDBK_CodeBoxDataBinded);
            this.CBH01_A6NOAC.SetReadOnly(true);

            if (string.IsNullOrEmpty(this._A6CRDT))
            {
                UP_FieldClear();

                this.TXT01_A6CRDT.SetReadOnly(false);
                this.SetStartingFocus(TXT01_A6CRDT);
            }
            else
            {
                this.TXT01_A6CRDT.SetReadOnly(true);
                this.SetStartingFocus(CBH01_A6NOSA);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2443B420", this._A6CRDT);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CBH01_A6CDBK.SetValue(dt.Rows[0]["A6CDBK"]);
                    this.CurrentDataTableRowMapping(dt, "01");
                }
            }

            this._Isloaded = true;
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender ,TButton.ClickEventCheckArgs e)
        {
            if (this.CKB01_A6EXPI.GetValue().ToString().Trim() == "")
            {
                if (this.TXT01_A6DTEX.GetValue().ToString().Trim() != "")
                {
                    this.ShowMessage("TY_M_AC_2CC52114");
                    this.SetFocus(this.TXT01_A6DTEX);
                    e.Successed = false;
                    return;
                }
                else
                {
                    bool bRtn = dateValidateCheck(this.TXT01_A6DTEX.GetValue().ToString().Trim());
                    if (!bRtn)
                    {
                        this.ShowMessage("TY_M_AC_2CC52114");
                        this.SetFocus(this.TXT01_A6DTEX);
                        e.Successed = false;
                        return ;
                    }
                }

                if (this.CBO01_A6BIEX.GetValue().ToString().Trim() != "")
                {
                    this.ShowMessage("TY_M_AC_2CC53115");
                    this.SetFocus(this.CBO01_A6BIEX);
                    e.Successed = false;
                    return;
                }
            }

            if (string.IsNullOrEmpty(this._A6CRDT))
            {
                int iCnt = 0;
                this.DbConnector.CommandClear(); // 저장전 등록된 카드번호 확인 체크 (ACRDCDF)
                this.DbConnector.Attach("TY_P_AC_3CDAJ750", this.TXT01_A6CRDT.GetValue().ToString().Trim());
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
                if (iCnt != 0)
                {
                    this.ShowMessage("TY_M_AC_3CDAH749");
                    this.SetFocus(this.TXT01_A6CRDT);
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

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            string sA6EXPI = string.Empty;

            if (this.CKB01_A6EXPI.GetValue().ToString().Trim() == "N")
            {
                sA6EXPI ="";
            }
            else
            {
                sA6EXPI =this.CKB01_A6EXPI.GetValue().ToString().Trim();
            }

            if (string.IsNullOrEmpty(this._A6CRDT))
                  //this.DbConnector.Attach("TY_P_AC_24422414", this.ControlFactory, "01");
                    this.DbConnector.Attach("TY_P_AC_24422414", 
                    this.TXT01_A6CRDT.GetValue().ToString().Trim(),
                    this.CBH01_A6NOSA.GetValue().ToString().Trim(),
                    this.CBH01_A6CDBK.GetValue().ToString().Trim(),
                    this.CBH01_A6NOAC.GetValue().ToString().Trim(),
                    this.TXT01_A6NMSA.GetValue().ToString().Trim(),
                    this.TXT01_A6NMPD.GetValue().ToString().Trim(),
                    this.TXT01_A6ADRS.GetValue().ToString().Trim(),
                    this.TXT01_A6ENDD.GetValue().ToString().Trim(),
                    this.CBO01_A6STGB.GetValue().ToString().Trim(),
                    this.TXT01_A6GJST.GetValue().ToString().Trim(),
                    this.CBO01_A6EDGB.GetValue().ToString().Trim(),
                    this.TXT01_A6GJED.GetValue().ToString().Trim(),
                    this.DTP01_A6DTST.GetValue().ToString().Trim(),
                    this.DTP01_A6DTED.GetValue().ToString().Trim(),
                    this.TXT01_A6NOPS.GetValue().ToString().Trim(),
                    sA6EXPI,
                    this.TXT01_A6DTEX.GetValue().ToString().Trim(),
                    this.CBO01_A6BIEX.GetValue().ToString().Trim(),
                    TYUserInfo.EmpNo.Trim()
                    );
            else
                  //this.DbConnector.Attach("TY_P_AC_24425415", this.ControlFactory, "01");
                    this.DbConnector.Attach("TY_P_AC_24425415", 
                    this.CBH01_A6NOSA.GetValue().ToString().Trim(),
                    this.CBH01_A6CDBK.GetValue().ToString().Trim(),
                    this.CBH01_A6NOAC.GetValue().ToString().Trim(),
                    this.TXT01_A6NMSA.GetValue().ToString().Trim(),
                    this.TXT01_A6NMPD.GetValue().ToString().Trim(),
                    this.TXT01_A6ADRS.GetValue().ToString().Trim(),
                    this.TXT01_A6ENDD.GetValue().ToString().Trim(),
                    this.CBO01_A6STGB.GetValue().ToString().Trim(),
                    this.TXT01_A6GJST.GetValue().ToString().Trim(),
                    this.CBO01_A6EDGB.GetValue().ToString().Trim(),
                    this.TXT01_A6GJED.GetValue().ToString().Trim(),
                    this.DTP01_A6DTST.GetValue().ToString().Trim(),
                    this.DTP01_A6DTED.GetValue().ToString().Trim(),
                    this.TXT01_A6NOPS.GetValue().ToString().Trim(),
                    sA6EXPI,
                    this.TXT01_A6DTEX.GetValue().ToString().Trim(),
                    this.CBO01_A6BIEX.GetValue().ToString().Trim(),
                    TYUserInfo.EmpNo.Trim(),
                    this.TXT01_A6CRDT.GetValue().ToString().Trim()
                    );

            this.DbConnector.ExecuteNonQuery();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();
        } 
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 계좌번호 코드 헬프 처리
        private void CBH01_A6CDBK_CodeBoxDataBinded(object sender, EventArgs e)
        {
            string groupCode = this.CBH01_A6CDBK.GetValue().ToString();
            this.CBH01_A6NOAC.DummyValue = groupCode;
            this.CBH01_A6NOAC.SetReadOnly(string.IsNullOrEmpty(groupCode));
            if (this._Isloaded) this.CBH01_A6NOAC.Initialize();
        }
        #endregion

        #region Description : 필드 초기화
        private void UP_FieldClear()
        {
            this.TXT01_A6ENDD.SetValue("23");
            this.CBO01_A6STGB.SetValue("2");
            this.TXT01_A6GJST.SetValue("1");
            this.CBO01_A6EDGB.SetValue("2");
            this.TXT01_A6GJED.SetValue("31");
        }
        #endregion

    }
}
