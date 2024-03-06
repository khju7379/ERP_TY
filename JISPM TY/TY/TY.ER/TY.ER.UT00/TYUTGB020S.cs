using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;

namespace TY.ER.UT00
{
    /// <summary>
    /// 코드박스 - 장기계약 조회 프로그램입니다.
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
    ///  TY_P_MR_2B8C1196 : 코드박스 - 장기계약 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_MR_2B84W204 : 코드박스-장기계약 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  OPM1020 : 계약업체
    ///  OPM1000 : 계약년도
    ///  OPM1040 : 계약내용
    ///  PRM1020 : 년월
    /// </summary>
    public partial class TYUTGB020S : TYBase
    {
        public string fsCKJBIPHANG = string.Empty;

        public string fsCKHOSTHH   = string.Empty;
        public string fsCKHOSTMM   = string.Empty;
	    public string fsCKHOENHH   = string.Empty;
        public string fsCKHOENMM   = string.Empty;

        public string fsCKPUSTHH = string.Empty;
        public string fsCKPUSTMM = string.Empty;
        public string fsCKPUENHH = string.Empty;
        public string fsCKPUENMM = string.Empty;

        public string fsCKBLQTY  = string.Empty;
        public string fsCKOBQTY  = string.Empty;
        public string fsCKTONH   = string.Empty;

        #region Description : 페이지 로드
        public TYUTGB020S()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        private void TYUTGB020S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_UT_72DHR694.Initialize();

            this.DTP01_STDATE.SetValue(DateTime.Now.AddDays(-3).ToString("yyyyMMdd"));

            SetStartingFocus(this.DTP01_STDATE);

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();

            this.DbConnector.Attach
               (
               "TY_P_UT_72DHQ693",
               Get_Date(this.DTP01_STDATE.GetValue().ToString()),
               Get_Date(this.DTP01_EDDATE.GetValue().ToString())
               );

            dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_UT_72DHR694.SetValue(dt);
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_UT_72DHR694_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsCKJBIPHANG = "";
            fsCKHOSTHH   = "";
            fsCKHOSTMM   = "";
            fsCKHOENHH   = "";
            fsCKHOENMM   = "";
            fsCKPUSTHH   = "";
            fsCKPUSTMM   = "";
            fsCKPUENHH   = "";
            fsCKPUENMM   = "";
            fsCKBLQTY    = "";
            fsCKOBQTY    = "";
            fsCKTONH     = "";

            fsCKJBIPHANG = this.FPS91_TY_S_UT_72DHR694.GetValue("CKJBIPHANG").ToString();

            fsCKHOSTHH   = this.FPS91_TY_S_UT_72DHR694.GetValue("CKHOSTHH").ToString();
            fsCKHOSTMM   = this.FPS91_TY_S_UT_72DHR694.GetValue("CKHOSTMM").ToString();
            fsCKHOENHH   = this.FPS91_TY_S_UT_72DHR694.GetValue("CKHOENHH").ToString();
            fsCKHOENMM   = this.FPS91_TY_S_UT_72DHR694.GetValue("CKHOENMM").ToString();

            fsCKPUSTHH   = this.FPS91_TY_S_UT_72DHR694.GetValue("CKPUSTHH").ToString();
            fsCKPUSTMM   = this.FPS91_TY_S_UT_72DHR694.GetValue("CKPUSTMM").ToString();
            fsCKPUENHH   = this.FPS91_TY_S_UT_72DHR694.GetValue("CKPUENHH").ToString();
            fsCKPUENMM   = this.FPS91_TY_S_UT_72DHR694.GetValue("CKPUENMM").ToString();

            fsCKBLQTY    = this.FPS91_TY_S_UT_72DHR694.GetValue("CKBLQTY").ToString();
            fsCKOBQTY    = this.FPS91_TY_S_UT_72DHR694.GetValue("CKOBQTY").ToString();
            fsCKTONH     = this.FPS91_TY_S_UT_72DHR694.GetValue("CKTONH").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}