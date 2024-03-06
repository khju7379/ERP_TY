using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.US00
{
    /// <summary>
    /// 항운노조원 명부 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.04.03 09:07
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_9439H225 : 항운노조 명부 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_9439I226 : 항운노조 명부 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUSNJ009S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSNJ009S()
        {
            InitializeComponent();
        }

        private void TYUSNJ009S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_US_9439I226.Visible = true;            

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));            

            SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_9439I226.Initialize();            

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                    "TY_P_US_9439H225",
                    TYUserInfo.SecureKey, "Y",
                    TYUserInfo.SecureKey, "Y",
                    this.DTP01_SDATE.GetString().ToString().Substring(0,6)                     
                );

            this.FPS91_TY_S_US_9439I226.SetValue(this.DbConnector.ExecuteDataTable());                
           
        }
        #endregion            

     

    }
}