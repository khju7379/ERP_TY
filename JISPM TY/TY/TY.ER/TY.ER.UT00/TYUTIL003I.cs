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
    /// 질소사용료 등록 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.07.06 15:00
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_676EI582 : 질소사용료 조회
    ///  TY_P_UT_676EL583 : 질소사용료 등록
    ///  TY_P_UT_676EN584 : 질소사용료 수정
    ///  TY_P_UT_676EP585 : 질소사용료 삭제
    ///  TY_P_UT_676EQ587 : 질소사용료 확인
    ///  TY_P_UT_676ER589 : 단가등록 마스타 조회(질소사용료 관리)
    ///  TY_P_UT_676ES591 : 질소 금액 조회(질소사용료 관리)
    ///  TY_P_UT_676EU593 : 년월 총금액 조회(질소사용료 관리)
    ///  TY_P_UT_676EV594 : TYC 화주 존재 확인(질소사용료 관리)
    ///  TY_P_UT_676EV595 : 탱크요율 조회(질소사용료 관리)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_676G2596 : 질소사용료 등록
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
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
    ///  JLHWAJU : 화주
    ///  JLHWAMUL : 화물
    ///  JLYYMM : 작업년월
    ///  JLAMT : 금액
    ///  JLAMTTOT : 총 금액
    ///  JLQTYTOT : 총 사용량
    ///  JLTANK : 탱크번호
    /// </summary>
    public partial class TYUTIL003I : TYBase
    {
        double fdDNJILSO = 0;
        double fdTOTAL = 0;
        double fdGUMAEK = 0;
        double fdJLAMT = 0;
        string fsCheck = string.Empty;
        double fdAMT = 0;

        #region Description : 페이지 로드
        public TYUTIL003I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_676G2596, "JLHWAJU", "VNSANGHO", "JLHWAJU");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_676G2596, "JLHWAMUL", "CDDESC1", "JLHWAMUL");
        }

        private void TYUTIL003I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_676G2596, "JLHWAJU", "JLHWAMUL", "JLTANK");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.DTP01_JLYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_JLYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_676G2596.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_676EI582", this.DTP01_JLYYMM.GetString().Substring(0, 6));

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_676G2596.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                this.TXT01_JLQTYTOT.Text = string.Format("{0:#,###}", double.Parse(dt.Compute("Sum(JLQTY)", null).ToString()));
                this.TXT01_JLAMTTOT.Text = string.Format("{0:#,###}", double.Parse(dt.Compute("Sum(JLAMT)", null).ToString()));
            }
            else
            {
                this.TXT01_JLQTYTOT.Text = "0";
                this.TXT01_JLAMTTOT.Text = "0";
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
                this.DbConnector.Attach("TY_P_UT_676EP585", dt);
                this.DbConnector.ExecuteNonQuery();

                this.BTN61_INQ_Click(null, null);
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
            DataTable dt = this.FPS91_TY_S_UT_676G2596.GetDataSourceInclude(TSpread.TActionType.Remove, "JLYYMM", "JLHWAJU", "JLHWAMUL", "JLTANK");

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
                    fdDNJILSO = 0;
                    fdTOTAL = 0;
                    fdGUMAEK = 0;
                    fdJLAMT = 0;
                    fdAMT = 0;

                    // 질소금액등록 확인
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_676ES591", this.DTP01_JLYYMM.GetString().Substring(0, 6));
                    dt = this.DbConnector.ExecuteDataTable();
                    fdDNJILSO = Convert.ToDouble(dt.Rows[0]["DNJILSO"].ToString());

                    UP_Calculate(ds.Tables[0].Rows[i]["JLYYMM"].ToString().Substring(0, 6),
                                    ds.Tables[0].Rows[i]["JLHWAJU"].ToString(),
                                    ds.Tables[0].Rows[i]["JLQTY"].ToString(),
                                    ds.Tables[0].Rows[i]["JLDANGA"].ToString());
                        
                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_676EL583", ds.Tables[0].Rows[i]["JLYYMM"].ToString().Substring(0, 6),
                                                                ds.Tables[0].Rows[i]["JLHWAJU"].ToString(),
                                                                ds.Tables[0].Rows[i]["JLHWAMUL"].ToString(),
                                                                ds.Tables[0].Rows[i]["JLTANK"].ToString(),
                                                                ds.Tables[0].Rows[i]["JLQTY"].ToString(),
                                                                ds.Tables[0].Rows[i]["JLDANGA"].ToString(),
                                                                fdAMT,
                                                                TYUserInfo.EmpNo
                                                                );
                    this.DbConnector.ExecuteTranQuery();

                    bool b = UP_TYCUpdate(ds.Tables[0].Rows[i]["JLYYMM"].ToString().Substring(0, 6),
                                            ds.Tables[0].Rows[i]["JLTANK"].ToString(),
                                            ds.Tables[0].Rows[i]["JLQTY"].ToString(),
                                            ds.Tables[0].Rows[i]["JLDANGA"].ToString(),
                                            fdGUMAEK);
                }
                
                //수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    fdDNJILSO = 0;
                    fdTOTAL = 0;
                    fdGUMAEK = 0;
                    fdJLAMT = 0;
                    fdAMT = 0;

                    // 질소금액등록 확인
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_676ES591", this.DTP01_JLYYMM.GetString().Substring(0, 6));
                    dt = this.DbConnector.ExecuteDataTable();
                    fdDNJILSO = Convert.ToDouble(dt.Rows[0]["DNJILSO"].ToString());

                    UP_Calculate(ds.Tables[1].Rows[i]["JLYYMM"].ToString().Substring(0, 6),
                                    ds.Tables[1].Rows[i]["JLHWAJU"].ToString(),
                                    ds.Tables[1].Rows[i]["JLQTY"].ToString(),
                                    ds.Tables[1].Rows[i]["JLDANGA"].ToString());

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_UT_676EN584", ds.Tables[1].Rows[i]["JLQTY"].ToString(),
                                                                ds.Tables[1].Rows[i]["JLDANGA"].ToString(),
                                                                fdAMT,
                                                                "C",
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["JLYYMM"].ToString().Substring(0, 6),
                                                                ds.Tables[1].Rows[i]["JLHWAJU"].ToString(),
                                                                ds.Tables[1].Rows[i]["JLHWAMUL"].ToString(),
                                                                ds.Tables[1].Rows[i]["JLTANK"].ToString()
                                                                );
                    this.DbConnector.ExecuteTranQuery();

                    bool b = UP_TYCUpdate(ds.Tables[1].Rows[i]["JLYYMM"].ToString().Substring(0, 6),
                                            ds.Tables[1].Rows[i]["JLTANK"].ToString(),
                                            ds.Tables[1].Rows[i]["JLQTY"].ToString(),
                                            ds.Tables[1].Rows[i]["JLDANGA"].ToString(),
                                            fdGUMAEK);
                    
                }
                this.BTN61_INQ_Click(null, null);

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

            ds.Tables.Add(this.FPS91_TY_S_UT_676G2596.GetDataSourceInclude(TSpread.TActionType.New, "JLYYMM", "JLHWAJU", "JLHWAMUL", "JLTANK", "JLQTY", "JLDANGA", "JLAMT"));

            ds.Tables.Add(this.FPS91_TY_S_UT_676G2596.GetDataSourceInclude(TSpread.TActionType.Update, "JLYYMM", "JLHWAJU", "JLHWAMUL", "JLTANK", "JLQTY", "JLDANGA", "JLAMT"));

            // 단가등록 확인
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_676ER589", this.DTP01_JLYYMM.GetString().Substring(0, 6));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["COUNT"].ToString() == "0")
                {
                    this.ShowCustomMessage("단가 등록 후 작업하세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }
            }

            // 질소금액등록 확인
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_676ES591", this.DTP01_JLYYMM.GetString().Substring(0, 6));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToDouble(dt.Rows[0]["DNJILSO"].ToString()) == 0)
                {
                    this.ShowCustomMessage("질소 금액 등록 후 작업하세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }
            }

            // 저장 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_676EQ587", ds.Tables[0].Rows[i]["JLYYMM"].ToString().Substring(0, 6),
                                                            ds.Tables[0].Rows[i]["JLHWAJU"].ToString(),
                                                            ds.Tables[0].Rows[i]["JLHWAMUL"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("이미 등록된 자료입니다.[" + ds.Tables[0].Rows[i]["JLHWAJU"].ToString() + "][" + ds.Tables[0].Rows[i]["JLHWAMUL"].ToString()+"]", 
                                            "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }

                // 탱크 요율 등록 확인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_676EV595", ds.Tables[0].Rows[i]["JLTANK"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["COUNT"].ToString() == "0")
                    {
                        this.ShowCustomMessage("탱크요율 작업하세요![" + ds.Tables[0].Rows[i]["JLTANK"].ToString() + "]", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
                }
            }

            // 수정 체크
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 탱크 요율 등록 확인
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_676EV595", ds.Tables[1].Rows[i]["JLTANK"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["COUNT"].ToString() == "0")
                    {
                        this.ShowCustomMessage("탱크요율 작업하세요![" + ds.Tables[1].Rows[i]["JLTANK"].ToString() + "]", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                        e.Successed = false;
                        return;
                    }
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

        #region Description : 행 추가
        private void FPS91_TY_S_UT_676G2596_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_UT_676G2596.SetValue(e.RowIndex, "JLYYMM", this.DTP01_JLYYMM.GetString());
        }
        #endregion

        #region Description : JLAMT 계산
        private void UP_Calculate(string sJLYYMM, string JLHWAJU, string JLQTY, string JLDANGA)
        {
            // 1원 절사
            if (JLHWAJU != "TYC")
            {
                string sJLQTY = JLQTY;
                string sJLDANGA = JLDANGA;
                if (sJLQTY == "") sJLQTY = "0";
                if (sJLDANGA == "") sJLDANGA = "0";

                fdAMT = Convert.ToDouble(UP_DotDelete(Convert.ToString(Convert.ToDouble(sJLQTY) * Convert.ToDouble(sJLDANGA) * 0.1)));

                fdAMT = fdAMT * 10;
            }

            // 년월 총금액 조회 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_676EU593", sJLYYMM);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fdTOTAL = Convert.ToDouble(dt.Rows[0]["JLAMT"].ToString()) - fdJLAMT + fdAMT;
                fdGUMAEK = fdDNJILSO - fdTOTAL;
            }
        }
        #endregion

        private bool UP_TYCUpdate(string sJLYYMM, string sJLTANK, string sJLQTY, string sJLDANGA, double sGUMAEK)
        {
            try
            {
                bool b = true;

                //TYC체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_676EV594", sJLYYMM.Substring(0, 6));

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["COUNT"].ToString() == "0")
                    {
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_UT_676EL583", this.DTP01_JLYYMM.GetString().Substring(0,6),
                                                                    "TYC",
                                                                    "S06",
                                                                    sJLTANK,
                                                                    sJLQTY,
                                                                    sJLDANGA,
                                                                    sGUMAEK,
                                                                    TYUserInfo.EmpNo
                                                                    );

                        this.DbConnector.ExecuteTranQuery();
                    }
                    else
                    {
                        this.DbConnector.CommandClear();

                        this.DbConnector.Attach("TY_P_UT_676EN584", sJLQTY,
                                                                    sJLDANGA,
                                                                    sGUMAEK,
                                                                    "C",
                                                                    TYUserInfo.EmpNo,
                                                                    this.DTP01_JLYYMM.GetString().Substring(0, 6),
                                                                    "TYC",
                                                                    "S06",
                                                                    sJLTANK
                                                                    );

                        this.DbConnector.ExecuteTranQuery();
                    }
                }
                return b;
            }
            catch
            {
                return false;
            }
        }

        #region Description : 추가 버튼
        private void BTN61_BTNADDROW_Click(object sender, EventArgs e)
        {
            int iRowCnt = 0;

            iRowCnt = int.Parse(Get_Numeric(this.TXT01_GTADDROW.GetValue().ToString()));

            for (int i = 0; i < iRowCnt; i++)
            {
                this.FPS91_TY_S_UT_676G2596.ActiveSheet.AddRows(i, 1);

                this.FPS91_TY_S_UT_676G2596.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";

                this.FPS91_TY_S_UT_676G2596.SetValue(i, "JLYYMM", this.DTP01_JLYYMM.GetValue().ToString());

                this.FPS91_TY_S_UT_676G2596.SetValue(i, "JLHWAJU", "");
                this.FPS91_TY_S_UT_676G2596.SetValue(i, "JLHWAMUL", "");
                this.FPS91_TY_S_UT_676G2596.SetValue(i, "JLTANK", "");
                this.FPS91_TY_S_UT_676G2596.SetValue(i, "JLQTY", "0");
                this.FPS91_TY_S_UT_676G2596.SetValue(i, "JLDANGA", "180");
                this.FPS91_TY_S_UT_676G2596.SetValue(i, "JLAMT", "0");

                this.FPS91_TY_S_UT_676G2596_Sheet1.Cells[i, 1].Locked = false;
                this.FPS91_TY_S_UT_676G2596_Sheet1.Cells[i, 3].Locked = false;
            }
        }
        #endregion
    }
}
