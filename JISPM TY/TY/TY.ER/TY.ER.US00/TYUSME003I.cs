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
    public partial class TYUSME003I : TYBase
    {
        private string fsLTHANGCHA;
        private string fsLTGOKJONG;

        #region Description : 페이지 로드
        public TYUSME003I(string sLTHANGCHA, string sLTGOKJONG)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기 
            this.fsLTHANGCHA  = sLTHANGCHA;
            this.fsLTGOKJONG  = sLTGOKJONG;
        }

        private void TYUSME003I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.FPS91_TY_S_US_928EJ693.Initialize();

            if (string.IsNullOrEmpty(this.fsLTHANGCHA) && string.IsNullOrEmpty(this.fsLTGOKJONG))
            {
                this.BTN61_GET.Visible = true;

                this.CBH01_LTHANGCHA.SetReadOnly(false);
                this.CBH01_LTGOKJONG.SetReadOnly(false);

                SetStartingFocus(this.CBH01_LTHANGCHA.CodeText);
            }
            else
            {
                this.BTN61_GET.Visible = false;

                this.CBH01_LTHANGCHA.SetReadOnly(true);
                this.CBH01_LTGOKJONG.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_928DM691", this.fsLTHANGCHA, this.fsLTGOKJONG);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "01");
                }

                // LAYTIME STATEMENT 팝업 조회
                UP_SEL_USIMCLTF(this.fsLTHANGCHA, this.fsLTGOKJONG);

                SetStartingFocus(this.TXT01_LTTOTSAVE);
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            this.BTN61_GET.Visible = true;

            this.fsLTHANGCHA = "";
            this.fsLTGOKJONG = "";

            UP_FieldClear();

            this.CBH01_LTHANGCHA.SetValue("");
            this.CBH01_LTGOKJONG.SetValue("");

            this.CBH01_LTHANGCHA.SetReadOnly(false);
            this.CBH01_LTGOKJONG.SetReadOnly(false);

            this.SetFocus(this.CBH01_LTHANGCHA.CodeText);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {

            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            string sLTSEQ01   = string.Empty;
            string sLTSEQ02   = string.Empty;
            string sLTSEQ03   = string.Empty;
            string sLTSEQ04   = string.Empty;
            string sLTSEQ05   = string.Empty;
            string sLTSEQ06   = string.Empty;
            string sLTSEQ07   = string.Empty;
            string sLTSEQ08   = string.Empty;
            string sLTSEQ09   = string.Empty;
            string sLTSEQ10   = string.Empty;
            string sLTSEQ11   = string.Empty;
            string sLTSEQ12   = string.Empty;
            string sLTSEQ13   = string.Empty;
            string sLTSEQ14   = string.Empty;
            string sLTSEQ15   = string.Empty;
            string sLTSEQ16   = string.Empty;
            string sLTSEQ17   = string.Empty;
            string sLTSEQ18   = string.Empty;
            string sLTSEQ19   = string.Empty;
            string sLTSEQ20   = string.Empty;
            string sLTSEQ21   = string.Empty;
            string sLTSEQ22   = string.Empty;
            string sLTSEQ23   = string.Empty;
            string sLTSEQ24   = string.Empty;

            string sLTHWAJU01 = string.Empty;
            string sLTHWAJU02 = string.Empty;
            string sLTHWAJU03 = string.Empty;
            string sLTHWAJU04 = string.Empty;
            string sLTHWAJU05 = string.Empty;
            string sLTHWAJU06 = string.Empty;
            string sLTHWAJU07 = string.Empty;
            string sLTHWAJU08 = string.Empty;
            string sLTHWAJU09 = string.Empty;
            string sLTHWAJU10 = string.Empty;
            string sLTHWAJU11 = string.Empty;
            string sLTHWAJU12 = string.Empty;
            string sLTHWAJU13 = string.Empty;
            string sLTHWAJU14 = string.Empty;
            string sLTHWAJU15 = string.Empty;
            string sLTHWAJU16 = string.Empty;
            string sLTHWAJU17 = string.Empty;
            string sLTHWAJU18 = string.Empty;
            string sLTHWAJU19 = string.Empty;
            string sLTHWAJU20 = string.Empty;
            string sLTHWAJU21 = string.Empty;
            string sLTHWAJU22 = string.Empty;
            string sLTHWAJU23 = string.Empty;
            string sLTHWAJU24 = string.Empty;

            string sLTTENTIME = string.Empty;
            string sLTACCTIME = string.Empty;
            string sLTSTTIME  = string.Empty;

            string sLTALLOW   = string.Empty;
            string sLTEXCEP   = string.Empty;
            string sLTUSED    = string.Empty;
            string sLTSAVE    = string.Empty;

            sLTSEQ01 = "0";
            sLTSEQ02 = "0";
            sLTSEQ03 = "0";
            sLTSEQ04 = "0";
            sLTSEQ05 = "0";
            sLTSEQ06 = "0";
            sLTSEQ07 = "0";
            sLTSEQ08 = "0";
            sLTSEQ09 = "0";
            sLTSEQ10 = "0";
            sLTSEQ11 = "0";
            sLTSEQ12 = "0";
            sLTSEQ13 = "0";
            sLTSEQ14 = "0";
            sLTSEQ15 = "0";
            sLTSEQ16 = "0";
            sLTSEQ17 = "0";
            sLTSEQ18 = "0";
            sLTSEQ19 = "0";
            sLTSEQ20 = "0";
            sLTSEQ21 = "0";
            sLTSEQ22 = "0";
            sLTSEQ23 = "0";
            sLTSEQ24 = "0";

            sLTHWAJU01 = "";
            sLTHWAJU02 = "";
            sLTHWAJU03 = "";
            sLTHWAJU04 = "";
            sLTHWAJU05 = "";
            sLTHWAJU06 = "";
            sLTHWAJU07 = "";
            sLTHWAJU08 = "";
            sLTHWAJU09 = "";
            sLTHWAJU10 = "";
            sLTHWAJU11 = "";
            sLTHWAJU12 = "";
            sLTHWAJU13 = "";
            sLTHWAJU14 = "";
            sLTHWAJU15 = "";
            sLTHWAJU16 = "";
            sLTHWAJU17 = "";
            sLTHWAJU18 = "";
            sLTHWAJU19 = "";
            sLTHWAJU20 = "";
            sLTHWAJU21 = "";
            sLTHWAJU22 = "";
            sLTHWAJU23 = "";
            sLTHWAJU24 = "";


            // 순번, 화주 가져오기
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    sLTSEQ01 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU01 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 1)
                {
                    sLTSEQ02 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU02 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 2)
                {
                    sLTSEQ03 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU03 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 3)
                {
                    sLTSEQ04 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU04 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 4)
                {
                    sLTSEQ05 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU05 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 5)
                {
                    sLTSEQ06 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU06 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 6)
                {
                    sLTSEQ07 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU07 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 7)
                {
                    sLTSEQ08 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU08 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 8)
                {
                    sLTSEQ09 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU09 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 9)
                {
                    sLTSEQ10 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU10 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 10)
                {
                    sLTSEQ11 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU11 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 11)
                {
                    sLTSEQ12 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU12 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 12)
                {
                    sLTSEQ13 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU13 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 13)
                {
                    sLTSEQ14 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU14 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 14)
                {
                    sLTSEQ15 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU15 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 15)
                {
                    sLTSEQ16 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU16 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 16)
                {
                    sLTSEQ17 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU17 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 17)
                {
                    sLTSEQ18 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU18 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 18)
                {
                    sLTSEQ19 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU19 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 19)
                {
                    sLTSEQ20 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU20 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }


                if (i == 20)
                {
                    sLTSEQ21 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU21 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 21)
                {
                    sLTSEQ22 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU22 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 22)
                {
                    sLTSEQ23 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU23 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }

                if (i == 23)
                {
                    sLTSEQ24 = ds.Tables[0].Rows[i]["LTSEQ"].ToString();
                    sLTHWAJU24 = ds.Tables[0].Rows[i]["LTHWAJU"].ToString();
                }
            }

            sLTTENTIME = Set_Fill2(this.TXT01_LTTENHH.GetValue().ToString()) + Set_Fill2(this.TXT01_LTTENMM.GetValue().ToString());
            sLTACCTIME = Set_Fill2(this.TXT01_LTACCHH.GetValue().ToString()) + Set_Fill2(this.TXT01_LTACCMM.GetValue().ToString());
            sLTSTTIME  = Set_Fill2(this.TXT01_LTSTHH.GetValue().ToString()) + Set_Fill2(this.TXT01_LTSTMM.GetValue().ToString());

            sLTALLOW   = this.TXT01_LTALLOW.GetValue().ToString().Substring(0, 2) + this.TXT01_LTALLOW.GetValue().ToString().Substring(3, 2) + this.TXT01_LTALLOW.GetValue().ToString().Substring(6, 2);
            sLTEXCEP   = this.TXT01_LTEXCEP.GetValue().ToString().Substring(0, 2) + this.TXT01_LTEXCEP.GetValue().ToString().Substring(3, 2) + this.TXT01_LTEXCEP.GetValue().ToString().Substring(6, 2);
            sLTUSED    = this.TXT01_LTUSED.GetValue().ToString().Substring(0, 2) + this.TXT01_LTUSED.GetValue().ToString().Substring(3, 2) + this.TXT01_LTUSED.GetValue().ToString().Substring(6, 2);
            sLTSAVE    = this.TXT01_LTSAVE.GetValue().ToString().Substring(0, 2) + this.TXT01_LTSAVE.GetValue().ToString().Substring(3, 2) + this.TXT01_LTSAVE.GetValue().ToString().Substring(6, 2);


            // LAYTIME STATEMENT 등록
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsLTHANGCHA) && string.IsNullOrEmpty(this.fsLTGOKJONG))
            {
                // 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92BDJ709",
                                        this.CBH01_LTHANGCHA.GetValue().ToString().Trim(),
                                        this.CBH01_LTGOKJONG.GetValue().ToString().Trim(),
                                        this.TXT01_LTCONTNO.GetValue().ToString(),
                                        this.CBO01_LTGUBUN.GetValue().ToString(),
                                        this.TXT01_LTBLQTY.GetValue().ToString(),
                                        Get_Date(this.DTP01_LTTENDATE.GetValue().ToString()),
                                        sLTTENTIME.ToString(),
                                        Get_Date(this.DTP01_LTACCDATE.GetValue().ToString()),
                                        sLTACCTIME.ToString(),
                                        Get_Date(this.DTP01_LTSTDATE.GetValue().ToString()),
                                        sLTSTTIME.ToString(),
                                        Get_Numeric(this.TXT01_LTQTY.GetValue().ToString()),
                                        sLTALLOW.ToString(),
                                        sLTEXCEP.ToString(),
                                        sLTUSED.ToString(),
                                        sLTSAVE.ToString(),
                                        Get_Numeric(this.TXT01_LTDAYSAVE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_LTTOTSAVE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_LTTOTAMT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_LTTYCSAVE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_LTTYCNHSAVE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_LTTYCBSSAVE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_LTHJSAVE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_LTHJNHSAVE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_LTHJBSSAVE.GetValue().ToString()),
                                        sLTSEQ01.ToString(),
                                        sLTHWAJU01.ToString(),
                                        sLTSEQ02.ToString(),
                                        sLTHWAJU02.ToString(),
                                        sLTSEQ03.ToString(),
                                        sLTHWAJU03.ToString(),
                                        sLTSEQ04.ToString(),
                                        sLTHWAJU04.ToString(),
                                        sLTSEQ05.ToString(),
                                        sLTHWAJU05.ToString(),
                                        sLTSEQ06.ToString(),
                                        sLTHWAJU06.ToString(),
                                        sLTSEQ07.ToString(),
                                        sLTHWAJU07.ToString(),
                                        sLTSEQ08.ToString(),
                                        sLTHWAJU08.ToString(),
                                        sLTSEQ09.ToString(),
                                        sLTHWAJU09.ToString(),
                                        sLTSEQ10.ToString(),
                                        sLTHWAJU10.ToString(),
                                        sLTSEQ11.ToString(),
                                        sLTHWAJU11.ToString(),
                                        sLTSEQ12.ToString(),
                                        sLTHWAJU12.ToString(),
                                        sLTSEQ13.ToString(),
                                        sLTHWAJU13.ToString(),
                                        sLTSEQ14.ToString(),
                                        sLTHWAJU14.ToString(),
                                        sLTSEQ15.ToString(),
                                        sLTHWAJU15.ToString(),
                                        sLTSEQ16.ToString(),
                                        sLTHWAJU16.ToString(),
                                        sLTSEQ17.ToString(),
                                        sLTHWAJU17.ToString(),
                                        sLTSEQ18.ToString(),
                                        sLTHWAJU18.ToString(),
                                        sLTSEQ19.ToString(),
                                        sLTHWAJU19.ToString(),
                                        sLTSEQ20.ToString(),
                                        sLTHWAJU20.ToString(),
                                        sLTSEQ21.ToString(),
                                        sLTHWAJU21.ToString(),
                                        sLTSEQ22.ToString(),
                                        sLTHWAJU22.ToString(),
                                        sLTSEQ23.ToString(),
                                        sLTHWAJU23.ToString(),
                                        sLTSEQ24.ToString(),
                                        sLTHWAJU24.ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper().Trim()
                                        );

                this.DbConnector.ExecuteNonQuery();

                this.fsLTHANGCHA = this.CBH01_LTHANGCHA.GetValue().ToString().Trim();
                this.fsLTGOKJONG = this.CBH01_LTGOKJONG.GetValue().ToString().Trim();
            }
            else
            {
                // LAYTIME STATEMENT 수정
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92BDJ710",
                                        this.TXT01_LTCONTNO.GetValue().ToString(),
                                        this.CBO01_LTGUBUN.GetValue().ToString(),
                                        this.TXT01_LTBLQTY.GetValue().ToString(),
                                        Get_Date(this.DTP01_LTTENDATE.GetValue().ToString()),
                                        sLTTENTIME.ToString(),
                                        Get_Date(this.DTP01_LTACCDATE.GetValue().ToString()),
                                        sLTACCTIME.ToString(),
                                        Get_Date(this.DTP01_LTSTDATE.GetValue().ToString()),
                                        sLTSTTIME.ToString(),
                                        Get_Numeric(this.TXT01_LTQTY.GetValue().ToString()),
                                        sLTALLOW.ToString(),
                                        sLTEXCEP.ToString(),
                                        sLTUSED.ToString(),
                                        sLTSAVE.ToString(),
                                        Get_Numeric(this.TXT01_LTDAYSAVE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_LTTOTSAVE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_LTTOTAMT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_LTTYCSAVE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_LTTYCNHSAVE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_LTTYCBSSAVE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_LTHJSAVE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_LTHJNHSAVE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_LTHJBSSAVE.GetValue().ToString()),
                                        sLTSEQ01.ToString(),
                                        sLTHWAJU01.ToString(),
                                        sLTSEQ02.ToString(),
                                        sLTHWAJU02.ToString(),
                                        sLTSEQ03.ToString(),
                                        sLTHWAJU03.ToString(),
                                        sLTSEQ04.ToString(),
                                        sLTHWAJU04.ToString(),
                                        sLTSEQ05.ToString(),
                                        sLTHWAJU05.ToString(),
                                        sLTSEQ06.ToString(),
                                        sLTHWAJU06.ToString(),
                                        sLTSEQ07.ToString(),
                                        sLTHWAJU07.ToString(),
                                        sLTSEQ08.ToString(),
                                        sLTHWAJU08.ToString(),
                                        sLTSEQ09.ToString(),
                                        sLTHWAJU09.ToString(),
                                        sLTSEQ10.ToString(),
                                        sLTHWAJU10.ToString(),
                                        sLTSEQ11.ToString(),
                                        sLTHWAJU11.ToString(),
                                        sLTSEQ12.ToString(),
                                        sLTHWAJU12.ToString(),
                                        sLTSEQ13.ToString(),
                                        sLTHWAJU13.ToString(),
                                        sLTSEQ14.ToString(),
                                        sLTHWAJU14.ToString(),
                                        sLTSEQ15.ToString(),
                                        sLTHWAJU15.ToString(),
                                        sLTSEQ16.ToString(),
                                        sLTHWAJU16.ToString(),
                                        sLTSEQ17.ToString(),
                                        sLTHWAJU17.ToString(),
                                        sLTSEQ18.ToString(),
                                        sLTHWAJU18.ToString(),
                                        sLTSEQ19.ToString(),
                                        sLTHWAJU19.ToString(),
                                        sLTSEQ20.ToString(),
                                        sLTHWAJU20.ToString(),
                                        sLTSEQ21.ToString(),
                                        sLTHWAJU21.ToString(),
                                        sLTSEQ22.ToString(),
                                        sLTHWAJU22.ToString(),
                                        sLTSEQ23.ToString(),
                                        sLTHWAJU23.ToString(),
                                        sLTSEQ24.ToString(),
                                        sLTHWAJU24.ToString(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper().Trim(),
                                        this.CBH01_LTHANGCHA.GetValue().ToString().Trim(),
                                        this.CBH01_LTGOKJONG.GetValue().ToString().Trim()
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            // LAYTIME STATEMENT 팝업 조회
            UP_SEL_USIMCLTF(this.CBH01_LTHANGCHA.GetValue().ToString().Trim(),
                            this.CBH01_LTGOKJONG.GetValue().ToString().Trim());


            this.BTN61_GET.Visible = false;

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 입항 내역 가져오기 버튼
        private void BTN61_GET_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_928FX695", this.CBH01_LTHANGCHA.GetValue().ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                this.SetFocus(this.DTP01_LTTENDATE);
            }
        }
        #endregion

        #region Description : 계약번호 버튼
        private void BTN61_CONTNO_Click(object sender, EventArgs e)
        {
            TYUSGB001S popup = new TYUSGB001S();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.TXT01_LTCONTNO.SetValue(popup.fsCONTNO); // 계약번호

                // 시간 및 금액 계산(조출료, 체선료 비율)
                UP_Compute(popup.fsCNDISBIYUL, popup.fsCNDEMBIYUL, "AUTO");

                this.TXT01_LTHJSAVE.Focus();
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_US_928EJ693.GetDataSourceInclude(TSpread.TActionType.New,    "LTSEQ", "LTHWAJU"));

            // 조출료, 체선료 비율 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_8BJHK186", this.TXT01_LTCONTNO.GetValue().ToString().Substring(0, 4), Set_Fill2(this.TXT01_LTCONTNO.GetValue().ToString().Substring(4, 2)));
            
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 시간 및 금액 계산(조출료, 체선료 비율)
                UP_Compute(dt.Rows[0]["CNDISBIYUL"].ToString(), dt.Rows[0]["CNDEMBIYUL"].ToString(), "");
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            // 스프레드 칼럼 데이터 넘겨주는 부분
            e.ArgData = ds;
        }
        #endregion

        #region Description : LAYTIME STATEMENT 팝업 조회
        private void UP_SEL_USIMCLTF(string sLTHANGCHA, string sLTGOKJONG)
        {
            this.FPS91_TY_S_US_928EJ693.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_928EH692", this.CBH01_LTHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_LTGOKJONG.GetValue().ToString().Trim());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_928EJ693.SetValue(dt);
        }
        #endregion

        #region Description : 시간 및 금액 계산(조출료, 체선료 비율)
        private void UP_Compute(string sCNDISBIYUL, string sCNDEMBIYUL, string sWKGUBUN)
        {
            string sGUBUN = string.Empty;
            string sIHSOSOK = string.Empty;

            // 제외 시간
            double dLDNEXDD = 0;
            double dLDNEXHH = 0;
            double dLDNEXMM = 0;

            // 사용시간
            double dLDTOTDD = 0;
            double dLDTOTHH = 0;
            double dLDTOTMM = 0;

            int i = 0;

            DataTable dt = new DataTable();

            // EXCEPT 허용시간 계산
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_928GZ698", this.CBH01_LTHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_LTGOKJONG.GetValue().ToString().Trim());

            dt = this.DbConnector.ExecuteDataTable();

            for (i = 0; i < dt.Rows.Count; i++)
            {
                dLDTOTHH = dLDTOTHH + double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["LDTOTHH"].ToString()).Trim()));
                dLDTOTMM = dLDTOTMM + double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["LDTOTMM"].ToString()).Trim()));
                sIHSOSOK = SetDefaultValue(dt.Rows[i]["IHSOSOK"].ToString()).Trim();
            }


            // EXCEPT 환산시간 계산
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_928H1700", this.CBH01_LTHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_LTGOKJONG.GetValue().ToString().Trim());

            dt = this.DbConnector.ExecuteDataTable();

            for (i = 0; i < dt.Rows.Count; i++)
            {
                dLDNEXHH = dLDNEXHH + double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["LDNEXHH"].ToString()).Trim()));
                dLDNEXMM = dLDNEXMM + double.Parse(Get_Numeric(SetDefaultValue(dt.Rows[i]["LDNEXMM"].ToString()).Trim()));
            }


            // 제외 기간
            for (i = 1; i < 32; i++)
            {
                if (dLDNEXHH < i * 24)
                {
                    if (sGUBUN == "")
                    {
                        // 제외기간 일수
                        dLDNEXDD = i - 1;
                        // 제외기간 시간
                        dLDNEXHH = dLDNEXHH - ((i - 1) * 24);
                        sGUBUN = "*";
                    }
                }
            }

            sGUBUN = "";

            for (i = 1; i < 24; i++)
            {
                if (dLDNEXMM < i * 60)
                {
                    if (sGUBUN == "")
                    {
                        // 제외기간 시간
                        dLDNEXHH = dLDNEXHH + i - 1;
                        // 제외기간 분
                        dLDNEXMM = dLDNEXMM - ((i - 1) * 60);
                        sGUBUN = "*";
                    }
                }
            }

            sGUBUN = "";

            // 사용 기간
            for (i = 1; i < 32; i++)
            {
                if (dLDTOTHH < i * 24)
                {
                    if (sGUBUN == "")
                    {
                        // 사용기간 일수
                        dLDTOTDD = i - 1;
                        // 사용기간 시간
                        dLDTOTHH = dLDTOTHH - ((i - 1) * 24);
                        sGUBUN = "*";
                    }
                }
            }

            sGUBUN = "";

            for (i = 1; i < 24; i++)
            {
                if (dLDTOTMM < i * 60)
                {
                    if (sGUBUN == "")
                    {
                        // 사용기간 시간
                        dLDTOTHH = dLDTOTHH + i - 1;
                        // 사용기간 분
                        dLDTOTMM = dLDTOTMM - ((i - 1) * 60);
                        sGUBUN = "*";
                    }
                }
            }

            double dTEMPQTY = 0;
            double dCALLOWDD = 0;
            double dCALLOWHH = 0;
            double dCALLOWMM = 0;
            double dHOYONGMM = 0;
            double dSAYONGMM = 0;
            string sCALLOWMM = string.Empty;

            // 허용기간
            dTEMPQTY = double.Parse(Get_Numeric(TXT01_LTBLQTY.GetValue().ToString().Trim())) / double.Parse(Get_Numeric(this.TXT01_LTQTY.GetValue().ToString()));
            dCALLOWDD = double.Parse(UP_DotDelete(Convert.ToString(dTEMPQTY)));
            dTEMPQTY = (dTEMPQTY - dCALLOWDD) * 24;
            dCALLOWHH = double.Parse(UP_DotDelete(Convert.ToString(dTEMPQTY)));
            sCALLOWMM = ((dTEMPQTY - dCALLOWHH) * 60).ToString("0");
            dCALLOWMM = double.Parse(sCALLOWMM);

            this.TXT01_LTALLOW.SetValue(Set_Fill2(Convert.ToString(dCALLOWDD)) + "/" + Set_Fill2(Convert.ToString(dCALLOWHH)) + ":" + Set_Fill2(sCALLOWMM));
            // 제외기간 
            this.TXT01_LTEXCEP.SetValue(Set_Fill2(Convert.ToString(dLDNEXDD)) + "/" + Set_Fill2(Convert.ToString(dLDNEXHH)) + ":" + Set_Fill2(Convert.ToString(dLDNEXMM)));
            // 사용기간 
            this.TXT01_LTUSED.SetValue(Set_Fill2(Convert.ToString(dLDTOTDD)) + "/" + Set_Fill2(Convert.ToString(dLDTOTHH)) + ":" + Set_Fill2(Convert.ToString(dLDTOTMM)));

            // 조체구분 
            //허용시간
            dHOYONGMM = (dCALLOWDD * 24 * 60) + (dCALLOWHH * 60) + Double.Parse(sCALLOWMM);
            //사용시간
            dSAYONGMM = (dLDTOTDD * 24 * 60) + (dLDTOTHH * 60) + dLDTOTMM;

            if (dHOYONGMM < dSAYONGMM)
            {
                // 체선료pnlLTGUBUN
                this.CBO01_LTGUBUN.SetValue("2");
            }
            else
            {
                // 조출료
                this.CBO01_LTGUBUN.SetValue("1");
            }

            // 조체기간
            double dSAVEDD = 0;
            double dSAVEHH = 0;
            double dSAVEMM = 0;
            // 조체구분이 1인경우 
            // 조체기간 = 허용기간 - 사용기간
            // 조체구분이 2인경우
            // 조체기간 = 사용기간 - 허용기간
            if (this.CBO01_LTGUBUN.GetValue().ToString() == "1")
            {
                if (dCALLOWHH - dLDTOTHH < 0)
                {
                    dCALLOWDD = dCALLOWDD - 1;
                    dCALLOWHH = dCALLOWHH + 24;
                }

                if (dCALLOWMM - dLDTOTMM < 0)
                {
                    dCALLOWHH = dCALLOWHH - 1;
                    dCALLOWMM = dCALLOWMM + 60;
                }

                dSAVEDD = dCALLOWDD - dLDTOTDD;
                dSAVEHH = dCALLOWHH - dLDTOTHH;

                if (dSAVEHH < 0)
                {
                    dSAVEDD = dSAVEDD - 1;
                    dSAVEHH = dSAVEHH + 24;
                }

                dSAVEMM = dCALLOWMM - dLDTOTMM;
            }
            else
            {
                if (dLDTOTHH - dCALLOWHH < 0)
                {
                    dLDTOTDD = dLDTOTDD - 1;
                    dLDTOTHH = dLDTOTHH + 24;
                }
                if (dLDTOTMM - dCALLOWMM < 0)
                {
                    dLDTOTHH = dLDTOTHH - 1;
                    dLDTOTMM = dLDTOTMM + 60;
                }
                dSAVEDD = dLDTOTDD - dCALLOWDD;
                dSAVEHH = dLDTOTHH - dCALLOWHH;
                dSAVEMM = dLDTOTMM - dCALLOWMM;
            }

            // 조체기간
            this.TXT01_LTSAVE.SetValue(Set_Fill2(Convert.ToString(dSAVEDD)) + "/" + Set_Fill2(Convert.ToString(dSAVEHH)) + ":" + Set_Fill2(Convert.ToString(dSAVEMM)));

            // 조체출액 = (조체기간(일자 + (시간/24) + (분/60/24))) * 일조출액
            // 소숫점 6자리에서 반올림 = (조체기간(일자 + (시간/24) + (분/60/24)))
            // 소숫점 3자리에서 반올림 = 조체출액

            string sSAVEDAY = string.Empty;

            double dLTTOTSAVE = 0;


            sSAVEDAY = (dSAVEDD + (dSAVEHH / 24) + (dSAVEMM / 60 / 24)).ToString("0.00000");
            this.TXT01_LTTOTSAVE.SetValue((double.Parse(sSAVEDAY) * double.Parse(this.TXT01_LTDAYSAVE.GetValue().ToString().Trim())).ToString("0.00"));
            dLTTOTSAVE = double.Parse(this.TXT01_LTTOTSAVE.GetValue().ToString());


            if (sWKGUBUN == "AUTO")
            {
                if (int.Parse(this.CBH01_LTHANGCHA.GetValue().ToString()) >= 201901)
                {
                    double dBIYUL = 0;

                    // 농협일 경우
                    if (sIHSOSOK.ToString() == "2")
                    {
                        double dBEBUNAMT = 0;
                        double dBEBUNTOT = 0;

                        double dOKYUNG_TOT_AMT = 0;
                        double dNH_TOT_AMT = 0;

                        double dOKYUNG_AMT1 = 0;
                        double dNH_AMT1 = 0;

                        double dOKYUNG_AMT2 = 0;
                        double dNH_AMT2 = 0;

                        string sHalf_LTTOTSAVE = string.Empty;

                        // 오경이 있는 경우 체크(거래처관리의 소속이 개별화주일 경우)
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_US_92BBD706", this.CBH01_LTHANGCHA.GetValue().ToString(),
                                                                    this.CBH01_LTGOKJONG.GetValue().ToString()
                                                                    );

                        dt = this.DbConnector.ExecuteDataTable();

                        if (dt.Rows.Count > 0) // 오경(개별화주 소속이 있을 경우)
                        {
                            // LAYTIME STATEMENT(개별화주가 있을 경우 배정량 가져오기)
                            this.DbConnector.CommandClear();
                            this.DbConnector.Attach("TY_P_US_92BBO707", this.CBH01_LTHANGCHA.GetValue().ToString(),
                                                                        this.CBH01_LTGOKJONG.GetValue().ToString(),
                                                                        this.CBH01_LTHANGCHA.GetValue().ToString(),
                                                                        this.CBH01_LTGOKJONG.GetValue().ToString()
                                                                        );

                            dt = this.DbConnector.ExecuteDataTable();

                            if (dt.Rows.Count > 0)
                            {
                                dBEBUNAMT = dLTTOTSAVE * double.Parse(dt.Rows[0]["JGBEJNQTY"].ToString()) / double.Parse(dt.Rows[0]["SUM_JGBEJNQTY"].ToString());

                                dBEBUNAMT = dBEBUNAMT * 1000;
                                dBEBUNAMT = dBEBUNAMT - (dBEBUNAMT % 1);
                                dBEBUNAMT = dBEBUNAMT / 1000;
                                dBEBUNTOT = dBEBUNTOT + dBEBUNAMT;

                                // 농협총액
                                dNH_TOT_AMT = double.Parse(String.Format("{0,9:N2}", dBEBUNAMT));

                                // 오경총액 = 조출총액 - 농협총액
                                dOKYUNG_TOT_AMT = double.Parse(String.Format("{0,9:N2}", dLTTOTSAVE - dNH_TOT_AMT));


                                #region Description : 하역사 배분액

                                // 하역사 배분액 - 농협
                                dNH_AMT2 = dNH_TOT_AMT / 2;
                                dNH_AMT2 = double.Parse(UP_DotDelete(Convert.ToString(dNH_AMT2 * 100)));
                                dNH_AMT2 = dNH_AMT2 / 100;

                                this.TXT01_LTTYCNHSAVE.SetValue(Convert.ToString(dNH_AMT2));

                                // 하역사 배분액 - 오경
                                dOKYUNG_AMT2 = dOKYUNG_TOT_AMT / 2;
                                dOKYUNG_AMT2 = double.Parse(UP_DotDelete(Convert.ToString(dOKYUNG_AMT2 * 100)));
                                dOKYUNG_AMT2 = dOKYUNG_AMT2 / 100;

                                this.TXT01_LTTYCBSSAVE.SetValue(Convert.ToString(dOKYUNG_AMT2));

                                // 하역사 배분액 - 총액
                                this.TXT01_LTTYCSAVE.SetValue(Convert.ToString(dNH_AMT2 + dOKYUNG_AMT2));

                                #endregion


                                #region Description : 화주 배분액

                                // 화주 배분액 - 농협
                                dNH_AMT1 = double.Parse(String.Format("{0,9:N2}", dNH_TOT_AMT - dNH_AMT2));

                                // 화주 배분액 - 농협
                                this.TXT01_LTHJNHSAVE.SetValue(Convert.ToString(dNH_AMT1));


                                // 화주 배분액 - 오경
                                dOKYUNG_AMT1 = double.Parse(String.Format("{0,9:N2}", dOKYUNG_TOT_AMT - dOKYUNG_AMT2));

                                // 화주 배분액 - 오경
                                this.TXT01_LTHJBSSAVE.SetValue(Convert.ToString(dOKYUNG_AMT1));

                                // 화주 배분액 - 총액
                                this.TXT01_LTHJSAVE.SetValue(Convert.ToString(dNH_AMT1 + dOKYUNG_AMT1));

                                #endregion
                            }
                        }
                        else
                        {
                            if (this.CBO01_LTGUBUN.GetValue().ToString() == "1") // 조출료
                            {
                                dBIYUL = (100 - double.Parse(sCNDISBIYUL.ToString())) / 100;
                            }
                            else // 체선료
                            {
                                dBIYUL = (100 - double.Parse(sCNDEMBIYUL.ToString())) / 100;
                            }

                            // 화주분
                            this.TXT01_LTHJSAVE.SetValue((dLTTOTSAVE * dBIYUL).ToString("0.00"));
                        }
                    }
                    else
                    {
                        if (this.CBO01_LTGUBUN.GetValue().ToString() == "1") // 조출료
                        {
                            dBIYUL = (100 - double.Parse(sCNDISBIYUL.ToString())) / 100;
                        }
                        else // 체선료
                        {
                            dBIYUL = (100 - double.Parse(sCNDEMBIYUL.ToString())) / 100;
                        }

                        // 화주분
                        this.TXT01_LTHJSAVE.SetValue((dLTTOTSAVE * dBIYUL).ToString("0.00"));
                    }
                }
                else
                {
                    // 화주분
                    if ((sIHSOSOK.ToString() == "1" || sIHSOSOK.ToString() == "C") && Int32.Parse(Get_Date(this.DTP01_IHIPHANG.GetValue().ToString())) > 20030831)
                    {
                        if (this.CBO01_LTGUBUN.GetValue().ToString() == "1")
                        {
                            if (this.CBH01_LTHANGCHA.GetValue().ToString() == "199634")
                            {
                                this.TXT01_LTHJSAVE.SetValue((dLTTOTSAVE * 0.8).ToString("0.00"));
                            }
                            else
                            {
                                this.TXT01_LTHJSAVE.SetValue((dLTTOTSAVE * 0.5).ToString("0.00"));
                            }
                        }
                        else
                        {
                            if (this.CBH01_LTHANGCHA.GetValue().ToString() == "199634")
                            {
                                this.TXT01_LTHJSAVE.SetValue((dLTTOTSAVE * 0.8).ToString("0.00"));
                            }
                            else
                            {
                                this.TXT01_LTHJSAVE.SetValue((dLTTOTSAVE * 0.7).ToString("0.00")); //2007.11.23 pjs 0.8==> 0.7 수정0.7==> 0.8
                            }
                        }
                    }
                    else
                    {
                        /* 200901항차부터 협회가 2(농협중앙회)일 경우
                         * 체선료 = 화주분(70%) 하역회사분(30%)
                         * 조출료 = 화주분(50%) 하역회사분(50%)
                         */
                        if (this.CBH01_LTHANGCHA.GetValue().ToString() == "199634")
                        {
                            this.TXT01_LTHJSAVE.SetValue((dLTTOTSAVE * 0.8).ToString("0.00"));
                        }
                        else
                        {
                            if (double.Parse(this.CBH01_LTHANGCHA.GetValue().ToString()) >= 200901)
                            {
                                // 조출료
                                if (this.CBO01_LTGUBUN.GetValue().ToString() == "1")
                                {
                                    // 화주분
                                    this.TXT01_LTHJSAVE.SetValue((dLTTOTSAVE * 0.5).ToString("0.00"));
                                }
                                else // 체선료
                                {
                                    // 화주분
                                    this.TXT01_LTHJSAVE.SetValue((dLTTOTSAVE * 0.7).ToString("0.00"));
                                }
                            }
                            else
                            {
                                this.TXT01_LTHJSAVE.SetValue((dLTTOTSAVE * 0.6).ToString("0.00"));
                            }
                        }
                    }
                }

                // 하역회사분
                this.TXT01_LTTYCSAVE.SetValue(Convert.ToString(dLTTOTSAVE - double.Parse(Get_Numeric(this.TXT01_LTHJSAVE.GetValue().ToString()))));
            }


            this.FPS91_TY_S_US_928EJ693.Initialize();

            // LAYTIME STATEMENT 하주 및 하역사분 구하기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_92B9B701", this.TXT01_LTHJSAVE.GetValue().ToString(),
                                                        this.TXT01_LTTYCSAVE.GetValue().ToString(),
                                                        this.CBH01_LTHANGCHA.GetValue().ToString(),
                                                        this.CBH01_LTGOKJONG.GetValue().ToString()
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_928EJ693.SetValue(dt);

            for (i = 0; i < dt.Rows.Count; i++)
            {
                this.FPS91_TY_S_US_928EJ693.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";
            }

        }
        #endregion

        #region Description : 필드 클리어
        private void UP_FieldClear()
        {
            this.FPS91_TY_S_US_928EJ693.Initialize();

            this.DTP01_IHIPHANG.SetValue("");
            this.TXT01_IHIPHH.SetValue("");
            this.TXT01_IHIPMM.SetValue("");

            this.DTP01_IHJUBDAT.SetValue("");
            this.TXT01_IHJUBHH.SetValue("");
            this.TXT01_IHJUBMM.SetValue("");

            this.DTP01_IHJAKSTDAT.SetValue("");
            this.TXT01_IHJAKSTHH.SetValue("");
            this.TXT01_IHJAKSTMM.SetValue("");

            this.DTP01_IHJAKENDAT.SetValue("");
            this.TXT01_IHJAKENHH.SetValue("");
            this.TXT01_IHJAKENMM.SetValue("");

            this.TXT01_LTBLQTY.SetValue("");
            this.TXT01_LTALLOW.SetValue("");
            this.TXT01_LTEXCEP.SetValue("");

            this.TXT01_LTUSED.SetValue("");
            this.TXT01_LTSAVE.SetValue("");
            this.CBO01_LTGUBUN.SetValue("");
            this.TXT01_LTTOTSAVE.SetValue("");
            this.TXT01_LTHJSAVE.SetValue("");
            this.TXT01_LTHJNHSAVE.SetValue("");
            this.TXT01_LTHJBSSAVE.SetValue("");

            this.TXT01_LTTYCSAVE.SetValue("");
            this.TXT01_LTTYCNHSAVE.SetValue("");
            this.TXT01_LTTYCBSSAVE.SetValue("");

            this.DTP01_LTTENDATE.SetValue("");
            this.TXT01_LTTENHH.SetValue("");
            this.TXT01_LTTENMM.SetValue("");

            this.DTP01_LTACCDATE.SetValue("");
            this.TXT01_LTACCHH.SetValue("");
            this.TXT01_LTACCMM.SetValue("");

            this.DTP01_LTSTDATE.SetValue("");
            this.TXT01_LTSTHH.SetValue("");
            this.TXT01_LTSTMM.SetValue("");

            this.TXT01_LTQTY.SetValue("");
            this.TXT01_LTDAYSAVE.SetValue("");
            this.TXT01_LTTOTAMT.SetValue("");
        }
        #endregion

        #region Description : 계약번호 이벤트
        private void TXT01_LTCONTNO_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.F1)
            {
                TYUSGB001S popup = new TYUSGB001S();

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.TXT01_LTCONTNO.SetValue(popup.fsCONTNO); // 계약번호

                    // 시간 및 금액 계산(조출료, 체선료 비율)
                    UP_Compute(popup.fsCNDISBIYUL, popup.fsCNDEMBIYUL, "AUTO");

                    this.TXT01_LTHJSAVE.Focus();
                }
            }
        }
        #endregion
    }
}