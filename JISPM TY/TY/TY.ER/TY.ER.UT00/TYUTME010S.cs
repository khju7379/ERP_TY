using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;

namespace TY.ER.UT00
{
    /// <summary>
    /// 보험번호 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2017.09.13 11:41
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_79DBV566 : 보험번호 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_79DBW567 : 보험번호 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  CHHWAJU : 화주
    ///  CHHWAMUL : 화물
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    ///  CMINNO : 보험번호
    /// </summary>
    public partial class TYUTME010S : TYBase
    {
        #region Description : 폼 로드
        public TYUTME010S()
        {
            InitializeComponent();
        }

        private void TYUTME010S_Load(object sender, System.EventArgs e)
        {
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;

            sSTDATE = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            sEDDATE = DateTime.Now.ToString("yyyy-MM-dd");

            this.DTP01_STDATE.SetValue(sSTDATE.Substring(0, 8) + "26");
            this.DTP01_EDDATE.SetValue(sEDDATE.Substring(0, 8) + "25");

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sSTDATE = this.DTP01_STDATE.GetValue().ToString().Replace("-","");
            string sEDDATE = this.DTP01_EDDATE.GetValue().ToString().Replace("-", "");

            if (sSTDATE == "19000101" || sSTDATE == "")
            {
                sSTDATE = "19000101";
            }
            if (sEDDATE == "19000101" || sEDDATE == "")
            {
                sEDDATE = "99990101";
            }

            if (Convert.ToDouble(sSTDATE) > Convert.ToDouble(sEDDATE))
            {
                this.ShowCustomMessage("시작일자가 종료일자보다 클수 없습니다.", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            else
            {
                DataTable dt = new DataTable();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach                    (
                    "TY_P_UT_79DBV566",
                    this.CBH01_CHHWAJU.GetValue().ToString(),
                    this.CBH01_CHHWAMUL.GetValue().ToString(),
                    sSTDATE,
                    sEDDATE,
                    this.TXT01_CMINNO.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_UT_79DBW567.SetValue(dt);
            }
        }
        #endregion
    }
}
