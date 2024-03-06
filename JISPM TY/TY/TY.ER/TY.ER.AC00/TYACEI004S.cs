using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 받을어음기타관리 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.08.20 10:37
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_28KBH433 : 받을어음기타관리 조회
    ///  TY_P_AC_25B6N351 : 받을어음 삭제
    ///  TY_P_AC_25F8N481 : 받을어음 내역 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_28KB3429 : 받을어음기타관리 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_25G3B502 : 받을어음 상태가 발생인 경우만 처리 가능합니다!
    ///  TY_M_GB_23NAD870 : 삭제할 데이터가 없습니다.
    ///  TY_M_GB_23NAD872 : 삭제하시겠습니까?
    ///  TY_M_GB_23NAD874 : 삭제하였습니다.
    ///  TY_M_GB_25F8V482 : 전표번호가 존재합니다!
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  REM : 삭제
    ///  E6CDCL : 거래처코드
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    ///  E6NONR : 어음번호
    /// </summary>
    public partial class TYACEI004S : TYBase
    {
        #region Description : 폼 로드 이벤트
        public TYACEI004S()
        {
            InitializeComponent();
        }

        private void TYACEI004S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);
            this.BTN61_INQ.ProcessCheck += new TButton.CheckHandler(BTN61_INQ_ProcessCheck);

            //this.MTB01_STDATE.SetValue(DateTime.Now.ToString("yyyyMMdd"));
            //this.MTB01_EDDATE.SetValue(DateTime.Now.AddMonths(1).ToString("yyyyMMdd"));

            this.SetStartingFocus(TXT01_E6NONR);
        }
        #endregion

        #region Description : 조회 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sSDATE = string.Empty;
            string sEDATE = string.Empty;
            string sE6CDCL = string.Empty;
            string sE6NONR = string.Empty;

            sSDATE = this.MTB01_STDATE.GetValue().ToString().Replace("-", "").Trim();
            sEDATE = this.MTB01_EDDATE.GetValue().ToString().Replace("-", "").Trim();
            sE6CDCL = this.CBH01_E6CDCL.GetValue().ToString().Trim();
            sE6NONR = this.TXT01_E6NONR.GetValue().ToString().Trim();

            if (this.CBH01_E6CDCL.GetValue().ToString().Trim() != "" || this.TXT01_E6NONR.GetValue().ToString().Trim() != "")
            {
                if (sSDATE == "")
                {
                    sSDATE = "19910101";
                }
                if (sEDATE == "")
                {
                    sEDATE = (DateTime.Now.ToString("yyyyMMdd"));
                }
            }

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_28KBH433", sE6NONR ,sE6CDCL , sSDATE, sEDATE );

            this.FPS91_TY_S_AC_28KB3429.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region Description : 조회 ProcessCheck 이벤트
        private void BTN61_INQ_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.MTB01_STDATE.GetValue().ToString().Replace("-", "").Trim() == "" && this.MTB01_EDDATE.GetValue().ToString().Replace("-", "").Trim() == "" &&
                this.CBH01_E6CDCL.GetValue().ToString().Trim() == "" && this.TXT01_E6NONR.GetValue().ToString().Trim() == "" )
            {
            }

            if (this.MTB01_STDATE.GetValue().ToString().Replace("-", "").Trim() != "" && this.MTB01_EDDATE.GetValue().ToString().Replace("-", "").Trim() == "")
            {
                this.ShowMessage("TY_M_AC_36P4V882");
                this.SetFocus(this.MTB01_EDDATE);
                e.Successed = false;
                return;
            }
        }
        #endregion

        #region Description : 삭제 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt_AM = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            DataTable dt_AS = dt_AM.Copy(); 

            this.DbConnector.CommandClear();

            //"E7NONR", "E7IDBG", "E7DTBG"
            dt_AM.Columns.Remove("E7NONR");
            dt_AM.Columns.Remove("E7IDBG");
            dt_AM.Columns.Remove("E7DTBG");                      

            this.DbConnector.Attach("TY_P_AC_25L45582", dt_AM);

            // "E6NONR", "E6IDBG", "E6DTBG", "E6CDGL");
            dt_AS.Columns.Remove("E6NONR");
            dt_AS.Columns.Remove("E6IDBG");
            dt_AS.Columns.Remove("E6DTBG");
            dt_AS.Columns.Remove("E6CDGL");
            this.DbConnector.Attach("TY_P_AC_28K5C448", dt_AS);
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }
        #endregion

        #region Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACEI001I(string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            if (this.FPS91_TY_S_AC_28KB3429.CurrentRowCount == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            DataTable dt = this.FPS91_TY_S_AC_28KB3429.GetDataSourceInclude(TSpread.TActionType.Remove, "E7NONR", "E7IDBG", "E7DTBG", "E6NONR", "E6IDBG", "E6DTBG", "E6CDGL");

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

        #region Description : FPS91_TY_S_AC_28KB3429_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_28KB3429_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYACEI004I(this.FPS91_TY_S_AC_28KB3429.GetValue("E7NONR").ToString(),
                                 this.FPS91_TY_S_AC_28KB3429.GetValue("E7DTBG").ToString(),
                                 this.FPS91_TY_S_AC_28KB3429.GetValue("E7IDBG").ToString()
                                 )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion


    }
}
