using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 채권연령분석 상세조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.07.20 15:47
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27K3Y165 : 채권 연령분석 상세내역 확인
    ///  TY_P_AC_27K3Z166 : 채권 연령분석 상세내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_27K41169 : 채권 연령분석 상세내역 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  BSCDAC : 계정과목
    ///  BSSAUP : 사업부
    ///  BSVEND : 거래처
    ///  BSGUGAN : 연령구간
    ///  BSYYMM : 년월
    /// </summary>
    public partial class TYACFS002S : TYBase
    {
        private string fsBSYYMM;
        private string fsBSSAUP;
        private string fsBSVEND;
        private string fsBSCDAC;

        #region Description : 폼 로드 이벤트
        public TYACFS002S(string sBSYYMM, string sBSSAUP, string sBSVEND, string sBSCDAC)
        {
            InitializeComponent();

            //this.SetPopupStyle();
            this.fsBSYYMM = sBSYYMM;
            this.fsBSSAUP = sBSSAUP;
            this.fsBSVEND = sBSVEND;
            this.fsBSCDAC = sBSCDAC;
        }

        private void TYACFS002S_Load(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(this.fsBSYYMM))
            {                
                this.DTP01_BSYYMM.SetValue(DateTime.Now.ToString("yyyyMM"));
            }
            else { this.DTP01_BSYYMM.SetValue(fsBSYYMM); }

            if (string.IsNullOrEmpty(this.fsBSSAUP) == false)
            {
                this.CBH01_BSSAUP.SetValue(fsBSSAUP);  
            }
            if (string.IsNullOrEmpty(this.fsBSVEND) == false)
            {
                this.CBH01_BSVEND.SetValue(fsBSVEND);
            }
            if (string.IsNullOrEmpty(this.fsBSCDAC) == false)
            {
                this.CBH01_BSCDAC.SetValue(fsBSCDAC);
            }

            this.CBH01_BSSAUP.DummyValue = this.DTP01_BSYYMM.GetValue() + "01";

            this.BTN61_INQ_Click(null, null);

            this.fsBSYYMM = "";
            this.fsBSSAUP = "";
            this.fsBSVEND = "";
            this.fsBSCDAC = "";
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_27K41169.Initialize();

            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(this.fsBSYYMM)) //조회
            {
                this.DbConnector.Attach("TY_P_AC_27K3Z166", this.ControlFactory, "01");
            }
            else //확인
            {
                this.DbConnector.Attach("TY_P_AC_27K3Y165", this.ControlFactory, "01");
            }
            this.FPS91_TY_S_AC_27K41169.SetValue(this.DbConnector.ExecuteDataTable());

            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_27K41169, "BSCDACNM", "합 계", SumRowType.Total);
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : DTP01_BSYYMM_ValueChanged 이벤트
        private void DTP01_BSYYMM_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_BSSAUP.DummyValue = this.DTP01_BSYYMM.GetValue() + "01";
        }
        #endregion

    }
}
