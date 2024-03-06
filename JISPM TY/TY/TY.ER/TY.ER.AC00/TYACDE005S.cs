using System;
using System.Drawing;
using TY.Service.Library;
using System.Data;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 예적금명세서조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.04.18 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_24I3D764 : 예적금명세서 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_24I41766 : 예적금명세서조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GCDDP : 사업장코드
    ///  GDATE : 일자
    /// </summary>
    public partial class TYACDE005S : TYBase
    {
        #region Description : 페이지 로드
        public TYACDE005S()
        {
            InitializeComponent();
        }

        private void TYACDE005S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.CBO01_GCDDP);

            // 마스터 스프레드 조회
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_24I3D764",
                this.CBO01_GCDDP.GetValue().ToString(),
                this.CBO01_GCDDP.GetValue().ToString(),
                this.CBO01_GCDDP.GetValue().ToString()
                );

            this.FPS91_TY_S_AC_24I41766.SetValue(this.DbConnector.ExecuteDataTable());

            this.SetSpreadSumRow(this.FPS91_TY_S_AC_24I41766, "E3CDDP", "소계", Color.LightBlue);
            this.SetSpreadSumRow(this.FPS91_TY_S_AC_24I41766, "E3CDDP", "총계", Color.SlateBlue);
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_24I63771",
                this.DTP01_GDATE.GetValue(),
                this.CBO01_GCDDP.GetValue().ToString()
                );

            SectionReport rpt = new TYACDE005R();

            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
        }
        #endregion

        #region Description : 사업장 코드 이벤트
        private void CBO01_GCDDP_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}