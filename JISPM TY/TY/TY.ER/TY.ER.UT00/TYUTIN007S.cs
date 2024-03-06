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
    /// 재고관리 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2016.06.30 10:44
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_66UAP473 : 재고관리
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_66UAQ474 : 재고관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_GB_2BF7Y364 : 조회가 완료되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  CJJEQTYTOT : 총 재고량
    ///  CNCAPA : 용량
    ///  CNHWAMUL : 화물
    ///  CNTANKNO : 탱크번호
    /// </summary>
    public partial class TYUTIN007S : TYBase
    {
        public TYUTIN007S()
        {
            InitializeComponent();
        }

        #region Description : 페이지 로드
        private void TYUTIN007S_Load(object sender, System.EventArgs e)
        {
            SetStartingFocus(this.TXT01_CNTANKNO);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_66UAQ474.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_66UAP473", this.TXT01_CNTANKNO.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.TXT01_CNCAPA.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Rows[0]["TNCAPA"].ToString()));
                this.TXT01_CNHWAMUL.Text = dt.Rows[0]["HMDESC1"].ToString();
                this.TXT01_CJJEQTYTOT.Text = string.Format("{0:#,##0.000}", double.Parse(dt.Compute("Sum(QTY)", null).ToString()));
            }

            this.FPS91_TY_S_UT_66UAQ474.SetValue(dt);
        }
        #endregion

        #region Description : 통관화주별 출고조회 코드헬프
        private void BTN61_UTTCODEHELP6_Click(object sender, EventArgs e)
        {
            TYUTGB012S popup = new TYUTGB012S("");

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
            }
        }
        #endregion

        #region Description : 탱크번호 이벤트
        private void TXT01_CNTANKNO_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion
    }
}
