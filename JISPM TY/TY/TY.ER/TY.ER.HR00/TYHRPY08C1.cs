using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 개인 급여관리 급여코드 조회 팝업 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2014.12.29 13:36
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4COGT965 : 코드박스-급여결과내역관리(급여코드)
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4CTEV990 : 개인 급여관리 급여코드 조회 팝업
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  KBSABUN : 사번
    ///  PAYGUBN : 급여구분
    /// </summary>
    public partial class TYHRPY08C1 : TYBase
    {   
        public string fscode = string.Empty;
        public string fsname = string.Empty;
        public string fsamt = string.Empty;

        string fsDATE = string.Empty;
        string fsSABUN = string.Empty;
        string fsGubn = string.Empty;

        #region Description : 페이지 로드
        public TYHRPY08C1(string sDATE, string sSABUN, string sGubn)
        {
            InitializeComponent();

            fsDATE = sDATE;
            fsSABUN = sSABUN;
            fsGubn = sGubn;
        }

        private void TYHRPY08C1_Load(object sender, System.EventArgs e)
        {
            this.CBH01_KBSABUN.SetValue(fsSABUN);
            
            UP_Run();
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 스프레드 조회
        private void UP_Run()
        {
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53B93647", fsSABUN,
                                                        fsDATE,
                                                        fsGubn
                                                        );
            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_4CTEV990.SetValue(dt);
        }
        #endregion

        #region Description : 스프레드 더블클릭
        private void FPS91_TY_S_HR_4CTEV990_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int row = (e == null ? 0 : e.Row);

            fscode = this.FPS91_TY_S_HR_4CTEV990.GetValue(row, "CODE").ToString();
            fsname = this.FPS91_TY_S_HR_4CTEV990.GetValue(row, "CODE_NAME").ToString();
            fsamt = this.FPS91_TY_S_HR_4CTEV990.GetValue(row, "AMOUNT").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}
