using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 주요시설현황 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.08.06 14:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_38G1Z392 : EIS 주요시설현황 조회
    ///  TY_P_AC_38G1Z393 : EIS 주요시설현황 등록
    ///  TY_P_AC_38G1Z394 : EIS 주요시설현황 수정
    ///  TY_P_AC_38G1Z395 : EIS 주요시설현황 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_38G20396 : EIS 주요시설현황 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_246A2488 : 저장 작업을 실패했습니다.
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_2452W459 : 저장할 데이터가 없습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  COPY : 복사
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  EDFCDDP : 사업부
    ///  EDFYYMM : 년월
    /// </summary>
    public partial class TYACPO003I : TYBase
    {
        public TYACPO003I()
        {
            InitializeComponent();

        }

        #region Description : Page_Load
        private void TYACPO003I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_38G20396, "EDFCDDP");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_38G20396, "EDFYYMM");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_38G20396, "EDFASSEQ");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_AC_38G20396, "EDFSEQNO");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_EDFYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.DTP01_EDFYYMM);
        } 
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_38G20396.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_38G1Z392", this.CBO01_EDFCDDP.GetValue() , this.DTP01_EDFYYMM.GetString().ToString().Substring(0, 6));
            this.FPS91_TY_S_AC_38G20396.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_38G1Z395", dt);
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            //저장
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_38G1Z393", ds.Tables[0].Rows[i]["EDFCDDP"].ToString(),
                                                            ds.Tables[0].Rows[i]["EDFYYMM"].ToString(),
                                                            ds.Tables[0].Rows[i]["EDFASSEQ"].ToString(),
                                                            ds.Tables[0].Rows[i]["EDFSEQNO"].ToString(),
                                                            ds.Tables[0].Rows[i]["EDFASETNM"].ToString(),
                                                            ds.Tables[0].Rows[i]["EDFWNERNM"].ToString(),
                                                            ds.Tables[0].Rows[i]["EDFQUANT"].ToString(),
                                                            ds.Tables[0].Rows[i]["EDFAREA"].ToString(),
                                                            ds.Tables[0].Rows[i]["EDFBIGO"].ToString()
                                                            ); 
            }
            //수정
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_AC_38G1Z394", ds.Tables[1].Rows[i]["EDFASETNM"].ToString(),
                                                            ds.Tables[1].Rows[i]["EDFWNERNM"].ToString(),
                                                            ds.Tables[1].Rows[i]["EDFQUANT"].ToString(),
                                                            ds.Tables[1].Rows[i]["EDFAREA"].ToString(),
                                                            ds.Tables[1].Rows[i]["EDFBIGO"].ToString(),

                                                            ds.Tables[1].Rows[i]["EDFCDDP"].ToString(),
                                                            ds.Tables[1].Rows[i]["EDFYYMM"].ToString(),
                                                            ds.Tables[1].Rows[i]["EDFASSEQ"].ToString(),
                                                            ds.Tables[1].Rows[i]["EDFSEQNO"].ToString()
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873");   
        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_AC_38G20396.GetDataSourceInclude(TSpread.TActionType.New, "EDFCDDP","EDFYYMM","EDFASSEQ","EDFSEQNO","EDFASETNM","EDFWNERNM","EDFQUANT","EDFAREA","EDFBIGO"));
            ds.Tables.Add(this.FPS91_TY_S_AC_38G20396.GetDataSourceInclude(TSpread.TActionType.Update, "EDFCDDP", "EDFYYMM", "EDFASSEQ", "EDFSEQNO", "EDFASETNM", "EDFWNERNM", "EDFQUANT", "EDFAREA", "EDFBIGO"));

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

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_38G20396.GetDataSourceInclude(TSpread.TActionType.Remove, "EDFCDDP", "EDFYYMM", "EDFASSEQ", "EDFSEQNO");

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

        #region Description : 복사 버튼 이벤트
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACPO003B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : FPS91_TY_S_AC_38G20396_RowInserted 이벤트
        private void FPS91_TY_S_AC_38G20396_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_AC_38G20396.SetValue(e.RowIndex, "EDFYYMM", this.DTP01_EDFYYMM.GetString().ToString().Substring(0, 6));
            this.FPS91_TY_S_AC_38G20396.SetValue(e.RowIndex, "EDFCDDP", this.CBO01_EDFCDDP.GetValue().ToString());
        }
        #endregion

    }
}
