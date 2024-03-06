using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;

namespace TY.ER.HR00
{
    /// <summary>
    /// 연말정산 추가소득관리 복사 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2020.01.02 15:26
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_A12FK644 : 연말정산 추가소득관리 복사 등록
    ///  TY_P_HR_A12FL645 : 연말정산 추가소득관리 복사 삭제
    ///  TY_P_HR_A12FM646 : 연말정산 추가소득관리 복사 자료 존재유무
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYHRNT04C1 : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYHRNT04C1()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYHRNT04C1_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.AddYears(-1).ToString("yyyy") );
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy"));

            TXT01_ACEXTRAINCOME.SetValue("600000");
        }
        #endregion

        #region  Description :  처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {

            //삭제
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_A12FL645", "TY", this.DTP01_EDATE.GetString().ToString().Substring(0, 4));
            this.DbConnector.ExecuteTranQuery();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_A12FK644", this.DTP01_EDATE.GetString().ToString().Substring(0, 4), 
                                                        this.TXT01_ACEXTRAINCOME.GetValue().ToString(),
                                                        "TY", this.DTP01_SDATE.GetString().ToString().Substring(0, 4));
            this.DbConnector.ExecuteTranQuery();

            this.ShowMessage("TY_M_AC_27J83134");
        }

        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            if (Convert.ToInt16(this.DTP01_SDATE.GetString().ToString().Substring(0, 4)) >= Convert.ToInt16(this.DTP01_EDATE.GetString().ToString().Substring(0, 4)))
            {
                this.SetFocus(this.DTP01_SDATE);
                this.ShowCustomMessage("기준년도가 복사년도 같거나 클수 없습니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (Convert.ToInt16(this.DTP01_EDATE.GetString().ToString().Substring(0, 4)) - Convert.ToInt16(this.DTP01_SDATE.GetString().ToString().Substring(0, 4)) > 1 )
            {
                this.SetFocus(this.DTP01_SDATE);
                this.ShowCustomMessage("복사년도는 1년단위로 복사 되어야 합니다!", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_A12FM646", "TY", this.DTP01_EDATE.GetString().ToString().Substring(0,4));
            Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());
            if (iCnt > 0)
            {
                if (!this.ShowCustomMessage("복사년도에 자료가 존재합니다! 그래도 복사하시겠습니까?", "확인", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Information))
                {
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (!this.ShowMessage("TY_M_AC_27J81133"))
                {
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion
    }
}
