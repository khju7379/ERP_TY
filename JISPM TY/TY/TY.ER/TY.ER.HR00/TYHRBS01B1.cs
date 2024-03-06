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
    public partial class TYHRBS01B1 : TYBase
    {
        private object _TXT01_BPMYEAR_Value;
        private object _CBH01_BPMSABUN_Value;
        private object _TXT01_EMPNO_Value;

        public string fsBPMYEAR;

        #region  Description : 폼로드 이벤트
        public TYHRBS01B1()
        {
            InitializeComponent();

            this.SetPopupStyle();
        }

        private void TYHRBS01B1_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.BTN61_BATCH.IsAsynchronous = true;

            this.Initialize_Controls("01");

            this.TXT01_BPMYEAR.SetValue(DateTime.Now.AddYears(1).ToString("yyyyMMdd").Substring(0, 4));

            SetStartingFocus(this.TXT01_BPMYEAR);
        }
        #endregion

        #region Description : 인건비 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            _TXT01_BPMYEAR_Value  = Get_Numeric(this.TXT01_BPMYEAR.GetValue().ToString());
            _CBH01_BPMSABUN_Value = this.CBH01_BPMSABUN.GetValue().ToString();
            _TXT01_EMPNO_Value    = TYUserInfo.EmpNo;
        }

        private void BTN61_BATCH_InvokerStart(object sender, TButton.ClickEventCheckArgs e)
        {
            e.DbConnector.CommandClear();
            e.DbConnector.Attach("TY_P_HR_85GAH045", _TXT01_BPMYEAR_Value, _CBH01_BPMSABUN_Value, _TXT01_EMPNO_Value, "");
            e.DbConnector.ExecuteScalar();
        }

        private void BTN61_BATCH_InvokerEnd(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = e.ArgData as DataSet;

            if (ds.Tables[0].Rows[0][0].ToString().Substring(0, 2) != "OK")
            {
                this.ShowMessage("TY_M_HR_85OBG093");
            }
            else
            {
                fsBPMYEAR = this.TXT01_BPMYEAR.GetValue().ToString();

                this.ShowMessage("TY_M_HR_85OBG092");

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

        #region Description : 인건비 처리 체크
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            _TXT01_BPMYEAR_Value  = Get_Numeric(this.TXT01_BPMYEAR.GetValue().ToString());

            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85OAW089", _TXT01_BPMYEAR_Value);

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_HR_85OAW090");
                e.Successed = false;
                return;
            }

            //예산 생성 유무
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_85SDA117", Get_Numeric(this.TXT01_BPMYEAR.GetValue().ToString()));
            int iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar().ToString());

            if (iCnt > 0)
            {
                this.ShowCustomMessage("인건비 예산 자료가 전송되었습니다! 다시 생성할수 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_HR_85OBF091"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}