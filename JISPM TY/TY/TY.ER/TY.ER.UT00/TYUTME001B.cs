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
    public partial class TYUTME001B : TYBase
    {
        #region Descriptino : 페이지 로드
        public TYUTME001B()
        {
            InitializeComponent();
        }

        private void TYUTME001B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_COPY.ProcessCheck += new TButton.CheckHandler(BTN61_COPY_ProcessCheck);

            this.DTP01_STDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(System.DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 복사 버튼
        private void BTN61_COPY_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                                   "TY_P_UT_726FN670",
                                   Get_Date(this.DTP01_STDATE.GetValue().ToString())
                                   );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.DbConnector.CommandClear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.Attach("TY_P_UT_7269U657", Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                                                                dt.Rows[i]["YOINGUB"].ToString(),
                                                                dt.Rows[i]["YOSEQ"].ToString(),
                                                                dt.Rows[i]["YODAY"].ToString(),
                                                                dt.Rows[i]["YOYOYUL"].ToString(),
                                                                dt.Rows[i]["YOBASICAMT"].ToString(),
                                                                TYUserInfo.EmpNo
                                                                ); // 저장
                }
                this.DbConnector.ExecuteTranQueryList();
            }
            
            this.ShowMessage("TY_M_AC_27J83134"); // 저장 메세지
        }
        #endregion

        #region Description : 닫기 버튼
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
        #endregion

        #region Description : 복사 ProcessCheck
        private void BTN61_COPY_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = new DataTable();

            // 복사일자에 보험 요율 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                                   "TY_P_UT_726FN670",
                                   Get_Date(this.DTP01_EDDATE.GetValue().ToString())
                                   );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                this.ShowMessage("TY_M_UT_726FS671");
                SetFocus(this.DTP01_EDDATE);

                e.Successed = false;
                return;
            }

            // 기준일자에 보험 요율 존재 체크
            this.DbConnector.CommandClear();
            this.DbConnector.Attach(
                                   "TY_P_UT_726FN670",
                                   Get_Date(this.DTP01_STDATE.GetValue().ToString())
                                   );

            dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowMessage("TY_M_UT_726FW672");
                SetFocus(this.DTP01_STDATE);
                e.Successed = false;
                return;
            }


            // 저장하시겠습니까?
            if (!this.ShowMessage("TY_M_AC_27J81133"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}
