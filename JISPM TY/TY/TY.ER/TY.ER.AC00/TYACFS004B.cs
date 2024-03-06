using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 개별분석 충당금 설정관리 복사 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.07.23 20:04
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_27N7D229 : 개별분석 충당금 설정관리 복사
    ///  TY_P_AC_27N7Q237 : 개별분석 충당금 설정관리 체크
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
    /// </summary>
    public partial class TYACFS004B : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACFS004B()
        {
            InitializeComponent();
            this.SetPopupStyle();
        }

        private void TYACFS004B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyyMM"));
            this.DTP01_GEDYYMM.SetValue(DateTime.Now.AddMonths(1).ToString("yyyyMM"));            

        }
        #endregion

        #region Description :처리 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_27N7D229", this.DTP01_GEDYYMM.GetValue(), Employer.EmpNo, this.DTP01_GSTYYMM.GetValue());

            this.DbConnector.ExecuteNonQuery();

            this.ShowMessage("TY_M_AC_27J83134");
        }
        #endregion

        #region Description :닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int iRowCnt = 0;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_27N7Q237", this.DTP01_GEDYYMM.GetValue());
            iRowCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            if (iRowCnt > 0)
            {
                this.ShowMessage("TY_M_AC_27J8T137");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_AC_27J81133"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}
