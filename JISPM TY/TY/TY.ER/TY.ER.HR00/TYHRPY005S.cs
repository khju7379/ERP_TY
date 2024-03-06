using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 개인급여기준관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.09.02 08:22
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_5928R795 : 개인급여관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_5928S796 : 개인급여관리 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  KBBALCD : 발령코드
    ///  KBJKCD : 직급
    ///  KBSABUN : 사번
    ///  KBGUNMU : 근무처
    ///  INQOPTION : 조회구분
    ///  KBBUSEO : 부서
    ///  KBHANGL : 한글이름
    /// </summary>
    public partial class TYHRPY005S : TYBase
    {
        #region Description : 폼로드
        public TYHRPY005S()
        {
            InitializeComponent();
        }

        private void TYHRPY005S_Load(object sender, System.EventArgs e)
        {
            this.CBH01_KBBUSEO.DummyValue = DateTime.Now.ToString("yyyyMMdd");
            this.CBO01_GOKCR.SetValue("B");
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sCBOKBJKCD = string.Empty;

            sCBOKBJKCD = this.CBO01_KBJKCD.GetValue().ToString();

            sCBOKBJKCD = sCBOKBJKCD.Replace("'", "");

            this.FPS91_TY_S_HR_5928S796.Initialize();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_5928R795", this.CBO01_GOKCR.GetValue(), sCBOKBJKCD, this.TXT01_KBHANGL.GetValue(), this.CBH01_KBSABUN.GetValue().ToString(),
                                                        this.CBH01_KBBUSEO.GetValue(), this.CBO01_KBGUNMU.GetValue());
            this.FPS91_TY_S_HR_5928S796.SetValue(this.DbConnector.ExecuteDataTable()); 
        }
        #endregion

        #region Description : 그리드 더블 클린 이벤트
        private void FPS91_TY_S_HR_5928S796_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.OpenModalPopup(new TYHRPY005I(this.FPS91_TY_S_HR_5928S796.GetValue("KBSABUN").ToString())) == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 보수월액 일괄등록 버튼 이벤트
        private void BTN61_INQOPTION_Click(object sender, EventArgs e)
        {
            TYHRPY005P popup = new TYHRPY005P();

            if (popup.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region  Description : 급여항목 일괄등록 버튼 이벤트
        private void BTN62_INQOPTION_Click(object sender, EventArgs e)
        {
            if ((new TYHRPY05C4()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 국민연금 보수월액 일괄등록 버튼 이벤트
        private void BTN61_INQ_FXL_Click(object sender, EventArgs e)
        {
            if ((new TYHRPY05C5()).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
