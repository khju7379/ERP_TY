using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.AC00
{
    /// <summary>
    /// 미지급금 관리 팝업-반제체크 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.10 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25A49237 : 미지급금 - 반제 체크
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25A40238 : 미지급금 - 반제 체크
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  M1DTED : 지급일자
    /// </summary>
    public partial class TYACFP002C : TYBase
    {
        #region Description : 페이지 로드
        public TYACFP002C(string sM1DTED)
        {
            InitializeComponent();

            this.SetPopupStyle();

            // 파라미터값 가져오기
            this.TXT01_M1DTED.SetValue(sM1DTED);
        }

        private void TYACFP002C_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_25A49237",
                 this.TXT01_M1DTED.GetValue().ToString().Replace("-","")
                );

            this.FPS91_TY_S_AC_25A40238.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion
    }
}
