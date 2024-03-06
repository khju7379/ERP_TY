using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// LPG 사용현황 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2019.05.24 09:23
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_95O9S620 : LPG 사용현황 출력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  YYYYMM : 기준 년월
    /// </summary>
    public partial class TYUTIL016P : TYBase
    {
        #region Description : 폼 로드
        public TYUTIL016P()
        {
            InitializeComponent();
        }

        private void TYUTIL016P_Load(object sender, System.EventArgs e)
        {
            this.DTP01_YYYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));

            SetStartingFocus(this.DTP01_YYYYMM);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;
            string sSTDAY = string.Empty;
            string sEDDAY = string.Empty;

            if (int.Parse(this.DTP01_YYYYMM.GetString()) > 202001)  // 2020-02월 부터 시작일 종료일 변경 (서태호 과장)
            {
                sSTDAY = "21";
                sEDDAY = "20";
            }
            else
            {
                sSTDAY = "26";
                sEDDAY = "25";
            }

            if (this.DTP01_YYYYMM.GetString().Substring(4, 2) != "01")
            {
                sSTDATE = this.DTP01_YYYYMM.GetString().Substring(0, 4) + "-" + Set_Fill2((Convert.ToInt32(this.DTP01_YYYYMM.GetString().Substring(4, 2)) - 1).ToString()) + "-" + sSTDAY;
            }
            else
            {
                sSTDATE = (Convert.ToInt32(this.DTP01_YYYYMM.GetString().Substring(0, 4)) - 1).ToString() + "-12-" + sSTDAY;
            }

            sEDDATE = this.DTP01_YYYYMM.GetString().Substring(0, 4) + "-" + this.DTP01_YYYYMM.GetString().Substring(4, 2) + "-" + sEDDAY;


            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_UT_95O9S620", sSTDATE,
                                                        sEDDATE,
                                                        this.DTP01_YYYYMM.GetString().Substring(0, 4) + "01",
                                                        this.DTP01_YYYYMM.GetString().Substring(0, 6));

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUTIL016R();
                // 가로 출력
                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
            else
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion
    }
}
