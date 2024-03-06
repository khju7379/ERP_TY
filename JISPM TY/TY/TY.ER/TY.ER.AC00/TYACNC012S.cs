using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EVA 계획금액 관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.05 18:21
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2956Z832 : EVA 계획금액관리 조회
    ///  TY_P_AC_29573833 : EVA 계획금액관리 입력
    ///  TY_P_AC_29573834 : EVA 계획금액관리 수정
    ///  TY_P_AC_29573837 : EVA 계획금액관리 월별 조회
    ///  TY_P_AC_29576835 : EVA 계획금액관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29574838 : EVA 계획금액관리 월별 조회
    ///  TY_S_AC_29578836 : EVA 계획금액관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  AMPCDAC : 계획계정
    ///  AMPDPMK : 사업장
    ///  AMPYEAR : 년
    /// </summary>
    public partial class TYACNC012S : TYBase
    {
        #region Description : 조회 버튼 이벤트
        public TYACNC012S()
        {
            InitializeComponent();
        }

        private void TYACNC012S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.TXT01_AMPYEAR.SetValue(DateTime.Now.ToString("yyyy"));

            this.CBH01_AMPDPMK.DummyValue = this.TXT01_AMPYEAR.GetValue() + "0101";

            this.SetStartingFocus(this.TXT01_AMPYEAR);
        }
        #endregion

        #region Description : 일괄등록 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACNC012B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2956Z832", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_29578836.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29576835", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[i]["AMPFLAG"].ToString().Trim() == "U")
                {
                    this.DbConnector.Attach("TY_P_AC_29573834", ds.Tables[0].Rows[i]["AMPAMT"].ToString(),
                                                                ds.Tables[0].Rows[i]["AMPYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["AMPDPMK"].ToString(),
                                                                ds.Tables[0].Rows[i]["AMPCDAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["AMPMONTH"].ToString()
                                                                ); //수정
                }
                else
                {
                    this.DbConnector.Attach("TY_P_AC_29573833", ds.Tables[0].Rows[i]["AMPYEAR"].ToString(),
                                                                ds.Tables[0].Rows[i]["AMPDPMK"].ToString(),
                                                                ds.Tables[0].Rows[i]["AMPCDAC"].ToString(),
                                                                ds.Tables[0].Rows[i]["AMPMONTH"].ToString(),
                                                                ds.Tables[0].Rows[i]["AMPAMT"].ToString()); //저장
                }
            }
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29573837", this.FPS91_TY_S_AC_29578836.GetValue("AMPYEAR").ToString(),
                                                        this.FPS91_TY_S_AC_29578836.GetValue("AMPDPMK").ToString(),
                                                        this.FPS91_TY_S_AC_29578836.GetValue("AMPCDAC").ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_AC_29574838.SetValue(UP_MonRowAdd(dt));

            this.ShowMessage("TY_M_GB_23NAD873");
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            // 수정
            ds.Tables.Add(this.FPS91_TY_S_AC_29574838.GetDataSourceInclude(TSpread.TActionType.Update, "AMPYEAR", "AMPMONTH", "AMPDPMK", "AMPCDAC", "AMPAMT","AMPFLAG"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
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

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_29574838.GetDataSourceInclude(TSpread.TActionType.Remove, "AMPYEAR", "AMPMONTH", "AMPDPMK", "AMPCDAC");

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

        #region Description : FPS91_TY_S_AC_29578836_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_29578836_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_29573837", this.FPS91_TY_S_AC_29578836.GetValue("AMPYEAR").ToString(), 
                                                        this.FPS91_TY_S_AC_29578836.GetValue("AMPDPMK").ToString(),
                                                        this.FPS91_TY_S_AC_29578836.GetValue("AMPCDAC").ToString());
            DataTable dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_AC_29574838.SetValue(UP_MonRowAdd(dt));
        }
        #endregion

        #region Description : TXT01_AMPYEAR_TextChanged 이벤트
        private void TXT01_AMPYEAR_TextChanged(object sender, EventArgs e)
        {
            if (TXT01_AMPYEAR.GetValue().ToString() != "")
            {
                this.CBH01_AMPDPMK.DummyValue = TXT01_AMPYEAR.GetValue() + "0101";
            }
            else
            {
                this.CBH01_AMPDPMK.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            }
        }
        #endregion

        #region Description : UP_MonRowAdd 사용자 함수
        private DataTable UP_MonRowAdd(DataTable dt)
        {
            DataTable Rowdt = new DataTable();
            DataRow rw;
            DataRow[] selectedRows;

            Rowdt = dt.Clone();

            for (int i = 1; i < 13; i++)
            {
                rw = Rowdt.NewRow();
                rw["AMPYEAR"] = dt.Rows[0]["AMPYEAR"].ToString();
                rw["AMPMONTH"] = i.ToString("00");
                rw["AMPDPMK"] = dt.Rows[0]["AMPDPMK"].ToString();
                rw["AMPCDAC"] = dt.Rows[0]["AMPCDAC"].ToString();
                rw["AMPAMT"] = Get_Numeric(dt.Compute("Sum(AMPAMT)", "AMPMONTH = " + i.ToString("00")).ToString());
                
                selectedRows = dt.Select("AMPMONTH = " + i.ToString("00")+"", "AMPMONTH ASC");

                if (selectedRows.Length > 0)
                {
                    rw["AMPFLAG"] = dt.Rows[0]["AMPFLAG"].ToString();
                }
                else
                {
                    rw["AMPFLAG"] = "A";
                }
                Rowdt.Rows.Add(rw);
            }

            return Rowdt;
        }
        #endregion
    }
}
