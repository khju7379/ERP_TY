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
    /// 코드박스-입항조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.04.10 13:08
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_94AAE323 : 입항 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_94AAF326 : 입항 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  EDHANGCHA : 항 차
    ///  STHANGCHA : 항 차
    /// </summary>
    public partial class TYUSGB004S : TYBase
    {
        public string fsIHHANGCHA = string.Empty;
        public string fsIHGOKJONG1 = string.Empty;
        public string fsIHIPHANG = string.Empty;
        public string fsIHBLQTY = string.Empty;
        public string fsIHJAKENDAT = string.Empty;

        #region Description : 폼 로드
        public TYUSGB004S(string sIBHANGCHA)
        {
            InitializeComponent();

            fsIHHANGCHA = sIBHANGCHA;
        }

        private void TYUSGB004S_Load(object sender, System.EventArgs e)
        {
            //UP_SelectLastHANGCHA();
            CBH01_STHANGCHA.SetValue(fsIHHANGCHA);
            CBH01_EDHANGCHA.SetValue(fsIHHANGCHA);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_US_94AAE323",
               this.CBH01_STHANGCHA.GetValue().ToString(),
               this.CBH01_EDHANGCHA.GetValue().ToString()
               );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_US_94AAF326.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                SetFocus(this.FPS91_TY_S_US_94AAF326);
            }
            else
            {
                SetFocus(this.CBH01_STHANGCHA.CodeText);
            }
        }
        #endregion

        #region Description : 스프레드 더블클릭
        private void FPS91_TY_S_US_94AAF326_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            UP_Get_Data();
        }

        private void FPS91_TY_S_US_94AAF326_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                UP_Get_Data();
            }
        }
        #endregion

        #region Description : 스프레드 데이터 가져오기
        private void UP_Get_Data()
        {
            fsIHHANGCHA = this.FPS91_TY_S_US_94AAF326.GetValue("IHHANGCHA").ToString();
            fsIHGOKJONG1 = this.FPS91_TY_S_US_94AAF326.GetValue("IHGOKJONG1").ToString();
            fsIHIPHANG = this.FPS91_TY_S_US_94AAF326.GetValue("IHIPHANG").ToString();
            fsIHBLQTY = this.FPS91_TY_S_US_94AAF326.GetValue("IHBLQTY").ToString();
            fsIHJAKENDAT = this.FPS91_TY_S_US_94AAF326.GetValue("IHJAKENDAT").ToString();
            if (fsIHJAKENDAT != "0")
            {
                fsIHJAKENDAT = Convert.ToDateTime(fsIHJAKENDAT.Substring(0, 4) + "-" + fsIHJAKENDAT.Substring(4, 2) + "-" + fsIHJAKENDAT.Substring(6, 2)).AddDays(18).ToString("yyyy-MM-dd");
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region Description : 마지막 항차 조회
        private void UP_SelectLastHANGCHA()
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_US_94AEG333");

            dt = this.DbConnector.ExecuteDataTable();
            
            this.CBH01_STHANGCHA.SetValue(dt.Rows[0]["IHHANGCHA"].ToString());
            this.CBH01_EDHANGCHA.SetValue(dt.Rows[0]["IHHANGCHA"].ToString());
        }
        #endregion

        #region Description : 항차 입력 이벤트
        private void CBH01_STHANGCHA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.CBH01_EDHANGCHA.SetValue(this.CBH01_STHANGCHA.GetValue().ToString());
            }
        }
        #endregion

        
    }
}
