using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 개별승인 전표관리 미승인전표 조회 팝업 프로그램입니다.
    /// 
    /// 작성자 : 김종술
    /// 작성일 : 2012.05.09 09:27
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25752081 : 미승인 자료 조회(팝업) - 개별승인
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2594S126 : 개별승인 팝업(미승인전표 조회)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  CNDB2DPMK : 전표번호_부서
    ///  CNDB2DTMK : 전표번호_일자
    ///  CNDB2NOSQ : 전표번호_번호
    /// </summary>
    public partial class TYACBJ002I : TYBase
    {
        public string sDPMK, sDTMK, sNOSQ;

        public TYACBJ002I(string sINDPMK ,string sINDTMK ,string sINNOSQ)
        {
            InitializeComponent();

            this.SetPopupStyle();

            sDPMK = sINDPMK;
            sDTMK = sINDTMK;
            sNOSQ = sINNOSQ;

        }

        #region Description : Page_Load
        private void TYACBJ002I_Load(object sender, System.EventArgs e)
        {
            this.CBH01_CNDB2DPMK.SetValue(sDPMK);
            this.DTP01_CNDB2DTMK.SetValue(sDTMK);
            this.TXT01_CNDB2NOSQ.SetValue(sNOSQ);

            this.BTN61_INQ_Click(null, null);
        }

        #endregion

        #region Description : 닫기
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 조회
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            //this.DbConnector.Attach("TY_P_AC_25752081", this.ControlFactory, "01");
            this.DbConnector.Attach("TY_P_AC_25752081", this.CBH01_CNDB2DPMK.GetValue().ToString(), this.DTP01_CNDB2DTMK.GetValue().ToString(),
                                    this.DTP01_CNDB2DTMK.GetValue().ToString(), this.TXT01_CNDB2NOSQ.GetValue().ToString(), this.TXT01_CNDB2NOSQ.GetValue().ToString());

            this.FPS91_TY_S_AC_2594S126.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region Description : 그리드 선택시 처리 [자료 넘겨줌]
        private void FPS91_TY_S_AC_2594S126_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int row = (e == null ? 0 : e.Row);

            sDPMK = this.FPS91_TY_S_AC_2594S126.GetValue(row, "B1DPMK").ToString();
            sDTMK = this.FPS91_TY_S_AC_2594S126.GetValue(row, "B1DTMK").ToString();
            sNOSQ = this.FPS91_TY_S_AC_2594S126.GetValue(row, "B1NOSQ").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        } 
        #endregion
    }
}
