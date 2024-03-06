using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 고정자산 자산번호 조회[팝업] 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.12.12 11:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2CC1D105 : 고정자산 디테일 조회[팝업]
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2CC1G106 : 고정자산 디테일 조회[팝업]
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  FXMLASCODE : 자산구분
    ///  FXSAUP : 귀속사업부
    ///  FXGETDATE : 취득일자
    ///  FXSNAME : 자산명
    /// </summary>
    public partial class TYAZHF05C1 : TYBase
    {
        public string fsASNUM = "";

        #region  Description : 폼로드 이벤트
        public TYAZHF05C1()
        {
            InitializeComponent();
        }

        private void TYAZHF05C1_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_AC_2CC1G106.Initialize();

            CBH01_FXSAUP.DummyValue = "19900101";
            
            this.SetStartingFocus(this.CBH01_FXMLASCODE.CodeText);            
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
            this.FPS91_TY_S_AC_2CC1G106.Initialize();
 
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2CC1D105", this.CBH01_FXMLASCODE.GetValue().ToString(), 
                                                        this.CBH01_FXSAUP.GetValue().ToString(), this.TXT01_FXSYEAR.GetValue(), this.TXT01_FXSNAME.GetValue());
            this.FPS91_TY_S_AC_2CC1G106.SetValue(this.DbConnector.ExecuteDataTable());
                        
            if (this.FPS91_TY_S_AC_2CC1G106.CurrentRowCount > 0)
            {
                this.FPS91_TY_S_AC_2CC1G106.Focus(); 
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
                return;
            }
        }
        #endregion      

        #region  Description : FPS91_TY_S_AC_2CC1G106_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_2CC1G106_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsASNUM = this.FPS91_TY_S_AC_2CC1G106.GetValue("FXFULLNUM").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_2CC1G106_KeyPress 이벤트
        private void FPS91_TY_S_AC_2CC1G106_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            this.FPS91_TY_S_AC_2CC1G106_CellDoubleClick(null, null);
        }
        #endregion
    }
}
