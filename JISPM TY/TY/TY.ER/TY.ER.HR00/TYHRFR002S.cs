using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여이체자료 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.01.06 16:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_5179Q063 : 급여이체자료 마스타 조회
    ///  TY_P_HR_517BZ067 : 급여이체자료 내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_517BW066 : 급여이체자료 마스타 조회
    ///  TY_S_HR_517C0068 : 급여이체자료 내역 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BTNTRANS : 이체
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  COINGUBN : 이체구분
    ///  PAYGUBN : 급여구분
    ///  PTGUBN : 급여구분
    ///  EDDATE : 종료일자
    ///  PAYJIDATE : 지급일자
    ///  PAYYYMM : 급여년월
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYHRFR002S : TYBase
    {
        #region Description : 페이지 로드
        public TYHRFR002S()
        {
            InitializeComponent();
        }

        private void TYHRFR002S_Load(object sender, System.EventArgs e)
        {

            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_517BW066, "E2YYMM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_517BW066, "E2GUBN");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_517BW066, "E2GUBNNM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_517BW066, "E2JIDATE");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_HR_517BW066, "E2INGUBN");

            this.DTP01_STDATE.SetValue(DateTime.Now.AddMonths(-2).ToString("yyyy-MM"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.DTP01_PAYYYMM.SetReadOnly(true);
            this.CBH01_PTGUBN.SetReadOnly(true);
            this.DTP01_PAYJIDATE.SetReadOnly(true);
            this.CBH01_COINGUBN.SetReadOnly(true);

            this.SetStartingFocus(this.DTP01_STDATE);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 이체 버튼 이벤트
        private void BTN61_BTNTRANS_Click(object sender, EventArgs e)
        {
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_517BW066.Initialize();
            this.FPS91_TY_S_HR_517C0068.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_53KE7761",
                this.DTP01_STDATE.GetString().Substring(0, 6),
                this.DTP01_EDDATE.GetString().Substring(0, 6),
                this.CBH01_PAYGUBN.GetValue().ToString()
                );

            this.FPS91_TY_S_HR_517BW066.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 마스타 그리드 더블클릭 이벤트
        private void FPS91_TY_S_HR_517BW066_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.FPS91_TY_S_HR_517C0068.Initialize();

            this.DTP01_PAYYYMM.SetValue(this.FPS91_TY_S_HR_517BW066.GetValue("E2YYMM").ToString());
            this.CBH01_PTGUBN.SetValue(this.FPS91_TY_S_HR_517BW066.GetValue("E2GUBN").ToString());
            this.DTP01_PAYJIDATE.SetValue(this.FPS91_TY_S_HR_517BW066.GetValue("E2JIDATE").ToString());
            this.CBH01_COINGUBN.SetValue(this.FPS91_TY_S_HR_517BW066.GetValue("E2INGUBN").ToString());

            this.DbConnector.CommandClear();

            if (this.FPS91_TY_S_HR_517BW066.GetValue("E2INGUBN").ToString() == "00")
            {
                this.DbConnector.Attach
                (
                "TY_P_HR_517BZ067",
                this.FPS91_TY_S_HR_517BW066.GetValue("E2YYMM").ToString(),
                this.FPS91_TY_S_HR_517BW066.GetValue("E2GUBN").ToString(),
                this.FPS91_TY_S_HR_517BW066.GetValue("E2JIDATE").ToString(),
                this.FPS91_TY_S_HR_517BW066.GetValue("E2INGUBN").ToString()
                );
            }
            else
            {
                this.DbConnector.Attach
                (
                "TY_P_HR_518E7072",
                this.FPS91_TY_S_HR_517BW066.GetValue("E2YYMM").ToString(),
                this.FPS91_TY_S_HR_517BW066.GetValue("E2GUBN").ToString(),
                this.FPS91_TY_S_HR_517BW066.GetValue("E2JIDATE").ToString(),
                this.FPS91_TY_S_HR_517BW066.GetValue("E2INGUBN").ToString()
                );
            }

            this.FPS91_TY_S_HR_517C0068.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_HR_517C0068.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_HR_517C0068, "COSABUN", "합   계", SumRowType.Total, "COAMT");
            }
        }
        #endregion
    }
}
