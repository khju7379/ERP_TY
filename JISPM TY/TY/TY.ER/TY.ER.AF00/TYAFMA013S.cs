using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

using System.Data.OleDb;

namespace TY.ER.AF00
{
    /// <summary>
    /// EIS 계열사 엑셀자료 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.11.13 15:10
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// </summary>
    public partial class TYAFMA013S : TYBase
    {
        private string fsCompanyCode = string.Empty;

        public TYAFMA013S()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYAFMA013S_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_3BD5M277, "ESISEQN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_3BJ94349, "ESSNOLN");

            this.DTP01_ESSYYMM.SetReadOnly(true);
            this.CBH01_ESSSUBGN.SetReadOnlyCode(true);
            this.CBH01_ESSSUBGN.SetReadOnlyText(true);
            this.TXT01_ESSSEQN.SetReadOnly(true);

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN62_SAV.ProcessCheck += new TButton.CheckHandler(BTN62_SAV_ProcessCheck);
            this.BTN62_REM.ProcessCheck += new TButton.CheckHandler(BTN62_REM_ProcessCheck);

            switch (TYUserInfo.EmpNo.Substring(0, 2))
            {
                case "HT":
                    fsCompanyCode = "TH";
                    break;
                case "TG":
                    fsCompanyCode = "TG";
                    break;
                case "TS":
                    fsCompanyCode = "TS";
                    break;
                case "TL":
                    fsCompanyCode = "TL";
                    break;
                default:
                    fsCompanyCode = "";
                    break;
            }

            if (fsCompanyCode != "")
            {
                this.CBH01_ESISUBGN.SetValue(fsCompanyCode);
                this.CBH01_ESISUBGN.SetReadOnly(true);

                this.SetStartingFocus(this.DTP01_ESIYYMM);
            }
            else
            {
                this.SetStartingFocus(this.CBH01_ESISUBGN.CodeText);
            }

            UP_start_dsp_month();

            //this.DTP01_ESIYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
        }
        #endregion

