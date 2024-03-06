using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;

namespace TY.ER.US00
{
    /// <summary>
    /// 코드박스-거래처관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.03.06 14:30
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_936EN005 : 코드박스-회계거래처(SILO)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_936EO006 : 코드박스-회계거래처(SILO)
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  VNCODE : 거래처코드
    ///  VNSANGHO : 거래처명
    /// </summary>
    public partial class TYUSGB002S : TYBase
    {
        public string fsVNSAUPNO = string.Empty;
        public string fsVNSANGHO = string.Empty;

        #region Descriptoin : 폼 로드
        public TYUSGB002S()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYUSGB002S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_US_936EO006.Initialize();

            SetStartingFocus(this.TXT01_VNSANGHO);
        }
        #endregion

        #region Descripton : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Descripton : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_936EO006.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_US_936EN005",
               this.TXT01_VNCODE.GetValue(),
               this.TXT01_VNSANGHO.GetValue()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_US_936EO006.SetValue(dt);
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        private void FPS91_TY_S_US_936EO006_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsVNSAUPNO = this.FPS91_TY_S_US_936EO006.GetValue("VNSAUPNO").ToString();
            fsVNSANGHO = this.FPS91_TY_S_US_936EO006.GetValue("VNSANGHO").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
    }
}
