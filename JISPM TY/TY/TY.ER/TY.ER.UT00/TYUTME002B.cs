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
    /// 하역료 단가 조회 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2016.06.08 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_7269L654 : 하역료 단가 관리
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  CHYMDATE : 기준일자
    ///  CHYMSEQ : 순번
    /// </summary>
    public partial class TYUTME002B : TYBase
    {
        #region Descriptino : 페이지 로드
        public TYUTME002B()
        {
            InitializeComponent();
        }

        private void TYUTME002B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_CREATE.ProcessCheck += new TButton.CheckHandler(BTN61_COPY_ProcessCheck);

            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;

            sSTDATE = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
            sEDDATE = DateTime.Now.ToString("yyyy-MM-dd");

            this.DTP01_STDATE.SetValue(sSTDATE.Substring(0, 8) + "26");
            this.DTP01_EDDATE.SetValue(sEDDATE.Substring(0, 8) + "25");

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 생성 버튼
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            string sOUTMSG = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_733IT842");

            this.DbConnector.Attach("TY_P_UT_733IU843", Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                                        Get_Date(this.DTP01_EDDATE.GetValue().ToString())
                                                        );

            this.DbConnector.ExecuteTranQueryList();

            // 지시 일괄 등록 SP 수행
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_72S90809", Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                                                        Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                                        sOUTMSG.ToString()
                                                        );

            sOUTMSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            if (sOUTMSG.Substring(0, 2) == "OK")
            {
                this.ShowMessage("TY_M_GB_26E30875"); // 저장 메세지
            }
            else
            {
                this.ShowMessage("TY_M_GB_26E31876"); // 저장 메세지
            }
        }
        #endregion

        #region Description : 생성 ProcessCheck
        private void BTN61_COPY_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            // 생성하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}
