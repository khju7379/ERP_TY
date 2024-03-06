using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 고정자산관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2012.12.03 17:07
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_2C352805 : 고정자산 Master 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_2C354807 : 고정자산 Master 조회
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  NEW : 신규
    ///  FXGUBN : 자산분류코드
    ///  FXNAME : 자산명
    ///  FXYEAR : 자산년도
    /// </summary>
    public partial class TYACHF004S : TYBase
    {
        #region  Description : 폼 로드 이벤트
        public TYACHF004S()
        {
            InitializeComponent();
        }

        private void TYACHF004S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            this.MTB01_FXYEAR.SetValue(DateTime.Now.AddYears(-3).ToString("yyyy"));
            this.MTB02_FXYEAR.SetValue(DateTime.Now.ToString("yyyy"));

            this.DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-3).ToString("yyyy-MM-dd"));
            this.DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            this.FPS91_TY_S_AC_C4795232.Initialize();

            UP_Set_PanelLocation(true, false);

            this.BTN61_INQ_Click(null, null);

            this.SetStartingFocus(this.MTB01_FXYEAR); 
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_AC_2C354807.Initialize(); 

            this.DbConnector.CommandClear();
            if (CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                this.DbConnector.Attach("TY_P_AC_2C352805", this.CBO01_INQOPTION.GetValue().ToString(), this.MTB01_FXYEAR.GetValue(), this.MTB02_FXYEAR.GetValue(), this.TXT01_FXNAME.GetValue().ToString(), this.CBH01_FXMLASCODE.GetValue());
            }
            else
            {
                this.DbConnector.Attach("TY_P_AC_2C352805", this.CBO01_INQOPTION.GetValue().ToString(), this.DTP01_SDATE.GetString().ToString(), this.DTP01_EDATE.GetString().ToString(), this.TXT01_FXNAME.GetValue().ToString(), this.CBH01_FXMLASCODE.GetValue());
            }
            this.FPS91_TY_S_AC_2C354807.SetValue(this.DbConnector.ExecuteDataTable());

        }
        #endregion

        #region Description : 신규 유형자산 조회
        private void UP_NEW_ASSET_Select()
        {
            this.FPS91_TY_S_AC_C4795232.Initialize();

            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;

            sSTDATE = DateTime.Now.AddMonths(-2).ToString("yyyy-MM-dd");
            sEDDATE = DateTime.Now.ToString("yyyy-MM-dd");

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_C479L237", Get_Date(sSTDATE.ToString()), Get_Date(sEDDATE.ToString()));

            this.FPS91_TY_S_AC_C4795232.SetValue(this.DbConnector.ExecuteDataTable());
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYACHF004I(string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : FPS91_TY_S_AC_2C354807_CellDoubleClick 이벤트
        private void FPS91_TY_S_AC_2C354807_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYACHF004I(this.FPS91_TY_S_AC_2C354807.GetValue("FXYEAR").ToString(), this.FPS91_TY_S_AC_2C354807.GetValue("FXSEQ").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_AC_2C62Y964", dt);  //고정자산 마스타 삭제
            this.DbConnector.Attach("TY_P_AC_2CB73076", dt);  //고정자산 디테일 삭제
            this.DbConnector.Attach("TY_P_AC_2CB74077", dt);  //고정자산 층별면적 삭제

            this.DbConnector.ExecuteNonQueryList();

            this.ShowMessage("TY_M_GB_23NAD874");

            this.BTN61_INQ_Click(null, null);

        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt = this.FPS91_TY_S_AC_2C354807.GetDataSourceInclude(TSpread.TActionType.Remove, "FXYEAR", "FXSEQ");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            //전표발행 유무 체크
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{

            //    this.DbConnector.CommandClear();
            //    this.DbConnector.Attach("TY_P_AC_2B1AV975", dt.Rows[i]["B1CDBK"].ToString(),
            //                            dt.Rows[i]["B1NOAC"].ToString(),
            //                            dt.Rows[i]["B1DATE"].ToString(),
            //                            dt.Rows[i]["B1NOSQ"].ToString());
            //    Int16 iCnt = Convert.ToInt16(this.DbConnector.ExecuteScalar());

            //    if (iCnt > 0)
            //    {
            //        this.ShowMessage("TY_M_GB_25F8V482");
            //        e.Successed = false;
            //        return;
            //    }
            //}

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion
        
        #region  Description : 조회 조건 위치 설정
        private void UP_Set_PanelLocation(bool bPanel1, bool bPanel2 )
        {
            pnlYear.Location = new System.Drawing.Point(111, 11);            
            pnlYear.Visible = bPanel1;

            pnlDate.Location = new System.Drawing.Point(111, 11);
            pnlDate.Visible = bPanel2;       
        }
        #endregion

        #region  Description : CBO01_INQOPTION_SelectedIndexChanged 이벤트
        private void CBO01_INQOPTION_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBO01_INQOPTION.GetValue().ToString() == "1")
            {
                UP_Set_PanelLocation(true, false);
            }
            else
            {
                UP_Set_PanelLocation(false, true);
            }
        }
        #endregion

        #region Description : 탭 이벤트
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)      // 유형자산 조회
            {
                BTN61_INQ_Click(null, null);
            }
            else if (tabControl1.SelectedIndex == 1) // 신규 유형자산 조회
            {

                UP_NEW_ASSET_Select();
            }
        }
        #endregion

        #region Description : 신규 유형자산 그리드 이벤트
        private void FPS91_TY_S_AC_C4795232_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYACHF004I(this.FPS91_TY_S_AC_C4795232.GetValue("FXYEAR").ToString(), this.FPS91_TY_S_AC_C4795232.GetValue("FXSEQ").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                UP_NEW_ASSET_Select();
        }
        #endregion
    }
}
