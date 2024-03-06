using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 용역직 인사기본사항 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.01.19 17:46
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_51GAN168 : 용역직 인사기본사항 조회(팝업)
    ///  TY_P_HR_51JHW190 : 용역직 인사기본사항 등록
    ///  TY_P_HR_51KDN197 : 용역직 인사기본사항 수정
    ///  TY_P_HR_51KDO198 : 용역직 인사기본사항 순번 가져오기
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  KYJJCD : 직종
    ///  KBSEXGB : 성별
    ///  KYBIRGB : 음력구분
    ///  KBGDATE : 그룹입사일
    ///  KBIDATE : 입사일자
    ///  KBBIRTH : 생년월일
    ///  KBHANGL : 한글이름
    ///  KBHANJA : 한자이름
    ///  KBJUMIN : 주민번호
    ///  KBRFID : RF카드번호
    ///  KBTELNO : 전화번호
    ///  KBUPCD : 우편번호
    ///  KYSEQ : 순번
    ///  KYYEAR : 년도
    ///  VNJUSO : 주소
    /// </summary>
    public partial class TYHRBS001I : TYBase
    {
        public DataSet ds = new DataSet();
        string fsBPMYEAR  = string.Empty;
        string fsBPMSEQ   = string.Empty;
        string fsBPMSABUN = string.Empty;

        #region Description : 폼 로드
        public TYHRBS001I(string sBPMYEAR, string sBPMSEQ, string sBPMSABUN)
        {
            InitializeComponent();

            fsBPMYEAR  = sBPMYEAR;
            fsBPMSEQ   = sBPMSEQ;
            fsBPMSABUN = sBPMSABUN;

            this.CBH01_BPMBUSEO.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            this.CBH01_BPMDPAC.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            // 급여구분
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_85HAD056, "BPSPYGUBN", "PYDESC1", "BPSPYGUBN");

            // 급여명
            this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_85HAD056, "BPSPAYCODE", "PLDESC1", "BPSPAYCODE");
        }

        private void TYHRBS001I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_85HAD056, "BPSPYGUBN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_85HAD056, "BPSPAYCODE");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            // 확인
            UP_Run(fsBPMYEAR, fsBPMSEQ, fsBPMSABUN);

            if (string.IsNullOrEmpty(this.fsBPMYEAR) && string.IsNullOrEmpty(this.fsBPMSEQ) && string.IsNullOrEmpty(this.fsBPMSABUN))
            {
                this.TXT01_BPMYEAR.SetReadOnly(false);
                this.TXT01_BPMSEQ.SetReadOnly(true);
                this.CBH01_BPMSABUN.SetReadOnly(false);
                this.TXT01_BPMSTYYMM.SetReadOnly(false);
                this.CBH01_BPMJKCD.SetReadOnly(false);
                this.TXT01_BPMHOBN.SetReadOnly(false);
                this.CBH01_BPMBUSEO.SetReadOnly(false);

                SetStartingFocus(this.TXT01_BPMYEAR);
            }
            else
            {
                this.TXT01_BPMYEAR.SetReadOnly(true);
                this.TXT01_BPMSEQ.SetReadOnly(true);
                this.CBH01_BPMSABUN.SetReadOnly(true);
                this.TXT01_BPMSTYYMM.SetReadOnly(true);
                this.CBH01_BPMJKCD.SetReadOnly(true);
                this.TXT01_BPMHOBN.SetReadOnly(true);
                this.CBH01_BPMBUSEO.SetReadOnly(true);

                SetStartingFocus(this.CBH01_BPMDPAC.CodeText);
            }
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.fsBPMYEAR) && string.IsNullOrEmpty(this.fsBPMSEQ) && string.IsNullOrEmpty(this.fsBPMSABUN))
            {
                this.TXT01_BPMSEQ.SetValue(UP_getSEQ());

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_85NGS082",
                                        Get_Numeric(this.TXT01_BPMYEAR.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_BPMSEQ.GetValue().ToString()),
                                        this.CBH01_BPMSABUN.GetValue().ToString(),
                                        this.TXT01_BPMSTYYMM.GetValue().ToString(),
                                        this.CBH01_BPMJKCD.GetValue().ToString(),
                                        this.CBH01_BPMBUSEO.GetValue().ToString(),
                                        this.CBH01_BPMDPAC.GetValue().ToString(),
                                        this.TXT01_BPMHOBN.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_BPMAVGOTTIME.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_BPMYCHACNT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_BPMDYRATE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_BPMNYRATE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_BPMADDAMOUNT.GetValue().ToString()),
                                        MTB01_BPMJODATE.GetValue().ToString().Replace("-","").Trim(),
                                        MTB01_BPMREDATE.GetValue().ToString().Replace("-", "").Trim(),
                                        TYUserInfo.EmpNo
                                        );
                this.DbConnector.ExecuteTranQuery();

                fsBPMYEAR = this.TXT01_BPMYEAR.GetValue().ToString();
                fsBPMSEQ = this.TXT01_BPMSEQ.GetValue().ToString();
                fsBPMSABUN = this.CBH01_BPMSABUN.GetValue().ToString();

            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_85HAQ059",
                                        this.TXT01_BPMSTYYMM.GetValue().ToString(),
                                        this.CBH01_BPMJKCD.GetValue().ToString(),
                                        this.CBH01_BPMBUSEO.GetValue().ToString(),
                                        this.CBH01_BPMDPAC.GetValue().ToString(),
                                        this.TXT01_BPMHOBN.GetValue().ToString(),
                                        Get_Numeric(this.TXT01_BPMAVGOTTIME.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_BPMYCHACNT.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_BPMDYRATE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_BPMNYRATE.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_BPMADDAMOUNT.GetValue().ToString()),
                                        MTB01_BPMJODATE.GetValue().ToString().Replace("-", "").Trim(),
                                        MTB01_BPMREDATE.GetValue().ToString().Replace("-", "").Trim(),
                                        TYUserInfo.EmpNo,
                                        Get_Numeric(this.TXT01_BPMYEAR.GetValue().ToString()),
                                        Get_Numeric(this.TXT01_BPMSEQ.GetValue().ToString()),
                                        this.CBH01_BPMSABUN.GetValue().ToString()
                                        );
                this.DbConnector.ExecuteTranQuery();
            }            


            #region Description : 디테일 등록, 수정 및 삭제

            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            //기본사항

            // 등록
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_85NGP081", Get_Numeric(this.TXT01_BPMYEAR.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_BPMSEQ.GetValue().ToString()),
                                                                this.CBH01_BPMSABUN.GetValue().ToString(),
                                                                "1",
                                                                ds.Tables[0].Rows[i]["BPSPYGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["BPSPAYCODE"].ToString(),
                                                                this.TXT01_BPMSTYYMM.GetValue().ToString(),
                                                                this.TXT01_BPMHOBN.GetValue().ToString(),
                                                                Get_Numeric(ds.Tables[0].Rows[i]["BPSPYAMT"].ToString()),
                                                                TYUserInfo.EmpNo
                                                                ); // 수정
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            // 수정
            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_85HAX061", Get_Numeric(ds.Tables[1].Rows[i]["BPSPYAMT"].ToString()),
                                                                ds.Tables[1].Rows[i]["BPSHOBN"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                Get_Numeric(this.TXT01_BPMYEAR.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_BPMSEQ.GetValue().ToString()),
                                                                this.CBH01_BPMSABUN.GetValue().ToString(),
                                                                ds.Tables[1].Rows[i]["BPSPYGUBN"].ToString(),
                                                                ds.Tables[1].Rows[i]["BPSPAYCODE"].ToString(),
                                                                this.TXT01_BPMSTYYMM.GetValue().ToString(),
                                                                ds.Tables[1].Rows[i]["BPSCKGUBN"].ToString()
                                                                ); // 수정
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            // 삭제
            if (ds.Tables[2].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_85HAV060", Get_Numeric(this.TXT01_BPMYEAR.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_BPMSEQ.GetValue().ToString()),
                                                                this.CBH01_BPMSABUN.GetValue().ToString(),
                                                                ds.Tables[2].Rows[i]["BPSPYGUBN"].ToString(),
                                                                ds.Tables[2].Rows[i]["BPSPAYCODE"].ToString(),
                                                                this.TXT01_BPMSTYYMM.GetValue().ToString(),
                                                                ds.Tables[2].Rows[i]["BPSCKGUBN"].ToString()
                                                                ); // 삭제
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            //호보인상
            // 등록
            if (ds.Tables[3].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[3].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_85NGP081", Get_Numeric(this.TXT01_BPMYEAR.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_BPMSEQ.GetValue().ToString()),
                                                                this.CBH01_BPMSABUN.GetValue().ToString(),
                                                                "3",
                                                                ds.Tables[3].Rows[i]["BPSPYGUBN"].ToString(),
                                                                ds.Tables[3].Rows[i]["BPSPAYCODE"].ToString(),
                                                                this.TXT01_BPMSTYYMM.GetValue().ToString(),
                                                                this.TXT01_BPMHOBN.GetValue().ToString(),
                                                                Get_Numeric(ds.Tables[3].Rows[i]["BPSPYAMT"].ToString()),
                                                                TYUserInfo.EmpNo
                                                                ); // 수정
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            // 수정
            if (ds.Tables[4].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[4].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_85HAX061", Get_Numeric(ds.Tables[4].Rows[i]["BPSPYAMT"].ToString()),
                                                                ds.Tables[4].Rows[i]["BPSHOBN"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                Get_Numeric(this.TXT01_BPMYEAR.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_BPMSEQ.GetValue().ToString()),
                                                                this.CBH01_BPMSABUN.GetValue().ToString(),
                                                                ds.Tables[4].Rows[i]["BPSPYGUBN"].ToString(),
                                                                ds.Tables[4].Rows[i]["BPSPAYCODE"].ToString(),
                                                                this.TXT01_BPMSTYYMM.GetValue().ToString(),
                                                                ds.Tables[4].Rows[i]["BPSCKGUBN"].ToString()
                                                                ); // 수정
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            // 삭제
            if (ds.Tables[5].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[5].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_85HAV060", Get_Numeric(this.TXT01_BPMYEAR.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_BPMSEQ.GetValue().ToString()),
                                                                this.CBH01_BPMSABUN.GetValue().ToString(),
                                                                ds.Tables[5].Rows[i]["BPSPYGUBN"].ToString(),
                                                                ds.Tables[5].Rows[i]["BPSPAYCODE"].ToString(),
                                                                this.TXT01_BPMSTYYMM.GetValue().ToString(),
                                                                ds.Tables[5].Rows[i]["BPSCKGUBN"].ToString()
                                                                ); // 삭제
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            //인상율적용
            // 등록
            if (ds.Tables[6].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[6].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_85NGP081", Get_Numeric(this.TXT01_BPMYEAR.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_BPMSEQ.GetValue().ToString()),
                                                                this.CBH01_BPMSABUN.GetValue().ToString(),
                                                                "4",
                                                                ds.Tables[6].Rows[i]["BPSPYGUBN"].ToString(),
                                                                ds.Tables[6].Rows[i]["BPSPAYCODE"].ToString(),
                                                                this.TXT01_BPMSTYYMM.GetValue().ToString(),
                                                                this.TXT01_BPMHOBN.GetValue().ToString(),
                                                                Get_Numeric(ds.Tables[6].Rows[i]["BPSPYAMT"].ToString()),
                                                                TYUserInfo.EmpNo
                                                                ); // 수정
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            // 수정
            if (ds.Tables[7].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[7].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_85HAX061", Get_Numeric(ds.Tables[7].Rows[i]["BPSPYAMT"].ToString()),
                                                                ds.Tables[7].Rows[i]["BPSHOBN"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                Get_Numeric(this.TXT01_BPMYEAR.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_BPMSEQ.GetValue().ToString()),
                                                                this.CBH01_BPMSABUN.GetValue().ToString(),
                                                                ds.Tables[7].Rows[i]["BPSPYGUBN"].ToString(),
                                                                ds.Tables[7].Rows[i]["BPSPAYCODE"].ToString(),
                                                                this.TXT01_BPMSTYYMM.GetValue().ToString(),
                                                                ds.Tables[7].Rows[i]["BPSCKGUBN"].ToString()
                                                                ); // 수정
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            // 삭제
            if (ds.Tables[8].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[8].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_85HAV060", Get_Numeric(this.TXT01_BPMYEAR.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_BPMSEQ.GetValue().ToString()),
                                                                this.CBH01_BPMSABUN.GetValue().ToString(),
                                                                ds.Tables[8].Rows[i]["BPSPYGUBN"].ToString(),
                                                                ds.Tables[8].Rows[i]["BPSPAYCODE"].ToString(),
                                                                this.TXT01_BPMSTYYMM.GetValue().ToString(),
                                                                ds.Tables[8].Rows[i]["BPSCKGUBN"].ToString()
                                                                ); // 삭제
                }
                this.DbConnector.ExecuteTranQueryList();
            }
            #endregion

            this.ShowMessage("TY_M_GB_23NAD873");

            // 확인
            UP_Run(fsBPMYEAR, fsBPMSEQ, fsBPMSABUN);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            // 기본사항
            ds.Tables.Add(this.FPS91_TY_S_HR_85HAD056.GetDataSourceInclude(TSpread.TActionType.New, "BPSPYGUBN", "BPSPAYCODE", "BPSPYAMT","BPSCKGUBN","BPSHOBN"));
            ds.Tables.Add(this.FPS91_TY_S_HR_85HAD056.GetDataSourceInclude(TSpread.TActionType.Update, "BPSPYGUBN", "BPSPAYCODE", "BPSPYAMT", "BPSCKGUBN", "BPSHOBN"));
            ds.Tables.Add(this.FPS91_TY_S_HR_85HAD056.GetDataSourceInclude(TSpread.TActionType.Remove, "BPSPYGUBN", "BPSPAYCODE", "BPSPYAMT", "BPSCKGUBN", "BPSHOBN"));
            // 호봉인상
            ds.Tables.Add(this.FPS92_TY_S_HR_8BE8X124.GetDataSourceInclude(TSpread.TActionType.New, "BPSPYGUBN", "BPSPAYCODE", "BPSPYAMT", "BPSCKGUBN", "BPSHOBN"));
            ds.Tables.Add(this.FPS92_TY_S_HR_8BE8X124.GetDataSourceInclude(TSpread.TActionType.Update, "BPSPYGUBN", "BPSPAYCODE", "BPSPYAMT", "BPSCKGUBN", "BPSHOBN"));
            ds.Tables.Add(this.FPS92_TY_S_HR_8BE8X124.GetDataSourceInclude(TSpread.TActionType.Remove, "BPSPYGUBN", "BPSPAYCODE", "BPSPYAMT", "BPSCKGUBN", "BPSHOBN"));

            //인상율적용
            ds.Tables.Add(this.FPS93_TY_S_HR_8BE8X124.GetDataSourceInclude(TSpread.TActionType.New, "BPSPYGUBN", "BPSPAYCODE", "BPSPYAMT", "BPSCKGUBN", "BPSHOBN"));
            ds.Tables.Add(this.FPS93_TY_S_HR_8BE8X124.GetDataSourceInclude(TSpread.TActionType.Update, "BPSPYGUBN", "BPSPAYCODE", "BPSPYAMT", "BPSCKGUBN", "BPSHOBN"));
            ds.Tables.Add(this.FPS93_TY_S_HR_8BE8X124.GetDataSourceInclude(TSpread.TActionType.Remove, "BPSPYGUBN", "BPSPAYCODE", "BPSPYAMT", "BPSCKGUBN", "BPSHOBN"));


            if (string.IsNullOrEmpty(this.fsBPMYEAR) && string.IsNullOrEmpty(this.fsBPMSEQ) && string.IsNullOrEmpty(this.fsBPMSABUN))
            {
                this.TXT01_BPMSEQ.SetValue(UP_getSEQ());
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                int i = 0;

                // 등록시 동일한 급여구분 및 급여명으로 내역 체크
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_HR_85NHV084", Get_Numeric(this.TXT01_BPMYEAR.GetValue().ToString()),
                                                                Get_Numeric(this.TXT01_BPMSEQ.GetValue().ToString()),
                                                                this.CBH01_BPMSABUN.GetValue().ToString(),
                                                                ds.Tables[0].Rows[i]["BPSPYGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["BPSPAYCODE"].ToString()
                                                                );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowMessage("TY_M_AC_3219C986");
                        e.Successed = false;
                        return;
                    }
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_85SDA117",
                                        this.TXT01_BPMYEAR.GetValue().ToString()
                                        );
                int iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
                if (iCnt > 0)
                {
                    this.ShowCustomMessage("인건비 예산 자료가 이미 사업계획으로 전송되었습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            //신규입사자경우 입사년월 필수 체크
            if (CBH01_BPMSABUN.GetValue().ToString() == "" && MTB01_BPMJODATE.GetValue().ToString().Replace("-", "").Trim().Length < 6 )
            {
                this.SetFocus(MTB01_BPMJODATE);
                this.ShowCustomMessage("입사년월을 입력하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (CBH01_BPMSABUN.GetValue().ToString() == "" && MTB01_BPMJODATE.GetValue().ToString().Replace("-", "").Trim().Length > 0)
            {
                if (MTB01_BPMJODATE.GetValue().ToString().Replace("-", "").Trim().Substring(0, 4) != TXT01_BPMYEAR.GetValue().ToString())
                {
                    this.SetFocus(MTB01_BPMJODATE);
                    this.ShowCustomMessage("입사년도는 사업년도와 같아야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            if (CBH01_BPMSABUN.GetValue().ToString() != "" && MTB01_BPMREDATE.GetValue().ToString().Replace("-", "").Trim().Length > 0)
            {
                    if (MTB01_BPMREDATE.GetValue().ToString().Replace("-", "").Trim().Substring(0, 4) != TXT01_BPMYEAR.GetValue().ToString())
                    {
                        this.SetFocus(MTB01_BPMJODATE);
                        this.ShowCustomMessage("퇴사년도는 사업년도와 같아야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
            }

            //기존직원경우 입사년월 제외
            if (CBH01_BPMSABUN.GetValue().ToString() != "" && MTB01_BPMJODATE.GetValue().ToString().Replace("-", "").Trim() != "")
            {
                this.SetFocus(MTB01_BPMJODATE);
                this.ShowCustomMessage("입사년월을 삭제하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            //신규입사자경우 퇴사년월을 입력할수 없다
            if (CBH01_BPMSABUN.GetValue().ToString() == "" && MTB01_BPMREDATE.GetValue().ToString().Replace("-", "").Trim() != "")
            {                
                this.SetFocus(MTB01_BPMREDATE);
                this.ShowCustomMessage("퇴사년월을 삭제하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            //기존직원경우 퇴사일자 체크
            if (CBH01_BPMSABUN.GetValue().ToString() != "" && ( MTB01_BPMREDATE.GetValue().ToString().Replace("-", "").Trim().Length > 0  && MTB01_BPMREDATE.GetValue().ToString().Replace("-", "").Trim().Length < 6 ) )
            {
                this.SetFocus(MTB01_BPMREDATE);
                this.ShowCustomMessage("퇴사년월 확인하세요!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;

        }
        #endregion

        #region Description : 데이터 확인
        private void UP_Run(string sBPMYEAR, string sBPMSEQ, string sBPMSABUN)
        {
            this.FPS91_TY_S_HR_85HAD056.Initialize();
            this.FPS92_TY_S_HR_8BE8X124.Initialize();
            this.FPS93_TY_S_HR_8BE8X124.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85HA8050", sBPMYEAR, sBPMSEQ, sBPMSABUN);
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");
            }

            // 기본사항내역
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85HAA051", sBPMYEAR, sBPMSEQ, sBPMSABUN, "1");                        
            this.FPS91_TY_S_HR_85HAD056.SetValue(this.DbConnector.ExecuteDataTable());

            //당해인상율
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85HAA051", sBPMYEAR, sBPMSEQ, sBPMSABUN, "2");
            this.FPS94_TY_S_HR_8BE8X124.SetValue(this.DbConnector.ExecuteDataTable());

            //호봉인상
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85HAA051", sBPMYEAR, sBPMSEQ, sBPMSABUN, "3");
            this.FPS92_TY_S_HR_8BE8X124.SetValue(this.DbConnector.ExecuteDataTable());
            //인상율적용
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85HAA051", sBPMYEAR, sBPMSEQ, sBPMSABUN, "4");
            this.FPS93_TY_S_HR_8BE8X124.SetValue(this.DbConnector.ExecuteDataTable());


        }
        #endregion

        #region Description : 순번 가져오기
        private string UP_getSEQ()
        {
            string sSEQ = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85NH2083", Get_Numeric(this.TXT01_BPMYEAR.GetValue().ToString()));

            sSEQ = this.DbConnector.ExecuteScalar().ToString();

            return sSEQ;
        }
        #endregion
    }
}
