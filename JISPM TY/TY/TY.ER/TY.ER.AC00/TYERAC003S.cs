using System;
using System.Drawing;
using TY.Service.Library;
using System.Data;
using TY.ER.GB00;
using GrapeCity.ActiveReports;
using Shoveling2010.SmartClient.SystemUtility.Controls;

namespace TY.ER.AC00
{
    /// <summary>
    /// 합계잔액시산표 프로그램입니다.
    /// 
    /// 작성자 : 김영우
    /// 작성일 : 2012.03.21 15:43
    /// </summary>
    public partial class TYERAC003S : TYBase
    {
        public TYERAC003S()
        {
            InitializeComponent();
        }

        private void TYERAC003S_Load(object sender, System.EventArgs e)
        {
            this.DTP01_YYYYMM.SetValue(DateTime.Today);
            this.FPS91_TY_S_AC_51M9Z207_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 3);
            this.FPS91_TY_S_AC_51M9Z207_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_51M9Z207_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 3);
            this.FPS91_TY_S_AC_51M9Z207_Sheet1.ColumnHeader.Cells[0, 0].Value = "차변";
            this.FPS91_TY_S_AC_51M9Z207_Sheet1.ColumnHeader.Cells[0, 3].Value = "계정과목";
            this.FPS91_TY_S_AC_51M9Z207_Sheet1.ColumnHeader.Cells[0, 4].Value = "대변";

        }

        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2BR8D694",this.DTP01_YYYYMM.GetString().Substring(0,6) );

            this.FPS91_TY_S_AC_51M9Z207.SetValue(UP_SuTotal_ds(this.DbConnector.ExecuteDataSet()));

            this.SetSpreadSumRow(this.FPS91_TY_S_AC_51M9Z207, "A1NMAC", "[합     계]", SumRowType.Total);
        }

        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach("TY_P_AC_2BR8D694",this.DTP01_YYYYMM.GetString().Substring(0,6) );

            SectionReport rpt = new TYERAC002R();
            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
            (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();

        }

        #region Description : 합계 계산
        private DataTable UP_SuTotal_ds(DataSet ds)
        {
            string sFilter = "";
            int i = 0;

            // 합계를 보여주기 위한 빈 로우 하나 생성
            DataTable table = new DataTable();
            table = ds.Tables[0].Clone();

            DataRow row;
            int nNum = ds.Tables[0].Rows.Count;

            if (nNum > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Select("", "C2CDAC ASC")) // 세목별 일때
                    table.Rows.Add(dr.ItemArray);

                nNum = table.Rows.Count; 

                row = table.NewRow();
                table.Rows.InsertAt(row, nNum);

                table.Rows[nNum]["A1NMAC"] = "[합     계]";

                sFilter = "A1TAG02 =  'Y' ";

                table.Rows[nNum]["SDR"] = ds.Tables[0].Compute("Sum(SDR)", sFilter).ToString();
                table.Rows[nNum]["SCR"] = ds.Tables[0].Compute("Sum(SDR)", sFilter).ToString();
                table.Rows[nNum]["HDAMT"] = ds.Tables[0].Compute("Sum(HDAMT)", sFilter).ToString();
                table.Rows[nNum]["HCAMT"] = ds.Tables[0].Compute("Sum(HCAMT)", sFilter).ToString();
                table.Rows[nNum]["HDSUM"] = ds.Tables[0].Compute("Sum(HDSUM)", sFilter).ToString();
                table.Rows[nNum]["HCSUM"] = ds.Tables[0].Compute("Sum(HCSUM)", sFilter).ToString();
            }

            return table;
        }
        #endregion

    }
}
