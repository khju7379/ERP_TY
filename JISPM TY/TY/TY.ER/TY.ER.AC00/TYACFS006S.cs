using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 대손충당금 대상금액 상세 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.09 16:31
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28A8W353 : 대손충당금 대상금액 상세 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28A8Y354 : 대손충당금 대상금액 상세 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  BTSHCDAC : 상위계정
    ///  BTSSAUP : 사업부
    ///  BTSYYMM : 기준년월
    ///  BTSVEND : 거래처
    /// </summary>
    public partial class TYACFS006S : TYBase
    {
        private string fsBTSYYMM;
        private string fsBTSSAUP;
        private string fsBTSHCDAC;
        private string fsBTSVEND;

        #region Description : 폼 로드 이벤트
        public TYACFS006S(string sBTSYYMM, string sBTSSAUP, string sBTSHCDAC, string sBTSVEND )
        {
            InitializeComponent();

            //this.SetPopupStyle();

            this.fsBTSYYMM = sBTSYYMM;
            this.fsBTSSAUP = sBTSSAUP;
            this.fsBTSHCDAC = sBTSHCDAC;
            this.fsBTSVEND = sBTSVEND;
        }

        private void TYACFS006S_Load(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(this.fsBTSYYMM))
            {
                this.DTP01_BTSYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            }
            else { this.DTP01_BTSYYMM.SetValue(fsBTSYYMM); }

            if (string.IsNullOrEmpty(this.fsBTSSAUP) == false)
            {
                this.CBH01_BTSSAUP.SetValue(fsBTSSAUP);
            }
            if (string.IsNullOrEmpty(this.fsBTSVEND) == false)
            {
                this.CBH01_BTSVEND.SetValue(fsBTSVEND);
            }
            if (string.IsNullOrEmpty(this.fsBTSHCDAC) == false)
            {
                this.CBH01_BTSHCDAC.SetValue(fsBTSHCDAC);
            }

            this.CBH01_BTSSAUP.DummyValue = this.DTP01_BTSYYMM.GetValue() + "01";

            this.BTN61_INQ_Click(null, null);

            this.fsBTSYYMM = "";
            this.fsBTSSAUP = "";
            this.fsBTSHCDAC = "";
            this.fsBTSVEND = "";            
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_28A8Y354.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_28A8W353", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_28A8Y354.SetValue(this.DbConnector.ExecuteDataTable());

            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_28A8Y354, "BTSHCDACNM", "합 계", SumRowType.Total, "BTSBONDAMT", "BTSDSNORAMT1", "BTSCHNORAMT1","BTSDSNORAMT2","BTSCHNORAMT2","BTSDSNORAMT3","BTSCHNORAMT3","BTSDSIDAYAMT","BTSCHIDAYAMT","BTSTOTAL","BTSBONDJANAMT");
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : DTP01_BTSYYMM_ValueChanged 이벤트
        private void DTP01_BTSYYMM_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_BTSSAUP.DummyValue = this.DTP01_BTSYYMM.GetValue() + "01";
        }
        #endregion
    }
}
