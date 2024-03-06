using System;
using System.Data;
using System.Drawing;
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
    public partial class TYUTIL017I : TYBase
    {
        double fdDNJILSO = 0;
        double fdTOTAL = 0;
        double fdGUMAEK = 0;
        double fdJLAMT = 0;
        string fsCheck = string.Empty;
        double fdAMT = 0;

        public TYUTIL017I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_B25FG492, "STDHWAJU",  "HJDESC1", "STDHWAJU");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_B25FG492, "STDHWAMUL", "HMDESC1", "STDHWAMUL");
        }

        private void TYUTIL017I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_B25FG492, "STDYYMM", "STDYYMMDD", "STDTANKNO", "STDHWAJU", "STDHWAMUL");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.DTP01_STDYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            this.BTN61_INQ_Click(null, null);

            this.BTN61_INQ_FXM_Click(null, null);

            SetStartingFocus(this.DTP01_STDYYMM);
        }

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_B25FG492.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B25FG491", this.DTP01_STDYYMM.GetString().Substring(0, 6));

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_B25FG492.SetValue(dt);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_FXM_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_B25FN493.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B25H2507", this.DTP01_STDYYMM.GetString().Substring(0, 6), this.DTP01_STDYYMM.GetString().Substring(0, 6));

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_B25FN493.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_UT_B25FN493.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_UT_B25FN493.GetValue(i, "STDYYMMDD").ToString() == "월별 합계")
                {
                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_UT_B25FN493.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                    this.FPS91_TY_S_UT_B25FN493.ActiveSheet.Rows[i].ForeColor = Color.Red;
                }
            }
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
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_B25GM500", ds.Tables[0].Rows[i]["STDYYMM"].ToString(),
                                                                    ds.Tables[0].Rows[i]["STDYYMMDD"].ToString(),
                                                                    ds.Tables[0].Rows[i]["STDTANKNO"].ToString(),
                                                                    ds.Tables[0].Rows[i]["STDHWAJU"].ToString(),
                                                                    ds.Tables[0].Rows[i]["STDHWAMUL"].ToString(),
                                                                    Get_Numeric(ds.Tables[0].Rows[i]["STDTIME"].ToString()),
                                                                    TYUserInfo.EmpNo
                                                                    );
                    }

                    this.DbConnector.ExecuteTranQueryList();
                }
                
                //수정
                if (ds.Tables[1].Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_B25GN503", Get_Numeric(ds.Tables[1].Rows[i]["STDTIME"].ToString()),
                                                                    TYUserInfo.EmpNo,
                                                                    ds.Tables[1].Rows[i]["STDYYMM"].ToString(),
                                                                    ds.Tables[1].Rows[i]["STDYYMMDD"].ToString(),
                                                                    ds.Tables[1].Rows[i]["STDTANKNO"].ToString(),
                                                                    ds.Tables[1].Rows[i]["STDHWAJU"].ToString(),
                                                                    ds.Tables[1].Rows[i]["STDHWAMUL"].ToString()
                                                                    );
                    }

                    this.DbConnector.ExecuteTranQueryList();
                }

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    this.BTN61_INQ_Click(null, null);

                    this.BTN61_INQ_FXM_Click(null, null);

                    this.ShowMessage("TY_M_GB_23NAD873");
                }
            }
            catch
            {
                this.ShowMessage("TY_M_AC_246A2488");
            }
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;


                if (dt.Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_UT_B25GN504", dt.Rows[i]["STDYYMM"].ToString(),
                                                                    dt.Rows[i]["STDYYMMDD"].ToString(),
                                                                    dt.Rows[i]["STDTANKNO"].ToString(),
                                                                    dt.Rows[i]["STDHWAJU"].ToString(),
                                                                    dt.Rows[i]["STDHWAMUL"].ToString());
                    }
                    this.DbConnector.ExecuteTranQueryList();

                    this.BTN61_INQ_Click(null, null);

                    this.BTN61_INQ_FXM_Click(null, null);

                    this.ShowMessage("TY_M_GB_23NAD874");
                }
            }
            catch
            {
                this.ShowMessage("TY_M_GB_43C9G671");
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_UT_B25FG492.GetDataSourceInclude(TSpread.TActionType.New,    "STDYYMM", "STDYYMMDD", "STDTANKNO", "STDHWAJU", "STDHWAMUL", "STDTIME"));

            ds.Tables.Add(this.FPS91_TY_S_UT_B25FG492.GetDataSourceInclude(TSpread.TActionType.Update, "STDYYMM", "STDYYMMDD", "STDTANKNO", "STDHWAJU", "STDHWAMUL", "STDTIME"));

            int iLen = 0;

            // 저장 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                iLen = Convert.ToString((double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["STDTIME"].ToString())) * 10)).Length;

                if (Convert.ToString((double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["STDTIME"].ToString())) * 10)).Substring(iLen - 1, 1) != "0" &&
                    Convert.ToString((double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["STDTIME"].ToString())) * 10)).Substring(iLen - 1, 1) != "5")
                {
                    this.ShowCustomMessage("소숫점 1자리 숫자는 0 또는 5만 올 수 있습니다.",
                                           "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }

                // 월별 가열료 자료 존재 시 등록 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B25GI499", ds.Tables[0].Rows[i]["STDYYMM"].ToString(),
                                                            "",
                                                            "");

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("월별 가열료 자료가 존재하므로 등록이 불가합니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }
            }

            // 수정 체크
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                iLen = Convert.ToString((double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["STDTIME"].ToString())) * 10)).Length;

                if (Convert.ToString((double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["STDTIME"].ToString())) * 10)).Substring(iLen - 1, 1) != "0" &&
                    Convert.ToString((double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["STDTIME"].ToString())) * 10)).Substring(iLen - 1, 1) != "5")
                {
                    this.ShowCustomMessage("소숫점 1자리 숫자는 0 또는 5만 올 수 있습니다.",
                                           "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }

                // 월별 가열료 자료 존재 시 수정 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B25GI499", ds.Tables[1].Rows[i]["STDYYMM"].ToString(),
                                                            ds.Tables[1].Rows[i]["STDHWAJU"].ToString(),
                                                            ds.Tables[1].Rows[i]["STDHWAMUL"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("월별 가열료 자료가 존재하므로 수정이 불가합니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
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

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_B25FG492.GetDataSourceInclude(TSpread.TActionType.Remove, "STDYYMM", "STDYYMMDD", "STDTANKNO", "STDHWAJU", "STDHWAMUL");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTable dt1 = new DataTable();

                // 월별 가열료 자료 존재 시 수정 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B25GI499", dt.Rows[i]["STDYYMM"].ToString(),
                                                            dt.Rows[i]["STDHWAJU"].ToString(),
                                                            dt.Rows[i]["STDHWAMUL"].ToString());

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    this.ShowCustomMessage("월별 가열료 자료가 존재하므로 삭제가 불가합니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }
            e.ArgData = dt;
        }
        #endregion

        #region Description : 행 추가
        private void FPS91_TY_S_UT_B25FG492_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_UT_B25FG492.SetValue(e.RowIndex, "STDYYMM", this.DTP01_STDYYMM.GetString());
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

                        this.DbConnector.Attach("TY_P_UT_676EL583", this.DTP01_STDYYMM.GetString().Substring(0,6),
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

                        this.DbConnector.Attach("TY_P_UT_676EN584", sJLTANK,
                                                                    sJLQTY,
                                                                    sJLDANGA,
                                                                    sGUMAEK,
                                                                    "C",
                                                                    TYUserInfo.EmpNo,
                                                                    this.DTP01_STDYYMM.GetString().Substring(0, 6),
                                                                    "TYC",
                                                                    "S06"
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
                this.FPS91_TY_S_UT_B25FG492.ActiveSheet.AddRows(i, 1);

                this.FPS91_TY_S_UT_B25FG492.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";

                this.FPS91_TY_S_UT_B25FG492.SetValue(i, "STDYYMM", this.DTP01_STDYYMM.GetValue().ToString());

                this.FPS91_TY_S_UT_B25FG492.SetValue(i, "STDYYMMDD", "0");
                this.FPS91_TY_S_UT_B25FG492.SetValue(i, "STDTANKNO", "");
                this.FPS91_TY_S_UT_B25FG492.SetValue(i, "STDHWAJU", "");
                this.FPS91_TY_S_UT_B25FG492.SetValue(i, "HJDESC1", "");
                this.FPS91_TY_S_UT_B25FG492.SetValue(i, "STDHWAMUL", "");
                this.FPS91_TY_S_UT_B25FG492.SetValue(i, "HMDESC1", "");

                this.FPS91_TY_S_UT_B25FG492.SetValue(i, "STDTIME", "0");

                this.FPS91_TY_S_UT_B25FG492_Sheet1.Cells[i, 1].Locked = false;
                this.FPS91_TY_S_UT_B25FG492_Sheet1.Cells[i, 2].Locked = false;
                this.FPS91_TY_S_UT_B25FG492_Sheet1.Cells[i, 3].Locked = false;
                this.FPS91_TY_S_UT_B25FG492_Sheet1.Cells[i, 4].Locked = false;
                this.FPS91_TY_S_UT_B25FG492_Sheet1.Cells[i, 5].Locked = false;
                this.FPS91_TY_S_UT_B25FG492_Sheet1.Cells[i, 6].Locked = false;
                this.FPS91_TY_S_UT_B25FG492_Sheet1.Cells[i, 7].Locked = false;
            }
        }
        #endregion
    }
}
