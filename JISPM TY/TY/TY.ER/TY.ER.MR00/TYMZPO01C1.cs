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
    public partial class TYMZPO01C1 : TYBase
    {
        public string fsPRM1000 = string.Empty; // 사업부
        public string fsPRM1020 = string.Empty; // 년월
        public string fsPRM1030 = string.Empty; // 순서
        public string fsPRM2020 = string.Empty; // 발생일자
        public string fsKBHANGL = string.Empty; // 신청자
        public string fsPRM2120 = string.Empty; // 구매요청명
        public string fsDTDESC1 = string.Empty; // 부서명
        public string fsPRM2080 = string.Empty; // 기술검토
        public string fsDTDESC2 = string.Empty; // 기술검토부서
        public string fsPRM2070 = string.Empty; // 구매방법
        public string fsPRM5130 = string.Empty; // 계약번호
        public string fsOPM1040 = string.Empty; // 계약내용
        public string fsPRM2100 = string.Empty; // 인도지역
        public string fsPRM2110 = string.Empty; // 인도조건
        public string fsPRM2050 = string.Empty; // 납기일자
        public string fsPRM3000 = string.Empty; // 요청화폐
        public string fsPRM3020 = string.Empty; // 지불조건
        public string fsPRM6010 = string.Empty; // 비용청구
        public string fsPRM6020 = string.Empty; // 청구구분
        public string fsPRM6030 = string.Empty; // 지불조건

        #region Description : 페이지 로드
        public TYMZPO01C1(string sPRM1000, string sSTPRM1020, string sEDPRM1020)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.TXT01_PRM1000.SetValue(sPRM1000);

            string sYEAR = string.Empty;
            string sMONTH = string.Empty;

            if (sSTPRM1020.ToString().Substring(4, 2) == "01")
            {
                sYEAR = Set_Fill4(Convert.ToString(int.Parse(sSTPRM1020.ToString().Substring(0, 4)) - 1));
                sMONTH = "12";
            }
            else
            {
                sYEAR = sSTPRM1020.ToString().Substring(0, 4).ToString();
                sMONTH = Set_Fill2(Convert.ToString(int.Parse(sSTPRM1020.ToString().Substring(4, 2)) - 1));
            }

            sSTPRM1020 = sYEAR + sMONTH;

            this.TXT01_GSTYYMM.SetValue(sSTPRM1020);
            this.TXT01_GEDYYMM.SetValue(sEDPRM1020);
        }

        private void TYMZPO01C1_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.TXT01_PRM1000);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_MR_2B94D234.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_MR_2B94B233",
               this.TXT01_PRM1000.GetValue(),
               this.TXT01_GSTYYMM.GetValue(),
               this.TXT01_GEDYYMM.GetValue()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_MR_2B94D234.SetValue(dt);
            }
            else
            {
                this.FPS91_TY_S_MR_2B94D234.SetValue(dt);
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
        private void FPS91_TY_S_MR_2B94D234_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsPRM1000 = ""; // 사업부
            fsPRM1020 = ""; // 년월
            fsPRM1030 = ""; // 순서
            fsPRM2020 = ""; // 발생일자
            fsKBHANGL = ""; // 신청자
            fsPRM2120 = ""; // 구매요청명
            fsDTDESC1 = ""; // 부서명
            fsPRM2080 = ""; // 기술검토
            fsDTDESC2 = ""; // 기술검토부서
            fsPRM2070 = ""; // 구매방법
            fsPRM5130 = ""; // 계약번호
            fsOPM1040 = ""; // 계약내용
            fsPRM2100 = ""; // 인도지역
            fsPRM2110 = ""; // 인도조건
            fsPRM2050 = ""; // 납기일자
            fsPRM3000 = ""; // 요청화폐
            fsPRM3020 = ""; // 지불조건

            fsPRM6010 = ""; // 비용청구
            fsPRM6020 = ""; // 청구구분
            fsPRM6030 = ""; // 지불조건

            fsPRM1000 = this.TXT01_PRM1000.GetValue().ToString();                   // 사업부
            fsPRM1020 = this.FPS91_TY_S_MR_2B94D234.GetValue("PRM1020").ToString(); // 년월
            fsPRM1030 = this.FPS91_TY_S_MR_2B94D234.GetValue("PRM1030").ToString(); // 순서
            fsPRM2020 = this.FPS91_TY_S_MR_2B94D234.GetValue("PRM2020").ToString(); // 발생일자
            fsKBHANGL = this.FPS91_TY_S_MR_2B94D234.GetValue("KBHANGL").ToString(); // 신청자
            fsPRM2120 = this.FPS91_TY_S_MR_2B94D234.GetValue("PRM2120").ToString(); // 구매요청명
            fsDTDESC1 = this.FPS91_TY_S_MR_2B94D234.GetValue("DTDESC1").ToString(); // 부서명
            fsPRM2080 = this.FPS91_TY_S_MR_2B94D234.GetValue("PRM2080").ToString(); // 기술검토
            fsDTDESC2 = this.FPS91_TY_S_MR_2B94D234.GetValue("DTDESC2").ToString(); // 기술검토부서
            fsPRM2070 = this.FPS91_TY_S_MR_2B94D234.GetValue("PRM2070").ToString(); // 구매방법
            fsPRM5130 = this.FPS91_TY_S_MR_2B94D234.GetValue("PRM5130").ToString(); // 계약번호
            fsOPM1040 = this.FPS91_TY_S_MR_2B94D234.GetValue("OPM1040").ToString(); // 계약내용
            fsPRM2100 = this.FPS91_TY_S_MR_2B94D234.GetValue("PRM2100").ToString(); // 인도지역
            fsPRM2110 = this.FPS91_TY_S_MR_2B94D234.GetValue("PRM2110").ToString(); // 인도조건
            fsPRM2050 = this.FPS91_TY_S_MR_2B94D234.GetValue("PRM2050").ToString(); // 납기일자
            fsPRM3000 = this.FPS91_TY_S_MR_2B94D234.GetValue("PRM3000").ToString(); // 요청화폐
            fsPRM3020 = this.FPS91_TY_S_MR_2B94D234.GetValue("PRM3020").ToString(); // 지불조건
            fsPRM6010 = this.FPS91_TY_S_MR_2B94D234.GetValue("PRM6010").ToString(); // 비용청구
            fsPRM6020 = this.FPS91_TY_S_MR_2B94D234.GetValue("PRM6020").ToString(); // 청구구분
            fsPRM6030 = this.FPS91_TY_S_MR_2B94D234.GetValue("PRM6030").ToString(); // 지불조건

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}
