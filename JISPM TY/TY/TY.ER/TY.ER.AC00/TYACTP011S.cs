using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using System.Drawing; 

namespace TY.ER.AC00
{
    /// <summary>
    /// 원천세 항운노조 내역조회 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2014.11.12 08:42
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_4BCB9378 : 원천세 항운노조 내역조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_4BCB1379 : 원천세 항운노조 내역조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  WJBRANCH : 지점구분
    ///  WJREVYYMM : 귀속년월
    ///  WREYYMM : 귀속년월
    /// </summary>
    public partial class TYACTP011S : TYBase
    {
        private string fsCompanyCode = string.Empty;

        #region  Description : 폼 로드 이벤트
        public TYACTP011S()
        {
            InitializeComponent();
        }

        private void TYACTP011S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_WJREVYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_WREYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.UP_Set_JuminAuthCheck(CBO01_INQ_AUTH);

            this.SetStartingFocus(this.CBO01_WJBRANCH);

            this.FPS91_TY_S_AC_4BCB1379.Initialize();
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_4BCB9378", this.CBO01_WJBRANCH.GetValue(), this.DTP01_WJREVYYMM.GetString().Substring(0, 6), this.DTP01_WREYYMM.GetString().Substring(0, 6), TYUserInfo.SecureKey, CBO01_INQ_AUTH.GetValue().ToString() );
            this.FPS91_TY_S_AC_4BCB1379.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion
    }
}
