using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 유독물 관리 대장 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2016.07.15 16:38
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_6BAEI713 : 유독물 관리 대장 출력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_MR_2BF50353 : 처리하시겠습니까?
    ///  TY_M_MR_2BF50354 : 처리하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  CSHWAJU : 화주
    ///  CSHWAMUL : 화물
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUTPS002B : TYBase
    {
        private string fsDMREDATE = string.Empty;

        #region Description : 페이지 로드
        public TYUTPS002B(string sDMREDATE)
        {
            InitializeComponent();

            fsDMREDATE = sDMREDATE.ToString();
        }

        private void TYUTPS002B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_STDATE.SetValue(Set_Date(fsDMREDATE.ToString()));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_EDDATE);
        }
        #endregion

        #region Description : 복사 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            try
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B2MH1706",
                                        Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                                        TYUserInfo.EmpNo,
                                        Get_Date(this.DTP01_STDATE.GetValue().ToString())
                                        );

                this.DbConnector.ExecuteTranQueryList();

                this.ShowMessage("TY_M_AC_27J83134");
            }
            catch (Exception ex)
            {
                this.ShowMessage("TY_M_HR_8AHFJ965");
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 생성 ProcessCheck
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 복사 하시겠습니까?
            if (!this.ShowMessage("TY_M_AC_27J81133"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}
