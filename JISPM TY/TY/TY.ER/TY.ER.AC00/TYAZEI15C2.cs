using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 미지급금 조회[팝업] 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.01.24 13:50
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_31OBG890 : 미지급금 조회[팝업]
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_31OBH891 : 미지급금 조회[팝업]
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  GSTDATE : 시작일자
    ///  M1VNCD : 거래처
    /// </summary>
    public partial class TYAZEI15C2 : TYBase
    {
        public string fsM1DTED;
        public string fsM1VNCD;
        public string fsM1NOSQ;
        public string fsM1AMT;
        public string fsM1WNJP;
        public string fsM1RKAC;


        #region  Description : 폼 로드 이벤트
        public TYAZEI15C2(string sM1VNCD)
        {
            InitializeComponent();

            this.fsM1VNCD = sM1VNCD;
        }

        private void TYAZEI15C2_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            this.CBH01_M1VNCD.SetValue(this.fsM1VNCD);

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.DTP01_GSTDATE); 
        }
        #endregion

        #region  Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_31OBH891.Initialize(); 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_31OBG890", this.DTP01_GSTDATE.GetString().ToString(), this.CBH01_M1VNCD.GetValue());
            this.FPS91_TY_S_AC_31OBH891.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_31OBH891_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_31OBH891_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int row = (e == null ? 0 : e.Row);

            fsM1DTED = this.FPS91_TY_S_AC_31OBH891.GetValue(row, "M1DTED").ToString();
            fsM1VNCD = this.FPS91_TY_S_AC_31OBH891.GetValue(row, "M1VNCD").ToString();
            fsM1NOSQ = this.FPS91_TY_S_AC_31OBH891.GetValue(row, "M1NOSQ").ToString();
            fsM1AMT  = this.FPS91_TY_S_AC_31OBH891.GetValue(row, "M1AMT").ToString();
            fsM1WNJP = this.FPS91_TY_S_AC_31OBH891.GetValue(row, "M1WNJP").ToString();
            fsM1RKAC = this.FPS91_TY_S_AC_31OBH891.GetValue(row, "M1RKAC").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_31OBH891_KeyPress 이벤트
        private void FPS91_TY_S_AC_31OBH891_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            int row = (e == null ? 0 : FPS91_TY_S_AC_31OBH891.ActiveSheet.ActiveRowIndex);

            fsM1DTED = this.FPS91_TY_S_AC_31OBH891.GetValue(row, "M1DTED").ToString();
            fsM1VNCD = this.FPS91_TY_S_AC_31OBH891.GetValue(row, "M1VNCD").ToString();
            fsM1NOSQ = this.FPS91_TY_S_AC_31OBH891.GetValue(row, "M1NOSQ").ToString();
            fsM1AMT  = this.FPS91_TY_S_AC_31OBH891.GetValue(row, "M1AMT").ToString();
            fsM1WNJP = this.FPS91_TY_S_AC_31OBH891.GetValue(row, "M1WNJP").ToString();
            fsM1RKAC = this.FPS91_TY_S_AC_31OBH891.GetValue(row, "M1RKAC").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion


    }
}
