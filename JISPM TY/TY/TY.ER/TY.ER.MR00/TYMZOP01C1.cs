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
    public partial class TYMZOP01C1 : TYBase
    {
        public string fsPOM1000  = string.Empty;
        public string fsPOM1010  = string.Empty;
        public string fsPOM1020  = string.Empty;
        public string fsPOM1030  = string.Empty;
        public string fsPON1100  = string.Empty;
        public string fsVNSANGHO = string.Empty;
        public string fsOPM1030  = string.Empty;
        public string fsPOM1180  = string.Empty;
        public string fsPOM1100  = string.Empty;
        public string fsPON1730  = string.Empty;
        public string fsDJDESC   = string.Empty;
        public string fsPON1110  = string.Empty;
        public string fsSGDESC   = string.Empty;
        public string fsPRM1000  = string.Empty;

        #region Description : 페이지 로드
        public TYMZOP01C1(string sSTPRM1020, string sEDPRM1020)
        {
            InitializeComponent();

            this.SetPopupStyle();

            this.TXT01_GSTYYMM.SetValue(sSTPRM1020);
            this.TXT01_GEDYYMM.SetValue(sEDPRM1020);
        }

        private void TYMZOP01C1_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.TXT01_GSTYYMM);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_MR_2C5AY920.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
               (
               "TY_P_MR_2C5AY919",
               this.TXT01_GSTYYMM.GetValue(),
               this.TXT01_GEDYYMM.GetValue()
               );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.FPS91_TY_S_MR_2C5AY920.SetValue(dt);
            }
            else
            {
                this.FPS91_TY_S_MR_2C5AY920.SetValue(dt);
                //this.ShowMessage("TY_M_AC_2422N250");
            }

            this.FPS91_TY_S_MR_2C5AY920.Focus();
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 스프레드 본예산 이벤트
        private void FPS91_TY_S_MR_2C5AY920_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsPOM1000  = "";
            fsPOM1010  = "";
            fsPOM1020  = "";
            fsPOM1030  = "";
            fsPON1100  = "";
            fsVNSANGHO = "";
            fsOPM1030  = "";
            fsPOM1180  = "";
            fsPOM1100  = "";
            fsPON1730  = "";
            fsDJDESC   = "";
            fsPON1110  = "";
            fsSGDESC   = "";
            fsPRM1000  = "";

            fsPOM1000  = this.FPS91_TY_S_MR_2C5AY920.GetValue("POM1000").ToString();  // 발주사업부
            fsPOM1010  = this.FPS91_TY_S_MR_2C5AY920.GetValue("POM1010").ToString();  // 발주구분
            fsPOM1020  = this.FPS91_TY_S_MR_2C5AY920.GetValue("POM1020").ToString();  // 년월
            fsPOM1030  = this.FPS91_TY_S_MR_2C5AY920.GetValue("POM1030").ToString();  // 순서
            fsPON1100  = this.FPS91_TY_S_MR_2C5AY920.GetValue("PON1100").ToString();  // 계약업체
            fsVNSANGHO = this.FPS91_TY_S_MR_2C5AY920.GetValue("VNSANGHO").ToString(); // 계약업체명
            fsOPM1030  = "1";                                                         // 계약종류
            fsPOM1180  = this.FPS91_TY_S_MR_2C5AY920.GetValue("POM1180").ToString();  // 계약내용
            fsPOM1100  = this.FPS91_TY_S_MR_2C5AY920.GetValue("POM1100").ToString();  // 계약일자
            fsPON1730  = this.FPS91_TY_S_MR_2C5AY920.GetValue("POM1730").ToString();  // 대금지불
            fsDJDESC   = this.FPS91_TY_S_MR_2C5AY920.GetValue("DJDESC").ToString();   // 대금지불명
            fsPON1110  = this.FPS91_TY_S_MR_2C5AY920.GetValue("PON1110").ToString();  // 부가세구분
            fsSGDESC   = this.FPS91_TY_S_MR_2C5AY920.GetValue("SGDESC").ToString();   // 부가세구분명
            fsPRM1000  = this.FPS91_TY_S_MR_2C5AY920.GetValue("PRM1000").ToString();  // 계약사업부

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}
