using System;
using System.Data;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;

namespace TY.ER.HR00
{
    /// <summary>
    /// 용역직 근태현황 조회 프로그램입니다.
    /// 
    /// 작성자 : 이상현
    /// 작성일 : 2015.03.13 11:04
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_HR_53DBD663 : 용역직 근태현황 조회
    ///  TY_P_HR_53DBJ666 : 용역직 근태현황 삭제
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_HR_53DBD665 : 용역직 근태현황 조회
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
    ///  KBSABUN : 사번
    ///  EDDATE : 종료일자
    ///  STDATE : 시작일자
    /// </summary>
    public partial class TYHRKB013S : TYBase
    {
        #region Description : 폼 로드
        public TYHRKB013S()
        {
            InitializeComponent();
        }

        private void TYHRKB013S_Load(object sender, System.EventArgs e)
        {
            // 삭제 체크
            this.BTN61_REM.ProcessCheck += new TButton.CheckHandler(BTN61_REM_ProcessCheck);

            UP_Spread_Title();
        }
        #endregion

        #region Description : 조회 버튼 이벤트
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {

            bool returnValue = UP_SearchCheck();

            if (returnValue != true)
            {
                return;
            }

            this.FPS91_TY_S_HR_53DBD665.Initialize();
            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53DBD663",
                                    this.DTP01_STDATE.GetString(),
                                    this.DTP01_EDDATE.GetString(),
                                    this.CBH01_KBSABUN.GetValue().ToString());

            DataTable dt = this.DbConnector.ExecuteDataTable();

            this.FPS91_TY_S_HR_53DBD665.SetValue(dt);

            if (dt.Rows.Count == 0)
            {
                this.ShowMessage("TY_M_AC_2422N250");
            }
        }
        #endregion

        #region Description : 신규 버튼 이벤트
        private void BTN61_NEW_Click(object sender, EventArgs e)
        {
            if ((new TYHRKB013I(string.Empty, string.Empty)).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 버튼 이벤트
        private void BTN61_REM_Click(object sender, EventArgs e)
        {
            DataSet ds = ((TButton.ClickEventCheckArgs)e).ArgData as DataSet;

            this.DbConnector.CommandClear();
            this.DbConnector.Attach("TY_P_HR_53DBJ666", ds.Tables[0]);
            this.DbConnector.ExecuteTranQueryList();

            this.ShowMessage("TY_M_GB_23NAD874"); // 삭제 메세지

            this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 삭제 ProcessCheck 이벤트
        private void BTN61_REM_ProcessCheck(object sender, TButton.ClickEventCheckArgs e)
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(this.FPS91_TY_S_HR_53DBD665.GetDataSourceInclude(TSpread.TActionType.Remove, "GIDATE", "GISABUN"));

            if (ds.Tables[0].Rows.Count == 0)
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

            e.ArgData = ds;
        }
        #endregion

        #region Description : 그리드 더블클릭 이벤트
        private void FPS91_TY_S_HR_53DBD665_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if ((new TYHRKB013I(this.FPS91_TY_S_HR_53DBD665.GetValue("GIDATE").ToString(), this.FPS91_TY_S_HR_53DBD665.GetValue("GISABUN").ToString()
                                    )).ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.BTN61_INQ_Click(null, null);
        }
        #endregion

        #region Description : 스프레드 타이틀 변경
        private void UP_Spread_Title()
        {
            this.FPS91_TY_S_HR_53DBD665_Sheet1.ColumnHeaderRowCount = 2;
            this.FPS91_TY_S_HR_53DBD665_Sheet1.RowHeaderColumnCount = 1;

            this.FPS91_TY_S_HR_53DBD665_Sheet1.AddColumnHeaderSpanCell(0, 0, 2, 1);
            this.FPS91_TY_S_HR_53DBD665_Sheet1.AddColumnHeaderSpanCell(0, 1, 2, 1);
            this.FPS91_TY_S_HR_53DBD665_Sheet1.AddColumnHeaderSpanCell(0, 2, 2, 1);
            this.FPS91_TY_S_HR_53DBD665_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_HR_53DBD665_Sheet1.AddColumnHeaderSpanCell(0, 4, 2, 1);
            this.FPS91_TY_S_HR_53DBD665_Sheet1.AddColumnHeaderSpanCell(0, 5, 1, 2);
            this.FPS91_TY_S_HR_53DBD665_Sheet1.AddColumnHeaderSpanCell(0, 7, 1, 2);

            this.FPS91_TY_S_HR_53DBD665_Sheet1.ColumnHeader.Cells[0, 5].Value = "주  간";
            this.FPS91_TY_S_HR_53DBD665_Sheet1.ColumnHeader.Cells[0, 7].Value = "야  간";

            this.FPS91_TY_S_HR_53DBD665_Sheet1.ColumnHeader.Cells[1, 5].Value = "출근시간";
            this.FPS91_TY_S_HR_53DBD665_Sheet1.ColumnHeader.Cells[1, 6].Value = "퇴근시간";
            this.FPS91_TY_S_HR_53DBD665_Sheet1.ColumnHeader.Cells[1, 7].Value = "출근시간";
            this.FPS91_TY_S_HR_53DBD665_Sheet1.ColumnHeader.Cells[1, 8].Value = "퇴근시간";
            
            this.FPS91_TY_S_HR_53DBD665_Sheet1.ColumnHeader.Cells[0, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            this.FPS91_TY_S_HR_53DBD665_Sheet1.ColumnHeader.Cells[0, 7].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        }
        #endregion

        #region  Description : 조회 체크
        private bool UP_SearchCheck()
        {
            if (Convert.ToInt32(this.DTP01_STDATE.GetString().ToString()) > Convert.ToInt32(this.DTP01_EDDATE.GetString().ToString()))
            {
                this.ShowCustomMessage("시작일자는 종료일자보다 이전이어야 합니다. ", "확인", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        #endregion
    }
}
