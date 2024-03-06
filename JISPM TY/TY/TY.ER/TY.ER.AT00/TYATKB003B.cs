using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;

namespace TY.ER.AT00
{
    /// <summary>
    /// 사택월별 요금 복사 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.10.17 10:25
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_88VEP676 : 사택월별 요금관리 내역 조회
    ///  TY_P_HR_88VEQ677 : 사택월별 요금관리 마스타 확인
    ///  TY_P_HR_8AHEV960 : 사택월별 요금관리 마스타 복사
    ///  TY_P_HR_8AHF0961 : 사택월별 요금관리 내역 복사
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_27J81133 : 복사 하시겠습니까?
    ///  TY_M_AC_27J83134 : 복사가 완료되었습니다
    ///  TY_M_AC_27J8T137 : 복사 월에 자료가 존재합니다 삭제후 작업하세요!
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  COPY : 복사
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYATKB003B : TYBase
    {
        #region Description : 폼 로드
        public TYATKB003B()
        {
            InitializeComponent();
        }

        private void TYATKB003B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_COPY.ProcessCheck += new TButton.CheckHandler(BTN61_COPY_ProcessCheck);

            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 복사 버튼
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            try
            {
                //마스타 복사
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_HR_8AHEV960", this.DTP01_EDDATE.GetString().Substring(0, 6),                                                            
                                                            TYUserInfo.EmpNo,
                                                            this.DTP01_STDATE.GetString().Substring(0, 6)
                                                            );
                this.DbConnector.ExecuteTranQueryList();

                //내역 복사
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_HR_8AHF0961", this.DTP01_EDDATE.GetString().Substring(0, 6),
                                                            TYUserInfo.EmpNo,
                                                            this.DTP01_STDATE.GetString().Substring(0, 6)
                                                            );
                this.DbConnector.ExecuteTranQueryList();

                this.ShowMessage("TY_M_AC_27J83134");
            }
            catch
            {
                this.ShowMessage("TY_M_HR_8AHFJ965");
            }
        }
        #endregion

        #region Description : 복사 ProcessCheck
        private void BTN61_COPY_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (Convert.ToInt32(this.DTP01_STDATE.GetString()) > Convert.ToInt32(this.DTP01_EDDATE.GetString()))
            {
                this.ShowCustomMessage("기준일자가 종료일자보다 클수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            // 기준일자에 마스타 존재유무
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_88VEQ677", this.DTP01_STDATE.GetString().Substring(0, 6));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("기준일자에 자료가 존재하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                e.Successed = false;
                return;
            }

            // 복사일자에 마스타 존재유무
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_88VEQ677",this.DTP01_EDDATE.GetString().Substring(0,6));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowCustomMessage("복사일자에 자료가 존재합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                e.Successed = false;
                return;
            }

            // 복사일자에 내역 존재유무
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_8AHG3968", this.DTP01_EDDATE.GetString().Substring(0, 6));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowCustomMessage("복사일자에 자료가 존재합니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                
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
