using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 급여계산관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2015.02.16 14:22
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_52GEE334 : 급여 계산
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_HR_52GEH335 : 급여 생성을 하시겠습니까?
    ///  TY_M_HR_52GEI336 : 급여 생성이 완료되었습니다!
    ///  TY_M_HR_52GEI337 : 급여 생성 작업중 오류가 발생하였습니다!
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  SEL : 선택
    ///  PAYGUBN : 급여구분
    ///  PAYSOPYCODE : 소급급여코드
    ///  PAYAMOUNT : 지급금액
    ///  PAYAPEDATE : 근무일자E
    ///  PAYAPSDATE : 근무일자S
    ///  PAYOTEDATE : OT일자E
    ///  PAYOTSDATE : OT일자S
    ///  PAYSOKP : 소급포함유무
    ///  PAYTAXADJ : 연말정산적용구분
    ///  PAYYYMM : 급여년월
    ///  PTSABUN : 사번
    /// </summary>
    public partial class TYHRBS002B : TYBase
    {
        private object _TXT01_BISYEAR_Value;
        private object _CBO01_INQOPTION_Value;
        private object _RATE_Value;
        private object _TXT01_EMPNO_Value;

        public string fsBISYEAR;

        #region  Description : 폼로드 이벤트
        public TYHRBS002B()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYHRBS002B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.BTN61_BATCH.IsAsynchronous = true;            

            this.Initialize_Controls("01");

            this.TXT01_BISYEAR.SetValue(DateTime.Now.AddYears(1).ToString("yyyyMMdd").Substring(0, 4));

            lblRate.Visible = false;

            this.CBO01_INQOPTION.SelectedIndex = 0;

            SetStartingFocus(this.TXT01_BISYEAR);
        }
        #endregion

        #region Description : 4대보험 급여실적 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            _TXT01_BISYEAR_Value   = Get_Numeric(this.TXT01_BISYEAR.GetValue().ToString());
            _CBO01_INQOPTION_Value = this.CBO01_INQOPTION.GetValue().ToString();
            _RATE_Value = this.TXT01_PTPAYRATE.GetValue().ToString();
            _TXT01_EMPNO_Value     = TYUserInfo.EmpNo;
        }

        private void BTN61_BATCH_InvokerStart(object sender, TButton.ClickEventCheckArgs e)
        {
            e.DbConnector.CommandClear();
            e.DbConnector.Attach("TY_P_HR_85H8W049", _TXT01_BISYEAR_Value, _CBO01_INQOPTION_Value,_RATE_Value, _TXT01_EMPNO_Value, "");
            e.DbConnector.ExecuteScalar();
        }

        private void BTN61_BATCH_InvokerEnd(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = e.ArgData as DataSet;

            if (ds.Tables[0].Rows[0][0].ToString().Substring(0, 2) != "OK")
            {
                this.ShowMessage("TY_M_HR_85ODB096");
            }
            else
            {
                fsBISYEAR = this.TXT01_BISYEAR.GetValue().ToString();

                this.ShowMessage("TY_M_HR_85ODA095");

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }
        #endregion

        #region Description : 종료 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 4대보험 급여실적 처리 체크
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            _TXT01_BISYEAR_Value = Get_Numeric(this.TXT01_BISYEAR.GetValue().ToString());

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85OAW089", Get_Numeric(this.TXT01_BISYEAR.GetValue().ToString()));

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_HR_85OAW090");
                e.Successed = false;
                return;
            }

            //예산 생성 유무
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85SDA116", Get_Numeric(this.TXT01_BISYEAR.GetValue().ToString()));
            int iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());

            if (iCnt > 0)
            {
                this.ShowCustomMessage("4대보험 예산 자료가 전송되었습니다! 다시 생성할수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }

            if (this.CBO01_INQOPTION.GetValue().ToString() == "Y")
            {
                if (Convert.ToInt32(Get_Numeric(TXT01_PTPAYRATE.GetValue().ToString())) <= 0)
                {
                    this.SetFocus(TXT01_PTPAYRATE);
                    this.ShowCustomMessage("성과급 지급율을 입력하세요!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_HR_85ODA094"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : CBO01_INQOPTION_SelectedValueChanged 체크
        private void CBO01_INQOPTION_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.CBO01_INQOPTION.GetValue().ToString().Trim() == "Y")
            {
                this.TXT01_PTPAYRATE.Visible = true;
                TXT01_PTPAYRATE.SetValue("100");
                lblRate.Visible = true;
            }
            else
            {
                this.TXT01_PTPAYRATE.Visible = false;
                lblRate.Visible = false;
            }
        }
        #endregion
    }
}
