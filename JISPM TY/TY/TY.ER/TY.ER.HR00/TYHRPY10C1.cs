using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 원천번호 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.03.24 15:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_53OFO856 : 원천번호 조회(인사)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_53OFQ858 : 원전번호 조회(인사)
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  B7CDAC : 계정코드
    /// </summary>
    public partial class TYHRPY10C1 : TYBase
    {
        private bool _Isloaded = false;

        private string fsB7CDAC;

        public string fsSJJPNO;
        public string fsB7CDACNM;
        public string fsB7AMAT;
        public string fsB7AMBJ;
        public string fsB7AMJN;
        public string fsB2RKAC;
        public string fsB2DPAC;

        #region Description : 폼 로드 이벤트
        public TYHRPY10C1(string sB7CDAC)
        {
            InitializeComponent();

            this.fsB7CDAC = sB7CDAC;
        }

        private void TYHRPY10C1_Load(object sender, System.EventArgs e)
        {
            this.CBH01_B7CDAC.SetValue(this.fsB7CDAC);
            
            this.CBH01_B7CDAC.SetReadOnly(true);
            
            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_53OFQ858.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53OH0880", this.fsB7CDAC);
            this.FPS91_TY_S_HR_53OFQ858.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : FPS91_TY_S_HR_53OFQ858_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_53OFQ858_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int row = (e == null ? 0 : e.Row);

            fsSJJPNO = this.FPS91_TY_S_HR_53OFQ858.GetValue(row, "SJJPNO").ToString();
            fsB7CDACNM = this.FPS91_TY_S_HR_53OFQ858.GetValue(row, "B7CDACNM").ToString();
            fsB7AMAT = this.FPS91_TY_S_HR_53OFQ858.GetValue(row, "B7AMAT").ToString();
            fsB7AMBJ = this.FPS91_TY_S_HR_53OFQ858.GetValue(row, "B7AMBJ").ToString();
            fsB7AMJN = this.FPS91_TY_S_HR_53OFQ858.GetValue(row, "B7AMJN").ToString();
            fsB2RKAC = this.FPS91_TY_S_HR_53OFQ858.GetValue(row, "B2RKAC").ToString();
            fsB2DPAC = this.FPS91_TY_S_HR_53OFQ858.GetValue(row, "B2DPAC").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region Description : FPS91_TY_S_HR_53OFQ858_KeyPress 이벤트
        private void FPS91_TY_S_HR_53OFQ858_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            int row = (e == null ? 0 : FPS91_TY_S_HR_53OFQ858.ActiveSheet.ActiveRowIndex);

            fsSJJPNO = this.FPS91_TY_S_HR_53OFQ858.GetValue(row, "SJJPNO").ToString();
            fsB7CDACNM = this.FPS91_TY_S_HR_53OFQ858.GetValue(row, "B7CDACNM").ToString();
            fsB7AMAT = this.FPS91_TY_S_HR_53OFQ858.GetValue(row, "B7AMAT").ToString();
            fsB7AMBJ = this.FPS91_TY_S_HR_53OFQ858.GetValue(row, "B7AMBJ").ToString();
            fsB7AMJN = this.FPS91_TY_S_HR_53OFQ858.GetValue(row, "B7AMJN").ToString();
            fsB2RKAC = this.FPS91_TY_S_HR_53OFQ858.GetValue(row, "B2RKAC").ToString();
            fsB2DPAC = this.FPS91_TY_S_HR_53OFQ858.GetValue(row, "B2DPAC").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}
