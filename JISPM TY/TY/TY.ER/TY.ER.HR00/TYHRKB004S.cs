using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 발령사항 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2014.11.25 16:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_4BPHB510 : 발령사항 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_4BPHH513 : 발령사항 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  BLCODE : 발령코드
    ///  BLSABUN : 발령사번
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRKB004S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRKB004S()
        {
            InitializeComponent();
        }

        private void TYHRKB004S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-12).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            

            bool returnValue = UP_SearchCheck();

            if (returnValue != true)
            {
                return;
            }

            this.FPS91_TY_S_HR_4BPHH513.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_4BPHB510", this.DTP01_SDATE.GetString(), this.DTP01_EDATE.GetString(), this.CBH01_BLCODE.GetValue(), this.CBH01_BLSABUN.GetValue());
            this.FPS91_TY_S_HR_4BPHH513.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRKB02C1("", string.Empty, string.Empty, "", "", "S")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void FPS91_TY_S_HR_4BPHH513_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRKB02C1(this.FPS91_TY_S_HR_4BPHH513.GetValue("BLSABUN").ToString(), 
                                this.FPS91_TY_S_HR_4BPHH513.GetValue("BLBUNOSEQ").ToString().Substring(0, 4), 
                                this.FPS91_TY_S_HR_4BPHH513.GetValue("BLBUNOSEQ").ToString().Substring(5, 3),
                                this.FPS91_TY_S_HR_4BPHH513.GetValue("BLDATE").ToString(),
                                this.FPS91_TY_S_HR_4BPHH513.GetValue("BLCODE").ToString(), 
                                "S")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 조회 체크
        private bool UP_SearchCheck()
        {
            if (Convert.ToInt32(this.DTP01_SDATE.GetString().ToString()) > Convert.ToInt32(this.DTP01_EDATE.GetString().ToString()))
            {
                this.ShowCustomMessage("시작일자는 종료일자보다 이전이어야 합니다. ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return false;
            }            

            return true;
        }
        #endregion


    }
}
