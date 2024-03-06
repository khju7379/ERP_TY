using System;
using System.Data;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Library;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using TY.Service.Library;
using System.Drawing;

namespace TY.ER.ED00
{
    /// <summary>
    /// 반출수기관리 조회 프로그램입니다.
    /// 
    /// 작성자 : 임경화
    /// 작성일 : 2020.04.09 17:03
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_UT_A4A9X234 : 반출수기관리 조회
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_UT_A4AA0236 : 반출수기관리 조회
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
    ///  SDATE : 시작일자
    ///  EDATE : 종료일자
    ///  EDIGJ : 공장
    ///  EDISINNO : 반입근거번호
    /// </summary>
    public partial class TYEDKB013S : TYBase
    {
        #region  Description : 폼로드 이벤트
        public TYEDKB013S()
        {
            InitializeComponent();
        }

        private void TYEDKB013S_Load(object sender, System.EventArgs e)
        {
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            DTP01_SDATE.SetValue(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd"));
            DTP01_EDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            BTN61_INQ_Click(null, null);

            this.SetStartingFocus(DTP01_SDATE);
        }
        #endregion

        #region  Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_UT_A4AA0236.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_UT_A4A9X234", CBO01_EDIGJ.GetValue(), this.DTP01_SDATE.GetString(), this.DTP01_EDATE.GetString(), this.TXT01_EDISINNO.GetValue() );
            this.FPS91_TY_S_UT_A4AA0236.SetValue(this.DbConnector.ExecuteDataTable());

            if (this.FPS91_TY_S_UT_A4AA0236.CurrentRowCount > 0)
            {
                for (int i = 0; i < this.FPS91_TY_S_UT_A4AA0236.CurrentRowCount; i++)
                {
                    if (this.FPS91_TY_S_UT_A4AA0236.GetValue(i, "EDMRCVGB").ToString() == "Y")
                    {
                        this.FPS91_TY_S_UT_A4AA0236_Sheet1.Rows[i].ForeColor = Color.Blue;
                    }
                    else if (this.FPS91_TY_S_UT_A4AA0236.GetValue(i, "EDMRCVGB").ToString() == "E")
                    {
                        this.FPS91_TY_S_UT_A4AA0236_Sheet1.Rows[i].ForeColor = Color.Red;
                    }
                }
            }
            
        }
        #endregion

        #region  Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYEDKB013I("", "")).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion

        #region  Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataTable dt = ((TButton.ClickEventCheckArgs)e).ArgData as DataTable;

            this.DbConnector.CommandClear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.DbConnector.Attach("TY_P_UT_A4ADF241", dt.Rows[i]["EDMDATE"].ToString(), dt.Rows[i]["EDMSEQ"].ToString());
            }
            this.DbConnector.ExecuteTranQueryList();

            this.BTN61_INQ_Click(null, null);
            this.ShowMessage("TY_M_GB_23NAD874");
        }

        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataTable dt1 = new DataTable();

            // 삭제 체크되어 있는 행의 칼럼(VNCODE)을 가져와서 체크하는 부분
            DataTable dt = this.FPS91_TY_S_UT_A4AA0236.GetDataSourceInclude(TSpread.TActionType.Remove, "EDMDATE", "EDMSEQ", "EDMRCVGB");

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_GB_23NAD870");
                e.Successed = false;
                return;
            }

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["EDMRCVGB"].ToString() == "Y")
                    {

                        this.ShowMessage("TY_M_UT_72SA1811");
                        e.Successed = false;
                        return;
                    }
                }
            }

            if (!this.ShowMessage("TY_M_GB_23NAD872"))
            {
                e.Successed = false;
                return;
            }

            e.ArgData = dt;
        }
        #endregion

        #region  Description : FPS91_TY_S_UT_A4AA0236_CellDoubleClick 이벤트
        private void FPS91_TY_S_UT_A4AA0236_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYEDKB013I(this.FPS91_TY_S_UT_A4AA0236.GetValue("EDMDATE").ToString(), this.FPS91_TY_S_UT_A4AA0236.GetValue("EDMSEQ").ToString())).ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BTN61_INQ_Click(null, null);
            }
        }
        #endregion
    }
}
