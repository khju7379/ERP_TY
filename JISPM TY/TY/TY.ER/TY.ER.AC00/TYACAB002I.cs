using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 관리항목코드 등록 팝업 프로그램입니다.
    /// 
    /// 작성자 : 관리자  11111
    /// 작성일 : 2012.03.28 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_23S15942 : 관리항목코드 조회
    /// 
    ///  # 스프레드 정보 ####
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_GB_23NAD871 : 저장하시겠습니까?
    ///  TY_M_GB_23NAD873 : 저장하였습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  CLO : 닫기
    ///  SAV : 저장
    ///  A2CDMI : 관리항목코드
    ///  A2IDPG : EDIT PROGRAM NAME
    ///  A2NMCD : 관리항목명
    ///  A2WNPG : WINDOWS PROGRAM NAME
    /// </summary>
    public partial class TYACAB002I : TYBase
    {
        private string fsA2CDMI;


        #region Description : Page Load()
        public TYACAB002I(string sA2CDMI)
        {
            InitializeComponent();

            this.SetPopupStyle();
            this.fsA2CDMI = sA2CDMI;
        }

        private void TYACAB002I_Load(object sender, System.EventArgs e)
        {
            this.BTN61_SAV.ProcessCheck += new TButton.CheckHandler(BTN61_SAV_ProcessCheck);

            if (string.IsNullOrEmpty(this.fsA2CDMI))
            {
                this.TXT01_A2CDMI.SetReadOnly(false);
            }
            else
            {
                this.TXT01_A2CDMI.SetReadOnly(true);

                this.DbConnector.CommandClear();
                this.DbConnector.Attach("TY_P_AC_23S15942", this.fsA2CDMI);
                DataTable dt = this.DbConnector.ExecuteDataTable();

                if (dt.Rows.Count > 0)
                    this.CurrentDataTableRowMapping(dt, "01");
            }

        }
        #endregion

        #region Description : 종료 버튼 이벤트
        private void BTN61_CLO_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Description : 저장 버튼 이벤트
        private void BTN61_SAV_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            if (string.IsNullOrEmpty(this.fsA2CDMI))
                this.DbConnector.Attach("TY_P_AC_23S3R968", this.ControlFactory, "01");
            else
                this.DbConnector.Attach("TY_P_AC_23S3U970", this.ControlFactory, "01");

            this.DbConnector.ExecuteNonQuery();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ShowMessage("TY_M_GB_23NAD873");
            this.Close();

        }
        #endregion

        #region Description : 저장 ProcessCheck 이벤트
        private void BTN61_SAV_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_23S15942", TXT01_A2CDMI.GetValue() );
            if (this.DbConnector.ExecuteDataTable().Rows.Count > 0)
            {
                this.ShowMessage("TY_M_GB_23S40973");
                e.Successed = false;
                return;
            };

            if (!this.ShowMessage("TY_M_GB_23NAD871"))
            {
                e.Successed = false;
                return;
            }
        }
        #endregion
    }
}
