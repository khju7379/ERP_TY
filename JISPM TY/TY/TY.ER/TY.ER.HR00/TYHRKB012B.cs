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
    public partial class TYHRKB012B : TYBase
    {
        #region Descripgion : 폼 로드
        public TYHRKB012B()
        {
            InitializeComponent();
        }

        private void TYHRKB012B_Load(object sender, System.EventArgs e)
        {
            // 생성 체크
            this.BTN61_CREATE.ProcessCheck += new TButton.CheckHandler(BTN61_CREATE_ProcessCheck);

            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
        }
        #endregion

        #region Descripgion : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Descripgion : 생성 버튼 이벤트
        private void BTN61_CREATE_Click(object sender, EventArgs e)
        {
            string sRET_CODE = string.Empty;
            string sRET_MSG = string.Empty;
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_HR_53CEA661", this.DTP01_STDATE.GetString(),
                                                        this.DTP01_EDDATE.GetString(),
                                                        TYUserInfo.EmpNo,
                                                        sRET_MSG
                                                        );

            sRET_MSG = Convert.ToString(this.DbConnector.ExecuteScalar());

            this.ShowMessage("TY_M_GB_26E30875");
        }
        #endregion

        #region Description : 생성 ProcessCheck 이벤트
        private void BTN61_CREATE_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            string STDATE = this.DTP01_STDATE.GetString();
            string EDDATE = this.DTP01_EDDATE.GetString();

            //if (STDATE.Substring(0, 4) != EDDATE.Substring(0, 4))
            //{
            //    this.ShowCustomMessage("생성년도가 동일하지 않습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            //    e.Successed = false;
            //    return;
            //}

            if ( Convert.ToInt16(STDATE.Substring(0, 4)) >  Convert.ToInt16(EDDATE.Substring(0, 4)) )
            {
                this.ShowCustomMessage("시작년도가 종료년도보다 클수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                e.Successed = false;
                return;
            }

            if (STDATE.Substring(0, 4) == EDDATE.Substring(0, 4))
            {
                if (Convert.ToInt16(STDATE.Substring(4, 2)) > Convert.ToInt16(EDDATE.Substring(4, 2)))
                {
                    this.ShowCustomMessage("생성시작월이 종료월보다 클수 없습니다.", "오류", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    e.Successed = false;
                    return;
                }
            }

            if (!this.ShowMessage("TY_M_GB_26E2Z874"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = ds;
        }
        #endregion
    }
}
