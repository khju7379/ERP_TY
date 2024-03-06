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
    public partial class TYUTIL020I : TYBase
    {
        string fsCheck = string.Empty;

        #region Description : 페이지 로드
        public TYUTIL020I()
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_B2FFZ555, "JLMHWAJU",  "HJDESC1", "JLMHWAJU");
            this.SetSpreadCodeHelper(this.FPS91_TY_S_UT_B2FFZ555, "JLMHWAMUL", "HMDESC1", "JLMHWAMUL");
        }

        private void TYUTIL020I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_B2FFZ555, "JLMYYMM", "JLMTANKNO", "JLMHWAJU", "JLMHWAMUL", "JLMTIME");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.DTP01_JLMYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_JLMYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_B2FFZ555.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B2FF5551", this.DTP01_JLMYYMM.GetString().Substring(0, 6));

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_B2FFZ555.SetValue(dt);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sJLMNIUSE   = string.Empty;
            string sJLAUTOCOMP = string.Empty;

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
                        sJLMNIUSE = (ds.Tables[0].Rows[i]["JLMNIUSE"].ToString() == "Y" || ds.Tables[0].Rows[i]["JLMNIUSE"].ToString() == "True") ? "Y" : "N";
                        sJLAUTOCOMP = (ds.Tables[0].Rows[i]["JLAUTOCOMP"].ToString() == "Y" || ds.Tables[0].Rows[i]["JLAUTOCOMP"].ToString() == "True") ? "Y" : "N";

                        this.DbConnector.Attach("TY_P_UT_B5RHB365", ds.Tables[0].Rows[i]["JLMYYMM"].ToString(),
                                                                    ds.Tables[0].Rows[i]["JLMTANKNO"].ToString(),
                                                                    ds.Tables[0].Rows[i]["JLMHWAJU"].ToString(),
                                                                    ds.Tables[0].Rows[i]["JLMHWAMUL"].ToString(),
                                                                    Get_Numeric(ds.Tables[0].Rows[i]["JLMTIME"].ToString()),
                                                                    Get_Numeric(ds.Tables[0].Rows[i]["JLMQTY"].ToString()),
                                                                    Get_Numeric(ds.Tables[0].Rows[i]["JLMDSS"].ToString()),
                                                                    Get_Numeric(ds.Tables[0].Rows[i]["JLMDSSQTY"].ToString()),
                                                                    Get_Numeric(ds.Tables[0].Rows[i]["JLMCHQTY"].ToString()),
                                                                    Get_Numeric(ds.Tables[0].Rows[i]["JLMUSQTY"].ToString()),
                                                                    sJLMNIUSE.ToString(),
                                                                    sJLAUTOCOMP.ToString(),
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
                        sJLMNIUSE   = (ds.Tables[1].Rows[i]["JLMNIUSE"].ToString() == "Y" || ds.Tables[1].Rows[i]["JLMNIUSE"].ToString() == "True") ? "Y" : "N";
                        sJLAUTOCOMP = (ds.Tables[1].Rows[i]["JLAUTOCOMP"].ToString() == "Y" || ds.Tables[1].Rows[i]["JLAUTOCOMP"].ToString() == "True") ? "Y" : "N";

                        this.DbConnector.Attach("TY_P_UT_B5RHC366", Get_Numeric(ds.Tables[1].Rows[i]["JLMTIME"].ToString()),
                                                                    Get_Numeric(ds.Tables[1].Rows[i]["JLMQTY"].ToString()),
                                                                    Get_Numeric(ds.Tables[1].Rows[i]["JLMDSS"].ToString()),
                                                                    Get_Numeric(ds.Tables[1].Rows[i]["JLMDSSQTY"].ToString()),
                                                                    Get_Numeric(ds.Tables[1].Rows[i]["JLMCHQTY"].ToString()),
                                                                    Get_Numeric(ds.Tables[1].Rows[i]["JLMUSQTY"].ToString()),
                                                                    sJLMNIUSE.ToString(),
                                                                    sJLAUTOCOMP.ToString(),
                                                                    TYUserInfo.EmpNo,
                                                                    ds.Tables[1].Rows[i]["JLMYYMM"].ToString(),
                                                                    ds.Tables[1].Rows[i]["JLMTANKNO"].ToString(),
                                                                    ds.Tables[1].Rows[i]["JLMHWAJU"].ToString(),
                                                                    ds.Tables[1].Rows[i]["JLMHWAMUL"].ToString()
                                                                    );
                    }

                    this.DbConnector.ExecuteTranQueryList();
                }

                if (ds.Tables[0].Rows.Count > 0 || ds.Tables[1].Rows.Count > 0)
                {
                    this.BTN61_INQ_Click(null, null);

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
                        this.DbConnector.Attach("TY_P_UT_B2GBQ565", dt.Rows[i]["JLMYYMM"].ToString(),
                                                                    dt.Rows[i]["JLMTANKNO"].ToString(),
                                                                    dt.Rows[i]["JLMHWAJU"].ToString(),
                                                                    dt.Rows[i]["JLMHWAMUL"].ToString());
                    }
                    this.DbConnector.ExecuteTranQueryList();

                    this.BTN61_INQ_Click(null, null);

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
            double dJLMQTY    = 0;
            double dJLMDSS    = 0;
            double dJLMDSSQTY = 0;
            double dJLMCHQTY  = 0;
            double dJLMUSQTY  = 0;

            for (int i = 0; i < this.FPS91_TY_S_UT_B2FFZ555.ActiveSheet.RowCount; i++)
            {
                if ((this.FPS91_TY_S_UT_B2FFZ555.GetValue(i, "JLMNIUSE").ToString() == "Y" || this.FPS91_TY_S_UT_B2FFZ555.GetValue(i, "JLMNIUSE").ToString() == "True") &&
                    (this.FPS91_TY_S_UT_B2FFZ555.ActiveSheet.RowHeader.Cells[this.FPS91_TY_S_UT_B2FFZ555.ActiveSheet.ActiveRowIndex, 0].Text != "N"))
                {
                    this.FPS91_TY_S_UT_B2FFZ555.ActiveSheet.RowHeader.Cells[i,0].Text = "U";
                }
            }

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_UT_B2FFZ555.GetDataSourceInclude(TSpread.TActionType.New,    "JLMYYMM", "JLMTANKNO", "JLMHWAJU", "JLMHWAMUL", "JLMTIME", "JLMQTY", "JLMDSS", "JLMDSSQTY", "JLMCHQTY", "JLMUSQTY", "JLMNIUSE", "JLAUTOCOMP"));

            ds.Tables.Add(this.FPS91_TY_S_UT_B2FFZ555.GetDataSourceInclude(TSpread.TActionType.Update, "JLMYYMM", "JLMTANKNO", "JLMHWAJU", "JLMHWAMUL", "JLMTIME", "JLMQTY", "JLMDSS", "JLMDSSQTY", "JLMCHQTY", "JLMUSQTY", "JLMNIUSE", "JLAUTOCOMP"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            // 저장 체크
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 질소 사용료 자료 존재 시 수정 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_676EQ587", ds.Tables[0].Rows[i]["JLMYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["JLMHWAJU"].ToString(),
                                                            ds.Tables[0].Rows[i]["JLMHWAMUL"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("질소 사용료 자료가 존재하므로 작업이 불가합니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }

                dJLMQTY    = 0;
                dJLMDSS    = 0;
                dJLMDSSQTY = 0;
                dJLMCHQTY  = 0;
                dJLMUSQTY  = 0;

                if (ds.Tables[0].Rows[i]["JLMNIUSE"].ToString() == "Y" || ds.Tables[0].Rows[i]["JLMNIUSE"].ToString() == "True")
                {
                    if (ds.Tables[0].Rows[i]["JLAUTOCOMP"].ToString() == "" || ds.Tables[0].Rows[i]["JLAUTOCOMP"].ToString() == "N" || ds.Tables[0].Rows[i]["JLAUTOCOMP"].ToString() == "False")
                    {
                        ds.Tables[0].Rows[i]["JLMCHQTY"] = UP_GET_CHQTY(Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                                                        Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                                                                        ds.Tables[0].Rows[i]["JLMTANKNO"].ToString().Trim(),
                                                                        ds.Tables[0].Rows[i]["JLMHWAJU"].ToString().Trim(),
                                                                        ds.Tables[0].Rows[i]["JLMHWAMUL"].ToString().Trim());
                    }
                }

                dJLMQTY    = double.Parse(Get_Numeric(UP_DotDelete(ds.Tables[0].Rows[i]["JLMQTY"].ToString())));
                dJLMDSS    = double.Parse(Get_Numeric(ds.Tables[0].Rows[i]["JLMDSS"].ToString()));
                dJLMDSSQTY = double.Parse(UP_DotDelete(Convert.ToString(dJLMDSS * 2.8)));
                dJLMCHQTY  = double.Parse(Get_Numeric(UP_DotDelete(ds.Tables[0].Rows[i]["JLMCHQTY"].ToString())));


                dJLMUSQTY  = UP_GET_USQTY(dJLMQTY, dJLMDSSQTY, dJLMCHQTY, ds.Tables[0].Rows[i]["JLMTANKNO"].ToString(), ds.Tables[0].Rows[i]["JLMNIUSE"].ToString());

                dJLMDSSQTY = double.Parse(UP_DotDelete(Convert.ToString(dJLMDSSQTY)));
                dJLMUSQTY  = double.Parse(UP_DotDelete(Convert.ToString(dJLMUSQTY)));

                if (dJLMUSQTY == 0)
                {
                    this.ShowCustomMessage("사용량을 확인하세요.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }

                ds.Tables[0].Rows[i]["JLMDSSQTY"] = Convert.ToString(dJLMDSSQTY);
                ds.Tables[0].Rows[i]["JLMUSQTY"]  = Convert.ToString(dJLMUSQTY);
            }

            // 수정 체크
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                // 질소 사용료 자료 존재 시 수정 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_676EQ587", ds.Tables[1].Rows[i]["JLMYYMM"].ToString(),
                                                            ds.Tables[1].Rows[i]["JLMHWAJU"].ToString(),
                                                            ds.Tables[1].Rows[i]["JLMHWAMUL"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("질소 사용료 자료가 존재하므로 수정이 불가합니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }

                if (ds.Tables[1].Rows[i]["JLMNIUSE"].ToString() == "Y" || ds.Tables[1].Rows[i]["JLMNIUSE"].ToString() == "True")
                {
                    if (ds.Tables[1].Rows[i]["JLAUTOCOMP"].ToString() == "" || ds.Tables[1].Rows[i]["JLAUTOCOMP"].ToString() == "N" || ds.Tables[1].Rows[i]["JLAUTOCOMP"].ToString() == "False")
                    {
                        ds.Tables[1].Rows[i]["JLMCHQTY"] = UP_GET_CHQTY(Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                                                        Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                                                                        ds.Tables[1].Rows[i]["JLMTANKNO"].ToString().Trim(),
                                                                        ds.Tables[1].Rows[i]["JLMHWAJU"].ToString().Trim(),
                                                                        ds.Tables[1].Rows[i]["JLMHWAMUL"].ToString().Trim());
                    }
                }

                dJLMQTY    = 0;
                dJLMDSS    = 0;
                dJLMDSSQTY = 0;
                dJLMCHQTY  = 0;
                dJLMUSQTY  = 0;

                dJLMQTY    = double.Parse(Get_Numeric(UP_DotDelete(ds.Tables[1].Rows[i]["JLMQTY"].ToString())));
                dJLMDSS    = double.Parse(Get_Numeric(ds.Tables[1].Rows[i]["JLMDSS"].ToString()));
                dJLMDSSQTY = double.Parse(UP_DotDelete(Convert.ToString(dJLMDSS * 2.8)));
                dJLMCHQTY  = double.Parse(Get_Numeric(UP_DotDelete(ds.Tables[1].Rows[i]["JLMCHQTY"].ToString())));

                dJLMUSQTY  = UP_GET_USQTY(dJLMQTY, dJLMDSSQTY, dJLMCHQTY, ds.Tables[1].Rows[i]["JLMTANKNO"].ToString(), ds.Tables[1].Rows[i]["JLMNIUSE"].ToString());

                dJLMDSSQTY = double.Parse(UP_DotDelete(Convert.ToString(dJLMDSSQTY)));
                dJLMUSQTY  = double.Parse(UP_DotDelete(Convert.ToString(dJLMUSQTY)));

                if (dJLMUSQTY == 0)
                {
                    this.ShowCustomMessage("사용량을 확인하세요.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }

                ds.Tables[1].Rows[i]["JLMDSSQTY"] = Convert.ToString(dJLMDSSQTY);
                ds.Tables[1].Rows[i]["JLMUSQTY"]  = Convert.ToString(dJLMUSQTY);
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
            DataTable dt = this.FPS91_TY_S_UT_B2FFZ555.GetDataSourceInclude(TSpread.TActionType.Remove, "JLMYYMM", "JLMTANKNO", "JLMHWAJU", "JLMHWAMUL");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataTable dt1 = new DataTable();

                // 질소 사용료 자료 존재 시 수정 불가
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_676EQ587", dt.Rows[i]["JLMYYMM"].ToString(),
                                                            dt.Rows[i]["JLMHWAJU"].ToString(),
                                                            dt.Rows[i]["JLMHWAMUL"].ToString());

                dt1 = this.DbConnector.ExecuteDataTable();

                if (dt1.Rows.Count > 0)
                {
                    this.ShowCustomMessage("질소 사용료 자료가 존재하므로 수정이 불가합니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
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
        private void FPS91_TY_S_UT_B2FFZ555_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_UT_B2FFZ555.SetValue(e.RowIndex, "JLDYYMM", this.DTP01_JLMYYMM.GetString());
        }
        #endregion

        #region Description : 추가 버튼
        private void BTN61_BTNADDROW_Click(object sender, EventArgs e)
        {
            int iRowCnt = 0;

            iRowCnt = int.Parse(Get_Numeric(this.TXT01_GTADDROW.GetValue().ToString()));

            for (int i = 0; i < iRowCnt; i++)
            {
                this.FPS91_TY_S_UT_B2FFZ555.ActiveSheet.AddRows(i, 1);

                this.FPS91_TY_S_UT_B2FFZ555.ActiveSheet.RowHeader.Cells[i, 0].Text = "N";

                this.FPS91_TY_S_UT_B2FFZ555.SetValue(i, "JLMYYMM", this.DTP01_JLMYYMM.GetValue().ToString());

                this.FPS91_TY_S_UT_B2FFZ555.SetValue(i, "JLMTANKNO", "");
                this.FPS91_TY_S_UT_B2FFZ555.SetValue(i, "JLMHWAJU",  "");
                this.FPS91_TY_S_UT_B2FFZ555.SetValue(i, "HJDESC1",   "");
                this.FPS91_TY_S_UT_B2FFZ555.SetValue(i, "JLMHWAMUL", "");
                this.FPS91_TY_S_UT_B2FFZ555.SetValue(i, "HMDESC1",   "");

                this.FPS91_TY_S_UT_B2FFZ555.SetValue(i, "JLMTIME",   "0");

                this.FPS91_TY_S_UT_B2FFZ555.SetValue(i, "JLMQTY",    "0");
                this.FPS91_TY_S_UT_B2FFZ555.SetValue(i, "JLMDSS",    "0");
                this.FPS91_TY_S_UT_B2FFZ555.SetValue(i, "JLMCHQTY",  "0");
                this.FPS91_TY_S_UT_B2FFZ555.SetValue(i, "JLMUSQTY",  "0");

                this.FPS91_TY_S_UT_B2FFZ555_Sheet1.Cells[i, 1].Locked = false;
                this.FPS91_TY_S_UT_B2FFZ555_Sheet1.Cells[i, 2].Locked = false;
                this.FPS91_TY_S_UT_B2FFZ555_Sheet1.Cells[i, 3].Locked = false;
                this.FPS91_TY_S_UT_B2FFZ555_Sheet1.Cells[i, 4].Locked = false;
                this.FPS91_TY_S_UT_B2FFZ555_Sheet1.Cells[i, 5].Locked = true;
                this.FPS91_TY_S_UT_B2FFZ555_Sheet1.Cells[i, 6].Locked = false;
                this.FPS91_TY_S_UT_B2FFZ555_Sheet1.Cells[i, 7].Locked = true;
                this.FPS91_TY_S_UT_B2FFZ555_Sheet1.Cells[i, 8].Locked = false;
                this.FPS91_TY_S_UT_B2FFZ555_Sheet1.Cells[i, 9].Locked = false;
                this.FPS91_TY_S_UT_B2FFZ555_Sheet1.Cells[i, 10].Locked = false;
            }
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_B2FFZ555_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            double dJLMQTY    = 0;
            double dJLMDSS    = 0;
            double dJLMDSSQTY = 0;
            double dJLMCHQTY  = 0;
            double dJLMUSQTY  = 0;

            //if (e.Column == 0)
            //{
                dJLMQTY    = double.Parse(Get_Numeric(UP_DotDelete(this.FPS91_TY_S_UT_B2FFZ555.GetValue("JLMQTY").ToString())));
                dJLMDSS    = double.Parse(Get_Numeric(this.FPS91_TY_S_UT_B2FFZ555.GetValue("JLMDSS").ToString()));
                dJLMDSSQTY = double.Parse(UP_DotDelete(Convert.ToString(dJLMDSS * 2.8)));
                dJLMCHQTY  = double.Parse(Get_Numeric(UP_DotDelete(this.FPS91_TY_S_UT_B2FFZ555.GetValue("JLMCHQTY").ToString())));

                dJLMUSQTY  = UP_GET_USQTY(dJLMQTY, dJLMDSSQTY, dJLMCHQTY, this.FPS91_TY_S_UT_B2FFZ555.GetValue("JLMTANKNO").ToString(), this.FPS91_TY_S_UT_B2FFZ555.GetValue("JLMNIUSE").ToString());

                dJLMDSSQTY = double.Parse(UP_DotDelete(Convert.ToString(dJLMDSSQTY)));
                dJLMUSQTY  = double.Parse(UP_DotDelete(Convert.ToString(dJLMUSQTY)));

                this.FPS91_TY_S_UT_B2FFZ555.SetValue("JLMDSSQTY", dJLMDSSQTY);
                this.FPS91_TY_S_UT_B2FFZ555.SetValue("JLMUSQTY", dJLMUSQTY);
            //}
        }
        #endregion

        #region Description : 출고량 가져오기
        private double UP_GET_CHQTY(string sSTDATE, string sEDDATE, string sCHIPTANK, string sCHHWAJU, string sCHHWAMUL)
        {
            double dCHMTQTY = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_B3F91949", sSTDATE.ToString().Trim(),
                                                        sEDDATE.ToString().Trim(),
                                                        sCHIPTANK.ToString().Trim(),
                                                        sCHHWAJU.ToString().Trim(),
                                                        sCHHWAMUL.ToString().Trim());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                dCHMTQTY = double.Parse(dt.Rows[0]["CHMTQTY"].ToString());
            }

            return dCHMTQTY;
        }
        #endregion

        #region Description : 사용량 가져오기
        private double UP_GET_USQTY(double dJLMQTY, double dJLMDSSQTY, double dJLMCHQTY, string sJLMTANKNO, string sJLMNIUSE)
        {
            double dJLMUSQTY = 0;
            double dCAPA = 0;

            dJLMUSQTY = dJLMQTY + double.Parse(UP_DotDelete(Convert.ToString(dJLMDSSQTY))) + dJLMCHQTY;

            if (sJLMNIUSE.ToString() == "Y" || sJLMNIUSE.ToString() == "True")
            {
                if (sJLMTANKNO.ToString() != "")
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_UT_6AQKH593", sJLMTANKNO.ToString());

                    DataTable dt = this.DbConnector.ExecuteDataTable();

                    if (dt.Rows.Count > 0)
                    {
                        dCAPA = double.Parse(dt.Rows[0]["TNCAPA"].ToString());
                    }

                    dJLMUSQTY = dJLMUSQTY + (dCAPA * 1.1);
                }
            }

            return dJLMUSQTY;
        }
        #endregion
    }
}
