using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 은행별잔액현황조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.04.17 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_24HCV705 : 은행별잔액조회
    ///  TY_P_AC_24HBO703 : 월 관리대장(AMGMMMF) DR,CR 가져오는 SP
    ///  TY_P_AC_24HBS704 : 일 관리대장(AMGDDMF) DR,CR 가져오는 SP
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_24H14709 : 은행별잔액현황조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  DATE : 일자
    /// </summary>
    public partial class TYACDE002S : TYBase
    {
        #region Description : 페이지 로드
        public TYACDE002S()
        {
            InitializeComponent();
        }

        private void TYACDE002S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.DTP01_DATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            //string sC4DR = string.Empty;
            //string sC4CR = string.Empty;
            //string sC3DR = string.Empty;
            //string sC3CR = string.Empty;

            //this.DbConnector.CommandClear();
            //// 월 관리대장 DR 가져오는 SP
            //this.DbConnector.Attach("TY_P_AC_24HBO703", this.DTP01_DATE.GetValue().ToString().Substring(5, 2), "DR", "");
            //// 월 관리대장 CR 가져오는 SP
            //this.DbConnector.Attach("TY_P_AC_24HBO703", this.DTP01_DATE.GetValue().ToString().Substring(5, 2), "CR", "");
            //// 일 관리대장 DR 가져오는 SP
            //this.DbConnector.Attach("TY_P_AC_24HBS704", this.DTP01_DATE.GetValue().ToString().Substring(8, 2), "DR", "");
            //// 일 관리대장 CR 가져오는 SP
            //this.DbConnector.Attach("TY_P_AC_24HBS704", this.DTP01_DATE.GetValue().ToString().Substring(8, 2), "CR", "");


            //// SP의 OUTPUT 값 가져오는 부분
            //sC4DR = Convert.ToString(this.DbConnector.ExecuteScalar(0));
            //sC4CR = Convert.ToString(this.DbConnector.ExecuteScalar(1));
            //sC3DR = Convert.ToString(this.DbConnector.ExecuteScalar(2));
            //sC3CR = Convert.ToString(this.DbConnector.ExecuteScalar(3));

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_24HCV705",
                this.DTP01_DATE.GetValue().ToString().Substring(0, 4),
                this.DTP01_DATE.GetValue().ToString().Substring(5, 2),
                this.DTP01_DATE.GetValue().ToString().Substring(8, 2)
                );

            this.FPS91_TY_S_AC_24H14709.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion
    }
}