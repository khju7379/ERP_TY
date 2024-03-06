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
    /// 입금표관리 팝업(입금표조회) 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.11.02 09:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2B2BH006 : 입금표 내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2B2BK009 : 입금표 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  TRLOCAL : 지　　역
    ///  TRSEQ : 순　　번
    ///  TRYEAR : 년　　도
    /// </summary>
    public partial class TYACBJ001M : TYBase
    {
        public string sLOCAL, sYEAR, sCDSB, sTRSEQ ;

        public TYACBJ001M(string sARLOCAL, string sARYEAR, string sARCDSB , string sTRSEQ)
        {
            InitializeComponent();

            this.SetPopupStyle();

            sLOCAL = sARLOCAL;
            sYEAR = sARYEAR;
            sCDSB = sARCDSB;
        }

        #region Descriontion : Page_Load
        private void TYACBJ001M_Load(object sender, System.EventArgs e)
        {
            this.TXT01_TRYEAR.SetValue(sYEAR);
            this.TXT01_TRDPMK.SetValue(sLOCAL);
            this.TXT01_TRCDSB.SetValue(sCDSB);

            this.BTN61_INQ_Click(null, null);
        } 
        #endregion


        #region Descriontion : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2B2BH006", this.TXT01_TRYEAR.GetValue().ToString(), this.TXT01_TRDPMK.GetValue().ToString(), this.TXT01_TRCDSB.GetValue().ToString());
            this.FPS91_TY_S_AC_2B2BK009.SetValue(this.DbConnector.ExecuteDataTable());
        } 
        #endregion

        #region Descriontion : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 그리드 선택시 이벤트
        private void FPS91_TY_S_AC_2B2BK009_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int row = (e == null ? 0 : e.Row);

            sYEAR = this.FPS91_TY_S_AC_2B2BK009.GetValue(row, "ARYEAR").ToString();
            sLOCAL = this.FPS91_TY_S_AC_2B2BK009.GetValue(row, "ARDPMK").ToString();
            sTRSEQ = this.FPS91_TY_S_AC_2B2BK009.GetValue(row, "ARSEQ").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        } 
        #endregion
    }
}
