using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 구매마감 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.01.04 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_317AI511 : 구매마감 - 미마감 조회(입고)
    ///  TY_P_MR_317AJ512 : 구매마감 - 마감 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_317AJ513 : 구매마감 - 미마감 조회(입고)
    ///  TY_S_MR_317AL514 : 구매마감 - 마감 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  MAGAM_CANCEL : 마감취소
    ///  MAGAM_OK : 마감생성
    ///  MET1020 : 매입처
    ///  GGUBUN : 구분
    ///  MET1010 : 사업부
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    ///  MET1000 : 마감일자
    /// </summary>
    public partial class TYMRMA001S : TYBase
    {
        #region Description : 페이지 로드
        public TYMRMA001S()
        {
            InitializeComponent();
        }

        private void TYMRMA001S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_MAGAM_OK.ProcessCheck += new TButton.CheckHandler(BTN61_MAGAM_OK_ProcessCheck);
            this.BTN61_MAGAM_CANCEL.ProcessCheck += new TButton.CheckHandler(BTN61_MAGAM_CANCEL_ProcessCheck);
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            this.FPS91_TY_S_MR_317AJ513.Visible = true;
            this.FPS91_TY_S_MR_317AL514.Visible = false;

            this.BTN61_MAGAM_OK.Visible = true;
            this.BTN61_MAGAM_CANCEL.Visible = false;
            this.BTN61_SAV.Visible = false;

            SetStartingFocus(this.CBO01_MET1010);
        }
        #endregion

        #region Description : 조회
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sKBBUSEO = string.Empty;

            DataTable dt = new DataTable();

            // 부서코드 가져오기
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_2BEBB293",
                DateTime.Now.ToString("yyyyMMdd"),
                TYUserInfo.EmpNo.ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                // 부서코드
                sKBBUSEO = dt.Rows[0]["KBBUSEO"].ToString();
            }

            if (TYUserInfo.EmpNo.ToString() == "0150-M" ||
                TYUserInfo.EmpNo.ToString() == "0287-M" ||
                TYUserInfo.EmpNo.ToString() == "0310-M" ||
                TYUserInfo.EmpNo.ToString() == "0311-M" ||
                TYUserInfo.EmpNo.ToString() == "0403-M" ||
                TYUserInfo.EmpNo.ToString() == "0425-M")
            {
                sKBBUSEO = "";
            }

            this.DbConnector.CommandClear();

            if (this.CBO01_GGUBUN.GetValue().ToString() == "1") // 미마감
            {
                this.BTN61_MAGAM_OK.Visible = true;
                this.BTN61_MAGAM_CANCEL.Visible = false;

                this.FPS91_TY_S_MR_317AJ513.Visible = true;
                this.FPS91_TY_S_MR_317AL514.Visible = false;

                this.DbConnector.Attach
                    (
                    "TY_P_MR_317AI511",
                    this.DTP01_GSTDATE.GetValue(),
                    this.DTP01_GEDDATE.GetValue(),
                    this.CBO01_MET1010.GetValue().ToString(),
                    this.CBH01_MET1020.GetValue().ToString(),
                    sKBBUSEO.ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_MR_317AJ513.SetValue(dt);

                this.BTN61_SAV.Visible = true;
            }
            else if (this.CBO01_GGUBUN.GetValue().ToString() == "2") // 마감
            {
                this.BTN61_MAGAM_OK.Visible = false;
                this.BTN61_MAGAM_CANCEL.Visible = true;

                this.FPS91_TY_S_MR_317AJ513.Visible = false;
                this.FPS91_TY_S_MR_317AL514.Visible = true;

                this.DbConnector.Attach
                (
                    "TY_P_MR_317AJ512",
                    this.DTP01_GSTDATE.GetValue(),
                    this.DTP01_GEDDATE.GetValue(),
                    this.CBO01_MET1010.GetValue().ToString(),
                    this.CBH01_MET1020.GetValue().ToString(),
                    sKBBUSEO.ToString()
                );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_MR_317AL514.SetValue(dt);

                this.BTN61_SAV.Visible = false;
            }
        }
        #endregion

        #region Description : 마감 생성 버튼
        private void BTN61_MAGAM_OK_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;

            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                // 구매마감 생성 - 월말마감집계 (입고감소금액)
                this.DbConnector.Attach("TY_P_MR_BBQFM801", ds.Tables[0].Rows[i]["RRM1100"].ToString(),
                                                            this.DTP01_MET1000.GetValue().ToString(),
                                                            ds.Tables[0].Rows[i]["RRN1100"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRNUM"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            "A",
                                                            sOUTMSG.ToString());
                //// 구매마감 생성 - 월말마감집계
                //this.DbConnector.Attach("TY_P_MR_31761529", ds.Tables[0].Rows[i]["RRM1100"].ToString(),
                //                                            this.DTP01_MET1000.GetValue().ToString(),
                //                                            ds.Tables[0].Rows[i]["RRN1100"].ToString(),
                //                                            ds.Tables[0].Rows[i]["RRNUM"].ToString(),
                //                                            TYUserInfo.EmpNo,
                //                                            "A",
                //                                            sOUTMSG.ToString());

                sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

                if (sOUTMSG.Substring(0, 2) != "OK")
                {
                    return;
                }
            }

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_MR_3174G518");
        }
        #endregion

        #region Description : 마감 취소 버튼
        private void BTN61_MAGAM_CANCEL_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 구매마감 취소 - 월말마감집계
                this.DbConnector.Attach("TY_P_MR_3175R527", ds.Tables[0].Rows[i]["MET1000"].ToString(),
                                                            ds.Tables[0].Rows[i]["MET1020"].ToString());

                // 구매마감 취소 - 월말마감내역화일
                this.DbConnector.Attach("TY_P_MR_3175S528", ds.Tables[0].Rows[i]["MET1000"].ToString(),
                                                            ds.Tables[0].Rows[i]["MET1020"].ToString(),
                                                            ds.Tables[0].Rows[i]["MET1040"].ToString());
            }

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_MR_3174G520");
        }
        #endregion

        #region Description : 마감 생성 ProcessCheck 이벤트
        private void BTN61_MAGAM_OK_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;
            int j = 0;

            int iCnt = 0;

            DataSet   ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dz = new DataTable();

            // 생성
            ds.Tables.Add(this.FPS91_TY_S_MR_317AJ513.GetDataSourceInclude(TSpread.TActionType.Select, "RRNUM", "RRN1100", "RRM1100", "RRM1500")); // 입고번호, 거래처, 입고일자, 월말구분

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    iCnt = 0;

                    if (ds.Tables[0].Rows[i]["RRM1500"].ToString() == "1") // 월말
                    {
                        // 구매마감 생성체크 - 월말마감집계
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_3174V523",
                            this.DTP01_MET1000.GetValue().ToString(),
                            ds.Tables[0].Rows[i]["RRN1100"].ToString()
                            );

                        iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                        if (iCnt > 0)
                        {
                            this.ShowMessage("TY_M_MR_3174H521");
                            e.Successed = false;
                            return;
                        }

                        iCnt = 0;

                        // 구매마감 생성체크 - 월말마감내역화일
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_3174Z524",
                            this.DTP01_MET1000.GetValue().ToString(),
                            ds.Tables[0].Rows[i]["RRN1100"].ToString(),
                            ds.Tables[0].Rows[i]["RRNUM"].ToString()
                            );

                        iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                        if (iCnt > 0)
                        {
                            this.ShowMessage("TY_M_MR_3174H521");
                            e.Successed = false;
                            return;
                        }
                    }

                    // 구매마감 예산 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_659H5845",
                        ds.Tables[0].Rows[i]["RRNUM"].ToString().Substring(0,1),
                        ds.Tables[0].Rows[i]["RRNUM"].ToString().Substring(1,1),
                        ds.Tables[0].Rows[i]["RRNUM"].ToString().Substring(2,6),
                        ds.Tables[0].Rows[i]["RRNUM"].ToString().Substring(8,4),
                        ds.Tables[0].Rows[i]["RRN1100"].ToString()
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        for (j = 0; j < dt.Rows.Count; j++)
                        {
                            if (dt.Rows[j]["RRN1070"].ToString() == "1") // 투자예산
                            {
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach
                                    (
                                    "TY_P_MR_659HE846",
                                    dt.Rows[j]["RRO2040"].ToString(), /* 적용년 */
                                    dt.Rows[j]["RRN1040"].ToString(), /* 부서   */
                                    dt.Rows[j]["RRN1080"].ToString(), /* 계정   */
                                    dt.Rows[j]["RRN1091"].ToString(), /* 순번   */
                                    dt.Rows[j]["RRO2050"].ToString()  /* 예산월 */
                                    );

                                dz = this.DbConnector.ExecuteDataTable();

                                if (dz.Rows.Count <= 0)
                                {
                                    this.ShowMessage("TY_M_MR_659HH848");
                                    e.Successed = false;
                                    return;
                                }
                            }
                            else if (dt.Rows[j]["RRN1070"].ToString() == "2") // 소모성 예산
                            {
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach
                                    (
                                    "TY_P_MR_659HJ849",
                                    dt.Rows[j]["RRO2040"].ToString(), /* 적용년 */
                                    dt.Rows[j]["RRN1040"].ToString(), /* 부서   */
                                    dt.Rows[j]["RRN1080"].ToString(), /* 계정   */
                                    dt.Rows[j]["RRN1090"].ToString(), /* 비품코드 */
                                    dt.Rows[j]["RRN1091"].ToString(), /* 순번   */
                                    dt.Rows[j]["RRO2050"].ToString()  /* 예산월 */
                                    );

                                dz = this.DbConnector.ExecuteDataTable();

                                if (dz.Rows.Count <= 0)
                                {
                                    this.ShowMessage("TY_M_MR_659HH848");
                                    e.Successed = false;
                                    return;
                                }
                            }
                            else if (dt.Rows[j]["RRN1070"].ToString() == "3") // 기타 예산
                            {
                                this.DbConnector.CommandClear();
                                this.DbConnector.Attach
                                    (
                                    "TY_P_MR_659HL850",
                                    dt.Rows[j]["RRO2040"].ToString(), /* 적용년 */
                                    dt.Rows[j]["RRO2050"].ToString(), /* 예산월 */
                                    dt.Rows[j]["RRN1040"].ToString(), /* 부서   */
                                    dt.Rows[j]["RRN1080"].ToString()  /* 계정   */
                                    );

                                dz = this.DbConnector.ExecuteDataTable();

                                if (dz.Rows.Count <= 0)
                                {
                                    this.ShowMessage("TY_M_MR_659HH848");
                                    e.Successed = false;
                                    return;
                                }
                            }
                        }
                    }
                    // 발주 내역사항 중 한 거래처에 노무비구분이 다른자료 유무 체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_A13DP656",
                        ds.Tables[0].Rows[i]["RRNUM"].ToString().Substring(0, 1),
                        ds.Tables[0].Rows[i]["RRNUM"].ToString().Substring(1, 1),
                        ds.Tables[0].Rows[i]["RRNUM"].ToString().Substring(2, 6),
                        ds.Tables[0].Rows[i]["RRNUM"].ToString().Substring(8, 4)
                        );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        this.ShowCustomMessage("동일 거래처에 노무비닷컴 구분이 다른 자료가 존재합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                        e.Successed = false;
                        return;
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_MR_3176V530");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_MR_3174F517"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 마감 취소 ProcessCheck 이벤트
        private void BTN61_MAGAM_CANCEL_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;
            int iCnt = 0;

            DataSet ds = new DataSet();

            // 취소
            ds.Tables.Add(this.FPS91_TY_S_MR_317AL514.GetDataSourceInclude(TSpread.TActionType.Select, "MET1000", "MET1020", "MET1040", "MET1030", "MET2030")); // 마감일자, 거래처, 입고번호, 월말구분, 전표번호

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    iCnt = 0;

                    if (ds.Tables[0].Rows[i]["MET2030"].ToString().ToString() != "")
                    {
                        // 구매마감 취소체크 - 월말마감집계
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_31754525",
                            ds.Tables[0].Rows[i]["MET1000"].ToString(),
                            ds.Tables[0].Rows[i]["MET1020"].ToString(),
                            ds.Tables[0].Rows[i]["MET2030"].ToString().Substring(0, 17)
                            );

                        iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                        if (iCnt > 0)
                        {
                            this.ShowMessage("TY_M_MR_3174H522");
                            e.Successed = false;
                            return;
                        }

                        iCnt = 0;

                        // 구매마감 취소체크 - 월말마감내역화일
                        this.DbConnector.CommandClear();
                        this.DbConnector.Attach
                            (
                            "TY_P_MR_31755526",
                            ds.Tables[0].Rows[i]["MET1000"].ToString(),
                            ds.Tables[0].Rows[i]["MET1020"].ToString(),
                            ds.Tables[0].Rows[i]["MET1040"].ToString(),
                            ds.Tables[0].Rows[i]["MET2030"].ToString().Substring(0, 17)
                            );

                        iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                        if (iCnt > 0)
                        {
                            this.ShowMessage("TY_M_MR_3174H522");
                            e.Successed = false;
                            return;
                        }
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_MR_3176V530");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_MR_3174G519"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int i = 0;

            int iCnt = 0;

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();            

            ds.Tables.Add(this.FPS91_TY_S_MR_317AJ513.GetDataSourceInclude(TSpread.TActionType.Select, "RRNUM", "RRN1100", "RRM1100", "RRM1500", "VNSANGHO")); // 입고번호, 거래처, 입고일자, 월말구분, 거래처명

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    // 구매마감 생성체크
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach
                        (
                        "TY_P_MR_BB8DW693",
                        ds.Tables[0].Rows[i]["RRN1100"].ToString(),
                        ds.Tables[0].Rows[i]["RRNUM"].ToString()
                        );

                    iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                    if (iCnt > 0)
                    {
                        this.ShowCustomMessage("마감 자료가 존재합니다.[" + ds.Tables[0].Rows[i]["VNSANGHO"].ToString() + "/" + ds.Tables[0].Rows[i]["RRNUM"].ToString() + "]", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
            }
            else
            {
                this.ShowMessage("TY_M_MR_3176V530");
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

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 구매입고 월말구분 업데이트
                this.DbConnector.Attach("TY_P_MR_BB8EN710", ds.Tables[0].Rows[i]["RRM1500"].ToString(),
                                                            ds.Tables[0].Rows[i]["RRNUM"].ToString().Substring(0, 1),
                                                            ds.Tables[0].Rows[i]["RRNUM"].ToString().Substring(1, 1),
                                                            ds.Tables[0].Rows[i]["RRNUM"].ToString().Substring(2, 6),
                                                            ds.Tables[0].Rows[i]["RRNUM"].ToString().Substring(8, 4));
            }

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion
    }
}