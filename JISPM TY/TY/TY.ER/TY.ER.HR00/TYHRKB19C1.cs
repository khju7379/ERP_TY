using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls; 
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 학자금 가족사항 조회 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2017.03.14 11:18
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_73EBE908 : 학자금 가족사항 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_73EBF909 : 학자금 가족사항 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    ///  KBSABUN : 사번
    /// </summary>
    public partial class TYHRKB19C1 : TYBase
    {
        private string fsKBSABUN;

        public string fsGJNAME;
        public string fsGJJUMIN;
        public string fsGJSEXGB;

        #region  Description : 폼 로드 이벤트
        public TYHRKB19C1(string sKBSABUN)
        {
            InitializeComponent();

            fsKBSABUN = sKBSABUN;
        }

        private void TYHRKB19C1_Load(object sender, System.EventArgs e)
        {
            this.CBH01_KBSABUN.SetValue(fsKBSABUN);

            BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_HR_73EBF909.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_73EBE908", this.CBH01_KBSABUN.GetValue());
            this.FPS91_TY_S_HR_73EBF909.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region  Description : FPS91_TY_S_HR_73EBF909_CellDoubleClick 이벤트
        private void FPS91_TY_S_HR_73EBF909_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            fsGJNAME = this.FPS91_TY_S_HR_73EBF909.GetValue("GJNAME").ToString();
            fsGJJUMIN = this.FPS91_TY_S_HR_73EBF909.GetValue("GJJUMIN").ToString();
            fsGJSEXGB = this.FPS91_TY_S_HR_73EBF909.GetValue("GJSEXGB").ToString();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

       

      
    }
}
