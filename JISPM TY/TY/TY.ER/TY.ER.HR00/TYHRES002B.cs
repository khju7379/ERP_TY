using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 조직도 복사 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.27 10:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_28RBD555 : 조직도 관리 복사
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_27J81133 : 복사 하시겠습니까?
    ///  TY_M_AC_27J83134 : 복사가 완료되었습니다
    /// 
    ///  # 필드사전 정보 ####
    ///  COPY : 복사
    ///  GEDDATE : 종료일자
    ///  GSTDATE : 시작일자
    ///  STDATE : 시작일자
    ///  MEMO : 조직도 메모
    ///  VERSION : 조직도 버전
    /// </summary>
    public partial class TYHRES002B : TYBase
    {
        private string fsENTER_CD;
        private string fsORG_CHART_NM;
        private string fsSDATE;
        private string fsEDATE;
        private string fsVERSION;


        #region Description : 폼 로드 이벤트
        public TYHRES002B(string sENTER_CD, string sORG_CHART_NM, string sSDATE, string sVERSION)
        {
            InitializeComponent();

            fsENTER_CD = sENTER_CD;
            fsORG_CHART_NM = sORG_CHART_NM;
            fsSDATE = sSDATE;
            fsVERSION = sVERSION;
        }
        
        private void TYHRES002B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_COPY.ProcessCheck += new TButton.CheckHandler(BTN61_COPY_ProcessCheck);

            this.DTP01_GSTDATE.SetValue(fsSDATE);
            this.TXT01_VERSION_OLD.SetValue(fsVERSION);

            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.DTP01_GEDDATE.SetValue(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));

            this.DTP01_GSTDATE.SetReadOnly(true);
            this.DTP01_GEDDATE.SetReadOnly(true);
            this.TXT01_VERSION_OLD.SetReadOnly(true);  
        }
        #endregion

        #region Description : 복사 버튼 이벤트
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_28RBD555", this.DTP01_STDATE.GetString(),this.TXT01_VERSION.GetValue(), this.TXT01_MEMO.GetValue(),
                                                       Employer.EmpNo, fsENTER_CD, fsORG_CHART_NM, fsSDATE);
            this.DbConnector.Attach("TY_P_HR_28R64557", this.DTP01_STDATE.GetString(), Employer.EmpNo, fsENTER_CD, fsSDATE);
            this.DbConnector.Attach("TY_P_HR_28R6B558", this.DTP01_STDATE.GetString(), Employer.EmpNo, fsENTER_CD, fsORG_CHART_NM, fsSDATE);
            this.DbConnector.Attach("TY_P_HR_28O3S544", this.DTP01_GEDDATE.GetString(), fsENTER_CD, fsORG_CHART_NM, fsSDATE, this.TXT01_VERSION_OLD.GetValue() );

            this.DbConnector.ExecuteTranQueryList();
            this.ShowMessage("TY_M_AC_27J83134");
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close(); 
        }
        #endregion

        #region Description : DTP01_STDATE_ValueChanged 이벤트
        private void DTP01_STDATE_ValueChanged(object sender, EventArgs e)
        {
            DateTime dt = Convert.ToDateTime(this.DTP01_STDATE.GetValue());

            dt = dt.AddDays(-1);

            this.DTP01_GEDDATE.SetValue(dt.ToString("yyyy-MM-dd"));
        }
        #endregion

        #region Description : 복사 ProcessCheck 이벤트
        private void BTN61_COPY_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {           
            if (!this.ShowMessage("TY_M_AC_27J81133"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

    }
}
