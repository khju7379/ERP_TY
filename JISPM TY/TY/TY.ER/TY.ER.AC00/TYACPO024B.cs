using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// EIS 채권현황 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2013.09.27 10:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28986349 : 대손충당금 대상금액 Master 체크
    ///  TY_P_AC_39R1G886 : EIS 대손충당금 대상금액 관리 Detail 등록
    ///  TY_P_AC_39R1N888 : EIS 대손충당금 대상금액 관리 Master 등록
    ///  TY_P_AC_39R1P889 : EIS 대손충당금 대상금액 관리 Master 삭제
    ///  TY_P_AC_39R1Q890 : EIS 대손충당금 대상금액 관리 Detail 삭제
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_39R1S891 : 대손충당금 대상금액관리에 해당월에 생성된 자료가 없습니다.
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACPO024B : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACPO024B()
        {
            InitializeComponent();

            this.SetPopupStyle(); 
        }

        private void TYACPO024B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region  Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            //삭제
            this.DbConnector.Attach("TY_P_AC_39R1P889", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            this.DbConnector.Attach("TY_P_AC_39R1Q890", this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));

            //생성
            //일반채권
            this.DbConnector.Attach("TY_P_AC_39R1G886", TYUserInfo.EmpNo, this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));
            //장기채권
            this.DbConnector.Attach("TY_P_AC_3C93K661", TYUserInfo.EmpNo, this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));

            this.DbConnector.Attach("TY_P_AC_39R1N888", TYUserInfo.EmpNo, this.DTP01_GSTYYMM.GetString().ToString().Substring(0, 6));

            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_26E30875");
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            int iRowCnt = 0;
            //대손충당금 대상관리에 생성된 자료가 있는지 체크            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_28986349", this.DTP01_GSTYYMM.GetValue());
            iRowCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            if (iRowCnt <= 0)
            {
                this.ShowMessage("TY_M_AC_39R1S891");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region  Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
 
        }
        #endregion
    }
}
