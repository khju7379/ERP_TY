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
    /// 하역료 단가 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2016.06.08 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_73EFY927 : 하역료 단가 관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  CHYMDATE : 기준일자
    ///  CHYMSEQ : 순번
    /// </summary>
    public partial class TYUTME004B : TYBase
    {
        private string fsYODATE     = string.Empty;
        private string fsYOINGUB    = string.Empty;
        private string fsIGDESC1    = string.Empty;
        private string fsYOBASICAMT = string.Empty;

        #region Descriptino : 페이지 로드
        public TYUTME004B()
        {
            InitializeComponent();
        }

        private void TYUTME004B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_UT_73EFY927, "JBMDATE");

            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            this.FPS91_TY_S_UT_73EFY927.Initialize();

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Descriptino : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_73EFY927.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73EFY926",
                                    Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                    Get_Date(this.DTP01_EDDATE.GetValue().ToString())
                                    );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_73EFY927.SetValue(dt);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            string sYOSEQ = string.Empty;
            int i = 0;

            DataTable dt = new DataTable();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_73EGY932", ds.Tables[0].Rows[i]["JBMDATE"].ToString(),
                                                            ds.Tables[0].Rows[i]["JBMINAMT"].ToString(),
                                                            ds.Tables[0].Rows[i]["JBMOUTAMT"].ToString(),
                                                            ds.Tables[0].Rows[i]["JBMHALINAMT"].ToString(),
                                                            ds.Tables[0].Rows[i]["JBMHALOUTAMT"].ToString(),
                                                            ds.Tables[0].Rows[i]["JBMSUNHALYUL"].ToString(),
                                                            ds.Tables[0].Rows[i]["JBMHALYUL"].ToString(),
                                                            TYUserInfo.EmpNo
                                                            ); // 저장

                this.DbConnector.ExecuteNonQuery();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                this.DbConnector.CommandClear();

                for (i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_73EGY933", ds.Tables[1].Rows[i]["JBMINAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["JBMOUTAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["JBMHALINAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["JBMHALOUTAMT"].ToString(),
                                                                ds.Tables[1].Rows[i]["JBMSUNHALYUL"].ToString(),
                                                                ds.Tables[1].Rows[i]["JBMHALYUL"].ToString(),
                                                                TYUserInfo.EmpNo,
                                                                ds.Tables[1].Rows[i]["JBMDATE"].ToString()
                                                                ); // 수정
                }

                this.DbConnector.ExecuteTranQueryList();
            }

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD873"); // 저장 메세지
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_73EGY934", dt);
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

        #region Description : 저장 ProcessCheck
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            // 스프레드에서 등록 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_73EFY927.GetDataSourceInclude(TSpread.TActionType.New,    "JBMDATE", "JBMINAMT", "JBMOUTAMT", "JBMHALINAMT", "JBMHALOUTAMT", "JBMSUNHALYUL", "JBMHALYUL"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_UT_73EFY927.GetDataSourceInclude(TSpread.TActionType.Update, "JBMDATE", "JBMINAMT", "JBMOUTAMT", "JBMHALINAMT", "JBMHALOUTAMT", "JBMSUNHALYUL", "JBMHALYUL"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_2452W459");
                e.Successed = false;
                return;
            }

            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion

        #region Description : 삭제 ProcessCheck
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_UT_73EFY927.GetDataSourceInclude(TSpread.TActionType.Remove, "JBMDATE");

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
    }
}