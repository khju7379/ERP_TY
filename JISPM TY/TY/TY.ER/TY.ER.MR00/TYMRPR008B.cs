using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;

namespace TY.ER.MR00
{
    /// <summary>
    /// 구매요청 복사 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2021.11.12 10:29
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_27J81133 : 복사 하시겠습니까?
    ///  TY_M_AC_27J83134 : 복사가 완료되었습니다
    ///  TY_M_HR_8AHFJ965 : 복사중 오류가 발생하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  COPY : 복사
    ///  COPYYYMM : 복사년월
    ///  PRM2120 : 공사및구매명
    ///  RRM1000 : 사업부
    ///  RRM1010 : RR(R)
    ///  RRM1020 : 발생년월
    ///  RRM1030 : 순서
    /// </summary>
    public partial class TYMRPR008B : TYBase
    {
        public string fsPRM1000 = string.Empty;
        public string fsPRM1010 = string.Empty;
        public string fsPRM1020 = string.Empty;
        public string fsPRM1030 = string.Empty;

        #region Description : 폼 로드
        public TYMRPR008B(string sPRNUM)
        {
            InitializeComponent();

            fsPRM1000 = sPRNUM.Substring(0, 1);
            fsPRM1010 = sPRNUM.Substring(2, 1);
            fsPRM1020 = sPRNUM.Substring(4, 6);
            fsPRM1030 = sPRNUM.Substring(11, 4);
        }

        private void TYMRPR008B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_COPY.ProcessCheck += new TButton.CheckHandler(BTN61_COPY_ProcessCheck);

            this.TXT01_PRM1000.SetValue(fsPRM1000);
            this.TXT01_PRM1010.SetValue(fsPRM1010);
            this.TXT01_PRM1020.SetValue(fsPRM1020);
            this.TXT01_PRM1030.SetValue(fsPRM1030);

            this.DTP01_COPYYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));
        }
        #endregion

        #region Description : 닫기버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 복사버튼
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            string sOUT_MSG = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_MR_BBCBC741",
                this.TXT01_PRM1000.GetValue().ToString(),
                this.TXT01_PRM1010.GetValue().ToString(),
                this.TXT01_PRM1020.GetValue().ToString(),
                this.TXT01_PRM1030.GetValue().ToString(),
                this.TXT01_PRM2120.GetValue().ToString(),
                this.DTP01_COPYYYMM.GetString().Substring(0, 6),
                TYUserInfo.EmpNo.ToString(),
                sOUT_MSG.ToString()
                );

            sOUT_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUT_MSG.Substring(0, 2).ToString() == "OK")
            {
                this.ShowMessage("TY_M_AC_27J83134");
                this.BTN61_COPY.Visible = false;
            }
            else
            {
                this.ShowMessage("TY_M_HR_8AHFJ965");

                return;
            }
        }
        #endregion

        #region Description : 복사 체크
        private void BTN61_COPY_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.TXT01_PRM1020.GetValue().ToString().Substring(0, 4) != this.DTP01_COPYYYMM.GetString().Substring(0, 4))
            {   
                this.ShowMessage("TY_M_MR_BBCAZ740");

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
