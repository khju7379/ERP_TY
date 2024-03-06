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
    /// 탱크제원대장 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.03.29 10:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_73TA4134 : 탱크제원대장 출력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  TNTANKNO : TANK번호
    /// </summary>
    public partial class TYUTPR006P : TYBase
    {
        #region Description : 폼 로드
        public TYUTPR006P()
        {
            InitializeComponent();
        }

        private void TYUTPR006P_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.TXT01_TNTANKNO);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73TA4134", this.TXT01_TNTANKNO.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_A41ED179.SetValue(dt);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_73TA4134", this.TXT01_TNTANKNO.GetValue().ToString());

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUTPR006R();
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
