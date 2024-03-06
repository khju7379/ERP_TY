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
    /// TANK 실측 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.10.31 14:27
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66SDH426 : 탱크번호 체크
    ///  TY_P_UT_6AVED609 : TANK 실측 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_6AVEH611 : TANK 실측 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  JLTANK : 탱크번호
    /// </summary>
    public partial class TYUTAU004S : TYBase
    {
        #region Description : 페이지 로드
        public TYUTAU004S()
        {
            InitializeComponent();
        }

        private void TYUTAU004S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_JLTANK);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            bool bCheck;
            string sTankNo = string.Empty;

            bCheck = UP_Check_TankNo();

            if (bCheck == true)
            {
                if (this.TXT01_JLTANK.GetValue().ToString() != "")
                {
                    sTankNo = this.TXT01_JLTANK.GetValue().ToString();
                }
                else
                {
                    sTankNo = "101";
                }

                this.FPS91_TY_S_UT_6AVEH611.Initialize();

                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_6AVED609", sTankNo);

                this.FPS91_TY_S_UT_6AVEH611.SetValue(this.DbConnector.ExecuteDataTable());
            }
            else
            {
                this.ShowCustomMessage("탱크번호를 확인하세요", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.TXT01_JLTANK.Focus();
            }
        }
        #endregion

        #region Description : 탱크 번호 체크
        private bool UP_Check_TankNo()
        {
            bool bRtn = true;

            if (this.TXT01_JLTANK.GetValue().ToString() != "")
            {
                this.DbConnector.CommandClear();

                this.DbConnector.Attach("TY_P_UT_66SDH426", this.TXT01_JLTANK.GetValue().ToString().Trim());

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    bRtn = true;
                }
                else
                {
                    bRtn = false;
                }
            }

            return bRtn;
        }
        #endregion
    }
}
