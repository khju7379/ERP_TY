using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.AC00
{
    /// <summary>
    /// 프로젝트 이자관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2020.11.03 17:21
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_AB3FR105 : 프로젝트 이자관리 조회
    ///  TY_P_AC_AB3H3108 : 프로젝트 월별 이자 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_AB3FY106 : 프로젝트 이자관리 조회
    ///  TY_S_AC_AB3H4109 : 프로젝트 월별이자 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYACNC015S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACNC015S()
        {
            InitializeComponent();
        }

        private void TYACNC015S_Load(object sender, System.EventArgs e)
        {

            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-12).ToString("yyyy-MM"));
            DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_AB3FY106.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_AB3FR105", this.DTP01_SDATE.GetString().Substring(0, 6), this.DTP01_EDATE.GetString().Substring(0, 6), "");
            this.FPS91_TY_S_AC_AB3FY106.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACNC015I(string.Empty,string.Empty,string.Empty)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_AB3FY106_CellClick 이벤트
        private void FPS91_TY_S_AC_AB3FY106_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //this.FPS91_TY_S_AC_AB49S115.Initialize();

            //this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_AB49S114", this.FPS91_TY_S_AC_AB3FY106.GetValue("AJNPJGB").ToString(),
            //                                            this.FPS91_TY_S_AC_AB3FY106.GetValue("AJNDATE").ToString(),
            //                                            this.FPS91_TY_S_AC_AB3FY106.GetValue("AJNDPAC").ToString());
            //this.FPS91_TY_S_AC_AB49S115.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_AB3FY106_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_AB3FY106_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.OpenModalPopup(new TYACNC015I(this.FPS91_TY_S_AC_AB3FY106.GetValue("AJNPJGB").ToString(),
                                                   this.FPS91_TY_S_AC_AB3FY106.GetValue("AJNDATE").ToString(),
                                                   this.FPS91_TY_S_AC_AB3FY106.GetValue("AJNDPAC").ToString()
                                                    )) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;            
            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_AB4A9118", dt); //마스타 삭제
            this.DbConnector.Attach("TY_P_AC_AB5D4135", dt); //차입이자율관리 삭제
            this.DbConnector.ExecuteNonQueryList();


            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);

        }
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_AB3FY106.GetDataSourceInclude(TSpread.TActionType.Remove, "AJNPJGB", "AJNDATE", "AJNDPAC");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();
                    this.DbConnector.Attach("TY_P_AC_AB5D0134", dt.Rows[i]["AJNPJGB"].ToString(),
                                                                dt.Rows[i]["AJNDATE"].ToString(),
                                                                dt.Rows[i]["AJNDPAC"].ToString());
                    Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());

                    if (iCnt > 0)
                    {
                        this.ShowCustomMessage("프로젝트 이자 생성자료가 존재합니다 삭제할수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        e.Successed = false;
                        return;
                    }
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
    }
}
