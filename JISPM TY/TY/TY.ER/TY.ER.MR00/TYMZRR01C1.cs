using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.MR00
{
    /// <summary>
    /// 코드박스 - 투자수선예산 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.11.08 10:56
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_MR_2B94B233 : 코드박스 - 구매요청 조회(발주)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2B94D234 : 코드박스 - 구매요청 조회(발주)
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  GCDDP : 사업장코드
    ///  GDATE : 일자
    /// </summary>
    public partial class TYMZRR01C1 : TYBase
    {
        public string fsPOM1000   = string.Empty;       
        public string fsPOM1020   = string.Empty;       
        public string fsPOM1030   = string.Empty;
        public string fsPOM1100   = string.Empty; /* 발의자 */
        public string fsPOM1100NM = string.Empty; /* 발의자 */
        public string fsPOM1180   = string.Empty; /* 공사및구매명 */
        public string fsPOM1130   = string.Empty; /* 발의부서   */
        public string fsPOM1130NM = string.Empty; /* 발의부서명 */ 
        public string fsPRM2020   = string.Empty; /* 요청일자 */
        public string fsPRM2010   = string.Empty; /* 요청부서 */
        public string fsPRM2010NM = string.Empty; /* 요청부서명 */
        public string fsPOM1720   = string.Empty; /* 발주금액 */
        public string fsPOM1160   = string.Empty; /* 인도조건 */
        public string fsPOM1150   = string.Empty; /* 인도지역 */
        public string fsPOM1120   = string.Empty; /* 요청번호 */
        public string fsPOM1910   = string.Empty; /* 월말구분 */
        public string fsPOM6010   = string.Empty; /* 비용청구 */
        public string fsPOM6020   = string.Empty; /* 청구구분 */
        public string fsPOM6030   = string.Empty; /* 청구화주 */

        #region Description : 페이지 로드
        public TYMZRR01C1(string sPRM1000, string sSTPRM1020, string sEDPRM1020)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.TXT01_RRM1000.SetValue(sPRM1000);

            string sYEAR  = string.Empty;
            string sMONTH = string.Empty;

            if (sSTPRM1020.ToString().Substring(4, 2) == "01")
            {
                sYEAR  = Set_Fill4(Convert.ToString(int.Parse(sSTPRM1020.ToString().Substring(0, 4)) - 1));
                sMONTH = "12";
            }
            else
            {
                sYEAR  = sSTPRM1020.ToString().Substring(0, 4).ToString();
                sMONTH = Set_Fill2(Convert.ToString(int.Parse(sSTPRM1020.ToString().Substring(4, 2)) - 1));
            }

            sSTPRM1020 = sYEAR + sMONTH;

            this.TXT01_GSTYYMM.SetValue(sSTPRM1020);
            this.TXT01_GEDYYMM.SetValue(sEDPRM1020);
        }

        private void TYMZRR01C1_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.TXT01_RRM1000);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_MR_2BR5E686.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_MR_2BR5B685",
               this.TXT01_RRM1000.GetValue(),
               this.TXT01_GSTYYMM.GetValue(),
               this.TXT01_GEDYYMM.GetValue()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_MR_2BR5E686.SetValue(dt);
            }
            else
            {
                this.FPS91_TY_S_MR_2BR5E686.SetValue(dt);
                //this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 스프레드 본예산 이벤트
        private void FPS91_TY_S_MR_2BR5E686_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsPOM1000   = "";
            fsPOM1020   = "";
            fsPOM1030   = "";
            fsPOM1100   = ""; /* 발의자 */
            fsPOM1100NM = ""; /* 발의자 */
            fsPOM1180   = ""; /* 공사및구매명 */
            fsPOM1130   = ""; /* 발의부서   */
            fsPOM1130NM = ""; /* 발의부서명 */ 
            fsPRM2020   = ""; /* 요청일자 */
            fsPRM2010   = ""; /* 요청부서 */
            fsPRM2010NM = ""; /* 요청부서명 */
            fsPOM1720   = ""; /* 발주금액 */
            fsPOM1160   = ""; /* 인도조건 */
            fsPOM1150   = ""; /* 인도지역 */
            fsPOM1120   = ""; /* 요청번호 */
            fsPOM1910   = ""; /* 월말구분 */
            fsPOM6010   = ""; /* 비용청구 */
            fsPOM6020   = ""; /* 청구구분 */
            fsPOM6030   = ""; /* 청구화주 */

            fsPOM1000   = this.FPS91_TY_S_MR_2BR5E686.GetValue("POM1000").ToString();   /* 사업부 */
            fsPOM1020   = this.FPS91_TY_S_MR_2BR5E686.GetValue("POM1020").ToString();   /* 년월 */
            fsPOM1030   = this.FPS91_TY_S_MR_2BR5E686.GetValue("POM1030").ToString();   /* 순서 */
            fsPOM1180   = this.FPS91_TY_S_MR_2BR5E686.GetValue("POM1180").ToString();   /* 공사및구매명 */
            fsPOM1120   = this.FPS91_TY_S_MR_2BR5E686.GetValue("POM1120").ToString();   /* 요청번호 */

            fsPOM1100   = this.FPS91_TY_S_MR_2BR5E686.GetValue("POM1100").ToString();   /* 발의자 */
            fsPOM1100NM = this.FPS91_TY_S_MR_2BR5E686.GetValue("KBHANGL").ToString();   /* 발의자 */            
            fsPOM1130   = this.FPS91_TY_S_MR_2BR5E686.GetValue("POM1130").ToString();   /* 발의부서   */
            fsPOM1130NM = this.FPS91_TY_S_MR_2BR5E686.GetValue("PODT_DESC").ToString(); /* 발의부서명 */ 
            fsPRM2020   = this.FPS91_TY_S_MR_2BR5E686.GetValue("PRM2020").ToString();   /* 요청일자 */
            fsPRM2010   = this.FPS91_TY_S_MR_2BR5E686.GetValue("PRM2010").ToString();   /* 요청부서 */
            fsPRM2010NM = this.FPS91_TY_S_MR_2BR5E686.GetValue("PRDT_DESC").ToString(); /* 요청부서명 */
            fsPOM1720   = this.FPS91_TY_S_MR_2BR5E686.GetValue("POM1720").ToString();   /* 발주금액 */
            fsPOM1160   = this.FPS91_TY_S_MR_2BR5E686.GetValue("POM1160").ToString();   /* 인도조건 */
            fsPOM1150   = this.FPS91_TY_S_MR_2BR5E686.GetValue("POM1150").ToString();   /* 인도지역 */
            fsPOM1910   = this.FPS91_TY_S_MR_2BR5E686.GetValue("POM1910").ToString();   /* 월말구분 */
            fsPOM6010   = this.FPS91_TY_S_MR_2BR5E686.GetValue("POM6010").ToString();   /* 비용청구 */
            fsPOM6020   = this.FPS91_TY_S_MR_2BR5E686.GetValue("POM6020").ToString();   /* 청구구분 */
            fsPOM6030   = this.FPS91_TY_S_MR_2BR5E686.GetValue("POM6030").ToString();   /* 청구화주 */

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}
