using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using DataDynamics.ActiveReports;

namespace TY.ER.UT00
{
    /// <summary>
    /// 유독물 관리 대장 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2016.07.15 16:38
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_6BAEI713 : 유독물 관리 대장 출력
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    ///  TY_M_MR_2BF50353 : 처리하시겠습니까?
    ///  TY_M_MR_2BF50354 : 처리하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  PRT : 출력
    ///  CSHWAJU : 화주
    ///  CSHWAMUL : 화물
    ///  EDATE : 종료일자
    ///  SDATE : 시작일자
    /// </summary>
    public partial class TYUTIL019B : TYBase
    {
        #region Description : 페이지 로드
        public TYUTIL019B()
        {
            InitializeComponent();
        }

        private void TYUTIL019B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_STDYYMM.SetValue(System.DateTime.Now.ToString("yyyy-MM"));
            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDYYMM);
        }
        #endregion

        #region Description : 처리 버튼
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if (this.CBO01_GGUBUN.GetValue().ToString() == "A")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B2AAG543",
                                        TYUserInfo.EmpNo.ToString().Trim().ToUpper(),
                                        Get_Date(this.DTP01_STDYYMM.GetValue().ToString()),
                                        this.CBH01_STDHWAJU.GetValue().ToString(),
                                        this.CBH01_STDHWAMUL.GetValue().ToString()
                                        );

                this.DbConnector.ExecuteTranQueryList();
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_UT_B3HEJ988",
                                        Get_Date(this.DTP01_STDYYMM.GetValue().ToString()),
                                        this.CBH01_STDHWAJU.GetValue().ToString(),
                                        this.CBH01_STDHWAMUL.GetValue().ToString()
                                        );

                this.DbConnector.ExecuteTranQueryList();
            }

            this.ShowMessage("TY_M_UT_B2AB9546");
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 생성 ProcessCheck
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            if (this.CBO01_GGUBUN.GetValue().ToString() == "A")
            {
                // 월별 질소 사용량 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_B2ABD549",
                    Get_Date(this.DTP01_STDYYMM.GetValue().ToString()), // 작업일자
                    this.CBH01_STDHWAJU.GetValue().ToString(),          // 화주
                    this.CBH01_STDHWAMUL.GetValue().ToString()          // 화물
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_7B495940");

                    e.Successed = false;
                    return;
                }
            }
            else
            {
                // 질소 존재 체크
                this.DbConnector.CommandClear();
                this.DbConnector.Attach
                    (
                    "TY_P_UT_676EQ587",
                    Get_Date(this.DTP01_STDYYMM.GetValue().ToString()), // 작업일자
                    this.CBH01_STDHWAJU.GetValue().ToString(),          // 화주
                    this.CBH01_STDHWAMUL.GetValue().ToString()          // 화물
                    );

                dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.ShowMessage("TY_M_UT_B2AB1548");

                    e.Successed = false;
                    return;
                }
            }
            
            // 생성 하시겠습니까?
            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}
