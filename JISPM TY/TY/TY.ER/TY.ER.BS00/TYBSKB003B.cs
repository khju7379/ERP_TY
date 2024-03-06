using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.BS00
{
    /// <summary>
    /// 투자및수선 세목코드 복사 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.07.06 11:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_777FW040 : 투자및수선 항목코드 복사
    ///  TY_P_AC_77CHB147 : 투자및수선 항목코드 복사 체크
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
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    ///  B2DPAC :  귀속부서
    ///  B2DPMK : 작성부서
    /// </summary>
    public partial class TYBSKB003B : TYBase
    {
        private string fsBIDPMK;
        private string fsBIDPAC;

        #region Description : 폼 로드
        public TYBSKB003B()
        {
            InitializeComponent();
        }

        private void TYBSKB003B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.AddYears(+1).ToString("yyyy"));

            this.SetStartingFocus(this.DTP01_STDATE);

            Get_BUSEO();
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            try
            {
                Get_BUSEO();

                this.DbConnector.CommandClear();

                //복사
                this.DbConnector.Attach("TY_P_AC_777FW040", this.DTP01_EDDATE.GetString().ToString().Substring(0, 4),
                                                            TYUserInfo.EmpNo,
                                                            this.DTP01_STDATE.GetString().ToString().Substring(0, 4),
                                                            fsBIDPMK,
                                                            fsBIDPAC
                                                            );
                this.DbConnector.ExecuteTranQueryList();

                this.ShowMessage("TY_M_AC_27J83134");
            }
            catch
            {
            }
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (Convert.ToInt32(this.DTP01_STDATE.GetString()) > Convert.ToInt32(this.DTP01_EDDATE.GetString()))
            {
                this.ShowCustomMessage("기준년도가 복사년도보다 큽니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                e.Successed = false;
                return;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_77CHB147", this.DTP01_EDDATE.GetString().ToString().Substring(0, 4),
                                                        fsBIDPMK,
                                                        fsBIDPAC);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowCustomMessage("[" + this.DTP01_EDDATE.GetString().ToString().Substring(0, 4) + "]년도가 자료가 존재합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

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

        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        #region Description : 부서코드 가져오기
        private void Get_BUSEO()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_77CF4145", TYUserInfo.EmpNo);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                fsBIDPMK = dt.Rows[0]["KBBSTEAM"].ToString();
                fsBIDPAC = dt.Rows[0]["KBBUSEO"].ToString();
            }
        }
        #endregion
    }
}
