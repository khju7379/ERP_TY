using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 전자세금계산서 발송 이력 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.06.07 11:04
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  MCHWAJU : 화주
    ///  MCDATE : 매출일자
    ///  MCJPNO : 전표번호
    /// </summary>
    public partial class TYUTME24C2 : TYBase
    {
        private string fsMCDATE;
        private string fsMCHWAJU;
        private string fsMCJPNO;
        private string fsCONVERSATION_ID;

        #region  Description : 폼 로드 이벤트
        public TYUTME24C2(string sMCDATE, string sMCHWAJU, string sMCJPNO,  string sCONVERSATION_ID)
        {
            InitializeComponent();

            this.SetPopupStyle();

            fsMCDATE = sMCDATE;
            fsMCHWAJU = sMCHWAJU;
            fsMCJPNO = sMCJPNO;
            fsCONVERSATION_ID = sCONVERSATION_ID;

        }

        private void TYUTME24C2_Load(object sender, System.EventArgs e)
        {
            this.DTP01_MCDATE.SetValue(fsMCDATE);
            if (fsMCJPNO.Substring(0, 1) != "S")
            {
                this.CBH01_MCHWAJU.SetValue(fsMCHWAJU);
                this.CBH01_IHHWAJU.Visible = false;
            }
            else
            {
                this.CBH01_IHHWAJU.SetValue(fsMCHWAJU);
                this.CBH01_MCHWAJU.Visible = false;
            }
            
            this.UP_DataBinding();
        }
        #endregion

        #region  Description : 데이타 바인딩 버튼 이벤트
        private void UP_DataBinding()
        {
            FPS91_TY_S_UT_767DA720.Initialize();

            if (fsCONVERSATION_ID.Length > 0)
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_767D5719", fsMCJPNO, fsCONVERSATION_ID.Substring(0, 28));
                FPS91_TY_S_UT_767DA720.SetValue(this.DbConnector.ExecuteDataTable());
            }
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
