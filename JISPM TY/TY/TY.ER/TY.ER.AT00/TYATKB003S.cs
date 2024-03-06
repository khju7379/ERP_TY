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
    /// 사택월별 요금 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2018.08.31 13:30
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_88VED669 : 사택월별 요금관리 마스타 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_893E4682 : 사택월별 요금 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  APMYYMM : 년월
    /// </summary>
    public partial class TYATKB003S : TYBase
    {
        #region Description : 폼 로드
        public TYATKB003S()
        {
            InitializeComponent();
        }

        private void TYATKB003S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_APMYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(this.DTP01_APMYYMM);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sAPMYYMM = this.DTP01_APMYYMM.GetString();

            if (sAPMYYMM == "" || sAPMYYMM == "19000101")
            {
                sAPMYYMM = "";
            }
            else{
                sAPMYYMM = this.DTP01_APMYYMM.GetString().Substring(0, 6);
            }

            this.FPS91_TY_S_HR_893E4682.Initialize();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_88VED669", sAPMYYMM.Substring(0,4)+"01" ,sAPMYYMM);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_893E4682.SetValue(dt);

        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYATKB003I(string.Empty)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 그리드 더블클릭
        private void FPS91_TY_S_HR_893E4682_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.OpenModalPopup(new TYATKB003I(this.FPS91_TY_S_HR_893E4682.GetValue("APMYYMM").ToString().Replace("-", ""))) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        private void DTP01_APMYYMM_Leave(object sender, EventArgs e)
        {
            if(this.DTP01_APMYYMM.GetValue().ToString() != "")
            {
                if(this.DTP01_APMYYMM.GetValue().ToString().Substring(0,6) == "190001")
                {
                    this.DTP01_APMYYMM.SetValue("");
                }
            }
        }

        #region Description : 복사 버튼
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYATKB003B()) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
