using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 무역파일번호 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.12.03 20:28
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_GB_2C38S817 : 무역파일번호 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_86JD6247 : 무역파일번호 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  FILENO : 파일번호
    /// </summary>
    public partial class TYACLO01C1 : TYBase
    {

        public string fsLOCCONTYEAR;
        public string fsLOCCONTSEQ;
        public string fsLOCCONTNUM;

        #region  Description : 폼 로드 이벤트
        public TYACLO01C1()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }
        #endregion

        private void TYACLO01C1_Load(object sender, System.EventArgs e)
        {
            this.TXT01_STDATE.SetValue(DateTime.Now.AddYears(-1).ToString("yyyy"));
            this.TXT01_EDDATE.SetValue(DateTime.Now.ToString("yyyy"));

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.TXT01_STDATE);
        }

        #region  Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_86JD6247.Initialize();
 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_86JD1248", this.TXT01_STDATE.GetValue().ToString(),
                                                        this.TXT01_EDDATE.GetValue().ToString());

            this.FPS91_TY_S_AC_86JD6247.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_86JD6247.CurrentRowCount > 0)
            {
                this.SetFocus(this.FPS91_TY_S_AC_86JD6247); 
            }
        }
        #endregion

        #region Description : 종료 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 스프레드 이벤트
        private void FPS91_TY_S_AC_86JD6247_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int row = (e == null ? 0 : e.Row);

            fsLOCCONTYEAR = this.FPS91_TY_S_AC_86JD6247.GetValue(row, "LOCCONTNO").ToString().Substring(0, 4);
            fsLOCCONTSEQ  = this.FPS91_TY_S_AC_86JD6247.GetValue(row, "LOCCONTNO").ToString().Substring(5, 2);
            fsLOCCONTNUM  = this.FPS91_TY_S_AC_86JD6247.GetValue(row, "LOCCONTSEQ").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }

        private void FPS91_TY_S_AC_86JD6247_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            int row = (e == null ? 0 : FPS91_TY_S_AC_86JD6247.ActiveSheet.ActiveRowIndex);

            fsLOCCONTYEAR = this.FPS91_TY_S_AC_86JD6247.GetValue(row, "LOCCONTNO").ToString().Substring(0, 4);
            fsLOCCONTSEQ  = this.FPS91_TY_S_AC_86JD6247.GetValue(row, "LOCCONTNO").ToString().Substring(5, 2);
            fsLOCCONTNUM  = this.FPS91_TY_S_AC_86JD6247.GetValue(row, "LOCCONTSEQ").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion
    }
}
