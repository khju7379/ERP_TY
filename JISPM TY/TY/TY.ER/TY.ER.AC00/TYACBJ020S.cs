using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shoveling2010.SmartClient.SystemUtility.Controls;
using Shoveling2010.SmartClient.SystemUtility.Library;
using TY.Service.Library;
using TY.Service.Library.Controls;
using TY.ER.GB00;
using GrapeCity.ActiveReports;

namespace TY.ER.AC00
{
    /// <summary>
    /// K-IFRS 합계잔액시산표 프로그램입니다.
    /// 
    /// 작성자 : 김상권
    /// 작성일 : 2012.09.25 10:20
    /// 
    /// =====================================================================================
    /// 아래는 본 프로그램에 속해있는 정보에 대한 설명입니다. 
    /// 주의) JISPM의 정보가 변경되었을 경우 틀릴 수 있으니 프로그램 작성 시 참고하시기 바랍니다.
    /// =====================================================================================
    /// 
    /// # 프로시저 정보 ####
    ///  TY_P_AC_29PBP293 : K-IFRS 합계잔액시산표
    /// 
    ///  # 스프레드 정보 ####
    ///  TY_S_AC_29PBN290 : K-IFRS 합계잔액시산표
    /// 
    ///  # 알림문자 정보 ####
    ///  TY_M_AC_2422N250 : 자료가 존재하지 않습니다.
    /// 
    ///  # 필드사전 정보 ####
    ///  INQ : 조회
    ///  PRT : 출력
    ///  GSTYYMM : 시작년월
    /// </summary>
    public partial class TYACBJ020S : TYBase
    {
        #region Description : 페이지 로드
        public TYACBJ020S()
        {
            InitializeComponent();
        }

        private void TYACBJ020S_Load(object sender, System.EventArgs e)
        {
            this.FPS91_TY_S_AC_29PBN290_Sheet1.AddColumnHeaderSpanCell(0, 0, 1, 3);
            this.FPS91_TY_S_AC_29PBN290_Sheet1.AddColumnHeaderSpanCell(0, 3, 2, 1);
            this.FPS91_TY_S_AC_29PBN290_Sheet1.AddColumnHeaderSpanCell(0, 4, 1, 3);
            this.FPS91_TY_S_AC_29PBN290_Sheet1.ColumnHeader.Cells[0, 0].Value = "차변";
            this.FPS91_TY_S_AC_29PBN290_Sheet1.ColumnHeader.Cells[0, 3].Value = "계정과목";
            this.FPS91_TY_S_AC_29PBN290_Sheet1.ColumnHeader.Cells[0, 4].Value = "대변";

            SetStartingFocus(this.DTP01_GSTYYMM);
        }
        #endregion

        #region Description : 조회 버튼
        private void BTN61_INQ_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_29PBP293",
                this.DTP01_GSTYYMM.GetString().Substring(0, 6)
                );

            this.FPS91_TY_S_AC_29PBN290.SetValue(UP_SuTotal_ds(this.DbConnector.ExecuteDataSet()));

            this.SetSpreadSumRow(this.FPS91_TY_S_AC_29PBN290, "A1NMAC", "[합     계]", SumRowType.Total);
        }
        #endregion

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

                foreach (DataRow dr in ds.Tables[0].Select("A1YNTB='Y'", "I2CDAC ASC"))
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

        #region Description : 출력 버튼
        private void BTN61_PRT_Click(object sender, EventArgs e)
        {
            this.DbConnector.CommandClear();

            this.DbConnector.Attach
                (
                "TY_P_AC_29PBP293",
                this.DTP01_GSTYYMM.GetString().Substring(0, 6)
                );

            SectionReport rpt = new TYACBJ020R();
            rpt.PageSettings.Orientation = GrapeCity.ActiveReports.Document.Section.PageOrientation.Landscape;
            (new TYERGB001P(rpt, this.DbConnector.ExecuteDataTable())).ShowDialog();
        }
        #endregion
    }
}