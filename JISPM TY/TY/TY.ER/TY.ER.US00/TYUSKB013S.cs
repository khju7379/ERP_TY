using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.US00
{
    /// <summary>
    /// 모선 스케줄관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.02.25 09:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_92P92882 : 모선 스케줄관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_92P93883 : 모선 스케줄관리 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUSKB013S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSKB013S()
        {
            InitializeComponent();
        }

        private void TYUSKB013S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy") + "1231");

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_92P93883.Initialize();            

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                 "TY_P_US_92P92882",
                 this.DTP01_SDATE.GetString().ToString(),
                 this.DTP01_EDATE.GetString().ToString()
                );

            this.FPS91_TY_S_US_92P93883.SetValue(this.DbConnector.ExecuteDataTable());          
        }
        #endregion        

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYUSKB013I(string.Empty, string.Empty)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {

            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_US_92PBH885", dt.Rows[i]["SHDATE"].ToString().Replace("-", ""), dt.Rows[i]["SHSEQ"].ToString());
                }
                this.DbConnector.ExecuteTranQueryList();
            }           
           
            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int iCnt = 0;

            DataTable dt = this.FPS91_TY_S_US_92P93883.GetDataSourceInclude(TSpread.TActionType.Remove, "SHDATE", "SHSEQ");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }
            
            //입항관리 등록 유무 체크
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_US_92QEH911", dt.Rows[i]["SHDATE"].ToString().Replace("-", ""), dt.Rows[i]["SHSEQ"].ToString());
                iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());

                if (iCnt > 0)
                {
                    this.ShowCustomMessage("입항관리에 등록되어 있습니다! 삭제 할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

        #region Description : FPS91_TY_S_US_92P93883_CellDoubleClick 버튼
        private void FPS91_TY_S_US_92P93883_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.OpenModalPopup(new TYUSKB013I(this.FPS91_TY_S_US_92P93883.GetValue("SHDATE").ToString(), this.FPS91_TY_S_US_92P93883.GetValue("SHSEQ").ToString())) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}