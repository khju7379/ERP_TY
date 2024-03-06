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
    /// 접대비 집계표 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.05.04 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_25422024 : 접대비 집계표 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_25423025 : 접대비 집계표
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACCE007S : TYBase
    {
        #region Description : 페이지 로드
        public TYACCE007S()
        {
            InitializeComponent();
        }

        private void TYACCE007S_Load(object sender, System.EventArgs e)
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
                "TY_P_AC_25422024",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue(),
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue()
                );

            this.FPS91_TY_S_AC_25423025.SetValue(this.DbConnector.ExecuteDataTable());

            this.SpreadSumRowAdd(this.FPS91_TY_S_AC_25423025, "YYMM", "합  계", SumRowType.Sum, "T1", "T2", "T3", "T4", "T5", "T6", "T7", "T8", "T9", "T10");
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.Attach
                (
                "TY_P_AC_25422024",
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue(),
                this.DTP01_GSTYYMM.GetValue(),
                this.DTP01_GEDYYMM.GetValue()
                );

            SectionReport rpt = new TYACCE007R();

            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

            (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
        }
        #endregion
    }
}
