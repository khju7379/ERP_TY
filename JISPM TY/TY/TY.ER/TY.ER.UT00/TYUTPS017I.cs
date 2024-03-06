using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 선급자재 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.19 09:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_32J79125 : 선급자재 미생성 조회
    ///  TY_P_MR_32J7A126 : 선급자재 생성 조회
    ///  TY_P_MR_32J7A127 : 선급자재 DETAIL 조회
    ///  TY_P_MR_32J7A128 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_32J7C129 : 선급자재 생성 조회
    ///  TY_S_MR_32J7M130 : 선급자재 DETAIL 조회
    ///  TY_S_UT_B1FE5328 : 선급자재 DETAIL 하위 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CANCEL : 취소
    ///  CREATE : 생성
    ///  INQ : 조회
    ///  JASAN_CRE : 자산생성
    ///  JASAN_DEL : 자산삭제
    ///  JPNO_CRE : 전표생성
    ///  JPNO_DEL : 전표삭제
    ///  FXDDPMK : 귀속부서
    ///  FXDSAUP : 선급사업부
    ///  FXDGETDATE : 취득일
    ///  GCDACGHAP : 계정총액
    ///  GDAESANGHAP : 대상총액
    ///  GJANGHAP : 잔액
    /// </summary>
    public partial class TYUTPS017I : TYBase
    {
        private string fsFXDNUM = string.Empty;

        private string fsJASANNUM = string.Empty;
        private string fsPONUM    = string.Empty;
        private string fsRRNUM    = string.Empty;
        private string fsVEND     = string.Empty;
        private string fsITEMCODE = string.Empty;
        private string fsCGVEND   = string.Empty;
        private string fsCHGUBUN  = string.Empty;
        private string fsGUBUN    = string.Empty;

        private string fsIJWKTYPE  = string.Empty;
        private string fsIJTMGUBN  = string.Empty;
        private string fsIJIPDATE  = string.Empty;

        private string fsIJHWAJU   = string.Empty;
        private string fsIJHWAMUL  = string.Empty;
        private string fsIJTANKNO  = string.Empty;
        private string fsIJIPQTY   = string.Empty;

        private string fsIJCARNO   = string.Empty;
        private string fsIJCONTAIN = string.Empty;
        private string fsIJSEALNUM = string.Empty;

        private string fsIJIPTIME1 = string.Empty;
        private string fsIJIPTIME2 = string.Empty;

        private string fsIJDESC    = string.Empty;

        private string fsHJDESC1   = string.Empty;
        private string fsHMDESC1   = string.Empty;

        #region Description : 페이지 로드
        public TYUTPS017I()
        {
            InitializeComponent();

            // 화물
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_B1FE5328, "CHMCODE", "HMDESC1", "CHMCODE");
            // 대표 화물
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_B1FE5328, "CHMSPHMCODE", "SPHMDESC1", "CHMSPHMCODE");
        }

        private void TYUTPS017I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_B1FE5328, "CHMCODE");

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.FPS91_TY_S_UT_B1FE5328.Initialize();

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.CBH01_CHMCODE.CodeText);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_UT_B1FE0327",
                this.CBH01_CHMCODE.GetValue().ToString(),
                this.CBO01_CHMEMGUBN1.GetValue().ToString(),
                this.CBO01_CHMSPGUBN1.GetValue().ToString()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_B1FE5328.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_UT_B1FE5328.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_UT_B1FE5328.GetValue(i, "CHMEMGUBN").ToString() == "Y")
                {
                    // 특정 ROW,COLUMN 색깔 변경
                    this.FPS91_TY_S_UT_B1FE5328_Sheet1.Cells[i, 2].ForeColor = Color.Red;
                    
                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_UT_B1FE5328_Sheet1.Cells[i, 2].Font = new Font("굴림", 9, FontStyle.Bold);
                }

                if (this.FPS91_TY_S_UT_B1FE5328.GetValue(i, "CHMSPGUBN").ToString() == "Y")
                {
                    // 특정 ROW,COLUMN 색깔 변경
                    this.FPS91_TY_S_UT_B1FE5328_Sheet1.Cells[i, 3].ForeColor = Color.Red;

                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_UT_B1FE5328_Sheet1.Cells[i, 3].Font = new Font("굴림", 9, FontStyle.Bold);
                }
            }

        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sIJIPTIME = string.Empty;
            string sIJNUMBER = string.Empty;

            int i = 0;

            DataTable dt = new DataTable();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            // 등록
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_B1IB3331", ds.Tables[0].Rows[i]["CHMCODE"].ToString(),
                                                                ds.Tables[0].Rows[i]["CHMEMGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["CHMSPGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["CHMSPHMCODE"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            // 수정
            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_B1IB6332", ds.Tables[1].Rows[i]["CHMEMGUBN"].ToString(),
                                                                ds.Tables[1].Rows[i]["CHMSPGUBN"].ToString(),
                                                                ds.Tables[1].Rows[i]["CHMSPHMCODE"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["CHMCODE"].ToString()
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            // 삭제
            if (ds.Tables[2].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_B1IB6333", ds.Tables[2].Rows[i]["CHMCODE"].ToString()
                                                                );
                }
                this.DbConnector.ExecuteTranQueryList();
            }

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_MR_2BF50354"); // 저장 메세지
        }
        #endregion

        #region Description : 처리 ProcessCheck
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_B1FE5328.GetDataSourceInclude(TSpread.TActionType.New,    "CHMCODE", "CHMEMGUBN", "CHMSPGUBN", "CHMSPHMCODE"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_B1FE5328.GetDataSourceInclude(TSpread.TActionType.Update, "CHMCODE", "CHMEMGUBN", "CHMSPGUBN", "CHMSPHMCODE"));
            // 스프레드에서 삭제 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_B1FE5328.GetDataSourceInclude(TSpread.TActionType.Remove, "CHMCODE"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            // 신규
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 존재체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B1IAT330", ds.Tables[0].Rows[i]["CHMCODE"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_7B495940");
                    e.Successed = false;
                    return;
                }

                // 화물
                this.DbConnector.CommandClear();
                this.DbConnector.Attach(
                                       "TY_P_UT_668DS093",
                                       "HM",
                                       ds.Tables[0].Rows[i]["CHMCODE"].ToString()
                                       );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count <= 0)
                {
                    this.ShowMessage("TY_M_UT_71NBR559");
                    e.Successed = false;
                    return;
                }

                // 대표화물
                if (ds.Tables[0].Rows[i]["CHMSPHMCODE"].ToString() != "")
                {
                    // 화물
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(
                                           "TY_P_UT_668DS093",
                                           "HM",
                                           ds.Tables[0].Rows[i]["CHMSPHMCODE"].ToString()
                                           );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_UT_71NBR559");
                        e.Successed = false;
                        return;
                    }
                }
            }

            // 수정
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 대표화물
                if (ds.Tables[1].Rows[i]["CHMSPHMCODE"].ToString() != "")
                {
                    // 화물
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach(
                                           "TY_P_UT_668DS093",
                                           "HM",
                                           ds.Tables[1].Rows[i]["CHMSPHMCODE"].ToString()
                                           );

                    dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count <= 0)
                    {
                        this.ShowMessage("TY_M_UT_71NBR559");
                        e.Successed = false;
                        return;
                    }
                }
            }

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion
    }
}