using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 사내이자 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.09.05 16:19
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2954D822 : 사내이자 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2954E823 : 사내이자 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACNC022S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACNC022S()
        {
            InitializeComponent();
        }

        private void TYACNC022S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GSTYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));
            this.DTP01_GEDYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            
            this.SetStartingFocus(this.DTP01_GSTYYMM);

            this.BTN61_INQ_Click(null, null);  
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2954D822", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_2954E823.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion
    }
}
