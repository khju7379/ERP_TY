using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// EIS 조직도 복사 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.31 10:56
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_28VB2703 : EIS 조직 인원 복사
    ///  TY_P_HR_28VB6704 : EIS 조직도 복사
    ///  TY_P_HR_28VB7705 : EIS 조직도 삭제
    ///  TY_P_HR_28VB8706 : EIS 조직 인원 삭제
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_27J81133 : 복사 하시겠습니까?
    ///  TY_M_AC_27J83134 : 복사가 완료되었습니다
    ///  TY_M_AC_27J8T137 : 복사 월에 자료가 존재합니다 삭제후 작업하세요!
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GEDYYMM : 종료년월
    ///  GSTYYMM : 시작년월
    ///  GOKCR : 생성구분
    /// </summary>
    public partial class TYHRES004B : TYBase
    {
        public TYHRES004B()
        {
            InitializeComponent();
        }

        private void TYHRES004B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.DTP01_GEDYYMM.SetValue(DateTime.Now.AddMonths(1).ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_GEDYYMM);  
        }

        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (this.CBO01_GOKCR.GetValue().ToString() == "A")
            {
                this.DbConnector.Attach("TY_P_HR_28VB6704", this.DTP01_GEDYYMM.GetValue(), Employer.EmpNo, this.DTP01_GSTYYMM.GetValue());
                this.DbConnector.Attach("TY_P_HR_28VB2703", this.DTP01_GEDYYMM.GetValue(), Employer.EmpNo, this.DTP01_GSTYYMM.GetValue());
            }
            else
            {
                this.DbConnector.Attach("TY_P_HR_28VB7705", this.DTP01_GEDYYMM.GetValue());
                this.DbConnector.Attach("TY_P_HR_28VB8706", this.DTP01_GEDYYMM.GetValue());
            }

            this.DbConnector.ExecuteTranQueryList();

            if (this.CBO01_GOKCR.GetValue().ToString() == "A")
            {
                this.ShowMessage("TY_M_AC_27J83134");
            }
            else
            {
                this.ShowMessage("TY_M_GB_23NAD874");
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close(); 
        }

        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int iRowCnt = 0;

            if (this.CBO01_GOKCR.GetValue().ToString() == "A")
            {
                //생성년월에 자료가 존재하는 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_28V12708", this.DTP01_GEDYYMM.GetValue());
                iRowCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

                if (iRowCnt > 0)
                {
                    this.ShowMessage("TY_M_AC_27J8T137");
                    e.Successed = false;
                    return;
                }
            }

            if (this.CBO01_GOKCR.GetValue().ToString() == "A")
            {
                if (!this.ShowMessage("TY_M_AC_27J81133"))
                {
                    e.Successed = false;
                    return;
                }
            }
            else
            {
                if (!this.ShowMessage("TY_M_GB_23NAD872"))
                {
                    e.Successed = false;
                    return;
                }
            }
        }
        #endregion
    }
}