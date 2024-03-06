using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.US00
{
    /// <summary>
    /// 계약관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2016.06.08 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66FFQ222 : 회계 거래처 코드의 거래처구분 체크
    ///  TY_P_UT_66FFV223 : 계약관리 확인
    ///  TY_P_UT_66FFW224 : 계약관리 수정
    ///  TY_P_UT_66FFY225 : 계약관리 등록
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
    ///  CNCONTGB : 계약구분
    ///  CNCURRCD : 화폐구분
    ///  CNHWAJU : 화주
    ///  CNHWAMUL : 화물
    ///  CNBOHP : 보관료화폐
    ///  CNBOJDA : 보장물동단위
    ///  CNBOJHP : 보장물동화폐
    ///  CNBUDUHP : 부두사용료화폐
    ///  CNCANHP : CAN 화폐
    ///  CNCHDA : 출고단위
    ///  CNCHHP : 출고화폐
    ///  CNDRHP : DRUM 화폐
    ///  CNHANDDA : 취급료단위
    ///  CNHANDHP : 취급료화폐
    ///  CNIPDA : 입고단위
    ///  CNIPHP : 입고화폐
    ///  CNISDA : 이송단위
    ///  CNISHP : 이송화폐
    ///  CNJILHP : 질소충전비화폐
    ///  CNTOJIHP : 토지사용료화폐
    ///  CNCONTEN : 계약종료일자
    ///  CNCONTIL : 계약일자
    ///  CNCONTST : 계약시작일자
    ///  CNBOAM : 보관료
    ///  CNBOJAM : 보장물동금액
    ///  CNBOJMON : 보장물동월수
    ///  CNBOJPER : 보장물동퍼센트
    ///  CNBOJQTY : 보장물동수량
    ///  CNBUDU : 부두사용료
    ///  CNCANAM : CAN 금액
    ///  CNCAPA : 용량
    ///  CNCHAM : 출고금액
    ///  CNYEAR : 계약번호
    ///  CNDRAM : DRUM 금액
    ///  CNHANDAM : 취급료
    ///  CNHANDOV : 취급료할증율
    ///  CNHAYKOV : 하역료할증율
    ///  CNIPAM : 입고금액
    ///  CNISAM : 이송금액
    ///  CNJILSO : 질소충전비
    ///  CNRATE : 환율
    ///  CNREQGB : 청구구분
    ///  CNSHOTA : 자연감모
    ///  CNTANKNO : 탱크번호
    ///  CNTOJI : 토지사용료
    /// </summary>
    public partial class TYUSKB003I : TYBase
    {
        private string fsCNYEAR;
        private string fsCNSEQ;

        #region Description : 페이지 로드
        public TYUSKB003I(string sCNYEAR, string sCNSEQ)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기 
            this.fsCNYEAR = sCNYEAR;
            this.fsCNSEQ  = sCNSEQ;
        }

        private void TYUSKB003I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.FPS91_TY_S_US_919H3469.Initialize();

            if (string.IsNullOrEmpty(this.fsCNYEAR) && string.IsNullOrEmpty(this.fsCNSEQ))
            {
                this.TXT01_CNYEAR.SetValue(DateTime.Now.ToString("yyyy"));

                this.TXT01_CNYEAR.SetReadOnly(false);
                this.TXT01_CNSEQ.SetReadOnly(true);

                SetStartingFocus(this.TXT01_CNYEAR);
            }
            else
            {
                this.TXT01_CNYEAR.SetReadOnly(true);
                this.TXT01_CNSEQ.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_8BJHK186", this.fsCNYEAR, this.fsCNSEQ);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "01");
                }

                // 보관료 단가 조회
                UP_SEL_USIBOKDNF("");

                SetStartingFocus(this.CBH01_CNHWAJU1.CodeText);
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            this.fsCNYEAR = "";

            this.TXT01_CNYEAR.SetValue(DateTime.Now.ToString("yyyy"));

            this.TXT01_CNSEQ.SetValue("");

            this.TXT01_CNYEAR.SetReadOnly(false);
            this.TXT01_CNSEQ.SetReadOnly(true);

            this.TXT01_CNYEAR.Focus();
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sCNYEAR  = string.Empty;
            string sCNSOSOK = string.Empty;

            sCNYEAR = this.TXT01_CNYEAR.GetValue().ToString().Trim() + "-" + Set_Fill3(this.TXT01_CNSEQ.GetValue().ToString().Trim());

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_987CU085", this.CBH01_CNHWAJU1.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sCNSOSOK = dt.Rows[0]["VNSOSOK"].ToString();
            }

            if (string.IsNullOrEmpty(this.fsCNYEAR))
            {
                // 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_8BKBS196",
                                        this.TXT01_CNYEAR.GetValue().ToString().Trim(),
                                        this.TXT01_CNSEQ.GetValue().ToString().Trim(),
                                        Get_Date(this.DTP01_CNSTDAT.GetValue().ToString().Trim()),
                                        Get_Date(this.DTP01_CNENDAT.GetValue().ToString().Trim()),
                                        sCNSOSOK.ToString(),
                                        this.CBH01_CNHWAJU1.GetValue().ToString().Trim(),
                                        this.CBH01_CNHWAJU2.GetValue().ToString().Trim(),
                                        this.CBO01_CNSISULGB.GetValue().ToString().Trim(),
                                        Get_Numeric(this.TXT01_CNSISULAMT.GetValue().ToString()).Trim(),
                                        this.CBO01_CNSISULVAT.GetValue().ToString().Trim(),
                                        Get_Numeric(this.TXT01_CNHAYAMT.GetValue().ToString().Trim()),
                                        this.CBO01_CNHAYVAT.GetValue().ToString().Trim(),
                                        this.CBH01_CNBOKDNGUBN.GetValue().ToString().Trim(),
                                        Get_Numeric(this.TXT01_CNBOKGIGAN.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_CNBOKKIBON.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_CNBOKOVDAT.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_CNBOKOVAMT.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_CNDISBIYUL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_CNDEMBIYUL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_CNBYISSAMT.GetValue().ToString().Trim()),
                                        this.CBO01_CNBYISSVAT.GetValue().ToString().Trim(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper().Trim()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }
            else
            {
                // 수정
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_8BKBS197",
                                        Get_Date(this.DTP01_CNSTDAT.GetValue().ToString().Trim()),
                                        Get_Date(this.DTP01_CNENDAT.GetValue().ToString().Trim()),
                                        sCNSOSOK.ToString(),
                                        this.CBH01_CNHWAJU1.GetValue().ToString().Trim(),
                                        this.CBH01_CNHWAJU2.GetValue().ToString().Trim(),
                                        this.CBO01_CNSISULGB.GetValue().ToString().Trim(),
                                        Get_Numeric(this.TXT01_CNSISULAMT.GetValue().ToString()).Trim(),
                                        this.CBO01_CNSISULVAT.GetValue().ToString().Trim(),
                                        Get_Numeric(this.TXT01_CNHAYAMT.GetValue().ToString().Trim()),
                                        this.CBO01_CNHAYVAT.GetValue().ToString().Trim(),
                                        this.CBH01_CNBOKDNGUBN.GetValue().ToString().Trim(),
                                        Get_Numeric(this.TXT01_CNBOKGIGAN.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_CNBOKKIBON.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_CNBOKOVDAT.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_CNBOKOVAMT.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_CNDISBIYUL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_CNDEMBIYUL.GetValue().ToString().Trim()),
                                        Get_Numeric(this.TXT01_CNBYISSAMT.GetValue().ToString().Trim()),
                                        this.CBO01_CNBYISSVAT.GetValue().ToString().Trim(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                        this.TXT01_CNYEAR.GetValue().ToString().Trim(),
                                        this.TXT01_CNSEQ.GetValue().ToString().Trim()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

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

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 시설사용료VAT
            if (this.TXT01_CNSISULAMT.GetValue().ToString().Trim() != "")
            {
                if ((this.CBO01_CNSISULVAT.GetValue().ToString().Trim()) != "N" && (this.CBO01_CNSISULVAT.GetValue().ToString().Trim() != "Y"))
                {
                    this.ShowMessage("TY_M_US_8BKDB198");
                    this.SetFocus(this.CBO01_CNSISULVAT);

                    e.Successed = false;
                    return;
                }
            }

            //// 화물료VAT
            //if (this.TXT01_CNHMAMT.GetValue().ToString().Trim() != "")
            //{
            //    if ((this.CBO01_CNHMVAT.GetValue().ToString().Trim()) != "N" && (this.CBO01_CNHMVAT.GetValue().ToString().Trim() != "Y"))
            //    {
            //        this.ShowMessage("TY_M_US_8BKDB198");
            //        this.SetFocus(this.CBO01_CNHMVAT);

            //        e.Successed = false;
            //        return;
            //    }
            //}

            // 하역료VAT
            if (this.TXT01_CNHAYAMT.GetValue().ToString().Trim() != "")
            {
                if ((this.CBO01_CNHAYVAT.GetValue().ToString().Trim()) != "N" && (this.CBO01_CNHAYVAT.GetValue().ToString().Trim() != "Y"))
                {
                    this.ShowMessage("TY_M_US_8BKDB198");
                    this.SetFocus(this.CBO01_CNHAYVAT);

                    e.Successed = false;
                    return;
                }
            }

            //// 부원료보관료VAT
            //if (this.TXT01_CNBYHAYAMT.GetValue().ToString().Trim() != "")
            //{
            //    if ((this.CBO01_CNBYHAYVAT.GetValue().ToString().Trim()) != "N" && (this.CBO01_CNBYHAYVAT.GetValue().ToString().Trim() != "Y"))
            //    {
            //        this.ShowMessage("TY_M_US_8BKDB198");
            //        this.SetFocus(this.CBO01_CNBYHAYVAT);

            //        e.Successed = false;
            //        return;
            //    }
            //}

            // 부원료이송료VAT
            if (this.TXT01_CNBYISSAMT.GetValue().ToString().Trim() != "")
            {
                if ((this.CBO01_CNBYISSVAT.GetValue().ToString().Trim()) != "N" && (this.CBO01_CNBYISSVAT.GetValue().ToString().Trim() != "Y"))
                {
                    this.ShowMessage("TY_M_US_8BKDB198");
                    this.SetFocus(this.CBO01_CNBYISSVAT);

                    e.Successed = false;
                    return;
                }
            }

            if (string.IsNullOrEmpty(this.fsCNYEAR))
            {
                // 계약순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_8BKDE199", this.TXT01_CNYEAR.GetValue().ToString().Trim());
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_CNSEQ.SetValue(dt.Rows[0][0].ToString());
                }
            }
            else
            {
                string sCONTNO = string.Empty;

                sCONTNO = this.TXT01_CNYEAR.GetValue().ToString() + Set_Fill2(this.TXT01_CNSEQ.GetValue().ToString());

                // 계약순번 가져오기
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_91BD2480", sCONTNO.ToString().Trim());
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_US_91BDB481");
                    this.SetFocus(this.DTP01_CNSTDAT);

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

        #region Description : 보관료 단가 조회
        private void UP_SEL_USIBOKDNF(string sBKDNGUBN)
        {
            this.FPS91_TY_S_US_919H3469.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_919H2468", sBKDNGUBN.ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_919H3469.SetValue(dt);
        }
        #endregion

        #region Description : 할증일수 엔터키
        private void TXT01_CNBYOVDAT_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(this.BTN61_SAV);
            }
        }
        #endregion

        #region Description : 보관료 단가 조회
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            // 보관료 단가 조회
            UP_SEL_USIBOKDNF("");
        }
        #endregion

        #region Description : 보관료 단가 관리
        private void BTN61_SILOCODEHELP01_Click(object sender, EventArgs e)
        {
            TYUSKB03C1 popup = new TYUSKB03C1("");

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // 보관료 단가 조회
                UP_SEL_USIBOKDNF("");
            }
        }
        #endregion

    }
}