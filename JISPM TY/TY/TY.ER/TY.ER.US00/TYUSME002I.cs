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
    public partial class TYUSME002I : TYBase
    {
        private string fsLDHANGCHA;
        private string fsLDGOKJONG;
        private string fsLDDATE;

        #region Description : 페이지 로드
        public TYUSME002I(string sLDHANGCHA, string sLDGOKJONG, string sLDDATE)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기 
            this.fsLDHANGCHA  = sLDHANGCHA;
            this.fsLDGOKJONG  = sLDGOKJONG;
            this.fsLDDATE     = sLDDATE;
        }

        private void TYUSME002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.FPS91_TY_S_US_91VB4650.Initialize();

            if (string.IsNullOrEmpty(this.fsLDHANGCHA) && string.IsNullOrEmpty(this.fsLDGOKJONG) && string.IsNullOrEmpty(this.fsLDDATE))
            {
                this.CBH01_LDHANGCHA.SetReadOnly(false);
                this.CBH01_LDGOKJONG.SetReadOnly(false);
                this.DTP01_LDDATE.SetReadOnly(false);

                SetStartingFocus(this.CBH01_LDHANGCHA.CodeText);
            }
            else
            {
                this.CBH01_LDHANGCHA.SetReadOnly(true);
                this.CBH01_LDGOKJONG.SetReadOnly(true);
                this.DTP01_LDDATE.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_91VB4651", this.fsLDHANGCHA, this.fsLDGOKJONG, this.fsLDDATE);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.CurrentDataTableRowMapping(dt, "01");
                }

                // LAYTIME EXCEPT 조회
                UP_SEL_USIMCLDNF(this.fsLDHANGCHA, this.fsLDGOKJONG, this.fsLDDATE);

                SetStartingFocus(this.CBH01_LDWEEK.CodeText);
            }
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            this.fsLDHANGCHA = "";
            this.fsLDGOKJONG = "";
            this.fsLDDATE    = "";

            //this.FPS91_TY_S_US_91VB4650.Initialize();
            //this.CBH01_LDHANGCHA.SetValue("");
            //this.CBH01_LDGOKJONG.SetValue("");
            //this.DTP01_LDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            UP_FieldClear();

            this.CBH01_LDHANGCHA.SetReadOnly(false);
            this.CBH01_LDGOKJONG.SetReadOnly(false);
            this.DTP01_LDDATE.SetReadOnly(false);

            this.SetFocus(this.CBH01_LDHANGCHA.CodeText);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            string sLDNEXSTTM = string.Empty;
            string sLDNEXEDTM = string.Empty;
            string sLDNEXCOTM = string.Empty;

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++) // 등록
                {
                    DataTable dt = new DataTable();

                    // 매출발생월 다음달 미수금액이 존재하는지 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_91VDZ660", this.CBH01_LDHANGCHA.GetValue().ToString().Trim(),
                                                                this.CBH01_LDGOKJONG.GetValue().ToString().Trim(),
                                                                Get_Date(this.DTP01_LDDATE.GetValue().ToString().Trim())
                                                                );
                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        ds.Tables[0].Rows[i]["LDNSEQ"] = dt.Rows[0]["LDNSEQ"].ToString();
                    }

                    sLDNEXSTTM = "0";
                    sLDNEXEDTM = "0";
                    sLDNEXCOTM = "0";

                    sLDNEXSTTM = Set_Fill2(ds.Tables[0].Rows[i]["LDNEXSTHH"].ToString().Trim()) + Set_Fill2(ds.Tables[0].Rows[i]["LDNEXSTMM"].ToString().Trim());
                    sLDNEXEDTM = Set_Fill2(ds.Tables[0].Rows[i]["LDNEXEDHH"].ToString().Trim()) + Set_Fill2(ds.Tables[0].Rows[i]["LDNEXEDMM"].ToString().Trim());
                    sLDNEXCOTM = Set_Fill2(ds.Tables[0].Rows[i]["LDNEXCOHH"].ToString().Trim()) + Set_Fill2(ds.Tables[0].Rows[i]["LDNEXCOMM"].ToString().Trim());

                    // 내역 등록
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_US_91VDW659", this.CBH01_LDHANGCHA.GetValue().ToString().Trim(),
                                                                this.CBH01_LDGOKJONG.GetValue().ToString().Trim(),
                                                                Get_Date(this.DTP01_LDDATE.GetValue().ToString().Trim()),
                                                                ds.Tables[0].Rows[i]["LDNSEQ"].ToString().Trim(),
                                                                sLDNEXSTTM.ToString(),
                                                                sLDNEXEDTM.ToString(),
                                                                sLDNEXCOTM.ToString(),
                                                                ds.Tables[0].Rows[i]["LDNEXSAYU"].ToString().Trim(),
                                                                TYUserInfo.EmpNo
                                                                );

                    this.DbConnector.ExecuteTranQueryList();
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                for (i = 0; i < ds.Tables[1].Rows.Count; i++) // 수정
                {
                    sLDNEXSTTM = "0";
                    sLDNEXEDTM = "0";
                    sLDNEXCOTM = "0";

                    sLDNEXSTTM = Set_Fill2(ds.Tables[1].Rows[i]["LDNEXSTHH"].ToString().Trim()) + Set_Fill2(ds.Tables[1].Rows[i]["LDNEXSTMM"].ToString().Trim());
                    sLDNEXEDTM = Set_Fill2(ds.Tables[1].Rows[i]["LDNEXEDHH"].ToString().Trim()) + Set_Fill2(ds.Tables[1].Rows[i]["LDNEXEDMM"].ToString().Trim());
                    sLDNEXCOTM = Set_Fill2(ds.Tables[1].Rows[i]["LDNEXCOHH"].ToString().Trim()) + Set_Fill2(ds.Tables[1].Rows[i]["LDNEXCOMM"].ToString().Trim());

                    // 내역 수정
                    this.DbConnector.Attach("TY_P_US_91VDS658", sLDNEXSTTM.ToString(),
                                                                sLDNEXEDTM.ToString(),
                                                                sLDNEXCOTM.ToString(),
                                                                ds.Tables[1].Rows[i]["LDNEXSAYU"].ToString().Trim(),
                                                                TYUserInfo.EmpNo,
                                                                this.CBH01_LDHANGCHA.GetValue().ToString().Trim(),
                                                                this.CBH01_LDGOKJONG.GetValue().ToString().Trim(),
                                                                Get_Date(this.DTP01_LDDATE.GetValue().ToString().Trim()),
                                                                ds.Tables[1].Rows[i]["LDNSEQ"].ToString().Trim()
                                                                );
                }
                
                this.DbConnector.ExecuteTranQueryList();
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                for (i = 0; i < ds.Tables[2].Rows.Count; i++) // 삭제
                {
                    // 내역 삭제
                    this.DbConnector.Attach("TY_P_US_91VDR657", this.CBH01_LDHANGCHA.GetValue().ToString().Trim(),
                                                                this.CBH01_LDGOKJONG.GetValue().ToString().Trim(),
                                                                Get_Date(this.DTP01_LDDATE.GetValue().ToString().Trim()),
                                                                ds.Tables[2].Rows[i]["LDNSEQ"].ToString().Trim()
                                                                );
                }

                this.DbConnector.ExecuteTranQueryList();
            }

            UP_AllowTime_ReCompute();

            string sLDSTTIME  = string.Empty;
            string sLDEDTIME  = string.Empty;
            string sLDTOTTIME = string.Empty;


            sLDSTTIME  = Set_Fill2(this.TXT01_LDSTTIMEHH.GetValue().ToString().Trim()) + Set_Fill2(this.TXT01_LDSTTIMEMM.GetValue().ToString().Trim());
            sLDEDTIME  = Set_Fill2(this.TXT01_LDEDTIMEHH.GetValue().ToString().Trim()) + Set_Fill2(this.TXT01_LDEDTIMEMM.GetValue().ToString().Trim());
            sLDTOTTIME = Set_Fill2(this.TXT01_LDTOTTIMEHH.GetValue().ToString().Trim()) + Set_Fill2(this.TXT01_LDTOTTIMEMM.GetValue().ToString().Trim());

            // LAYTIME EXCEPT 마스터 등록
            this.DbConnector.CommandClear();
            if (string.IsNullOrEmpty(this.fsLDHANGCHA) && string.IsNullOrEmpty(this.fsLDGOKJONG) && string.IsNullOrEmpty(this.fsLDDATE))
            {
                // 등록
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_91VBS655",
                                        this.CBH01_LDHANGCHA.GetValue().ToString().Trim(),
                                        this.CBH01_LDGOKJONG.GetValue().ToString().Trim(),
                                        Get_Date(this.DTP01_LDDATE.GetValue().ToString().Trim()),
                                        this.CBH01_LDWEEK.GetValue().ToString(),
                                        this.TXT01_LDDAY.GetValue().ToString().Trim(),
                                        sLDSTTIME.ToString(),
                                        sLDEDTIME.ToString(),
                                        sLDTOTTIME.ToString(),
                                        this.TXT01_LDSAYU.GetValue().ToString().Trim(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper().Trim()
                                        );

                this.DbConnector.ExecuteNonQuery();

                this.fsLDHANGCHA = this.CBH01_LDHANGCHA.GetValue().ToString().Trim();
                this.fsLDGOKJONG = this.CBH01_LDGOKJONG.GetValue().ToString().Trim();
                this.fsLDDATE = Get_Date(this.DTP01_LDDATE.GetValue().ToString().Trim());
            }
            else
            {
                // LAYTIME EXCEPT 마스터 수정
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_91VBT656",
                                        this.CBH01_LDWEEK.GetValue().ToString(),
                                        this.TXT01_LDDAY.GetValue().ToString().Trim(),
                                        sLDSTTIME.ToString(),
                                        sLDEDTIME.ToString(),
                                        sLDTOTTIME.ToString(),
                                        this.TXT01_LDSAYU.GetValue().ToString().Trim(),
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper().Trim(),
                                        this.CBH01_LDHANGCHA.GetValue().ToString().Trim(),
                                        this.CBH01_LDGOKJONG.GetValue().ToString().Trim(),
                                        Get_Date(this.DTP01_LDDATE.GetValue().ToString().Trim())
                                        );

                this.DbConnector.ExecuteNonQuery();
            }

            // LAYTIME EXCEPT 조회
            UP_SEL_USIMCLDNF(this.CBH01_LDHANGCHA.GetValue().ToString().Trim(),
                             this.CBH01_LDGOKJONG.GetValue().ToString().Trim(),
                             Get_Date(this.DTP01_LDDATE.GetValue().ToString().Trim()));

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
            string sTIME = string.Empty;

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_US_91VB4650.GetDataSourceInclude(TSpread.TActionType.New,    "LDNSEQ", "LDNEXSTHH", "LDNEXSTMM", "LDNEXEDHH", "LDNEXEDMM", "LDNEXCOHH", "LDNEXCOMM", "LDNEXSAYU"));
            ds.Tables.Add(this.FPS91_TY_S_US_91VB4650.GetDataSourceInclude(TSpread.TActionType.Update, "LDNSEQ", "LDNEXSTHH", "LDNEXSTMM", "LDNEXEDHH", "LDNEXEDMM", "LDNEXCOHH", "LDNEXCOMM", "LDNEXSAYU"));
            ds.Tables.Add(this.FPS91_TY_S_US_91VB4650.GetDataSourceInclude(TSpread.TActionType.Remove, "LDNSEQ"));

            // 신규등록시 마스타 존재여부 체크
            if (string.IsNullOrEmpty(this.fsLDHANGCHA) && string.IsNullOrEmpty(this.fsLDGOKJONG) && string.IsNullOrEmpty(this.fsLDDATE))
            {   
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_91VB4651", CBH01_LDHANGCHA.GetValue().ToString(), CBH01_LDGOKJONG.GetValue().ToString(), DTP01_LDDATE.GetString());

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_3219C986");
                    this.SetFocus(this.DTP01_LDDATE);

                    e.Successed = false;
                    return;
                }
            }


            // 시작시간(분)
            if (TXT01_LDSTTIMEMM.GetValue().ToString().Trim() != "")
            {
                if (Int64.Parse(Set_Fill2(TXT01_LDSTTIMEMM.GetValue().ToString().Trim())) > 59)
                {
                    this.ShowMessage("TY_M_US_91VBJ652");
                    this.SetFocus(this.TXT01_LDSTTIMEMM);

                    e.Successed = false;
                    return;
                }
            }
            // 종료시간(분)
            if (TXT01_LDEDTIMEMM.GetValue().ToString().Trim() != "")
            {
                if (Int64.Parse(Set_Fill2(TXT01_LDEDTIMEMM.GetValue().ToString().Trim())) > 59)
                {
                    this.ShowMessage("TY_M_US_91VBJ653");
                    this.SetFocus(this.TXT01_LDEDTIMEMM);

                    e.Successed = false;
                    return;
                }
            }

            int i = 0;

            // 내역 등록
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (Get_Numeric(ds.Tables[0].Rows[i]["LDNEXSTHH"].ToString().Trim()) != "00" || Get_Numeric(ds.Tables[0].Rows[i]["LDNEXSTMM"].ToString().Trim()) != "00" ||
                    Get_Numeric(ds.Tables[0].Rows[i]["LDNEXEDHH"].ToString().Trim()) != "00" || Get_Numeric(ds.Tables[0].Rows[i]["LDNEXEDMM"].ToString().Trim()) != "00")
                {
                    if (Convert.ToInt16(Get_Numeric(ds.Tables[0].Rows[i]["LDNEXSTMM"].ToString().Trim())) > 59)
                    {
                        this.ShowMessage("TY_M_US_91VBJ652");

                        e.Successed = false;
                        return;
                    }

                    if (Convert.ToInt16(Get_Numeric(ds.Tables[0].Rows[i]["LDNEXEDMM"].ToString().Trim())) > 59)
                    {
                        this.ShowMessage("TY_M_US_91VBJ653");

                        e.Successed = false;
                        return;
                    }

                    // 내역 제외시간 계산
                    sTIME = UP_Compute_AllowTime(Set_Fill2(Get_Numeric(ds.Tables[0].Rows[i]["LDNEXSTHH"].ToString().Trim())), Set_Fill2(Get_Numeric(ds.Tables[0].Rows[i]["LDNEXSTMM"].ToString().Trim())),
                                                 Set_Fill2(Get_Numeric(ds.Tables[0].Rows[i]["LDNEXEDHH"].ToString().Trim())), Set_Fill2(Get_Numeric(ds.Tables[0].Rows[i]["LDNEXEDMM"].ToString().Trim())));

                    ds.Tables[0].Rows[i]["LDNEXCOHH"] = sTIME.ToString().Substring(0, 2);
                    ds.Tables[0].Rows[i]["LDNEXCOMM"] = sTIME.ToString().Substring(2, 2);
                }
            }


            // 내역 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                if (Get_Numeric(ds.Tables[1].Rows[i]["LDNEXSTHH"].ToString().Trim()) != "00" || Get_Numeric(ds.Tables[1].Rows[i]["LDNEXSTMM"].ToString().Trim()) != "00" ||
                    Get_Numeric(ds.Tables[1].Rows[i]["LDNEXEDHH"].ToString().Trim()) != "00" || Get_Numeric(ds.Tables[1].Rows[i]["LDNEXEDMM"].ToString().Trim()) != "00")
                {
                    if (Convert.ToInt16(Get_Numeric(ds.Tables[1].Rows[i]["LDNEXSTMM"].ToString().Trim())) > 59)
                    {
                        this.ShowMessage("TY_M_US_91VBJ652");

                        e.Successed = false;
                        return;
                    }

                    if (Convert.ToInt16(Get_Numeric(ds.Tables[1].Rows[i]["LDNEXEDMM"].ToString().Trim())) > 59)
                    {
                        this.ShowMessage("TY_M_US_91VBJ653");

                        e.Successed = false;
                        return;
                    }

                    // 내역 제외시간 계산
                    sTIME = UP_Compute_AllowTime(Set_Fill2(Get_Numeric(ds.Tables[1].Rows[i]["LDNEXSTHH"].ToString().Trim())), Set_Fill2(Get_Numeric(ds.Tables[1].Rows[i]["LDNEXSTMM"].ToString().Trim())),
                                                 Set_Fill2(Get_Numeric(ds.Tables[1].Rows[i]["LDNEXEDHH"].ToString().Trim())), Set_Fill2(Get_Numeric(ds.Tables[1].Rows[i]["LDNEXEDMM"].ToString().Trim())));

                    ds.Tables[1].Rows[i]["LDNEXCOHH"] = sTIME.ToString().Substring(0, 2);
                    ds.Tables[1].Rows[i]["LDNEXCOMM"] = sTIME.ToString().Substring(2, 2);
                }
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

        #region Description : LAYTIME EXCEPT 조회
        private void UP_SEL_USIMCLDNF(string sLDNHANGCHA, string sLDNGOKJONG, string sLDNDATE)
        {
            this.FPS91_TY_S_US_91VB4650.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_91VB3649", sLDNHANGCHA.ToString(), sLDNGOKJONG.ToString(), sLDNDATE.ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_91VB4650.SetValue(dt);
        }
        #endregion

        #region Description : 시간 계산
        private string UP_Compute_AllowTime(string sSTHH, string sSTMM, string sEDHH, string sEDMM)
        {
            string sTIME     = string.Empty;
            string sLDSTTIME = string.Empty;
            string sLDEDTIME = string.Empty;

            sLDSTTIME = Set_Fill2(sSTHH) + "." + Set_Fill2(sSTMM);
            sLDEDTIME = Set_Fill2(sEDHH) + "." + Set_Fill2(sEDMM);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_91VBN654", sLDSTTIME.ToString(), sLDEDTIME.ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sTIME = dt.Rows[0]["EXCEPT_TIME"].ToString();
            }

            return sTIME;
        }
        #endregion

        #region Description : 허용환산시간 계산
        private void UP_AllowTime_ReCompute()
        {
            int i = 0;
            string sTIME = string.Empty;

            // 허용환산시간 계산
            sTIME = UP_Compute_AllowTime(Set_Fill2(this.TXT01_LDSTTIMEHH.GetValue().ToString()), Set_Fill2(this.TXT01_LDSTTIMEMM.GetValue().ToString()),
                                         Set_Fill2(this.TXT01_LDEDTIMEHH.GetValue().ToString()), Set_Fill2(this.TXT01_LDEDTIMEMM.GetValue().ToString()));

            this.TXT01_LDTOTTIMEHH.SetValue(sTIME.ToString().Substring(0, 2));
            this.TXT01_LDTOTTIMEMM.SetValue(sTIME.ToString().Substring(2, 2));

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_91VB3649", this.CBH01_LDHANGCHA.GetValue().ToString().Trim(),
                                                        this.CBH01_LDGOKJONG.GetValue().ToString().Trim(),
                                                        Get_Date(this.DTP01_LDDATE.GetValue().ToString().Trim())
                                                        );

            dt = this.DbConnector.ExecuteDataTable();

            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (Get_Numeric(dt.Rows[i]["LDNEXSTHH"].ToString().Trim()) != "00" || Get_Numeric(dt.Rows[i]["LDNEXSTMM"].ToString().Trim()) != "00" ||
                    Get_Numeric(dt.Rows[i]["LDNEXEDHH"].ToString().Trim()) != "00" || Get_Numeric(dt.Rows[i]["LDNEXEDMM"].ToString().Trim()) != "00")
                {
                    // 내역 제외시간 계산
                    sTIME = UP_Compute_AllowTime(Set_Fill2(Get_Numeric(dt.Rows[i]["LDNEXSTHH"].ToString().Trim())), Set_Fill2(Get_Numeric(dt.Rows[i]["LDNEXSTMM"].ToString().Trim())),
                                                 Set_Fill2(Get_Numeric(dt.Rows[i]["LDNEXEDHH"].ToString().Trim())), Set_Fill2(Get_Numeric(dt.Rows[i]["LDNEXEDMM"].ToString().Trim())));

                    dt.Rows[i]["LDNEXCOHH"] = sTIME.ToString().Substring(0, 2);
                    dt.Rows[i]["LDNEXCOMM"] = sTIME.ToString().Substring(2, 2);

                    if (Convert.ToInt16(Get_Numeric(this.TXT01_LDTOTTIMEMM.GetValue().ToString().Trim())) < Convert.ToInt16(Get_Numeric(dt.Rows[i]["LDNEXCOMM"].ToString())))
                    {
                        this.TXT01_LDTOTTIMEHH.SetValue(Set_Fill2(Convert.ToString(Convert.ToInt16(Get_Numeric(this.TXT01_LDTOTTIMEHH.GetValue().ToString().Trim())) - 1)));
                        this.TXT01_LDTOTTIMEMM.SetValue(Set_Fill2(Convert.ToString(60 + Convert.ToInt16(Get_Numeric(this.TXT01_LDTOTTIMEMM.GetValue().ToString().Trim())) - Convert.ToInt16(Get_Numeric(dt.Rows[i]["LDNEXCOMM"].ToString())))));
                    }
                    else
                    {
                        this.TXT01_LDTOTTIMEMM.SetValue(Set_Fill2(Convert.ToString(Convert.ToInt16(Get_Numeric(this.TXT01_LDTOTTIMEMM.GetValue().ToString().Trim())) - Convert.ToInt16(Get_Numeric(dt.Rows[i]["LDNEXCOMM"].ToString())))));
                    }

                    this.TXT01_LDTOTTIMEHH.SetValue(Set_Fill2(Convert.ToString(Convert.ToInt16(Get_Numeric(this.TXT01_LDTOTTIMEHH.GetValue().ToString().Trim())) - Convert.ToInt16(Get_Numeric(dt.Rows[i]["LDNEXCOHH"].ToString())))));
                }
            }
        }
        #endregion

        #region Description : 내역 허용시간 계산
        private void UP_Detail_Compute_AllowTime()
        {
            string sLDSTTIME = string.Empty;
            string sLDEDTIME = string.Empty;

            sLDSTTIME = Set_Fill2(this.TXT01_LDSTTIMEHH.GetValue().ToString()) + "." + Set_Fill2(this.TXT01_LDSTTIMEMM.GetValue().ToString());
            sLDEDTIME = Set_Fill2(this.TXT01_LDEDTIMEHH.GetValue().ToString()) + "." + Set_Fill2(this.TXT01_LDEDTIMEMM.GetValue().ToString());

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_91VBN654", sLDSTTIME.ToString(), sLDEDTIME.ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_LDTOTTIMEHH.SetValue(dt.Rows[0]["EXCEPT_TIME"].ToString().Substring(0, 2));
                this.TXT01_LDTOTTIMEMM.SetValue(dt.Rows[0]["EXCEPT_TIME"].ToString().Substring(2, 2));
            }
        }
        #endregion

        #region Description : 필드 클리어
        private void UP_FieldClear()
        {
            this.TXT01_LDDAY.SetValue("");
            this.TXT01_LDSTTIMEHH.SetValue("");
            this.TXT01_LDSTTIMEMM.SetValue("");
            this.TXT01_LDEDTIMEHH.SetValue("");
            this.TXT01_LDEDTIMEMM.SetValue("");
            this.TXT01_LDTOTTIMEHH.SetValue("");
            this.TXT01_LDTOTTIMEMM.SetValue("");
            this.TXT01_LDSAYU.SetValue("");
        }
        #endregion

        #region Description : 텍스트 이벤트
        private void TXT01_LDSAYU_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SetFocus(BTN61_SAV);
            }
        }
        #endregion
    }
}