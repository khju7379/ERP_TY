using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여변동 옵션관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.12.09 16:35
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_5C9GN293 : 급여변동 옵션관리 조회
    ///  TY_P_HR_5C9GS296 : 급여변동관리 옵션코드 등록
    ///  TY_P_HR_5C9GS297 : 급여변동관리 옵션코드 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_5C9GO295 : 급여변동 옵션관리 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    /// </summary>
    public partial class TYHRPY14C1 : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRPY14C1()
        {
            InitializeComponent();
            SetPopupStyle();
        }

        private void TYHRPY14C1_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ_Click(null, null);
        }
        #endregion        

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_5C9GO295.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5C9GN293");
            this.FPS91_TY_S_HR_5C9GO295.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.UP_Save();

            this.Close();
        }
        #endregion

        #region  Description : DB 처리
        private void UP_Save()
        {

            if (this.FPS91_TY_S_HR_5C9GO295.CurrentRowCount > 0)
            {
                this.DbConnector.Attach("TY_P_HR_5C9GS297");
                this.DbConnector.ExecuteTranQuery();

                for (int i = 0; i < this.FPS91_TY_S_HR_5C9GO295.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_HR_5C9GO295.GetValue(i, "OPPAYCODE").ToString().Trim() == "True" || this.FPS91_TY_S_HR_5C9GO295.GetValue(i, "OPPAYCODE").ToString().Trim() == "Y" )
                    {
                        this.DbConnector.Attach("TY_P_HR_5C9GS296", this.FPS91_TY_S_HR_5C9GO295.GetValue(i, "PSDCODE").ToString().Trim(), TYUserInfo.EmpNo);
                    }
                }

                if (this.DbConnector.CommandCount > 0)
                {
                    this.DbConnector.ExecuteTranQueryList();
                }
            }
            
        }
        #endregion

        #region  Description : 폼 닫는 이벤트
        private void TYHRPY14C1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.UP_Save();
        }
        #endregion

    }
}
