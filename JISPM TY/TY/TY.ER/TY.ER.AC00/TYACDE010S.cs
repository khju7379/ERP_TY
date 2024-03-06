using System;
using System.Drawing;
using TY.Service.Library;
using System.Data;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 계좌별 외화거래내역서 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.04.24 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_24O3B831 : 계좌별 외화거래내역서 조회 및 출력
    ///  TY_P_AC_24O45837 : 외화 (계좌번호)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_24O3N834 : 계좌별 외화거래내역서
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GCDBK : 은행코드
    ///  GNOAC : 계좌번호
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYACDE010S : TYBase
    {
        #region Description : 페이지 로드
        public TYACDE010S()
        {
            InitializeComponent();
        }

        private void TYACDE010S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.DTP01_GSTDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_24O3B831",
                this.DTP01_GSTDATE.GetValue(),
                this.DTP01_GEDDATE.GetValue(),
                this.CBO01_GCDBK.GetValue().ToString(),
                this.CBO01_GNOAC.GetValue().ToString(),
                this.DTP01_GSTDATE.GetValue(),
                this.DTP01_GEDDATE.GetValue(),
                this.CBO01_GCDBK.GetValue().ToString(),
                this.CBO01_GNOAC.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_24O3N834.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_24O3B831",
                this.DTP01_GSTDATE.GetValue(),
                this.DTP01_GEDDATE.GetValue(),
                this.CBO01_GCDBK.GetValue().ToString(),
                this.CBO01_GNOAC.GetValue().ToString(),
                this.DTP01_GSTDATE.GetValue(),
                this.DTP01_GEDDATE.GetValue(),
                this.CBO01_GCDBK.GetValue().ToString(),
                this.CBO01_GNOAC.GetValue().ToString()
                );

            SectionReport rpt = new TYACDE010R();

            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
        }
        #endregion

        #region Description : 은행코드 이벤트
        private void CBO01_GCDBK_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_24O45837",
                this.CBO01_GCDBK.GetValue().ToString()
                );

            // 콤보박스에 바인드
            this.CBO01_GNOAC.DataBind(this.DbConnector.ExecuteDataTable(), true);

            this.CBO01_GNOAC.Initialize();
        }
        #endregion
    }
}