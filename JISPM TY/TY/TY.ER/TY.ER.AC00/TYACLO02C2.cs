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
    ///  TY_S_AC_94HDS381 : 무역파일번호 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  FILENO : 파일번호
    /// </summary>
    public partial class TYACLO02C2 : TYBase
    {

        public string fsLOREACCTNO;
        public string fsLOREACCTSEQ;
        public string fsLOACCONTSEQ;
        public string fsLOREACCTNUM;
        public string fsLOACDATE;
        public string fsLOACAMT;
        public string fsSTATUS;

        #region  Description : 폼 로드 이벤트
        public TYACLO02C2()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYACLO02C2_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd"));

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region  Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_94HDS381.Initialize();
 
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_94HDP380", Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                                        Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                                                        this.CBH01_SBANK.GetValue().ToString()
                                                        );

            this.FPS91_TY_S_AC_94HDS381.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_AC_94HDS381.CurrentRowCount > 0)
            {
                this.SetFocus(this.FPS91_TY_S_AC_94HDS381); 
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
        private void FPS91_TY_S_AC_94HDS381_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int row = (e == null ? 0 : e.Row);

            fsLOREACCTNO  = this.FPS91_TY_S_AC_94HDS381.GetValue(row, "LOREACCTNO").ToString();
            fsLOREACCTSEQ = this.FPS91_TY_S_AC_94HDS381.GetValue(row, "LOREACCTSEQ").ToString();
            fsLOREACCTNUM = this.FPS91_TY_S_AC_94HDS381.GetValue(row, "LOREACCTNUM").ToString();
            

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void FPS91_TY_S_AC_94HDS381_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            int row = (e == null ? 0 : FPS91_TY_S_AC_94HDS381.ActiveSheet.ActiveRowIndex);

            fsLOREACCTNO  = this.FPS91_TY_S_AC_94HDS381.GetValue(row, "LOCCONTNO").ToString();
            fsLOREACCTSEQ = this.FPS91_TY_S_AC_94HDS381.GetValue(row, "LOCCONTNO").ToString();
            fsLOREACCTNUM = this.FPS91_TY_S_AC_94HDS381.GetValue(row, "LOCCONTNO").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion
    }
}
