using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using System.Drawing;

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
    public partial class TYHRBS002I : TYBase
    {
        public DataSet ds = new DataSet();
        string fsBISYEAR  = string.Empty;
        string fsBISSEQ   = string.Empty;
        string fsBISSABUN = string.Empty;

        #region Description : 폼 로드
        public TYHRBS002I(string sBISYEAR, string sBISSEQ, string sBISSABUN)
        {
            InitializeComponent();

            fsBISYEAR  = sBISYEAR;
            fsBISSEQ   = sBISSEQ;
            fsBISSABUN = sBISSABUN;

            this.CBH01_BPMBUSEO.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            this.CBH01_BPMDPAC.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            //// 급여구분
            //this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_85TEM129, "BPSPYGUBN", "PYDESC1", "BPSPYGUBN");

            //// 급여명
            //this.SetSpreadCodeHelper(this.FPS91_TY_S_HR_85TEM129, "BPSPAYCODE", "PLDESC1", "BPSPAYCODE");
        }

        private void TYHRBS002I_Load(object sender, System.EventArgs e)
        {
            //// Key필드 수정모드시 잠금
            //this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_85TEM129, "BPSPYGUBN");
            //this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_85TEM129, "BPSPAYCODE");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            // 확인
            UP_Run(fsBISYEAR, fsBISSEQ, fsBISSABUN);

            SetStartingFocus(this.FPS91_TY_S_HR_85TEM129);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(this.fsBISYEAR) && string.IsNullOrEmpty(this.fsBISSEQ) && string.IsNullOrEmpty(this.fsBISSABUN))
            //{
            //    this.TXT01_BISSEQ.SetValue(UP_getSEQ());

            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach("TY_P_HR_85NGS082",
            //                            Get_Numeric(this.TXT01_BISYEAR.GetValue().ToString()),
            //                            Get_Numeric(this.TXT01_BISSEQ.GetValue().ToString()),
            //                            this.CBH01_BISSABUN.GetValue().ToString(),
            //                            this.TXT01_BISSTYYMM.GetValue().ToString(),
            //                            this.CBH01_BISJKCD.GetValue().ToString(),
            //                            this.CBH01_BISBUSEO.GetValue().ToString(),
            //                            this.CBH01_BISDPAC.GetValue().ToString(),
            //                            this.TXT01_BISHOBN.GetValue().ToString(),
            //                            Get_Numeric(this.TXT01_BISAVGOTTIME.GetValue().ToString()),
            //                            Get_Numeric(this.TXT01_BISYCHACNT.GetValue().ToString()),
            //                            Get_Numeric(this.TXT01_BISDYRATE.GetValue().ToString()),
            //                            Get_Numeric(this.TXT01_BISNYRATE.GetValue().ToString()),
            //                            TYUserInfo.EmpNo
            //                            );
            //    this.DbConnector.ExecuteTranQuery();
            //}
            //else
            //{
            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach("TY_P_HR_85HAQ059",
            //                            this.TXT01_BISSTYYMM.GetValue().ToString(),
            //                            this.CBH01_BISJKCD.GetValue().ToString(),
            //                            this.CBH01_BISBUSEO.GetValue().ToString(),
            //                            this.CBH01_BISDPAC.GetValue().ToString(),
            //                            this.TXT01_BISHOBN.GetValue().ToString(),
            //                            Get_Numeric(this.TXT01_BISAVGOTTIME.GetValue().ToString()),
            //                            Get_Numeric(this.TXT01_BISYCHACNT.GetValue().ToString()),
            //                            Get_Numeric(this.TXT01_BISDYRATE.GetValue().ToString()),
            //                            Get_Numeric(this.TXT01_BISNYRATE.GetValue().ToString()),
            //                            TYUserInfo.EmpNo,
            //                            Get_Numeric(this.TXT01_BISYEAR.GetValue().ToString()),
            //                            Get_Numeric(this.TXT01_BISSEQ.GetValue().ToString()),
            //                            this.CBH01_BISSABUN.GetValue().ToString()
            //                            );
            //    this.DbConnector.ExecuteTranQuery();
            //}


            #region Description : 디테일 등록, 수정 및 삭제

            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 수정
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_85TES130", Get_Numeric(ds.Tables[0].Rows[i]["BISPYAMT"].ToString()),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[0].Rows[i]["BISYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["BISSEQ"].ToString(),
                                                                ds.Tables[0].Rows[i]["BISSABUN"].ToString(),
                                                                ds.Tables[0].Rows[i]["BISPYGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["BISPYYYMM"].ToString(),
                                                                ds.Tables[0].Rows[i]["BISJIDATE"].ToString(),
                                                                ds.Tables[0].Rows[i]["BISPAYCODE"].ToString()
                                                                ); // 수정
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            // 삭제
            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_HR_85TET131", ds.Tables[1].Rows[i]["BISYEAR"].ToString(),
                                                                ds.Tables[1].Rows[i]["BISSEQ"].ToString(),
                                                                ds.Tables[1].Rows[i]["BISSABUN"].ToString(),
                                                                ds.Tables[1].Rows[i]["BISPYGUBN"].ToString(),
                                                                ds.Tables[1].Rows[i]["BISPYYYMM"].ToString(),
                                                                ds.Tables[1].Rows[i]["BISJIDATE"].ToString(),
                                                                ds.Tables[1].Rows[i]["BISPAYCODE"].ToString()
                                                                ); // 삭제
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            #endregion

            this.ShowMessage("TY_M_GB_23NAD873");

            // 확인
            UP_Run(fsBISYEAR, fsBISSEQ, fsBISSABUN);
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

            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_85TEM129.GetDataSourceInclude(TSpread.TActionType.Update, "BISYEAR", "BISSEQ", "BISSABUN", "BISPYGUBN", "BISPYYYMM", "BISJIDATE", "BISPAYCODE", "BISPYAMT", "PYDESC1"));
            // 스프레드에서 삭제 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_HR_85TEM129.GetDataSourceInclude(TSpread.TActionType.Remove, "BISYEAR", "BISSEQ", "BISSABUN", "BISPYGUBN", "BISPYYYMM", "BISJIDATE", "BISPAYCODE", "PYDESC1"));


            int i = 0;

            // 수정
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i]["PYDESC1"].ToString() == "[월 합계]")
                    {
                        this.ShowMessage("TY_M_GB_85TFH132");
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    if (ds.Tables[1].Rows[i]["PYDESC1"].ToString() == "[월 합계]")
                    {
                        this.ShowMessage("TY_M_GB_85TFH132");
                        e.Successed = false;
                        return;
                    }
                }
            }

            //if (string.IsNullOrEmpty(this.fsBISYEAR) && string.IsNullOrEmpty(this.fsBISSEQ) && string.IsNullOrEmpty(this.fsBISSABUN))
            //{
            //    this.TXT01_BISSEQ.SetValue(UP_getSEQ());
            //}

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    int i = 0;

            //    // 등록시 동일한 급여구분 및 급여명으로 내역 체크
            //    for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        this.DbConnector.CommandClear();
            //        this.DbConnector.Attach("TY_P_HR_85NHV084", Get_Numeric(this.TXT01_BISYEAR.GetValue().ToString()),
            //                                                    Get_Numeric(this.TXT01_BISSEQ.GetValue().ToString()),
            //                                                    this.CBH01_BISSABUN.GetValue().ToString(),
            //                                                    ds.Tables[0].Rows[i]["BPSPYGUBN"].ToString(),
            //                                                    ds.Tables[0].Rows[i]["BPSPAYCODE"].ToString()
            //                                                    );

            //        dt = this.DbConnector.ExecuteDataTable();

            //        if (dt.Rows.Count > 0)
            //        {
            //            this.ShowMessage("TY_M_AC_3219C986");
            //            e.Successed = false;
            //            return;
            //        }
            //    }
            //}

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
        private void UP_Run(string sBISYEAR, string sBISSEQ, string sBISSABUN)
        {
            this.FPS91_TY_S_HR_85TEM129.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85TEL128", sBISYEAR, sBISSEQ, sBISSABUN);
            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CurrentDataTableRowMapping(dt, "01");

                this.FPS91_TY_S_HR_85TEM129.SetValue(dt);

                for (int i = 0; i < this.FPS91_TY_S_HR_85TEM129.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_HR_85TEM129.GetValue(i, "PYDESC1").ToString() == "[월 합계]")
                    {
                        // 특정 칼럼 색깔 입히기
                        this.FPS91_TY_S_HR_85TEM129.ActiveSheet.Rows[i].BackColor = Color.SkyBlue;
                        this.FPS91_TY_S_HR_85TEM129.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);

                        this.FPS91_TY_S_HR_85TEM129.ActiveSheet.Rows[i].Locked = true;
                    }
                }
            }
        }
        #endregion

        #region Description : 순번 가져오기
        private string UP_getSEQ()
        {
            string sSEQ = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85NH2083", Get_Numeric(this.TXT01_BISYEAR.GetValue().ToString()));

            sSEQ = this.DbConnector.ExecuteScalar().ToString();

            return sSEQ;
        }
        #endregion
    }
}
