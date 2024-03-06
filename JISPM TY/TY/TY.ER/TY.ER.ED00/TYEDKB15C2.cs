using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.ED00
{
    /// <summary>
    /// 우편번호 검색 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2020.06.05 13:16
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_A65DQ620 : 우편번호 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_A65DR621 : 우편번호 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  PCITY : 시도
    ///  PCITYGUN : 시군구
    ///  PHJDONGNM : 행정동명
    ///  PROADNAME : 도로명
    /// </summary>
    public partial class TYEDKB15C2 : TYBase
    {
        public string fsPOSTNUM;

        #region  Description : 폼 로드 이벤트
        public TYEDKB15C2()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYEDKB15C2_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            this.SetStartingFocus(TXT01_PROADNAME);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_A65DR621.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A65DQ620", 
                                                        TXT01_PROADNAME.GetValue().ToString(),
                                                        TXT01_PHJDONGNM.GetValue().ToString());
            this.FPS91_TY_S_UT_A65DR621.SetValue(this.DbConnector.ExecuteDataTable());           
        }
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (
                TXT01_PROADNAME.GetValue().ToString() == "" &&
                TXT01_PHJDONGNM.GetValue().ToString() == "")
            {
                this.ShowCustomMessage("조회 조건에 한가지를 반드시 입력해야합니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return; 
            }
        }
        #endregion

        #region  Description : FPS91_TY_S_UT_A65DR621_CellDoubleClick 이벤트
        private void FPS91_TY_S_UT_A65DR621_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsPOSTNUM = this.FPS91_TY_S_UT_A65DR621.GetValue("POSTNUM").ToString();

            BTN61_CLO_Click(null, null);
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        

    }
}
