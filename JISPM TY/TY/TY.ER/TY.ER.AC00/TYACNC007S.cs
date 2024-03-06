using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;

namespace TY.ER.AC00
{
    /// <summary>
    /// 미승인전표 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.11.15 14:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2BF50356 : 미승인 전표 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2BF55357 : 미승인 전표 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  B2CDAC : 계정코드
    ///  B2DPMK : 작성부서
    ///  B2HISAB : 작성사번
    ///  INQOPTION : 조회구분
    ///  INQOPTION2 : 조회구분
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACNC007S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACNC007S()
        {
            InitializeComponent();
        }

        private void TYACNC007S_Load(object sender, System.EventArgs e)
        {
            (this.FPS91_TY_S_AC_2BF55357.Sheets[0].Columns[32].Editor as TButtonCellType).Picture = global::TY.Service.Library.Properties.Resources.printer;

            this.DTP01_GSTDATE.SetValue(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.CBH01_B2DPMK.DummyValue = this.DTP01_GSTDATE.GetString();

            this.SetStartingFocus(this.DTP01_GSTDATE);

        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_2BF55357.Initialize();
 
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_31G3F753", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), this.CBH01_B2CDAC.GetValue(), this.CBH02_B2CDAC.GetValue(),
                                                        this.CBO01_INQOPTION.GetValue(), this.CBH01_B2DPMK.GetValue(), this.CBH01_B2HISAB.GetValue(), this.CBO01_INQOPTION2.GetValue(),
                                                        this.TXT01_B2RKAC.GetValue()  
                                                        );
            DataTable dt = this.DbConnector.ExecuteDataTable();
            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_AC_2BF55357.SetValue(dt);
                this.ShowMessage("TY_M_GB_2BF7Y364"); 
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250"); 
            }
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if (this.OpenModalPopup(new TYACNC007I(string.Empty, string.Empty, string.Empty)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : DTP01_GSTDATE_ValueChanged 이벤트
        private void DTP01_GSTDATE_ValueChanged(object sender, EventArgs e)
        {
            this.CBH01_B2DPMK.DummyValue = this.DTP01_GSTDATE.GetString();
        }
        #endregion

        #region  Description : DTP01_GSTDATE_ValueChanged 이벤트
        private void FPS91_TY_S_AC_2BF55357_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sB2DPMK = this.FPS91_TY_S_AC_2BF55357.GetValue("JUNPYO").ToString().Substring(0,6);
            string sB2DTMK = this.FPS91_TY_S_AC_2BF55357.GetValue("JUNPYO").ToString().Substring(7,8);
            string sB2NOSQ = this.FPS91_TY_S_AC_2BF55357.GetValue("JUNPYO").ToString().Substring(16,3);


            if (this.OpenModalPopup(new TYACNC007I(sB2DPMK, sB2DTMK, sB2NOSQ)) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
