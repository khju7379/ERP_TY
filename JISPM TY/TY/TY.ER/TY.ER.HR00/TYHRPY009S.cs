using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여인상액관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.04.13 13:33
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_54DDM170 : 인사기본사사항 조회(급여인상액관리용)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_54DDN173 : 인사기본사항 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  INQ : 조회
    ///  KBSABUN : 사번
    ///  INQOPTION : 조회구분
    /// </summary>
    public partial class TYHRPY009S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRPY009S()
        {
            InitializeComponent();
        }

        private void TYHRPY009S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.SetStartingFocus(this.CBO01_KBCODE);
        }
        #endregion

        #region  Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            if (this.OpenModalPopup(new TYHRPY009I(ds.Tables[0],"","","","","","")) == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);  
            }
        }

        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_54DDN173.GetDataSourceInclude(TSpread.TActionType.Select, "KBSABUN", "KBHANGL", "KBJKCD", "KBHOBN", "KBBSTEAM", "KBBSTEAMNM"));

            if (ds.Tables[0].Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_25F59464");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_HR_54DEN177"))
            {
                e.Successed = false;
                return;
            }
            e.ArgData = ds;
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_54DDN173.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_54DDM170", this.CBH01_KBSABUN.GetValue().ToString(), this.CBO01_KBCODE.GetValue(), this.CBH01_KBJKCD.GetValue().ToString());
            this.FPS91_TY_S_HR_54DDN173.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
