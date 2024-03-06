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
    /// TANK 실측 DATA 내역조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.11.07 15:34
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66SDH426 : 탱크번호 체크
    ///  TY_P_UT_6B7FK649 : TANK 실측 DATA 내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_6B7FL650 : TANK 실측 DATA 내역조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  TUGOIL : 전송일자
    ///  TUGOTK : 탱크 번호
    ///  TUGOTM : 전송시간
    /// </summary>
    public partial class TYUTAU007S : TYBase
    {
        #region Description : 페이지 로드
        public TYUTAU007S()
        {
            InitializeComponent();
        }

        private void TYUTAU007S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_TUGOIL.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            SetStartingFocus(this.TXT01_TUGOTK);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            // 탱크 번호 체크
            if (UP_TankNoCheck() == true)
            {
                string sTUGOTIM = string.Empty;

                if(this.TXT01_TUGOTM.GetValue().ToString() == "")
                {
                    sTUGOTIM = "000000";
                }
                else{
                    sTUGOTIM = this.TXT01_TUGOTM.GetValue().ToString();
                }

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_6B7FK649", this.TXT01_TUGOTK.GetValue().ToString(),
                                                            this.DTP01_TUGOIL.GetString(),
                                                            sTUGOTIM);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_UT_6B7FL650.SetValue(dt);
            }
            else
            {
                this.ShowCustomMessage("탱크 번호를 확인하세요.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                this.TXT01_TUGOTK.Focus();
            }
        }
        #endregion

        #region Description : 탱크 번호 체크
        private bool UP_TankNoCheck()
        {
            bool bRtn = false;
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66SDH426", this.TXT01_TUGOTK.GetValue().ToString().Trim());
            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                bRtn = true;
            }

            return bRtn;
        }
        #endregion
    }
}
