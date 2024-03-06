using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;


namespace TY.ER.BS00
{
    /// <summary>
    /// 사업계획 항운노조 요율.단가관리 복사 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.11.07 16:08
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_9B7GA485 : 사업계획 항운노조 요율.단가관리 복사
    ///  TY_P_AC_9B7GA486 : 사업계획 항운노조 요율.단가관리 중복확인
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_27J81133 : 복사 하시겠습니까?
    ///  TY_M_AC_27J83134 : 복사가 완료되었습니다
    ///  TY_M_US_9AOEX418 : 복사 년월에 데이터가 존재합니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYBSHU001B : TYBase
    {

        #region  Description : 폼 로드 이벤트
        public TYBSHU001B()
        {
            InitializeComponent();

            this.SetPopupStyle();

        }

        private void TYBSHU001B_Load(object sender, System.EventArgs e)
        {

            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));
            this.DTP01_EDATE.SetValue(DateTime.Now.AddYears(1).ToString("yyyy"));

            this.SetStartingFocus(this.DTP01_SDATE);
        }
        #endregion

        #region  Description : 처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_9B7GA485", DTP01_EDATE.GetString().ToString().Substring(0, 4),
                                                        TYUserInfo.EmpNo,
                                                        DTP01_SDATE.GetString().ToString().Substring(0, 4)
                );
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_AC_27J83134");
        }

        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (Convert.ToInt16(DTP01_SDATE.GetString().ToString().Substring(0, 4)) >= Convert.ToInt16(DTP01_EDATE.GetString().ToString().Substring(0, 4)))
            {
                this.SetFocus(this.DTP01_EDATE);
                this.ShowCustomMessage("복사년도가 기준년도보다 커야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }
           
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_9B7GA486", DTP01_EDATE.GetString().ToString().Substring(0,4));
            Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());
            if (iCnt > 0)
            {                    
                    this.ShowCustomMessage("복사년도에 이미 자료가 존재합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;                    
            }

            if (!this.ShowMessage("TY_M_AC_27J81133"))
            {
                e.Successed = false;
                return;
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
