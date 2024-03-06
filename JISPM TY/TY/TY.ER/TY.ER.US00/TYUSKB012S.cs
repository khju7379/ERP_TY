using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.US00
{
    /// <summary>
    /// 일별/화주별 출고현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2019.02.18 16:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_US_92IH3802 : 일별/화주별 차량출고 집계 조회
    ///  TY_P_US_92IHA803 : 일별/화주별 차량출고 내역 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_US_92IHB804 : 일별/화주별 차량출고 집계 조회
    ///  TY_S_US_92IHC805 : 일별/화주별 차량출고 내역 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  CHGOKJONG : 곡종
    ///  CHHANGCHA : 항 차
    ///  CHHWAJU : 화주
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUSKB012S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYUSKB012S()
        {
            InitializeComponent();
        }

        private void TYUSKB012S_Load(object sender, System.EventArgs e)
        {
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_92IHB804, "CHCHULDAT");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_92IHB804, "HJNAME");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_92IHB804, "CHGOKJONG");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_92IHB804, "CHGOKJONGNM");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_92IHB804, "CHHWAJU");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_92IHB804, "CHWONSAN");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_92IHB804, "CHMTQTY");
            this.SetSpreadFixedWidthColumn(this.FPS91_TY_S_US_92IHB804, "CNT");

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.SetStartingFocus(this.DTP01_SDATE);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_92IHB804.Initialize();
            this.FPS91_TY_S_US_92IHC805.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_92IH3802", this.DTP01_SDATE.GetString(), this.DTP01_EDATE.GetString(), this.CBH01_CHHWAJU.GetValue(), this.CBH01_CHGOKJONG.GetValue());

            dt = this.DbConnector.ExecuteDataTable();
            this.FPS91_TY_S_US_92IHB804.SetValue(dt);

            if (dt.Rows.Count > 0)
            {
                // 상세 조회 메소드
                UP_Detail_DataBinding(dt.Rows[0]["CHCHULDAT"].ToString(),
                                      dt.Rows[0]["CHHWAJU"].ToString(),
                                      dt.Rows[0]["CHGOKJONG"].ToString(),
                                      dt.Rows[0]["CHWONSAN"].ToString()
                                      );
            }
        }
        #endregion

        #region  Description : FPS91_TY_S_US_92IHB804_CellDoubleClick 이벤트
        private void FPS91_TY_S_US_92IHB804_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 상세 조회 메소드
            UP_Detail_DataBinding(this.FPS91_TY_S_US_92IHB804.GetValue("CHCHULDAT").ToString().Replace("-", "").Trim(),
                                  this.FPS91_TY_S_US_92IHB804.GetValue("CHHWAJU").ToString(),
                                  this.FPS91_TY_S_US_92IHB804.GetValue("CHGOKJONG").ToString(),
                                  this.FPS91_TY_S_US_92IHB804.GetValue("CHWONSAN").ToString());
        }
        #endregion

        #region Description : 상세 조회 메소드
        private void UP_Detail_DataBinding(string sCHCHULDAT, string sCHHWAJU, string sCHGOKJONG, string sCHWONSAN)
        {
            this.FPS91_TY_S_US_92IHC805.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_US_92IHA803", sCHCHULDAT.ToString(),
                                                        sCHHWAJU.ToString(),
                                                        sCHGOKJONG.ToString(),
                                                        sCHWONSAN.ToString()
                                                        );

            this.FPS91_TY_S_US_92IHC805.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_US_92IHC805.CurrentRowCount > 0)
            {
                this.SpreadSumRowAdd(this.FPS91_TY_S_US_92IHC805, "CHNUMBER", "합  계", SumRowType.Sum, "CHMTQTY");
            }
        }
        #endregion
    }
}
