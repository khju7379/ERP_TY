using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 자금항목등록 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.03.30 15:43 
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_23UBK177 : 자금항목코드 조회
    ///  TY_P_AC_23U34202 : 자금항목코드 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_23UBJ175 : 자금항목코도 조회
    /// 
    ///  # 알림문자 정보 ####
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  REM : 삭제
    ///  SAV : 저장
    ///  A3CDFD : 자금항목코드
    ///  A3NMFD : 자금항목명
    /// </summary>
    public partial class TYACAB005S : TYBase
    {
        #region Description : Page Load()
        public TYACAB005S()
        {
            InitializeComponent();
        }

        private void TYACAB005S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.TXT01_A3CDFD);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_23UBK177", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_23UBJ175.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACAB005I(string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_23U34202", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_23UBJ175.GetDataSourceInclude(TSpread.TActionType.Remove, "A3CDFD");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region Description : 스프레드 클릭 이벤트
        private void FPS91_TY_S_AC_23UBJ175_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYACAB005I(this.FPS91_TY_S_AC_23UBJ175.GetValue("A3CDFD").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);           
                       
            
            //TYACAB005I form = new TYACAB005I(this.FPS91_TY_S_AC_23UBJ175.GetValue("A3CDFD").ToString());
            //form.Owner = this;
            //form.Show();
            

             
        }
        #endregion
    }
}