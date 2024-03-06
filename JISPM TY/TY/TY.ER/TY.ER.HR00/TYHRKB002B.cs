using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 용역직 근태생성 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.03.06 17:53
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    ///  TY_M_GB_26E31876 : 생성 작업을 실패했습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  CREATE : 생성
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYHRKB002B : TYBase
    {
        #region Descripgion : 폼 로드
        public TYHRKB002B()
        {
            InitializeComponent();
        }

        private void TYHRKB002B_Load(object sender, System.EventArgs e)
        {
            // 생성 체크
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_SDATE.SetValue(DateTime.Now.ToString("yyyy"));

            this.SetStartingFocus(DTP01_SDATE);
            
        }
        #endregion

        #region Descripgion : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            string sKPCUTRATE = string.Empty;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_A7REY827", this.DTP01_SDATE.GetString().Substring(0, 4),
                                                        CBH01_KBSABUN.GetValue().ToString()
                                                        );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    this.DbConnector.CommandClear();

                    //임금피크 표기년차관리 삭제
                    this.DbConnector.Attach("TY_P_HR_A7RFD829", dt.Rows[i]["KBSABUN"].ToString());

                    for (int j = 1; j < 6; j++)
                    {                        
                        //임금피크 표기년차관리 등록
                        this.DbConnector.Attach("TY_P_HR_A7RF1828",
                                                dt.Rows[i]["KBSABUN"].ToString(),
                                                j.ToString(),
                                                (Convert.ToInt32(dt.Rows[i]["KBPKSDATE"].ToString().Substring(0,4)) + j - 1).ToString(),
                                                dt.Rows[i]["CUTRATE"+j.ToString()].ToString(),
                                                TYUserInfo.EmpNo
                                                );
                    }
                    //인사기본사항 UPDATE
                    this.DbConnector.Attach("TY_P_HR_A7RFI830", "Y", "DC", TYUserInfo.EmpNo,  dt.Rows[i]["KBSABUN"].ToString());

                    this.DbConnector.ExecuteTranQueryList();
                }          

            }

            this.ShowMessage("TY_M_GB_26E30875");
        }
        #endregion

        #region Description : 생성 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_A7REY827", this.DTP01_SDATE.GetString().Substring(0,4),
                                                        CBH01_KBSABUN.GetValue().ToString()
                                                        );

            DataTable dt = this.DbConnector.ExecuteDataTable();

            if (dt.Rows.Count <= 0)
            {
                this.ShowCustomMessage("임금피크 대상자가 없습니다!", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
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

        #region Descripgion : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
       

        
    }
}
