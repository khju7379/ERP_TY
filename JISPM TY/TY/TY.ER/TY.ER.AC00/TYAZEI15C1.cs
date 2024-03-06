using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 지급어음번호 조회[팝업] 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.01.24 09:01
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_31OBD887 : 지급어음번호 조회[팝업]
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_31OBE889 : 지급어음번호 조회[팝업]
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  F5NONC : 용지번호
    /// </summary>
    public partial class TYAZEI15C1 : TYBase
    {

        public string fsF5NONC;
        public string fsF5CLNC;

        #region  Description : 종료 버튼 이벤트
        public TYAZEI15C1()
        {
            InitializeComponent();
        }

        private void TYAZEI15C1_Load(object sender, System.EventArgs e)
        {
            this.SetStartingFocus(this.TXT01_F5NONC); 
        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_31OBD887", this.TXT01_F5NONC.GetValue().ToString() );
            this.FPS91_TY_S_AC_31OBE889.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_31OBE889_CellDoubleClick
        private void FPS91_TY_S_AC_31OBE889_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int row = (e == null ? 0 : e.Row);

            fsF5NONC = this.FPS91_TY_S_AC_31OBE889.GetValue(row, "F5NONC").ToString();
            fsF5CLNC = this.FPS91_TY_S_AC_31OBE889.GetValue(row, "F5CLNC").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();

        }
        #endregion

        #region  Description : FPS91_TY_S_AC_31OBE889_KeyPress
        private void FPS91_TY_S_AC_31OBE889_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            int row = (e == null ? 0 : FPS91_TY_S_AC_31OBE889.ActiveSheet.ActiveRowIndex);

            fsF5NONC = this.FPS91_TY_S_AC_31OBE889.GetValue(row, "F5NONC").ToString();
            fsF5CLNC = this.FPS91_TY_S_AC_31OBE889.GetValue(row, "F5CLNC").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

    }
}
