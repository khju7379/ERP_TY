using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.US00
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
    ///  TY_S_US_919E5461 : 하역료 단가 관리
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
    public partial class TYUSME01C1 : TYBase
    {
        private string fsYODATE     = string.Empty;
        private string fsYOINGUB    = string.Empty;
        private string fsIGDESC1    = string.Empty;
        private string fsYOBASICAMT = string.Empty;

        #region Descriptino : 페이지 로드
        public TYUSME01C1()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYUSME01C1_Load(object sender, System.EventArgs e)
        {
            this.BTN61_EDIT.ProcessCheck += new TButton.CheckHandler(BTN61_EDIT_ProcessCheck);
            
            this.FPS91_TY_S_US_919E5461.Initialize();

            SetStartingFocus(this.CBH01_GHANGCHA.CodeText);
        }
        #endregion

        #region Descriptino : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_919E5461.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_919E9463",
                                    this.CBH01_GHWAJU.GetValue().ToString(),
                                    this.CBH01_GHANGCHA.GetValue().ToString()
                                    );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_919E5461.SetValue(dt);
        }
        #endregion

        #region Description : 저장 버튼
        private void BTN61_EDIT_Click(object sender, EventArgs e)
        {
            string sYOSEQ = string.Empty;
            int i = 0;

            DataTable dt = new DataTable();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_US_919D7460", ds.Tables[0].Rows[i]["JGJESTDAT"].ToString(),
                                                            ds.Tables[0].Rows[i]["JGHANGCHA"].ToString(),
                                                            ds.Tables[0].Rows[i]["JGGOKJONG"].ToString(),
                                                            ds.Tables[0].Rows[i]["JGHWAJU"].ToString()
                                                            ); // 수정
            }
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_MR_2BD3Z286"); // 수정 메세지

            this.BTN61_INQ_Click(null, null);
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

        #region Description : 수정 ProcessCheck
        private void BTN61_EDIT_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_919E5461.GetDataSourceInclude(TSpread.TActionType.Update, "JGHANGCHA", "JGGOKJONG", "JGHWAJU", "JGJESTDAT"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_919ED464");
                e.Successed = false;
                return;
            }

            // 수정 하시겠습니까?
            if (!this.ShowMessage("TY_M_MR_2BD3Y285"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion
    }
}
