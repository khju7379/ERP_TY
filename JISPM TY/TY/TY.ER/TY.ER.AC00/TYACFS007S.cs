using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 대손충당금 설정상세 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.10 16:34
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28EAI383 : 대손충당금 설정상세 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28EAJ384 : 대손충당금 설정상세 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  BJSHCDAC : 상위계정
    ///  BJSSAUP : 사업부
    ///  BJSYYMM : 기준년월
    ///  BJSVEND : 거래처
    /// </summary>
    public partial class TYACFS007S : TYBase
    {
       private string fsBJSYYMM;
       private string fsBJSSAUP;
	   private string fsBJSHCDAC;

       #region Description : 폼 로드 이벤트
       public TYACFS007S(string sBJSYYMM, string sBJSSAUP, string sBJSHCDAC )
        {
            InitializeComponent();

            //this.SetPopupStyle();

            this.fsBJSYYMM = sBJSYYMM;
            this.fsBJSSAUP = sBJSSAUP;
            this.fsBJSHCDAC = sBJSHCDAC;
        }

        private void TYACFS007S_Load(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(this.fsBJSYYMM))
            {
                this.DTP01_BJSYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));
            }
            else { this.DTP01_BJSYYMM.SetValue(fsBJSYYMM); }

            if (string.IsNullOrEmpty(this.fsBJSSAUP) == false)
            {
                this.CBH01_BJSSAUP.SetValue(fsBJSSAUP);
            }
            if (string.IsNullOrEmpty(this.fsBJSHCDAC) == false)
            {
                this.CBH01_BJSHCDAC.SetValue(fsBJSHCDAC);
            }

            this.CBH01_BJSSAUP.DummyValue = this.DTP01_BJSYYMM.GetValue() + "01";

            this.BTN61_INQ_Click(null, null);

            this.fsBJSYYMM = "";
            this.fsBJSSAUP = "";
            this.fsBJSHCDAC = "";
            
        }
       #endregion

        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_28EAJ384.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_28EAI383", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_28EAJ384.SetValue(this.DbConnector.ExecuteDataTable());

            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_28EAJ384, "BJSHCDACNM", "합 계", SumRowType.Total);
        }
        #endregion

        #region Description : DTP01_BJSYYMM_ValueChanged 버튼 이벤트
        private void DTP01_BJSYYMM_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_BJSSAUP.DummyValue = this.DTP01_BJSYYMM.GetValue() + "01";
        }
        #endregion
    }
}
