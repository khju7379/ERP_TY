using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.UT00
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
    ///  JISIIL : 계약번호
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
    public partial class TYUTAU003I : TYBase
    {
        private string fsJISIIL = string.Empty;
        private string fsJISISQ = string.Empty;
        private string fsGUBUN  = string.Empty;

        #region Description : 페이지 로드
        public TYUTAU003I(string sJISIIL, string sJISISQ)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기 
            this.fsJISIIL = sJISIIL;
            this.fsJISISQ = sJISISQ;
        }

        private void TYUTAU003I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(this.fsJISIIL))
            {
                fsGUBUN = "NEW";

                this.DTP01_JISIIL.SetReadOnly(false);
                this.TXT01_JISISQ.SetReadOnly(true);

                UP_FieldClear();

                SetStartingFocus(this.DTP01_JISIIL);
            }
            else
            {
                this.DTP01_JISIIL.SetReadOnly(true);
                this.TXT01_JISISQ.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_71DDY458", this.fsJISIIL, this.fsJISISQ);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    fsGUBUN = "UPT";
                    this.CurrentDataTableRowMapping(dt, "01");
                }

                SetStartingFocus(this.CBO01_JISIHT);
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            // DB2 저장
            UP_Save();

            // 오라클 저장
            UP_Save_Oracle();

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

        #region Description : DB2 저장 메소드
        private void UP_Save()
        {
            string sSEND_GUBUN = string.Empty;
            string sHSOPTION = string.Empty;

            DataTable dt = new DataTable();

            sSEND_GUBUN = "NEW";

            if (this.TXT01_JISITK.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_71HE0495", this.TXT01_JISITK.GetValue().ToString().Trim());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sSEND_GUBUN = "UPT";
                }
            }

            if (string.IsNullOrEmpty(this.fsJISIIL))
            {
                sHSOPTION = "A";

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_71HFZ509", Get_Date(this.DTP01_JISIIL.GetValue().ToString()));

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_JISISQ.SetValue(dt.Rows[0]["JSSEQ"].ToString());

                    if (dt.Rows[0]["JSSEQ"].ToString() == "1")
                    {
                        // 등록
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_71HG7510", Get_Date(this.DTP01_JISIIL.GetValue().ToString()));

                        this.DbConnector.ExecuteNonQuery();
                    }
                    else
                    {
                        // 수정
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach("TY_P_UT_71HG9511", this.TXT01_JISISQ.GetValue().ToString(), Get_Date(this.DTP01_JISIIL.GetValue().ToString()));

                        this.DbConnector.ExecuteNonQuery();
                    }
                }
            }
            else
            {
                sHSOPTION = "C";
            }

            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(this.fsJISIIL))
            {
                // 작업지시 등록
                this.DbConnector.Attach("TY_P_UT_71HFK505", Get_Date(this.DTP01_JISIIL.GetValue().ToString()),
                                                            this.TXT01_JISISQ.GetValue().ToString(),
                                                            this.CBO01_JISIHT.GetValue().ToString(),
                                                            Set_TankNo(this.TXT01_JISITK.GetValue().ToString()),
                                                            Get_Date(this.DTP01_JIIPHANG.GetValue().ToString()),
                                                            this.CBH01_JISIVS.GetValue().ToString().ToUpper(),
                                                            this.CBH01_JISIVS.GetText().ToString(),
                                                            this.CBH01_JISIHJ.GetValue().ToString().ToUpper(),
                                                            this.CBH01_JISIHJ.GetText().ToString(),
                                                            this.CBH01_JISIHM.GetValue().ToString().ToUpper(),
                                                            this.CBH01_JISIHM.GetText().ToString(),
                                                            Get_Numeric(this.TXT01_JISIQTY.GetValue().ToString()),
                                                            Get_Date(this.DTP01_JISTDATE.GetValue().ToString()),
                                                            Set_Fill2(this.TXT01_JISTTIME1.GetValue().ToString()) + Set_Fill2(this.TXT01_JISTTIME2.GetValue().ToString()),
                                                            Get_Date(this.DTP01_JIENDATE.GetValue().ToString()),
                                                            Set_Fill2(this.TXT01_JIENTIME1.GetValue().ToString()) + Set_Fill2(this.TXT01_JIENTIME2.GetValue().ToString()),
                                                            this.TXT01_JISIIGTK.GetValue().ToString(),
                                                            this.CBH01_JISABUN.GetValue().ToString(),
                                                            this.CBH01_JISABUN.GetText().ToString(),
                                                            this.TXT01_JIVCF.GetValue().ToString(),
                                                            this.TXT01_JIWCF.GetValue().ToString(),
                                                            this.TXT01_JIPUMP.GetValue().ToString(),
                                                            this.TXT01_JISISJ.GetValue().ToString(),
                                                            this.TXT01_JISIWT.GetValue().ToString(),
                                                            this.TXT01_JISENDGB.GetValue().ToString(),
                                                            this.TXT01_JIPUMPLN.GetValue().ToString(),
                                                            Get_Numeric(this.TXT01_JIENSILL.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_JIENSILT.GetValue().ToString())
                                                            );
            }
            else
            {
                // 작업지시 수정
                this.DbConnector.Attach("TY_P_UT_71HFK506", this.CBO01_JISIHT.GetValue().ToString(),
                                                            Set_TankNo(this.TXT01_JISITK.GetValue().ToString()),
                                                            Get_Date(this.DTP01_JIIPHANG.GetValue().ToString()),
                                                            this.CBH01_JISIVS.GetValue().ToString().ToUpper(),
                                                            this.CBH01_JISIVS.GetText().ToString(),
                                                            this.CBH01_JISIHJ.GetValue().ToString().ToUpper(),
                                                            this.CBH01_JISIHJ.GetText().ToString(),
                                                            this.CBH01_JISIHM.GetValue().ToString().ToUpper(),
                                                            this.CBH01_JISIHM.GetText().ToString(),
                                                            Get_Numeric(this.TXT01_JISIQTY.GetValue().ToString()),
                                                            Get_Date(this.DTP01_JISTDATE.GetValue().ToString()),
                                                            Set_Fill2(this.TXT01_JISTTIME1.GetValue().ToString()) + Set_Fill2(this.TXT01_JISTTIME2.GetValue().ToString()),
                                                            Get_Date(this.DTP01_JIENDATE.GetValue().ToString()),
                                                            Set_Fill2(this.TXT01_JIENTIME1.GetValue().ToString()) + Set_Fill2(this.TXT01_JIENTIME2.GetValue().ToString()),
                                                            this.TXT01_JISIIGTK.GetValue().ToString(),
                                                            this.CBH01_JISABUN.GetValue().ToString(),
                                                            this.CBH01_JISABUN.GetText().ToString(),
                                                            this.TXT01_JIVCF.GetValue().ToString(),
                                                            this.TXT01_JIWCF.GetValue().ToString(),
                                                            this.TXT01_JIPUMP.GetValue().ToString(),
                                                            this.TXT01_JISISJ.GetValue().ToString(),
                                                            this.TXT01_JISIWT.GetValue().ToString(),
                                                            this.TXT01_JISENDGB.GetValue().ToString(),
                                                            this.TXT01_JIPUMPLN.GetValue().ToString(),
                                                            Get_Numeric(this.TXT01_JIENSILL.GetValue().ToString()),
                                                            Get_Numeric(this.TXT01_JIENSILT.GetValue().ToString()),
                                                            Get_Date(this.DTP01_JISIIL.GetValue().ToString()),
                                                            this.TXT01_JISISQ.GetValue().ToString()
                                                            );
            }

            // 자동화 TANK자동화 HISTORY
            this.DbConnector.Attach("TY_P_UT_71DBO448", TYUserInfo.EmpNo,
                                                        sHSOPTION.ToString(),
                                                        Get_Date(this.DTP01_JISIIL.GetValue().ToString()),
                                                        this.TXT01_JISISQ.GetValue().ToString(),
                                                        this.CBO01_JISIHT.GetValue().ToString(),
                                                        Set_TankNo(this.TXT01_JISITK.GetValue().ToString()),
                                                        Get_Date(this.DTP01_JIIPHANG.GetValue().ToString()),
                                                        this.CBH01_JISIVS.GetValue().ToString().ToUpper(),
                                                        this.CBH01_JISIVS.GetText().ToString(),
                                                        this.CBH01_JISIHJ.GetValue().ToString().ToUpper(),
                                                        this.CBH01_JISIHJ.GetText().ToString(),
                                                        this.CBH01_JISIHM.GetValue().ToString().ToUpper(),
                                                        this.CBH01_JISIHM.GetText().ToString(),
                                                        Get_Numeric(this.TXT01_JISIQTY.GetValue().ToString()),
                                                        Get_Date(this.DTP01_JISTDATE.GetValue().ToString()),
                                                        Set_Fill2(this.TXT01_JISTTIME1.GetValue().ToString()) + Set_Fill2(this.TXT01_JISTTIME2.GetValue().ToString()),
                                                        Get_Date(this.DTP01_JIENDATE.GetValue().ToString()),
                                                        Set_Fill2(this.TXT01_JIENTIME1.GetValue().ToString()) + Set_Fill2(this.TXT01_JIENTIME2.GetValue().ToString()),
                                                        this.TXT01_JISIIGTK.GetValue().ToString(),
                                                        this.CBH01_JISABUN.GetValue().ToString(),
                                                        this.CBH01_JISABUN.GetText().ToString(),
                                                        this.TXT01_JIVCF.GetValue().ToString(),
                                                        this.TXT01_JIWCF.GetValue().ToString(),
                                                        this.TXT01_JIPUMP.GetValue().ToString(),
                                                        this.TXT01_JISISJ.GetValue().ToString(),
                                                        this.TXT01_JISIWT.GetValue().ToString()
                                                        );

            // 탱크 파일 업데이트(UTITANKF)
            this.DbConnector.Attach("TY_P_UT_71HEW497", this.CBH01_JISIHM.GetValue().ToString(),
                                                        this.TXT01_JIVCF.GetValue().ToString(),
                                                        this.TXT01_JIWCF.GetValue().ToString(),
                                                        this.TXT01_JISITK.GetValue().ToString().Trim()
                                                        );

            //입고, 선적일경우 입항파일에 UPDATE(UTIVESJF)
            if (this.CBO01_JISIHT.GetValue().ToString() == "1" || this.CBO01_JISIHT.GetValue().ToString() == "4")
            {
                if (string.IsNullOrEmpty(this.fsJISIIL)) // 등록
                {
                    // 선박작업관리 파일 등록
                    this.DbConnector.Attach("TY_P_UT_71HF2498", Get_Date(this.DTP01_JIIPHANG.GetValue().ToString()),
                                                                this.CBH01_JISIVS.GetValue().ToString().ToUpper(),
                                                                Set_TankNo(this.TXT01_JISITK.GetValue().ToString()),
                                                                this.CBH01_JISIHM.GetValue().ToString().ToUpper(),
                                                                this.CBH01_JISIHJ.GetValue().ToString().ToUpper(),
                                                                this.CBO01_JISIHT.GetValue().ToString(),
                                                                Get_Date(this.DTP01_JISIIL.GetValue().ToString()),
                                                                this.TXT01_JISISQ.GetValue().ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                }
                else // 수정
                {
                    // 선박작업관리 파일 삭제
                    this.DbConnector.Attach("TY_P_UT_71HFN507", Get_Date(this.DTP01_JISIIL.GetValue().ToString()),
                                                                this.TXT01_JISISQ.GetValue().ToString()
                                                                );

                    // 선박작업관리 파일 등록
                    this.DbConnector.Attach("TY_P_UT_71HF2498", Get_Date(this.DTP01_JIIPHANG.GetValue().ToString()),
                                                                this.CBH01_JISIVS.GetValue().ToString().ToUpper(),
                                                                Set_TankNo(this.TXT01_JISITK.GetValue().ToString()),
                                                                this.CBH01_JISIHM.GetValue().ToString().ToUpper(),
                                                                this.CBH01_JISIHJ.GetValue().ToString().ToUpper(),
                                                                this.CBO01_JISIHT.GetValue().ToString(),
                                                                Get_Date(this.DTP01_JISIIL.GetValue().ToString()),
                                                                this.TXT01_JISISQ.GetValue().ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                }
            }

            if (this.TXT01_JISITK.GetValue().ToString() != "")
            {
                if (sSEND_GUBUN.ToString() == "NEW")
                {
                    this.DbConnector.Attach("TY_P_UT_71HF1499", Set_TankNo(this.TXT01_JISITK.GetValue().ToString()),
                                                                this.CBH01_JISIHJ.GetValue().ToString(),
                                                                this.CBH01_JISIHJ.GetText().ToString(),
                                                                this.CBH01_JISIHM.GetValue().ToString(),
                                                                this.CBH01_JISIHM.GetText().ToString(),
                                                                this.TXT01_JIENSILL.GetValue().ToString(),
                                                                this.TXT01_JIENSILT.GetValue().ToString(),
                                                                "N"
                                                                );
                }
                else
                {
                    if (this.CBH01_JISIHJ.GetValue().ToString() != "" && this.CBH01_JISIHM.GetValue().ToString() != "")
                    {
                        this.DbConnector.Attach("TY_P_UT_71HF5500", this.CBH01_JISIHJ.GetValue().ToString(),
                                                                    this.CBH01_JISIHJ.GetText().ToString(),
                                                                    this.CBH01_JISIHM.GetValue().ToString(),
                                                                    this.CBH01_JISIHM.GetText().ToString(),
                                                                    this.TXT01_JIENSILL.GetValue().ToString(),
                                                                    this.TXT01_JIENSILT.GetValue().ToString(),
                                                                    "N",
                                                                    this.TXT01_JISITK.GetValue().ToString().Trim()
                                                                    );
                    }

                    if (this.CBH01_JISIHJ.GetValue().ToString() != "" && this.CBH01_JISIHM.GetValue().ToString() == "")
                    {
                        this.DbConnector.Attach("TY_P_UT_71HFB502", this.CBH01_JISIHJ.GetValue().ToString(),
                                                                    this.CBH01_JISIHJ.GetText().ToString(),
                                                                    this.TXT01_JIENSILL.GetValue().ToString(),
                                                                    this.TXT01_JIENSILT.GetValue().ToString(),
                                                                    "N",
                                                                    this.TXT01_JISITK.GetValue().ToString().Trim()
                                                                    );
                    }

                    if (this.CBH01_JISIHJ.GetValue().ToString() == "" && this.CBH01_JISIHM.GetValue().ToString() != "")
                    {
                        this.DbConnector.Attach("TY_P_UT_71HFE503", this.CBH01_JISIHM.GetValue().ToString(),
                                                                    this.CBH01_JISIHM.GetText().ToString(),
                                                                    this.TXT01_JIENSILL.GetValue().ToString(),
                                                                    this.TXT01_JIENSILT.GetValue().ToString(),
                                                                    "N",
                                                                    this.TXT01_JISITK.GetValue().ToString().Trim()
                                                                    );
                    }

                    if (this.CBH01_JISIHJ.GetValue().ToString() == "" && this.CBH01_JISIHM.GetValue().ToString() == "")
                    {
                        this.DbConnector.Attach("TY_P_UT_71HFE504", this.TXT01_JIENSILL.GetValue().ToString(),
                                                                    this.TXT01_JIENSILT.GetValue().ToString(),
                                                                    "N",
                                                                    this.TXT01_JISITK.GetValue().ToString().Trim()
                                                                    );
                    }
                }
            }

            this.DbConnector.ExecuteTranQueryList();
        }
        #endregion

        #region Description : 오라클 저장
        private void UP_Save_Oracle()
        {
            string sJISIHT = string.Empty;

            DataTable dt = new DataTable();

            try
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_71HFU508", Get_Date(this.DTP01_JISIIL.GetValue().ToString()), this.TXT01_JISISQ.GetValue().ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sJISIHT = dt.Rows[0]["JISIHT"].ToString();

                    if (sJISIHT.ToString() == "6")
                    {
                        // 오라클 자동화 업데이트
                        UP_ChangeDensity
                            (
                            dt.Rows[0]["JISIHM"].ToString(),
                            dt.Rows[0]["JISIHMNM"].ToString(),
                            dt.Rows[0]["JIVCF"].ToString(),
                            dt.Rows[0]["JIWCF"].ToString(),
                            dt.Rows[0]["JISITK"].ToString(),
                            dt.Rows[0]["JISIHJ"].ToString(),
                            dt.Rows[0]["JISIHJNM"].ToString()
                            );
                    }
                    else if (sJISIHT.ToString() == "7")
                    {
                        // 오라클 자동화 업데이트
                        UP_GasTankMonitor
                            (
                            dt.Rows[0]["JISTSILL"].ToString(),
                            dt.Rows[0]["JISTSILT"].ToString()
                            );
                    }
                    else if (sJISIHT.ToString() == "1" || sJISIHT.ToString() == "2" ||
                            sJISIHT.ToString() == "3" || sJISIHT.ToString() == "4")
                    {
                        // 오라클 자동화 업데이트
                        UP_UpdateWkod
                            (
                            dt.Rows[0]["JISIIL"].ToString(),
                            dt.Rows[0]["JISISQ"].ToString(),
                            sJISIHT.ToString(),
                            dt.Rows[0]["JISITK"].ToString(),
                            dt.Rows[0]["JIIPHANG"].ToString(),
                            dt.Rows[0]["JISIVS"].ToString(),
                            dt.Rows[0]["JISIVSNM"].ToString(),
                            dt.Rows[0]["JISIHJ"].ToString(),
                            dt.Rows[0]["JISIHJNM"].ToString(),
                            dt.Rows[0]["JISIHM"].ToString(),
                            dt.Rows[0]["JISIHMNM"].ToString(),
                            dt.Rows[0]["JISIQTY"].ToString(),
                            dt.Rows[0]["JISTDATE"].ToString(),
                            dt.Rows[0]["JISTTIME"].ToString(),
                            dt.Rows[0]["JIENDATE"].ToString(),
                            dt.Rows[0]["JIENTIME"].ToString(),
                            "99999999",
                            "9999",
                            "99999999",
                            "9999",
                            dt.Rows[0]["JISIIGTK"].ToString(),
                            dt.Rows[0]["JIVCF"].ToString(),
                            dt.Rows[0]["JIWCF"].ToString(),
                            dt.Rows[0]["JIPUMP"].ToString(),
                            dt.Rows[0]["JISENDGB"].ToString(),
                            dt.Rows[0]["JIPUMPLN"].ToString(),
                            "0"
                            );
                    }
                }
            }
            catch (Exception ex)
            {
                string a = string.Empty;
                string b = string.Empty;

                a = ex.ToString();
                b = a.ToString();
            }
        }
        #endregion

        #region Description : 오라클 비중 변경 메소드
        private void UP_ChangeDensity(string sJISIHM,  string sJISIHMNM, string sJIVCF,
                                      string sJIWCF,   string sJISITK,   string sJISIHJ,
                                      string sJISIHJNM)
        {
            string sHMBJ_GUBUN = string.Empty;
            string sTKST_GUBUN = string.Empty;
            string sHWAMUL     = string.Empty;

            DataTable dt = new DataTable();

            try
            {
                // HMBJ
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_71HGJ512", sJISIHM.ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sHMBJ_GUBUN = "UPT";
                }
                else
                {
                    sHMBJ_GUBUN = "INS";
                }

                if (sJISIHM.ToString() == "A27")
                {
                    sHWAMUL = "무수초산";
                }
                else
                {
                    sHWAMUL = sJISIHMNM.ToString();
                }

                // TKST 조회
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_677HL669", sJISITK.ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    sTKST_GUBUN = "UPT";
                }


                this.DbConnector.CommandClear();

                //  오라클 - HMBJ
                if (sHMBJ_GUBUN.ToString() == "INS")
                {
                    this.DbConnector.Attach("TY_P_UT_677GY666", sJISIHM.ToString(),
                                                                sHWAMUL.ToString(),
                                                                sJIWCF.ToString(),
                                                                sJIVCF.ToString()
                                                                );
                }
                else
                {
                    this.DbConnector.Attach("TY_P_UT_677GW665", sHWAMUL.ToString(),
                                                                sJIWCF.ToString(),
                                                                sJIVCF.ToString(),
                                                                sJISIHM.ToString()
                                                                );
                }

                // 오라클 - TKST
                if (sTKST_GUBUN.ToString() == "UPT")
                {
                    if (sHWAMUL.Length > 14)
                    {
                        sHWAMUL = sHWAMUL.Substring(0, 15).ToString();
                    }

                    this.DbConnector.Attach("TY_P_UT_677HN670", sJISIHM.ToString(),
                                                                sHWAMUL.ToString(),
                                                                sJIWCF.ToString(),
                                                                sJIVCF.ToString(),
                                                                sJISITK.ToString()
                                                                );
                }

                this.DbConnector.ExecuteTranQueryList();
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region Description : 오라클 5007탱크 업데이트 메소드
        private void UP_GasTankMonitor(string sJISTSILL, string sJISTSILT)
        {
            double dLevel = 0;
            double dTemp = 0;
            double dLevelcm = 0;
            double dLevelmm = 0;
            double dNumeric = 0;
            double dVolume1 = 0;
            double dVolume2 = 0;
            double dDensity = 0;

            string sOraSql  = string.Empty;
            string sVolume  = string.Empty;
            string sMass    = string.Empty;

            dLevel = double.Parse(sJISTSILL);
            dTemp = double.Parse(sJISTSILT);

            dLevelcm = double.Parse(UP_DotDelete(Convert.ToString(dLevel)));
            dLevelmm = dLevel - dLevelcm;

            //레벨에 해당하는 Volume값을 GAUG Table에서 찾는다.
            dNumeric = dLevelcm + 0;

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_71HGY516", Convert.ToString(dNumeric));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dVolume1 = double.Parse(Get_Numeric(dt.Rows[0]["VOLUME"].ToString()));
            }



            dNumeric = dLevelcm + 1;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_71HGY516", Convert.ToString(dNumeric));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dVolume2 = double.Parse(Get_Numeric(dt.Rows[0]["VOLUME"].ToString()));
            }

            sVolume = string.Format("{0,9:N3}", dVolume1 + (dVolume2 - dVolume1) * dLevelmm).Trim();


            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_71HH1517", sJISTSILT.ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dDensity = double.Parse(Get_Numeric(dt.Rows[0]["DENSITY"].ToString()));
            }

            sMass = string.Format("{0,9:N3}", double.Parse(sVolume) * dDensity).Trim();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_71HH3518", Convert.ToString(dLevel),
                                                        sMass.ToString(),
                                                        Convert.ToString(dTemp),
                                                        sVolume.ToString(),
                                                        DateTime.Now.ToString("yyyyMMddHHmmss")
                                                        );
                

            this.DbConnector.ExecuteTranQueryList();
        }
        #endregion

        #region Description : 오라클 WKOD 업데이트 메소드
        private void UP_UpdateWkod(string sJISIIL,   string sJISISQ,   string sJISIHT,   string sJISITK,
                                   string sJIIPHANG, string sJISIVS,   string sJISIVSNM, string sJISIHJ,
                                   string sJISIHJNM, string sJISIHM,   string sJISIHMNM, string sJISIQTY,
                                   string sJISTDATE, string sJISTTIME, string sJIENDATE, string sJIENTIME,
                                   string sJISTILJA, string sJISTSLDT, string sJIENILJA, string sJIENSLDT,
                                   string sJISIIGTK, string sJIVCF,    string sJIWCF,    string sJIPUMP,
                                   string sJISENDGB, string sJIPUMPLN, string sJISIWT)
        {
            string sOraSql = string.Empty;
            string sGUBUN  = string.Empty;
            string sHMCODE = string.Empty;
            string sHMNAME = string.Empty;
            string sTEMP_L = string.Empty;
            string sTEMP_H = string.Empty;
            string sVCF    = string.Empty;
            string sWCF    = string.Empty;
            string sTNO    = string.Empty;

            if (sJISIHT.ToString() == "3")
            {
                sTNO = sJISIIGTK.ToString();
            }
            else
            {
                sTNO = sJISITK.ToString();
                sJISIIGTK = "-";
            }

            if (sJISIVSNM.Length > 14)
            {
                sJISIVSNM = sJISIVSNM.Substring(0, 14).ToString();
            }

            if (sJISIHJNM.Length > 14)
            {
                sJISIHJNM = sJISIHJNM.Substring(0, 12).ToString();
            }

            if (sJISIHMNM.Length > 14)
            {
                sJISIHMNM = sJISIHMNM.Substring(0, 12).ToString();
            }

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_71HGJ512", sJISIHM.ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sHMCODE = dt.Rows[0]["HMCODE"].ToString();
                sHMNAME = dt.Rows[0]["HMNAME"].ToString();
                sTEMP_L = dt.Rows[0]["TEMP_L"].ToString();
                sTEMP_H = dt.Rows[0]["TEMP_H"].ToString();
                sVCF    = dt.Rows[0]["VCF"].ToString();
                sWCF    = dt.Rows[0]["WCF"].ToString();
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_677HL669", sTNO.ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                sGUBUN = "UPT";
            }
            else
            {
                sGUBUN = "INS";
            }

            this.DbConnector.CommandClear();

            // WKOD
            this.DbConnector.Attach("TY_P_UT_71HHA519", sJISIIL.ToString(),
                                                        sJISISQ.ToString(),
                                                        sJISIHT.ToString(),
                                                        sJISITK.ToString(),
                                                        sJIIPHANG.ToString(),
                                                        sJISIVS.ToString(),
                                                        sJISIVSNM.ToString(),
                                                        sJISIHJ.ToString(),
                                                        sJISIHJNM.ToString(),
                                                        sJISIHM.ToString(),
                                                        sJISIHMNM.ToString(),
                                                        Get_Numeric(sJISIQTY.ToString()),
                                                        sJISTDATE.ToString(),
                                                        sJISTTIME.ToString(),
                                                        sJIENDATE.ToString(),
                                                        sJIENTIME.ToString(),
                                                        sJISTILJA.ToString(),
                                                        sJISTSLDT.ToString(),
                                                        sJIENILJA.ToString(),
                                                        sJIENSLDT.ToString(),
                                                        sJISIIGTK.ToString(),
                                                        Get_Numeric(sJIVCF.ToString()),
                                                        Get_Numeric(sJIWCF.ToString()),
                                                        sJIPUMP.ToString(),
                                                        sJISENDGB.ToString(),
                                                        sJIPUMPLN.ToString(),
                                                        sJISIWT.ToString()
                                                        );

            if (sHMNAME.Length > 11)
            {
                sHMNAME = sHMNAME.Substring(0, 12).ToString();
            }

            if (sJISIHJNM.Length > 11)
            {
                sJISIHJNM = sJISIHJNM.Substring(0, 12).ToString();
            }

            // TKST
            if (sGUBUN == "INS")
            {
                this.DbConnector.Attach("TY_P_UT_71HHH520", sTNO.ToString(),
                                                            sHMCODE.ToString(),
                                                            sHMNAME.ToString(),
                                                            Get_Numeric(sTEMP_L.ToString()),
                                                            Get_Numeric(sTEMP_H.ToString()),
                                                            Get_Numeric(sVCF.ToString()),
                                                            Get_Numeric(sWCF.ToString()),
                                                            sJISIHJ.ToString(),
                                                            sJISIHJNM.ToString()
                                                            );
            }
            else
            {
                this.DbConnector.Attach("TY_P_UT_71HHI521", sHMCODE.ToString(),
                                                            sHMNAME.ToString(),
                                                            Get_Numeric(sTEMP_L.ToString()),
                                                            Get_Numeric(sTEMP_H.ToString()),
                                                            Get_Numeric(sVCF.ToString()),
                                                            Get_Numeric(sWCF.ToString()),
                                                            sJISIHJ.ToString(),
                                                            sJISIHJNM.ToString(),
                                                            sTNO.ToString().Trim()
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();
        }
        #endregion

        #region Description : 필드 클리어
        private void UP_FieldClear()
        {
            this.TXT01_JISISJ.SetValue("");
            this.TXT01_JISIWT.SetValue("");
            this.DTP01_JIIPHANG.SetValue("");
            this.CBH01_JISIVS.SetValue("");
            this.CBH01_JISIHJ.SetValue("");
            this.CBH01_JISIHM.SetValue("");
            this.TXT01_JISITK.SetValue("");
            this.CBH01_JISABUN.SetValue("");
            this.DTP01_JISTDATE.SetValue("");
            this.TXT01_JISTTIME1.SetValue("");
            this.TXT01_JISTTIME2.SetValue("");
            this.DTP01_JISTILJA.SetValue("");
            this.TXT01_JISTSLDT1.SetValue("");
            this.TXT01_JISTSLDT2.SetValue("");
            this.DTP01_JIENDATE.SetValue("");
            this.TXT01_JIENTIME1.SetValue("");
            this.TXT01_JIENTIME2.SetValue("");
            this.DTP01_JIENILJA.SetValue("");
            this.TXT01_JIENSLDT1.SetValue("");
            this.TXT01_JIENSLDT2.SetValue("");
            this.TXT01_JISIQTY.SetValue("");
            this.TXT01_JISILQTY.SetValue("");
            this.TXT01_JISIIGTK.SetValue("");
            this.TXT01_JIPUMP.SetValue("");
            this.TXT01_JIVCF.SetValue("");
            this.TXT01_JIWCF.SetValue("");
            this.TXT01_JIENSILL.SetValue("");
            this.TXT01_JIENSILT.SetValue("");
            this.TXT01_JISENDGB.SetValue("");
            this.TXT01_JIPUMPLN.SetValue("");
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if ((this.CBO01_JISIHT.GetValue().ToString() != "2" && fsGUBUN == "UPT") && this.TXT01_JISISJ.GetValue().ToString() == "*")
            {
                this.ShowMessage("TY_M_UT_71H9D472");
                SetFocus(this.CBO01_JISIHT);

                e.Successed = false;
                return;
            }

            if (fsGUBUN == "UPT")
            {
                if (this.TXT01_JISIWT.GetValue().ToString() == "Y")
                {
                    this.ShowMessage("TY_M_UT_71DAV443");
                    SetFocus(this.CBO01_JISIHT);

                    e.Successed = false;
                    return;
                }
            }

            if (this.CBO01_JISIHT.GetValue().ToString() == "1" || this.CBO01_JISIHT.GetValue().ToString() == "4")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_71DBE445",
                                       Get_Date(this.DTP01_JISIIL.GetValue().ToString()),
                                       this.TXT01_JISISQ.GetValue().ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (Int32.Parse(dt.Rows[0]["VJJBSTIL"].ToString()) != 0)
                    {
                        this.ShowMessage("TY_M_UT_71DBG446");
                        e.Successed = false;
                        return;
                    }

                    if (Int32.Parse(dt.Rows[0]["VJJBENIL"].ToString()) != 0)
                    {
                        this.ShowMessage("TY_M_UT_71DBG447");
                        e.Successed = false;
                        return;
                    }
                }
            }






            if (Int32.Parse(Get_Numeric(Get_Date(this.DTP01_JISTDATE.GetValue().ToString()))) > 0)
            {
                if (Int32.Parse(Get_Date(this.DTP01_JISIIL.GetValue().ToString())) > Int32.Parse(Get_Date(this.DTP01_JISTDATE.GetValue().ToString())))
                {
                    this.ShowMessage("TY_M_UT_71H9J473");
                    SetFocus(this.DTP01_JISTDATE);

                    e.Successed = false;
                    return;
                }
            }

            //본선
            if (this.CBO01_JISIHT.GetValue().ToString() != "6" && this.CBO01_JISIHT.GetValue().ToString() != "7")
            {
                if (this.CBH01_JISIVS.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_UT_71H9N474");
                    SetFocus(this.CBH01_JISIVS.CodeText);

                    e.Successed = false;
                    return;
                }
            }

            //화주
            if (this.CBO01_JISIHT.GetValue().ToString() != "6" && this.CBO01_JISIHT.GetValue().ToString() != "7")
            {
                if (this.CBH01_JISIHJ.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_UT_71H9N475");
                    SetFocus(this.CBH01_JISIHJ.CodeText);

                    e.Successed = false;
                    return;
                }
            }

            //화물
            if (this.CBO01_JISIHT.GetValue().ToString() != "7")
            {
                if (this.CBH01_JISIHM.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_UT_71H9P476");
                    SetFocus(this.CBH01_JISIHM.CodeText);

                    e.Successed = false;
                    return;
                }
            }

            if (this.CBH01_JISIHM.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_71H9R477",
                                       this.CBH01_JISIHM.GetValue().ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_JIVCF.SetValue(dt.Rows[0]["HMVCF"].ToString());

                    if (double.Parse(Get_Numeric(this.TXT01_JIWCF.GetValue().ToString())) == 0)
                    {
                        this.TXT01_JIWCF.SetValue(dt.Rows[0]["HMWCF"].ToString());
                    }
                }
                else
                {
                    this.ShowMessage("TY_M_UT_71H9S478");
                    SetFocus(this.CBH01_JISIHM.CodeText);

                    e.Successed = false;
                    return;
                }
            }

            if (this.TXT01_JISITK.GetValue().ToString() == "")
            {
                this.ShowMessage("TY_M_UT_66SDG425");
                SetFocus(this.TXT01_JISITK);

                e.Successed = false;
                return;
            }

            if ((this.CBO01_JISIHT.GetValue().ToString() != "7") && (this.TXT01_JISITK.GetValue().ToString() == "5007"))
            {
                this.ShowMessage("TY_M_UT_71H9U479");
                SetFocus(this.TXT01_JISITK);

                e.Successed = false;
                return;
            }

            if (this.CBO01_JISIHT.GetValue().ToString() == "7")
            {
                if (this.TXT01_JISITK.GetValue().ToString() != "5007")
                {
                    this.ShowMessage("TY_M_UT_71H9V480");
                    SetFocus(this.TXT01_JISITK);

                    e.Successed = false;
                    return;
                }

                if (Convert.ToDouble(this.TXT01_JIENSILL.GetValue().ToString()) <= 0)
                {
                    this.ShowMessage("TY_M_UT_71H9V481");
                    SetFocus(this.TXT01_JIENSILL);

                    e.Successed = false;
                    return;
                }

                if (Convert.ToDouble(this.TXT01_JIENSILT.GetValue().ToString()) == 0 || Convert.ToDouble(this.TXT01_JIENSILT.GetValue().ToString()) > -1)
                {
                    this.ShowMessage("TY_M_UT_71H9W482");
                    SetFocus(this.TXT01_JIENSILT);

                    e.Successed = false;
                    return;
                }
            }

            // 탱크확인
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_676GL604",
                                    this.TXT01_JISITK.GetValue().ToString().Trim()
                                    );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_676GD601");
                SetFocus(this.TXT01_JISITK);

                e.Successed = false;
                return;
            }

            //사번확인
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                                    "TY_P_UT_677ES637",
                                    this.CBH01_JISABUN.GetValue().ToString()
                                    );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_71HA0483");
                SetFocus(this.CBH01_JISABUN.CodeText);

                e.Successed = false;
                return;
            }

            if (this.CBO01_JISIHT.GetValue().ToString() == "6" || this.CBO01_JISIHT.GetValue().ToString() == "7")
            {
                this.DTP01_JIIPHANG.SetValue("");
                this.CBH01_JISIVS.SetValue("");
                this.CBH01_JISIHJ.SetValue("");
            }

            if (this.CBO01_JISIHT.GetValue().ToString() == "2")
            {
                //시작일자
                string sYear = "";
                string sMonth = "";
                string sDay = "";


                if (Get_Date(this.DTP01_JISTDATE.GetValue().ToString()).Length == 8)
                {
                    //시간체크
                    if (Int32.Parse(Get_Numeric(Set_Fill2(this.TXT01_JISTTIME1.GetValue().ToString()))) > 24 || Int32.Parse(Get_Numeric(Set_Fill2(this.TXT01_JISTTIME1.GetValue().ToString()))) < 1)
                    {
                        this.ShowMessage("TY_M_UT_71HAJ485");
                        SetFocus(this.TXT01_JISTTIME1);

                        e.Successed = false;
                        return;
                    }
                    if (Int32.Parse(Get_Numeric(Set_Fill2(this.TXT01_JISTTIME2.GetValue().ToString()))) > 59 || Int32.Parse(Get_Numeric(Set_Fill2(this.TXT01_JISTTIME2.GetValue().ToString()))) < 0)
                    {
                        this.ShowMessage("TY_M_UT_71HAJ485");
                        SetFocus(this.TXT01_JISTTIME2);

                        e.Successed = false;
                        return;
                    }
                }

                if (Get_Date(this.DTP01_JIENDATE.GetValue().ToString()).Length == 8)
                {
                    //종료시간
                    sYear  = Get_Date(this.DTP01_JIENDATE.GetValue().ToString()).Substring(0, 4);
                    sMonth = Get_Date(this.DTP01_JIENDATE.GetValue().ToString()).Substring(4, 2);
                    sDay   = Get_Date(this.DTP01_JIENDATE.GetValue().ToString()).Substring(6, 2);

                    //시간체크
                    if (Int32.Parse(Get_Numeric(this.TXT01_JIENTIME1.GetValue().ToString())) > 24 || Int32.Parse(Get_Numeric(this.TXT01_JIENTIME1.GetValue().ToString())) < 1)
                    {
                        this.ShowMessage("TY_M_UT_71HAJ485");
                        SetFocus(this.TXT01_JIENTIME1);

                        e.Successed = false;
                        return;
                    }
                    if (Int32.Parse(Get_Numeric(this.TXT01_JIENTIME2.GetValue().ToString())) > 59 || Int32.Parse(Get_Numeric(this.TXT01_JIENTIME2.GetValue().ToString())) < 0)
                    {
                        this.ShowMessage("TY_M_UT_71HAJ485");
                        SetFocus(this.TXT01_JIENTIME2);

                        e.Successed = false;
                        return;
                    }
                }

                int iSTTIME = Int32.Parse(Get_Numeric(Set_Fill2(this.TXT01_JISTTIME1.GetValue().ToString()) + Set_Fill2(this.TXT01_JISTTIME2.GetValue().ToString())));
                int iENTIME = Int32.Parse(Get_Numeric(Set_Fill2(this.TXT01_JIENTIME1.GetValue().ToString()) + Set_Fill2(this.TXT01_JIENTIME2.GetValue().ToString())));

                if ((Int32.Parse(Get_Date(this.DTP01_JISTDATE.GetValue().ToString())) == Int32.Parse(Get_Date(this.DTP01_JIENDATE.GetValue().ToString()))) &&
                    ((iENTIME - iSTTIME) < 2))
                {
                    this.ShowMessage("TY_M_UT_71HAM486");
                    SetFocus(this.TXT01_JIENTIME1);

                    e.Successed = false;
                    return;
                }

                if (this.TXT01_JISENDGB.GetValue().ToString() == "2" && double.Parse(Get_Numeric(this.TXT01_JISIQTY.GetValue().ToString())) <= 0)
                {
                    this.ShowMessage("TY_M_UT_71HAR487");
                    SetFocus(this.TXT01_JISIQTY);

                    e.Successed = false;
                    return;
                }

                if (this.TXT01_JIPUMPLN.GetValue().ToString() != "A" && this.TXT01_JIPUMPLN.GetValue().ToString() != "B")
                {
                    this.ShowMessage("TY_M_UT_71HAS488");
                    SetFocus(this.TXT01_JIPUMPLN);

                    e.Successed = false;
                    return;
                }

                if (this.TXT01_JIPUMP.GetValue().ToString() == "")
                {
                    this.ShowMessage("TY_M_UT_71HAS488");
                    SetFocus(this.TXT01_JIPUMPLN);

                    e.Successed = false;
                    return;
                }
            }

            if (this.CBO01_JISIHT.GetValue().ToString() != "6" || this.CBO01_JISIHT.GetValue().ToString() != "7")
            {
            }

            //이고탱크
            if (this.CBO01_JISIHT.GetValue().ToString() == "3" || this.CBO01_JISIHT.GetValue().ToString() == "2")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_676GL604",
                                        this.TXT01_JISIIGTK.GetValue().ToString().Trim()
                                        );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_676GD601");
                    SetFocus(this.TXT01_JISITK);

                    e.Successed = false;
                    return;
                }
            }
            else
            {
                this.TXT01_JISIIGTK.SetValue("");
            }

            if (this.CBO01_JISIHT.GetValue().ToString() == "2" && fsGUBUN == "NEW")
            {
                if (Int32.Parse(Get_Date(this.DTP01_JISTDATE.GetValue().ToString())) < Int32.Parse(Get_Date(DateTime.Now.ToString("yyyy-MM-dd"))))
                {
                    this.ShowMessage("TY_M_UT_71HBW489");
                    SetFocus(this.DTP01_JISTDATE);

                    e.Successed = false;
                    return;
                }

                string sTime = Get_Numeric(Set_Fill2(this.TXT01_JISTTIME1.GetValue().ToString())) + Get_Numeric(Set_Fill2(this.TXT01_JISTTIME2.GetValue().ToString()));

                if (Int32.Parse(Get_Date(this.DTP01_JISTDATE.GetValue().ToString())) == Int32.Parse(Get_Date(DateTime.Now.ToString("yyyy-MM-dd"))))
                {
                    if (Int32.Parse(sTime) < Int32.Parse(Get_Date(DateTime.Now.ToString("yyyy-MM-dd")).Substring(0, 4)))
                    {
                        this.ShowMessage("TY_M_UT_71HBW489");
                        SetFocus(this.TXT01_JISTTIME1);

                        e.Successed = false;
                        return;
                    }
                }
            }

            if (this.CBO01_JISIHT.GetValue().ToString() == "2")
            {
                if (Int32.Parse(Get_Date(this.DTP01_JIENDATE.GetValue().ToString())) < Int32.Parse(Get_Date(this.DTP01_JISTDATE.GetValue().ToString())))
                {
                    this.ShowMessage("TY_M_UT_71HBW490");
                    SetFocus(this.DTP01_JIENDATE);

                    e.Successed = false;
                    return;
                }

                string sSTTime = Get_Numeric(Set_Fill2(this.TXT01_JISTTIME1.GetValue().ToString())) + Get_Numeric(Set_Fill2(this.TXT01_JISTTIME2.GetValue().ToString()));
                string sENTime = Get_Numeric(Set_Fill2(this.TXT01_JIENTIME1.GetValue().ToString())) + Get_Numeric(Set_Fill2(this.TXT01_JIENTIME2.GetValue().ToString()));

                if (Int32.Parse(Get_Date(this.DTP01_JIENDATE.GetValue().ToString())) == Int32.Parse(Get_Date(this.DTP01_JISTDATE.GetValue().ToString())))
                {
                    if (Int32.Parse(sENTime) < Int32.Parse(sSTTime))
                    {
                        this.ShowMessage("TY_M_UT_71HBX491");
                        SetFocus(this.TXT01_JIENTIME1);

                        e.Successed = false;
                        return;
                    }
                }
            }

            if (this.TXT01_JISITK.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_71H9R477",
                                       this.CBH01_JISIHM.GetValue().ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_JIENSILL.SetValue(dt.Rows[0]["HMTEMPH"].ToString());
                    this.TXT01_JIENSILT.SetValue(dt.Rows[0]["HMTEMPL"].ToString());
                }
            }



            if (this.CBH01_JISIHM.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_71H9R477",
                                       this.CBH01_JISIHM.GetValue().ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.TXT01_JIVCF.SetValue(dt.Rows[0]["HMVCF"].ToString());
                    if (double.Parse(Get_Numeric(this.TXT01_JIWCF.GetValue().ToString())) == 0)
                    {
                        this.TXT01_JIWCF.SetValue(dt.Rows[0]["HMWCF"].ToString());
                    }
                }
                else
                {
                    this.ShowMessage("TY_M_UT_71H9S478");
                    SetFocus(this.CBH01_JISIHM.CodeText);

                    e.Successed = false;
                    return;
                }
            }
            else
            {
                this.TXT01_JIVCF.Text = "0";
                this.TXT01_JIWCF.Text = "0";
            }

            if (this.TXT01_JIPUMP.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_71HC0492",
                                       this.TXT01_JIPUMP.GetValue().ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_71HCP493");
                    SetFocus(this.TXT01_JIPUMP);

                    e.Successed = false;
                    return;
                }
            }

            if (Get_Date(this.DTP01_JISTDATE.GetValue().ToString()).Length == 8)
            {
                if (Int32.Parse(Get_Date(this.DTP01_JISTDATE.GetValue().ToString()).Substring(4, 2)) != 0 &&
                    Int32.Parse(Get_Date(this.DTP01_JISTDATE.GetValue().ToString()).Substring(4, 2)) != 12)
                {
                    if (Get_Date(this.DTP01_JISTDATE.GetValue().ToString()).Substring(0, 4) != Get_Date(this.DTP01_JIENDATE.GetValue().ToString()).Substring(0, 4))
                    {
                        this.ShowMessage("TY_M_UT_71HCQ494");
                        SetFocus(this.TXT01_JIPUMP);

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

        #region Description : 코드헬프 버튼
        private void BTN61_UTTCODEHELP1_Click(object sender, EventArgs e)
        {
            if (this.CBO01_JISIHT.GetValue().ToString() == "1")
            {
                TYUTGB003S popup = new TYUTGB003S();

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.DTP01_JIIPHANG.SetValue(popup.fsIPHANG); // 입항일자
                    this.CBH01_JISIVS.SetValue(popup.fsBONSUN);   // 본선명
                }
            }
            else if (this.CBO01_JISIHT.GetValue().ToString() == "3")
            {
                TYUTGB019S popup = new TYUTGB019S();

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.DTP01_JIIPHANG.SetValue(popup.fsIPHANG);   // 입항일자
                    this.CBH01_JISIVS.SetValue(popup.fsBONSUN);     // 본선
                    this.CBH01_JISIHJ.SetValue(popup.fsHWAJU);      // 화주
                    this.CBH01_JISIHM.SetValue(popup.fsHWAMUL);     // 화물
                    this.TXT01_JISITK.SetValue(popup.fsTANKNO);     // TANK번호
                }
            }
            else if (this.CBO01_JISIHT.GetValue().ToString() == "2" || this.CBO01_JISIHT.GetValue().ToString() == "4" || this.CBO01_JISIHT.GetValue().ToString() == "5")
            {
                TYUTGB008S popup = new TYUTGB008S("");

                if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.DTP01_JIIPHANG.SetValue(popup.fsIPHANG);   // 입항일자
                    this.CBH01_JISIVS.SetValue(popup.fsBONSUN);     // 본선
                    this.CBH01_JISIHJ.SetValue(popup.fsHWAJU);      // 화주
                    this.CBH01_JISIHM.SetValue(popup.fsHWAMUL);     // 화물
                }
            }

            SetFocus(this.CBH01_JISABUN.CodeText);
        }
        #endregion

        #region Description : 텍스트 박스 엔터키
        //private void TXT01_CNCHAM_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == '\r')
        //    {
        //        SetFocus(this.CBO01_CNCHDA);
        //    }
        //}

        //private void CBO01_CNCHDA_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == '\r')
        //    {
        //        SetFocus(this.CBO01_CNCHHP);
        //    }
        //}

        //private void CBO01_CNCHHP_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == '\r')
        //    {
        //        SetFocus(this.TXT01_CNIPAM);
        //    }
        //}

        //private void TXT01_CNIPAM_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == '\r')
        //    {
        //        SetFocus(this.CBO01_CNIPDA);
        //    }
        //}

        //private void CBO01_CNBOJDA_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == '\r')
        //    {
        //        SetFocus(this.CBO01_JISIHT);
        //    }
        //}

        //private void CBO01_CNBOJHP_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == '\r')
        //    {
        //        SetFocus(this.TXT01_CNBUDU);
        //    }
        //}

        //private void TXT01_CNBUDU_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == '\r')
        //    {
        //        SetFocus(this.CBO01_CNBUDUHP);
        //    }
        //}
        #endregion
    }
}
