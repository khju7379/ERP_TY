using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;

namespace TY.ER.AT00
{
    /// <summary>
    /// 공제항목코드관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.08.14 14:28
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_88EFK555 : 공제항목코드 등록
    ///  TY_P_HR_88EFM556 : 공제항목코드 수정
    ///  TY_P_HR_88EFP557 : 공제항목코드 삭제
    ///  TY_P_HR_88EFR558 : 공제항목코드 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_88EFS560 : 공제항목코드 관리
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
    /// </summary>
    public partial class TYATKB001I : TYBase
    {
        #region Description : 폼 로드
        public TYATKB001I()
        {
            InitializeComponent();
        }

        private void TYATKB001I_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadKeyColumn(this.FPS91_TY_S_HR_88EFS560, "APCODE");

            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            this.FPS91_TY_S_HR_88EFS560.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_88EFR558");

            this.FPS91_TY_S_HR_88EFS560.SetValue(this.DbConnector.ExecuteDataTable());
            
        }
        #endregion

        #region Description : 저장버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;
            string sCODE = string.Empty;

            try
            {   
                // 신규등록
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sCODE = ds.Tables[0].Rows[i]["APCODE"].ToString().ToUpper();

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_HR_88EFK555", ds.Tables[0].Rows[i]["APCODE"].ToString(),
                                                                ds.Tables[0].Rows[i]["APDESC1"].ToString(),
                                                                ds.Tables[0].Rows[i]["APGUBN"].ToString(),
                                                                ds.Tables[0].Rows[i]["APCALGN"].ToString(),
                                                                ds.Tables[0].Rows[i]["APPRINT"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }
               
                // 수정
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    sCODE = ds.Tables[1].Rows[i]["APCODE"].ToString().ToUpper();

                    this.DbConnector.CommandClear();

                    this.DbConnector.Attach("TY_P_HR_88EFM556", ds.Tables[1].Rows[i]["APDESC1"].ToString(),
                                                                ds.Tables[1].Rows[i]["APGUBN"].ToString(),
                                                                ds.Tables[1].Rows[i]["APCALGN"].ToString(),
                                                                ds.Tables[1].Rows[i]["APPRINT"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["APCODE"].ToString()
                                                                );
                    this.DbConnector.ExecuteTranQuery();
                }

                this.BTN61_INQ_Click(null, null);

                this.ShowMessage("TY_M_GB_23NAD873");
            }
            catch
            {
                this.ShowCustomMessage("[" + sCODE + "] 항목 저장에 실패하였습니다", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Description : 저장 체크
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            ds.Tables.Add(this.FPS91_TY_S_HR_88EFS560.GetDataSourceInclude(TSpread.TActionType.New, "APCODE", "APDESC1", "APGUBN", "APCALGN", "APPRINT"));

            ds.Tables.Add(this.FPS91_TY_S_HR_88EFS560.GetDataSourceInclude(TSpread.TActionType.Update, "APCODE", "APDESC1", "APGUBN", "APCALGN", "APPRINT"));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_HR_88EHH570", ds.Tables[0].Rows[i]["APCODE"].ToString());

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowCustomMessage("[" + ds.Tables[0].Rows[i]["APCODE"].ToString() + "] 이미 등록된 자료입니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_88EFP557", dt);
                this.DbConnector.ExecuteTranQuery();
            }

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_88EFS560.GetDataSourceInclude(TSpread.TActionType.Remove, "APCODE");

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

        private void FPS91_TY_S_HR_88EFS560_RowInserted(object sender, TSpread.TAlterEventRow e)
        {
            this.FPS91_TY_S_HR_88EFS560.SetValue(e.RowIndex, "APGUBN", "1");
            this.FPS91_TY_S_HR_88EFS560.SetValue(e.RowIndex, "APCALGN", "1");
            this.FPS91_TY_S_HR_88EFS560.SetValue(e.RowIndex, "APPRINT", "Y");
        }
    }
}
