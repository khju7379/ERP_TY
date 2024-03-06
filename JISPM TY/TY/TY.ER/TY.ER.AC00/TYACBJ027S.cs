using System;
using System.Drawing;
using TY.Service.Library;
using System.Data;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// 일별현금출납장 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.04.06 16:13
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_24645494 : 일별현금출납장 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_24915516 : 일별현금출납장 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYACBJ027S : TYBase
    {
        #region Description : 페이지 로드
        public TYACBJ027S()
        {
            InitializeComponent();
        }

        private void TYACBJ027S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.Focus();
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sSTDATE1 = Convert.ToString(Convert.ToDateTime(this.DTP01_STDATE.GetValue()).ToString("yyyyMMdd"));
            string sSTDATE2 = Convert.ToString(Convert.ToDateTime(this.DTP01_STDATE.GetValue()).ToString("yyyyMMdd"));
            string sSTDATE3 = Convert.ToString(Convert.ToDateTime(this.DTP01_STDATE.GetValue()).ToString("yyyyMMdd"));

            string sEDDATE1 = Convert.ToString(Convert.ToDateTime(this.DTP01_EDDATE.GetValue()).ToString("yyyyMMdd"));
            string sEDDATE2 = Convert.ToString(Convert.ToDateTime(this.DTP01_EDDATE.GetValue()).ToString("yyyyMMdd"));
            string sEDDATE3 = Convert.ToString(Convert.ToDateTime(this.DTP01_EDDATE.GetValue()).ToString("yyyyMMdd"));

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_24645494",
                sSTDATE1.ToString(),
                sEDDATE1.ToString(),
                sSTDATE2.ToString(),
                sEDDATE2.ToString(),
                sSTDATE3.ToString(),
                sEDDATE3.ToString()
                );

            this.FPS91_TY_S_AC_24915516.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sSTDATE1 = Convert.ToString(Convert.ToDateTime(this.DTP01_STDATE.GetValue()).ToString("yyyyMMdd"));
            string sSTDATE2 = Convert.ToString(Convert.ToDateTime(this.DTP01_STDATE.GetValue()).ToString("yyyyMMdd"));
            string sSTDATE3 = Convert.ToString(Convert.ToDateTime(this.DTP01_STDATE.GetValue()).ToString("yyyyMMdd"));

            string sEDDATE1 = Convert.ToString(Convert.ToDateTime(this.DTP01_EDDATE.GetValue()).ToString("yyyyMMdd"));
            string sEDDATE2 = Convert.ToString(Convert.ToDateTime(this.DTP01_EDDATE.GetValue()).ToString("yyyyMMdd"));
            string sEDDATE3 = Convert.ToString(Convert.ToDateTime(this.DTP01_EDDATE.GetValue()).ToString("yyyyMMdd"));

            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2496M574",
                sSTDATE1.ToString(),
                sEDDATE1.ToString(),
                sSTDATE2.ToString(),
                sEDDATE2.ToString(),
                sSTDATE3.ToString(),
                sEDDATE3.ToString()
                );

            SectionReport rpt = new TYACBJ027R();

            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
            
            (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
        }
        #endregion
    }
}