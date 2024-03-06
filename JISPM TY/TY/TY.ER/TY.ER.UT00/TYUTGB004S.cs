using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 코드박스 - 장기계약 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.08 10:56
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2B8C1196 : 코드박스 - 장기계약 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2B84W204 : 코드박스-장기계약 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  OPM1020 : 계약업체
    ///  OPM1000 : 계약년도
    ///  OPM1040 : 계약내용
    ///  PRM1020 : 년월
    /// </summary>
    public partial class TYUTGB004S : TYBase
    {
        public string fsIPHANG = string.Empty;
        public string fsBONSUN = string.Empty;
        public string fsHWAJU  = string.Empty;
        public string fsHWAMUL = string.Empty;

        #region Description : 페이지 로드
        public TYUTGB004S()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        public TYUTGB004S(string sIPHANG, string sBONSUN)
        {
            InitializeComponent();
            this.SetPopupStyle();

            fsIPHANG = sIPHANG.ToString();
            fsBONSUN = sBONSUN.ToString();
        }

        private void TYUTGB004S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_UT_674F5554.Initialize();

            this.DTP01_STIPHANG.SetReadOnly(true);
            this.CBH01_SBONSUN.SetReadOnly(true);

            this.DTP01_STIPHANG.SetValue(fsIPHANG.ToString());
            this.CBH01_SBONSUN.SetValue(fsBONSUN.ToString());

            //SetStartingFocus(this.DTP01_STIPHANG);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_UT_674F4553",
               this.DTP01_STIPHANG.GetValue().ToString(),
               this.CBH01_SBONSUN.GetValue().ToString()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_UT_674F5554.SetValue(dt);
            }
            else
            {
                this.FPS91_TY_S_UT_674F5554.SetValue(dt);

                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_674F5554_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsHWAJU  = "";
            fsHWAMUL = "";

            fsHWAJU  = this.FPS91_TY_S_UT_674F5554.GetValue("CMHWAJU").ToString();
            fsHWAMUL = this.FPS91_TY_S_UT_674F5554.GetValue("CMHWAMUL").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}