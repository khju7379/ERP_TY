using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using System.Drawing;

namespace TY.ER.ED00
{
    /// <summary>
    /// 반출신고이력 조회 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2020.04.13 16:00
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_A4DG5260 : 반출신고이력 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_A4DGP261 : 반출보고서 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    /// </summary>
    public partial class TYEDKB13C2 : TYBase
    {
        private string fsEDIGJ;
        private string fsEDISINNO;
        private string fsEDIJUKHA;
        private string fsEDIBLMSN;
        private string fsEDIBLHSN;

        #region  Description : 폼 로드 이벤트
        public TYEDKB13C2(string sEDIGJ, string sEDISINNO, string sEDIJUKHA, string sEDIBLMSN, string sEDIBLHSN)
        {
            InitializeComponent();

            fsEDIGJ = sEDIGJ;
            fsEDISINNO = sEDISINNO;
            fsEDIJUKHA = sEDIJUKHA;
            fsEDIBLMSN = sEDIBLMSN;
            fsEDIBLHSN = sEDIBLHSN;
        }

        private void TYEDKB13C2_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_UT_A4DGP261.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A4DG5260", fsEDIGJ, fsEDISINNO, fsEDIJUKHA, fsEDIBLMSN, fsEDIBLHSN);
            this.FPS91_TY_S_UT_A4DGP261.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_UT_A4DGP261.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_UT_A4DGP261.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_UT_A4DGP261.GetValue(i, "EDIRCVGB").ToString() == "Y")
                    {
                        this.FPS91_TY_S_UT_A4DGP261_Sheet1.Rows[i].ForeColor = Color.Blue;
                    }
                    else if (this.FPS91_TY_S_UT_A4DGP261.GetValue(i, "EDIRCVGB").ToString() == "E")
                    {
                        this.FPS91_TY_S_UT_A4DGP261_Sheet1.Rows[i].ForeColor = Color.Red;
                    }
                }
            }
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
