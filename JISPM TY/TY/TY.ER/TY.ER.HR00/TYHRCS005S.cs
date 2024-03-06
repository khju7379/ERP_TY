using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 외부식수관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2016.02.19 17:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_62JHW544 : 외부식수관리 조회
    ///  TY_P_HR_62M8T547 : 외부식수관리 확인
    ///  TY_P_HR_62M8W549 : 외부식수관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_62JHX546 : 외부식수관리 조회
    ///  TY_S_HR_62M8T548 : 외부식수관리 확인
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  EDATE : 종료일자
    ///  FDGEORE : 업체명
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRCS005S : TYBase
    {

        private string fsFDDATE = string.Empty;
        private string fsFDGUBUN = string.Empty;
        private string fsFDVNCODE = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYHRCS005S()
        {
            InitializeComponent();
        }

        private void TYHRCS005S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.DTP01_STDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));


            this.SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_62JHX546.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_62JHW544", this.DTP01_STDATE.GetString(), this.DTP01_EDDATE.GetString(), CBO01_FDGUBUN.GetValue().ToString() , TXT01_FDGEORE.GetValue().ToString());
            this.FPS91_TY_S_HR_62JHX546.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYHRCS005I("","","",""
                                                   )) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_62M8W549", dt);
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.UP_DataBinding();
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_HR_62M8T548.GetDataSourceInclude(TSpread.TActionType.Remove, "FDDATE", "FDGUBUN", "FDCNT", "FDGATEGN");

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

        #region Description : FPS91_TY_S_HR_62JHX546_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_62JHX546_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsFDDATE = this.FPS91_TY_S_HR_62JHX546.GetValue("FDDATE").ToString();
            fsFDGUBUN = this.FPS91_TY_S_HR_62JHX546.GetValue("FDGEORE").ToString();
            fsFDVNCODE = this.FPS91_TY_S_HR_62JHX546.GetValue("FDVNCODE").ToString();

            this.UP_DataBinding();

        }
        #endregion

        #region Description : FPS91_TY_S_HR_62M8T548_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_62M8T548_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.OpenModalPopup(new TYHRCS005I(this.FPS91_TY_S_HR_62M8T548.GetValue("FDDATE").ToString(),
                                                   this.FPS91_TY_S_HR_62M8T548.GetValue("FDGUBUN").ToString(),
                                                   this.FPS91_TY_S_HR_62M8T548.GetValue("FDCNT").ToString(),
                                                   this.FPS91_TY_S_HR_62M8T548.GetValue("FDGATEGN").ToString()
                                                   )) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : UP_DataBinding 이벤트
        private void UP_DataBinding()
        {
            if (fsFDVNCODE.Trim() != "")
            {
                fsFDGUBUN = "";
            }
            else
            {
                fsFDVNCODE = "";
            }

            this.FPS91_TY_S_HR_62M8T548.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_62M8T547", fsFDDATE, fsFDGUBUN, fsFDVNCODE);
            this.FPS91_TY_S_HR_62M8T548.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

    }
}
