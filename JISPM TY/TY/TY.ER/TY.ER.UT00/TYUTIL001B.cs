using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 탱크세척 요율표 복사 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2022.02.17 15:51
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_C2HEL077 : 탱크세척 요율표 복사
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_27J81133 : 복사 하시겠습니까?
    ///  TY_M_AC_27J83134 : 복사가 완료되었습니다
    ///  TY_M_AC_27J8T137 : 복사 월에 자료가 존재합니다 삭제후 작업하세요!
    ///  TY_M_HR_8AHFJ965 : 복사중 오류가 발생하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  COPY : 복사
    ///  COPYYYMM : 복사년월
    ///  YOYYMM : 년월
    /// </summary>
    public partial class TYUTIL001B : TYBase
    {
        string fsYOYYMM = string.Empty;

        #region Description : 폼 로드
        public TYUTIL001B(string sYOYYMM)
        {
            InitializeComponent();

            fsYOYYMM = sYOYYMM;
        }

        private void TYUTIL001B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_COPY.ProcessCheck += new TButton.CheckHandler(BTN61_COPY_ProcessCheck);

            this.DTP01_YOYYMM.SetValue(fsYOYYMM);
            this.DTP01_COPYYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_YOYYMM);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 복사 버튼
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            //복사
            this.DbConnector.Attach("TY_P_UT_C2HEL077", this.DTP01_COPYYYMM.GetString().ToString().Substring(0, 6),
                                                        TYUserInfo.EmpNo,
                                                        this.DTP01_YOYYMM.GetString().ToString().Substring(0, 6)
                                                        );

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_AC_27J83134");
        }
        #endregion

        #region Description : 복사 ProcessCheck 이벤트
        private void BTN61_COPY_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (this.DTP01_YOYYMM.GetString().ToString().Substring(0, 6) == this.DTP01_COPYYYMM.GetString().ToString().Substring(0, 6))
            {
                this.ShowCustomMessage("기준년월과 복사년월이 동일합니다", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_C2HEV078", this.DTP01_YOYYMM.GetString().ToString().Substring(0, 6));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_C3A9L140");

                e.Successed = false;
                return;
            }

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_C2HEV078", this.DTP01_COPYYYMM.GetString().ToString().Substring(0, 6));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_AC_27J8T137");

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
    }
}
