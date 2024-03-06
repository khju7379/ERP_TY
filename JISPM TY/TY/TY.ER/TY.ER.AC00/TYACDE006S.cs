using System;
using System.Drawing;
using TY.Service.Library;
using System.Data;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 정기적금불입현황출력 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.04.19 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_24J31781 : 정기적금불입현황 조회
    ///  TY_P_AC_24J34782 : 정기적금불입현황 출력
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_24J3E785 : 정기적금불입현황출력
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GCDBK : 은행코드
    ///  GDATE : 일자
    /// </summary>
    public partial class TYACDE006S : TYBase
    {
        #region Description : 페이지 로드
        public TYACDE006S()
        {
            InitializeComponent();
        }

        private void TYACDE006S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.DTP01_GDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_24J31781",
                this.CBH01_GCDBK.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_24J3E785.SetValue(this.DbConnector.ExecuteDataTable());

            this.SetSpreadSumRow(this.FPS91_TY_S_AC_24J3E785, "BKDESC", "소계", Color.LightBlue);
            this.SetSpreadSumRow(this.FPS91_TY_S_AC_24J3E785, "BKDESC", "총계", Color.SlateBlue);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_24J34782",
                this.CBH01_GCDBK.GetValue().ToString(),
                this.DTP01_GDATE.GetValue()
                );

            SectionReport rpt = new TYACDE006R();

            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
        }
        #endregion
    }
}