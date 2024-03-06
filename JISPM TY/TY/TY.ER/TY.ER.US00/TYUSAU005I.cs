using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;


namespace TY.ER.US00
{
    /// <summary>
    /// 무인계근설정관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.04.04 16:46
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_944GW240 : 무인계근설정 확인
    ///  TY_P_US_944GW241 : 무인계근설정 수정
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  GIGAGUBN : 무인계근설정
    /// </summary>
    public partial class TYUSAU005I : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYUSAU005I()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYUSAU005I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            UP_DataBinding();

            this.SetStartingFocus(CBO01_GIGAGUBN);
        }
        #endregion

        #region  Description :  데이터 바인딩 이벤트
        private void UP_DataBinding()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_944GW240");
            DataTable dt = this.DbConnector.ExecuteDataTable();

            CBO01_GIGAGUBN.SetValue(dt.Rows[0]["GIGAGUBN"].ToString());
        }
        #endregion

        #region  Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_944GW241", CBO01_GIGAGUBN.GetValue().ToString());
            this.DbConnector.ExecuteTranQuery();

            UP_DataBinding();
            
            this.ShowMessage("TY_M_GB_23NAD873");
        }
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
