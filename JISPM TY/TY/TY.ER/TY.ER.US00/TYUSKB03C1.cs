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
    ///  TY_S_US_919HR470 : 하역료 단가 관리
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
    /// </summary>
    public partial class TYUSKB03C1 : TYBase
    {
        string fsBKDNGUBN = string.Empty;

        #region Descriptino : 페이지 로드
        public TYUSKB03C1(string sBKDNGUBN)
        {
            InitializeComponent();

            this.SetSpreadCodeHelper(this.FPS91_TY_S_US_919HR470, "BKDNGUBN", "BKDESC1", "BKDNGUBN");

            this.SetPopupStyle();

            // 파라미터값 가져오기 
            this.fsBKDNGUBN = sBKDNGUBN;
        }

        private void TYUSKB03C1_Load(object sender, System.EventArgs e)
        {
            // Key필드 수정모드시 잠금
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_919HR470, "BKDNGUBN");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_919HR470, "BKDNSTDAY");
            this.SetSpreadKeyColumn(this.FPS91_TY_S_US_919HR470, "BKDNEDDAY");

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);            

            this.FPS91_TY_S_US_919HR470.Initialize();

            this.CBH01_BKDNGUBN.SetValue(this.fsBKDNGUBN.ToString());

            // 조회
            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.CBH01_BKDNGUBN.CodeText);
        }
        #endregion

        #region Descriptino : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_919HR470.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_919H2468", this.CBH01_BKDNGUBN.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_919HR470.SetValue(dt);
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            int i = 0;

            DataTable dt = new DataTable();

            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();

            // 등록
            for (i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_US_91A8K473", ds.Tables[0].Rows[i]["BKDNGUBN"].ToString(),
                                                            ds.Tables[0].Rows[i]["BKDNSTDAY"].ToString(),
                                                            ds.Tables[0].Rows[i]["BKDNEDDAY"].ToString(),
                                                            ds.Tables[0].Rows[i]["BKDNDANGA"].ToString(),
                                                            TYUserInfo.EmpNo
                                                            );
            }

            // 수정
            for (i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_US_91A8M474", ds.Tables[1].Rows[i]["BKDNDANGA"].ToString(),
                                                            TYUserInfo.EmpNo,
                                                            ds.Tables[1].Rows[i]["BKDNGUBN"].ToString(),
                                                            ds.Tables[1].Rows[i]["BKDNSTDAY"].ToString(),
                                                            ds.Tables[1].Rows[i]["BKDNEDDAY"].ToString()
                                                            );
            }

            // 삭제
            for (i = 0; i < ds.Tables[2].Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_US_91A8O475", ds.Tables[2].Rows[i]["BKDNGUBN"].ToString(),
                                                            ds.Tables[2].Rows[i]["BKDNSTDAY"].ToString(),
                                                            ds.Tables[2].Rows[i]["BKDNEDDAY"].ToString()
                                                            );
            }

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_MR_2BF50354"); // 수정 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 처리 ProcessCheck
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            // 스프레드에서 저장 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_919HR470.GetDataSourceInclude(TSpread.TActionType.New,    "BKDNGUBN", "BKDNSTDAY", "BKDNEDDAY", "BKDNDANGA"));
            // 스프레드에서 수정 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_919HR470.GetDataSourceInclude(TSpread.TActionType.Update, "BKDNGUBN", "BKDNSTDAY", "BKDNEDDAY", "BKDNDANGA"));
            // 스프레드에서 삭제 할 항목들
            ds.Tables.Add(this.FPS91_TY_S_US_919HR470.GetDataSourceInclude(TSpread.TActionType.Remove, "BKDNGUBN", "BKDNSTDAY", "BKDNEDDAY"));

            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_MR_2BF4Z352");
                e.Successed = false;
                return;
            }

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (ds.Tables[0].Rows[0]["BKDNGUBN"].ToString() == "1")
                {
                    this.ShowMessage("TY_M_US_91ABK476");
                    this.SetFocus(this.CBH01_BKDNGUBN.CodeText);

                    e.Successed = false;
                    return;
                }
            }

            // 처리 하시겠습니까?
            if (!this.ShowMessage("TY_M_MR_2BF50353"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion
    }
}
