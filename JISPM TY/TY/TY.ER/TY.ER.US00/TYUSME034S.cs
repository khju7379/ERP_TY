using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library; 
using TY.Service.Library.Controls;
using TY.Service.Library.Controls.TYSpreadCellType;
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls.FpSpreadCellType;
using TY.ER.GB00;
using TY.ER.AC00;

namespace TY.ER.US00
{
    /// <summary>
    /// 선급자재 관리 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2013.02.19 09:59
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// </summary>
    public partial class TYUSME034S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSME034S()
        {
            InitializeComponent();
        }

        private void TYUSME034S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_US_95LES595.Initialize();

            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;

            string sDATE = string.Empty;

            this.FPS91_TY_S_US_95LES595.Initialize();

            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_95LES594",
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString())
                );

            dt = UP_DataTableSumRow(this.DbConnector.ExecuteDataTable());

            this.FPS91_TY_S_US_95LES595.SetValue(dt);

            for (int i = 0; i < this.FPS91_TY_S_US_95LES595.ActiveSheet.RowCount; i++)
            {
                if (this.FPS91_TY_S_US_95LES595.GetValue(i, "MEDESC1").ToString() == "월별 소계")
                {
                    // 특정 ROW 글자 크기 변경
                    this.FPS91_TY_S_US_95LES595.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                    this.FPS91_TY_S_US_95LES595.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 194);
                }
            }
        }
        #endregion

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            string sSTDATE = string.Empty;
            string sEDDATE = string.Empty;

            string sDATE = string.Empty;

            DataTable dt = new DataTable();

            this.DbConnector.Attach
                (
                "TY_P_US_95LES594",
                Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                Get_Date(this.DTP01_EDDATE.GetValue().ToString())
                );

            dt = UP_DataTableSumRow(this.DbConnector.ExecuteDataTable());

            if (dt.Rows.Count > 0)
            {
                SectionReport rpt = new TYUSME034R();

                rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;

                (new TYERGB001P(rpt, dt)).ShowDialog();
            }
        }
        #endregion

        #region Description : 합 계
        private DataTable UP_DataTableSumRow(DataTable dt)
        {
            DataTable table = new DataTable();

            table = dt;

            string sMIYYMM = "";

            DataRow row;
            int nNum = table.Rows.Count;
            int i = 0;

            for (i = 1; i < nNum; i++)
            {
                if (table.Rows[i - 1]["MIYYMM"].ToString() != table.Rows[i]["MIYYMM"].ToString())
                {
                    row = table.NewRow();
                    table.Rows.InsertAt(row, i);

                    // 소 계 이름 넣기
                    table.Rows[i]["MIYYMM"] = table.Rows[i - 1]["MIYYMM"].ToString();
                    table.Rows[i]["MEDESC1"] = "월별 소계";

                    table.Rows[i]["MIMAECH"] = "";

                    sMIYYMM = " MIYYMM    = '" + table.Rows[i - 1]["MIYYMM"].ToString() + "'";

                    table.Rows[i]["MIJUNAMT"] = Get_Numeric(table.Compute("SUM(MIJUNAMT)", sMIYYMM).ToString());
                    table.Rows[i]["MIDANGAMT"] = Get_Numeric(table.Compute("SUM(MIDANGAMT)", sMIYYMM).ToString());
                    table.Rows[i]["UTAMT"] = Get_Numeric(table.Compute("SUM(UTAMT)", sMIYYMM).ToString());
                    table.Rows[i]["MIDANGJAN"] = Get_Numeric(table.Compute("SUM(MIDANGJAN)", sMIYYMM).ToString());
                    table.Rows[i]["MIDANGVAT"] = Get_Numeric(table.Compute("SUM(MIDANGVAT)", sMIYYMM).ToString());
                    table.Rows[i]["MIIPAMT"] = Get_Numeric(table.Compute("SUM(MIIPAMT)", sMIYYMM).ToString());
                    table.Rows[i]["MIMISUAMT"] = Get_Numeric(table.Compute("SUM(MIMISUAMT)", sMIYYMM).ToString());

                    nNum = nNum + 1;
                    i = i + 1;

                }
            }

            row = table.NewRow();
            table.Rows.InsertAt(row, i);
            // 소 계 이름 넣기
            table.Rows[i]["MIYYMM"] = table.Rows[i - 1]["MIYYMM"].ToString();
            table.Rows[i]["MEDESC1"] = "월별 소계";

            table.Rows[i]["MIMAECH"] = "";

            sMIYYMM = " MIYYMM    = '" + table.Rows[i - 1]["MIYYMM"].ToString() + "'";

            table.Rows[i]["MIJUNAMT"] = Get_Numeric(table.Compute("SUM(MIJUNAMT)", sMIYYMM).ToString());
            table.Rows[i]["MIDANGAMT"] = Get_Numeric(table.Compute("SUM(MIDANGAMT)", sMIYYMM).ToString());
            table.Rows[i]["UTAMT"] = Get_Numeric(table.Compute("SUM(UTAMT)", sMIYYMM).ToString());
            table.Rows[i]["MIDANGJAN"] = Get_Numeric(table.Compute("SUM(MIDANGJAN)", sMIYYMM).ToString());
            table.Rows[i]["MIDANGVAT"] = Get_Numeric(table.Compute("SUM(MIDANGVAT)", sMIYYMM).ToString());
            table.Rows[i]["MIIPAMT"] = Get_Numeric(table.Compute("SUM(MIIPAMT)", sMIYYMM).ToString());
            table.Rows[i]["MIMISUAMT"] = Get_Numeric(table.Compute("SUM(MIMISUAMT)", sMIYYMM).ToString());

            return table;
        }
        #endregion
    }
}