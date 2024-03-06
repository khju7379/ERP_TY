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
    /// EIS 유형자산현황 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2013.08.06 14:52
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_38GBQ379 : EIS 유형자산현황 조회
    ///  TY_P_AC_38GBR380 : EIS 유형자산현황 등록
    ///  TY_P_AC_38GBS381 : EIS 유형자산현황 수정
    ///  TY_P_AC_38GBS382 : EIS 유형자산현황 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_38GBT383 : EIS 유형자산현황 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  EDACDDP : 사업부
    ///  EDAYYMM : 년월
    /// </summary>
    public partial class TYACPO004S : TYBase
    {
        public TYACPO004S()
        {
            InitializeComponent();
        }

        #region Description : Page_Load
        private void TYACPO004S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_EDAYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.SetStartingFocus(this.DTP01_EDAYYMM);
        } 
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_38GBT383.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_38GBQ379", this.DTP01_EDAYYMM.GetString().ToString().Substring(0,6), this.CBO01_EDACDDP.GetValue());
            this.FPS91_TY_S_AC_38GBT383.SetValue(this.DbConnector.ExecuteDataTable());
        } 
        #endregion
    }
}
