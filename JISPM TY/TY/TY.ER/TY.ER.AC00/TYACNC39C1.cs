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
    /// 구분손익 제외전표 조회 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2018.12.18 11:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_8CIBV343 : 구분손익 제외전표관리 승인전표 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_8CIBW344 : 구분손익 제외전표관리 승인전표 조회 팝업
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    ///  B2DPMK : 작성부서
    /// </summary>
    public partial class TYACNC39C1 : TYBase
    {

        public string fsB2JPNO = string.Empty;

        private string fsGubn = string.Empty;

        public TYACNC39C1(string sGubn)
        {
            this.SetPopupStyle();
            InitializeComponent();

            fsGubn = sGubn;
        }

        #region Descriontion : Page_Load
        private void TYACNC39C1_Load(object sender, System.EventArgs e)
        {           
            this.DTP01_GSTDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_B2DPMK.DummyValue = this.DTP01_GSTDATE.GetString();

            this.BTN61_INQ_Click(null, null);
        } 
        #endregion       

        #region Descriontion : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_8CIBW344.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_8CIBV343", this.DTP01_GSTDATE.GetString().ToString(), this.DTP01_GEDDATE.GetString().ToString(), CBH01_B2DPMK.GetValue(), CBH01_B2CDAC.GetValue(), fsGubn);
            this.FPS91_TY_S_AC_8CIBW344.SetValue(this.DbConnector.ExecuteDataTable());
        } 
        #endregion

        #region Descriontion : FPS91_TY_S_AC_8CIBW344_CellDoubleClick 버튼
        private void FPS91_TY_S_AC_8CIBW344_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsB2JPNO = this.FPS91_TY_S_AC_8CIBW344.GetValue("B2JPNO").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Descriontion : DTP01_GSTDATE_ValueChanged 버튼
        private void DTP01_GSTDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_B2DPMK.DummyValue = this.DTP01_GSTDATE.GetString();
        }
        #endregion

        #region Descriontion : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

       
    }
}
