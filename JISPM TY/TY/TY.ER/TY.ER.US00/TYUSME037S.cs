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
    public partial class TYUSME037S : TYBase
    {
        #region Description : 페이지 로드
        public TYUSME037S()
        {
            InitializeComponent();
        }

        private void TYUSME037S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_US_963BQ692.Initialize();

            this.DTP01_STDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));
            this.DTP01_EDDATE.SetValue(DateTime.Now.ToString("yyyy-MM-dd"));

            SetStartingFocus(this.DTP01_STDATE);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.FPS91_TY_S_US_963BQ692.Initialize();

            DataTable dt = new DataTable();

            if (this.CBO01_INQOPTION.GetValue().ToString() == "1")
            {

                this.DbConnector.Attach
                    (
                    "TY_P_US_963BP690",
                    Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                    Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                    this.CBH01_GHWAJU.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_US_963BQ692.SetValue(UP_DataTableSumRow(dt));

                for (int i = 0; i < this.FPS91_TY_S_US_963BQ692.ActiveSheet.RowCount; i++)
                {
                    if (this.FPS91_TY_S_US_963BQ692.GetValue(i, "VNSANGHO").ToString() == "월별 소계")
                    {
                        // 특정 ROW 글자 크기 변경
                        //this.FPS91_TY_S_US_963BQ692.ActiveSheet.Rows[i].Font = new Font("굴림", 9, FontStyle.Bold);
                        this.FPS91_TY_S_US_963BQ692.ActiveSheet.Rows[i].BackColor = Color.FromArgb(218, 239, 194);
                    }
                }
            }
            else
            {
                string sMIYYMM = this.DTP01_STDATE.GetValue().ToString() + " ~ " + this.DTP01_EDDATE.GetValue().ToString();

                this.DbConnector.Attach
                    (
                    "TY_P_US_AB4E6124",
                    sMIYYMM,
                    Get_Date(this.DTP01_STDATE.GetValue().ToString()),
                    Get_Date(this.DTP01_EDDATE.GetValue().ToString()),
                    this.CBH01_GHWAJU.GetValue().ToString()
                    );

                dt = this.DbConnector.ExecuteDataTable();

                this.FPS91_TY_S_US_963BQ692.SetValue(dt);

                if (this.FPS91_TY_S_US_963BQ692.CurrentRowCount > 0)
                {
                    this.SpreadSumRowAdd(this.FPS91_TY_S_US_963BQ692, "VNSANGHO", "합 계", SumRowType.Total, "MIDANGAMT", "MIDANGVAT", "HAP");
                }
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
                    table.Rows[i]["MIHWAJU"] = "";

                    table.Rows[i]["VNSANGHO"] = "월별 소계";

                    sMIYYMM = " MIYYMM    = '" + table.Rows[i - 1]["MIYYMM"].ToString() + "'";

                    table.Rows[i]["MIDANGAMT"] = Get_Numeric(table.Compute("SUM(MIDANGAMT)", sMIYYMM).ToString());
                    table.Rows[i]["MIDANGVAT"] = Get_Numeric(table.Compute("SUM(MIDANGVAT)", sMIYYMM).ToString());
                    table.Rows[i]["HAP"] = Get_Numeric(table.Compute("SUM(HAP)", sMIYYMM).ToString());

                    nNum = nNum + 1;
                    i = i + 1;

                }
            }

            row = table.NewRow();
            table.Rows.InsertAt(row, i);
            // 소 계 이름 넣기
            table.Rows[i]["MIYYMM"] = table.Rows[i - 1]["MIYYMM"].ToString();
            table.Rows[i]["MIHWAJU"] = "";

            table.Rows[i]["VNSANGHO"] = "월별 소계";

            sMIYYMM = " MIYYMM    = '" + table.Rows[i - 1]["MIYYMM"].ToString() + "'";

            table.Rows[i]["MIDANGAMT"] = Get_Numeric(table.Compute("SUM(MIDANGAMT)", sMIYYMM).ToString());
            table.Rows[i]["MIDANGVAT"] = Get_Numeric(table.Compute("SUM(MIDANGVAT)", sMIYYMM).ToString());
            table.Rows[i]["HAP"] = Get_Numeric(table.Compute("SUM(HAP)", sMIYYMM).ToString());

            return table;
        }
        #endregion
    }
}