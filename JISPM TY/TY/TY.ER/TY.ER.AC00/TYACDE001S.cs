using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 예적금관리내역 등록 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.04.13 15:43
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_24GAG663 : 예적금관리 조회
    ///  TY_P_AC_24GB7669 : 예적금관리 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_24GAO665 : 예적금관리 조회
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
    ///  E3NOAC : 계좌번호
    /// </summary>
    public partial class TYACDE001S : TYBase
    {
        #region Description : Page Load()
        public TYACDE001S()
        {
            InitializeComponent();

            //this.Session.ValueChanged += new TYSession.SessionValueChangedEventHandler(Session_ValueChanged);
        }

        //void Session_ValueChanged(object sender, TYSession.SessionValueChangedEventArgs e)
        //{
        //    if (e.Key == "key")
        //    {
        //        object[] value = (object[])e.Value;
        //        if (value != null && value.Length == 2)
        //        {
        //            this.CBH01_E3CDBK.SetValue(value[0]);
        //            this.TXT01_E3NOAC.SetValue(value[1]);
        //        }
        //    }
        //}


        private void TYACDE001S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.BTN61_INQ_Click(null, null);

            SetStartingFocus(this.TXT01_E3NOAC);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();
            
            this.DbConnector.Attach("TY_P_AC_24GAG663", this.ControlFactory, "01");
            this.FPS91_TY_S_AC_24GAO665.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 신규 버튼
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACDE001I(string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_24GB7669", dt);
            this.DbConnector.ExecuteNonQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 삭제 체크
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_24GAO665.GetDataSourceInclude(TSpread.TActionType.Remove, "E3NOAC");

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
        private void FPS91_TY_S_AC_24GAO665_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // 파라미터값 보내기
            if ((new TYACDE001I(this.FPS91_TY_S_AC_24GAO665.GetValue("E3NOAC").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion
    }
}
