using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// EIS 정년현황 생성 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.22 14:05
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_28M3L488 : EIS 정년일자 계산 조회
    ///  TY_P_HR_28M3O490 : EIS 정년현황 생성
    ///  TY_P_HR_28M3T491 : EIS 정년현황 삭제
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_28D5W379 : 자료가 존재합니다! 삭제후 작업하세요
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_26E2Z874 : 생성하시겠습니까?
    ///  TY_M_GB_26E30875 : 생성되었습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  BATCH : 처리
    ///  CLO : 닫기
    ///  GOKCR : 생성구분
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYHRES001B : TYBase
    {
        #region Description : 폼 로드 버튼 이벤트
        public TYHRES001B()
        {
            InitializeComponent();
        }

        private void TYHRES001B_Load(object sender, System.EventArgs e)
        {
            this.BTN61_BATCH.ProcessCheck += new TButton.CheckHandler(BTN61_BATCH_ProcessCheck);

            this.DTP01_GSTYYMM.SetValue(DateTime.Now.ToString("yyyy-MM"));

            this.SetStartingFocus(DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 생성 버튼 이벤트
        private void BTN61_BATCH_Click(object sender, EventArgs e)
        {
            if (this.CBO01_GOKCR.GetValue().ToString() == "A")
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_28M3T491", this.DTP01_GSTYYMM.GetValue());
                this.DbConnector.ExecuteTranQuery();

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_28M3L488", this.DTP01_GSTYYMM.GetValue(), TYUserInfo.SecureKey, "Y" );

                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                {
                    this.DbConnector.CommandClear();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        this.DbConnector.Attach("TY_P_HR_28M3O490", this.DTP01_GSTYYMM.GetValue(),
                                                                    dt.Rows[i]["KBBUSEO"].ToString(),
                                                                    dt.Rows[i]["KBSABUN"].ToString(),
                                                                    dt.Rows[i]["LASTYEAR"].ToString(),
                                                                    dt.Rows[i]["KBHANGL"].ToString(),
                                                                    dt.Rows[i]["RESIGNDATE"].ToString(),
                                                                    Employer.EmpNo
                                                                    );
                    }
                }
                this.DbConnector.ExecuteTranQueryList();

                this.ShowMessage("TY_M_GB_26E30875");
            }
            else
            {
                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_HR_28M3T491", this.DTP01_GSTYYMM.GetValue());
                this.DbConnector.ExecuteTranQuery();

                this.ShowMessage("TY_M_GB_23NAD874");
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        #endregion

        #region Description : 닫기 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();  
        }
        #endregion

        #region Description : 처리 ProcessCheck 이벤트
        private void BTN61_BATCH_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.CBO01_GOKCR.GetValue().ToString()  == "A")
            {
                if (!this.ShowMessage("TY_M_GB_26E2Z874"))
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
