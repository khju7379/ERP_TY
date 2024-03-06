using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TY.Service.Library;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 접대비 CHECK LIST 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.07 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_257AF035 : 접대비 CHECK LIST 조회
    ///  TY_P_AC_257B2041 : 접대비 CHECK LIST 출력
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_257AH038 : 접대비 CHECK LIST 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACCE009S : TYBase
    {
        #region Description : 페이지 로드
        public TYACCE009S()
        {
            InitializeComponent();
        }

        private void TYACCE009S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_257AF035",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue()
                );

            this.FPS91_TY_S_AC_257AH038.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.Attach
                (
                "TY_P_AC_257B2041",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue(),
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue()
                );

            SectionReport rpt = new TYACCE009R();

            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
        }
        #endregion
    }
}