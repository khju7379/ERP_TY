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
    public partial class TYMZOP03C1 : TYBase
    {
        public string fsArgOPN1030   = string.Empty;
        public string fsArgOPN1030NM = string.Empty;
        public string fsArgOPN1040   = string.Empty;
        public string fsArgOPN1040NM = string.Empty;
        public string fsArgOPN1050   = string.Empty;
        public string fsArgOPN1050NM = string.Empty;
        public string fsArgOPN1060   = string.Empty;
        public string fsArgOPN1060NM = string.Empty;
        public string fsArgOPN1070   = string.Empty;
        public string fsArgOPN1080   = string.Empty;
        public string fsArgOPN1080NM = string.Empty;
        public string fsArgOPN1090   = string.Empty;

        #region Description : 페이지 로드
        public TYMZOP03C1(string sPON1000, string sPON1010, string sPON1020, string sPON1030)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.TXT01_PON1000.SetValue(sPON1000);
            this.TXT01_PON1010.SetValue(sPON1010);
            this.TXT01_PON1020.SetValue(sPON1020);
            this.TXT01_PON1030.SetValue(sPON1030);
        }

        private void TYMZOP03C1_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.BTN61_INQ);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_MR_2C53P932.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
               (
               "TY_P_MR_2C53P931",
               this.TXT01_PON1000.GetValue(),
               this.TXT01_PON1010.GetValue(),
               this.TXT01_PON1020.GetValue(),
               this.TXT01_PON1030.GetValue()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_MR_2C53P932.SetValue(dt);
            }
            else
            {
                this.FPS91_TY_S_MR_2C53P932.SetValue(dt);
            }
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_MR_2C53P932_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsArgOPN1030   = "";
            fsArgOPN1030NM = "";
            fsArgOPN1040   = "";
            fsArgOPN1040NM = "";
            fsArgOPN1050   = "";
            fsArgOPN1050NM = "";
            fsArgOPN1060   = "";
            fsArgOPN1060NM = "";
            fsArgOPN1070   = "";
            fsArgOPN1080   = "";
            fsArgOPN1080NM = "";
            fsArgOPN1090   = "";

            fsArgOPN1030   = this.FPS91_TY_S_MR_2C53P932.GetValue("PON1040").ToString(); /* 귀속부서   */
            fsArgOPN1030NM = this.FPS91_TY_S_MR_2C53P932.GetValue("DTDESC").ToString();  /* 귀속부서명 */
            fsArgOPN1040   = this.FPS91_TY_S_MR_2C53P932.GetValue("PON1060").ToString(); /* 예산구분   */
            fsArgOPN1040NM = this.FPS91_TY_S_MR_2C53P932.GetValue("YSDESC").ToString();  /* 예산구분명 */
            fsArgOPN1050   = this.FPS91_TY_S_MR_2C53P932.GetValue("PON1070").ToString(); /* 적용계정   */
            fsArgOPN1050NM = this.FPS91_TY_S_MR_2C53P932.GetValue("A1NMAC").ToString();  /* 적용계정명 */
            fsArgOPN1060   = this.FPS91_TY_S_MR_2C53P932.GetValue("PON1080").ToString(); /* 비품코드   */
            fsArgOPN1060NM = this.FPS91_TY_S_MR_2C53P932.GetValue("BPDESC").ToString();  /* 비품코드명 */
            fsArgOPN1070   = this.FPS91_TY_S_MR_2C53P932.GetValue("PON1090").ToString(); /* 순번       */
            fsArgOPN1080   = this.FPS91_TY_S_MR_2C53P932.GetValue("PON1050").ToString(); /* 품목코드   */
            fsArgOPN1080NM = this.FPS91_TY_S_MR_2C53P932.GetValue("Z105013").ToString(); /* 품목명     */
            fsArgOPN1090   = this.FPS91_TY_S_MR_2C53P932.GetValue("PON1160").ToString(); /* 발주단가   */

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}
