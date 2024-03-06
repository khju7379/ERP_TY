using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 이체계좌정보 조회 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.03.16 20:35
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_53GKN683 : 이체계좌정보 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_53GKO684 : 이체계좌정보 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  COGUBN : 이체구분
    /// </summary>
    public partial class TYHRFR01C1 : TYBase
    {
        private string fsCOSABUN;
        private string fsCOGUBN;
        
        #region Description : 폼 로드 이벤트
        public TYHRFR01C1(string sCOSABUN, string sCOGUBN)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsCOSABUN  = sCOSABUN;
            fsCOGUBN   = sCOGUBN;
        }       
        private void TYHRFR01C1_Load(object sender, System.EventArgs e)
        {
            this.CBH01_COSABUN.SetValue(fsCOSABUN);
            this.CBH01_COGUBN.SetValue(fsCOGUBN);

            this.SetStartingFocus(this.CBH01_COGUBN);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_53KI9769.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53KI7767", this.CBH01_COSABUN.GetValue(), this.CBH01_COGUBN.GetValue());
            this.FPS91_TY_S_HR_53KI9769.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion       

    }
}
