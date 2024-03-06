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
    ///  TY_S_MR_35S53760 : 코드박스 - 구매요청 조회(발주)
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
    public partial class TYMZPR06C1 : TYBase
    {
        public string fsPRM1000 = string.Empty; // 사업부
        public string fsPRM1020 = string.Empty; // 년월
        public string fsPRM1030 = string.Empty; // 순서
        public string fsPRM2040 = string.Empty; // 요청자
        public string fsPRM2120 = string.Empty; // 구매요청명

        #region Description : 페이지 로드
        public TYMZPR06C1(string sPRM1000, string sSTPRM1020, string sEDPRM1020)
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

        private void TYMZPR06C1_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.TXT01_PRM1000);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_MR_35S53760.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_MR_35S4Z758",
               this.TXT01_PRM1000.GetValue(),
               this.TXT01_GSTYYMM.GetValue(),
               this.TXT01_GEDYYMM.GetValue(),
               this.TXT01_PRM2120.GetValue().ToString()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_MR_35S53760.SetValue(dt);
            }
            else
            {
                this.FPS91_TY_S_MR_35S53760.SetValue(dt);
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
        private void FPS91_TY_S_MR_35S53760_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsPRM1000 = ""; // 사업부
            fsPRM1020 = ""; // 년월
            fsPRM1030 = ""; // 순서
            fsPRM2040 = ""; // 요청자
            fsPRM2120 = ""; // 구매요청명

            fsPRM1000 = this.FPS91_TY_S_MR_35S53760.GetValue("PRM1000").ToString(); // 사업부
            fsPRM1020 = this.FPS91_TY_S_MR_35S53760.GetValue("PRM1020").ToString(); // 년월
            fsPRM1030 = this.FPS91_TY_S_MR_35S53760.GetValue("PRM1030").ToString(); // 순서
            fsPRM2040 = this.FPS91_TY_S_MR_35S53760.GetValue("PRM2040").ToString(); // 요청자
            fsPRM2120 = this.FPS91_TY_S_MR_35S53760.GetValue("PRM2120").ToString(); // 구매요청명

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}
