using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.BS00
{
    /// <summary>
    /// 투자및수선 세목코드 관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.07.06 11:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_776DY036 : 투자및수선 항목코드 등록
    ///  TY_P_AC_776E0037 : 투자및수선 항목코드 수정
    ///  TY_P_AC_777FY041 : 투자및수선 항목코드 확인
    ///  TY_P_AC_777FZ042 : 투자및수선 항목코드 조회
    ///  TY_P_AC_777G6043 : 투자및수선 항목코드 삭제
    ///  TY_P_AC_77BFY119 : 투자및수선 계정과목 전체 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_77BG7122 : 투자및수선 계정과목 조회
    ///  TY_S_AC_77BG8123 : 투자및수선 항목코드 조회
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
    ///  COPY : 복사
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  BIDPAC : 귀속부서
    ///  BIDPMK : 작성부서
    ///  BSCDGB : 조회구분
    ///  BSCDAC : 계정과목
    ///  BSCDACNM : 계정과목명
    ///  BSCDHC : 계정세목
    ///  BSCDHCNM : 계정세목명
    ///  BSYEAR : 년도
    /// </summary>
    public partial class TYBSKB003I : TYBase
    {
        #region Description : 폼 로드
        public TYBSKB003I()
        {
            InitializeComponent();
        }

        private void TYBSKB003I_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_77BG8123, "BICDSC");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.TXT01_BSYEAR.Text = System.DateTime.Now.ToString("yyyy");

            this.CBH01_BIDPMK.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            this.CBH01_BIDPAC.DummyValue = DateTime.Now.ToString("yyyyMMdd");

            Set_BUSEO();

            this.CBH01_BIDPMK.SetReadOnly(true);
            this.CBH01_BIDPAC.SetReadOnly(true);
            this.TXT01_BSCDAC.ReadOnly = true;
            this.TXT01_BSCDACNM.ReadOnly = true;
            this.TXT01_BSCDHC.ReadOnly = true;
            this.TXT01_BSCDHCNM.ReadOnly = true;

            SetStartingFocus(this.TXT01_BSYEAR);
        }
        #endregion

        #region Description : 복사 버튼
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYBSKB003B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            try
            {
                string sBSCDAC = string.Empty;
                
                this.FPS91_TY_S_AC_77BG7122.Initialize();
                this.FPS91_TY_S_AC_77BG8123.Initialize();

                this.DbConnector.CommandClear();

                if (this.CBO01_BSCDGB.GetValue().ToString() == "A")
                {
                    if (this.CBH01_BIDPAC.GetValue().ToString() == "A10000" || this.CBH01_BIDPAC.GetValue().ToString() == "A50000" ||
                        this.CBH01_BIDPAC.GetValue().ToString() == "A80000" || this.CBH01_BIDPAC.GetValue().ToString() == "A90000" ||
                        this.CBH01_BIDPAC.GetValue().ToString() == "C10000")
                    {
                        sBSCDAC = "424";
                    }
                    else if (this.CBH01_BIDPAC.GetValue().ToString() == "S10000" || this.CBH01_BIDPAC.GetValue().ToString() == "T10000" ||
                            this.CBH01_BIDPAC.GetValue().ToString() == "E10000" || this.CBH01_BIDPAC.GetValue().ToString() == "D10000")
                    {
                        sBSCDAC = "441";
                    }
                    else if (this.CBH01_BIDPAC.GetValue().ToString() == "S40000" || this.CBH01_BIDPAC.GetValue().ToString() == "T40000")
                    {
                        sBSCDAC = "442";
                    }

                    this.DbConnector.Attach("TY_P_AC_77BFY119", sBSCDAC);
                }
                else if (this.CBO01_BSCDGB.GetValue().ToString() == "S")
                {
                    this.DbConnector.Attach("TY_P_AC_77DD6152", this.TXT01_BSYEAR.GetValue().ToString(),
                                                                this.CBH01_BIDPMK.GetValue().ToString(),
                                                                this.CBH01_BIDPAC.GetValue().ToString());
                }

                DataTable dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_AC_77BG7122.SetValue(dt);

                UP_init();
            }
            catch
            {

            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_777G6043", dt);
                this.DbConnector.ExecuteNonQuery();

                UP_GetBSCD(this.TXT01_BSYEAR.GetValue().ToString(),
                           this.CBH01_BIDPMK.GetValue().ToString(),
                           this.CBH01_BIDPAC.GetValue().ToString(),
                           this.TXT01_BSCDAC.GetValue().ToString(),
                           this.TXT01_BSCDHC.GetValue().ToString());

                this.ShowMessage("TY_M_GB_23NAD874");
            }
            catch
            {
                this.ShowMessage("TY_M_GB_43C9G671");
            }
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_77BG8123.GetDataSourceInclude(TSpread.TActionType.Remove, "BIYEAR", "BIDPMK", "BIDPAC", "BICDAC", "BICDHC", "BICDSC");

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

            DataTable dt = new DataTable();

            try
            {
                //신규등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    
                    this.DbConnector.Attach("TY_P_AC_776DY036", ds.Tables[0].Rows[i]["BIYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIDPMK"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIDPAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BICDAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BICDHC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BICDSC"].ToString(),
                                                                ds.Tables[0].Rows[i]["BICDSCNM"].ToString(),
                                                                ds.Tables[0].Rows[i]["BIBIGO"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }

                //수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_AC_776E0037", ds.Tables[1].Rows[i]["BICDSCNM"].ToString(),
                                                                ds.Tables[1].Rows[i]["BIBIGO"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["BIYEAR"].ToString(),
                                                                ds.Tables[1].Rows[i]["BIDPMK"].ToString(),
                                                                ds.Tables[1].Rows[i]["BIDPAC"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICDAC"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICDHC"].ToString(),
                                                                ds.Tables[1].Rows[i]["BICDSC"].ToString()
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }

                UP_GetBSCD(this.TXT01_BSYEAR.GetValue().ToString(),
                           this.CBH01_BIDPMK.GetValue().ToString(),
                           this.CBH01_BIDPAC.GetValue().ToString(),
                           this.TXT01_BSCDAC.GetValue().ToString(),
                           this.TXT01_BSCDHC.GetValue().ToString());


                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_AC_77BG8123.GetDataSourceInclude(TSpread.TActionType.New, "BIYEAR", "BIDPMK", "BIDPAC", "BICDAC", "BICDHC", "BICDSC", "BICDSCNM", "BIBIGO"));

            ds.Tables.Add(this.FPS91_TY_S_AC_77BG8123.GetDataSourceInclude(TSpread.TActionType.Update, "BIYEAR", "BIDPMK", "BIDPAC", "BICDAC", "BICDHC", "BICDSC", "BICDSCNM", "BIBIGO"));

            // 저장 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_777FY041", ds.Tables[0].Rows[i]["BIYEAR"].ToString(),
                                                            ds.Tables[0].Rows[i]["BIDPMK"].ToString(),
                                                            ds.Tables[0].Rows[i]["BIDPAC"].ToString(),
                                                            ds.Tables[0].Rows[i]["BICDAC"].ToString(),
                                                            ds.Tables[0].Rows[i]["BICDHC"].ToString(),
                                                            ds.Tables[0].Rows[i]["BICDSC"].ToString()
                                                            );

                DataTable dtTmp = this.DbConnector.ExecuteDataTable();

                if (dtTmp.Rows.Count > 0)
                {
                    this.ShowCustomMessage("이미 등록된 항목입니다.[" + ds.Tables[0].Rows[i]["BICDSC"].ToString() + "][" + ds.Tables[0].Rows[i]["BICDSCNM"].ToString() + "]",
                                            "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }
            }

            // 수정 체크
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {

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

        #region Description : 투자및수선 항목코드 조회
        private void UP_GetBSCD(string sBIYEAR, string sBIDPMK, string sBIDPAC, string sBICDAC, string sBICDHC)
        {
            this.FPS91_TY_S_AC_77BG8123.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_777FZ042", sBIYEAR,
                                                        sBIDPMK,
                                                        sBIDPAC,
                                                        sBICDAC,
                                                        sBICDHC);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_77BG8123.SetValue(dt);
        }
        #endregion

        #region Description : 필드 초기화
        private void UP_init()
        {
            this.TXT01_BSCDAC.Text = "";
            this.TXT01_BSCDACNM.Text = "";
            this.TXT01_BSCDHC.Text = "";
            this.TXT01_BSCDHCNM.Text = "";
        }
        #endregion

        #region Description : 계정과목 그리드 더블클릭
        private void FPS91_TY_S_AC_77BG7122_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 계정과목이 전표계정인 경우
            if (this.FPS91_TY_S_AC_77BG7122.GetValue("A1TAG02").ToString() == "Y")
            {
                // 계정과목만 존재하는 경우(계정과목 = 계정세목)
                if (this.FPS91_TY_S_AC_77BG7122.GetValue("BSCDHC").ToString() == "")
                {
                    UP_GetBSCD(this.TXT01_BSYEAR.GetValue().ToString(),
                               this.CBH01_BIDPMK.GetValue().ToString(),
                               this.CBH01_BIDPAC.GetValue().ToString(),
                               this.FPS91_TY_S_AC_77BG7122.GetValue("BSCDAC").ToString(),
                               this.FPS91_TY_S_AC_77BG7122.GetValue("BSCDAC").ToString());

                    this.TXT01_BSCDAC.Text = this.FPS91_TY_S_AC_77BG7122.GetValue("BSCDAC").ToString();
                    this.TXT01_BSCDACNM.Text = this.FPS91_TY_S_AC_77BG7122.GetValue("BSCDACNM").ToString();
                    this.TXT01_BSCDHC.Text = this.FPS91_TY_S_AC_77BG7122.GetValue("BSCDAC").ToString();
                    this.TXT01_BSCDHCNM.Text = this.FPS91_TY_S_AC_77BG7122.GetValue("BSCDACNM").ToString();
                }
                else
                {
                    UP_GetBSCD(this.TXT01_BSYEAR.GetValue().ToString(),
                               this.CBH01_BIDPMK.GetValue().ToString(),
                               this.CBH01_BIDPAC.GetValue().ToString(),
                               this.FPS91_TY_S_AC_77BG7122.GetValue("BSCDAC").ToString(),
                               this.FPS91_TY_S_AC_77BG7122.GetValue("BSCDHC").ToString());

                    this.TXT01_BSCDAC.Text = this.FPS91_TY_S_AC_77BG7122.GetValue("BSCDAC").ToString();
                    this.TXT01_BSCDACNM.Text = this.FPS91_TY_S_AC_77BG7122.GetValue("BSCDACNM").ToString();
                    this.TXT01_BSCDHC.Text = this.FPS91_TY_S_AC_77BG7122.GetValue("BSCDHC").ToString();
                    this.TXT01_BSCDHCNM.Text = this.FPS91_TY_S_AC_77BG7122.GetValue("BSCDHCNM").ToString();
                }
            }
        }
        #endregion

        #region Description : FPS91_TY_S_AC_77BG8123_RowInserted 이벤트
        private void FPS91_TY_S_AC_77BG8123_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_77BG8123.SetValue(e.RowIndex, "BIYEAR", TXT01_BSYEAR.GetValue().ToString());
            this.FPS91_TY_S_AC_77BG8123.SetValue(e.RowIndex, "BIDPMK", CBH01_BIDPMK.GetValue().ToString());
            this.FPS91_TY_S_AC_77BG8123.SetValue(e.RowIndex, "BIDPAC", CBH01_BIDPAC.GetValue().ToString());
            this.FPS91_TY_S_AC_77BG8123.SetValue(e.RowIndex, "BICDAC", TXT01_BSCDAC.GetValue().ToString());
            this.FPS91_TY_S_AC_77BG8123.SetValue(e.RowIndex, "BICDHC", TXT01_BSCDHC.GetValue().ToString());
        }
        #endregion

        #region Description : 부서코드 가져오기
        private void Set_BUSEO()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_77CF4145", TYUserInfo.EmpNo);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.CBH01_BIDPMK.SetValue(dt.Rows[0]["KBBSTEAM"].ToString());
                this.CBH01_BIDPAC.SetValue(dt.Rows[0]["KBBUSEO"].ToString());
            }
        }
        #endregion
    }
}
 