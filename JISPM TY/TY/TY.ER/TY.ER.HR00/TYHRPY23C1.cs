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
    /// 호봉인상자 팝업 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.11.06 14:01
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_5B6E6118 : 승진급 자료 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_5B6E8120 : 승진급 자료 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  INQ : 조회
    /// </summary>
    public partial class TYHRPY23C1 : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRPY23C1()
        {
            InitializeComponent();
        }

        private void TYHRPY23C1_Load(object sender, System.EventArgs e)
        {
            this.DTP01_STDATE.SetValue(System.DateTime.Now.AddMonths(-12).ToString("yyyy-MM"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM"));
            this.SetStartingFocus(DTP01_STDATE);

            BTN61_INQ_Click(null, null);
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
            this.FPS91_TY_S_HR_5B6E8120.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                (
                "TY_P_HR_5B6E6118",
                DTP01_STDATE.GetString().Substring(0, 6),
                DTP01_EDDATE.GetString().Substring(0, 6)
                );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_5B6E8120.SetValue(dt);
        }
        #endregion

        #region  Description : 그리드 더블 클릭
        private void FPS91_TY_S_HR_5B6E8120_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string sSNYYMM = this.FPS91_TY_S_HR_5B6E8120.GetValue("SNYYMM").ToString();
            string sSNBALYY = this.FPS91_TY_S_HR_5B6E8120.GetValue("SNBALYY").ToString();
            string sSNBALSEQ = this.FPS91_TY_S_HR_5B6E8120.GetValue("SNBALSEQ").ToString();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach
                ("TY_P_HR_5B6EJ121",sSNYYMM,sSNBALYY,sSNBALSEQ);

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                if (this.OpenModalPopup(new TYHRPY009I(dt, "", "", "", "", "","HB")) == System.Windows.Forms.DialogResult.OK)
                {
                    this.BTN61_INQ_Click(null, null);
                }
            }
            else
            {
                this.ShowCustomMessage("호봉인상 대상자 자료가 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return;
            }

        }
        #endregion
    }
}
