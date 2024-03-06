using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 폐기물 탱크 재고 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.11.25 14:36
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_6BPEL875 : 폐기물 탱크 재고 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_6BPEM876 : 폐기물 탱크 재고 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYUTDI002S : TYBase
    {
        #region Description : 페이지 로드
        public TYUTDI002S()
        {
            InitializeComponent();
        }

        private void TYUTDI002S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            this.BTN61_INQ_Click(null, null);
            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_6BPEM876.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_6BPEL875", this.DTP01_STDATE.GetString(),
                                                        this.DTP01_EDDATE.GetString());

            this.FPS91_TY_S_UT_6BPEM876.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 조회 체크
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            string STDATE = this.DTP01_STDATE.GetString();
            string EDDATE = this.DTP01_EDDATE.GetString();

            if (Convert.ToInt32(STDATE) > Convert.ToInt32(EDDATE))
            {
                this.DTP01_STDATE.Focus();
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다 .", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}
