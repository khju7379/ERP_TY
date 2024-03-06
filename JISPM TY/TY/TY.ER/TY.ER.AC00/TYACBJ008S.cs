using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using GrapeCity.ActiveReports;
using TY.ER.GB00;

namespace TY.ER.AC00
{
    /// <summary>
    /// 관리보조부 출력 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.11.29 16:14
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2BT3H748 : 관리보조부 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2BT4A749 : 관리보조부 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  B4CDAC : 계정코드
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACBJ008S : TYBase
    {
        #region  Description : 폼 로드  이벤트
        public TYACBJ008S()
        {
            InitializeComponent();
        }

        private void TYACBJ008S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_GEDYYMM.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            
            this.SetStartingFocus(this.CBH01_B4CDAC.CodeText);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_2BT4A749.Initialize(); 
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2BT3H748", this.CBH01_B4CDAC.GetValue().ToString(), this.CBH02_B4CDAC.GetValue().ToString(), this.DTP01_GSTYYMM.GetString().ToString().Substring(0,6), this.DTP01_GEDYYMM.GetString().ToString().Substring(0,6), "1");
            this.FPS91_TY_S_AC_2BT4A749.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_2BT4A749.CurrentRowCount > 0)
            {
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_2BT4A749, "RKAC", "전기이월", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_2BT4A749, "RKAC", "전월이월", SumRowType.SubTotal);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_2BT4A749, "RKAC", "월 계", SumRowType.Sum);
                this.SetSpreadSumRow(this.FPS91_TY_S_AC_2BT4A749, "RKAC", "누 계", SumRowType.Total);
            }

        }
        #endregion

        #region  Description : 출력 버튼 이벤트
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2BT3H748", this.CBH01_B4CDAC.GetValue().ToString(), this.CBH02_B4CDAC.GetValue().ToString(), this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6), this.DTP01_GEDYYMM.GetString().ToString().Substring(0, 6), "2");

            dt = this.DbConnector.ExecuteDataTable();

            SectionReport rpt = null;
            rpt = new TYACBJ008R();
            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
            (new TYERGB001P(rpt, dt)).ShowDialog();

        }
        #endregion
    }
}