        #region Description : 경영이슈 조회
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3BD5L276",
                this.CBH01_ESISUBGN.GetValue(),
                this.DTP01_ESIYYMM.GetString().ToString().Substring(0, 6)
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_3BD5M277.SetValue(dt);
        }
        #endregion

        #region Description : 경영이슈 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_3BE1L290", ds.Tables[0].Rows[i]["ESISUBGN"].ToString(),
                                                            ds.Tables[0].Rows[i]["ESIYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["ESISEQN"].ToString(),
                                                            ds.Tables[0].Rows[i]["ESISTITLE"].ToString()); //저장
            }

            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_3BJ92340", ds.Tables[1].Rows[i]["ESISTITLE"].ToString(),
                                                            ds.Tables[1].Rows[i]["ESISUBGN"].ToString(),
                                                            ds.Tables[1].Rows[i]["ESIYYMM"].ToString(),
                                                            ds.Tables[1].Rows[i]["ESISEQN"].ToString()); //수정
            }

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            // 경영이슈 마스터 삭제
            this.DbConnector.Attach("TY_P_AC_3BJ93341", ds.Tables[0]);
            this.DbConnector.ExecuteNonQueryList();

            this.DbConnector.CommandClear();
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                // 경영이슈 내역 일괄삭제
                this.DbConnector.Attach("TY_P_AC_3BJ9O350", ds.Tables[0].Rows[i]["ESISUBGN"].ToString(),
                                                            ds.Tables[0].Rows[i]["ESIYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["ESISEQN"].ToString());
            }
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 경영이슈 내역 조회
        private void UP_SEL_ESISSUESF()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_AC_3BJ8Z339",
                this.CBH01_ESSSUBGN.GetValue(),
                this.DTP01_ESSYYMM.GetString().ToString().Substring(0, 6),
                this.TXT01_ESSSEQN.GetValue()
                );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_AC_3BJ94349.SetValue(dt);
        }
        #endregion

        #region Description : 경영이슈 내역 저장 버튼
        private void BTN62_SAV_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_3BJ94344", ds.Tables[0].Rows[i]["ESSSUBGN"].ToString(),
                                                            ds.Tables[0].Rows[i]["ESSYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["ESSSEQN"].ToString(),
                                                            ds.Tables[0].Rows[i]["ESSNOLN"].ToString(),
                                                            ds.Tables[0].Rows[i]["ESSISSUE"].ToString(),
                                                            ds.Tables[0].Rows[i]["ESSCORRE"].ToString()
                                                            ); //저장
            }

            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_3BJ95345", ds.Tables[1].Rows[i]["ESSISSUE"].ToString(),
                                                            ds.Tables[1].Rows[i]["ESSCORRE"].ToString(),
                                                            ds.Tables[1].Rows[i]["ESSSUBGN"].ToString(),
                                                            ds.Tables[1].Rows[i]["ESSYYMM"].ToString(),
                                                            ds.Tables[1].Rows[i]["ESSSEQN"].ToString(),
                                                            ds.Tables[1].Rows[i]["ESSNOLN"].ToString()
                                                            ); //수정
            }

            this.DbConnector.ExecuteTranQueryList();

            UP_SEL_ESISSUESF();
            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 경영이슈 내역 삭제 버튼
        private void BTN62_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3BJ96346", ds.Tables[0]);
            this.DbConnector.ExecuteNonQueryList();

            UP_SEL_ESISSUESF();

            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 경영이슈 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // ------------------------   마감 완료 CHECK 시작  ------------------------------------------ //

            this.DbConnector.CommandClear(); // TY_P_AC_27H64059
            this.DbConnector.Attach("TY_P_AC_3C92V659", this.DTP01_ESIYYMM.GetValue().ToString().Substring(0, 4), this.DTP01_ESIYYMM.GetValue().ToString().Substring(4, 2));
            DataTable dt1 = this.DbConnector.ExecuteDataTable();

            if (dt1.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_27H6I062"); // EIS 마감 년월이 존재 하지 않습니다.
                e.Successed = false;
                return;
            }
            else
            {
                if (dt1.Rows[0]["ECSBBUN"].ToString() == "Z")
                {
                    this.ShowMessage("TY_M_AC_27H6I063"); // EIS 적용 완료상태 입니다. (처리 불가)
                    e.Successed = false;
                    return;
                }
            }

            // ------------------------   마감 완료 CHECK 끝 ------------------------------------------ //

            if (TYUserInfo.EmpNo.Substring(0, 1).ToString() == "0" || TYUserInfo.EmpNo.Substring(0, 1).ToString() == "C")
            {
                this.ShowMessage("TY_M_AC_3992B618");
                e.Successed = false;
                return;
            }

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_3BD5M277.GetDataSourceInclude(TSpread.TActionType.New, "ESISUBGN", "ESIYYMM", "ESISEQN", "ESISTITLE"));
            ds.Tables.Add(this.FPS91_TY_S_AC_3BD5M277.GetDataSourceInclude(TSpread.TActionType.Update, "ESISUBGN", "ESIYYMM", "ESISEQN", "ESISTITLE"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }

            int i = 0;
            DataTable dt = new DataTable();

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3BJBG356",
                    ds.Tables[0].Rows[i]["ESISUBGN"].ToString(),
                    ds.Tables[0].Rows[i]["ESIYYMM"].ToString(),
                    ds.Tables[0].Rows[i]["ESISEQN"].ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_387AG357");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 경영이슈 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (TYUserInfo.EmpNo.Substring(0, 1).ToString() == "0" || TYUserInfo.EmpNo.Substring(0, 1).ToString() == "C")
            {
                this.ShowMessage("TY_M_AC_3992B618");
                e.Successed = false;
                return;
            }

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_3BD5M277.GetDataSourceInclude(TSpread.TActionType.Remove, "ESISUBGN", "ESIYYMM", "ESISEQN"));

            // 삭제
            if (ds.Tables[0].Rows.Count <= 0)
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

            e.ArgData = ds;
        }
        #endregion

        #region Description : 경영이슈 내역 저장 체크
        private void BTN62_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (TYUserInfo.EmpNo.Substring(0, 1).ToString() == "0" || TYUserInfo.EmpNo.Substring(0, 1).ToString() == "C")
            {
                this.ShowMessage("TY_M_AC_3992B618");
                e.Successed = false;
                return;
            }

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_3BJ94349.GetDataSourceInclude(TSpread.TActionType.New,    "ESSSUBGN", "ESSYYMM", "ESSSEQN", "ESSNOLN", "ESSISSUE", "ESSCORRE"));
            ds.Tables.Add(this.FPS91_TY_S_AC_3BJ94349.GetDataSourceInclude(TSpread.TActionType.Update, "ESSSUBGN", "ESSYYMM", "ESSSEQN", "ESSNOLN", "ESSISSUE", "ESSCORRE"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }

            int i = 0;
            DataTable dt = new DataTable();

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_AC_3BJBH357",
                    ds.Tables[0].Rows[i]["ESSSUBGN"].ToString(),
                    ds.Tables[0].Rows[i]["ESSYYMM"].ToString(),
                    ds.Tables[0].Rows[i]["ESSSEQN"].ToString(),
                    ds.Tables[0].Rows[i]["ESSNOLN"].ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_AC_387AG357");
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 경영이슈 내역 삭제 체크
        private void BTN62_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (TYUserInfo.EmpNo.Substring(0, 1).ToString() == "0" || TYUserInfo.EmpNo.Substring(0, 1).ToString() == "C")
            {
                this.ShowMessage("TY_M_AC_3992B618");
                e.Successed = false;
                return;
            }

            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_3BJ94349.GetDataSourceInclude(TSpread.TActionType.Remove, "ESSSUBGN", "ESSYYMM", "ESSSEQN", "ESSNOLN"));

            // 삭제
            if (ds.Tables[0].Rows.Count <= 0)
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

            e.ArgData = ds;
        }
        #endregion

        #region Description : 경영이슈 스프레드 더블클릭
        private void FPS91_TY_S_AC_3BD5M277_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.CBH01_ESSSUBGN.SetValue(this.FPS91_TY_S_AC_3BD5M277.GetValue("ESISUBGN").ToString());
            this.DTP01_ESSYYMM.SetValue(this.FPS91_TY_S_AC_3BD5M277.GetValue("ESIYYMM").ToString());
            this.TXT01_ESSSEQN.SetValue(this.FPS91_TY_S_AC_3BD5M277.GetValue("ESISEQN").ToString());

            UP_SEL_ESISSUESF();
        }
        #endregion

        #region Description : 경영이슈 자료 생성
        private void FPS91_TY_S_AC_3BD5M277_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_3BD5M277.SetValue(e.RowIndex, "ESISUBGN", this.CBH01_ESISUBGN.GetValue().ToString());
            this.FPS91_TY_S_AC_3BD5M277.SetValue(e.RowIndex, "CMDESC", this.CBH01_ESISUBGN.GetText().ToString());
            this.FPS91_TY_S_AC_3BD5M277.SetValue(e.RowIndex, "ESIYYMM", this.DTP01_ESIYYMM.GetString().ToString().Substring(0, 6));
        }
        #endregion

        #region Description : 경영이슈 내역 자료 생성
        private void FPS91_TY_S_AC_3BJ94349_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_3BJ94349.SetValue(e.RowIndex, "ESSSUBGN", this.CBH01_ESSSUBGN.GetValue().ToString());
            this.FPS91_TY_S_AC_3BJ94349.SetValue(e.RowIndex, "CMDESC",   this.CBH01_ESSSUBGN.GetText().ToString());
            this.FPS91_TY_S_AC_3BJ94349.SetValue(e.RowIndex, "ESSYYMM",  this.DTP01_ESSYYMM.GetString().ToString().Substring(0, 6));
            this.FPS91_TY_S_AC_3BJ94349.SetValue(e.RowIndex, "ESSSEQN",  this.TXT01_ESSSEQN.GetValue().ToString());
        }
        #endregion

        #region Description : EIS 계열사 최종 마감 월 조회
        private void UP_start_dsp_month()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_3C94Q662");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count == 0)
            {
                this.DTP01_ESIYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
            }
            else
            {
                this.DTP01_ESIYYMM.SetValue(dt.Rows[0]["YYMM"].ToString());
            }
        }
        #endregion

    }
}