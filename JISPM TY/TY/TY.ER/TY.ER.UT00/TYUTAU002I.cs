using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 화물비중관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.10.26 20:16
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_6AQJZ582 : 화물비중관리 조회
    ///  TY_P_UT_6AQK0583 : 화물비중관리 등록
    ///  TY_P_UT_6AQK1588 : 화물비중관리 수정
    ///  TY_P_UT_6AQK2589 : 화물비중관리 삭제
    ///  TY_P_UT_6AQK4584 : 화물비중관리 (자동화 테이블 수정)
    ///  TY_P_UT_6AQK5590 : 화물비중관리 (지시파일 등록)
    ///  TY_P_UT_6AQK7585 : 화물비중관리 (지시순번 조회)
    ///  TY_P_UT_6AQK8586 : 화물비중관리 (지시순번 업데이트)
    ///  TY_P_UT_6AQK9587 : 화물비중관리 (지시순번 등록)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_6AQKB592 : 화물비중관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    ///  TY_M_GB_43C9G671 : 삭제 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  HMCODE : 화물
    /// </summary>
    public partial class TYUTAU002I : TYBase
    {
        #region Description : 페이지 로드
        public TYUTAU002I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_6AQKB592, "HMCODE", "HMNAME", "HMCODE");
        }

        private void TYUTAU002I_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_UT_6AQKB592.Initialize();
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_6AQKB592, "HMCODE");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.CBH01_HMCODE.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_6AQKB592.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_6AQJZ582", this.CBH01_HMCODE.GetValue().ToString());

            this.FPS91_TY_S_UT_6AQKB592.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            string sJSSEQ = string.Empty;
            string sDATE = System.DateTime.Now.ToString("yyyyMMdd");

            string sGUBUN = string.Empty;
            string sHWAMUL = string.Empty;

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_6AQK2589", dt.Rows[i]["HMCODE"].ToString());
                    this.DbConnector.ExecuteNonQuery();

                    // 지시 순번 조회
                    sJSSEQ = UP_Get_SEQ(sDATE);

                    // 지시 파일 등록
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_6AQK5590", sDATE,
                                                                sJSSEQ,
                                                                "6",
                                                                dt.Rows[i]["HMCODE"].ToString(),
                                                                dt.Rows[i]["HMNAME"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                dt.Rows[i]["HMVCF"].ToString(),
                                                                dt.Rows[i]["HMWCF"].ToString()
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }
            }

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_6AQKB592.GetDataSourceInclude(TSpread.TActionType.Remove, "HMCODE", "HMNAME", "HMVCF", "HMWCF", "HMTEMPH", "HMTEMPL");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;

        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            string sJSSEQ = string.Empty;
            string sDATE = System.DateTime.Now.ToString("yyyyMMdd");
            
            string sHWAMUL = string.Empty;

            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    // 신규등록
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        sHWAMUL = ds.Tables[0].Rows[i]["HMCODE"].ToString().ToUpper();

                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_UT_6AQK0583", ds.Tables[0].Rows[i]["HMCODE"].ToString(),
                                                                    ds.Tables[0].Rows[i]["HMNAME"].ToString(),
                                                                    ds.Tables[0].Rows[i]["HMVCF"].ToString(),
                                                                    ds.Tables[0].Rows[i]["HMWCF"].ToString(),
                                                                    ds.Tables[0].Rows[i]["HMTEMPH"].ToString(),
                                                                    ds.Tables[0].Rows[i]["HMTEMPL"].ToString(),
                                                                    "A",
                                                                    TYUserInfo.EmpNo
                                                                    );
                        this.DbConnector.ExecuteTranQuery();

                        // 풀어야 함
                        #region Description : 풀어야 함

                        // 자동화 테이블 UPDATE
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_UT_6AQK4584", ds.Tables[0].Rows[i]["HMTEMPH"].ToString(),
                                                                    ds.Tables[0].Rows[i]["HMTEMPL"].ToString(),
                                                                    ds.Tables[0].Rows[i]["HMCODE"].ToString()
                                                                    );
                        this.DbConnector.ExecuteTranQuery();

                        // 지시 순번 조회
                        sJSSEQ = UP_Get_SEQ(sDATE);

                        // 지시 파일 등록
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_UT_6AQK5590", sDATE,
                                                                    sJSSEQ,
                                                                    "6",
                                                                    ds.Tables[0].Rows[i]["HMCODE"].ToString(),
                                                                    ds.Tables[0].Rows[i]["HMNAME"].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    ds.Tables[0].Rows[i]["HMVCF"].ToString(),
                                                                    ds.Tables[0].Rows[i]["HMWCF"].ToString()
                                                                    );
                        this.DbConnector.ExecuteTranQuery();

                        //// 오라클 자동화 업데이트
                        UP_Oracle_Update(sDATE,
                                         sJSSEQ,
                                         ds.Tables[0].Rows[i]["HMVCF"].ToString(),
                                         ds.Tables[0].Rows[i]["HMWCF"].ToString(),
                                         ds.Tables[0].Rows[i]["HMTEMPH"].ToString(),
                                         ds.Tables[0].Rows[i]["HMTEMPL"].ToString()
                                         );

                        #endregion
                    }
                }

                if (ds.Tables[1].Rows.Count > 0)
                {   
                    // 수정
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        sHWAMUL = ds.Tables[1].Rows[i]["HMCODE"].ToString().ToUpper();

                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_UT_6AQK1588", ds.Tables[1].Rows[i]["HMNAME"].ToString(),
                                                                    ds.Tables[1].Rows[i]["HMVCF"].ToString(),
                                                                    ds.Tables[1].Rows[i]["HMWCF"].ToString(),
                                                                    ds.Tables[1].Rows[i]["HMTEMPH"].ToString(),
                                                                    ds.Tables[1].Rows[i]["HMTEMPL"].ToString(),
                                                                    "C",
                                                                    TYUserInfo.EmpNo,
                                                                    ds.Tables[1].Rows[i]["HMCODE"].ToString()
                                                                    );
                        this.DbConnector.ExecuteTranQuery();

                        // 풀어야 함
                        #region Description : 풀어야 함

                        // 자동화 테이블 UPDATE
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_UT_6AQK4584", ds.Tables[1].Rows[i]["HMTEMPH"].ToString(),
                                                                    ds.Tables[1].Rows[i]["HMTEMPL"].ToString(),
                                                                    ds.Tables[1].Rows[i]["HMCODE"].ToString()
                                                                    );
                        this.DbConnector.ExecuteTranQuery();

                        // 지시 순번 조회
                        sJSSEQ = UP_Get_SEQ(sDATE);

                        // 지시 파일 등록
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_UT_6AQK5590", sDATE,
                                                                    sJSSEQ,
                                                                    "6",
                                                                    ds.Tables[1].Rows[i]["HMCODE"].ToString(),
                                                                    ds.Tables[1].Rows[i]["HMNAME"].ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    ds.Tables[1].Rows[i]["HMVCF"].ToString(),
                                                                    ds.Tables[1].Rows[i]["HMWCF"].ToString()
                                                                    );
                        this.DbConnector.ExecuteTranQuery();

                        // 오라클 자동화 업데이트
                        UP_Oracle_Update(sDATE,
                                        sJSSEQ,
                                         ds.Tables[1].Rows[i]["HMVCF"].ToString(),
                                         ds.Tables[1].Rows[i]["HMWCF"].ToString(),
                                         ds.Tables[1].Rows[i]["HMTEMPH"].ToString(),
                                         ds.Tables[1].Rows[i]["HMTEMPL"].ToString()
                                         );

                        #endregion
                    }
                }

                this.BTN61_INQ_Click(null, null);

                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch
            {   
                this.ShowCustomMessage("[" + sHWAMUL + "] 항목 저장에 실패하였습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_UT_6AQKB592.GetDataSourceInclude(TSpread.TActionType.New, "HMCODE", "HMNAME", "HMVCF", "HMWCF", "HMTEMPH", "HMTEMPL"));

            ds.Tables.Add(this.FPS91_TY_S_UT_6AQKB592.GetDataSourceInclude(TSpread.TActionType.Update, "HMCODE", "HMNAME", "HMVCF", "HMWCF", "HMTEMPH", "HMTEMPL"));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_6B1DN634", ds.Tables[0].Rows[i]["HMCODE"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("[" + ds.Tables[0].Rows[i]["HMCODE"].ToString() + "] 이미 등록된 자료입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 지시 순번 조회
        private string UP_Get_SEQ(string sJSDATE)
        {
            int iSEQ;
            DataTable dt = new DataTable();

            try
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_6AQK7585", sJSDATE);

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    iSEQ = Convert.ToInt32(dt.Rows[0]["JSSEQ"].ToString()) + 1;

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_6AQK8586", iSEQ, sJSDATE);

                    this.DbConnector.ExecuteTranQuery();
                }
                else
                {
                    iSEQ = 1;

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_6AQK9587", sJSDATE);

                    this.DbConnector.ExecuteTranQuery();
                }

                return iSEQ.ToString();
            }
            catch
            {
                return "0";
            }


            
        }
        #endregion

        #region Description : 오라클 자동화 파일 업데이트
        private bool UP_Oracle_Update(string sJISIIL, string sJSSEQ, string sHMVCF,
                                      string sHMWCF, string sHMTEMPH, string sHMTEMPL)
        {
            bool bRtn = false;
            string sJISIHT = string.Empty;

            DataTable dt = new DataTable();

            try
            {
                // 지시 파일 확인
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_6ASBC597", sJISIIL, sJSSEQ);

                dt = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["JISIHT"].ToString() == "6")
                    {
                        // 오라클 자동화 업데이트
                        UP_ChangeDensity(dt.Rows[i]["JISIHM"].ToString(),
                                         dt.Rows[i]["JISIHMNM"].ToString(),
                                         sHMVCF,
                                         sHMWCF,
                                         dt.Rows[i]["JISITK"].ToString(),
                                         dt.Rows[i]["JISIHJ"].ToString(),
                                         dt.Rows[i]["JISIHJNM"].ToString(),
                                         sHMTEMPH,
                                         sHMTEMPL);
                    }
                }

                return bRtn;
            }
            catch
            {
                return bRtn;
            }
        }
        #endregion

        #region Description : 비중 업데이트
        private bool UP_ChangeDensity(string sJISIHM,   string sJISIHMNM, string sJIVCF,
									  string sJIWCF,    string sJISITK,   string sJISIHJ,
									  string sJISIHJNM, string sHMTEMPH,  string sHMTEMPL)
        {
            bool bRtn = false;
            string sHWAMUL = string.Empty;

            try
            {
                DataTable dt = new DataTable();

                // 오라클 HMBJ 확인

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_6ASD1598", sJISIHM);

                dt = this.DbConnector.ExecuteDataTable();

                if (sJISIHM == "A27")
                {
                    sHWAMUL = "무수초산";
                }
                else
                {
                    sHWAMUL = sJISIHMNM;
                }

                if (dt.Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_6ASED600", sHWAMUL,
                                                                sJIWCF,
                                                                sJIVCF,
                                                                sHMTEMPL,
                                                                sHMTEMPH,
                                                                sJISIHM
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }
                else
                {   
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_6ASEB599", sJISIHM,
                                                                sHWAMUL,
                                                                sJIWCF,
                                                                sJIVCF,
                                                                sHMTEMPL,
                                                                sHMTEMPH
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }

                // TKST 조회
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_6ASEF601", sJISIHM);

                dt = this.DbConnector.ExecuteDataTable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (sHWAMUL.Length > 14)
                    {
                        sHWAMUL = sHWAMUL.Substring(0, 15);
                    }

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_6ASEG602", sJISIHM,
                                                                sHWAMUL,
                                                                sJIWCF,
                                                                sJIVCF,
                                                                sHMTEMPL,
                                                                sHMTEMPH
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }

                bRtn = true;

                return bRtn;
            }
            catch
            {
                return bRtn;
            }
        }
        #endregion
    }
}
