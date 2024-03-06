using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 무역파일번호 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.12.03 20:28
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_GB_2C38S817 : 무역파일번호 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2C38T818 : 무역파일번호 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  FILENO : 파일번호
    /// </summary>
    public partial class TYAZBJ01C6 : TYBase
    {

        public string fsFILENO;
        public string fsLCNO;
        public string fsBLNO;
        public string fsITEM_CODE;
        public string fsITEM_NAME;

        public string fsIN_FILENO;

        

        #region  Description : 폼 로드 이벤트
        public TYAZBJ01C6(string sFILENO)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsIN_FILENO = sFILENO;
        }
        #endregion

        private void TYAZBJ01C6_Load(object sender, System.EventArgs e)
        {
            this.SetStartingFocus(this.TXT01_FILENO);
            this.TXT01_FILENO.SetValue(fsIN_FILENO);
        }

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_2C38T818.Initialize();
 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_GB_2C38S817", this.TXT01_FILENO.GetValue().ToString(),  this.TXT01_BLNO.GetValue().ToString(), this.CBH01_VALUE34.GetValue());
            this.FPS91_TY_S_AC_2C38T818.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_2C38T818.CurrentRowCount > 0)
            {
                this.SetFocus(this.FPS91_TY_S_AC_2C38T818); 
            }
        }
        #endregion

        #region Description : FPS91_TY_S_AC_2C38T818_CellDoubleClick
        private void FPS91_TY_S_AC_2C38T818_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int row = (e == null ? 0 : e.Row);

            fsFILENO = this.FPS91_TY_S_AC_2C38T818.GetValue(row, "FILE_NO").ToString();
            fsLCNO = this.FPS91_TY_S_AC_2C38T818.GetValue(row, "LCNUMBER").ToString();
            fsBLNO = this.FPS91_TY_S_AC_2C38T818.GetValue(row, "BLNUMBER").ToString();
            fsITEM_CODE = this.FPS91_TY_S_AC_2C38T818.GetValue(row, "PUMMOK").ToString();
            fsITEM_NAME = this.FPS91_TY_S_AC_2C38T818.GetValue(row, "PUMMOKNM").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region Description : FPS91_TY_S_AC_2C38T818_KeyPress
        private void FPS91_TY_S_AC_2C38T818_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            int row = (e == null ? 0 : FPS91_TY_S_AC_2C38T818.ActiveSheet.ActiveRowIndex);

            fsFILENO = this.FPS91_TY_S_AC_2C38T818.GetValue(row, "FILE_NO").ToString();
            fsLCNO = this.FPS91_TY_S_AC_2C38T818.GetValue(row, "LCNUMBER").ToString();
            fsBLNO = this.FPS91_TY_S_AC_2C38T818.GetValue(row, "BLNUMBER").ToString();
            fsITEM_CODE = this.FPS91_TY_S_AC_2C38T818.GetValue(row, "PUMMOK").ToString();
            fsITEM_NAME = this.FPS91_TY_S_AC_2C38T818.GetValue(row, "PUMMOKNM").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion


        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

       

    }
}
