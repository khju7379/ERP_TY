using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 전자세금계산서 승인번호 조회 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.03.14 09:10
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_34H2D515 : 전자세금계산서 발행 자료 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_34H2E516 : 전자세금계산서 발행자료 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  B2DPMK : 작성부서
    ///  VNCODE : 거래처코드
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    /// </summary>
    public partial class TYAZZZ07C2 : TYBase
    {
        public string fsJUNNO = "";
        public string fsAPPROVE_ID = "";

        #region  Description : 폼 로드
        public TYAZZZ07C2()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYAZZZ07C2_Load(object sender, System.EventArgs e)
        {
            this.DTP01_GSTDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            this.DTP01_GEDDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));            

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.CBH01_VNCODE);
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
            string sVNSAUPNO = string.Empty;

            //거래처코드에 해당하는 사업자번호 찾기
            if (this.CBH01_VNCODE.GetValue().ToString().Trim() != "")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_2B18W972",  this.CBH01_VNCODE.GetValue());
                DataTable dt = this.DbConnector.ExecuteDataTable();
                if (dt.Rows.Count > 0)
                {
                    sVNSAUPNO = dt.Rows[0]["VNSAUPNO"].ToString();
                }
            }

            this.FPS91_TY_S_AC_43E9P723.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_43E9O721", this.DTP01_GSTDATE.GetString(), this.DTP01_GEDDATE.GetString(), sVNSAUPNO);

            this.FPS91_TY_S_AC_43E9P723.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : DTP01_GSTDATE_ValueChanged
        private void DTP01_GSTDATE_ValueChanged(object sender, EventArgs e)
        {
           // this.CBH01_B2DPMK.DummyValue = this.DTP01_GSTDATE.GetString().ToString();
        }
        #endregion

        #region  Description : DTP01_GEDDATE_ValueChanged
        private void DTP01_GEDDATE_ValueChanged(object sender, EventArgs e)
        {
            //this.CBH01_B2DPMK.DummyValue = this.DTP01_GEDDATE.GetString().ToString();
        }
        #endregion

        #region  Description : DTP01_GEDDATE_ValueChanged
        private void FPS91_TY_S_AC_43E9P723_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsJUNNO = this.FPS91_TY_S_AC_43E9P723.GetValue("BILL_NO").ToString();
            fsAPPROVE_ID = this.FPS91_TY_S_AC_43E9P723.GetValue("APPROVE_ID").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region  Description : DTP01_GEDDATE_ValueChanged
        private void FPS91_TY_S_AC_43E9P723_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            this.FPS91_TY_S_AC_43E9P723_CellDoubleClick(null, null);
        }
        #endregion

        
    }
}
