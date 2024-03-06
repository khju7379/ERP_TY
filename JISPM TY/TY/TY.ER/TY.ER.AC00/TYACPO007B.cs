using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 품목별분류관리 복사 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.07.11 16:20
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_37B4G087 : EIS 품목분류관리 복사
    ///  TY_P_AC_37B4G088 : EIS 품목분류관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_27J81133 : 복사 하시겠습니까?
    ///  TY_M_AC_27J83134 : 복사가 완료되었습니다
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACPO007B : TYBase
    {
        #region  Description : 폼로드 이벤트
        public TYACPO007B()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        private void TYACPO007B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM"));

            this.DTP01_GEDYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region  Description : 복사 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
                //삭제
            	this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_37B4G088", this.DTP01_GEDYYMM.GetString().ToString().Substring(0, 6));   
                this.DbConnector.ExecuteTranQuery();
               //복사
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_37B4G087", this.DTP01_GEDYYMM.GetString().ToString().Substring(0, 6), TYUserInfo.EmpNo, this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
                this.DbConnector.ExecuteTranQuery();

                this.ShowMessage("TY_M_AC_27J83134");
        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 처리 체크
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (!this.ShowMessage("TY_M_AC_27J81133"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}
